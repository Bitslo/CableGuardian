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
        public bool RequireHome { get; set; } = false;        
        public string Name { get; set; }
        public bool Frozen { get; set; }

        public Profile()
        {
            Name = "New Profile";            
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
        }

        public void Activate()
        {
            foreach (var item in Actions)
            {
                item.Enabled = true;
            }
        }

        public void DeActivate()
        {
            foreach (var item in Actions)
            {
                item.Enabled = false;
            }
        }

        public void LoadFromXml(XElement xUserProfile)
        {
            if (xUserProfile != null)
            {
                Name = xUserProfile.GetElementValueTrimmed("Name");
                Frozen = xUserProfile.GetElementValueBool("Frozen");                
                RequireHome = xUserProfile.GetElementValueBool("RequireHome");

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

                foreach (var trig in xUserProfile.Descendants().Where(element => element.Name == "TriggeredAction"))
                {
                    TriggeredAction newAct = new TriggeredAction(FormMain.Tracker, this);
                    newAct.LoadFromXml(trig);                    
                }
            }
        }

        public XElement GetXml()
        {
            return new XElement("Profile",
                                   new XElement("Name", Name),
                                   new XElement("Frozen", Frozen),                                   
                                   new XElement("RequireHome", RequireHome),
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
