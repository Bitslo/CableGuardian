using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Xml.Linq;

namespace CableGuardian
{
    public enum AudioDeviceSource { OculusHome, Windows, Manual }

    /// <summary>
    /// Handles audio device selection. Get currently selected device number with GetxxxxxDeviceNumber()
    /// </summary>
    class AudioDevicePool
    {
        public EventHandler<EventArgs> WaveOutDeviceChanged;

        static List<WaveOutDevice> _WaveOutDevices = new List<WaveOutDevice>();
        public static IList<WaveOutDevice> WaveOutDevices { get { return _WaveOutDevices.AsReadOnly(); } }

        bool SuppressFlaggedEvents = false;
        AudioDeviceSource _WaveOutDeviceSource = AudioDeviceSource.OculusHome;
        public AudioDeviceSource WaveOutDeviceSource
        {
            get
            {
                return _WaveOutDeviceSource;
            }
            set
            {
                bool changed = (_WaveOutDeviceSource != value);
                _WaveOutDeviceSource = value;
                if (!SuppressFlaggedEvents && changed)
                    InvokeWaveOutDeviceChanged(new EventArgs());

            }
        }

        const int WindowsWaveOutDeviceNumber = -1; // get current OS setting for wave out device
        OculusConnection Oculus;        
        WaveOutDevice ManualDevice;
                
        static AudioDevicePool()
        {
            FindWaveOutDevices();
        }


        public AudioDevicePool(OculusConnection oculus) 
        {
            Oculus = oculus;
            CommonConstructor();
        }
        
        void CommonConstructor()
        {
            if (WaveOutDevices.Count > 0)
            {
                ManualDevice = WaveOutDevices[0];
            }
        }


        static void FindWaveOutDevices()
        {
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                try
                {
                    WaveOutCapabilities device = WaveOut.GetCapabilities(i);
                    _WaveOutDevices.Add(new WaveOutDevice(i, device.ProductName));
                }
                catch (Exception)
                {
                    // intentionally ignore
                }
            }
        }

        /// <summary>
        /// Invokes all connected wave-objects to refresh their audio device from the source
        /// </summary>
        public void SendDeviceRefreshRequest()
        {
            InvokeWaveOutDeviceChanged(new EventArgs());
        }

        /// <summary>
        /// Sets the wave out device. WaveOutDeviceSource will be set to Manual.
        /// </summary>
        /// <param name=""></param>
        public void SetWaveOutDevice(WaveOutDevice device)
        {
            if (device == null)
            {
                //throw new Exception("Wave out device cannot be null.");
                // this should not happen anymore (fixed in profile loading)... but since we were not handling this exception, let's just cop out and change device source to windows
                SuppressFlaggedEvents = true;
                WaveOutDeviceSource = AudioDeviceSource.Windows;
                SuppressFlaggedEvents = false;
                return;
            }

            SuppressFlaggedEvents = true;
            WaveOutDeviceSource = AudioDeviceSource.Manual;
            SuppressFlaggedEvents = false;
            ManualDevice = device;
            InvokeWaveOutDeviceChanged(new EventArgs());
        }

        public int GetWaveOutDeviceNumber()
        {
            if (WaveOutDeviceSource == AudioDeviceSource.OculusHome && Oculus != null)
                return Oculus.WaveOutDeviceNumber;
            else if (WaveOutDeviceSource == AudioDeviceSource.Manual && ManualDevice != null)
                return ManualDevice.Number;
            else
                return WindowsWaveOutDeviceNumber;
        }

        public string GetWaveOutDeviceName()
        {
            if (WaveOutDeviceSource == AudioDeviceSource.OculusHome && Oculus != null)
                return "Oculus";
            else if (WaveOutDeviceSource == AudioDeviceSource.Manual && ManualDevice != null)
                return ManualDevice.Name;
            else
                return "Windows";
        }

        /// <summary>
        /// returns the WaveOutDevice object that has a matching Name -property
        /// </summary>
        /// <param name="name"></param>
        public static WaveOutDevice GetWaveOutDevice(string name)
        {
            return WaveOutDevices.Where(d => d.Name == name).FirstOrDefault();            
        }

        void InvokeWaveOutDeviceChanged(EventArgs e)
        {
            if (WaveOutDeviceChanged != null)
            {
                WaveOutDeviceChanged(this, e);
            }
        }

        public void LoadFromXml(XElement xAudioDevicePool)
        {
            if (xAudioDevicePool != null)
            {
                if (Enum.TryParse(xAudioDevicePool.GetElementValueTrimmed("WaveOutDeviceSource"), out AudioDeviceSource src))
                    WaveOutDeviceSource = src;

                string waveOutName = xAudioDevicePool.GetElementValueTrimmed("WaveOutDeviceName");
                if (String.IsNullOrWhiteSpace(waveOutName))
                {
                    WaveOutDevice device = WaveOutDevices.Where(d => d.Name == waveOutName).FirstOrDefault();
                    ManualDevice = device ?? WaveOutDevices.FirstOrDefault();
                }
            }
        }

        public XElement GetXml()
        {
            return new XElement("AudioDevicePool",                                   
                                   new XElement("WaveOutDeviceSource", WaveOutDeviceSource.ToString()),
                                   new XElement("WaveOutDeviceName", ManualDevice?.Name)
                                   );
        }

    }
}
