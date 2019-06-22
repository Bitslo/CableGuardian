namespace CableGuardian
{
    partial class ConditionEditor
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
            this.numericUpDownHalfTurns = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxOperator = new System.Windows.Forms.ComboBox();
            this.numericUpDownPeak = new System.Windows.Forms.NumericUpDown();
            this.labelPeak = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelPeak = new System.Windows.Forms.Panel();
            this.panelRotSide = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSide = new System.Windows.Forms.ComboBox();
            this.panelHalfTurns = new System.Windows.Forms.Panel();
            this.panelHalfTurnsMax = new System.Windows.Forms.Panel();
            this.numericUpDownHalfTurnsMax = new System.Windows.Forms.NumericUpDown();
            this.labelHalfTurnsMax = new System.Windows.Forms.Label();
            this.panelAccu = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAccu = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHalfTurns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPeak)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelPeak.SuspendLayout();
            this.panelRotSide.SuspendLayout();
            this.panelHalfTurns.SuspendLayout();
            this.panelHalfTurnsMax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHalfTurnsMax)).BeginInit();
            this.panelAccu.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownHalfTurns
            // 
            this.numericUpDownHalfTurns.Location = new System.Drawing.Point(140, 3);
            this.numericUpDownHalfTurns.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownHalfTurns.Name = "numericUpDownHalfTurns";
            this.numericUpDownHalfTurns.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownHalfTurns.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "AND     half-turns";
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Location = new System.Drawing.Point(101, 3);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(36, 21);
            this.comboBoxOperator.TabIndex = 4;
            // 
            // numericUpDownPeak
            // 
            this.numericUpDownPeak.Location = new System.Drawing.Point(108, 3);
            this.numericUpDownPeak.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownPeak.Name = "numericUpDownPeak";
            this.numericUpDownPeak.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownPeak.TabIndex = 20;
            // 
            // labelPeak
            // 
            this.labelPeak.AutoSize = true;
            this.labelPeak.ForeColor = System.Drawing.Color.White;
            this.labelPeak.Location = new System.Drawing.Point(6, 7);
            this.labelPeak.Name = "labelPeak";
            this.labelPeak.Size = new System.Drawing.Size(92, 13);
            this.labelPeak.TabIndex = 13;
            this.labelPeak.Text = "peak half-turns >=";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "AND";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panelPeak, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelRotSide, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelHalfTurns, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelHalfTurnsMax, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelAccu, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(36, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 29);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // panelPeak
            // 
            this.panelPeak.Controls.Add(this.numericUpDownPeak);
            this.panelPeak.Controls.Add(this.labelPeak);
            this.panelPeak.Location = new System.Drawing.Point(634, 0);
            this.panelPeak.Margin = new System.Windows.Forms.Padding(0);
            this.panelPeak.Name = "panelPeak";
            this.panelPeak.Size = new System.Drawing.Size(146, 29);
            this.panelPeak.TabIndex = 4;
            this.panelPeak.Visible = false;
            // 
            // panelRotSide
            // 
            this.panelRotSide.Controls.Add(this.label1);
            this.panelRotSide.Controls.Add(this.comboBoxSide);
            this.panelRotSide.Location = new System.Drawing.Point(0, 0);
            this.panelRotSide.Margin = new System.Windows.Forms.Padding(0);
            this.panelRotSide.Name = "panelRotSide";
            this.panelRotSide.Size = new System.Drawing.Size(129, 29);
            this.panelRotSide.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "rotation side is";
            // 
            // comboBoxSide
            // 
            this.comboBoxSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSide.FormattingEnabled = true;
            this.comboBoxSide.Location = new System.Drawing.Point(75, 3);
            this.comboBoxSide.Name = "comboBoxSide";
            this.comboBoxSide.Size = new System.Drawing.Size(51, 21);
            this.comboBoxSide.TabIndex = 0;
            // 
            // panelHalfTurns
            // 
            this.panelHalfTurns.Controls.Add(this.numericUpDownHalfTurns);
            this.panelHalfTurns.Controls.Add(this.label4);
            this.panelHalfTurns.Controls.Add(this.comboBoxOperator);
            this.panelHalfTurns.Location = new System.Drawing.Point(129, 0);
            this.panelHalfTurns.Margin = new System.Windows.Forms.Padding(0);
            this.panelHalfTurns.Name = "panelHalfTurns";
            this.panelHalfTurns.Size = new System.Drawing.Size(181, 29);
            this.panelHalfTurns.TabIndex = 1;
            // 
            // panelHalfTurnsMax
            // 
            this.panelHalfTurnsMax.Controls.Add(this.numericUpDownHalfTurnsMax);
            this.panelHalfTurnsMax.Controls.Add(this.labelHalfTurnsMax);
            this.panelHalfTurnsMax.Location = new System.Drawing.Point(310, 0);
            this.panelHalfTurnsMax.Margin = new System.Windows.Forms.Padding(0);
            this.panelHalfTurnsMax.Name = "panelHalfTurnsMax";
            this.panelHalfTurnsMax.Size = new System.Drawing.Size(150, 29);
            this.panelHalfTurnsMax.TabIndex = 2;
            // 
            // numericUpDownHalfTurnsMax
            // 
            this.numericUpDownHalfTurnsMax.Location = new System.Drawing.Point(110, 3);
            this.numericUpDownHalfTurnsMax.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownHalfTurnsMax.Name = "numericUpDownHalfTurnsMax";
            this.numericUpDownHalfTurnsMax.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownHalfTurnsMax.TabIndex = 12;
            // 
            // labelHalfTurnsMax
            // 
            this.labelHalfTurnsMax.AutoSize = true;
            this.labelHalfTurnsMax.ForeColor = System.Drawing.Color.White;
            this.labelHalfTurnsMax.Location = new System.Drawing.Point(3, 7);
            this.labelHalfTurnsMax.Name = "labelHalfTurnsMax";
            this.labelHalfTurnsMax.Size = new System.Drawing.Size(103, 13);
            this.labelHalfTurnsMax.TabIndex = 12;
            this.labelHalfTurnsMax.Text = "AND     half-turns   <";
            // 
            // panelAccu
            // 
            this.panelAccu.Controls.Add(this.label3);
            this.panelAccu.Controls.Add(this.comboBoxAccu);
            this.panelAccu.Location = new System.Drawing.Point(460, 0);
            this.panelAccu.Margin = new System.Windows.Forms.Padding(0);
            this.panelAccu.Name = "panelAccu";
            this.panelAccu.Size = new System.Drawing.Size(174, 29);
            this.panelAccu.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "AND    twisting is";
            // 
            // comboBoxAccu
            // 
            this.comboBoxAccu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccu.FormattingEnabled = true;
            this.comboBoxAccu.Location = new System.Drawing.Point(92, 3);
            this.comboBoxAccu.Name = "comboBoxAccu";
            this.comboBoxAccu.Size = new System.Drawing.Size(79, 21);
            this.comboBoxAccu.TabIndex = 16;
            // 
            // ConditionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Name = "ConditionEditor";
            this.Size = new System.Drawing.Size(816, 29);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHalfTurns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPeak)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelPeak.ResumeLayout(false);
            this.panelPeak.PerformLayout();
            this.panelRotSide.ResumeLayout(false);
            this.panelRotSide.PerformLayout();
            this.panelHalfTurns.ResumeLayout(false);
            this.panelHalfTurns.PerformLayout();
            this.panelHalfTurnsMax.ResumeLayout(false);
            this.panelHalfTurnsMax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHalfTurnsMax)).EndInit();
            this.panelAccu.ResumeLayout(false);
            this.panelAccu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownHalfTurns;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.NumericUpDown numericUpDownPeak;
        private System.Windows.Forms.Label labelPeak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelRotSide;
        private System.Windows.Forms.ComboBox comboBoxSide;
        private System.Windows.Forms.Panel panelHalfTurns;
        private System.Windows.Forms.Panel panelHalfTurnsMax;
        private System.Windows.Forms.ComboBox comboBoxAccu;
        private System.Windows.Forms.Panel panelAccu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelPeak;
        private System.Windows.Forms.Label labelHalfTurnsMax;
        private System.Windows.Forms.NumericUpDown numericUpDownHalfTurnsMax;
    }
}
