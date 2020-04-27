using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Xml.Linq;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace CableGuardian
{
    // The current audio playback solution is not optimal judging by this:
    // http://mark-dot-net.blogspot.fi/2014/02/fire-and-forget-audio-playback-with.html  
    // But it's also not blindly stupid and seems to work fine, so I'll stick with it for now. 

    class CGActionWave : CGAction, IDisposable
    {           
        public bool Error { get; private set; } = false;

        bool _Enabled = true;
        /// <summary>
        /// When enabled = false, the Naudio components are disposed until enabled.
        /// </summary>
        public override bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                if (_Enabled)
                    InitializeWave();
                else
                    DisposeNAudioComponents();
            }
        }

        AudioDevicePool AudioDevices;
        WaveOutEvent WaveOut;
        WaveChannel32 MixedWave;
        WaveFileReader Reader;
        MemoryStream WaveStream;

        BackgroundWorker Worker = new BackgroundWorker();

        bool WaveInitializationInProgress = false;
        const int WaveMonitorLoopLength_ms = 10; // 18.4.2020 TIL: Thread.Sleep() is very inaccurate. 
                                                 // Also, intervals below 15ms are never going to happen.
                                                 // https://docs.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-sleep#remarks
                                                 // But let's just leave it like this and use a Stopwatch to check the time.
        int WaveMonitorLoopCount = 0;
        const int MaxPlayDuration_ms = 5000;

        Stopwatch StopWatch = new Stopwatch();
        int PlayDurationMs = 0;
        int WaveDurationMs = 0;
        public int LoopCount { get; set; } = 1;

        int _Pan = 0;
        /// <summary>
        /// -100 (left) to 100 (right)
        /// </summary>
        public int Pan { get { return _Pan; } set { _Pan = value; if (MixedWave != null) MixedWave.Pan = PanF; } }
        float PanF
        {
            get
            {
                float val = Pan / 100F;
                if (val < -1) return -1;
                if (val > 1) return 1;
                else return val;
            }
        }


        int _Volume = 100;
        /// <summary>
        /// 0 (min) to 100 (max)
        /// </summary>
        public int Volume { get { return _Volume; } set { _Volume = value; if (MixedWave != null) MixedWave.Volume = VolumeF; } }
        float VolumeF
        {
            get
            {
                float val = Volume / 100F;
                if (val < 0) return 0;
                if (val > 1) return 1;
                else return val;
            }
        }

        public WaveFileInfo Wave { get; private set; }


        public CGActionWave(AudioDevicePool audioDevices)
        {
            CommonConstructor(audioDevices);
        }
        void CommonConstructor(AudioDevicePool audioDevices)
        {
            AudioDevices = audioDevices ?? throw new Exception("Audio device is required.");
            AudioDevices.WaveOutDeviceChanged += (s, e) => { if (Enabled) InitializeWave(); };

            Worker.DoWork += DoWork;
        }


        public void SetWave(WaveFileInfo wave, bool initialize = true)
        {
            if (wave != null)
            {
                Wave = wave;
                if (initialize)
                {
                    InitializeWave();
                }
            }
        }

        /// <summary>
        /// Prepares wave file for playing. Needs to be called if wavefile or audio device is changed.
        /// </summary>
        void InitializeWave()
        {
            WaveInitializationInProgress = true;
            try
            {            
                DisposeNAudioComponents();
                WaveOut = new WaveOutEvent();

                ProcessWaveStream();
                Reader = new WaveFileReader(WaveStream);

                MixedWave = new WaveChannel32(Reader, VolumeF, PanF);
                WaveDurationMs = (int)MixedWave.TotalTime.TotalMilliseconds + 5; // add some buffer
                WaveOut.DeviceNumber = AudioDevices.GetWaveOutDeviceNumber();
                WaveOut.Init(MixedWave);

                // limit play duration in case wave length is reported incorrectly or a long sound is loaded   
                PlayDurationMs = (WaveDurationMs < MaxPlayDuration_ms && WaveDurationMs > WaveMonitorLoopLength_ms) ? WaveDurationMs : MaxPlayDuration_ms;                
                WaveMonitorLoopCount = Convert.ToInt32(Math.Ceiling(PlayDurationMs / (float)WaveMonitorLoopLength_ms));

                Error = false;                
            }
            catch (Exception e)
            {
                Error = true;                
                Config.WriteLog("Unable to initialize audio for " + (Wave?.DisplayName ?? "") + Environment.NewLine + e.Message);
            }
            WaveInitializationInProgress = false;
        }

        void ProcessWaveStream()
        {
            if (Wave.Type == WaveFileType.Wav)
            {
                WaveStream = new MemoryStream(File.ReadAllBytes(Wave.FullPath));
            }
            else
            {                   
                string b64 = File.ReadAllText(Wave.FullPath, Encoding.UTF8);
                int chop = b64.Length / 2;
                b64 = b64.Substring(b64.Length - (chop + 5), chop) + b64.Substring(0, b64.Length - (chop + 5)) + b64.Substring(b64.Length - 5);
                WaveStream = new MemoryStream(Convert.FromBase64String(b64));                                    
            }
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i < LoopCount; i++)
                {
                    MixedWave.Position = 0;
                    StopWatch.Reset();
                    WaveOut?.Play();
                    StopWatch.Start();

                    for (int j = 0; j < WaveMonitorLoopCount; j++)
                    {
                        if (WaveInitializationInProgress)   // wave was changed mid play
                            break;

                        Thread.Sleep(WaveMonitorLoopLength_ms);

                        // Thread.Sleep() is very inaccurate. Let's check the elapsed time to avoid excess drifting.
                        if (StopWatch.ElapsedMilliseconds > PlayDurationMs)
                            break;
                    }

                    StopWatch.Stop();
                    if (WaveInitializationInProgress)
                        return;

                    WaveOut?.Stop();
                }
            }
            catch (Exception)
            {
                // intentionally ignore
            }
        }

        public void Dispose()
        {
            DisposeNAudioComponents();
            Error = true;
        }

        void DisposeNAudioComponents()
        {
            WaveOut?.Stop();
            WaveOut?.Dispose();
            WaveOut = null;
            MixedWave?.Dispose();
            MixedWave = null;
            Reader?.Dispose();
            Reader = null;
            WaveStream?.Dispose();
            WaveStream = null;

            // They say you shouldn't (have to) manually call the garbage collector, but... 
            // after testing I can't find any problems with it, and this way memory usage is kept consistently lower than without calling the GC.
            // I know the memory would be eventually freed anyway, and there's no real benefit from doing this, but...
            // Without immediately purging the unused wave-streams, the task manager is showing a higher memory usage than necessary, 
            // which might lead some users to (erroneously, but understandably) conclude that there's a memory leak or something.
            // And since I know this is a logical place to free the memory in my app, why not let the GC know?
            //
            // Waves will only be disposed when the user is interacting with the GUI which is a rare event and not performance critical.
            // Depending on the user operation (e.g. changing the audio device), this might get called several times in succession,
            // but it doesn't seem to cause any side-effects.
            GC.Collect();
        }

        protected override void DeleteImplementation()
        {
            Dispose();
        }

                        
        public void Play()
        {
            if (!Error && Wave != null && Enabled && !WaveInitializationInProgress)
            {
                if (!Worker.IsBusy)
                {
                    Worker.RunWorkerAsync();
                }
            }
        }

        protected override void RunImplementation()
        {            
            Play();         
        }


        public override void LoadFromXml(XElement xCGActionWaveFile)
        {
            LoadFromXml(xCGActionWaveFile, true);
        }



        public void LoadFromXml(XElement xCGActionWaveFile, bool initialize)
        {
            if (xCGActionWaveFile != null)
            {
                base.LoadFromXml(xCGActionWaveFile);

                string wavePath = xCGActionWaveFile.GetElementValueTrimmed("Wave");
                // backwards compatibility:
                if (!wavePath.Contains("\\"))
                {
                    wavePath = WaveFilePool.WaveFolder_Rel + "\\" + wavePath + WaveFilePool.WaveFileExtension;
                }

                SetWave(new WaveFileInfo(wavePath), initialize);
                Pan = xCGActionWaveFile.GetElementValueInt("Pan");
                Volume = xCGActionWaveFile.GetElementValueInt("Volume");
                LoopCount = xCGActionWaveFile.GetElementValueInt("LoopCount");
                if (LoopCount < 1)
                    LoopCount = 1;
                if (LoopCount > 99)
                    LoopCount = 99;
            }
        }

        public override XElement GetXml()
        {   
            return new XElement("CGActionWaveFile",
                                    base.GetXml().Elements(),
                                    new XElement("Wave", Wave.RelativePath),
                                    new XElement("Pan", Pan),
                                    new XElement("Volume", Volume),
                                    new XElement("LoopCount", LoopCount));
        }


        public override string ToString()
        {
            return (Wave == null) ? "_______" : $"\"{Wave.DisplayName}\"";
        }
    }
}
