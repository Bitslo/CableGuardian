namespace CableGuardian
{
    partial class WaveActionControl
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
            this.WaveEdit = new CableGuardian.WaveEditor();
            this.TrigEdit = new CableGuardian.TriggerEditor();
            this.CondEdit = new CableGuardian.ConditionEditor();
            this.label4 = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WaveEdit
            // 
            this.WaveEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.WaveEdit.Location = new System.Drawing.Point(0, 0);
            this.WaveEdit.Name = "WaveEdit";
            this.WaveEdit.Size = new System.Drawing.Size(437, 49);
            this.WaveEdit.TabIndex = 0;
            // 
            // TrigEdit
            // 
            this.TrigEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TrigEdit.Location = new System.Drawing.Point(496, -2);
            this.TrigEdit.Name = "TrigEdit";
            this.TrigEdit.Size = new System.Drawing.Size(177, 54);
            this.TrigEdit.TabIndex = 4;
            // 
            // CondEdit
            // 
            this.CondEdit.AutoSize = true;
            this.CondEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CondEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.CondEdit.Location = new System.Drawing.Point(3, 55);
            this.CondEdit.Name = "CondEdit";
            this.CondEdit.Size = new System.Drawing.Size(670, 29);
            this.CondEdit.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(448, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "WHEN";
            // 
            // labelLine
            // 
            this.labelLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLine.ForeColor = System.Drawing.Color.Gray;
            this.labelLine.Location = new System.Drawing.Point(-6, 41);
            this.labelLine.Margin = new System.Windows.Forms.Padding(0);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(696, 19);
            this.labelLine.TabIndex = 10;
            this.labelLine.Tag = "MANUAL";
            this.labelLine.Text = "_________________________________________________________________________________" +
    "_______________________________________________________";
            this.labelLine.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WaveActionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CondEdit);
            this.Controls.Add(this.TrigEdit);
            this.Controls.Add(this.WaveEdit);
            this.Controls.Add(this.labelLine);
            this.Name = "WaveActionControl";
            this.Size = new System.Drawing.Size(676, 85);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WaveEditor WaveEdit;
        private TriggerEditor TrigEdit;
        private ConditionEditor CondEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelLine;
    }
}
