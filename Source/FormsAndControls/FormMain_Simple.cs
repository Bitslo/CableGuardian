using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CableGuardian
{
    public enum SimpleNotifType { Beep, Speech }

    partial class FormMain
    {
        Profile SimpleModeProfile;

        readonly string ResetOnMountWarning = $"CAUTION!!! Enabling this will reset the turn counter each time you put on the headset.{Environment.NewLine}" +                                            
                                                   $"NOTE: Depending on the detection hardware and API implementation, this feature may not work as you'd expect.";

        bool IsSimpleModeOn = false;
        void SimpleMode_TurnOn()
        {
            Enabled = false;
            Width = 328;            
            checkBoxConnLost.Visible = false;
            checkBoxOnAPIQuit.Visible = false;
            checkBoxSticky.Visible = false;
            labelProf.Visible = false;            
            panelSimple.Visible = true;
            panelSimple.Focus();
            IsSimpleModeOn = true;
                        
            CenterToScreen();            

            RestoreDefaultProfiles_Standard(Config.ActiveProfile.API);
            SimpleMode_LoadProfile(Config.SimpleModeNotifType);
            SimpleMode_SaveConfigAndUpdateProfile();

            //if ((SimpleModeProfile.API == VRAPI.OculusVR && !OculusConn.OculusHMDConnected())
            //    ||
            //    (SimpleModeProfile.API == VRAPI.OpenVR && OculusConn.OculusHMDConnected()))
                comboBoxAPI.Visible = true;

            Config.UseSimpleMode = true;
            // use these defaults with simple mode            
            Config.TurnCountMemoryMinutes = -1;
            Config.TrayMenuNotifications = true;
            Config.NotifyWhenVRConnectionLost = true;            
            Config.ConnLostNotificationIsSticky = true;

            SimpleMode_LoadValuesToGui();
                        
            SaveConfigurationToFile();
            SetProfilesSaveStatus(true);

            if (!Config.StartMinimized)
                RestoreFromTray();
                        
            TTip.SetToolTip(checkBoxExitWithSteamVR, $"{Config.ProgramTitle} will close when SteamVR is closed.");

            RefreshUILabel();

            Enabled = true;
        }

        void SimpleMode_TurnOff()
        {
            if (WelcomeForm != null && WelcomeForm.Visible)
                WelcomeForm.Close();

            Width = OriginalWidth;            
            checkBoxConnLost.Visible = true;
            checkBoxOnAPIQuit.Visible = true;
            checkBoxSticky.Visible = true;
            labelProf.Visible = true;            
            panelSimple.Visible = false;
            comboBoxAPI.Visible = false;
            IsSimpleModeOn = false;

            Config.UseSimpleMode = false;
            CenterToScreen();
            SaveConfigurationToFile();
            SaveProfilesToFile();

            bool skipStatus = SkipFlaggedEventHandlers;
            SkipFlaggedEventHandlers = true;
            comboBoxProfile.SelectedItem = SimpleModeProfile;
            SkipFlaggedEventHandlers = false;
            // force event handler to refresh profile controls
            ComboBoxProfile_SelectedIndexChanged(comboBoxProfile, null);
            SkipFlaggedEventHandlers = skipStatus;

            LoadConfigToGui();                        
            TTip.SetToolTip(checkBoxExitWithSteamVR, $"{Config.ProgramTitle} will close when SteamVR is closed - unless there are unsaved changes to your profiles.");

            RefreshUILabel();

            string msg = $"Welcome to {Config.ProgramTitle} advanced mode!";                   
            MessageBox.Show(this, msg, "Advanced mode activated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
     
        void SimpleMode_InitializeAppearance()
        {            
            TTip.SetToolTip(numericUpDownHalfTurns, $"A sound will play each time you reach this (or a higher) number of HALF-TURNS (180\u00B0).");
            TTip.SetToolTip(comboBoxNotifType, $"Select the type of sound you wish to hear when turning too much." + Environment.NewLine
                                    + $"\u2022 Beep --> You will get two short beeps in the ear that is on the side of excess rotation. (Turn left too much --> left ear)" + Environment.NewLine
                                    + $"\u2022 Speech --> A voice will say \"Turn left\" or \"Turn right\" according to the direction the cable will untwist." + Environment.NewLine + Environment.NewLine
                                    + $"With both options you get a separate \"happy\" notification sound when returning back to neutral orientation (= no twist).");
            TTip.SetToolTip(trackBarVolume, $"Set the volume level for the notification sounds.");
            TTip.SetToolTip(checkBoxResetOnMount, ResetOnMountWarning);
            TTip.SetToolTip(pictureBoxPlay, $"Test the sounds." + Environment.NewLine + Environment.NewLine
                                    + $"\u2022 Mouse left button --> Play the sound that you will hear when you turn too much to the LEFT." + Environment.NewLine
                                    + $"\u2022 Mouse middle button --> Play the sound that you will hear when you return back to NEUTRAL orientation." + Environment.NewLine
                                    + $"\u2022 Mouse right button --> Play the sound that you will hear when you turn too much to the RIGHT.");
            TTip.SetToolTip(comboBoxAPI, $"API for reading the headset data. (Application Programming Interface){Environment.NewLine+Environment.NewLine}"
                                    +$"\u2022 {VRAPI.OculusVR} = Native Oculus connection. Recommended for Oculus headsets. Works with both platforms (Oculus Home + SteamVR).{Environment.NewLine}"
                                    +$"\u2022 {VRAPI.OpenVR}  = SteamVR connection. For all SteamVR compatible headsets.");            
            TTip.SetToolTip(checkBoxMountingSound, $"Play a sound when putting on the headset.{Environment.NewLine}Can be useful together with \"Reset on mount\" or just by itself to ensure that the app is running."
                                                + Environment.NewLine + "You can preview the sound by checking this box." + Environment.NewLine + Environment.NewLine
                                                + $"NOTE: Depending on the detection hardware and API implementation, this feature may not work as you'd expect.");

            panelSimple.Location = new Point(6, 229);            
            comboBoxAPI.Location = new Point(12, 459);

            comboBoxNotifType.DataSource = Enum.GetValues(typeof(SimpleNotifType));
            comboBoxAPI.DataSource = Enum.GetValues(typeof(VRAPI));

            checkBoxMountingSound.ForeColor = Color.White;
            checkBoxMountingSound.Text = "\u266C";

                        
        }

        void SimpleMode_AddEventHandlers()
        {
            comboBoxAPI.SelectedIndexChanged += ComboBoxAPI_SelectedIndexChanged;
            checkBoxResetOnMount.CheckedChanged += SimpleMode_CheckBoxResetOnMount_CheckedChanged;
            checkBoxMountingSound.CheckedChanged += SimpleMode_CheckBoxMountingSound_CheckedChanged;                        
            comboBoxNotifType.SelectedIndexChanged += SimpleMode_ComboBoxNotifType_SelectedIndexChanged;
            numericUpDownHalfTurns.ValueChanged += SimpleMode_NumericUpDownHalfTurns_ValueChanged;
            trackBarVolume.ValueChanged += SimpleMode_TrackBarVolume_ValueChanged;
            pictureBoxPlay.MouseUp += SimpleMode_PictureBoxPlay_MouseUp;
            pictureBoxPlay.MouseEnter += (s, e) => { pictureBoxPlay.Image = Properties.Resources.Play_hover; };
            pictureBoxPlay.MouseLeave += (s, e) => { pictureBoxPlay.Image = Properties.Resources.Play; };

        }

        private void ComboBoxAPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if ((VRAPI)comboBoxAPI.SelectedItem != VRAPI.OculusVR && OculusConn.Status == VRConnectionStatus.AllOK && SimpleModeProfile.API == VRAPI.OculusVR)
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
            RestoreDefaultProfiles_Standard((VRAPI)comboBoxAPI.SelectedItem);
            SimpleMode_LoadProfile(Config.SimpleModeNotifType);
            SimpleMode_SaveConfigAndUpdateProfile();
            SaveProfilesToFile();
            Enabled = true;
        }

        private void SimpleMode_PictureBoxPlay_MouseUp(object sender, MouseEventArgs e)
        {
            Profile p = SimpleModeProfile;
            
            if (e.Button == MouseButtons.Middle)
            {
                TriggeredAction act = p.Actions.Where(a => a.TheTrigger.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition).First();
                (act.TheAction as CGActionWave).Play();
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    TriggeredAction act = p.Actions.Where(a => a.TheTrigger.TriggeringEvent != YawTrackerOrientationEvent.ResetPosition
                                                            && a.TheTrigger.RotCondition.TargetRotationSide == Direction.Left).First();
                    (act.TheAction as CGActionWave).Play();

                }
                else if(e.Button == MouseButtons.Right)
                {
                    TriggeredAction act = p.Actions.Where(a => a.TheTrigger.TriggeringEvent != YawTrackerOrientationEvent.ResetPosition
                                                            && a.TheTrigger.RotCondition.TargetRotationSide == Direction.Right).First();
                    (act.TheAction as CGActionWave).Play();

                }
            }
        }

        private void SimpleMode_TrackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Config.SimpleModeVolume = trackBarVolume.Value;
            SimpleMode_SetVolumeLabelText();            
        }

        void SimpleMode_SetVolumeLabelText()
        {
            labelVolVal.Text = trackBarVolume.Value.ToString();
            SimpleMode_SaveConfigAndUpdateProfile();
        }

        private void SimpleMode_NumericUpDownHalfTurns_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Config.SimpleModeThreshold = (uint)numericUpDownHalfTurns.Value;
            SimpleMode_SaveConfigAndUpdateProfile();
        }

        private void SimpleMode_ComboBoxNotifType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            Config.SimpleModeNotifType = (SimpleNotifType)Enum.Parse(typeof(SimpleNotifType),comboBoxNotifType.SelectedItem.ToString());
            SimpleMode_LoadProfile(Config.SimpleModeNotifType);
            SimpleMode_SaveConfigAndUpdateProfile();
        }
       

        private void SimpleMode_CheckBoxResetOnMount_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if (checkBoxResetOnMount.Checked)
            {
                string msg = ResetOnMountWarning.Replace(Environment.NewLine, Environment.NewLine + Environment.NewLine);                                
                MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }

            Config.SimpleModeResetOnMount = checkBoxResetOnMount.Checked;
            SimpleMode_SaveConfigAndUpdateProfile();
        }

        private void SimpleMode_CheckBoxMountingSound_CheckedChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            if (checkBoxMountingSound.Checked)
            {
                SimpleModeProfile.MountingSound.Play();
            }

            Config.SimpleModePlayMountingSound = checkBoxMountingSound.Checked;
            SimpleMode_SaveConfigAndUpdateProfile();
        }

        void SimpleMode_LoadValuesToGui()
        {
            bool skipStatus = SkipFlaggedEventHandlers;
            SkipFlaggedEventHandlers = true;

            trackBarVolume.Value = Config.SimpleModeVolume;
            checkBoxResetOnMount.Checked = Config.SimpleModeResetOnMount;
            checkBoxMountingSound.Checked = Config.SimpleModePlayMountingSound;
            numericUpDownHalfTurns.Value = Config.SimpleModeThreshold;
            comboBoxNotifType.SelectedItem = Config.SimpleModeNotifType;
            comboBoxAPI.SelectedItem = SimpleModeProfile.API;

            SkipFlaggedEventHandlers = skipStatus;

            SimpleMode_SetVolumeLabelText();
        }

        void SimpleMode_LoadProfile(SimpleNotifType typ)
        {
            if (typ == SimpleNotifType.Beep)
                SimpleModeProfile = Config.GetProfileByName("CG_Beep");
            else
                SimpleModeProfile = Config.GetProfileByName("CG_Speech");

            LoadProfile(SimpleModeProfile);
        }

        void SimpleMode_SaveConfigAndUpdateProfile()
        {
            Profile p = SimpleModeProfile;

            Config.NotifyOnAPIQuit = (p.API == VRAPI.OculusVR) ? true : false;

            p.ResetOnMount = Config.SimpleModeResetOnMount;
            p.PlayMountingSound = Config.SimpleModePlayMountingSound;

            foreach (var a in p.Actions)
            {
                if (a.TheTrigger.TriggeringEvent == YawTrackerOrientationEvent.ResetPosition)
                {                    
                    a.TheTrigger.RotCondition.TargetPeakHalfTurns = Config.SimpleModeThreshold;
                    (a.TheAction as CGActionWave).Volume = Convert.ToInt32(Math.Ceiling(Config.SimpleModeVolume / 4F));
                }
                else
                {                       
                    a.TheTrigger.RotCondition.TargetHalfTurns = Config.SimpleModeThreshold;
                    (a.TheAction as CGActionWave).Volume = Config.SimpleModeVolume;
                }
            }

            SaveConfigurationToFile();
        }

    }
}
