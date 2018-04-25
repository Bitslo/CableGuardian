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
        public EventHandler<ChangeEventArgs> ChangeMade;
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

            labelPeak.Text = "peak full rotations \u2265";
            labelFullRotMax.Text = "AND   full rotations \u2264";
            TTip.SetToolTip(numericUpDownFullRot, "Number of full (360) rotations to one side.");
            TTip.SetToolTip(numericUpDownFullRotMax, $"Full rotation upper limit. Trigger won't fire when rotation count is higher than this value.");
            TTip.SetToolTip(comboBoxAccu,$"Movement direction at trigger point.{Environment.NewLine}" +
                                        $"For example if you only want to be alerted when your current movement direction is increasing the cable twisting. ");
            TTip.SetToolTip(numericUpDownPeak, $"Peak number of full (360) rotations to one side.{Environment.NewLine}" +
                                                $"When returning to reset position, the trigger will fire (once) only if this number has been reached.");
            TTip.SetToolTip(comboBoxSide, "Direction of the overall rotation from reset position. ( = the direction where cable twisting increases)");            
                       
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
            numericUpDownFullRot.ValueChanged += NumericUpDownFullRot_ValueChanged;
            numericUpDownFullRotMax.ValueChanged += NumericUpDownFullRotMax_ValueChanged;
            numericUpDownPeak.ValueChanged += NumericUpDownPeak_ValueChanged;

        }

        private void NumericUpDownFullRotMax_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Condition.TargetFullRotationsMax = (uint)numericUpDownFullRotMax.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownFullRotMax));
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

            Condition.TargetPeakFullRotations = (uint)numericUpDownPeak.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownPeak));
        }

        private void NumericUpDownFullRot_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Condition.TargetFullRotations = (uint)numericUpDownFullRot.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownFullRot));
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

            CheckFullRotMaxVisibility();
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
            numericUpDownFullRot.Value = Condition.TargetFullRotations;
            numericUpDownFullRotMax.Value = Condition.TargetFullRotationsMax;
            numericUpDownPeak.Value = Condition.TargetPeakFullRotations;

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
                numericUpDownFullRot.Value = 0;
                numericUpDownFullRotMax.Value = 99;
                ShowOnlyResetPositionConditions(true);
            }
            else
            {
                ShowOnlyResetPositionConditions(false);
                numericUpDownPeak.Value = 0;
            }
        }

        void CheckFullRotMaxVisibility()
        {
            if (Condition == null)
                return;

            if (Condition.CompOperator == CompareOperator.Equal || Condition.ParentTrigger.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
            {                
                panelFullRotMax.Visible = false;
                numericUpDownFullRotMax.Value = 99;
            }
            else
            {                
                panelFullRotMax.Visible = true;
            }
        }

        void ShowOnlyResetPositionConditions(bool show)
        {
            // this order is not trivial as it causes resizing of the control  // should suppress painting during change
            panelRotSide.Visible = !show;
            panelPeak.Visible = show; 
            panelFullRot.Visible = !show;
            CheckFullRotMaxVisibility();
            panelAccu.Visible = !show;            
        }

        void InvokeChangeMade(ChangeEventArgs e)
        {
            if (ChangeMade != null)
            {
                ChangeMade(this, e);
            }
        }

    }
}
