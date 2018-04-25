using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CableGuardian
{
    class TriggeredAction : IDisposable
    {
        public Trigger TheTrigger { get; private set; }
        public CGAction TheAction { get; private set; }
        
        public Profile ParentProfile { get; private set; }        

        /// <summary>
        /// When enabled, trigger will fire on tracker events. Otherwise not.
        /// TheAction will also receive enabled status for any suppressive actions that might be needed.
        /// </summary>
        public bool Enabled { get { return TheTrigger.Enabled; } set { TheTrigger.Enabled = value; TheAction.Enabled = value; } }
                
        public TriggeredAction(Trigger trigger, CGAction action, Profile parentProfile)
        {                        
            CommonConstructor(trigger,parentProfile);
            TheAction = action;            
        }

        /// <summary>
        /// You need to call LoadFromXml() after using this constructor
        /// </summary>
        /// <param name="tracker"></param>
        /// <param name="parentProfile"></param>
        public TriggeredAction(YawTracker tracker, Profile parentProfile)
        {
            CommonConstructor(new Trigger(tracker),parentProfile);
        }

        void CommonConstructor(Trigger trig, Profile parentProfile)
        {
            ParentProfile = parentProfile;
            ParentProfile.AddAction(this);
            TheTrigger = trig;
            TheTrigger.Fire += OnTriggerFire;
        }

        public void Dispose()
        {   
            ParentProfile = null;

            TheTrigger.Delete();
            TheAction?.Delete();            
        }

        void OnTriggerFire(Object sender,  RotationEventArgs e)
        {
            TheAction?.Run();
        }

        public void LoadFromXml(XElement xTriggeredAction)
        {
            if (TheAction != null)
                throw new Exception("Instance has already been initialilzed.");

            if (xTriggeredAction != null)
            {                
                TheTrigger.LoadFromXml(xTriggeredAction.Element("Trigger"));
                
                XElement xWaveFile = xTriggeredAction.Element("CGActionWaveFile");
                if (xWaveFile != null)
                {
                    CGActionWave wav = new CGActionWave(FormMain.WaveOutPool);
                    wav.LoadFromXml(xWaveFile);
                    TheAction = wav;                    
                }
            }
        }

        public XElement GetXml()
        {
            return new XElement("TriggeredAction",
                               TheTrigger.GetXml(),
                               TheAction?.GetXml()
                                );
        }

        public override string ToString()
        {
            string output = ((TheAction is CGActionWave) ? "PLAY " : "DO ") + TheAction.ToString();
            output += " WHEN " + TheTrigger.ToString();

            return output;
        }
    }
}
