using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CableGuardian
{
    class Trigger
    {
        public event EventHandler<RotationEventArgs> Fire;

        protected YawTracker TriggeringTracker { get; private set; }
        //List<RotationCondition> Conditions { get; } = new List<RotationCondition>();
        public RotationCondition RotCondition { get; private set; }
        
        bool _Enabled = true;
        /// <summary>
        /// When enabled, trigger will fire on tracker events. Otherwise not.
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                RefreshTrackerEventsSubscription();
            }
        }

        uint FireCountSinceLastResetPos = 0;
        /// <summary>
        /// Max number of times the trigger can be fired until it must be reset by the tracker returning to neutral orientation.
        /// 0 = unlimited.
        /// </summary>
        public uint FireLimitPerReset { get; set; }

        YawTrackerOrientationEvent _TriggeringEvent = YawTrackerOrientationEvent.Yaw0Yaw180;
        public YawTrackerOrientationEvent TriggeringEvent 
        {
            get { return _TriggeringEvent; }
            set
            {
                _TriggeringEvent = value;
                RefreshTrackerEventsSubscription();
            }
        }
                
        public Trigger(YawTracker triggeringTracker)
        {
            TriggeringTracker = triggeringTracker;            
            RotCondition = new RotationCondition(this);
            TriggeringEvent = YawTrackerOrientationEvent.Yaw0Yaw180;
        }

        void RefreshTrackerEventsSubscription()
        {
            // remove possibly existing subscriptions to prevent duplicates
            TriggeringTracker.Yaw0 -= OnTriggeringTrackerEvent;
            TriggeringTracker.Yaw180 -= OnTriggeringTrackerEvent;
            TriggeringTracker.ResetPosition -= OnTriggeringTrackerEvent;
            TriggeringTracker.ResetPosition -= OnResetPosition;

            if (Enabled)
            {
                if (TriggeringEvent == YawTrackerOrientationEvent.Yaw0 || TriggeringEvent == YawTrackerOrientationEvent.Yaw0Yaw180)
                    TriggeringTracker.Yaw0 += OnTriggeringTrackerEvent;

                if (TriggeringEvent == YawTrackerOrientationEvent.Yaw180 || TriggeringEvent == YawTrackerOrientationEvent.Yaw0Yaw180)
                    TriggeringTracker.Yaw180 += OnTriggeringTrackerEvent;

                if (TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
                    TriggeringTracker.ResetPosition += OnTriggeringTrackerEvent;

                TriggeringTracker.ResetPosition += OnResetPosition;
            }
        }

        public void Delete()
        {
            RotCondition.Delete();
            Enabled = false;
        }
               
        
        void OnResetPosition(object sender, RotationEventArgs e)
        {            
             FireCountSinceLastResetPos = 0;            
        }

        void OnTriggeringTrackerEvent(object sender, RotationEventArgs e)
        {
            CheckConditionsAndFire(e);
        }

        public void CheckConditionsAndFire(RotationEventArgs e, bool ignoreConditions = false)
        {
            if (ConditionsAreTrue(e) || ignoreConditions)
            {
                InvokeFire(e);
                FireCountSinceLastResetPos++;
            }            
        }
         
       
        protected bool ConditionsAreTrue(RotationEventArgs e)
        {
            if (FireLimitPerReset > 0 && FireCountSinceLastResetPos >= FireLimitPerReset)
            {
                return false;
            }
            
            if (RotCondition != null)
            {
                if (RotCondition.IsTrue(e) == false)
                    return false;
            }
            
            // further conditions...

            return true;
        }

        void InvokeFire(RotationEventArgs e)
        {
            Fire?.Invoke(this, e);
        }


        public void LoadFromXml(XElement xTrackerTrigger)
        {
            if (xTrackerTrigger != null)
            {
                if (Enum.TryParse(xTrackerTrigger.GetElementValueTrimmed("TriggeringEvent"), out YawTrackerOrientationEvent trig))
                    TriggeringEvent = trig;
                else
                    TriggeringEvent =  YawTrackerOrientationEvent.Yaw0Yaw180;

                FireLimitPerReset = (uint)xTrackerTrigger.GetElementValueInt("FireLimitPerZero");

                RotCondition.LoadFromXml(xTrackerTrigger.Element("RotationCondition"));                       
            }
        }

        public XElement GetXml()
        {
            return new XElement("Trigger", 
                                new XElement("TriggeringEvent", TriggeringEvent.ToString()),
                                new XElement("FireLimitPerZero", FireLimitPerReset),
                                RotCondition.GetXml() 
                                );
        }

        public override string ToString()
        {
            string output = "";
            if (TriggeringEvent == YawTrackerOrientationEvent.Yaw0)
                output += YawTracker.S_Yaw0;
            else if (TriggeringEvent == YawTrackerOrientationEvent.Yaw180)
                output += YawTracker.S_Yaw180;
            else if (TriggeringEvent == YawTrackerOrientationEvent.Yaw0Yaw180)
                output += YawTracker.S_Yaw0Yaw180;
            else if (TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
                output += YawTracker.S_ResetPosition;

            output += (FireLimitPerReset > 0) ? $" (max {FireLimitPerReset.ToString()})" : "" ;
            output += RotCondition?.ToString() ?? "";

            return output;
        }

    }
}
