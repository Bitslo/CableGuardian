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
    partial class WaveEditor : UserControl
    {
        public EventHandler<ChangeEventArgs> ChangeMade;
                
        CGActionWave TheWave;
        ToolTip TTip = new ToolTip() { AutoPopDelay = 20000 };

        bool SkipFlaggedEventHandlers = false;
       
        public WaveEditor()
        {
            InitializeComponent();

            if (!FormMain.RunFromDesigner)
            {
                comboBoxWave.DataSource = CGActionWave.AvailableWaves;
                TTip.SetToolTip(pictureBoxRefresh, $"Scan for wave files (*.wav) in: \"{Config.ExeFolder}\"");
                TTip.SetToolTip(labelNoWaves, $"Please add wave files (*.wav) to \"{Config.ExeFolder}\" and click the refresh button.");
                TTip.SetToolTip(comboBoxWave, $"Due to audio implementation, max length for a wave is 5 seconds.");

                InitializeAppearance();
            }
                                    
            AddEventHandlers();
            
        }

        void InitializeAppearance()
        {   
            labelNoWaves.ForeColor = Config.CGErrorColor;
        }

        public void LoadWaveAction(CGActionWave theWave)
        {
            TheWave = theWave;
            LoadValuesToGui(); 
        }
                

        void AddEventHandlers()
        {
            comboBoxWave.SelectedIndexChanged += ComboBoxWave_SelectedIndexChanged;
            trackBarVolume.ValueChanged += TrackBarVolume_ValueChanged;
            trackBarPan.ValueChanged += TrackBarPan_ValueChanged;            
            pictureBoxPlay.Click += PictureBoxPlay_Click;
            pictureBoxRefresh.Click += PictureBoxRefresh_Click;

            KeyUp += AnyControl_KeyUp;
            foreach (Control ctl in Controls)
            {
                ctl.KeyUp += AnyControl_KeyUp;
            }
        }

        private void PictureBoxRefresh_Click(object sender, EventArgs e)
        {
            RefreshWaveCombo();
        }

        private void AnyControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)' ')
            {
                TheWave.Play();
            }
        }

        void LoadValuesToGui()
        {
            SkipFlaggedEventHandlers = true;

            string wave = TheWave.Wave;
            if (!String.IsNullOrWhiteSpace(wave) && comboBoxWave.Items.Contains(wave))
                comboBoxWave.SelectedItem = wave;
            else if (comboBoxWave.Items.Count > 0)
            {
                comboBoxWave.SelectedItem = comboBoxWave.Items[0];
                TheWave.Wave = comboBoxWave.Items[0].ToString();
            }

            trackBarVolume.Value = TheWave.Volume;
            trackBarPan.Value = TheWave.Pan;

            SkipFlaggedEventHandlers = false;

            SetVolumeLabelText();
            SetPanLabelText();

            CheckNoWaveState();
        }

        private void TrackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TheWave.Volume = trackBarVolume.Value;
            SetVolumeLabelText();
            InvokeChangeMade(new ChangeEventArgs(trackBarVolume));
        }
                
        private void TrackBarPan_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TheWave.Pan = trackBarPan.Value;
            SetPanLabelText();
            InvokeChangeMade(new ChangeEventArgs(trackBarPan));
        }

        void SetVolumeLabelText()
        {
            labelVolVal.Text = trackBarVolume.Value.ToString();
        }

        void SetPanLabelText()
        {
            int val = (trackBarPan.Value / 2);            
            labelL.Text = "L" + (50 - val).ToString();
            labelR.Text = (50 + val).ToString() + "R";
        }

        private void PictureBoxPlay_Click(object sender, EventArgs e)
        {
            TheWave.Play();
        }

        private void ComboBoxWave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TheWave.Wave = comboBoxWave.SelectedItem.ToString();
            InvokeChangeMade(new ChangeEventArgs(comboBoxWave));
        }

        void RefreshWaveCombo()
        {            
            object selectedItem = comboBoxWave.SelectedItem;

            SkipFlaggedEventHandlers = true;
            Enabled = false;
            comboBoxWave.DataSource = null;
            CGActionWave.ScanWaveFilesInFolder(Config.ExeFolder);
            comboBoxWave.DataSource = CGActionWave.AvailableWaves;
            Enabled = true;
           
            if (selectedItem != null && comboBoxWave.Items.Contains(selectedItem))
            {
                comboBoxWave.SelectedItem = selectedItem;
            }
            else if (comboBoxWave.SelectedItem != null)
            {
                TheWave.Wave = comboBoxWave.SelectedItem.ToString();
            }
            SkipFlaggedEventHandlers = false;

            CheckNoWaveState();           
        }

        void CheckNoWaveState()
        {
            bool enabled = (comboBoxWave.Items.Count > 0);
            foreach (Control ctl in Controls)
            {
                if (ctl != pictureBoxRefresh && ctl != labelNoWaves)
                {
                    ctl.Enabled = enabled;
                }
            }
            labelNoWaves.Visible = !enabled;
        }


        void InvokeChangeMade(ChangeEventArgs e)
        {
            if (ChangeMade != null)
            {
                ChangeMade(this, e);
            }
        }

    }
}
