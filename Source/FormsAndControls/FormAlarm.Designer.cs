namespace CableGuardian
{
    partial class FormAlarm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelMount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.waveEditor1 = new CableGuardian.WaveEditor();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.Color.Yellow;
            this.buttonClose.Location = new System.Drawing.Point(0, 0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(145, 25);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close alarm clock settings";
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelMount);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.waveEditor1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 103);
            this.panel1.TabIndex = 47;
            // 
            // labelMount
            // 
            this.labelMount.AutoSize = true;
            this.labelMount.ForeColor = System.Drawing.Color.White;
            this.labelMount.Location = new System.Drawing.Point(287, 21);
            this.labelMount.Name = "labelMount";
            this.labelMount.Size = new System.Drawing.Size(165, 13);
            this.labelMount.TabIndex = 57;
            this.labelMount.Tag = "";
            this.labelMount.Text = "Use the tray icon to set the alarm.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(236, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 13);
            this.label1.TabIndex = 58;
            this.label1.Tag = "";
            this.label1.Text = "Audio device is taken from the active profile.";
            // 
            // waveEditor1
            // 
            this.waveEditor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.waveEditor1.Location = new System.Drawing.Point(11, 41);
            this.waveEditor1.Name = "waveEditor1";
            this.waveEditor1.Size = new System.Drawing.Size(437, 49);
            this.waveEditor1.TabIndex = 2;
            // 
            // FormAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(465, 103);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAlarm";
            this.ShowInTaskbar = false;
            this.Text = "Alarm Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private WaveEditor waveEditor1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelMount;
        private System.Windows.Forms.Label label1;
    }
}