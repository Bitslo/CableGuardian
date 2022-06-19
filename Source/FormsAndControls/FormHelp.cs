using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace CableGuardian
{
    public partial class FormHelp : Form
    {
        int CurrentPage = 2;
        int NumberOfPages = 2;
        static bool RunFromDesigner { get { return (LicenseManager.UsageMode == LicenseUsageMode.Designtime); } }
        ToolTip TTip = new ToolTip() { AutoPopDelay = 30000, ShowAlways = true };

        public FormHelp()
        {
            InitializeComponent();

            buttonClose.Click += ButtonClose_Click;
            buttonPage.Click += ButtonPage_Click;
            buttonDiscussions.Click += ButtonDiscussions_Click;
            buttonCopyInfo.Click += ButtonCopyInfo_Click;            
            pictureBoxStandard.Click += PictureBoxStandard_Click;

            pictureBoxStandard.MouseEnter += (s, e) => { pictureBoxStandard.Image = Properties.Resources.Title_hover; };
            pictureBoxStandard.MouseLeave += (s, e) => { pictureBoxStandard.Image = Properties.Resources.Title; };
            
            labelVersion.Text = Config.ProgramTitle + " v." +
                                System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString() +
                                " \u00A9 Bitslo";

            TTip.SetToolTip(buttonCopyInfo, "Copies your system information and Cable Guardian configuration to clipboard." + Environment.NewLine
                                        + "It can be useful when solving problems.");
            TTip.SetToolTip(buttonDiscussions, "Steam Discussions may have the answer your looking for. Feel free to start a new discussion!" + Environment.NewLine
                                        + "p.s. Check out the user guide below.");

            if (!RunFromDesigner)
            {        
                labelTitle.ForeColor = Config.CGColor;                
            }

            SetLayoutForCurrentPage();
        }

        private void PictureBoxStandard_Click(object sender, EventArgs e)
        {
            FormMain.OpenSteamPage("steam://url/CommunityFilePage/2091663814", "https://steamcommunity.com/sharedfiles/filedetails/?id=2091663814", this);
        }


        private void ButtonCopyInfo_Click(object sender, EventArgs e)
        {
            string separator = "_____________________________________________________________________________________________";

            string txt = "";
            txt += separator;
            txt += Environment.NewLine + Environment.NewLine;
            txt += Config.ProgramTitle + " v." + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            txt += Environment.NewLine;
            txt += (Environment.Is64BitOperatingSystem) ? "64 bit OS" : "32 bit OS";
            txt += (Environment.Is64BitProcess) ? ", 64 bit process" : ", 32 bit process";
            txt += Environment.NewLine;
            txt += GetOSDescription() + Environment.NewLine;
            txt += GetDotNetDescription() + Environment.NewLine;
            txt += "OpenVR: " + FormMain.OpenVRConn.Status.ToString() + " - " + FormMain.OpenVRConn.StatusMessage;
            txt += Environment.NewLine;
            txt += "Oculus: " + FormMain.OculusConn.Status.ToString() + " - " + FormMain.OculusConn.StatusMessage;
            txt += Environment.NewLine;
            txt += "Active profile = " + Config.ActiveProfile?.Name ?? "";
            txt += Environment.NewLine;
            txt += separator;
            txt += Environment.NewLine + Environment.NewLine + Environment.NewLine;

            try
            {
                txt += Config.GetProfilesXml().ToString();
                txt += Environment.NewLine + Environment.NewLine;

                txt += Config.GetConfigXml().ToString();
                txt += Environment.NewLine + Environment.NewLine;

                if (File.Exists(Program.LogFile))
                    txt += File.ReadAllText(Program.LogFile);
            }
            catch (Exception)
            {
                // intentionally ignore
            }

            string msg = $"System configuration copied to clipboard!";

            try
            {
                Clipboard.SetText(txt);
            }
            catch (Exception ex)
            {
                msg = "Sorry, copying system configuration to clipboard failed*." + Environment.NewLine + Environment.NewLine + "* " + ex.Message;
            }

            MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonDiscussions_Click(object sender, EventArgs e)
        {
            FormMain.OpenSteamPage("steam://openurl/https://steamcommunity.com/app/1208080/discussions/0/", "https://steamcommunity.com/app/1208080/discussions/0/", this);
        }
                

        string GetDotNetDescription()
        {
            try
            {
                string key = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
                using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(key, false))
                {
                    return ".NET Framework = " + reg.GetValue("Release").ToString();
                }
            }
            catch (Exception)
            {
                // intentionally ignore
            }
            return "";
        }

        string GetOSDescription()
        {            
            try
            {
                string key = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
                using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(key, false))
                {
                    string ver = reg.GetValue("ProductName")?.ToString() ?? "";
                    ver +=  ", Version = " + (reg.GetValue("ReleaseId")?.ToString() ?? "");
                    ver += ", Build = " + (reg.GetValue("CurrentBuild")?.ToString() ?? "");
                    ver += "." + (reg.GetValue("UBR")?.ToString() ?? "");
                    ver += " (" + (reg.GetValue("BuildLabEx")?.ToString() ?? "") + ")";

                    return ver;
                }
            }
            catch (Exception)
            {
                // intentionally ignore
            }
            return "";
        }

        private void ButtonPage_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            if (CurrentPage > NumberOfPages)            
                CurrentPage = 1;

            SetLayoutForCurrentPage();
        }

        void SetLayoutForCurrentPage()
        {
            buttonPage.Text = $"{CurrentPage}/{NumberOfPages}";

            if (CurrentPage == 1)
            {                
                this.BackgroundImage = global::CableGuardian.Properties.Resources.Help1;
                foreach (Control item in Controls)
                {
                    if (item != buttonPage && item != buttonClose)
                    {
                        item.Visible = false;
                    }
                }
            }
            else
            {
                this.BackgroundImage = global::CableGuardian.Properties.Resources.Help2;
                foreach (Control item in Controls)
                {
                    if (item != buttonPage && item != buttonClose)
                    {
                        item.Visible = true;
                    }
                }

                buttonPage.Visible = false;

            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
