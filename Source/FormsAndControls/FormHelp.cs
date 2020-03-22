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
            
            labelVersion.Text = Config.ProgramTitle + " v." +
                                System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString() +
                                " \u00A9 Bitslo";

            if (!RunFromDesigner)
            {                
                labelTray.ForeColor = Config.CGColor;
                labelSaves.ForeColor = Config.CGColor;             
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
