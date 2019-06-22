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
    public partial class FormSound : Form
    {
        internal FormSound()
        {
            InitializeComponent();
        }

        internal FormSound(CGActionWave waveAction, string infoText = "")
        {
            InitializeComponent();
            waveEditor1.LoadWaveAction(waveAction);
            labelInfo.Text = infoText;

            buttonClose.Click += (s,e) => { Close(); };
            BackColor = Config.CGBackColor;
        }
    }
}
