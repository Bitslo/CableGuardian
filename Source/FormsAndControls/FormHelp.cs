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
        public FormHelp()
        {
            InitializeComponent();

            labelAddWaves.Text = $"You can add your own wave files (*.wav) by copying them into: \"{Config.ExeFolder}\"";
            buttonClose.Click += ButtonClose_Click;
            labelCopyDll.Text = $"If you get a dll-error: Try copying LibOVRRT64_1.dll and LibOVRRT32_1.dll from {Environment.NewLine} " +
                                $"\".\\Oculus\\Support\\oculus-runtime\" into: \"{Config.ExeFolder}\"";
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
