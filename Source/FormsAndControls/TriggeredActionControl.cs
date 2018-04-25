using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CableGuardian
{
   
    
    partial class TriggeredActionControl : UserControl
    {
        public TriggeredActionControl()
        {
            InitializeComponent();
        }

        public virtual void Release()
        {

        }        
    }

    class TriggeredActionControlEventArgs : EventArgs
    {
        public TriggeredAction TheAction { get; }
        public TriggeredActionControl TheControl { get; }
        public TriggeredActionControlEventArgs(TriggeredAction theAction, TriggeredActionControl theControl)
        {
            TheAction = theAction;
            TheControl = theControl;
        }
    }

}
