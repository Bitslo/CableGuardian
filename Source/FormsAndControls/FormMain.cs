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
        ContextMenuStrip TrayMenu = new ContextMenuStrip();
        ToolStripLabel TrayMenuTitle = new ToolStripLabel(Config.ProgramTitle);
        ToolStripLabel TrayMenuRotations = new ToolStripLabel("Full rotations: 00000000");
        ToolStripMenuItem TrayMenuReset = new ToolStripMenuItem("Reset rotation counter");
        ToolStripSeparator TrayMenuSeparator1 = new ToolStripSeparator();
        ToolStripMenuItem TrayMenuAlarmIn = new ToolStripMenuItem("Alarm me in");
        ToolStripMenuItem TrayMenuAlarmAt = new ToolStripMenuItem("Alarm me at");
        ToolStripMenuItem TrayMenuAlarmClear = new ToolStripMenuItem("Cancel alarm");
        ToolStripSeparator TrayMenuSeparator2 = new ToolStripSeparator();
        ToolStripMenuItem TrayMenuGUI = new ToolStripMenuItem("GUI");
        ToolStripMenuItem TrayMenuExit = new ToolStripMenuItem("Exit");
        public static bool RunFromDesigner { get { return (LicenseManager.UsageMode == LicenseUsageMode.Designtime); } }

        internal static YawTracker Tracker { get; private set; }
        internal static OculusConnection OculusConn { get; private set; } = new OculusConnection();
        internal static OpenVRConnection OpenVRConn { get; private set; } = new OpenVRConnection();
        internal static AudioDevicePool WaveOutPool { get; private set; } = new AudioDevicePool(OculusConn);
        VRObserver Observer;
        VRConnection ActiveConnection;
        Timer AlarmTimer = new Timer();        
        DateTime AlarmTime;
        int TimerHours = 0;
        int TimerMinutes = 0;
        int TimerSeconds = 0;

        Point MouseDragPosOnForm = new Point(0, 0);        
        bool UpdateYawToForm = false;
        bool SkipFlaggedEventHandlers = false;
        bool ProfilesSaved = false;
        bool MouseDownOnComboBox = false;
        bool APIChangedByUser = false;
        /// <summary>
        /// One-time flag to allow hiding the form at startup
        /// </summary>
        bool ForceHide = true;

        public FormMain()
        {
            InitializeComponent();
            Environment.CurrentDirectory = Config.ExeFolder; // always run from exe folder to avoid problems with dlls            

            comboBoxAPI.DataSource = Enum.GetValues(typeof(VRAPI));

            // poll interval of 180ms should suffice (5.5 Hz)
            // (head rotation must stay below 180 degrees between samples)
            Observer = new VRObserver(OculusConn, 180);
            Observer.Start();
            Tracker = new YawTracker(Observer);
                        
            if (!RunFromDesigner)
            {
                InitializeTrayMenu();
                InitializeAppearance();
            }
            
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

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(ForceHide ? false : value);
        }

        void ReadConfigAndProfilesFromFile()
        {
            try
            {
                Config.CheckDefaultSounds();
            }
            catch (Exception ex)
            {
                string msg = String.Format("Unable* to load default sounds.  {0}{0} * {1}", Environment.NewLine, ex.Message);
                RestoreFromTray();
                MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
                RestoreFromTray();
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
            checkBoxConnLost.Checked = Config.NotifyWhenVRConnectionLost;
            checkBoxTrayNotifications.Checked = Config.TrayMenuNotifications;
            SkipFlaggedEventHandlers = false;
            LoadProfile(startProf);

            if (!Config.StartMinimized)
                RestoreFromTray();

            CheckWindowsStartUpStatus();

            SkipFlaggedEventHandlers = true;
            comboBoxAPI.SelectedItem = Config.API;
            SkipFlaggedEventHandlers = false;

        }

        void InitializeTrayMenu()
        {
            TrayMenuTitle.Font = new Font(TrayMenuTitle.Font, FontStyle.Bold);
            TrayMenuTitle.ForeColor = Config.CGErrorColor;
            TrayMenuRotations.Font = new Font(TrayMenuRotations.Font, FontStyle.Bold);

            notifyIcon1.ContextMenuStrip = TrayMenu;
            TrayMenu.Items.Add(TrayMenuTitle);
            TrayMenu.Items.Add(TrayMenuRotations);            
            TrayMenu.Items.Add(TrayMenuReset);            
            TrayMenu.Items.Add(TrayMenuSeparator1);
            TrayMenu.Items.Add(TrayMenuAlarmIn);
            TrayMenu.Items.Add(TrayMenuAlarmAt);
            TrayMenu.Items.Add(TrayMenuAlarmClear);            
            TrayMenu.Items.Add(TrayMenuSeparator2);
            TrayMenu.Items.Add(TrayMenuGUI);
            TrayMenu.Items.Add(TrayMenuExit);

            BuilAlarmMenu();
        }

        void BuilAlarmMenu()
        {
            //ToolStripMenuItem itemAM = new ToolStripMenuItem("AM");            
            //TrayMenuAlarmAt.DropDownItems.Add(itemAM);

            //ToolStripMenuItem itemPM = new ToolStripMenuItem("PM");            
            //TrayMenuAlarmAt.DropDownItems.Add(itemPM);

            for (int i = 0; i < 12; i++)
            {
                ToolStripMenuItem itemH = new ToolStripMenuItem(i.ToString() + "h");                
                itemH.Tag = i;
                TrayMenuAlarmIn.DropDownItems.Add(itemH);

                int ath = (i == 0) ? 12 : i;  // to get 12 first since it actually represents zero in the AM/PM system

                ToolStripMenuItem itemAMH = new ToolStripMenuItem(ath.ToString());
                itemAMH.Tag = (ath == 12) ? 0 : ath;
                TrayMenuAlarmAt.DropDownItems.Add(itemAMH);
                                
                //ToolStripMenuItem itemPMH = new ToolStripMenuItem(ath.ToString());                
                //itemPMH.Tag = (ath == 12) ? ath : ath + 12;
                //itemPM.DropDownItems.Add(itemPMH);

                for (int j = 0; j < 60; j += 5)
                {
                    ToolStripMenuItem itemM = new ToolStripMenuItem(i.ToString() + "h " + j.ToString() + "min");
                    itemM.Tag = j;
                    itemH.DropDownItems.Add(itemM);
                    itemM.Click += TrayMenuAlarmInItem_Click;
                                       
                    ToolStripMenuItem itemAMM = new ToolStripMenuItem(ath.ToString() + ":" + ((j < 10) ? "0" : "") + j.ToString()); // + " AM");
                    itemAMM.Tag = j;
                    itemAMH.DropDownItems.Add(itemAMM);
                    itemAMM.Click += TrayMenuAlarmAtItem_Click;

                    /*
                    ToolStripMenuItem itemPMM = new ToolStripMenuItem(ath.ToString() + ":" + ((j < 10) ? "0" : "") + j.ToString() + " PM");
                    itemPMM.Tag = j;
                    itemPMH.DropDownItems.Add(itemPMM);
                    itemPMM.Click += TrayMenuAlarmAtItem_Click;
                    */
                }
            }
        }        

               
        void InitializeAppearance()
        {
            InitializeAppearanceCommon(this);

            StartPosition = FormStartPosition.CenterScreen;
            notifyIcon1.Text = Config.ProgramTitle;
            notifyIcon1.Icon = CableGuardian.Properties.Resources.CG_error;            
            Icon = CableGuardian.Properties.Resources.CG_error;
            TTip.SetToolTip(pictureBoxPlus, "Add a new profile");
            TTip.SetToolTip(pictureBoxMinus, "Delete profile");
            TTip.SetToolTip(checkBoxHome, $"When checked, HMD is polled only when Oculus Home is running. This minimizes CPU usage for those non-VR moments. {Environment.NewLine}" +
                $"On the flip side, the presence of Home is polled once in two seconds, requiring some CPU time during tracking.");
            TTip.SetToolTip(comboBoxAPI, $"This is a one time setting for most. Choose {VRAPI.OculusVR} for Oculus headsets, {VRAPI.OpenVR} for others.");
            TTip.SetToolTip(checkBoxConnLost, $"Show Windows notification when connection to the VR headset changes from OK to NOT OK.{Environment.NewLine}" + 
                                                "This can happen for example when the VR-drivers are updated.");
            TTip.SetToolTip(buttonReset, $"NOTE that the reset can also be done from the {Config.ProgramTitle} tray icon.");
            TTip.SetToolTip(buttonAlarm, $"Adjust the alarm sound here. Use the {Config.ProgramTitle} tray icon to set the alarm.");
            TTip.SetToolTip(checkBoxTrayNotifications, $"When checked, a Windows notification is displayed when you make selections in the {Config.ProgramTitle} tray menu. (for feedback)");

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
            buttonRetry.Click += ButtonRetry_Click;
            buttonAlarm.Click += ButtonAlarm_Click;

            comboBoxProfile.SelectedIndexChanged += ComboBoxProfile_SelectedIndexChanged;
            checkBoxShowYaw.CheckedChanged += CheckBoxShowYaw_CheckedChanged;
            checkBoxWindowsStart.CheckedChanged += CheckBoxWindowsStart_CheckedChanged;
            checkBoxStartMinimized.CheckedChanged += CheckBoxStartMinimized_CheckedChanged;
            comboBoxAPI.SelectedIndexChanged += ComboBoxAPI_SelectedIndexChanged;
            checkBoxHome.CheckedChanged += CheckBoxHome_CheckedChanged;
            checkBoxConnLost.CheckedChanged += CheckBoxConnLost_CheckedChanged;
            checkBoxTrayNotifications.CheckedChanged += CheckBoxTrayNotifications_CheckedChanged;

            Observer.StateRefreshed += Observer_StateRefreshed;
            Tracker.FullRotationsChanged += OnFullRotationsChanged;
            profileEditor.ProfileNameChanged += (s, e) => { RefreshProfileCombo(); };
            profileEditor.ChangeMade += OnChangeMade;
            OculusConn.StatusChanged += OnVRConnectionStatusChanged;
            OpenVRConn.StatusChanged += OnVRConnectionStatusChanged;
            OculusConn.StatusChangedToAllOK += (s,e) => { WaveOutPool.SendDeviceRefreshRequest(); };
            OculusConn.StatusChangedToNotOK += OnVRConnectionLost;
            OpenVRConn.StatusChangedToNotOK += OnVRConnectionLost;

            TrayMenuReset.Click += TrayMenutReset_Click;
            TrayMenuAlarmClear.Click += TrayMenuAlarmClear_Click;            
            TrayMenuGUI.Click += (s,e) => { RestoreFromTray();};
            TrayMenuExit.Click += (s, e) => { Exit(); };
            TrayMenu.Opening += TrayMenu_Opening;

            AlarmTimer.Tick += AlarmTimer_Tick;
        }

        void Exit()
        {
            if (ForceHide) // form has never been shown
            {
                Application.Exit();
            }
            else
            {
                Close();
            }            
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

        private void TrayMenu_Opening(object sender, CancelEventArgs e)
        {
            uint fullRot = Tracker.GetFullRotations();
            Direction rotSide = Tracker.GetRotationSide();
            TrayMenuRotations.Text = "Full rotations: " + fullRot.ToString() + ((fullRot > 0) ? " (" + rotSide.ToString() + ")" : "");

            if (TimerHours == 0 && TimerMinutes == 0 && TimerSeconds == 0)
            {
                TrayMenuAlarmIn.Text = $"Alarm me in";                
                TrayMenuAlarmIn.ForeColor = Color.Empty;
                TrayMenuAlarmAt.Text = $"Alarm me @";
                TrayMenuAlarmAt.ForeColor = Color.Empty;
                TrayMenuAlarmClear.Text = $"Cancel alarm";
                TrayMenuAlarmClear.Enabled = false;
            }
            else
            {   
                TimeSpan remain = AlarmTime.Subtract(DateTime.Now);
                TrayMenuAlarmIn.Text = $"Alarm me in {remain.Hours}h {remain.Minutes}min {remain.Seconds}s";                
                TrayMenuAlarmIn.ForeColor = Config.CGColor;
                TrayMenuAlarmAt.Text = $"Alarm me @ {AlarmTime.ToShortTimeString()}";
                TrayMenuAlarmAt.ForeColor = Config.CGColor;
                TrayMenuAlarmClear.Text = $"Cancel alarm";
                TrayMenuAlarmClear.Enabled = true;
            }

            // update AM/PM for available alarm times (next 12h):
            DateTime now = DateTime.Now;
            int nowHour0_11 = (now.Hour > 11) ? now.Hour - 12 : now.Hour;
            int nowMin = now.Minute;
            bool isPM = (now.Hour > 11);
            string amPM = "AM";

            foreach (ToolStripMenuItem itemH in TrayMenuAlarmAt.DropDownItems)
            {
                int menuHour0_11 = (int)itemH.Tag;
                foreach (ToolStripMenuItem itemMin in itemH.DropDownItems)
                {
                    int menuMin = (int)itemMin.Tag;
                    if (menuHour0_11 < nowHour0_11)
                    {
                        amPM = (isPM) ? "AM" : "PM";
                    }
                    else if (menuHour0_11 > nowHour0_11)
                    {
                        amPM = (isPM) ? "PM" : "AM";
                    }
                    else // current hour
                    {
                        if (menuMin <= nowMin)
                        {
                            amPM = (isPM) ? "AM" : "PM";
                        }
                        else
                        {
                            amPM = (isPM) ? "PM" : "AM";
                        }
                    }
                    int menuH12 = (menuHour0_11 == 0) ? 12 : menuHour0_11;                    
                    itemMin.Text = menuH12.ToString() + ":" + ((menuMin < 10) ? "0" : "") + menuMin.ToString() + " " + amPM;
                }
            }
        }

        private void TrayMenutReset_Click(object sender, EventArgs e)
        {
            Tracker.Reset();
            labelFullRot.Text = "0";
            if (Config.TrayMenuNotifications)
            {
                notifyIcon1.ShowBalloonTip(2000, Config.ProgramTitle, "Reset successfull. Full rotations = 0.", ToolTipIcon.None);
                // to clear the notification from the list:
                notifyIcon1.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        private void TrayMenuAlarmInItem_Click(object sender, EventArgs e)
        {
            AlarmTimer.Stop();
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripMenuItem parent = item.OwnerItem as ToolStripMenuItem;
            TimerHours = (int)parent.Tag;
            TimerMinutes = (int)item.Tag;
            AlarmTime = DateTime.Now.AddHours(TimerHours);
            AlarmTime = AlarmTime.AddMinutes(TimerMinutes);

            int interval = (TimerHours * 3600 * 1000) + (TimerMinutes * 60 * 1000);
            //int interval = (TimerHours * 3600 * 10) + (TimerMinutes * 60 * 10); // for testing

            SetAlarm(interval);
        }

        private void TrayMenuAlarmAtItem_Click(object sender, EventArgs e)
        {            
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripMenuItem parent = item.OwnerItem as ToolStripMenuItem;
            int hours = (int)parent.Tag;
            int minutes = (int)item.Tag;
            DateTime now = DateTime.Now;
            // No more AM/PM menu. The nearest one is chosen automatically.
            DateTime AlarmTimeAM = new DateTime(now.Year, now.Month, now.Day, hours, minutes, 0);
            DateTime AlarmTimePM = new DateTime(now.Year, now.Month, now.Day, (hours == 0) ? 12 : hours + 12, minutes, 0);
            if (AlarmTimeAM < now)
                AlarmTimeAM = AlarmTimeAM.AddDays(1);
            if (AlarmTimePM < now)
                AlarmTimePM = AlarmTimePM.AddDays(1);
                        
            AlarmTime = (AlarmTimeAM < AlarmTimePM) ? AlarmTimeAM : AlarmTimePM;
            
            //AlarmTime = new DateTime(now.Year,now.Month,now.Day,hours,minutes,0);
            //if (AlarmTime < now)
            //    AlarmTime = AlarmTime.AddDays(1);
                        
            TimerHours = (AlarmTime - now).Hours;
            TimerMinutes = (AlarmTime - now).Minutes;
            TimerSeconds = (AlarmTime - now).Seconds;

            int interval = (TimerHours * 3600 * 1000) + (TimerMinutes * 60 * 1000) + (TimerSeconds * 1000);
            //int interval = (TimerHours * 3600 * 10) + (TimerMinutes * 60 * 10); // for testing
           
            SetAlarm(interval);         
        }

                        
        void SetAlarm(int interval)
        {
            AlarmTimer.Stop();
            if (interval > 0)
            {
                AlarmTimer.Interval = interval;
                AlarmTimer.Start();
                if (Config.TrayMenuNotifications)
                {
                    notifyIcon1.ShowBalloonTip(5000, Config.ProgramTitle, $"Alarm will go off in {TimerHours}h {TimerMinutes}min {TimerSeconds}s (@ {AlarmTime.ToShortTimeString()}).", ToolTipIcon.None);
                    // to clear the notification from the list:
                    notifyIcon1.Visible = false;
                    notifyIcon1.Visible = true;
                }
            }
            else
            {
                PlayAlarm();
            }
        }

        private void TrayMenuAlarmClear_Click(object sender, EventArgs e)
        {
            AlarmTimer.Stop();
            TimerHours = TimerMinutes = TimerSeconds = 0;
            if (Config.TrayMenuNotifications)
            {
                notifyIcon1.ShowBalloonTip(2000, Config.ProgramTitle, "Alarm cancelled.", ToolTipIcon.None);
                // to clear the notification from the list:
                notifyIcon1.Visible = false;
                notifyIcon1.Visible = true;
            }            
        }
        
        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            PlayAlarm();
        }

        private void PlayAlarm()
        {
            AlarmTimer.Stop();
            TimerHours = TimerMinutes = TimerSeconds = 0;
            Config.Alarm.Play();
        }

        private void CheckBoxHome_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            OculusConn.RequireHome = (checkBoxHome.Checked);
            Config.RequireHome = (checkBoxHome.Checked);
            SaveConfigurationToFile();
        }

        private void CheckBoxConnLost_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;
                        
            Config.NotifyWhenVRConnectionLost = (checkBoxConnLost.Checked);
            SaveConfigurationToFile();
        }
                
        private void CheckBoxTrayNotifications_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Config.TrayMenuNotifications = (checkBoxTrayNotifications.Checked);
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


        private void ButtonAlarm_Click(object sender, EventArgs e)
        {
            OpenAlarmSettings();   
        }

        private void OpenAlarmSettings()
        {
            FormAlarm al = new FormAlarm();
            al.StartPosition = FormStartPosition.Manual;
            al.Location = PointToScreen(new Point(buttonAlarm.Location.X - 2, buttonAlarm.Location.Y - 2));
            SkipFlaggedEventHandlers = true;            
            TrayMenu.Enabled = false;
            al.ShowDialog(this);            
            TrayMenu.Enabled = true;
            SkipFlaggedEventHandlers = false;
            SaveConfigurationToFile();
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

            APIChangedByUser = true;

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
                Icon = CableGuardian.Properties.Resources.CG_error;
                labelStatus.ForeColor = Config.CGErrorColor;
                notifyIcon1.Icon = CableGuardian.Properties.Resources.CG_error;
                notifyIcon1.Text = Config.ProgramTitle + $": {Config.API} - {conn.Status.ToString()}";
                TrayMenuTitle.ForeColor = Config.CGErrorColor;
                TrayMenuTitle.Text = Config.ProgramTitle + " - NOT OK";
            }
            else
            {
                Icon = CableGuardian.Properties.Resources.CG;
                labelStatus.ForeColor = Config.CGColor;
                notifyIcon1.Icon = CableGuardian.Properties.Resources.CG;
                notifyIcon1.Text = Config.ProgramTitle + $": {Config.API} - {conn.Status.ToString()}";
                TrayMenuTitle.ForeColor = Config.CGColor;
                TrayMenuTitle.Text = Config.ProgramTitle + " - All OK";
            }

            labelStatus.Text = $"VR Headset Connection Status:{Environment.NewLine}{Environment.NewLine}" + conn.StatusMessage;
                       
            buttonRetry.Visible = (conn.Status == VRConnectionStatus.Closed);           
        }

        void OnVRConnectionLost(object sender, EventArgs e)
        {
            if (Config.NotifyWhenVRConnectionLost && !APIChangedByUser)
                notifyIcon1.ShowBalloonTip(2000, Config.ProgramTitle, $"VR headset connection lost. {Config.ProgramTitle} offline.", ToolTipIcon.Warning);

            APIChangedByUser = false;
        }

        private void PictureBoxClose_MouseClick(object sender, MouseEventArgs e)
        {
            Exit();
        }

        void MinimizeToTray()
        {
            //WindowState = FormWindowState.Minimized;
            //ShowInTaskbar = false;
            Hide();
        }
        void RestoreFromTray()
        {
            //WindowState = FormWindowState.Normal;
            //ShowInTaskbar = true;         
            ForceHide = false;
            Show();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            //if (WindowState == FormWindowState.Normal)
            if (Visible)
                MinimizeToTray();
            else
                RestoreFromTray();            
        }

        private void PictureBoxMinimize_MouseClick(object sender, MouseEventArgs e)
        {
            MinimizeToTray();
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
                AlarmTimer.Dispose();
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
