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
    

    partial class ProfileEditor : UserControl
    {
        public EventHandler<ChangeEventArgs> ChangeMade;
        public EventHandler<EventArgs> ProfileNameChanged;

        Profile TheProfile;
        ToolTip TTip = new ToolTip() { AutoPopDelay = 20000 };
        /// <summary>
        /// Don't disable event handlers before method calls. Use only within a single method when accessing properties.
        /// </summary>
        bool SkipFlaggedEventHandlers = false;
        

        public ProfileEditor()
        {
            InitializeComponent();                        
                        
            comboBoxDeviceSource.DataSource = Enum.GetValues(typeof(AudioDeviceSource));
            comboBoxManual.DataSource = AudioDevicePool.WaveOutDevices;

            TTip.SetToolTip(checkBoxStartup,"Load this profile when program starts");
            TTip.SetToolTip(checkBoxFreeze, "Freeze profile to prevent accidental changes");
            TTip.SetToolTip(pictureBoxPlus, "Add a new action");
            TTip.SetToolTip(comboBoxDeviceSource, "How to determine the audio device for playing the waves: Oculus Home, Windows or manual setting.");
            TTip.SetToolTip(labelOcuChanges, $"Changes in Oculus Home audio settings are only updated to {Config.ProgramTitle} when you change the audio settings in {Config.ProgramTitle}  ");

            if (!FormMain.RunFromDesigner)
            {   
                InitializeAppearance();
            }
                       
            AddEventHandlers();    
        }

        void InitializeAppearance()
        {   
            labelAddHint.ForeColor = Config.CGColor;
            listBoxActions.BackColor = Config.CGBackColor;
            listBoxActions.ForeColor = Config.CGColor;
        }

        void AddEventHandlers()
        {
            checkBoxFreeze.CheckedChanged += CheckBoxFreeze_CheckedChanged;
            comboBoxDeviceSource.SelectedIndexChanged += ComboBoxDeviceSource_SelectedIndexChanged;
            comboBoxManual.SelectedIndexChanged += ComboBoxManual_SelectedIndexChanged;
            textBoxName.TextChanged += TextBoxName_TextChanged;
            pictureBoxPlus.Click += PictureBoxPlus_Click;
            pictureBoxMinus.Click += PictureBoxMinus_Click;            
            listBoxActions.SelectedIndexChanged += ListBoxActions_SelectedIndexChanged;
            checkBoxStartup.CheckedChanged += CheckBoxStartup_CheckedChanged;            
            listBoxActions.DrawItem += listBoxActions_DrawItem;
            WaveActionCtl.ChangeMade += OnActionControlChangeMade;
        }

        private void ComboBoxManual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            WaveOutDevice dev = comboBoxManual.SelectedItem as WaveOutDevice;
            if (dev != null)
            {
                FormMain.WaveOutPool.SetWaveOutDevice(dev);
                TheProfile.TheWaveOutDevice = dev;
            }

            InvokeChangeMade(new ChangeEventArgs(comboBoxManual));
        }

        void OnActionControlChangeMade(object sender, ChangeEventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if (e.OriginalSender is TrackBar == false)            
                RefreshActionsListbox();

            InvokeChangeMade(e);
        }
                
        private void listBoxActions_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lst = sender as ListBox;
            string txt = (e.Index >= 0) ? lst.Items[e.Index].ToString() : "";
            string line = "_____________________________________________________________________________________________________________________________________________";

            e.DrawBackground();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {   
                e.Graphics.DrawString(txt, this.Font, SystemBrushes.HighlightText, e.Bounds.Left + 2, e.Bounds.Top + 8);
                if (!String.IsNullOrWhiteSpace(txt))
                    e.Graphics.DrawString(line, this.Font, Brushes.Gray, e.Bounds.Left - 2, e.Bounds.Top + 17);
            }
            else
            {
                using (SolidBrush br = new SolidBrush(e.ForeColor))                
                {             
                    e.Graphics.DrawString(txt, this.Font, br, e.Bounds.Left + 2, e.Bounds.Top + 8);
                    if (!String.IsNullOrWhiteSpace(txt))
                        e.Graphics.DrawString(line, this.Font, Brushes.Gray, e.Bounds.Left - 2, e.Bounds.Top + 17);
                }
            }
        }

        private void CheckBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if (checkBoxStartup.Checked)
                Config.StartUpProfile = TheProfile;
            else
                Config.StartUpProfile = null;

            InvokeProfileNameChanged(new EventArgs()); // to refresh profiles combo
            InvokeChangeMade(new ChangeEventArgs(checkBoxStartup));
        }

        private void PictureBoxMinus_Click(object sender, EventArgs e)
        {
            TriggeredAction selAction = listBoxActions.SelectedItem as TriggeredAction;
            if (selAction != null)
            {
                int index = listBoxActions.SelectedIndex;
                DeleteAction(selAction);
                if (listBoxActions.Items.Count > 0)
                {
                    listBoxActions.SelectedIndex = (listBoxActions.Items.Count > index) ? index : index-1;
                }
                SetControlVisibility();
                InvokeChangeMade(new ChangeEventArgs(pictureBoxMinus));
            }
        }

        private void ListBoxActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TriggeredAction ta = listBoxActions.SelectedItem as TriggeredAction;
            if (ta != null)
            {
                SkipFlaggedEventHandlers = true;
                WaveActionCtl.LoadWaveAction(ta);
                SkipFlaggedEventHandlers = false;
            }
        }
                
        private void PictureBoxPlus_Click(object sender, EventArgs e)
        {
            AddWaveAction();
            SetControlVisibility();
            InvokeChangeMade(new ChangeEventArgs(pictureBoxPlus));
        }

        void AddWaveAction()
        {            
            Trigger t = new Trigger(FormMain.Tracker);
            CGActionWave a = new CGActionWave(FormMain.WaveOutPool);
            TriggeredAction ta = new TriggeredAction(t, a, TheProfile);

            RefreshActionsListbox();
            SkipFlaggedEventHandlers = true;
            listBoxActions.SelectedItem = ta;
            SkipFlaggedEventHandlers = false;
            WaveActionCtl.LoadWaveAction(ta);

        }

        private void ComboBoxDeviceSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TheProfile.WaveOutDeviceSource = (AudioDeviceSource)comboBoxDeviceSource.SelectedItem;
            FormMain.WaveOutPool.WaveOutDeviceSource = (AudioDeviceSource)comboBoxDeviceSource.SelectedItem;

            SetControlVisibility();

            if (TheProfile.WaveOutDeviceSource == AudioDeviceSource.Manual)
            {
                WaveOutDevice dev = comboBoxManual.SelectedItem as WaveOutDevice;
                if (dev != null)
                {
                    FormMain.WaveOutPool.SetWaveOutDevice(dev);
                    TheProfile.TheWaveOutDevice = dev;
                }
            }
                        
            InvokeChangeMade(new ChangeEventArgs(comboBoxDeviceSource));
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            string newName = textBoxName.Text;

            if (!String.IsNullOrEmpty(newName) && (from Profile p in Config.Profiles where p.Name == newName && p != TheProfile select p).Count() == 0)
            {
                TheProfile.Name = newName;
                InvokeProfileNameChanged(new EventArgs());
                InvokeChangeMade(new ChangeEventArgs(textBoxName));
            }
        }
            

        private void CheckBoxFreeze_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TheProfile.Frozen = checkBoxFreeze.Checked;
            CheckProfileFrozenStatus();
            InvokeChangeMade(new ChangeEventArgs(checkBoxFreeze));
        }

        public void LoadProfile(Profile profile)
        {
            TheProfile = profile;
            
            LoadValuesToGui();
        }

        void LoadValuesToGui()
        {
            SkipFlaggedEventHandlers = true;
            checkBoxFreeze.Checked = TheProfile.Frozen;
            checkBoxStartup.Checked = (Config.StartUpProfile == TheProfile);
            comboBoxDeviceSource.SelectedItem = TheProfile.WaveOutDeviceSource;            
            FormMain.WaveOutPool.WaveOutDeviceSource = TheProfile.WaveOutDeviceSource;
            if (TheProfile.WaveOutDeviceSource == AudioDeviceSource.Manual)
            {
                comboBoxManual.SelectedItem = TheProfile.TheWaveOutDevice;
                FormMain.WaveOutPool.SetWaveOutDevice(TheProfile.TheWaveOutDevice);
            }            

            textBoxName.Text = TheProfile.Name;
            SkipFlaggedEventHandlers = false;

            LoadActions();
            SetControlVisibility();
            CheckProfileFrozenStatus();
        }

        void LoadActions()
        {            
            RefreshActionsListbox();
            if (listBoxActions.Items.Count > 0)
            {
                TriggeredAction first = listBoxActions.Items[0] as TriggeredAction;
                SkipFlaggedEventHandlers = true;
                listBoxActions.SelectedItem = first;
                WaveActionCtl.LoadWaveAction(first);
                SkipFlaggedEventHandlers = false;
            }
            
        }

                      
        void DeleteAction(TriggeredAction action)
        {
            if (action == null)
                throw new Exception("null action cannot be deleted");
            
            TheProfile.DeleteAction(action);
            RefreshActionsListbox();            
        }

        void RefreshActionsListbox()
        {
            SkipFlaggedEventHandlers = true;

            object selected = listBoxActions.SelectedItem;

            listBoxActions.DataSource = null;
            listBoxActions.DataSource = TheProfile.Actions;

            if (selected != null && listBoxActions.Items.Contains(selected))
            {
                listBoxActions.SelectedItem = selected;
            }

            SkipFlaggedEventHandlers = false;
        }


        void SetControlVisibility()
        {
           
            labelOcuChanges.Visible = (TheProfile.WaveOutDeviceSource == AudioDeviceSource.Oculus);
            comboBoxManual.Visible = (TheProfile.WaveOutDeviceSource == AudioDeviceSource.Manual);
            labelAddHint.Visible = (listBoxActions.Items.Count == 0);
            WaveActionCtl.Visible = !(listBoxActions.Items.Count == 0);
        }

        void CheckProfileFrozenStatus()
        {            
            foreach (Control item in Controls)
            {
                if (item != checkBoxFreeze && item != checkBoxStartup)
                {
                    item.Enabled = !TheProfile.Frozen;
                }                
            }

            if (TheProfile.Frozen)
            {
                listBoxActions.SelectedItem = null;
            }
        }

        void InvokeProfileNameChanged(EventArgs e)
        {
            if (ProfileNameChanged != null)
            {
                ProfileNameChanged(this, e);
            }
        }

        void InvokeChangeMade(ChangeEventArgs e)
        {
            if (ChangeMade != null)
            {
                ChangeMade(this, e);
            }
        }

    }

    class ChangeEventArgs : EventArgs
    {
        /// <summary>
        /// The (sub)control that originally invoked the event
        /// </summary>
        public Control OriginalSender { get; }
        public ChangeEventArgs(Control originalSender)
        {
            OriginalSender = originalSender;
        }
    }
}
