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
            labelSimple.MouseEnter += (s, e) => { labelSimple.ForeColor = Color.Yellow; };
            labelSimple.MouseLeave += (s, e) => { labelSimple.ForeColor = Color.White; };
            labelSimple.Click += LabelSimple_Click; 

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

        private void LabelSimple_Click(object sender, EventArgs e)
        {
            string msg = $"Too much hassle? Click Yes to go back to simplified mode (with default settings).{Environment.NewLine}{Environment.NewLine}"
               + $"Click No to stay in full mode.";
            if (MessageBox.Show(this, msg, "Switch to simplified mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
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

            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
