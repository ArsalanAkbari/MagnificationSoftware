namespace MagnifierSoftwareV_1.EvaluationEye
{
    partial class InformationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationForm));
            this.userName_label = new System.Windows.Forms.Label();
            this.userName_textBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.MiniMize_button = new System.Windows.Forms.Button();
            this.Exit_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_SensitivityFactor = new System.Windows.Forms.TrackBar();
            this.lbl_SensitivityFactor = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_OpenTestFolder = new System.Windows.Forms.Button();
            this.traceBaarSensitivityDownbutton = new System.Windows.Forms.Button();
            this.traceBaarSensitivityUpbutton = new System.Windows.Forms.Button();
            this.startTestButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SensitivityFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // userName_label
            // 
            this.userName_label.AutoSize = true;
            this.userName_label.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userName_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName_label.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.userName_label.Location = new System.Drawing.Point(5, 54);
            this.userName_label.Name = "userName_label";
            this.userName_label.Size = new System.Drawing.Size(152, 20);
            this.userName_label.TabIndex = 7;
            this.userName_label.Text = "Enter Your Name:";
            // 
            // userName_textBox
            // 
            this.userName_textBox.Location = new System.Drawing.Point(169, 54);
            this.userName_textBox.Name = "userName_textBox";
            this.userName_textBox.Size = new System.Drawing.Size(232, 20);
            this.userName_textBox.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MiniMize_button);
            this.panel1.Controls.Add(this.Exit_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 41);
            this.panel1.TabIndex = 37;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 26);
            this.label1.TabIndex = 38;
            this.label1.Text = "Calibration";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label1_MouseDown);
            // 
            // MiniMize_button
            // 
            this.MiniMize_button.BackColor = System.Drawing.Color.White;
            this.MiniMize_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.MiniMize_button.FlatAppearance.BorderSize = 3;
            this.MiniMize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiniMize_button.Font = new System.Drawing.Font("Bodoni MT Condensed", 13.68F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiniMize_button.ForeColor = System.Drawing.Color.Transparent;
            this.MiniMize_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.minimize;
            this.MiniMize_button.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.MiniMize_button.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MiniMize_button.Location = new System.Drawing.Point(888, 0);
            this.MiniMize_button.Margin = new System.Windows.Forms.Padding(2);
            this.MiniMize_button.Name = "MiniMize_button";
            this.MiniMize_button.Size = new System.Drawing.Size(41, 41);
            this.MiniMize_button.TabIndex = 36;
            this.MiniMize_button.UseVisualStyleBackColor = false;
            this.MiniMize_button.Click += new System.EventHandler(this.MiniMize_button_Click);
            // 
            // Exit_button
            // 
            this.Exit_button.BackColor = System.Drawing.Color.White;
            this.Exit_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.Exit_button.FlatAppearance.BorderSize = 3;
            this.Exit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit_button.Font = new System.Drawing.Font("Bodoni MT Condensed", 13.68F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit_button.ForeColor = System.Drawing.Color.Transparent;
            this.Exit_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.delete1;
            this.Exit_button.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Exit_button.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Exit_button.Location = new System.Drawing.Point(929, 0);
            this.Exit_button.Margin = new System.Windows.Forms.Padding(2);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(41, 41);
            this.Exit_button.TabIndex = 35;
            this.Exit_button.UseVisualStyleBackColor = false;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.Window;
            this.label6.Location = new System.Drawing.Point(93, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 30);
            this.label6.TabIndex = 96;
            this.label6.Text = "Size of the Ball / Squre";
            // 
            // tb_SensitivityFactor
            // 
            this.tb_SensitivityFactor.BackColor = System.Drawing.Color.Black;
            this.tb_SensitivityFactor.Location = new System.Drawing.Point(80, 120);
            this.tb_SensitivityFactor.Maximum = 100;
            this.tb_SensitivityFactor.Minimum = 10;
            this.tb_SensitivityFactor.Name = "tb_SensitivityFactor";
            this.tb_SensitivityFactor.Size = new System.Drawing.Size(241, 45);
            this.tb_SensitivityFactor.TabIndex = 92;
            this.tb_SensitivityFactor.Tag = "444";
            this.tb_SensitivityFactor.Value = 10;
            this.tb_SensitivityFactor.Scroll += new System.EventHandler(this.tb_SensitivityFactor_Scroll);
            // 
            // lbl_SensitivityFactor
            // 
            this.lbl_SensitivityFactor.BackColor = System.Drawing.Color.Black;
            this.lbl_SensitivityFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_SensitivityFactor.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_SensitivityFactor.Location = new System.Drawing.Point(184, 92);
            this.lbl_SensitivityFactor.Name = "lbl_SensitivityFactor";
            this.lbl_SensitivityFactor.Size = new System.Drawing.Size(63, 34);
            this.lbl_SensitivityFactor.TabIndex = 93;
            this.lbl_SensitivityFactor.Text = "?";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(414, 90);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(10);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(544, 332);
            this.richTextBox1.TabIndex = 97;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(409, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(500, 25);
            this.label3.TabIndex = 98;
            this.label3.Text = "Please read the Information before doing the Test !";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 437);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(976, 10);
            this.panel2.TabIndex = 39;
            // 
            // button_OpenTestFolder
            // 
            this.button_OpenTestFolder.BackColor = System.Drawing.Color.White;
            this.button_OpenTestFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.button_OpenTestFolder.Image = global::MagnifierSoftwareV_1.Properties.Resources.folder1;
            this.button_OpenTestFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_OpenTestFolder.Location = new System.Drawing.Point(12, 379);
            this.button_OpenTestFolder.Name = "button_OpenTestFolder";
            this.button_OpenTestFolder.Size = new System.Drawing.Size(271, 43);
            this.button_OpenTestFolder.TabIndex = 99;
            this.button_OpenTestFolder.Text = "Open Test Results  Folder";
            this.button_OpenTestFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_OpenTestFolder.UseVisualStyleBackColor = false;
            this.button_OpenTestFolder.Click += new System.EventHandler(this.button_OpenTestFolder_Click);
            // 
            // traceBaarSensitivityDownbutton
            // 
            this.traceBaarSensitivityDownbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarSensitivityDownbutton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarSensitivityDownbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_more_black_48dp;
            this.traceBaarSensitivityDownbutton.Location = new System.Drawing.Point(10, 97);
            this.traceBaarSensitivityDownbutton.Name = "traceBaarSensitivityDownbutton";
            this.traceBaarSensitivityDownbutton.Size = new System.Drawing.Size(64, 68);
            this.traceBaarSensitivityDownbutton.TabIndex = 95;
            this.traceBaarSensitivityDownbutton.UseVisualStyleBackColor = false;
            this.traceBaarSensitivityDownbutton.Click += new System.EventHandler(this.traceBaarSensitivityDownbutton_Click);
            // 
            // traceBaarSensitivityUpbutton
            // 
            this.traceBaarSensitivityUpbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarSensitivityUpbutton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarSensitivityUpbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_less_black_48dp;
            this.traceBaarSensitivityUpbutton.Location = new System.Drawing.Point(335, 97);
            this.traceBaarSensitivityUpbutton.Name = "traceBaarSensitivityUpbutton";
            this.traceBaarSensitivityUpbutton.Size = new System.Drawing.Size(64, 68);
            this.traceBaarSensitivityUpbutton.TabIndex = 94;
            this.traceBaarSensitivityUpbutton.UseVisualStyleBackColor = false;
            this.traceBaarSensitivityUpbutton.Click += new System.EventHandler(this.traceBaarSensitivityUpbutton_Click);
            // 
            // startTestButton
            // 
            this.startTestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.startTestButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.startTestButton.FlatAppearance.BorderSize = 3;
            this.startTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startTestButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.startTestButton.Image = global::MagnifierSoftwareV_1.Properties.Resources.play_button1;
            this.startTestButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.startTestButton.Location = new System.Drawing.Point(12, 319);
            this.startTestButton.Name = "startTestButton";
            this.startTestButton.Size = new System.Drawing.Size(271, 43);
            this.startTestButton.TabIndex = 5;
            this.startTestButton.Text = "Start The Test";
            this.startTestButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.startTestButton.UseVisualStyleBackColor = false;
            this.startTestButton.Click += new System.EventHandler(this.startTestButton_Click);
            this.startTestButton.MouseLeave += new System.EventHandler(this.startTestButton_MouseLeave);
            this.startTestButton.MouseHover += new System.EventHandler(this.startTestButton_MouseHover);
            // 
            // InformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(976, 447);
            this.Controls.Add(this.button_OpenTestFolder);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.traceBaarSensitivityDownbutton);
            this.Controls.Add(this.traceBaarSensitivityUpbutton);
            this.Controls.Add(this.tb_SensitivityFactor);
            this.Controls.Add(this.lbl_SensitivityFactor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.userName_label);
            this.Controls.Add(this.userName_textBox);
            this.Controls.Add(this.startTestButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "InformationForm";
            this.Text = "InformationForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.InformationForm_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SensitivityFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button startTestButton;
        private System.Windows.Forms.Label userName_label;
        private System.Windows.Forms.TextBox userName_textBox;
        private System.Windows.Forms.Button MiniMize_button;
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button traceBaarSensitivityDownbutton;
        private System.Windows.Forms.Button traceBaarSensitivityUpbutton;
        private System.Windows.Forms.TrackBar tb_SensitivityFactor;
        private System.Windows.Forms.Label lbl_SensitivityFactor;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_OpenTestFolder;
    }
}