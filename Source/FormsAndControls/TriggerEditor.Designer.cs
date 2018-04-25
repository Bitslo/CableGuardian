namespace CableGuardian
{
    partial class TriggerEditor
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
            this.comboBoxEvent = new System.Windows.Forms.ComboBox();
            this.numericUpDownLimit = new System.Windows.Forms.NumericUpDown();
            this.labelLimit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxEvent
            // 
            this.comboBoxEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEvent.FormattingEnabled = true;
            this.comboBoxEvent.Location = new System.Drawing.Point(0, 5);
            this.comboBoxEvent.Name = "comboBoxEvent";
            this.comboBoxEvent.Size = new System.Drawing.Size(173, 21);
            this.comboBoxEvent.TabIndex = 0;
            // 
            // numericUpDownLimit
            // 
            this.numericUpDownLimit.Location = new System.Drawing.Point(136, 30);
            this.numericUpDownLimit.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownLimit.Name = "numericUpDownLimit";
            this.numericUpDownLimit.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownLimit.TabIndex = 4;
            // 
            // labelLimit
            // 
            this.labelLimit.AutoSize = true;
            this.labelLimit.ForeColor = System.Drawing.Color.White;
            this.labelLimit.Location = new System.Drawing.Point(50, 32);
            this.labelLimit.Name = "labelLimit";
            this.labelLimit.Size = new System.Drawing.Size(84, 13);
            this.labelLimit.TabIndex = 14;
            this.labelLimit.Text = "Repetition limit =";
            // 
            // TriggerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.labelLimit);
            this.Controls.Add(this.numericUpDownLimit);
            this.Controls.Add(this.comboBoxEvent);
            this.Name = "TriggerEditor";
            this.Size = new System.Drawing.Size(177, 54);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxEvent;
        private System.Windows.Forms.NumericUpDown numericUpDownLimit;
        private System.Windows.Forms.Label labelLimit;
    }
}
