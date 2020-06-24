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
        ToolTip TTip = new ToolTip() { AutoPopDelay = 30000};

        public FormHelp()
        {
            InitializeComponent();

            buttonClose.Click += ButtonClose_Click;
            buttonPage.Click += ButtonPage_Click;
            labelSimple.MouseEnter += (s, e) => { labelSimple.ForeColor = Color.Yellow; };
            labelSimple.MouseLeave += (s, e) => { labelSimple.ForeColor = Color.White; };
            labelSimple.Click += LabelSimple_Click;            
            buttonEmail.Click += ButtonEmail_Click;
            pictureBoxStandard.Click += PictureBoxStandard_Click;

            pictureBoxStandard.MouseEnter += (s, e) => { pictureBoxStandard.Image = Properties.Resources.Title_hover; };
            pictureBoxStandard.MouseLeave += (s, e) => { pictureBoxStandard.Image = Properties.Resources.Title; };
            
            labelVersion.Text = Config.ProgramTitle + " v." +
                                System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString() +
                                " \u00A9 Bitslo";

            TTip.SetToolTip(buttonEmail, "Copies a support email template to the clipboard.");

            if (!RunFromDesigner)
            {        
                labelTitle.ForeColor = Config.CGColor;                
            }

            SetLayoutForCurrentPage();
        }

        private void PictureBoxStandard_Click(object sender, EventArgs e)
        {            
            string id = "2091663814";

            try
            {
                if (Process.GetProcessesByName("Steam").Any())
                {
                    if (Process.GetProcessesByName(Config.SteamVRProcessName).Any())
                        throw new Exception("SteamVR is running and preventing opening store.");

                    Process.Start("steam://url/CommunityFilePage/" + id);
                }
                else
                {
                    throw new Exception("Steam not running");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Process.Start("https://steamcommunity.com/sharedfiles/filedetails/?id=" + id);
                }
                catch (Exception exx)
                {
                    string msg = "Sorry, unable to open the Steam page.  :("
                            + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + exx.Message;
                    MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void ButtonEmail_Click(object sender, EventArgs e)
        {
            string separator = "__________________________________________________________________________________";

            string addr = "bitslo" + "." + "cableguardian";
            addr += "@g";
            addr += "ma" + "il.";
            addr += "com";

            string txt = addr;
            txt += Environment.NewLine;
            txt += "*******************************";
            txt += Environment.NewLine;
            txt += "^ Send to the address above.";
            txt += Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            txt += "TYPE YOUR MESSAGE HERE. ATTACH SCREENSHOTS WHEN NEEDED.";
            txt += Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            txt += "IF YOU ARE REPORTING AN ISSUE, PLEASE INCLUDE EVERYTHING BELOW THIS LINE UNEDITED.";
            txt += Environment.NewLine;
            txt += separator;
            txt += Environment.NewLine;
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

            string msg = $"Email body copied to clipboard. Create a new email and paste the clipboard contents into the empty message. {Environment.NewLine}{Environment.NewLine}"
              + $"Copy the receiver address from the first line. Don't forget to include your description of the issue / idea." + Environment.NewLine+ Environment.NewLine
              +"Thanks!";
            try
            {
                Clipboard.SetText(txt);                
            }
            catch (Exception ex)
            {
                msg = "Sorry, copying the email body to clipboard failed*. Please send your message to " + addr + Environment.NewLine + Environment.NewLine + "* " + ex.Message;
            }
            
            MessageBox.Show(this, msg, "SUPPORT EMAIL", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private void LabelSimple_Click(object sender, EventArgs e)
        {
            string msg = $"Too much hassle? Click Yes to go back to simplified mode. You will lose the changes you made in full mode.{Environment.NewLine}{Environment.NewLine}"
               + $"Click No to stay in full mode.";
            if (MessageBox.Show(this, msg, "Switch to simplified mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
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
                    if (item != buttonPage && item != buttonClose && item != labelSimple)
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
                    if (item != buttonPage && item != buttonClose && item != labelSimple)
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
