﻿namespace MagnifierSoftwareV_1.MouseMove
{
    partial class MagnifierForm
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
            this.SuspendLayout();
            // 
            // MagnifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 260);
            this.DoubleBuffered = true;
            this.Name = "MagnifierForm";
            this.Text = "MagnifierForm";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MagnifierForm_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}