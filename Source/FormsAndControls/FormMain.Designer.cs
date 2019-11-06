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
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelYaw = new System.Windows.Forms.Label();
            this.pictureBoxMinimize = new System.Windows.Forms.PictureBox();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBoxStartMinUser = new System.Windows.Forms.CheckBox();
            this.checkBoxShowYaw = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxWindowsStart = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.pictureBoxPlus = new System.Windows.Forms.PictureBox();
            this.labelProf = new System.Windows.Forms.Label();
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.pictureBoxMinus = new System.Windows.Forms.PictureBox();
            this.pictureBoxHelp = new System.Windows.Forms.PictureBox();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.checkBoxConnLost = new System.Windows.Forms.CheckBox();
            this.buttonAlarm = new System.Windows.Forms.Button();
            this.checkBoxTrayNotifications = new System.Windows.Forms.CheckBox();
            this.pictureBoxClone = new System.Windows.Forms.PictureBox();
            this.labelHalfTurnTitle = new System.Windows.Forms.Label();
            this.labelHalfTurns = new System.Windows.Forms.Label();
            this.checkBoxPlaySoundOnHMDInteraction = new System.Windows.Forms.CheckBox();
            this.buttonJingle = new System.Windows.Forms.Button();
            this.checkBoxOnAPIQuit = new System.Windows.Forms.CheckBox();
            this.checkBoxStartMinAuto = new System.Windows.Forms.CheckBox();
            this.checkBoxSticky = new System.Windows.Forms.CheckBox();
            this.labelAutoStart = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxSteamVRStartUp = new System.Windows.Forms.PictureBox();
            this.checkBoxSteamVRStart = new System.Windows.Forms.CheckBox();
            this.checkBoxRememberRotation = new System.Windows.Forms.CheckBox();
            this.numericUpDownRotMemory = new System.Windows.Forms.NumericUpDown();
            this.labelRotMemMinutes = new System.Windows.Forms.Label();
            this.profileEditor = new CableGuardian.ProfileEditor();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClone)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSteamVRStartUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRotMemory)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReset.ForeColor = System.Drawing.Color.White;
            this.buttonReset.Location = new System.Drawing.Point(177, 260);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(142, 23);
            this.buttonReset.TabIndex = 20;
            this.buttonReset.Text = "Reset turn counter";
            this.buttonReset.UseVisualStyleBackColor = true;
            // 
            // labelYaw
            // 
            this.labelYaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYaw.ForeColor = System.Drawing.Color.White;
            this.labelYaw.Location = new System.Drawing.Point(6, 366);
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
            // checkBoxStartMinUser
            // 
            this.checkBoxStartMinUser.ForeColor = System.Drawing.Color.White;
            this.checkBoxStartMinUser.Location = new System.Drawing.Point(9, 279);
            this.checkBoxStartMinUser.Name = "checkBoxStartMinUser";
            this.checkBoxStartMinUser.Size = new System.Drawing.Size(162, 17);
            this.checkBoxStartMinUser.TabIndex = 4;
            this.checkBoxStartMinUser.Text = "Minimize on User startup";
            this.checkBoxStartMinUser.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowYaw
            // 
            this.checkBoxShowYaw.AutoSize = true;
            this.checkBoxShowYaw.ForeColor = System.Drawing.Color.White;
            this.checkBoxShowYaw.Location = new System.Drawing.Point(9, 345);
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
            this.checkBoxWindowsStart.ForeColor = System.Drawing.Color.White;
            this.checkBoxWindowsStart.Location = new System.Drawing.Point(117, 1);
            this.checkBoxWindowsStart.Name = "checkBoxWindowsStart";
            this.checkBoxWindowsStart.Size = new System.Drawing.Size(87, 17);
            this.checkBoxWindowsStart.TabIndex = 0;
            this.checkBoxWindowsStart.Text = "Windows";
            this.checkBoxWindowsStart.UseVisualStyleBackColor = true;
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
            this.pictureBoxPlus.Location = new System.Drawing.Point(522, 14);
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
            this.comboBoxProfile.Location = new System.Drawing.Point(389, 11);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(125, 21);
            this.comboBoxProfile.TabIndex = 40;
            // 
            // pictureBoxMinus
            // 
            this.pictureBoxMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxMinus.Image = global::CableGuardian.Properties.Resources.MinusSmall;
            this.pictureBoxMinus.Location = new System.Drawing.Point(566, 14);
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
            // buttonRetry
            // 
            this.buttonRetry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRetry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRetry.ForeColor = System.Drawing.Color.White;
            this.buttonRetry.Location = new System.Drawing.Point(177, 388);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(142, 23);
            this.buttonRetry.TabIndex = 28;
            this.buttonRetry.Text = "Retry VR connection";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Visible = false;
            // 
            // checkBoxConnLost
            // 
            this.checkBoxConnLost.AutoSize = true;
            this.checkBoxConnLost.ForeColor = System.Drawing.Color.White;
            this.checkBoxConnLost.Location = new System.Drawing.Point(13, 463);
            this.checkBoxConnLost.Name = "checkBoxConnLost";
            this.checkBoxConnLost.Size = new System.Drawing.Size(153, 17);
            this.checkBoxConnLost.TabIndex = 36;
            this.checkBoxConnLost.Text = "Connection lost notification";
            this.checkBoxConnLost.UseVisualStyleBackColor = true;
            // 
            // buttonAlarm
            // 
            this.buttonAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAlarm.ForeColor = System.Drawing.Color.White;
            this.buttonAlarm.Location = new System.Drawing.Point(177, 289);
            this.buttonAlarm.Name = "buttonAlarm";
            this.buttonAlarm.Size = new System.Drawing.Size(142, 23);
            this.buttonAlarm.TabIndex = 24;
            this.buttonAlarm.Text = "Alarm clock sound...";
            this.buttonAlarm.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrayNotifications
            // 
            this.checkBoxTrayNotifications.AutoSize = true;
            this.checkBoxTrayNotifications.ForeColor = System.Drawing.Color.White;
            this.checkBoxTrayNotifications.Location = new System.Drawing.Point(9, 301);
            this.checkBoxTrayNotifications.Name = "checkBoxTrayNotifications";
            this.checkBoxTrayNotifications.Size = new System.Drawing.Size(135, 17);
            this.checkBoxTrayNotifications.TabIndex = 12;
            this.checkBoxTrayNotifications.Text = "Tray menu notifications";
            this.checkBoxTrayNotifications.UseVisualStyleBackColor = true;
            // 
            // pictureBoxClone
            // 
            this.pictureBoxClone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxClone.Image = global::CableGuardian.Properties.Resources.CloneSmall;
            this.pictureBoxClone.Location = new System.Drawing.Point(544, 14);
            this.pictureBoxClone.Name = "pictureBoxClone";
            this.pictureBoxClone.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxClone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxClone.TabIndex = 58;
            this.pictureBoxClone.TabStop = false;
            // 
            // labelHalfTurnTitle
            // 
            this.labelHalfTurnTitle.AutoSize = true;
            this.labelHalfTurnTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHalfTurnTitle.ForeColor = System.Drawing.Color.White;
            this.labelHalfTurnTitle.Location = new System.Drawing.Point(143, 366);
            this.labelHalfTurnTitle.Name = "labelHalfTurnTitle";
            this.labelHalfTurnTitle.Size = new System.Drawing.Size(70, 16);
            this.labelHalfTurnTitle.TabIndex = 59;
            this.labelHalfTurnTitle.Text = "Half-turns: ";
            this.labelHalfTurnTitle.Visible = false;
            // 
            // labelHalfTurns
            // 
            this.labelHalfTurns.AutoSize = true;
            this.labelHalfTurns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHalfTurns.ForeColor = System.Drawing.Color.White;
            this.labelHalfTurns.Location = new System.Drawing.Point(237, 366);
            this.labelHalfTurns.Name = "labelHalfTurns";
            this.labelHalfTurns.Size = new System.Drawing.Size(15, 16);
            this.labelHalfTurns.TabIndex = 60;
            this.labelHalfTurns.Text = "0";
            this.labelHalfTurns.Visible = false;
            // 
            // checkBoxPlaySoundOnHMDInteraction
            // 
            this.checkBoxPlaySoundOnHMDInteraction.AutoSize = true;
            this.checkBoxPlaySoundOnHMDInteraction.ForeColor = System.Drawing.Color.White;
            this.checkBoxPlaySoundOnHMDInteraction.Location = new System.Drawing.Point(9, 323);
            this.checkBoxPlaySoundOnHMDInteraction.Name = "checkBoxPlaySoundOnHMDInteraction";
            this.checkBoxPlaySoundOnHMDInteraction.Size = new System.Drawing.Size(116, 17);
            this.checkBoxPlaySoundOnHMDInteraction.TabIndex = 61;
            this.checkBoxPlaySoundOnHMDInteraction.Text = "Confirmation sound";
            this.checkBoxPlaySoundOnHMDInteraction.UseVisualStyleBackColor = true;
            // 
            // buttonJingle
            // 
            this.buttonJingle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonJingle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJingle.ForeColor = System.Drawing.Color.White;
            this.buttonJingle.Location = new System.Drawing.Point(177, 318);
            this.buttonJingle.Name = "buttonJingle";
            this.buttonJingle.Size = new System.Drawing.Size(142, 23);
            this.buttonJingle.TabIndex = 62;
            this.buttonJingle.Text = "Confirmation sound...";
            this.buttonJingle.UseVisualStyleBackColor = true;
            // 
            // checkBoxOnAPIQuit
            // 
            this.checkBoxOnAPIQuit.AutoSize = true;
            this.checkBoxOnAPIQuit.ForeColor = System.Drawing.Color.White;
            this.checkBoxOnAPIQuit.Location = new System.Drawing.Point(167, 463);
            this.checkBoxOnAPIQuit.Name = "checkBoxOnAPIQuit";
            this.checkBoxOnAPIQuit.Size = new System.Drawing.Size(87, 17);
            this.checkBoxOnAPIQuit.TabIndex = 63;
            this.checkBoxOnAPIQuit.Text = "...on API quit";
            this.checkBoxOnAPIQuit.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartMinAuto
            // 
            this.checkBoxStartMinAuto.ForeColor = System.Drawing.Color.White;
            this.checkBoxStartMinAuto.Location = new System.Drawing.Point(9, 257);
            this.checkBoxStartMinAuto.Name = "checkBoxStartMinAuto";
            this.checkBoxStartMinAuto.Size = new System.Drawing.Size(173, 17);
            this.checkBoxStartMinAuto.TabIndex = 64;
            this.checkBoxStartMinAuto.Text = "Minimize on auto-startup";
            this.checkBoxStartMinAuto.UseVisualStyleBackColor = true;
            // 
            // checkBoxSticky
            // 
            this.checkBoxSticky.AutoSize = true;
            this.checkBoxSticky.ForeColor = System.Drawing.Color.White;
            this.checkBoxSticky.Location = new System.Drawing.Point(254, 463);
            this.checkBoxSticky.Name = "checkBoxSticky";
            this.checkBoxSticky.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSticky.TabIndex = 65;
            this.checkBoxSticky.Text = "...sticky";
            this.checkBoxSticky.UseVisualStyleBackColor = true;
            // 
            // labelAutoStart
            // 
            this.labelAutoStart.AutoSize = true;
            this.labelAutoStart.ForeColor = System.Drawing.Color.White;
            this.labelAutoStart.Location = new System.Drawing.Point(3, 2);
            this.labelAutoStart.Name = "labelAutoStart";
            this.labelAutoStart.Size = new System.Drawing.Size(83, 13);
            this.labelAutoStart.TabIndex = 69;
            this.labelAutoStart.Text = "Auto start && exit:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBoxSteamVRStartUp);
            this.panel1.Controls.Add(this.checkBoxSteamVRStart);
            this.panel1.Controls.Add(this.labelAutoStart);
            this.panel1.Controls.Add(this.checkBoxWindowsStart);
            this.panel1.Location = new System.Drawing.Point(9, 206);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 22);
            this.panel1.TabIndex = 70;
            // 
            // pictureBoxSteamVRStartUp
            // 
            this.pictureBoxSteamVRStartUp.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxSteamVRStartUp.Image = global::CableGuardian.Properties.Resources.Attention;
            this.pictureBoxSteamVRStartUp.Location = new System.Drawing.Point(286, 1);
            this.pictureBoxSteamVRStartUp.Name = "pictureBoxSteamVRStartUp";
            this.pictureBoxSteamVRStartUp.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSteamVRStartUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSteamVRStartUp.TabIndex = 71;
            this.pictureBoxSteamVRStartUp.TabStop = false;
            this.pictureBoxSteamVRStartUp.Visible = false;
            // 
            // checkBoxSteamVRStart
            // 
            this.checkBoxSteamVRStart.ForeColor = System.Drawing.Color.White;
            this.checkBoxSteamVRStart.Location = new System.Drawing.Point(210, 1);
            this.checkBoxSteamVRStart.Name = "checkBoxSteamVRStart";
            this.checkBoxSteamVRStart.Size = new System.Drawing.Size(72, 17);
            this.checkBoxSteamVRStart.TabIndex = 70;
            this.checkBoxSteamVRStart.Text = "SteamVR";
            this.checkBoxSteamVRStart.UseVisualStyleBackColor = true;
            this.checkBoxSteamVRStart.Visible = false;
            // 
            // checkBoxRememberRotation
            // 
            this.checkBoxRememberRotation.ForeColor = System.Drawing.Color.White;
            this.checkBoxRememberRotation.Location = new System.Drawing.Point(9, 235);
            this.checkBoxRememberRotation.Name = "checkBoxRememberRotation";
            this.checkBoxRememberRotation.Size = new System.Drawing.Size(173, 17);
            this.checkBoxRememberRotation.TabIndex = 71;
            this.checkBoxRememberRotation.Text = "Remember turn count for -->";
            this.checkBoxRememberRotation.UseVisualStyleBackColor = true;
            // 
            // numericUpDownRotMemory
            // 
            this.numericUpDownRotMemory.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownRotMemory.Location = new System.Drawing.Point(177, 234);
            this.numericUpDownRotMemory.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRotMemory.Name = "numericUpDownRotMemory";
            this.numericUpDownRotMemory.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownRotMemory.TabIndex = 72;
            this.numericUpDownRotMemory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRotMemMinutes
            // 
            this.labelRotMemMinutes.AutoSize = true;
            this.labelRotMemMinutes.ForeColor = System.Drawing.Color.White;
            this.labelRotMemMinutes.Location = new System.Drawing.Point(266, 236);
            this.labelRotMemMinutes.Name = "labelRotMemMinutes";
            this.labelRotMemMinutes.Size = new System.Drawing.Size(43, 13);
            this.labelRotMemMinutes.TabIndex = 73;
            this.labelRotMemMinutes.Text = "minutes";
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
            this.Controls.Add(this.labelRotMemMinutes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxSticky);
            this.Controls.Add(this.checkBoxOnAPIQuit);
            this.Controls.Add(this.buttonJingle);
            this.Controls.Add(this.checkBoxPlaySoundOnHMDInteraction);
            this.Controls.Add(this.labelHalfTurns);
            this.Controls.Add(this.labelHalfTurnTitle);
            this.Controls.Add(this.pictureBoxClone);
            this.Controls.Add(this.checkBoxTrayNotifications);
            this.Controls.Add(this.buttonAlarm);
            this.Controls.Add(this.checkBoxConnLost);
            this.Controls.Add(this.buttonRetry);
            this.Controls.Add(this.pictureBoxHelp);
            this.Controls.Add(this.profileEditor);
            this.Controls.Add(this.pictureBoxMinus);
            this.Controls.Add(this.pictureBoxPlus);
            this.Controls.Add(this.labelProf);
            this.Controls.Add(this.comboBoxProfile);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.checkBoxShowYaw);
            this.Controls.Add(this.pictureBoxMinimize);
            this.Controls.Add(this.pictureBoxClose);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelYaw);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.checkBoxStartMinAuto);
            this.Controls.Add(this.checkBoxStartMinUser);
            this.Controls.Add(this.numericUpDownRotMemory);
            this.Controls.Add(this.checkBoxRememberRotation);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClone)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSteamVRStartUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRotMemory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelYaw;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.PictureBox pictureBoxMinimize;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox checkBoxStartMinUser;
        private System.Windows.Forms.CheckBox checkBoxShowYaw;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxWindowsStart;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.PictureBox pictureBoxPlus;
        private System.Windows.Forms.Label labelProf;
        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.PictureBox pictureBoxMinus;
        private ProfileEditor profileEditor;
        private System.Windows.Forms.PictureBox pictureBoxHelp;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.CheckBox checkBoxConnLost;
        private System.Windows.Forms.Button buttonAlarm;
        private System.Windows.Forms.CheckBox checkBoxTrayNotifications;
        private System.Windows.Forms.PictureBox pictureBoxClone;
        private System.Windows.Forms.Label labelHalfTurnTitle;
        private System.Windows.Forms.Label labelHalfTurns;
        private System.Windows.Forms.CheckBox checkBoxPlaySoundOnHMDInteraction;
        private System.Windows.Forms.Button buttonJingle;
        private System.Windows.Forms.CheckBox checkBoxOnAPIQuit;
        private System.Windows.Forms.CheckBox checkBoxStartMinAuto;
        private System.Windows.Forms.CheckBox checkBoxSticky;
        private System.Windows.Forms.Label labelAutoStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxSteamVRStart;
        private System.Windows.Forms.PictureBox pictureBoxSteamVRStartUp;
        private System.Windows.Forms.CheckBox checkBoxRememberRotation;
        private System.Windows.Forms.NumericUpDown numericUpDownRotMemory;
        private System.Windows.Forms.Label labelRotMemMinutes;
    }
}

