namespace CableGuardian
{
    partial class FormHelpSimple
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
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonDiscussions = new System.Windows.Forms.Button();
            this.buttonGuides = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonTutorial = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.Gray;
            this.labelVersion.Location = new System.Drawing.Point(1, 1);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(155, 13);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Cable Guardian v.1.01 by Bitslo";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonDiscussions
            // 
            this.buttonDiscussions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonDiscussions.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonDiscussions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonDiscussions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDiscussions.ForeColor = System.Drawing.Color.White;
            this.buttonDiscussions.Location = new System.Drawing.Point(188, 81);
            this.buttonDiscussions.Name = "buttonDiscussions";
            this.buttonDiscussions.Size = new System.Drawing.Size(134, 26);
            this.buttonDiscussions.TabIndex = 7;
            this.buttonDiscussions.Text = "Steam Discussions...";
            this.buttonDiscussions.UseVisualStyleBackColor = false;
            // 
            // buttonGuides
            // 
            this.buttonGuides.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonGuides.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonGuides.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonGuides.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGuides.ForeColor = System.Drawing.Color.White;
            this.buttonGuides.Location = new System.Drawing.Point(188, 56);
            this.buttonGuides.Name = "buttonGuides";
            this.buttonGuides.Size = new System.Drawing.Size(134, 26);
            this.buttonGuides.TabIndex = 6;
            this.buttonGuides.Text = "User Guides...";
            this.buttonGuides.UseVisualStyleBackColor = false;
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(188, 6);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(134, 26);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close help";
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // buttonTutorial
            // 
            this.buttonTutorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.buttonTutorial.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonTutorial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonTutorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTutorial.ForeColor = System.Drawing.Color.White;
            this.buttonTutorial.Location = new System.Drawing.Point(188, 31);
            this.buttonTutorial.Name = "buttonTutorial";
            this.buttonTutorial.Size = new System.Drawing.Size(134, 26);
            this.buttonTutorial.TabIndex = 8;
            this.buttonTutorial.Text = "Show welcome screen";
            this.buttonTutorial.UseVisualStyleBackColor = false;
            // 
            // FormHelpSimple
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CableGuardian.Properties.Resources.HelpSimple;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(328, 492);
            this.ControlBox = false;
            this.Controls.Add(this.buttonTutorial);
            this.Controls.Add(this.buttonDiscussions);
            this.Controls.Add(this.buttonGuides);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHelpSimple";
            this.ShowInTaskbar = false;
            this.Text = "Cable Guardian Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonDiscussions;
        private System.Windows.Forms.Button buttonGuides;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonTutorial;
    }
}