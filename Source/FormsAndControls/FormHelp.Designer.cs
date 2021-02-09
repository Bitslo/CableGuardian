namespace CableGuardian
{
    partial class FormHelp
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.buttonPage = new System.Windows.Forms.Button();
            this.panelAck = new System.Windows.Forms.Panel();
            this.labelSimple = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxStandard = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonCopyInfo = new System.Windows.Forms.Button();
            this.buttonDiscussions = new System.Windows.Forms.Button();
            this.panelAck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStandard)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(896, 10);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(121, 30);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close help";
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Acknowledgements:";
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(399, 37);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(234, 14);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Cable Guardian v.1.01 by Bitslo";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(156, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(240, 13);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "https://github.com/naudio/NAudio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Audio playback with NAudio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Oculus C# wrapping:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.Blue;
            this.textBox2.Location = new System.Drawing.Point(156, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(240, 13);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "https://oculuswrap.codeplex.com";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.Blue;
            this.textBox3.Location = new System.Drawing.Point(156, 56);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(240, 13);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "https://github.com/ab4d/Ab3d.OculusWrap";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Wave files:";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.Blue;
            this.textBox4.Location = new System.Drawing.Point(156, 69);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(240, 13);
            this.textBox4.TabIndex = 9;
            this.textBox4.Text = "https://sonniss.com/gameaudiogdc18";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.Color.Blue;
            this.textBox5.Location = new System.Drawing.Point(156, 81);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(240, 13);
            this.textBox5.TabIndex = 14;
            this.textBox5.Text = "https://freesound.org/        (0-license)";
            // 
            // buttonPage
            // 
            this.buttonPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonPage.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPage.ForeColor = System.Drawing.Color.White;
            this.buttonPage.Location = new System.Drawing.Point(777, 6);
            this.buttonPage.Name = "buttonPage";
            this.buttonPage.Size = new System.Drawing.Size(36, 30);
            this.buttonPage.TabIndex = 15;
            this.buttonPage.Text = "1/2";
            this.buttonPage.UseVisualStyleBackColor = false;
            // 
            // panelAck
            // 
            this.panelAck.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelAck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAck.Controls.Add(this.textBox2);
            this.panelAck.Controls.Add(this.label1);
            this.panelAck.Controls.Add(this.textBox5);
            this.panelAck.Controls.Add(this.textBox1);
            this.panelAck.Controls.Add(this.label3);
            this.panelAck.Controls.Add(this.label4);
            this.panelAck.Controls.Add(this.textBox3);
            this.panelAck.Controls.Add(this.textBox4);
            this.panelAck.Controls.Add(this.label5);
            this.panelAck.Location = new System.Drawing.Point(13, 360);
            this.panelAck.Name = "panelAck";
            this.panelAck.Size = new System.Drawing.Size(1004, 117);
            this.panelAck.TabIndex = 16;
            // 
            // labelSimple
            // 
            this.labelSimple.AutoSize = true;
            this.labelSimple.BackColor = System.Drawing.Color.Transparent;
            this.labelSimple.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSimple.ForeColor = System.Drawing.Color.White;
            this.labelSimple.Location = new System.Drawing.Point(895, 45);
            this.labelSimple.Name = "labelSimple";
            this.labelSimple.Size = new System.Drawing.Size(124, 13);
            this.labelSimple.TabIndex = 82;
            this.labelSimple.Text = "Back to simplified mode?";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.labelTitle.Location = new System.Drawing.Point(421, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(188, 24);
            this.labelTitle.TabIndex = 89;
            this.labelTitle.Text = "HELP AND ABOUT";
            // 
            // pictureBoxStandard
            // 
            this.pictureBoxStandard.Image = global::CableGuardian.Properties.Resources.Title;
            this.pictureBoxStandard.Location = new System.Drawing.Point(453, 167);
            this.pictureBoxStandard.Name = "pictureBoxStandard";
            this.pictureBoxStandard.Size = new System.Drawing.Size(127, 127);
            this.pictureBoxStandard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStandard.TabIndex = 92;
            this.pictureBoxStandard.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(425, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 16);
            this.label7.TabIndex = 91;
            this.label7.Text = "USER GUIDE ON STEAM";
            // 
            // buttonCopyInfo
            // 
            this.buttonCopyInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonCopyInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonCopyInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonCopyInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyInfo.ForeColor = System.Drawing.Color.White;
            this.buttonCopyInfo.Location = new System.Drawing.Point(516, 54);
            this.buttonCopyInfo.Name = "buttonCopyInfo";
            this.buttonCopyInfo.Size = new System.Drawing.Size(117, 24);
            this.buttonCopyInfo.TabIndex = 94;
            this.buttonCopyInfo.Text = "Copy System Info";
            this.buttonCopyInfo.UseVisualStyleBackColor = false;
            // 
            // buttonDiscussions
            // 
            this.buttonDiscussions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonDiscussions.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonDiscussions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonDiscussions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDiscussions.ForeColor = System.Drawing.Color.White;
            this.buttonDiscussions.Location = new System.Drawing.Point(399, 54);
            this.buttonDiscussions.Name = "buttonDiscussions";
            this.buttonDiscussions.Size = new System.Drawing.Size(117, 24);
            this.buttonDiscussions.TabIndex = 93;
            this.buttonDiscussions.Text = "Steam Discussions";
            this.buttonDiscussions.UseVisualStyleBackColor = false;
            // 
            // FormHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CableGuardian.Properties.Resources.Help2;
            this.ClientSize = new System.Drawing.Size(1032, 492);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCopyInfo);
            this.Controls.Add(this.buttonDiscussions);
            this.Controls.Add(this.pictureBoxStandard);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelSimple);
            this.Controls.Add(this.panelAck);
            this.Controls.Add(this.buttonPage);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHelp";
            this.ShowInTaskbar = false;
            this.Text = "Cable Guardian Help";
            this.panelAck.ResumeLayout(false);
            this.panelAck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStandard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button buttonPage;
        private System.Windows.Forms.Panel panelAck;
        private System.Windows.Forms.Label labelSimple;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxStandard;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonCopyInfo;
        private System.Windows.Forms.Button buttonDiscussions;
    }
}