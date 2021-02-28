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

namespace CableGuardian
{
    public partial class FormHelpSimple : Form
    {
        ToolTip TTip = new ToolTip() { AutoPopDelay = 30000, OwnerDraw = true, IsBalloon = false };

        public FormHelpSimple()
        {
            InitializeComponent();

            buttonClose.Click += ButtonClose_Click;
            buttonGuides.Click += ButtonGuides_Click;
            buttonDiscussions.Click += ButtonDiscussions_Click;
            labelVersion.Text = Config.ProgramTitle + " v." +
                                System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString() +
                                " \u00A9 Bitslo";

            TTip.SetToolTip(buttonGuides, $"See the {Config.ProgramTitle} user guides on Steam.");
            TTip.SetToolTip(buttonDiscussions, "Steam Discussions may have the answer your looking for. Feel free to start a new discussion!");
        }

        private void ButtonGuides_Click(object sender, EventArgs e)
        {
            FormMain.OpenSteamPage("steam://openurl/https://steamcommunity.com/app/1208080/guides/", "https://steamcommunity.com/app/1208080/guides/", this);
        }

        private void ButtonDiscussions_Click(object sender, EventArgs e)
        {
            FormMain.OpenSteamPage("steam://openurl/https://steamcommunity.com/app/1208080/discussions/0/", "https://steamcommunity.com/app/1208080/discussions/0/", this);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
