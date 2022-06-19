using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace CableGuardian
{
 
    public partial class FormWelcome : Form
    {
        public event EventHandler<AnimEventArgs> AnimStatus;
        public event EventHandler<AnimEventArgs> AnimAPI;
        public event EventHandler<AnimEventArgs> AnimUI;
        public event EventHandler<AnimEventArgs> AnimMinimize;
        public event EventHandler<AnimEventArgs> AnimRotSettings;
        public event EventHandler<AnimEventArgs> AnimLaunchOptions;
        public event EventHandler<AnimEventArgs> AnimFullMode;
        public event EventHandler<AnimEventArgs> AnimHelp;
        public event EventHandler<EventArgs> AnimOff;
        public event EventHandler<EventArgs> TrayWelcome;

        Control ActiveCtl = null;

        ToolTip TTip = new ToolTip() { AutoPopDelay = 30000, ShowAlways = true };
        public static bool RunFromDesigner { get { return (LicenseManager.UsageMode == LicenseUsageMode.Designtime); } }
        Point MouseDragPosOnForm = new Point(0, 0);
        bool MouseDownOnComboBox = false;
        BackgroundWorker Animator = new BackgroundWorker();

        Color ColorInactive = Color.DimGray;
        Color ColorActive = Color.White;
        
        public FormWelcome()
        {
            InitializeComponent();
            AddDragAndHoverEventHandlersAndSetColors();
                    
            Animator.WorkerReportsProgress = true;
            Animator.DoWork += Animator_DoWork;
            Animator.ProgressChanged += Animator_ProgressChanged;

            FormClosing += (s,e) => { Stop = true; };

            AddEventHandlers();                        
            
            if (!RunFromDesigner)
            {
                Animator.RunWorkerAsync();
                labelTips.ForeColor = Config.CGColor;                
            }

            TTip.SetToolTip(labelTips, "Move the mouse over the text below to learn the basics.");
            TTip.SetToolTip(labelTooltip, "Most items in the desktop UI have pop-up messages like this one.");
            TTip.SetToolTip(labelNoDashboard, $"In normal day-to-day operation {Config.ProgramTitle} runs in the background and you can just forget about it."
                                            + Environment.NewLine
                                            + $"When you need to change the settings, use the desktop UI."
                                            + Environment.NewLine + Environment.NewLine
                                            + $"While there is no actual VR dashboard in the headset, it's somewhat possible to use the UI via SteamVR Desktop view.");
            TTip.SetToolTip(labelTurnLimit, $"How much is too much? You decide! The goal is to find a good compromise between freedom and cable life." 
                                            + Environment.NewLine 
                                            +"--> Set the \"Half-turn threshold\" to the LOWEST value you can live with.");
            TTip.SetToolTip(labelStatus, $"The status box will tell you if the headset is not being tracked." +
                $"{Environment.NewLine}There's a blue \"OK\" text when you're ready to go.");            
            TTip.SetToolTip(labelAPI, $"Oculus users can select between two APIs (Application Programming Interface)." +
                $"{Environment.NewLine + Environment.NewLine}\u2022 OculusVR (Oculus Native API): Available in every game (Oculus + SteamVR). RECOMMENDED for this non-Pro version of {Config.ProgramTitle}." +
                $"{Environment.NewLine}\u2022 OpenVR (SteamVR API): Available only when SteamVR is running.");
            TTip.SetToolTip(labelFullMode, $"{Config.ProgramTitle} is currently in simple mode." +
                $"{Environment.NewLine}To make use of the app's full potential, feel welcome to try the advanced mode by clicking the flashing mode indicator.");
            TTip.SetToolTip(labelFullmodeSub, $"The advanced mode is flexible but can feel a bit overwhelming at first. Check out the user guides for help.");
            TTip.SetToolTip(pictureBoxBlue, Config.ProgramTitle + " can be accessed from this icon in the Windows notification area." 
                                            + Environment.NewLine + Environment.NewLine
                                            + "Blue icon: headset connection ok" 
                                            + Environment.NewLine 
                                            + "Red icon: headset connection NOT ok");
            string msg = "Close the welcome screen." + Environment.NewLine
                                            + "You can open it again from the help menu \"?\" which will be available after this window is closed.";
            TTip.SetToolTip(checkBoxGotIt, msg);
            TTip.SetToolTip(buttonClose, msg);
                        
        }
                
        void AddEventHandlers()
        {
            checkBoxGotIt.CheckedChanged += (s, e) => { checkBoxGotIt.Visible = false; buttonClose.Visible = true; };
            buttonClose.Click += (s, e) => { Config.WelcomeFormClosed = true; Close(); };                                    
            VisibleChanged += FormWelcome_VisibleChanged;            

            foreach (Control ctl in panel1.Controls)
            {                
                ctl.MouseEnter += Ctl_MouseEnter;
                ctl.MouseLeave += Ctl_MouseLeave;
            }            
        }

        private void Ctl_MouseLeave(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            ctl.ForeColor = ColorInactive;
        }

        bool TrayMsgShown = false;
        private void Ctl_MouseEnter(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            ctl.ForeColor = ColorActive;

            if (!TrayMsgShown && ctl == labelMin)
            {                
                TrayMsgShown = true;
                TrayWelcome?.Invoke(this, EventArgs.Empty);
            }
        }

        bool IsAnimActive = false;
        private void Animator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bool state = e.ProgressPercentage == 1 ? true : false;
            bool animActivated = true;

            if (ActiveCtl == labelStatus)
                AnimStatus?.Invoke(this, new AnimEventArgs(state));
            else if (ActiveCtl == labelAPI)
                AnimAPI?.Invoke(this, new AnimEventArgs(state));
            else if (ActiveCtl == labelUi || ActiveCtl == labelNoDashboard || ActiveCtl == labelTooltip)
                AnimUI?.Invoke(this, new AnimEventArgs(state));
            else if (ActiveCtl == labelMin)
                AnimMinimize?.Invoke(this, new AnimEventArgs(state));
            else if (ActiveCtl == labelTurnLimit)
                AnimRotSettings?.Invoke(this, new AnimEventArgs(state));
            else if (ActiveCtl == labelLaunchOptions)
                AnimLaunchOptions?.Invoke(this, new AnimEventArgs(state));
            else if (ActiveCtl == checkBoxGotIt || ActiveCtl == buttonClose)
                AnimHelp?.Invoke(this, new AnimEventArgs(state));
            else if (ActiveCtl == labelFullMode || ActiveCtl == labelFullmodeSub)
                AnimFullMode?.Invoke(this, new AnimEventArgs(state));
            else 
            {                
                animActivated = false;
            }
            
            if (animActivated)
            {
                if (!IsAnimActive)
                {
                    IsAnimActive = true;
                    labelTips.ForeColor = Config.CGColor;
                }
            }
            else 
            {
                if (IsAnimActive)
                {
                    AnimOff?.Invoke(this, EventArgs.Empty);
                    IsAnimActive = false;                    
                }
                else
                {
                    labelTips.ForeColor = e.ProgressPercentage == 1 ? Config.CGColor : Color.White; 
                }
            }
        }

        private void FormWelcome_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (!Animator.IsBusy)
                {
                    Animator.RunWorkerAsync();
                }
            }
            else
            {
                Stop = true;
            }
                
        }

        bool Stop = false;
        private void Animator_DoWork(object sender, DoWorkEventArgs e)
        {
            Stop = false;
            bool state = true;
            while (!Stop)
            {                
                state = !state;
                Animator.ReportProgress(state ? 1 : 0);
                                
                Thread.Sleep(250);
            }

            ActiveCtl = null;
            Animator.ReportProgress(1);
        }

        void AddDragAndHoverEventHandlersAndSetColors()
        {
            MouseMove += DragPoint_MouseMove;
            MouseDown += DragPoint_MouseDown;
            MouseEnter += (s, e) => { AnimOff?.Invoke(this, EventArgs.Empty); ActiveCtl = null; };

            foreach (Control ctl in (from Control c in Controls  select c))
            {   
                ctl.MouseDown += DragPoint_MouseDown;
                ctl.MouseMove += DragPoint_MouseMove;
                ctl.MouseEnter += (s, e) => { AnimOff?.Invoke(this, EventArgs.Empty); ActiveCtl = ctl; };

                foreach (Control item in ctl.Controls)
                {
                    item.MouseDown += DragPoint_MouseDown;
                    item.MouseMove += DragPoint_MouseMove;
                    item.MouseEnter += (s, e) => { AnimOff?.Invoke(this, EventArgs.Empty); ActiveCtl = item; };

                    if (item is Label)
                    {
                        item.ForeColor = ColorInactive;
                    }
                }
            }
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

    }
    public class AnimEventArgs : EventArgs
    {
        public bool State { get; }
        public AnimEventArgs(bool state)
        {
            State = state;
        }
    }
}
