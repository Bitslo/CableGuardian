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
        public event EventHandler<EventArgs> ProfileChangeMade;
        bool InvokesProfileChanged = false;

        internal FormSound()
        {
            InitializeComponent();
        }

        internal FormSound(CGActionWave waveAction, string infoText = "", bool invokesProfileChanged = false)
        {
            InitializeComponent();
            InvokesProfileChanged = invokesProfileChanged;
            waveEditor1.ChangeMade += OnChangeMade;
            waveEditor1.LoadWaveAction(waveAction);
            labelInfo.Text = infoText;

            buttonClose.Click += (s,e) => { Close(); };
            BackColor = Config.CGBackColor;
        }

        private void OnChangeMade(object sender, ChangeEventArgs e)
        {
            if (InvokesProfileChanged)
            {
                ProfileChangeMade?.Invoke(this, new EventArgs());
            }
        }
    }
}
