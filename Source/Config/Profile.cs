using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CableGuardian 
{
    class Profile : IDisposable
    {
        List<TriggeredAction> _Actions { get; set; } = new List<TriggeredAction>();
        public IList<TriggeredAction> Actions { get { return _Actions.AsReadOnly(); } }
        public VRAPI API { get; set; } = VRAPI.OculusVR;
        public AudioDeviceSource WaveOutDeviceSource { get; set; } = AudioDeviceSource.OculusHome;
        public WaveOutDevice TheWaveOutDevice { get; set; }
        public bool OriginalWaveOutDeviceNotFound { get; private set; }
        public string NotFoundDeviceName { get; private set; }
        public bool RequireHome { get; set; } = false;        
        public string Name { get; set; }
        public bool Frozen { get; set; }
        public bool ResetOnMount { get; set; } = false;
        public bool PlayMountingSound { get; set; } = false;
        public CGActionWave MountingSound { get; private set; } = new CGActionWave(FormMain.WaveOutPool);

        public Profile(VRAPI api = VRAPI.OculusVR)
        {
            Name = "New Profile";
            API = api;
            WaveOutDeviceSource = (api == VRAPI.OculusVR) ? AudioDeviceSource.OculusHome : AudioDeviceSource.Windows;

            // default jingle:
            MountingSound.SetWave(new WaveFileInfo(WaveFilePool.DefaultAudioFolder_Rel + "\\CG_Jingle" + WaveFilePool.CgAudioExtension));
            MountingSound.Pan = 0;
            MountingSound.Volume = 50;
            MountingSound.LoopCount = 1;
        }        

        public void AddAction(TriggeredAction action)
        {
            if (action.ParentProfile != null && action.ParentProfile != this)
                throw new Exception("This action already belongs to another profile");

            if (!Actions.Contains(action))
            {
                _Actions.Add(action);
            }        
        }

        public void DeleteAction(TriggeredAction action)
        {
            if (Actions.Contains(action))
            {
                _Actions.Remove(action);
                action.Dispose();                
            }
        }

        public void Dispose()
        {
            List<TriggeredAction> toDelete = Actions.ToList();
            foreach (var item in toDelete)
            {
                item.Dispose();
            }
            
            MountingSound.Dispose();
        }

        public void Activate()
        {
            foreach (var item in Actions)
            {
                item.Enabled = true;
            }

            MountingSound.Enabled = true;
        }

        public void Deactivate()
        {
            foreach (var item in Actions)
            {
                item.Enabled = false;
            }
            
            MountingSound.Enabled = false;
        }

        public void LoadFromXml(XElement xUserProfile)
        {
            if (xUserProfile != null)
            {
                Name = xUserProfile.GetElementValueTrimmed("Name");                
                Frozen = xUserProfile.GetElementValueBool("Frozen");                
                RequireHome = xUserProfile.GetElementValueBool("RequireHome");
                ResetOnMount = xUserProfile.GetElementValueBool("ResetOnMount");

                XElement xMountingSound = null;
                if (xUserProfile.GetElementValueOrNull("PlayMountingSound") == null)
                {
                    PlayMountingSound = Config.PlayMountingSound_Legacy; // backwards compatibility
                    xMountingSound = Config.JingleXML_Legacy;
                    Config.SaveProfilesAtStartup = true;
                }
                else
                {
                    PlayMountingSound = xUserProfile.GetElementValueBool("PlayMountingSound");
                    xMountingSound = xUserProfile.Element("MountingSound");
                }
                MountingSound.LoadFromXml(xMountingSound?.Element("CGActionWaveFile"));

                if (xUserProfile.GetElementValueOrNull("API") == null) 
                {
                    API = Config.LegacyAPI; // backwards compatibility, API was in config-xml before
                }
                else
                {
                    if (Enum.TryParse(xUserProfile.GetElementValueTrimmed("API"), out VRAPI a))
                        API = a;
                    else
                        API = VRAPI.OculusVR;
                }

                string wout = xUserProfile.GetElementValueTrimmed("WaveOutDeviceSource");
                if (Enum.TryParse(wout, out AudioDeviceSource ads))
                    WaveOutDeviceSource = ads;
                else
                {
                    if (String.Compare(wout,"Oculus", true) == 0) // backwards compatibility                    
                        WaveOutDeviceSource = AudioDeviceSource.OculusHome;                    
                    else
                        WaveOutDeviceSource = AudioDeviceSource.Windows;
                }
                
                string waveOutName = xUserProfile.GetElementValueTrimmed("WaveOutDeviceName");                
                TheWaveOutDevice = AudioDevicePool.GetWaveOutDevice(waveOutName);

                OriginalWaveOutDeviceNotFound = false;
                if (TheWaveOutDevice == null && WaveOutDeviceSource == AudioDeviceSource.Manual)
                {
                    OriginalWaveOutDeviceNotFound = true;
                    NotFoundDeviceName = waveOutName;
                    if (API == VRAPI.OculusVR)
                        WaveOutDeviceSource = AudioDeviceSource.OculusHome;
                    else
                        WaveOutDeviceSource = AudioDeviceSource.Windows;
                }

                foreach (var trig in xUserProfile.Descendants().Where(element => element.Name == "TriggeredAction"))
                {
                    TriggeredAction newAct = new TriggeredAction(FormMain.Tracker, this);
                    newAct.LoadFromXml(trig, false);                    
                }
            }
        }

        public XElement GetXml()
        {
            return new XElement("Profile",
                                   new XElement("Name", Name),
                                   new XElement("Frozen", Frozen),                                   
                                   new XElement("RequireHome", RequireHome),
                                   new XElement("ResetOnMount", ResetOnMount),
                                   new XElement("PlayMountingSound", PlayMountingSound),
                                   new XElement("MountingSound", MountingSound.GetXml()),
                                   new XElement("API", API),
                                   new XElement("WaveOutDeviceSource", WaveOutDeviceSource),
                                   new XElement("WaveOutDeviceName", TheWaveOutDevice?.Name),
                                   new XElement("Actions", from TriggeredAction t in Actions select t.GetXml())
                                   );
        }

        public override string ToString()
        {
            return Name + ((Config.StartUpProfile == this) ? " [startup]" : "");
        }

    }
}
