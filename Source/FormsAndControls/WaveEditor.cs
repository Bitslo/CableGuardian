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
        public event EventHandler<ChangeEventArgs> ChangeMade;
                
        CGActionWave TheWave;
        ToolTip TTip = new ToolTip() { AutoPopDelay = 20000 };

        bool SkipFlaggedEventHandlers = false;
       
        public WaveEditor()
        {
            InitializeComponent();

            if (!FormMain.RunFromDesigner)
            {
                comboBoxWave.DataSource = WaveFilePool.GetAvailableWaves();
                TTip.SetToolTip(pictureBoxRefresh, $"Refresh available sounds." + Environment.NewLine +
                                                    $"Put your custom sound files ({WaveFilePool.WaveFileExtension}) in: \"{WaveFilePool.WaveFolder}\"");
                TTip.SetToolTip(pictureBoxAddWaves, $"Open Windows explorer to manage your custom sounds ({WaveFilePool.WaveFileExtension}).");
                TTip.SetToolTip(labelNoWaves, $"Please add sound files ({WaveFilePool.WaveFileExtension}) to \"{WaveFilePool.WaveFolder}\" and click the refresh button.");
                TTip.SetToolTip(comboBoxWave, $"Select the sound file to play. Due to audio implementation, only the first 5 seconds of the wave will be played.");
                TTip.SetToolTip(numericUpDownLoop, $"Loop count. How many times the sound is played in succession per single trigger. Max=9.");
                TTip.SetToolTip(trackBarPan, $"Mouse middle button = center");

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
            trackBarPan.MouseUp += TrackBarPan_MouseUp;
            numericUpDownLoop.ValueChanged += NumericUpDownLoop_ValueChanged;
            pictureBoxPlay.Click += PictureBoxPlay_Click;
            pictureBoxRefresh.Click += PictureBoxRefresh_Click;            
            pictureBoxAddWaves.Click += PictureBoxAddWaves_Click;

            pictureBoxRefresh.MouseEnter += (s, e) => { pictureBoxRefresh.Image = Properties.Resources.Refresh24_hover; };
            pictureBoxRefresh.MouseLeave += (s, e) => { pictureBoxRefresh.Image = Properties.Resources.Refresh24; };
            pictureBoxAddWaves.MouseEnter += (s, e) => { pictureBoxAddWaves.Image = Properties.Resources.Explorer_hover; };
            pictureBoxAddWaves.MouseLeave += (s, e) => { pictureBoxAddWaves.Image = Properties.Resources.Explorer; };
            pictureBoxPlay.MouseEnter += (s, e) => { pictureBoxPlay.Image = Properties.Resources.Play_hover; };
            pictureBoxPlay.MouseLeave += (s, e) => { pictureBoxPlay.Image = Properties.Resources.Play; };

            KeyUp += AnyControl_KeyUp;
            foreach (Control ctl in Controls)
            {
                ctl.KeyUp += AnyControl_KeyUp;
            }
        }

        private void PictureBoxAddWaves_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {
                    p.StartInfo.FileName = "explorer";
                    p.StartInfo.Arguments = "\"" + WaveFilePool.WaveFolder + "\"";
                    p.Start();
                }
            }
            catch (Exception ex)
            {
                string msg = $"Unable to open location: {WaveFilePool.WaveFolder} {Environment.NewLine}{ex.Message}";
                Config.WriteLog(msg);
                MessageBox.Show(this, msg, Config.ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
        }

        private void TrackBarPan_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                trackBarPan.Value = 0;
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

            if (Config.WaveComboRefreshRequired)  // needed when loading default profiles & sounds. A bit gimmicky.
            {
                RefreshWaveCombo();
                Config.WaveComboRefreshRequired = false;
            }
            
            object match = null;
            foreach (var item in comboBoxWave.Items)
            {
                if ((item as WaveFileInfo).ValueEquals(TheWave.Wave))
                {
                    match = item;
                }
            }

            if (match != null)
                comboBoxWave.SelectedItem = match;
            else if (comboBoxWave.Items.Count > 0)
            {
                comboBoxWave.SelectedItem = comboBoxWave.Items[0];
                TheWave.SetWave(comboBoxWave.Items[0] as WaveFileInfo);
            }

            trackBarVolume.Value = TheWave.Volume;
            trackBarPan.Value = TheWave.Pan;
            numericUpDownLoop.Value = TheWave.LoopCount;

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

        private void NumericUpDownLoop_ValueChanged(object sender, EventArgs e)
        {
            if (SkipFlaggedEventHandlers)
                return;

            TheWave.LoopCount = (int)numericUpDownLoop.Value;
            InvokeChangeMade(new ChangeEventArgs(numericUpDownLoop));
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

            TheWave.SetWave(comboBoxWave.SelectedItem as WaveFileInfo);
            InvokeChangeMade(new ChangeEventArgs(comboBoxWave));
        }

        void RefreshWaveCombo()
        {            
            WaveFileInfo previouslySelected = (comboBoxWave.SelectedItem as WaveFileInfo);

            SkipFlaggedEventHandlers = true;
            Enabled = false;
            comboBoxWave.DataSource = null;
            comboBoxWave.DataSource = WaveFilePool.GetAvailableWaves();
            Enabled = true;

            object matchToPreviouslySelected = null;
            foreach (var item in comboBoxWave.Items)
            {
                if ((item as WaveFileInfo).ValueEquals(previouslySelected))
                {
                    matchToPreviouslySelected = item;
                }
            }

            object matchToCurrentWave = null;
            foreach (var item in comboBoxWave.Items)
            {
                if ((item as WaveFileInfo).ValueEquals(TheWave?.Wave))
                {
                    matchToCurrentWave = item;
                }
            }

            if (matchToPreviouslySelected != null)
            {
                comboBoxWave.SelectedItem = matchToPreviouslySelected;
            }
            else if (matchToCurrentWave != null)
            {
                comboBoxWave.SelectedItem = matchToCurrentWave;
            }
            else if (previouslySelected != null)
            {
                TheWave.SetWave(previouslySelected);
            }
            SkipFlaggedEventHandlers = false;

            CheckNoWaveState();           
        }

        void CheckNoWaveState()
        {
            bool enabled = (comboBoxWave.Items.Count > 0);
            foreach (Control ctl in Controls)
            {
                if (ctl != pictureBoxRefresh && ctl != labelNoWaves && ctl != pictureBoxAddWaves)
                {
                    ctl.Enabled = enabled;
                }
            }
            labelNoWaves.Visible = !enabled;
        }


        void InvokeChangeMade(ChangeEventArgs e)
        {
            ChangeMade?.Invoke(this, e);
        }

    }
}
