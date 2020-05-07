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
            this.checkBoxTrayNotifications = new System.Windows.Forms.CheckBox();
            this.pictureBoxClone = new System.Windows.Forms.PictureBox();
            this.labelHalfTurnTitle = new System.Windows.Forms.Label();
            this.labelHalfTurns = new System.Windows.Forms.Label();
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
            this.pictureBoxGetPro = new System.Windows.Forms.PictureBox();
            this.labelAlarmAt = new System.Windows.Forms.Label();
            this.pictureBoxAlarmClock = new System.Windows.Forms.PictureBox();
            this.pictureBoxDefaults = new System.Windows.Forms.PictureBox();
            this.panelSimple = new System.Windows.Forms.Panel();
            this.checkBoxMountingSound = new System.Windows.Forms.CheckBox();
            this.pictureBoxPlay = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxNotifType = new System.Windows.Forms.ComboBox();
            this.checkBoxResetOnMount = new System.Windows.Forms.CheckBox();
            this.labelVolVal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownHalfTurns = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMore = new System.Windows.Forms.Label();
            this.comboBoxAPI = new System.Windows.Forms.ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGetPro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlarmClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefaults)).BeginInit();
            this.panelSimple.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHalfTurns)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReset.ForeColor = System.Drawing.Color.White;
            this.buttonReset.Location = new System.Drawing.Point(177, 363);
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
            this.labelYaw.Location = new System.Drawing.Point(179, 344);
            this.labelYaw.Name = "labelYaw";
            this.labelYaw.Size = new System.Drawing.Size(128, 20);
            this.labelYaw.TabIndex = 0;
            this.labelYaw.Text = "0.0";
            this.labelYaw.Visible = false;
            // 
            // pictureBoxMinimize
            // 
            this.pictureBoxMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMinimize.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.pictureBoxClose.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.checkBoxStartMinUser.Location = new System.Drawing.Point(9, 257);
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
            this.checkBoxShowYaw.Visible = false;
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
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(618, 10);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(150, 23);
            this.buttonSave.TabIndex = 44;
            this.buttonSave.Text = "Save profiles to disk";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // pictureBoxPlus
            // 
            this.pictureBoxPlus.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.pictureBoxMinus.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.pictureBoxHelp.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.buttonRetry.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonRetry.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
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
            // checkBoxTrayNotifications
            // 
            this.checkBoxTrayNotifications.AutoSize = true;
            this.checkBoxTrayNotifications.ForeColor = System.Drawing.Color.White;
            this.checkBoxTrayNotifications.Location = new System.Drawing.Point(9, 279);
            this.checkBoxTrayNotifications.Name = "checkBoxTrayNotifications";
            this.checkBoxTrayNotifications.Size = new System.Drawing.Size(135, 17);
            this.checkBoxTrayNotifications.TabIndex = 12;
            this.checkBoxTrayNotifications.Text = "Tray menu notifications";
            this.checkBoxTrayNotifications.UseVisualStyleBackColor = true;
            // 
            // pictureBoxClone
            // 
            this.pictureBoxClone.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.labelHalfTurnTitle.Location = new System.Drawing.Point(7, 366);
            this.labelHalfTurnTitle.Name = "labelHalfTurnTitle";
            this.labelHalfTurnTitle.Size = new System.Drawing.Size(70, 16);
            this.labelHalfTurnTitle.TabIndex = 59;
            this.labelHalfTurnTitle.Text = "Half-turns: ";
            // 
            // labelHalfTurns
            // 
            this.labelHalfTurns.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHalfTurns.ForeColor = System.Drawing.Color.White;
            this.labelHalfTurns.Location = new System.Drawing.Point(71, 360);
            this.labelHalfTurns.Name = "labelHalfTurns";
            this.labelHalfTurns.Size = new System.Drawing.Size(104, 27);
            this.labelHalfTurns.TabIndex = 60;
            this.labelHalfTurns.Text = "0";
            this.labelHalfTurns.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.checkBoxStartMinAuto.Location = new System.Drawing.Point(9, 235);
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
            this.checkBoxRememberRotation.Location = new System.Drawing.Point(9, 301);
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
            this.numericUpDownRotMemory.Location = new System.Drawing.Point(177, 300);
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
            this.labelRotMemMinutes.Location = new System.Drawing.Point(266, 302);
            this.labelRotMemMinutes.Name = "labelRotMemMinutes";
            this.labelRotMemMinutes.Size = new System.Drawing.Size(43, 13);
            this.labelRotMemMinutes.TabIndex = 73;
            this.labelRotMemMinutes.Text = "minutes";
            // 
            // pictureBoxGetPro
            // 
            this.pictureBoxGetPro.Image = global::CableGuardian.Properties.Resources.GetPro;
            this.pictureBoxGetPro.Location = new System.Drawing.Point(814, 4);
            this.pictureBoxGetPro.Name = "pictureBoxGetPro";
            this.pictureBoxGetPro.Size = new System.Drawing.Size(85, 32);
            this.pictureBoxGetPro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxGetPro.TabIndex = 74;
            this.pictureBoxGetPro.TabStop = false;
            // 
            // labelAlarmAt
            // 
            this.labelAlarmAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlarmAt.ForeColor = System.Drawing.Color.White;
            this.labelAlarmAt.Location = new System.Drawing.Point(127, 184);
            this.labelAlarmAt.Name = "labelAlarmAt";
            this.labelAlarmAt.Size = new System.Drawing.Size(125, 16);
            this.labelAlarmAt.TabIndex = 78;
            this.labelAlarmAt.Text = "AlarmAt";
            this.labelAlarmAt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelAlarmAt.Visible = false;
            // 
            // pictureBoxAlarmClock
            // 
            this.pictureBoxAlarmClock.Image = global::CableGuardian.Properties.Resources.AlarmClockBW_small;
            this.pictureBoxAlarmClock.Location = new System.Drawing.Point(253, 125);
            this.pictureBoxAlarmClock.Name = "pictureBoxAlarmClock";
            this.pictureBoxAlarmClock.Size = new System.Drawing.Size(54, 77);
            this.pictureBoxAlarmClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAlarmClock.TabIndex = 77;
            this.pictureBoxAlarmClock.TabStop = false;
            // 
            // pictureBoxDefaults
            // 
            this.pictureBoxDefaults.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxDefaults.Image = global::CableGuardian.Properties.Resources.Defaults;
            this.pictureBoxDefaults.Location = new System.Drawing.Point(588, 9);
            this.pictureBoxDefaults.Name = "pictureBoxDefaults";
            this.pictureBoxDefaults.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxDefaults.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDefaults.TabIndex = 79;
            this.pictureBoxDefaults.TabStop = false;
            // 
            // panelSimple
            // 
            this.panelSimple.Controls.Add(this.checkBoxMountingSound);
            this.panelSimple.Controls.Add(this.pictureBoxPlay);
            this.panelSimple.Controls.Add(this.label2);
            this.panelSimple.Controls.Add(this.comboBoxNotifType);
            this.panelSimple.Controls.Add(this.checkBoxResetOnMount);
            this.panelSimple.Controls.Add(this.labelVolVal);
            this.panelSimple.Controls.Add(this.label4);
            this.panelSimple.Controls.Add(this.trackBarVolume);
            this.panelSimple.Controls.Add(this.label3);
            this.panelSimple.Controls.Add(this.numericUpDownHalfTurns);
            this.panelSimple.Controls.Add(this.label1);
            this.panelSimple.Location = new System.Drawing.Point(362, 206);
            this.panelSimple.Name = "panelSimple";
            this.panelSimple.Size = new System.Drawing.Size(317, 132);
            this.panelSimple.TabIndex = 80;
            this.panelSimple.Visible = false;
            // 
            // checkBoxMountingSound
            // 
            this.checkBoxMountingSound.AutoSize = true;
            this.checkBoxMountingSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMountingSound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.checkBoxMountingSound.Location = new System.Drawing.Point(275, 109);
            this.checkBoxMountingSound.Name = "checkBoxMountingSound";
            this.checkBoxMountingSound.Size = new System.Drawing.Size(41, 28);
            this.checkBoxMountingSound.TabIndex = 88;
            this.checkBoxMountingSound.Text = "S";
            this.checkBoxMountingSound.UseVisualStyleBackColor = true;
            // 
            // pictureBoxPlay
            // 
            this.pictureBoxPlay.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxPlay.Image = global::CableGuardian.Properties.Resources.Play;
            this.pictureBoxPlay.Location = new System.Drawing.Point(288, 43);
            this.pictureBoxPlay.Name = "pictureBoxPlay";
            this.pictureBoxPlay.Size = new System.Drawing.Size(20, 32);
            this.pictureBoxPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPlay.TabIndex = 83;
            this.pictureBoxPlay.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 82;
            this.label2.Text = "Half-turn";
            // 
            // comboBoxNotifType
            // 
            this.comboBoxNotifType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNotifType.FormattingEnabled = true;
            this.comboBoxNotifType.Location = new System.Drawing.Point(76, 48);
            this.comboBoxNotifType.Name = "comboBoxNotifType";
            this.comboBoxNotifType.Size = new System.Drawing.Size(94, 21);
            this.comboBoxNotifType.TabIndex = 81;
            // 
            // checkBoxResetOnMount
            // 
            this.checkBoxResetOnMount.AutoSize = true;
            this.checkBoxResetOnMount.ForeColor = System.Drawing.Color.White;
            this.checkBoxResetOnMount.Location = new System.Drawing.Point(170, 115);
            this.checkBoxResetOnMount.Name = "checkBoxResetOnMount";
            this.checkBoxResetOnMount.Size = new System.Drawing.Size(101, 17);
            this.checkBoxResetOnMount.TabIndex = 80;
            this.checkBoxResetOnMount.Text = "Reset on mount";
            this.checkBoxResetOnMount.UseVisualStyleBackColor = true;
            // 
            // labelVolVal
            // 
            this.labelVolVal.ForeColor = System.Drawing.Color.White;
            this.labelVolVal.Location = new System.Drawing.Point(250, 32);
            this.labelVolVal.Name = "labelVolVal";
            this.labelVolVal.Size = new System.Drawing.Size(35, 13);
            this.labelVolVal.TabIndex = 79;
            this.labelVolVal.Text = "100";
            this.labelVolVal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(181, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Volume";
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(177, 46);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(104, 45);
            this.trackBarVolume.TabIndex = 78;
            this.trackBarVolume.Value = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(74, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "Notification type";
            // 
            // numericUpDownHalfTurns
            // 
            this.numericUpDownHalfTurns.BackColor = System.Drawing.Color.Yellow;
            this.numericUpDownHalfTurns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHalfTurns.Location = new System.Drawing.Point(11, 48);
            this.numericUpDownHalfTurns.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownHalfTurns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHalfTurns.Name = "numericUpDownHalfTurns";
            this.numericUpDownHalfTurns.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownHalfTurns.TabIndex = 72;
            this.numericUpDownHalfTurns.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownHalfTurns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 70;
            this.label1.Text = "threshold";
            // 
            // labelMore
            // 
            this.labelMore.AutoSize = true;
            this.labelMore.ForeColor = System.Drawing.Color.White;
            this.labelMore.Location = new System.Drawing.Point(210, 445);
            this.labelMore.Name = "labelMore";
            this.labelMore.Size = new System.Drawing.Size(105, 13);
            this.labelMore.TabIndex = 81;
            this.labelMore.Text = "Need more choices?";
            this.labelMore.Visible = false;
            // 
            // comboBoxAPI
            // 
            this.comboBoxAPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAPI.FormattingEnabled = true;
            this.comboBoxAPI.Location = new System.Drawing.Point(12, 438);
            this.comboBoxAPI.Name = "comboBoxAPI";
            this.comboBoxAPI.Size = new System.Drawing.Size(81, 21);
            this.comboBoxAPI.TabIndex = 82;
            this.comboBoxAPI.Visible = false;
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
            this.Controls.Add(this.comboBoxAPI);
            this.Controls.Add(this.labelMore);
            this.Controls.Add(this.panelSimple);
            this.Controls.Add(this.pictureBoxDefaults);
            this.Controls.Add(this.labelAlarmAt);
            this.Controls.Add(this.pictureBoxAlarmClock);
            this.Controls.Add(this.pictureBoxGetPro);
            this.Controls.Add(this.labelRotMemMinutes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxSticky);
            this.Controls.Add(this.checkBoxOnAPIQuit);
            this.Controls.Add(this.labelHalfTurns);
            this.Controls.Add(this.labelHalfTurnTitle);
            this.Controls.Add(this.pictureBoxClone);
            this.Controls.Add(this.checkBoxTrayNotifications);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGetPro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlarmClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefaults)).EndInit();
            this.panelSimple.ResumeLayout(false);
            this.panelSimple.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHalfTurns)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxTrayNotifications;
        private System.Windows.Forms.PictureBox pictureBoxClone;
        private System.Windows.Forms.Label labelHalfTurnTitle;
        private System.Windows.Forms.Label labelHalfTurns;
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
        private System.Windows.Forms.PictureBox pictureBoxGetPro;
        private System.Windows.Forms.Label labelAlarmAt;
        private System.Windows.Forms.PictureBox pictureBoxAlarmClock;
        private System.Windows.Forms.PictureBox pictureBoxDefaults;
        private System.Windows.Forms.Panel panelSimple;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownHalfTurns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelVolVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.CheckBox checkBoxResetOnMount;
        private System.Windows.Forms.ComboBox comboBoxNotifType;
        private System.Windows.Forms.Label labelMore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxPlay;
        private System.Windows.Forms.ComboBox comboBoxAPI;
        private System.Windows.Forms.CheckBox checkBoxMountingSound;
    }
}

