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
    partial class ConditionEditor : UserControl
    {
        public event EventHandler<ChangeEventArgs> ChangeMade;
        public RotationCondition Condition { get; private set; }
        ToolTip TTip = new ToolTip() { AutoPopDelay = 20000 };

        const string Equal = "=";
        const string EqualOrGreaterThan = "\u2265";

        bool SkipFlaggedEventHandlers = false;
        
        public ConditionEditor()
        {            
            InitializeComponent();
            
            AddComboItems();    
            AddEventHandlers();

            labelPeak.Text = "peak half-turns   \u2265";
            labelHalfTurnsMax.Text = "AND    half-turns   \u2264";
            TTip.SetToolTip(numericUpDownHalfTurns, "The required number of half-turns (180\u00B0) to one side before triggering the sound.");
            TTip.SetToolTip(numericUpDownHalfTurnsMax, $"Half-turns upper limit. Trigger won't fire when half-turn count is higher than this value.");
            TTip.SetToolTip(comboBoxAccu,$"Movement direction at trigger point.{Environment.NewLine}" +
                                        $"For example if you only want to be alerted when your current movement direction is increasing the cable twisting. ");
            TTip.SetToolTip(numericUpDownPeak, $"The required PEAK number of half-turns (180\u00B0) to one side before triggering the sound.{Environment.NewLine}" +
                                                $"--> When returning to neutral orientation, the trigger will fire (once) ONLY if this number of half-turns has been reached.");
            TTip.SetToolTip(comboBoxSide, "Direction of the overall rotation from neutral orientation. ( = the direction where cable twisting increases)");            
                       
        }
              

        public void LoadCondition(RotationCondition condition)
        {
            Condition = condition;
            LoadValuesToGui();
        }

        void AddComboItems()
        {
            comboBoxOperator.Items.Add(Equal);
            comboBoxOperator.Items.Add(EqualOrGreaterThan);            

            comboBoxSide.DataSource = Enum.GetValues(typeof(Direction));
            comboBoxAccu.DataSource = Enum.GetValues(typeof(AccumulationStatus));
           
        }

        void AddEventHandlers()
        {
            comboBoxOperator.SelectedIndexChanged += ComboBoxOperator_SelectedIndexChanged;            
            comboBoxSide.SelectedIndexChanged += ComboBoxSide_SelectedIndexChanged;
            comboBoxAccu.SelectedIndexChanged += ComboBoxAccu_SelectedIndexChanged;
            numericUpDownHalfTurns.ValueChanged += NumericUpDownHalfTurns_ValueChanged;
            numericUpDownHalfTurnsMax.ValueChanged += NumericUpDownHalfTurnsMax_ValueChanged;
            numericUpDownPeak.ValueChanged += NumericUpDownPeak_ValueChanged;

        }

        private void NumericUpDownHalfTurnsMax_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Condition.TargetHalfTurnsMax = (uint)numericUpDownHalfTurnsMax.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownHalfTurnsMax));
        }

        private void ComboBoxAccu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Condition.TargetAccumulation = (AccumulationStatus)comboBoxAccu.SelectedItem;
            InvokeChangeMade(new ChangeEventArgs(comboBoxAccu));
        }

        private void NumericUpDownPeak_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Condition.TargetPeakHalfTurns = (uint)numericUpDownPeak.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownPeak));
        }

        private void NumericUpDownHalfTurns_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Condition.TargetHalfTurns = (uint)numericUpDownHalfTurns.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownHalfTurns));
        }

        private void ComboBoxSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Condition.TargetRotationSide = (Direction)comboBoxSide.SelectedItem;
            InvokeChangeMade(new ChangeEventArgs(comboBoxSide));
        }

        private void ComboBoxOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if (comboBoxOperator.SelectedItem.ToString() == Equal)
            {
                Condition.CompOperator = CompareOperator.Equal;                
            }
            else
            {
                Condition.CompOperator = CompareOperator.EqualOrGreaterThan;                
            }

            CheckHalfTurnMaxVisibility();
            InvokeChangeMade(new ChangeEventArgs(comboBoxOperator));
        }

        void LoadValuesToGui()
        {
            SkipFlaggedEventHandlers = true;

            if (Condition.CompOperator ==  CompareOperator.Equal)
                comboBoxOperator.SelectedItem = Equal;            
            else
                comboBoxOperator.SelectedItem = EqualOrGreaterThan;

            comboBoxSide.SelectedItem = Condition.TargetRotationSide;
            comboBoxAccu.SelectedItem = Condition.TargetAccumulation;
            numericUpDownHalfTurns.Value = Condition.TargetHalfTurns;
            numericUpDownHalfTurnsMax.Value = Condition.TargetHalfTurnsMax;
            numericUpDownPeak.Value = Condition.TargetPeakHalfTurns;

            SkipFlaggedEventHandlers = false;

            SetControlVisibilityAndDefaults();
        }
                
        public void SetControlVisibilityAndDefaults()
        {
            if (Condition == null)
                return;

            if (Condition.ParentTrigger.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
            {                
                comboBoxSide.SelectedItem = Direction.Either;
                comboBoxOperator.SelectedItem = EqualOrGreaterThan;
                comboBoxAccu.SelectedItem = AccumulationStatus.Either;                
                numericUpDownHalfTurns.Value = 0;
                numericUpDownHalfTurnsMax.Value = 99;
                ShowOnlyResetPositionConditions(true);
            }
            else
            {
                ShowOnlyResetPositionConditions(false);
                numericUpDownPeak.Value = 0;
            }
        }

        void CheckHalfTurnMaxVisibility()
        {
            if (Condition == null)
                return;

            if (Condition.CompOperator == CompareOperator.Equal || Condition.ParentTrigger.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
            {                
                panelHalfTurnsMax.Visible = false;
                numericUpDownHalfTurnsMax.Value = 99;
            }
            else
            {                
                panelHalfTurnsMax.Visible = true;
            }
        }

        void ShowOnlyResetPositionConditions(bool show)
        {
            // this order is not trivial as it causes resizing of the control  // should suppress painting during change
            panelRotSide.Visible = !show;
            panelPeak.Visible = show; 
            panelHalfTurns.Visible = !show;
            CheckHalfTurnMaxVisibility();
            panelAccu.Visible = !show;            
        }

        void InvokeChangeMade(ChangeEventArgs e)
        {
            ChangeMade?.Invoke(this, e);
        }

    }
}
