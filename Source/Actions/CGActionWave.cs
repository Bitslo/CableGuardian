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

namespace CableGuardian
{
    // The current audio playback solution is not optimal judging by this:
    // http://mark-dot-net.blogspot.fi/2014/02/fire-and-forget-audio-playback-with.html  
    // But it's also not blindly stupid and seems to work fine, so I'll stick with it for now. 

    class CGActionWave : CGAction, IDisposable
    {
        const string WaveFileExtension = ".wav";

        static List<string> _AvailableWaves { get; set; } = new List<string>();
        public static IList<string> AvailableWaves { get { return _AvailableWaves.AsReadOnly(); } }

        public string ErrorMessage { get; private set; } = null;
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

        BackgroundWorker Worker = new BackgroundWorker();

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

        string _Wave;
        public string Wave
        {
            get { return _Wave; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _Wave = value;
                    InitializeWave();                                   
                }
            }
        }


        static CGActionWave()
        {
            ScanWaveFilesInFolder(Program.ExeFolder);
        }
              

        public CGActionWave(AudioDevicePool audioDevices, string wave, int volume = 100, int pan = 0)
        {
            CommonConstructor(audioDevices);

            Wave = wave;
            Volume = volume;
            Pan = pan;
        }

        public CGActionWave(AudioDevicePool audioDevices)
        {
            CommonConstructor(audioDevices);       
        }
        void CommonConstructor(AudioDevicePool audioDevices)
        {
            AudioDevices = audioDevices ?? throw new Exception("Audio device is required.");
            AudioDevices.WaveOutDeviceChanged += (s, e) => { InitializeWave(); };
            
            Worker.DoWork += DoWork;           
        }

        /// <summary>
        /// Prepares wave file for playing. Needs to be called if wavefile or audio device is changed.
        /// </summary>
        void InitializeWave()
        {
            try
            {
                DisposeNAudioComponents();
                WaveOut = new WaveOutEvent();
                Reader = new WaveFileReader(_Wave + WaveFileExtension);
                MixedWave = new WaveChannel32(Reader, VolumeF, PanF);
                WaveDurationMs = (int)MixedWave.TotalTime.TotalMilliseconds + 100; // add some buffer
                WaveOut.DeviceNumber = AudioDevices.GetWaveOutDeviceNumber();
                WaveOut.Init(MixedWave);

                Error = false;
                ErrorMessage = null;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }

               

        void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i < LoopCount; i++)
                {
                    MixedWave.Position = 0;
                    WaveOut.Play();
                    if (WaveDurationMs < 5000) // in case wave length is reported incorrectly or a long sound is loaded
                        Thread.Sleep(WaveDurationMs);
                    else
                        Thread.Sleep(5000);

                    WaveOut.Stop();
                    Thread.Sleep(20);
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
        }

        protected override void DeleteImplementation()
        {
            Dispose();
        }

        public static void ScanWaveFilesInFolder(string path)
        {
            _AvailableWaves.Clear();
            try
            {
                _AvailableWaves.AddRange(Directory.GetFiles(path, "*" + WaveFileExtension));
                for (int i = 0; i < _AvailableWaves.Count; i++)
                {
                    _AvailableWaves[i] = Path.GetFileNameWithoutExtension(_AvailableWaves[i]);
                }
            }
            catch (Exception)
            {
                // intentionally ignore
            }

            // add internal default waves... pistä pilkkua nimeen tai jotain 
        }
                        
        public void Play()
        {
            if (!Error && !String.IsNullOrWhiteSpace(Wave) && Enabled)
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
            if (xCGActionWaveFile != null)
            {
                base.LoadFromXml(xCGActionWaveFile);

                Wave = xCGActionWaveFile.GetElementValueTrimmed("Wave");
                Pan = xCGActionWaveFile.GetElementValueInt("Pan");
                Volume = xCGActionWaveFile.GetElementValueInt("Volume");
                LoopCount = xCGActionWaveFile.GetElementValueInt("LoopCount");
                if (LoopCount < 1)
                    LoopCount = 1;
                if (LoopCount > 9)
                    LoopCount = 9;
            }
        }

        public override XElement GetXml()
        {   
            return new XElement("CGActionWaveFile",
                                    base.GetXml().Elements(),
                                    new XElement("Wave", Wave),
                                    new XElement("Pan", Pan),
                                    new XElement("Volume", Volume),
                                    new XElement("LoopCount", LoopCount));
        }


        public override string ToString()
        {
            return (String.IsNullOrEmpty(Wave)) ? "_______" : $"\"{Wave}\"";
        }
    }
}
