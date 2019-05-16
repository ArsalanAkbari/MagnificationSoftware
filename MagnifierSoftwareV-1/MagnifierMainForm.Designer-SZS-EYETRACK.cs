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
            this.button1 = new System.Windows.Forms.Button();
            this.Calibration_Button = new System.Windows.Forms.Button();
            this.addUser_button = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.MagnifierEye_Button = new System.Windows.Forms.Button();
            this.Exit_button = new System.Windows.Forms.Button();
            this.Setup_button = new System.Windows.Forms.Button();
            this.Magnifier_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(42, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 38);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Calibration_Button
            // 
            this.Calibration_Button.BackColor = System.Drawing.Color.White;
            this.Calibration_Button.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Calibration_Button.ForeColor = System.Drawing.Color.Black;
            this.Calibration_Button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Calibration_Button.Location = new System.Drawing.Point(42, 418);
            this.Calibration_Button.Name = "Calibration_Button";
            this.Calibration_Button.Size = new System.Drawing.Size(240, 32);
            this.Calibration_Button.TabIndex = 15;
            this.Calibration_Button.Text = "Calibration";
            this.Calibration_Button.UseVisualStyleBackColor = false;
            this.Calibration_Button.Click += new System.EventHandler(this.Calibration_Button_Click);
            // 
            // addUser_button
            // 
            this.addUser_button.BackColor = System.Drawing.Color.White;
            this.addUser_button.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addUser_button.ForeColor = System.Drawing.Color.Black;
            this.addUser_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_person_add_black_48dp;
            this.addUser_button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.addUser_button.Location = new System.Drawing.Point(28, 17);
            this.addUser_button.Name = "addUser_button";
            this.addUser_button.Size = new System.Drawing.Size(107, 100);
            this.addUser_button.TabIndex = 16;
            this.addUser_button.UseVisualStyleBackColor = false;
            this.addUser_button.Click += new System.EventHandler(this.addUser_button_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(497, 25);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(496, 425);
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // MagnifierEye_Button
            // 
            this.MagnifierEye_Button.BackColor = System.Drawing.Color.White;
            this.MagnifierEye_Button.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MagnifierEye_Button.ForeColor = System.Drawing.Color.White;
            this.MagnifierEye_Button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_remove_red_eye_black_48dp;
            this.MagnifierEye_Button.Location = new System.Drawing.Point(154, 134);
            this.MagnifierEye_Button.Name = "MagnifierEye_Button";
            this.MagnifierEye_Button.Size = new System.Drawing.Size(107, 100);
            this.MagnifierEye_Button.TabIndex = 11;
            this.MagnifierEye_Button.UseVisualStyleBackColor = false;
            this.MagnifierEye_Button.Click += new System.EventHandler(this.MagnifierEye_Button_Click);
            this.MagnifierEye_Button.MouseHover += new System.EventHandler(this.EyeMagnifierButton_MouseHover);
            // 
            // Exit_button
            // 
            this.Exit_button.BackColor = System.Drawing.Color.White;
            this.Exit_button.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit_button.ForeColor = System.Drawing.Color.White;
            this.Exit_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_close_black_48dp;
            this.Exit_button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_button.Location = new System.Drawing.Point(154, 249);
            this.Exit_button.Margin = new System.Windows.Forms.Padding(2);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(107, 100);
            this.Exit_button.TabIndex = 9;
            this.Exit_button.UseVisualStyleBackColor = false;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            this.Exit_button.MouseHover += new System.EventHandler(this.ExitButtone_MouseHover);
            // 
            // Setup_button
            // 
            this.Setup_button.BackColor = System.Drawing.Color.White;
            this.Setup_button.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Setup_button.ForeColor = System.Drawing.Color.White;
            this.Setup_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_settings_black_48dp;
            this.Setup_button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Setup_button.Location = new System.Drawing.Point(154, 17);
            this.Setup_button.Margin = new System.Windows.Forms.Padding(2);
            this.Setup_button.Name = "Setup_button";
            this.Setup_button.Size = new System.Drawing.Size(107, 100);
            this.Setup_button.TabIndex = 8;
            this.Setup_button.UseVisualStyleBackColor = false;
            this.Setup_button.Click += new System.EventHandler(this.Setup_button_Click);
            this.Setup_button.MouseHover += new System.EventHandler(this.SetupButton_MouseHover);
            // 
            // Magnifier_Button
            // 
            this.Magnifier_Button.BackColor = System.Drawing.Color.White;
            this.Magnifier_Button.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Magnifier_Button.ForeColor = System.Drawing.Color.White;
            this.Magnifier_Button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_mouse_black_48dp1;
            this.Magnifier_Button.Location = new System.Drawing.Point(28, 134);
            this.Magnifier_Button.Margin = new System.Windows.Forms.Padding(2);
            this.Magnifier_Button.Name = "Magnifier_Button";
            this.Magnifier_Button.Size = new System.Drawing.Size(107, 100);
            this.Magnifier_Button.TabIndex = 7;
            this.Magnifier_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Magnifier_Button.UseVisualStyleBackColor = false;
            this.Magnifier_Button.Click += new System.EventHandler(this.Magnifier_Button_Click);
            this.Magnifier_Button.MouseHover += new System.EventHandler(this.MouseMagnifierButton_MouseHover);
            // 
            // MagnifierMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(430, 477);
            this.Controls.Add(this.addUser_button);
            this.Controls.Add(this.Calibration_Button);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.MagnifierEye_Button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Exit_button);
            this.Controls.Add(this.Setup_button);
            this.Controls.Add(this.Magnifier_Button);
            this.Name = "MagnifierMainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MagnifierEye_Button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.Button Setup_button;
        private System.Windows.Forms.Button Magnifier_Button;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button Calibration_Button;
        private System.Windows.Forms.Button addUser_button;
    }
}

