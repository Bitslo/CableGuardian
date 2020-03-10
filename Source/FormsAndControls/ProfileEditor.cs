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
        public EventHandler<EventArgs> VRConnectionParameterChanged;
        
        Profile TheProfile;
        ToolTip TTip = new ToolTip() { AutoPopDelay = 20000 };
        /// <summary>
        /// Don't disable event handlers before method calls. Use only within a single method when accessing properties.
        /// </summary>
        bool SkipFlaggedEventHandlers = false;

        readonly string ResetOnMountWarning = $"CAUTION!!! Enabling this will reset the turn counter each time you put on the headset.{Environment.NewLine}" +
                                         $"Usually better disabled, this setting might be useful if your headset only tracks rotation when wearing it.{Environment.NewLine}" +
                                            $"NOTE: Depending on the detection hardware and API implementation, this feature may not work as you'd expect.";

        public ProfileEditor()
        {
            InitializeComponent();

            comboBoxAPI.DataSource = Enum.GetValues(typeof(VRAPI));
            comboBoxDeviceSource.DataSource = Enum.GetValues(typeof(AudioDeviceSource));
            comboBoxManual.DataSource = AudioDevicePool.GetWaveOutDevices();

            TTip.SetToolTip(comboBoxAPI, $"Choose {VRAPI.OculusVR} for Oculus headsets, {VRAPI.OpenVR} for others.{Environment.NewLine}"
                                            + $"If you switch between different headsets (with different API / Audio device), it is recommended to make a separate profile for each.{Environment.NewLine}" 
                                            + "You can use the \"Clone profile\" -button to quickly copy the rotation settings to another profile.");
            TTip.SetToolTip(checkBoxStartup,$"Load this profile when program starts.{Environment.NewLine}" 
                                            + "If a startup profile has not been defined, the previously used profile will be loaded.");
            TTip.SetToolTip(checkBoxFreeze, "Freeze profile to prevent accidental changes.");
            TTip.SetToolTip(pictureBoxPlus, "Add a new action");
            TTip.SetToolTip(pictureBoxClone, "Clone the selected action");
            TTip.SetToolTip(pictureBoxMinus, "Remove the selected action");
            TTip.SetToolTip(comboBoxDeviceSource, "Source for the audio device for playing the waves in this profile.");
            TTip.SetToolTip(labelOcuChanges, $"Change in Oculus Home audio device is only updated to {Config.ProgramTitle} at startup OR when you change the device here.");            
            TTip.SetToolTip(checkBoxHome, $"When checked, the headset orientation is polled only when Oculus Home is running. This minimizes CPU usage for those non-VR moments. {Environment.NewLine}" +
                                         $"The presence of Home is polled once in two seconds.");
            TTip.SetToolTip(checkBoxResetOnMount, ResetOnMountWarning);

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
            comboBoxAPI.SelectedIndexChanged += ComboBoxAPI_SelectedIndexChanged;
            checkBoxHome.CheckedChanged += CheckBoxHome_CheckedChanged;
            checkBoxResetOnMount.CheckedChanged += CheckBoxResetOnMount_CheckedChanged;
            checkBoxFreeze.CheckedChanged += CheckBoxFreeze_CheckedChanged;
            comboBoxDeviceSource.SelectedIndexChanged += ComboBoxDeviceSource_SelectedIndexChanged;
            comboBoxManual.SelectedIndexChanged += ComboBoxManual_SelectedIndexChanged;
            textBoxName.TextChanged += TextBoxName_TextChanged;
            pictureBoxPlus.Click += PictureBoxPlus_Click;
            pictureBoxClone.Click += PictureBoxClone_Click;
            pictureBoxMinus.Click += PictureBoxMinus_Click;            
            listBoxActions.SelectedIndexChanged += ListBoxActions_SelectedIndexChanged;
            checkBoxStartup.CheckedChanged += CheckBoxStartup_CheckedChanged;            
            listBoxActions.DrawItem += listBoxActions_DrawItem;
            WaveActionCtl.ChangeMade += OnActionControlChangeMade;
            comboBoxManual.DropDown += ComboBoxManual_DropDown;


            KeyUp += AnyControl_KeyUp;
            foreach (Control ctl in Controls)
            {
                if (ctl is TextBox == false && ctl is CheckBox == false)
                    ctl.KeyUp += AnyControl_KeyUp;
            }

        }

        private void CheckBoxResetOnMount_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if (checkBoxResetOnMount.Checked)
                MessageBox.Show(this, ResetOnMountWarning.Replace(Environment.NewLine, Environment.NewLine + Environment.NewLine), Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            TheProfile.ResetOnMount = checkBoxResetOnMount.Checked;
            InvokeChangeMade(new ChangeEventArgs(sender as Control));
        }

        private void ComboBoxManual_DropDown(object sender, EventArgs e)
        {
            RefreshManualDeviceCombo();
        }

        void RefreshManualDeviceCombo()
        {
            WaveOutDevice previouslySelected = (comboBoxManual.SelectedItem as WaveOutDevice);

            SkipFlaggedEventHandlers = true;
            Enabled = false;
            comboBoxManual.DataSource = null;
            comboBoxManual.DataSource = AudioDevicePool.GetWaveOutDevices();
            Enabled = true;

            object matchToPreviouslySelected = null;
            foreach (var item in comboBoxManual.Items)
            {
                if ((item as WaveOutDevice).ValueEquals(previouslySelected))
                {
                    matchToPreviouslySelected = item;
                }
            }

            object matchToCurrentProfile = null;
            foreach (var item in comboBoxManual.Items)
            {
                if ((item as WaveOutDevice).ValueEquals(TheProfile?.TheWaveOutDevice))
                {
                    matchToCurrentProfile = item;
                }
            }

            if (matchToPreviouslySelected != null)
            {
                comboBoxManual.SelectedItem = matchToPreviouslySelected;
            }
            else if (matchToCurrentProfile != null)
            {
                comboBoxManual.SelectedItem = matchToCurrentProfile;
            }
            else if (previouslySelected != null)
            {
                TheProfile.TheWaveOutDevice = previouslySelected;
            }
            SkipFlaggedEventHandlers = false;                        
        }

        private void PictureBoxClone_Click(object sender, EventArgs e)
        {
            CloneWaveAction();
            SetControlVisibility();
            InvokeChangeMade(new ChangeEventArgs(pictureBoxClone));
        }

        private void AnyControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)' ')
            {
                TriggeredAction ta = listBoxActions.SelectedItem as TriggeredAction;
                (ta?.TheAction as CGActionWave)?.Play();                
            }
        }

        private void ComboBoxAPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;
            
            if ((VRAPI)comboBoxAPI.SelectedItem != VRAPI.OculusVR && FormMain.OculusConn.Status == VRConnectionStatus.AllOK && TheProfile.API == VRAPI.OculusVR)
            {
                string msg = String.Format("IMPORTANT{0}{0}When you are using an Oculus Headset, you DON'T need to change the API when using SteamVR apps. " +
                                            "It is highly recommended to leave this setting to \"{1}\" at all times!{0}{0}Continue anyway?", Environment.NewLine, VRAPI.OculusVR.ToString());
                if (MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    SkipFlaggedEventHandlers = true;
                    comboBoxAPI.SelectedItem = VRAPI.OculusVR;
                    SkipFlaggedEventHandlers = false;
                    return;
                }
            }

            TheProfile.API = (VRAPI)comboBoxAPI.SelectedItem;

            if (FormMain.ActiveConnection?.Status == VRConnectionStatus.AllOK)            
                FormMain.IntentionalAPIChange = true; // really cheap and dirty trick to prevent connection lost notification
            
            SetControlVisibility();
            InvokeChangeMade(new ChangeEventArgs(comboBoxAPI));            
            InvokeVRConnectionParameterChanged(new EventArgs());
        }

       
        private void CheckBoxHome_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TheProfile.RequireHome = checkBoxHome.Checked;
            InvokeChangeMade(new ChangeEventArgs(checkBoxHome));
            InvokeVRConnectionParameterChanged(new EventArgs());
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

        void CloneWaveAction()
        {
            TriggeredAction selAction = (listBoxActions.SelectedItem as TriggeredAction);
            if (selAction != null)
            {
                TriggeredAction ta = new TriggeredAction(FormMain.Tracker, TheProfile);
                ta.LoadFromXml(selAction.GetXml());
                
                RefreshActionsListbox();
                SkipFlaggedEventHandlers = true;
                listBoxActions.SelectedItem = ta;
                SkipFlaggedEventHandlers = false;
                WaveActionCtl.LoadWaveAction(ta);
            }
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

            if (!String.IsNullOrEmpty(newName))//&& (from Profile p in Config.Profiles where p.Name == newName && p != TheProfile select p).Count() == 0)
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

            SkipFlaggedEventHandlers = true;

            checkBoxResetOnMount.Checked = TheProfile.ResetOnMount;
            checkBoxFreeze.Checked = TheProfile.Frozen;
            checkBoxStartup.Checked = (Config.StartUpProfile == TheProfile);
            checkBoxHome.Checked = TheProfile.RequireHome;            
            comboBoxDeviceSource.SelectedItem = TheProfile.WaveOutDeviceSource;
            comboBoxAPI.SelectedItem = TheProfile.API;
            
            if (TheProfile.WaveOutDeviceSource == AudioDeviceSource.Manual)            
                comboBoxManual.SelectedItem = TheProfile.TheWaveOutDevice;                        

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
            checkBoxHome.Visible = (TheProfile.API == VRAPI.OculusVR);
            labelOcuChanges.Visible = (TheProfile.WaveOutDeviceSource == AudioDeviceSource.OculusHome);
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

        void InvokeVRConnectionParameterChanged(EventArgs e)
        {
            if (VRConnectionParameterChanged != null)
            {
                VRConnectionParameterChanged(this, e);
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
