using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;



namespace CableGuardian
{
    public enum VRAPI { OculusVR, OpenVR }

    public partial class FormMain : Form
    {
        ToolTip TTip = new ToolTip() { AutoPopDelay = 20000 };
        public static bool RunFromDesigner { get { return (LicenseManager.UsageMode == LicenseUsageMode.Designtime); } }

        internal static YawTracker Tracker { get; private set; }
        internal static OculusConnection OculusConn { get; private set; } = new OculusConnection();
        internal static OpenVRConnection OpenVRConn { get; private set; } = new OpenVRConnection();
        internal static AudioDevicePool WaveOutPool { get; private set; } = new AudioDevicePool(OculusConn);
        VRObserver Observer;
        VRConnection ActiveConnection;
        
        Point MouseDragPosOnForm = new Point(0, 0);        
        bool UpdateYawToForm = false;
        bool SkipFlaggedEventHandlers = false;
        bool ProfilesSaved = false;
        bool MouseDownOnComboBox = false;

        public FormMain()
        {
            InitializeComponent();
            Minimize();

            comboBoxAPI.DataSource = Enum.GetValues(typeof(VRAPI));

            // poll interval of 180ms should suffice (5.5 Hz)
            // (head rotation must stay below 180 degrees between samples)
            Observer = new VRObserver(OculusConn, 180);
            Observer.Start();
            Tracker = new YawTracker(Observer);

            if (!RunFromDesigner)            
                InitializeAppearance();
            
            AddEventHandlers();

            ReadConfigAndProfilesFromFile();
            LoadConfigToGui();

            // Open VR connection. This should be done after loading profiles, so that Oculus audio source gets updated.
            if (Config.API == VRAPI.OculusVR)
                SetActiveConnection(OculusConn);
            else
                SetActiveConnection(OpenVRConn);

            SetProfilesSaveStatus(true);
        }

        void ReadConfigAndProfilesFromFile()
        {
            try
            {
                ReadConfigFromFile();
            }
            catch (Exception)
            {
                // intentionally ignore... atm there's nothing that vital in the config
            }

            try
            {
                ReadProfilesFromFile();
            }
            catch (Exception ex)
            {
                string msg = String.Format("Unable* to load profiles from file. {2} will not be sounding any alerts until a new profile has been defined.  {0}{0} * {1}", Environment.NewLine, ex.Message, Config.ProgramTitle);
                Restore();
                MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void LoadConfigToGui()
        {
            RefreshProfileCombo();            
            Profile startProf = Config.StartUpProfile ?? Config.Profiles.FirstOrDefault();
            SkipFlaggedEventHandlers = true;
            comboBoxProfile.SelectedItem = startProf;            
            checkBoxStartMinimized.Checked = Config.StartMinimized;
            checkBoxHome.Checked = Config.RequireHome;
            OculusConn.RequireHome = Config.RequireHome;
            SkipFlaggedEventHandlers = false;
            LoadProfile(startProf);

            if (!Config.StartMinimized)
                Restore();

            CheckWindowsStartUpStatus();

            SkipFlaggedEventHandlers = true;
            comboBoxAPI.SelectedItem = Config.API;
            SkipFlaggedEventHandlers = false;

        }

        void InitializeAppearance()
        {
            InitializeAppearanceCommon(this);

            StartPosition = FormStartPosition.CenterScreen;
            notifyIcon1.Text = Config.ProgramTitle;
            TTip.SetToolTip(pictureBoxPlus, "Add a new profile");
            TTip.SetToolTip(pictureBoxMinus, "Delete profile");
            TTip.SetToolTip(checkBoxHome, $"When checked, HMD is polled only when Oculus Home is running. This minimizes CPU usage for those non-VR moments. {Environment.NewLine}" +
                $"On the flip side, the presence of Home is polled once in two seconds, requiring some CPU time during tracking.");
            TTip.SetToolTip(comboBoxAPI, $"This is a one time setting for most. Choose {VRAPI.OculusVR} for Oculus headsets, {VRAPI.OpenVR} for others.");


            buttonSave.ForeColor = Config.CGColor;            
            labelProf.ForeColor = Config.CGColor;
            labelYaw.ForeColor = Config.CGColor;
            labelDataWarning.ForeColor = Config.CGColor;
            labelFullRot.ForeColor = Config.CGColor;
            labelFullRotTitle.ForeColor = Config.CGColor;
        }

        void InitializeAppearanceCommon(Control ctl)
        {
            if (ctl.Tag?.ToString() == "MANUAL") // skip tagged objects
                return;

            if (ctl is UserControl || ctl is Panel)
            {
                ctl.BackColor = Config.CGBackColor;
            }

            if (ctl is Label || ctl is CheckBox || ctl is Button)
            {
                ctl.ForeColor = Color.White;
            }

            foreach (Control item in ctl.Controls)
            {
                InitializeAppearanceCommon(item);
            }
        }

        void AddEventHandlers()
        {
            AddEventHandlersCommon(this);
            AddDragEventHandlers();

            FormClosing += FormMain_FormClosing;
            FormClosed += FormMain_FormClosed;
            notifyIcon1.MouseDoubleClick += NotifyIcon1_MouseDoubleClick;

            pictureBoxMinimize.MouseClick += PictureBoxMinimize_MouseClick;
            pictureBoxClose.MouseClick += PictureBoxClose_MouseClick;
            pictureBoxMinus.Click += PictureBoxMinus_Click;
            pictureBoxPlus.Click += (s, e) => { AddProfile(); SetProfilesSaveStatus(false); };
            pictureBoxHelp.Click += PictureBoxHelp_Click;

            buttonSave.Click += ButtonSave_Click;
            buttonReset.Click += ButtonReset_Click;
            buttonRetry.Click += ButtonRetry_Click; ;

            comboBoxProfile.SelectedIndexChanged += ComboBoxProfile_SelectedIndexChanged;
            checkBoxShowYaw.CheckedChanged += CheckBoxShowYaw_CheckedChanged;
            checkBoxWindowsStart.CheckedChanged += CheckBoxWindowsStart_CheckedChanged;
            checkBoxStartMinimized.CheckedChanged += CheckBoxStartMinimized_CheckedChanged;
            comboBoxAPI.SelectedIndexChanged += ComboBoxAPI_SelectedIndexChanged;
            checkBoxHome.CheckedChanged += CheckBoxHome_CheckedChanged;

            Observer.StateRefreshed += Observer_StateRefreshed;
            Tracker.FullRotationsChanged += OnFullRotationsChanged;
            profileEditor.ProfileNameChanged += (s, e) => { RefreshProfileCombo(); };
            profileEditor.ChangeMade += OnChangeMade;
            OculusConn.StatusChanged += OnVRConnectionStatusChanged;
            OpenVRConn.StatusChanged += OnVRConnectionStatusChanged;
            OculusConn.StatusChangedToAllOK += (s,e) => { WaveOutPool.SendDeviceRefreshRequest(); };
        }

        void AddEventHandlersCommon(Control ctl)
        {
            if (ctl is ComboBox)
            {
                ctl.MouseDown += (s, e) => { MouseDownOnComboBox = true; };                
            }

            foreach (Control item in ctl.Controls)
            {
                AddEventHandlersCommon(item);
            }
        }


        private void CheckBoxHome_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            OculusConn.RequireHome = (checkBoxHome.Checked);
            Config.RequireHome = (checkBoxHome.Checked);
            SaveConfigurationToFile();
        }

        private void ButtonRetry_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (Config.API == VRAPI.OculusVR)
            {
                OculusConn.Open();
            }
            else
            {
                OpenVRConn.Open();
            }
            Enabled = true;
        }

        private void PictureBoxHelp_Click(object sender, EventArgs e)
        {
            FormHelp help = new FormHelp();
            help.StartPosition = FormStartPosition.CenterParent;
            SkipFlaggedEventHandlers = true;
            help.ShowDialog(this);
            SkipFlaggedEventHandlers = false;
        }

        void SetProfilesSaveStatus(bool saved)
        {
            ProfilesSaved = saved;
            buttonSave.ForeColor = (saved) ? Config.CGColor : Config.CGErrorColor ;
        }

        void AddProfile()
        {
            Profile prof = new Profile();
            Config.AddProfile(prof);
            
            RefreshProfileCombo();

            SkipFlaggedEventHandlers = true;
            comboBoxProfile.SelectedItem = prof;
            SkipFlaggedEventHandlers = false;

            LoadProfile(prof);            
        }

       
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveProfilesToFile();
        }

        
       

        private void ComboBoxAPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;
                        
            if ((VRAPI)comboBoxAPI.SelectedItem != VRAPI.OculusVR && OculusConn.Status == VRConnectionStatus.AllOK && Config.API == VRAPI.OculusVR)
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

            Enabled = false;
            
            if ((VRAPI)comboBoxAPI.SelectedItem == VRAPI.OculusVR )
                SetActiveConnection(OculusConn);
            else
                SetActiveConnection(OpenVRConn);

            Enabled = true;
            Cursor.Current = Cursors.Default;

            Config.API = (VRAPI)comboBoxAPI.SelectedItem;

            SaveConfigurationToFile();
        }

        /// <summary>
        /// Sets the VR API connection and starts tracking the rotation.
        /// </summary>
        /// <param name="api"></param>
        void SetActiveConnection(VRConnection connection)
        {
            VRConnection connToClose;
            
            if (connection == OculusConn)
            {
                connToClose = OpenVRConn;
                labelMount.Visible = false;
                checkBoxHome.Visible = true;
            }
            else
            {
                connToClose = OculusConn;
                labelMount.Visible = true;
                checkBoxHome.Visible = false;
            }

            
            connToClose.Close();

            ActiveConnection = connection;

            if (connection.Status != VRConnectionStatus.Closed)
                connection.Close();

            connection.Open();
            Observer.SetVRConnection(connection);
                        
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveProfilesToFile();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void OnChangeMade(object sender, ChangeEventArgs e)
        {
            SetProfilesSaveStatus(false);
        }

        private void CheckBoxStartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Config.StartMinimized = checkBoxStartMinimized.Checked;
            SaveConfigurationToFile();
        }

        private void PictureBoxMinus_Click(object sender, EventArgs e)
        {
            Profile selProf = comboBoxProfile.SelectedItem as Profile;
            if (selProf != null)
            {   
                string msg = $"Delete profile \"{selProf.Name}\"?";
                if(MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    DeleteProfile(selProf);

                SetProfilesSaveStatus(false);
            }            
        }

        void DeleteProfile(Profile profile)
        {
            if (profile == null)
                throw new Exception("null profile cannot be deleted");

            Profile other = Config.Profiles.Where(p => p != profile).FirstOrDefault();

            if (other != null)
            {
                //LoadProfile(other);
                comboBoxProfile.SelectedItem = other;
            }
            else
                AddProfile();

            Config.RemoveProfile(profile);
            profile.Dispose();

            RefreshProfileCombo();
        }

        private void ComboBoxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            bool saveStatus = ProfilesSaved;
            LoadProfile(comboBoxProfile.SelectedItem as Profile);
            SetProfilesSaveStatus(saveStatus);
        }

        void LoadProfile(Profile p)
        {
            if (p != null)
            {
                profileEditor.Visible = true;
                Config.SetActiveProfile(p);
                profileEditor.LoadProfile(p);
            }
            else
            {
                profileEditor.Visible = false;
            }
        }

        void RefreshProfileCombo()
        {
            SkipFlaggedEventHandlers = true;

            object selected = comboBoxProfile.SelectedItem;
            comboBoxProfile.DataSource = null;
            comboBoxProfile.DataSource = Config.Profiles;

            if (selected != null && comboBoxProfile.Items.Contains(selected))
            {
                comboBoxProfile.SelectedItem = selected;
            }

            SkipFlaggedEventHandlers = false;
        }

        private void CheckBoxWindowsStart_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)            
                return;
                        
            try
            {
                Config.WriteWindowsStartupToRegistry(checkBoxWindowsStart.Checked);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Unable* to access registry to set startup status. Try running this app as Administrator.{0}{0}", Environment.NewLine);
                msg += String.Format("Alternatively, you can manually add a shortcut to {2} into your startup folder: {1} {0}{0}",Environment.NewLine, Environment.GetFolderPath(Environment.SpecialFolder.Startup), Config.ProgramTitle);                
                msg += "*" + ex.Message;
                MessageBox.Show(this, msg, Config.ProgramTitle);
            }

            CheckWindowsStartUpStatus();
        }

        void CheckWindowsStartUpStatus()
        {
            bool check = false;
            try
            {
                check = Config.ReadWindowsStartupFromRegistry();              
            }
            catch (Exception)
            {
                // intentionally ignore
            }

            SkipFlaggedEventHandlers = true;
            checkBoxWindowsStart.Checked = check;
            SkipFlaggedEventHandlers = false;
        }

        private void CheckBoxShowYaw_CheckedChanged(object sender, EventArgs e)
        {
            UpdateYawToForm = checkBoxShowYaw.Checked;
            labelYaw.Visible = checkBoxShowYaw.Checked;
            labelDataWarning.Visible = checkBoxShowYaw.Checked;
            labelFullRotTitle.Visible = checkBoxShowYaw.Checked;
            labelFullRot.Visible = checkBoxShowYaw.Checked;
        }

        void Observer_StateRefreshed(object sender, EventArgs e)
        {
            if (UpdateYawToForm)
            {
                labelYaw.Text = YawTracker.RadToDeg(Tracker.YawValue).ToString();
            }
        }

        void AddDragEventHandlers()
        {
            MouseMove += DragPoint_MouseMove;
            MouseDown += DragPoint_MouseDown;
            pictureBoxLogo.MouseDown += DragPoint_MouseDown;
            pictureBoxLogo.MouseMove += DragPoint_MouseMove;

            foreach (Control ctl in Controls)
            {
                if (ctl is Label)
                {
                    ctl.MouseDown += DragPoint_MouseDown;
                    ctl.MouseMove += DragPoint_MouseMove;
                }                
            }
        }

        void OnVRConnectionStatusChanged(object sender, EventArgs e)
        {
            VRConnection conn = sender as VRConnection;

            if (conn != ActiveConnection)
                return;

            if (conn.Status != VRConnectionStatus.AllOK)
            {
                labelStatus.ForeColor = Config.CGErrorColor;
                notifyIcon1.Icon = CableGuardian.Properties.Resources.CG_error;
                notifyIcon1.Text = Config.ProgramTitle + $": {Config.API} - {conn.Status.ToString()}";
            }
            else
            {
                labelStatus.ForeColor = Config.CGColor;
                notifyIcon1.Icon = CableGuardian.Properties.Resources.CG;
                notifyIcon1.Text = Config.ProgramTitle + $": {Config.API} - {conn.Status.ToString()}";
            }

            labelStatus.Text = $"VR Headset Connection Status:{Environment.NewLine}{Environment.NewLine}" + conn.StatusMessage;
                       
            buttonRetry.Visible = (conn.Status == VRConnectionStatus.Closed);
           
        }

        private void PictureBoxClose_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        void Minimize()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }
        void Restore()
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if (WindowState == FormWindowState.Normal)
                Minimize();
            else
                Restore();            
        }

        private void PictureBoxMinimize_MouseClick(object sender, MouseEventArgs e)
        {
            Minimize();
        }

        private void DragPoint_MouseDown(object sender, MouseEventArgs e)
        {            
            MouseDragPosOnForm = PointToClient(Cursor.Position);
            MouseDownOnComboBox = false;
        }

        private void DragPoint_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDownOnComboBox) // comboboxes were causing some erratic form movement
            {                
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                Location = new Point(Cursor.Position.X - MouseDragPosOnForm.X, Cursor.Position.Y - MouseDragPosOnForm.Y);
                
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (!ProfilesSaved)
            {                
                string msg = String.Format("There are unsaved changes to your profiles. Close anyway?");
                e.Cancel = (MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel);                                    
            }

            if (!e.Cancel)
            {
                OculusConn.StatusChanged -= OnVRConnectionStatusChanged; // to prevent running eventhandler after form close
                OpenVRConn.StatusChanged -= OnVRConnectionStatusChanged;
                OpenVRConn?.Dispose();
                OculusConn?.Dispose();
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {   
            notifyIcon1.Visible = false;
        }

        void SaveConfigurationToFile()
        {
            try
            {
                Config.WriteConfigToFile();
            }
            catch (Exception e)
            {
                string msg = $"Saving configuration to {Config.ConfigFile} failed! {Environment.NewLine}{Environment.NewLine} {e.Message}";
                MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        void SaveProfilesToFile()
        {
            try
            {
                Config.WriteProfilesToFile();
                SetProfilesSaveStatus(true);
            }
            catch (Exception e)
            {
                string msg = $"Saving profiles to {Config.ProfilesFile} failed! {Environment.NewLine}{Environment.NewLine} {e.Message}";
                MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        void ReadConfigFromFile()
        {
            Config.ReadConfigFromFile();            
        }

        void ReadProfilesFromFile()
        {
            Config.ReadProfilesFromFile();            
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            string msg = String.Format("This will reset the rotation counter to zero. " +                                        
                                        "Make sure the cable has been completely untwisted before continuing.{0}{0}" +
                                        "Note that the reset position will always be set to 0 degrees (facing forward) " +
                                        "regardless of the headset orientation when applying this reset operation. Also note that " +
                                        "the rotation counter is automatically reset when {1} is started. {0}{0}" +
                                        "Continue with the reset?", Environment.NewLine, Config.ProgramTitle);
            if (MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Tracker.Reset();
                labelFullRot.Text = "0";
            }
        }
               
        private void OnFullRotationsChanged(object sender, RotationEventArgs e)
        {
            if (UpdateYawToForm)
            {
                labelFullRot.Text = e.FullRotations.ToString() + ((e.FullRotations > 0) ? " (" + e.RotationSide.ToString() + ")" : ""); 
            }            
        }
        
    }
}
