using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CableGuardian
{
    partial class TriggerEditor : UserControl
    {
        public event EventHandler<ChangeEventArgs> ChangeMade;
        public event EventHandler<EventArgs> TriggeringEventChanged;
        
        Trigger TheTrigger;
        ToolTip TTip = new ToolTip() { AutoPopDelay = 20000 };

        public TriggerEditor()
        {
            InitializeComponent();
            
            comboBoxEvent.Items.Add(YawTracker.S_ResetPosition);
            comboBoxEvent.Items.Add(YawTracker.S_Yaw0);
            comboBoxEvent.Items.Add(YawTracker.S_Yaw180);
            comboBoxEvent.Items.Add(YawTracker.S_Yaw0Yaw180);            
                                    
            comboBoxEvent.SelectedIndexChanged += ComboBoxEvent_SelectedIndexChanged;
            numericUpDownLimit.ValueChanged += NumericUpDownLimit_ValueChanged;           
            TTip.SetToolTip(numericUpDownLimit, "Max fire count for the trigger until total rotation returns to zero (neutral orientation). \n0 = no limit.");
            TTip.SetToolTip(comboBoxEvent, "Orientation to trigger the sound (if all conditions match).");

        }



        private void NumericUpDownLimit_ValueChanged(object sender, EventArgs e)
        {
            TheTrigger.FireLimitPerReset = (uint)numericUpDownLimit.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownLimit));
        }

        public void LoadTrigger(Trigger trigger)
        {
            if (trigger == null)
                throw new Exception("Trigger to edit cannot be null");

            TheTrigger = trigger;
            LoadValuesToGui();
        }

        void LoadValuesToGui()
        {
            if (TheTrigger.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
                comboBoxEvent.SelectedItem = YawTracker.S_ResetPosition;
            else if (TheTrigger.TriggeringEvent == YawTrackerOrientationEvent.Yaw0)
                comboBoxEvent.SelectedItem = YawTracker.S_Yaw0;
            else if (TheTrigger.TriggeringEvent == YawTrackerOrientationEvent.Yaw180)
                comboBoxEvent.SelectedItem = YawTracker.S_Yaw180;
            else 
                comboBoxEvent.SelectedItem = YawTracker.S_Yaw0Yaw180;

            numericUpDownLimit.Value = TheTrigger.FireLimitPerReset;

            SetControlVisibilityAndDefaults();
        }

        private void ComboBoxEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEvent.SelectedItem.ToString() == YawTracker.S_ResetPosition)            
                TheTrigger.TriggeringEvent = YawTrackerOrientationEvent.ResetPosition;
            else if (comboBoxEvent.SelectedItem.ToString() == YawTracker.S_Yaw0)
                TheTrigger.TriggeringEvent = YawTrackerOrientationEvent.Yaw0;
            else if (comboBoxEvent.SelectedItem.ToString() == YawTracker.S_Yaw180)
                TheTrigger.TriggeringEvent = YawTrackerOrientationEvent.Yaw180;
            else
                TheTrigger.TriggeringEvent = YawTrackerOrientationEvent.Yaw0Yaw180;

            SetControlVisibilityAndDefaults();
            InvokeTriggeringEventChanged(new EventArgs());
            InvokeChangeMade(new ChangeEventArgs(comboBoxEvent));
        }

        void SetControlVisibilityAndDefaults()
        {
            if (TheTrigger.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
            {
                ShowLimit(false);
                numericUpDownLimit.Value = 0;
            }
            else
                ShowLimit(true);
        }

        void ShowLimit(bool show)
        {
            labelLimit.Visible = show;
            numericUpDownLimit.Visible = show;
        }

        void InvokeTriggeringEventChanged(EventArgs e)
        {
            TriggeringEventChanged?.Invoke(this, e);
        }

        void InvokeChangeMade(ChangeEventArgs e)
        {
            ChangeMade?.Invoke(this, e);
        }
    }
}
