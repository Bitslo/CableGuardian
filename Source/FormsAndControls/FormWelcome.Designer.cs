namespace CableGuardian
{
    partial class FormWelcome
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
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelTips = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxBlue = new System.Windows.Forms.PictureBox();
            this.labelFullmodeSub = new System.Windows.Forms.Label();
            this.labelFullMode = new System.Windows.Forms.Label();
            this.labelLaunchOptions = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelNoDashboard = new System.Windows.Forms.Label();
            this.labelTurnLimit = new System.Windows.Forms.Label();
            this.labelTooltip = new System.Windows.Forms.Label();
            this.labelUi = new System.Windows.Forms.Label();
            this.labelAPI = new System.Windows.Forms.Label();
            this.checkBoxGotIt = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::CableGuardian.Properties.Resources.SmallCapsule;
            this.pictureBoxLogo.Location = new System.Drawing.Point(271, 12);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(231, 87);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxLogo.TabIndex = 31;
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("MV Boli", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.ForeColor = System.Drawing.Color.White;
            this.labelWelcome.Location = new System.Drawing.Point(40, 30);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(227, 49);
            this.labelWelcome.TabIndex = 32;
            this.labelWelcome.Tag = "";
            this.labelWelcome.Text = "Welcome to";
            // 
            // labelTips
            // 
            this.labelTips.AutoSize = true;
            this.labelTips.Font = new System.Drawing.Font("MV Boli", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTips.ForeColor = System.Drawing.Color.LightGray;
            this.labelTips.Location = new System.Drawing.Point(53, 108);
            this.labelTips.Name = "labelTips";
            this.labelTips.Size = new System.Drawing.Size(184, 25);
            this.labelTips.TabIndex = 33;
            this.labelTips.Text = "Quick Introduction:";
            // 
            // buttonClose
            // 
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(221, 451);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(113, 29);
            this.buttonClose.TabIndex = 34;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("MV Boli", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Silver;
            this.labelStatus.Location = new System.Drawing.Point(3, 87);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(409, 25);
            this.labelStatus.TabIndex = 35;
            this.labelStatus.Text = "2: Confirm headset connection status is OK";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBoxBlue);
            this.panel1.Controls.Add(this.labelFullmodeSub);
            this.panel1.Controls.Add(this.labelFullMode);
            this.panel1.Controls.Add(this.labelLaunchOptions);
            this.panel1.Controls.Add(this.labelMin);
            this.panel1.Controls.Add(this.labelNoDashboard);
            this.panel1.Controls.Add(this.labelTurnLimit);
            this.panel1.Controls.Add(this.labelTooltip);
            this.panel1.Controls.Add(this.labelUi);
            this.panel1.Controls.Add(this.labelAPI);
            this.panel1.Controls.Add(this.labelStatus);
            this.panel1.Location = new System.Drawing.Point(49, 136);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 302);
            this.panel1.TabIndex = 36;
            // 
            // pictureBoxBlue
            // 
            this.pictureBoxBlue.Image = global::CableGuardian.Properties.Resources.CG_icon_blue;
            this.pictureBoxBlue.Location = new System.Drawing.Point(383, 210);
            this.pictureBoxBlue.Name = "pictureBoxBlue";
            this.pictureBoxBlue.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxBlue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxBlue.TabIndex = 47;
            this.pictureBoxBlue.TabStop = false;
            // 
            // labelFullmodeSub
            // 
            this.labelFullmodeSub.AutoSize = true;
            this.labelFullmodeSub.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFullmodeSub.ForeColor = System.Drawing.Color.Silver;
            this.labelFullmodeSub.Location = new System.Drawing.Point(43, 270);
            this.labelFullmodeSub.Name = "labelFullmodeSub";
            this.labelFullmodeSub.Size = new System.Drawing.Size(340, 20);
            this.labelFullmodeSub.TabIndex = 46;
            this.labelFullmodeSub.Text = "• Stay away if you want to keep things simple";
            // 
            // labelFullMode
            // 
            this.labelFullMode.AutoSize = true;
            this.labelFullMode.Font = new System.Drawing.Font("MV Boli", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFullMode.ForeColor = System.Drawing.Color.Silver;
            this.labelFullMode.Location = new System.Drawing.Point(3, 245);
            this.labelFullMode.Name = "labelFullMode";
            this.labelFullMode.Size = new System.Drawing.Size(448, 25);
            this.labelFullMode.TabIndex = 45;
            this.labelFullMode.Text = "5: Check out the advanced mode for full control";
            // 
            // labelLaunchOptions
            // 
            this.labelLaunchOptions.AutoSize = true;
            this.labelLaunchOptions.Font = new System.Drawing.Font("MV Boli", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLaunchOptions.ForeColor = System.Drawing.Color.Silver;
            this.labelLaunchOptions.Location = new System.Drawing.Point(3, 185);
            this.labelLaunchOptions.Name = "labelLaunchOptions";
            this.labelLaunchOptions.Size = new System.Drawing.Size(377, 25);
            this.labelLaunchOptions.TabIndex = 44;
            this.labelLaunchOptions.Text = "4: Set the launch options to your liking";
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMin.ForeColor = System.Drawing.Color.Silver;
            this.labelMin.Location = new System.Drawing.Point(43, 210);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(332, 20);
            this.labelMin.TabIndex = 43;
            this.labelMin.Text = "• UI can be minimized to tray - see the icon:";
            // 
            // labelNoDashboard
            // 
            this.labelNoDashboard.AutoSize = true;
            this.labelNoDashboard.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoDashboard.ForeColor = System.Drawing.Color.Silver;
            this.labelNoDashboard.Location = new System.Drawing.Point(43, 55);
            this.labelNoDashboard.Name = "labelNoDashboard";
            this.labelNoDashboard.Size = new System.Drawing.Size(201, 20);
            this.labelNoDashboard.TabIndex = 42;
            this.labelNoDashboard.Text = "• There is no VR dashboard";
            // 
            // labelTurnLimit
            // 
            this.labelTurnLimit.AutoSize = true;
            this.labelTurnLimit.Font = new System.Drawing.Font("MV Boli", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTurnLimit.ForeColor = System.Drawing.Color.Silver;
            this.labelTurnLimit.Location = new System.Drawing.Point(3, 145);
            this.labelTurnLimit.Name = "labelTurnLimit";
            this.labelTurnLimit.Size = new System.Drawing.Size(408, 25);
            this.labelTurnLimit.TabIndex = 41;
            this.labelTurnLimit.Text = "3: Set the turn limit and notification type";
            // 
            // labelTooltip
            // 
            this.labelTooltip.AutoSize = true;
            this.labelTooltip.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTooltip.ForeColor = System.Drawing.Color.Silver;
            this.labelTooltip.Location = new System.Drawing.Point(43, 35);
            this.labelTooltip.Name = "labelTooltip";
            this.labelTooltip.Size = new System.Drawing.Size(352, 20);
            this.labelTooltip.TabIndex = 40;
            this.labelTooltip.Text = "• Hold the cursor over items for tips and details";
            // 
            // labelUi
            // 
            this.labelUi.AutoSize = true;
            this.labelUi.Font = new System.Drawing.Font("MV Boli", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUi.ForeColor = System.Drawing.Color.Silver;
            this.labelUi.Location = new System.Drawing.Point(3, 10);
            this.labelUi.Name = "labelUi";
            this.labelUi.Size = new System.Drawing.Size(353, 25);
            this.labelUi.TabIndex = 39;
            this.labelUi.Text = "1: Identify the desktop User Interface";
            // 
            // labelAPI
            // 
            this.labelAPI.AutoSize = true;
            this.labelAPI.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAPI.ForeColor = System.Drawing.Color.Silver;
            this.labelAPI.Location = new System.Drawing.Point(43, 112);
            this.labelAPI.Name = "labelAPI";
            this.labelAPI.Size = new System.Drawing.Size(364, 20);
            this.labelAPI.TabIndex = 36;
            this.labelAPI.Text = "• For Oculus HMDs, OculusVR API is recommended";
            // 
            // checkBoxGotIt
            // 
            this.checkBoxGotIt.AutoSize = true;
            this.checkBoxGotIt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxGotIt.ForeColor = System.Drawing.Color.White;
            this.checkBoxGotIt.Location = new System.Drawing.Point(240, 456);
            this.checkBoxGotIt.Name = "checkBoxGotIt";
            this.checkBoxGotIt.Size = new System.Drawing.Size(79, 20);
            this.checkBoxGotIt.TabIndex = 37;
            this.checkBoxGotIt.Text = "OK, got it";
            this.checkBoxGotIt.UseVisualStyleBackColor = true;
            // 
            // FormWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(555, 492);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxGotIt);
            this.Controls.Add(this.labelTips);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.pictureBoxLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormWelcome";
            this.ShowInTaskbar = false;
            this.Text = "Cable Guardian Help";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelTips;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxGotIt;
        private System.Windows.Forms.Label labelUi;
        private System.Windows.Forms.Label labelAPI;
        private System.Windows.Forms.Label labelTooltip;
        private System.Windows.Forms.Label labelTurnLimit;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelNoDashboard;
        private System.Windows.Forms.Label labelLaunchOptions;
        private System.Windows.Forms.Label labelFullMode;
        private System.Windows.Forms.Label labelFullmodeSub;
        private System.Windows.Forms.PictureBox pictureBoxBlue;
    }
}