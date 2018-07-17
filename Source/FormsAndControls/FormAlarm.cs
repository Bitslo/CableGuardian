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
    public partial class FormAlarm : Form
    {
        public static bool RunFromDesigner { get { return (LicenseManager.UsageMode == LicenseUsageMode.Designtime); } }        

        public FormAlarm()
        {
            InitializeComponent();
            waveEditor1.LoadWaveAction(Config.Alarm);

            buttonClose.Click += (s,e) => { Close(); };

            if (!RunFromDesigner)
            {
                BackColor = Config.CGBackColor;
            }
        }
    }
}
