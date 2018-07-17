namespace CableGuardian
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.labelFullRotTitle = new System.Windows.Forms.Label();
            this.labelFullRot = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelYaw = new System.Windows.Forms.Label();
            this.pictureBoxMinimize = new System.Windows.Forms.PictureBox();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBoxStartMinimized = new System.Windows.Forms.CheckBox();
            this.checkBoxShowYaw = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxWindowsStart = new System.Windows.Forms.CheckBox();
            this.labelDataWarning = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.pictureBoxPlus = new System.Windows.Forms.PictureBox();
            this.labelProf = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.pictureBoxMinus = new System.Windows.Forms.PictureBox();
            this.pictureBoxHelp = new System.Windows.Forms.PictureBox();
            this.comboBoxAPI = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.labelMount = new System.Windows.Forms.Label();
            this.checkBoxHome = new System.Windows.Forms.CheckBox();
            this.checkBoxConnLost = new System.Windows.Forms.CheckBox();
            this.buttonAlarm = new System.Windows.Forms.Button();
            this.checkBoxTrayNotifications = new System.Windows.Forms.CheckBox();
            this.profileEditor = new CableGuardian.ProfileEditor();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // labelFullRotTitle
            // 
            this.labelFullRotTitle.AutoSize = true;
            this.labelFullRotTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFullRotTitle.ForeColor = System.Drawing.Color.White;
            this.labelFullRotTitle.Location = new System.Drawing.Point(5, 298);
            this.labelFullRotTitle.Name = "labelFullRotTitle";
            this.labelFullRotTitle.Size = new System.Drawing.Size(108, 20);
            this.labelFullRotTitle.TabIndex = 7;
            this.labelFullRotTitle.Text = "Full rotations: ";
            this.labelFullRotTitle.Visible = false;
            // 
            // labelFullRot
            // 
            this.labelFullRot.AutoSize = true;
            this.labelFullRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFullRot.ForeColor = System.Drawing.Color.White;
            this.labelFullRot.Location = new System.Drawing.Point(110, 298);
            this.labelFullRot.Name = "labelFullRot";
            this.labelFullRot.Size = new System.Drawing.Size(18, 20);
            this.labelFullRot.TabIndex = 9;
            this.labelFullRot.Text = "0";
            this.labelFullRot.Visible = false;
            // 
            // buttonReset
            // 
            this.buttonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReset.ForeColor = System.Drawing.Color.White;
            this.buttonReset.Location = new System.Drawing.Point(176, 249);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(143, 23);
            this.buttonReset.TabIndex = 20;
            this.buttonReset.Text = "Reset rotation counter";
            this.buttonReset.UseVisualStyleBackColor = true;
            // 
            // labelYaw
            // 
            this.labelYaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYaw.ForeColor = System.Drawing.Color.White;
            this.labelYaw.Location = new System.Drawing.Point(5, 280);
            this.labelYaw.Name = "labelYaw";
            this.labelYaw.Size = new System.Drawing.Size(128, 20);
            this.labelYaw.TabIndex = 0;
            this.labelYaw.Text = "0.0";
            this.labelYaw.Visible = false;
            // 
            // pictureBoxMinimize
            // 
            this.pictureBoxMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxMinimize.Image = global::CableGuardian.Properties.Resources.Minimize;
            this.pictureBoxMinimize.Location = new System.Drawing.Point(969, 10);
            this.pictureBoxMinimize.Name = "pictureBoxMinimize";
            this.pictureBoxMinimize.Size = new System.Drawing.Size(24, 22);
            this.pictureBoxMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxMinimize.TabIndex = 32;
            this.pictureBoxMinimize.TabStop = false;
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxClose.Image = global::CableGuardian.Properties.Resources.Close;
            this.pictureBoxClose.Location = new System.Drawing.Point(999, 10);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(24, 22);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxClose.TabIndex = 31;
            this.pictureBoxClose.TabStop = false;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::CableGuardian.Properties.Resources.CGLogo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(9, 10);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(300, 194);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxLogo.TabIndex = 30;
            this.pictureBoxLogo.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // checkBoxStartMinimized
            // 
            this.checkBoxStartMinimized.AutoSize = true;
            this.checkBoxStartMinimized.ForeColor = System.Drawing.Color.White;
            this.checkBoxStartMinimized.Location = new System.Drawing.Point(176, 207);
            this.checkBoxStartMinimized.Name = "checkBoxStartMinimized";
            this.checkBoxStartMinimized.Size = new System.Drawing.Size(128, 17);
            this.checkBoxStartMinimized.TabIndex = 4;
            this.checkBoxStartMinimized.Text = "Start minimized to tray";
            this.checkBoxStartMinimized.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowYaw
            // 
            this.checkBoxShowYaw.AutoSize = true;
            this.checkBoxShowYaw.ForeColor = System.Drawing.Color.White;
            this.checkBoxShowYaw.Location = new System.Drawing.Point(9, 230);
            this.checkBoxShowYaw.Name = "checkBoxShowYaw";
            this.checkBoxShowYaw.Size = new System.Drawing.Size(134, 17);
            this.checkBoxShowYaw.TabIndex = 8;
            this.checkBoxShowYaw.Text = "Show live rotation data";
            this.checkBoxShowYaw.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatus.ForeColor = System.Drawing.Color.White;
            this.labelStatus.Location = new System.Drawing.Point(9, 388);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(310, 95);
            this.labelStatus.TabIndex = 0;
            // 
            // checkBoxWindowsStart
            // 
            this.checkBoxWindowsStart.AutoSize = true;
            this.checkBoxWindowsStart.ForeColor = System.Drawing.Color.White;
            this.checkBoxWindowsStart.Location = new System.Drawing.Point(9, 207);
            this.checkBoxWindowsStart.Name = "checkBoxWindowsStart";
            this.checkBoxWindowsStart.Size = new System.Drawing.Size(117, 17);
            this.checkBoxWindowsStart.TabIndex = 0;
            this.checkBoxWindowsStart.Text = "Start with Windows";
            this.checkBoxWindowsStart.UseVisualStyleBackColor = true;
            // 
            // labelDataWarning
            // 
            this.labelDataWarning.ForeColor = System.Drawing.Color.White;
            this.labelDataWarning.Location = new System.Drawing.Point(6, 317);
            this.labelDataWarning.Name = "labelDataWarning";
            this.labelDataWarning.Size = new System.Drawing.Size(313, 44);
            this.labelDataWarning.TabIndex = 36;
            this.labelDataWarning.Text = "It\'s recommended to hide the live data to save every last bit of that precious CP" +
    "U time.";
            this.labelDataWarning.Visible = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(783, 10);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(150, 23);
            this.buttonSave.TabIndex = 44;
            this.buttonSave.Text = "Save profiles to disk";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // pictureBoxPlus
            // 
            this.pictureBoxPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPlus.Image = global::CableGuardian.Properties.Resources.PlusSmall;
            this.pictureBoxPlus.Location = new System.Drawing.Point(571, 14);
            this.pictureBoxPlus.Name = "pictureBoxPlus";
            this.pictureBoxPlus.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxPlus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPlus.TabIndex = 46;
            this.pictureBoxPlus.TabStop = false;
            // 
            // labelProf
            // 
            this.labelProf.AutoSize = true;
            this.labelProf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProf.ForeColor = System.Drawing.Color.White;
            this.labelProf.Location = new System.Drawing.Point(324, 9);
            this.labelProf.Name = "labelProf";
            this.labelProf.Size = new System.Drawing.Size(61, 20);
            this.labelProf.TabIndex = 45;
            this.labelProf.Text = "Profile: ";
            // 
            // comboBoxProfile
            // 
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Location = new System.Drawing.Point(391, 11);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(176, 21);
            this.comboBoxProfile.TabIndex = 40;
            // 
            // pictureBoxMinus
            // 
            this.pictureBoxMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxMinus.Image = global::CableGuardian.Properties.Resources.MinusSmall;
            this.pictureBoxMinus.Location = new System.Drawing.Point(593, 14);
            this.pictureBoxMinus.Name = "pictureBoxMinus";
            this.pictureBoxMinus.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxMinus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxMinus.TabIndex = 47;
            this.pictureBoxMinus.TabStop = false;
            // 
            // pictureBoxHelp
            // 
            this.pictureBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxHelp.Image = global::CableGuardian.Properties.Resources.Help;
            this.pictureBoxHelp.Location = new System.Drawing.Point(939, 10);
            this.pictureBoxHelp.Name = "pictureBoxHelp";
            this.pictureBoxHelp.Size = new System.Drawing.Size(24, 22);
            this.pictureBoxHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxHelp.TabIndex = 51;
            this.pictureBoxHelp.TabStop = false;
            // 
            // comboBoxAPI
            // 
            this.comboBoxAPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAPI.FormattingEnabled = true;
            this.comboBoxAPI.Location = new System.Drawing.Point(224, 364);
            this.comboBoxAPI.Name = "comboBoxAPI";
            this.comboBoxAPI.Size = new System.Drawing.Size(95, 21);
            this.comboBoxAPI.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(173, 367);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "VR API:";
            // 
            // buttonRetry
            // 
            this.buttonRetry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRetry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRetry.ForeColor = System.Drawing.Color.White;
            this.buttonRetry.Location = new System.Drawing.Point(9, 362);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(155, 23);
            this.buttonRetry.TabIndex = 28;
            this.buttonRetry.Text = "Retry VR connection";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Visible = false;
            // 
            // labelMount
            // 
            this.labelMount.AutoSize = true;
            this.labelMount.ForeColor = System.Drawing.Color.White;
            this.labelMount.Location = new System.Drawing.Point(6, 345);
            this.labelMount.Name = "labelMount";
            this.labelMount.Size = new System.Drawing.Size(303, 13);
            this.labelMount.TabIndex = 56;
            this.labelMount.Tag = "MANUAL";
            this.labelMount.Text = "With OpenVR, the HMD must be mounted for tracking to work.";
            this.labelMount.Visible = false;
            // 
            // checkBoxHome
            // 
            this.checkBoxHome.AutoSize = true;
            this.checkBoxHome.ForeColor = System.Drawing.Color.White;
            this.checkBoxHome.Location = new System.Drawing.Point(9, 253);
            this.checkBoxHome.Name = "checkBoxHome";
            this.checkBoxHome.Size = new System.Drawing.Size(130, 17);
            this.checkBoxHome.TabIndex = 16;
            this.checkBoxHome.Text = "Require Oculus Home";
            this.checkBoxHome.UseVisualStyleBackColor = true;
            this.checkBoxHome.Visible = false;
            // 
            // checkBoxConnLost
            // 
            this.checkBoxConnLost.AutoSize = true;
            this.checkBoxConnLost.ForeColor = System.Drawing.Color.White;
            this.checkBoxConnLost.Location = new System.Drawing.Point(13, 463);
            this.checkBoxConnLost.Name = "checkBoxConnLost";
            this.checkBoxConnLost.Size = new System.Drawing.Size(239, 17);
            this.checkBoxConnLost.TabIndex = 36;
            this.checkBoxConnLost.Text = "Show notification when VR connection is lost";
            this.checkBoxConnLost.UseVisualStyleBackColor = true;
            // 
            // buttonAlarm
            // 
            this.buttonAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAlarm.ForeColor = System.Drawing.Color.White;
            this.buttonAlarm.Location = new System.Drawing.Point(176, 272);
            this.buttonAlarm.Name = "buttonAlarm";
            this.buttonAlarm.Size = new System.Drawing.Size(143, 23);
            this.buttonAlarm.TabIndex = 24;
            this.buttonAlarm.Text = "Alarm clock settings...";
            this.buttonAlarm.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrayNotifications
            // 
            this.checkBoxTrayNotifications.AutoSize = true;
            this.checkBoxTrayNotifications.ForeColor = System.Drawing.Color.White;
            this.checkBoxTrayNotifications.Location = new System.Drawing.Point(176, 230);
            this.checkBoxTrayNotifications.Name = "checkBoxTrayNotifications";
            this.checkBoxTrayNotifications.Size = new System.Drawing.Size(135, 17);
            this.checkBoxTrayNotifications.TabIndex = 12;
            this.checkBoxTrayNotifications.Text = "Tray menu notifications";
            this.checkBoxTrayNotifications.UseVisualStyleBackColor = true;
            // 
            // profileEditor
            // 
            this.profileEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.profileEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.profileEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profileEditor.Location = new System.Drawing.Point(328, 38);
            this.profileEditor.Name = "profileEditor";
            this.profileEditor.Size = new System.Drawing.Size(695, 445);
            this.profileEditor.TabIndex = 48;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(1032, 492);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxTrayNotifications);
            this.Controls.Add(this.buttonAlarm);
            this.Controls.Add(this.checkBoxConnLost);
            this.Controls.Add(this.checkBoxHome);
            this.Controls.Add(this.labelMount);
            this.Controls.Add(this.buttonRetry);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxAPI);
            this.Controls.Add(this.pictureBoxHelp);
            this.Controls.Add(this.profileEditor);
            this.Controls.Add(this.pictureBoxMinus);
            this.Controls.Add(this.pictureBoxPlus);
            this.Controls.Add(this.labelProf);
            this.Controls.Add(this.comboBoxProfile);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.checkBoxWindowsStart);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.checkBoxShowYaw);
            this.Controls.Add(this.checkBoxStartMinimized);
            this.Controls.Add(this.pictureBoxMinimize);
            this.Controls.Add(this.pictureBoxClose);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelFullRot);
            this.Controls.Add(this.labelFullRotTitle);
            this.Controls.Add(this.labelYaw);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.labelDataWarning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Cable Guardian";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelFullRotTitle;
        private System.Windows.Forms.Label labelFullRot;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelYaw;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.PictureBox pictureBoxMinimize;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox checkBoxStartMinimized;
        private System.Windows.Forms.CheckBox checkBoxShowYaw;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxWindowsStart;
        private System.Windows.Forms.Label labelDataWarning;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.PictureBox pictureBoxPlus;
        private System.Windows.Forms.Label labelProf;
        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.PictureBox pictureBoxMinus;
        private ProfileEditor profileEditor;
        private System.Windows.Forms.PictureBox pictureBoxHelp;
        private System.Windows.Forms.ComboBox comboBoxAPI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Label labelMount;
        private System.Windows.Forms.CheckBox checkBoxHome;
        private System.Windows.Forms.CheckBox checkBoxConnLost;
        private System.Windows.Forms.Button buttonAlarm;
        private System.Windows.Forms.CheckBox checkBoxTrayNotifications;
    }
}

