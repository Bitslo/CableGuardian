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
            this.labelAddHint = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxDeviceSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxActions = new System.Windows.Forms.ListBox();
            this.pictureBoxMinus = new System.Windows.Forms.PictureBox();
            this.labelOcuChanges = new System.Windows.Forms.Label();
            this.WaveActionCtl = new CableGuardian.WaveActionControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxStartup
            // 
            this.checkBoxStartup.AutoSize = true;
            this.checkBoxStartup.ForeColor = System.Drawing.Color.White;
            this.checkBoxStartup.Location = new System.Drawing.Point(9, 38);
            this.checkBoxStartup.Name = "checkBoxStartup";
            this.checkBoxStartup.Size = new System.Drawing.Size(122, 17);
            this.checkBoxStartup.TabIndex = 12;
            this.checkBoxStartup.Text = "Set as startup profile";
            this.checkBoxStartup.UseVisualStyleBackColor = true;
            // 
            // checkBoxFreeze
            // 
            this.checkBoxFreeze.AutoSize = true;
            this.checkBoxFreeze.ForeColor = System.Drawing.Color.White;
            this.checkBoxFreeze.Location = new System.Drawing.Point(134, 38);
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
            this.labelAudio.Location = new System.Drawing.Point(246, 11);
            this.labelAudio.Name = "labelAudio";
            this.labelAudio.Size = new System.Drawing.Size(107, 13);
            this.labelAudio.TabIndex = 46;
            this.labelAudio.Text = "Audio device source:";
            // 
            // comboBoxManual
            // 
            this.comboBoxManual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManual.FormattingEnabled = true;
            this.comboBoxManual.Location = new System.Drawing.Point(455, 8);
            this.comboBoxManual.Name = "comboBoxManual";
            this.comboBoxManual.Size = new System.Drawing.Size(229, 21);
            this.comboBoxManual.TabIndex = 8;
            // 
            // pictureBoxPlus
            // 
            this.pictureBoxPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPlus.Image = global::CableGuardian.Properties.Resources.PlusSmall;
            this.pictureBoxPlus.Location = new System.Drawing.Point(645, 39);
            this.pictureBoxPlus.Name = "pictureBoxPlus";
            this.pictureBoxPlus.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxPlus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPlus.TabIndex = 1;
            this.pictureBoxPlus.TabStop = false;
            // 
            // labelAddHint
            // 
            this.labelAddHint.AutoSize = true;
            this.labelAddHint.ForeColor = System.Drawing.Color.White;
            this.labelAddHint.Location = new System.Drawing.Point(448, 39);
            this.labelAddHint.Name = "labelAddHint";
            this.labelAddHint.Size = new System.Drawing.Size(193, 13);
            this.labelAddHint.TabIndex = 52;
            this.labelAddHint.Tag = "MANUAL";
            this.labelAddHint.Text = "Click the plus sign to add an action  --->";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(60, 8);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(176, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // comboBoxDeviceSource
            // 
            this.comboBoxDeviceSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDeviceSource.FormattingEnabled = true;
            this.comboBoxDeviceSource.Location = new System.Drawing.Point(358, 8);
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
            this.pictureBoxMinus.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.labelOcuChanges.Location = new System.Drawing.Point(455, 11);
            this.labelOcuChanges.Name = "labelOcuChanges";
            this.labelOcuChanges.Size = new System.Drawing.Size(231, 13);
            this.labelOcuChanges.TabIndex = 60;
            this.labelOcuChanges.Text = "Oculus Home setting is not auto-refreshed here.";
            this.labelOcuChanges.Visible = false;
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
            // ProfileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.labelOcuChanges);
            this.Controls.Add(this.pictureBoxMinus);
            this.Controls.Add(this.WaveActionCtl);
            this.Controls.Add(this.listBoxActions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDeviceSource);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.pictureBoxPlus);
            this.Controls.Add(this.labelAddHint);
            this.Controls.Add(this.comboBoxManual);
            this.Controls.Add(this.labelAudio);
            this.Controls.Add(this.checkBoxFreeze);
            this.Controls.Add(this.checkBoxStartup);
            this.Name = "ProfileEditor";
            this.Size = new System.Drawing.Size(695, 445);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxStartup;
        private System.Windows.Forms.CheckBox checkBoxFreeze;
        private System.Windows.Forms.Label labelAudio;
        private System.Windows.Forms.ComboBox comboBoxManual;
        private System.Windows.Forms.PictureBox pictureBoxPlus;
        private System.Windows.Forms.Label labelAddHint;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxDeviceSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxActions;
        private WaveActionControl WaveActionCtl;
        private System.Windows.Forms.PictureBox pictureBoxMinus;
        private System.Windows.Forms.Label labelOcuChanges;
    }
}
