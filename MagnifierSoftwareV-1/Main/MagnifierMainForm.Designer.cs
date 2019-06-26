namespace MagnifierSoftwareV_1
{
    partial class MagnifierMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagnifierMainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Exit_button = new System.Windows.Forms.Button();
            this.MiniMize_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_ZoomFaktor = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ZoomIn_button = new System.Windows.Forms.Button();
            this.ZoomOutbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.HelpToturial_button = new System.Windows.Forms.Button();
            this.MouseHidenMainForm_button = new System.Windows.Forms.Button();
            this.Setup_button = new System.Windows.Forms.Button();
            this.Evaluation_button = new System.Windows.Forms.Button();
            this.FullScreenMouse_Button = new System.Windows.Forms.Button();
            this.FullscreenMaginfierEye_Button = new System.Windows.Forms.Button();
            this.MaginfierEye_Button = new System.Windows.Forms.Button();
            this.MagniferUsingMouse_button = new System.Windows.Forms.Button();
            this.TobiiCalibration_button = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Exit_button);
            this.panel1.Controls.Add(this.MiniMize_button);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label6_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MagnifierSoftwareV_1.Properties.Resources.Eyetracker1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Exit_button
            // 
            this.Exit_button.BackColor = System.Drawing.Color.White;
            this.Exit_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.Exit_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.Exit_button, "Exit_button");
            this.Exit_button.ForeColor = System.Drawing.Color.Transparent;
            this.Exit_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.delete1;
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.UseVisualStyleBackColor = false;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            // 
            // MiniMize_button
            // 
            this.MiniMize_button.BackColor = System.Drawing.Color.White;
            this.MiniMize_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.MiniMize_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.MiniMize_button, "MiniMize_button");
            this.MiniMize_button.ForeColor = System.Drawing.Color.Transparent;
            this.MiniMize_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.minimize;
            this.MiniMize_button.Name = "MiniMize_button";
            this.MiniMize_button.UseVisualStyleBackColor = false;
            this.MiniMize_button.Click += new System.EventHandler(this.MiniMize_button_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // label_ZoomFaktor
            // 
            resources.ApplyResources(this.label_ZoomFaktor, "label_ZoomFaktor");
            this.label_ZoomFaktor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.label_ZoomFaktor.Name = "label_ZoomFaktor";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.panel2.Controls.Add(this.label_ZoomFaktor);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.ZoomIn_button);
            this.panel2.Controls.Add(this.ZoomOutbutton);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // ZoomIn_button
            // 
            this.ZoomIn_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.ZoomIn_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ZoomIn_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.ZoomIn_button, "ZoomIn_button");
            this.ZoomIn_button.ForeColor = System.Drawing.Color.Black;
            this.ZoomIn_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.zooming_magnifying_glass;
            this.ZoomIn_button.Name = "ZoomIn_button";
            this.ZoomIn_button.UseVisualStyleBackColor = false;
            this.ZoomIn_button.Click += new System.EventHandler(this.ZoomIn_button_Click);
            // 
            // ZoomOutbutton
            // 
            this.ZoomOutbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.ZoomOutbutton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ZoomOutbutton.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.ZoomOutbutton, "ZoomOutbutton");
            this.ZoomOutbutton.ForeColor = System.Drawing.Color.Black;
            this.ZoomOutbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.zoom_out;
            this.ZoomOutbutton.Name = "ZoomOutbutton";
            this.ZoomOutbutton.UseVisualStyleBackColor = false;
            this.ZoomOutbutton.Click += new System.EventHandler(this.ZoomOutbutton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Name = "label5";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // HelpToturial_button
            // 
            this.HelpToturial_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.HelpToturial_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.HelpToturial_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.HelpToturial_button, "HelpToturial_button");
            this.HelpToturial_button.ForeColor = System.Drawing.Color.White;
            this.HelpToturial_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.Help2;
            this.HelpToturial_button.Name = "HelpToturial_button";
            this.HelpToturial_button.UseVisualStyleBackColor = false;
            this.HelpToturial_button.Click += new System.EventHandler(this.HelpToturial_button_Click);
            // 
            // MouseHidenMainForm_button
            // 
            this.MouseHidenMainForm_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.MouseHidenMainForm_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.MouseHidenMainForm_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.MouseHidenMainForm_button, "MouseHidenMainForm_button");
            this.MouseHidenMainForm_button.ForeColor = System.Drawing.Color.White;
            this.MouseHidenMainForm_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.cursor2;
            this.MouseHidenMainForm_button.Name = "MouseHidenMainForm_button";
            this.MouseHidenMainForm_button.UseVisualStyleBackColor = false;
            this.MouseHidenMainForm_button.Click += new System.EventHandler(this.MouseHidenMainForm_button_Click);
            // 
            // Setup_button
            // 
            this.Setup_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.Setup_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.Setup_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.Setup_button, "Setup_button");
            this.Setup_button.ForeColor = System.Drawing.Color.White;
            this.Setup_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.settings;
            this.Setup_button.Name = "Setup_button";
            this.Setup_button.UseVisualStyleBackColor = false;
            this.Setup_button.Click += new System.EventHandler(this.Setup_button_Click);
            // 
            // Evaluation_button
            // 
            this.Evaluation_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.Evaluation_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.Evaluation_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.Evaluation_button, "Evaluation_button");
            this.Evaluation_button.ForeColor = System.Drawing.Color.White;
            this.Evaluation_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.test;
            this.Evaluation_button.Name = "Evaluation_button";
            this.Evaluation_button.UseVisualStyleBackColor = false;
            this.Evaluation_button.Click += new System.EventHandler(this.Evaluation_button_Click);
            // 
            // FullScreenMouse_Button
            // 
            this.FullScreenMouse_Button.BackColor = System.Drawing.Color.White;
            this.FullScreenMouse_Button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FullScreenMouse_Button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.FullScreenMouse_Button, "FullScreenMouse_Button");
            this.FullScreenMouse_Button.ForeColor = System.Drawing.Color.Black;
            this.FullScreenMouse_Button.Name = "FullScreenMouse_Button";
            this.FullScreenMouse_Button.UseVisualStyleBackColor = false;
            this.FullScreenMouse_Button.Click += new System.EventHandler(this.FullScreenMouse_Click);
            this.FullScreenMouse_Button.MouseLeave += new System.EventHandler(this.FullScreenMouse_MouseLeave);
            this.FullScreenMouse_Button.MouseHover += new System.EventHandler(this.FullScreenMouse_MouseHover);
            // 
            // FullscreenMaginfierEye_Button
            // 
            this.FullscreenMaginfierEye_Button.BackColor = System.Drawing.Color.White;
            this.FullscreenMaginfierEye_Button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FullscreenMaginfierEye_Button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.FullscreenMaginfierEye_Button, "FullscreenMaginfierEye_Button");
            this.FullscreenMaginfierEye_Button.ForeColor = System.Drawing.Color.Black;
            this.FullscreenMaginfierEye_Button.Image = global::MagnifierSoftwareV_1.Properties.Resources.fullscreenEye;
            this.FullscreenMaginfierEye_Button.Name = "FullscreenMaginfierEye_Button";
            this.FullscreenMaginfierEye_Button.UseVisualStyleBackColor = false;
            this.FullscreenMaginfierEye_Button.Click += new System.EventHandler(this.button4_Click);
            this.FullscreenMaginfierEye_Button.MouseLeave += new System.EventHandler(this.FullscreenMaginfierEye_MouseLeave);
            this.FullscreenMaginfierEye_Button.MouseHover += new System.EventHandler(this.FullscreenMaginfierEye_MouseHover);
            // 
            // MaginfierEye_Button
            // 
            this.MaginfierEye_Button.BackColor = System.Drawing.Color.White;
            this.MaginfierEye_Button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MaginfierEye_Button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.MaginfierEye_Button, "MaginfierEye_Button");
            this.MaginfierEye_Button.ForeColor = System.Drawing.Color.Black;
            this.MaginfierEye_Button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_remove_red_eye_black_48dp;
            this.MaginfierEye_Button.Name = "MaginfierEye_Button";
            this.MaginfierEye_Button.UseVisualStyleBackColor = false;
            this.MaginfierEye_Button.Click += new System.EventHandler(this.button3_Click);
            this.MaginfierEye_Button.MouseLeave += new System.EventHandler(this.MagnifyingGlass_MouseLeave);
            this.MaginfierEye_Button.MouseHover += new System.EventHandler(this.MagnifyingGlass_MouseHover);
            // 
            // MagniferUsingMouse_button
            // 
            this.MagniferUsingMouse_button.BackColor = System.Drawing.Color.White;
            this.MagniferUsingMouse_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MagniferUsingMouse_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.MagniferUsingMouse_button, "MagniferUsingMouse_button");
            this.MagniferUsingMouse_button.ForeColor = System.Drawing.Color.Black;
            this.MagniferUsingMouse_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_mouse_black_48dp1;
            this.MagniferUsingMouse_button.Name = "MagniferUsingMouse_button";
            this.MagniferUsingMouse_button.UseVisualStyleBackColor = false;
            this.MagniferUsingMouse_button.Click += new System.EventHandler(this.MagniferUsingMouse_button_Click);
            this.MagniferUsingMouse_button.MouseLeave += new System.EventHandler(this.MagniferUsingMouse_MouseLeave);
            this.MagniferUsingMouse_button.MouseHover += new System.EventHandler(this.MagniferUsingMouse_MouseHover);
            // 
            // TobiiCalibration_button
            // 
            this.TobiiCalibration_button.BackColor = System.Drawing.Color.White;
            this.TobiiCalibration_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.TobiiCalibration_button.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.TobiiCalibration_button, "TobiiCalibration_button");
            this.TobiiCalibration_button.ForeColor = System.Drawing.Color.Black;
            this.TobiiCalibration_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.calibration_mark1;
            this.TobiiCalibration_button.Name = "TobiiCalibration_button";
            this.TobiiCalibration_button.UseVisualStyleBackColor = false;
            this.TobiiCalibration_button.Click += new System.EventHandler(this.Button1_Click_2);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Name = "label7";
            // 
            // MagnifierMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TobiiCalibration_button);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HelpToturial_button);
            this.Controls.Add(this.MouseHidenMainForm_button);
            this.Controls.Add(this.Setup_button);
            this.Controls.Add(this.Evaluation_button);
            this.Controls.Add(this.FullScreenMouse_Button);
            this.Controls.Add(this.FullscreenMaginfierEye_Button);
            this.Controls.Add(this.MaginfierEye_Button);
            this.Controls.Add(this.MagniferUsingMouse_button);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "MagnifierMainForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.Button Setup_button;
        private System.Windows.Forms.Button MiniMize_button;
        private System.Windows.Forms.Button MouseHidenMainForm_button;
        private System.Windows.Forms.Button HelpToturial_button;
        private System.Windows.Forms.Button ZoomIn_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ZoomOutbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label_ZoomFaktor;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button MagniferUsingMouse_button;
        public System.Windows.Forms.Button MaginfierEye_Button;
        public System.Windows.Forms.Button FullscreenMaginfierEye_Button;
        public System.Windows.Forms.Button FullScreenMouse_Button;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Button Evaluation_button;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Button TobiiCalibration_button;
    }
}

