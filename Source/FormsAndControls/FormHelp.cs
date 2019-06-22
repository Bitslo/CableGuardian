using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CableGuardian
{
    public partial class FormHelp : Form
    {
        int CurrentPage = 1;
        int NumberOfPages = 2;
        static bool RunFromDesigner { get { return (LicenseManager.UsageMode == LicenseUsageMode.Designtime); } }

        public FormHelp()
        {
            InitializeComponent();

            buttonClose.Click += ButtonClose_Click;
            buttonPage.Click += ButtonPage_Click;

            labelAddWaves.Text = $"You can add your own sound files (*.wav) by copying them into: \"{Config.ExeFolder}\"";            
            labelCopyDll.Text = $"For Oculus users: If you get a dll-error (unlikely), try copying LibOVRRT64_1.dll and LibOVRRT32_1.dll from {Environment.NewLine} " +
                                $"\".\\Oculus\\Support\\oculus-runtime\" into: \"{Config.ExeFolder}\"";
            
            labelVersion.Text = Config.ProgramTitle + " v." +
                                System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString() +
                                " \u00A9 Bitslo";

            if (!RunFromDesigner)
            {
                labelAddWaves.ForeColor = Config.CGColor;
                labelTray.ForeColor = Config.CGColor;
                labelSaves.ForeColor = Config.CGColor;
                labelCopyDll.ForeColor = Config.CGErrorColor;
                labelRestart.ForeColor = Config.CGErrorColor;
            }

            SetLayoutForCurrentPage();
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

            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
