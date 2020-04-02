namespace CableGuardian
{
    partial class ProfileEditor
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
            this.checkBoxStartup = new System.Windows.Forms.CheckBox();
            this.checkBoxFreeze = new System.Windows.Forms.CheckBox();
            this.labelAudio = new System.Windows.Forms.Label();
            this.comboBoxManual = new System.Windows.Forms.ComboBox();
            this.pictureBoxPlus = new System.Windows.Forms.PictureBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxDeviceSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxActions = new System.Windows.Forms.ListBox();
            this.pictureBoxMinus = new System.Windows.Forms.PictureBox();
            this.labelOcuChanges = new System.Windows.Forms.Label();
            this.labelAPI = new System.Windows.Forms.Label();
            this.comboBoxAPI = new System.Windows.Forms.ComboBox();
            this.checkBoxHome = new System.Windows.Forms.CheckBox();
            this.WaveActionCtl = new CableGuardian.WaveActionControl();
            this.pictureBoxClone = new System.Windows.Forms.PictureBox();
            this.checkBoxResetOnMount = new System.Windows.Forms.CheckBox();
            this.checkBoxMountingSound = new System.Windows.Forms.CheckBox();
            this.pictureBoxMounting = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMounting)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxStartup
            // 
            this.checkBoxStartup.AutoSize = true;
            this.checkBoxStartup.ForeColor = System.Drawing.Color.White;
            this.checkBoxStartup.Location = new System.Drawing.Point(9, 38);
            this.checkBoxStartup.Name = "checkBoxStartup";
            this.checkBoxStartup.Size = new System.Drawing.Size(91, 17);
            this.checkBoxStartup.TabIndex = 12;
            this.checkBoxStartup.Text = "Startup profile";
            this.checkBoxStartup.UseVisualStyleBackColor = true;
            // 
            // checkBoxFreeze
            // 
            this.checkBoxFreeze.AutoSize = true;
            this.checkBoxFreeze.ForeColor = System.Drawing.Color.White;
            this.checkBoxFreeze.Location = new System.Drawing.Point(106, 38);
            this.checkBoxFreeze.Name = "checkBoxFreeze";
            this.checkBoxFreeze.Size = new System.Drawing.Size(89, 17);
            this.checkBoxFreeze.TabIndex = 16;
            this.checkBoxFreeze.Text = "Freeze profile";
            this.checkBoxFreeze.UseVisualStyleBackColor = true;
            // 
            // labelAudio
            // 
            this.labelAudio.AutoSize = true;
            this.labelAudio.ForeColor = System.Drawing.Color.White;
            this.labelAudio.Location = new System.Drawing.Point(308, 11);
            this.labelAudio.Name = "labelAudio";
            this.labelAudio.Size = new System.Drawing.Size(72, 13);
            this.labelAudio.TabIndex = 46;
            this.labelAudio.Text = "Audio device:";
            // 
            // comboBoxManual
            // 
            this.comboBoxManual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManual.FormattingEnabled = true;
            this.comboBoxManual.Location = new System.Drawing.Point(484, 8);
            this.comboBoxManual.Name = "comboBoxManual";
            this.comboBoxManual.Size = new System.Drawing.Size(199, 21);
            this.comboBoxManual.TabIndex = 8;
            // 
            // pictureBoxPlus
            // 
            this.pictureBoxPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxPlus.Image = global::CableGuardian.Properties.Resources.PlusSmall;
            this.pictureBoxPlus.Location = new System.Drawing.Point(623, 39);
            this.pictureBoxPlus.Name = "pictureBoxPlus";
            this.pictureBoxPlus.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxPlus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPlus.TabIndex = 1;
            this.pictureBoxPlus.TabStop = false;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(60, 8);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(125, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // comboBoxDeviceSource
            // 
            this.comboBoxDeviceSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDeviceSource.FormattingEnabled = true;
            this.comboBoxDeviceSource.Location = new System.Drawing.Point(383, 8);
            this.comboBoxDeviceSource.Name = "comboBoxDeviceSource";
            this.comboBoxDeviceSource.Size = new System.Drawing.Size(93, 21);
            this.comboBoxDeviceSource.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Name:";
            // 
            // listBoxActions
            // 
            this.listBoxActions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxActions.FormattingEnabled = true;
            this.listBoxActions.ItemHeight = 30;
            this.listBoxActions.Location = new System.Drawing.Point(9, 65);
            this.listBoxActions.Name = "listBoxActions";
            this.listBoxActions.Size = new System.Drawing.Size(675, 274);
            this.listBoxActions.TabIndex = 20;
            // 
            // pictureBoxMinus
            // 
            this.pictureBoxMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxMinus.Image = global::CableGuardian.Properties.Resources.MinusSmall;
            this.pictureBoxMinus.Location = new System.Drawing.Point(667, 39);
            this.pictureBoxMinus.Name = "pictureBoxMinus";
            this.pictureBoxMinus.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxMinus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxMinus.TabIndex = 59;
            this.pictureBoxMinus.TabStop = false;
            // 
            // labelOcuChanges
            // 
            this.labelOcuChanges.AutoSize = true;
            this.labelOcuChanges.ForeColor = System.Drawing.Color.White;
            this.labelOcuChanges.Location = new System.Drawing.Point(488, 11);
            this.labelOcuChanges.Name = "labelOcuChanges";
            this.labelOcuChanges.Size = new System.Drawing.Size(194, 13);
            this.labelOcuChanges.TabIndex = 60;
            this.labelOcuChanges.Text = "No auto-refresh for Oculus Home audio.";
            this.labelOcuChanges.Visible = false;
            // 
            // labelAPI
            // 
            this.labelAPI.AutoSize = true;
            this.labelAPI.ForeColor = System.Drawing.Color.White;
            this.labelAPI.Location = new System.Drawing.Point(191, 11);
            this.labelAPI.Name = "labelAPI";
            this.labelAPI.Size = new System.Drawing.Size(27, 13);
            this.labelAPI.TabIndex = 62;
            this.labelAPI.Text = "API:";
            // 
            // comboBoxAPI
            // 
            this.comboBoxAPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAPI.FormattingEnabled = true;
            this.comboBoxAPI.Location = new System.Drawing.Point(220, 8);
            this.comboBoxAPI.Name = "comboBoxAPI";
            this.comboBoxAPI.Size = new System.Drawing.Size(81, 21);
            this.comboBoxAPI.TabIndex = 61;
            // 
            // checkBoxHome
            // 
            this.checkBoxHome.AutoSize = true;
            this.checkBoxHome.ForeColor = System.Drawing.Color.White;
            this.checkBoxHome.Location = new System.Drawing.Point(507, 38);
            this.checkBoxHome.Name = "checkBoxHome";
            this.checkBoxHome.Size = new System.Drawing.Size(94, 17);
            this.checkBoxHome.TabIndex = 63;
            this.checkBoxHome.Text = "Require Home";
            this.checkBoxHome.UseVisualStyleBackColor = true;
            // 
            // WaveActionCtl
            // 
            this.WaveActionCtl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WaveActionCtl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.WaveActionCtl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WaveActionCtl.Location = new System.Drawing.Point(9, 349);
            this.WaveActionCtl.Name = "WaveActionCtl";
            this.WaveActionCtl.Size = new System.Drawing.Size(675, 85);
            this.WaveActionCtl.TabIndex = 26;
            // 
            // pictureBoxClone
            // 
            this.pictureBoxClone.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxClone.Image = global::CableGuardian.Properties.Resources.CloneSmall;
            this.pictureBoxClone.Location = new System.Drawing.Point(645, 39);
            this.pictureBoxClone.Name = "pictureBoxClone";
            this.pictureBoxClone.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxClone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxClone.TabIndex = 64;
            this.pictureBoxClone.TabStop = false;
            // 
            // checkBoxResetOnMount
            // 
            this.checkBoxResetOnMount.AutoSize = true;
            this.checkBoxResetOnMount.ForeColor = System.Drawing.Color.White;
            this.checkBoxResetOnMount.Location = new System.Drawing.Point(349, 38);
            this.checkBoxResetOnMount.Name = "checkBoxResetOnMount";
            this.checkBoxResetOnMount.Size = new System.Drawing.Size(152, 17);
            this.checkBoxResetOnMount.TabIndex = 65;
            this.checkBoxResetOnMount.Text = "Reset turn count on mount";
            this.checkBoxResetOnMount.UseVisualStyleBackColor = true;
            // 
            // checkBoxMountingSound
            // 
            this.checkBoxMountingSound.AutoSize = true;
            this.checkBoxMountingSound.ForeColor = System.Drawing.Color.White;
            this.checkBoxMountingSound.Location = new System.Drawing.Point(201, 38);
            this.checkBoxMountingSound.Name = "checkBoxMountingSound";
            this.checkBoxMountingSound.Size = new System.Drawing.Size(102, 17);
            this.checkBoxMountingSound.TabIndex = 66;
            this.checkBoxMountingSound.Text = "Mounting sound";
            this.checkBoxMountingSound.UseVisualStyleBackColor = true;
            // 
            // pictureBoxMounting
            // 
            this.pictureBoxMounting.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxMounting.Image = global::CableGuardian.Properties.Resources.Action;
            this.pictureBoxMounting.Location = new System.Drawing.Point(316, 35);
            this.pictureBoxMounting.Name = "pictureBoxMounting";
            this.pictureBoxMounting.Size = new System.Drawing.Size(21, 21);
            this.pictureBoxMounting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMounting.TabIndex = 121;
            this.pictureBoxMounting.TabStop = false;
            // 
            // ProfileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.pictureBoxMounting);
            this.Controls.Add(this.checkBoxMountingSound);
            this.Controls.Add(this.checkBoxResetOnMount);
            this.Controls.Add(this.pictureBoxClone);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.checkBoxStartup);
            this.Controls.Add(this.checkBoxFreeze);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxHome);
            this.Controls.Add(this.labelAPI);
            this.Controls.Add(this.comboBoxAPI);
            this.Controls.Add(this.labelOcuChanges);
            this.Controls.Add(this.pictureBoxMinus);
            this.Controls.Add(this.WaveActionCtl);
            this.Controls.Add(this.listBoxActions);
            this.Controls.Add(this.comboBoxDeviceSource);
            this.Controls.Add(this.pictureBoxPlus);
            this.Controls.Add(this.comboBoxManual);
            this.Controls.Add(this.labelAudio);
            this.Name = "ProfileEditor";
            this.Size = new System.Drawing.Size(695, 445);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMounting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxStartup;
        private System.Windows.Forms.CheckBox checkBoxFreeze;
        private System.Windows.Forms.Label labelAudio;
        private System.Windows.Forms.ComboBox comboBoxManual;
        private System.Windows.Forms.PictureBox pictureBoxPlus;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxDeviceSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxActions;
        private WaveActionControl WaveActionCtl;
        private System.Windows.Forms.PictureBox pictureBoxMinus;
        private System.Windows.Forms.Label labelOcuChanges;
        private System.Windows.Forms.Label labelAPI;
        private System.Windows.Forms.ComboBox comboBoxAPI;
        private System.Windows.Forms.CheckBox checkBoxHome;
        private System.Windows.Forms.PictureBox pictureBoxClone;
        private System.Windows.Forms.CheckBox checkBoxResetOnMount;
        private System.Windows.Forms.CheckBox checkBoxMountingSound;
        private System.Windows.Forms.PictureBox pictureBoxMounting;
    }
}
