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

        Point MouseDragPosOnForm = new Point(0, 0);
        bool MouseDownOnComboBox = false;

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

            AddDragEventHandlers(this);
        }

        private void OnChangeMade(object sender, ChangeEventArgs e)
        {
            if (InvokesProfileChanged)
            {
                ProfileChangeMade?.Invoke(this, new EventArgs());
            }
        }

        void AddDragEventHandlers(Control c)
        {
            if (c is Form || c is Panel || c is Label)
            {
                c.MouseMove += DragPoint_MouseMove;
                c.MouseDown += DragPoint_MouseDown;
            }
            if (c is ComboBox)
            {
                c.MouseDown += (s, e) => { MouseDownOnComboBox = true; };
            }
            foreach (Control ctl in c.Controls)
            {
                if (ctl is TrackBar == false)
                    AddDragEventHandlers(ctl);
            }
        }
        private void DragPoint_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDragPosOnForm = PointToClient(Cursor.Position);
            MouseDownOnComboBox = false;
        }

        private void DragPoint_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDownOnComboBox) // comboboxes were causing some erratic form movement
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                Location = new Point(Cursor.Position.X - MouseDragPosOnForm.X, Cursor.Position.Y - MouseDragPosOnForm.Y);

            }
        }
    }
}
