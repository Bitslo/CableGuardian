namespace CableGuardian
{
    partial class WaveEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarPan = new System.Windows.Forms.TrackBar();
            this.labelL = new System.Windows.Forms.Label();
            this.labelR = new System.Windows.Forms.Label();
            this.comboBoxWave = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelVolVal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxRefresh = new System.Windows.Forms.PictureBox();
            this.pictureBoxPlay = new System.Windows.Forms.PictureBox();
            this.labelNoWaves = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownLoop = new System.Windows.Forms.NumericUpDown();
            this.pictureBoxAddWaves = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAddWaves)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(189, 4);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(104, 45);
            this.trackBarVolume.TabIndex = 4;
            this.trackBarVolume.Value = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(197, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Volume";
            // 
            // trackBarPan
            // 
            this.trackBarPan.Location = new System.Drawing.Point(294, 4);
            this.trackBarPan.Maximum = 100;
            this.trackBarPan.Minimum = -100;
            this.trackBarPan.Name = "trackBarPan";
            this.trackBarPan.Size = new System.Drawing.Size(104, 45);
            this.trackBarPan.SmallChange = 2;
            this.trackBarPan.TabIndex = 8;
            this.trackBarPan.TickFrequency = 2;
            // 
            // labelL
            // 
            this.labelL.ForeColor = System.Drawing.Color.White;
            this.labelL.Location = new System.Drawing.Point(301, 34);
            this.labelL.Name = "labelL";
            this.labelL.Size = new System.Drawing.Size(51, 13);
            this.labelL.TabIndex = 5;
            this.labelL.Text = "L";
            // 
            // labelR
            // 
            this.labelR.ForeColor = System.Drawing.Color.White;
            this.labelR.Location = new System.Drawing.Point(348, 34);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(46, 13);
            this.labelR.TabIndex = 6;
            this.labelR.Text = "R";
            this.labelR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxWave
            // 
            this.comboBoxWave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWave.FormattingEnabled = true;
            this.comboBoxWave.Location = new System.Drawing.Point(6, 25);
            this.comboBoxWave.Name = "comboBoxWave";
            this.comboBoxWave.Size = new System.Drawing.Size(128, 21);
            this.comboBoxWave.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "PLAY sound:";
            // 
            // labelVolVal
            // 
            this.labelVolVal.ForeColor = System.Drawing.Color.White;
            this.labelVolVal.Location = new System.Drawing.Point(263, 34);
            this.labelVolVal.Name = "labelVolVal";
            this.labelVolVal.Size = new System.Drawing.Size(35, 13);
            this.labelVolVal.TabIndex = 10;
            this.labelVolVal.Text = "100";
            this.labelVolVal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(397, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "(space)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(333, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Pan";
            // 
            // pictureBoxRefresh
            // 
            this.pictureBoxRefresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxRefresh.Image = global::CableGuardian.Properties.Resources.Refresh24;
            this.pictureBoxRefresh.Location = new System.Drawing.Point(165, 22);
            this.pictureBoxRefresh.Name = "pictureBoxRefresh";
            this.pictureBoxRefresh.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRefresh.TabIndex = 14;
            this.pictureBoxRefresh.TabStop = false;
            // 
            // pictureBoxPlay
            // 
            this.pictureBoxPlay.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxPlay.Image = global::CableGuardian.Properties.Resources.Play;
            this.pictureBoxPlay.Location = new System.Drawing.Point(410, 3);
            this.pictureBoxPlay.Name = "pictureBoxPlay";
            this.pictureBoxPlay.Size = new System.Drawing.Size(20, 32);
            this.pictureBoxPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPlay.TabIndex = 9;
            this.pictureBoxPlay.TabStop = false;
            // 
            // labelNoWaves
            // 
            this.labelNoWaves.AutoSize = true;
            this.labelNoWaves.BackColor = System.Drawing.Color.White;
            this.labelNoWaves.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelNoWaves.Location = new System.Drawing.Point(9, 29);
            this.labelNoWaves.Name = "labelNoWaves";
            this.labelNoWaves.Size = new System.Drawing.Size(106, 13);
            this.labelNoWaves.TabIndex = 15;
            this.labelNoWaves.Tag = "MANUAL";
            this.labelNoWaves.Text = "NO WAVES FOUND";
            this.labelNoWaves.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(82, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 61;
            this.label3.Tag = "";
            this.label3.Text = "X";
            // 
            // numericUpDownLoop
            // 
            this.numericUpDownLoop.Location = new System.Drawing.Point(97, 3);
            this.numericUpDownLoop.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownLoop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLoop.Name = "numericUpDownLoop";
            this.numericUpDownLoop.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownLoop.TabIndex = 60;
            this.numericUpDownLoop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownLoop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pictureBoxAddWaves
            // 
            this.pictureBoxAddWaves.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxAddWaves.Image = global::CableGuardian.Properties.Resources.Explorer;
            this.pictureBoxAddWaves.Location = new System.Drawing.Point(138, 25);
            this.pictureBoxAddWaves.Name = "pictureBoxAddWaves";
            this.pictureBoxAddWaves.Size = new System.Drawing.Size(24, 20);
            this.pictureBoxAddWaves.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxAddWaves.TabIndex = 62;
            this.pictureBoxAddWaves.TabStop = false;
            // 
            // WaveEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.pictureBoxAddWaves);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownLoop);
            this.Controls.Add(this.labelNoWaves);
            this.Controls.Add(this.pictureBoxRefresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelVolVal);
            this.Controls.Add(this.pictureBoxPlay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxWave);
            this.Controls.Add(this.labelR);
            this.Controls.Add(this.labelL);
            this.Controls.Add(this.trackBarPan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarVolume);
            this.Name = "WaveEditor";
            this.Size = new System.Drawing.Size(437, 49);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAddWaves)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarPan;
        private System.Windows.Forms.Label labelL;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.ComboBox comboBoxWave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBoxPlay;
        private System.Windows.Forms.Label labelVolVal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxRefresh;
        private System.Windows.Forms.Label labelNoWaves;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownLoop;
        private System.Windows.Forms.PictureBox pictureBoxAddWaves;
    }
}
