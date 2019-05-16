namespace MagnifierSoftwareV_1.MouseMove
{
    partial class ConfigurationForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Color_Blindness_comboBox = new System.Windows.Forms.ComboBox();
            this.cb_HideMouseCursor = new System.Windows.Forms.CheckBox();
            this.lbl_ZoomFactor = new System.Windows.Forms.Label();
            this.cb_Symmetry = new System.Windows.Forms.CheckBox();
            this.tb_ZoomFactor = new System.Windows.Forms.TrackBar();
            this.tb_SpeedFactor = new System.Windows.Forms.TrackBar();
            this.tb_Width = new System.Windows.Forms.TrackBar();
            this.tb_Height = new System.Windows.Forms.TrackBar();
            this.lbl_SpeedFactor = new System.Windows.Forms.Label();
            this.lbl_Width = new System.Windows.Forms.Label();
            this.lbl_Height = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HideMouseButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Exit_button = new System.Windows.Forms.Button();
            this.traceBaarWidthButton = new System.Windows.Forms.Button();
            this.traceBaarHightButton = new System.Windows.Forms.Button();
            this.traceBaarSpeedDownbutton = new System.Windows.Forms.Button();
            this.traceBaarSpeedUpbutton = new System.Windows.Forms.Button();
            this.traceBaarZoomButton2 = new System.Windows.Forms.Button();
            this.traceBaarZoomButton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ZoomFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SpeedFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(138, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 30);
            this.label2.TabIndex = 82;
            this.label2.Text = "Hide Mouse Curser";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(138, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 30);
            this.label1.TabIndex = 72;
            this.label1.Text = "Select Color Blindness C.B";
            // 
            // Color_Blindness_comboBox
            // 
            this.Color_Blindness_comboBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Color_Blindness_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Color_Blindness_comboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Color_Blindness_comboBox.FormattingEnabled = true;
            this.Color_Blindness_comboBox.Items.AddRange(new object[] {
            "Normal",
            "Invert all Colors",
            "Achromatomaly-----Complete C.B",
            "Achromatopsia------Complete C.B",
            "Deuteranopia--------Red Green C.B",
            "Deuteranomaly------Red Green C.B",
            "Protanomaly---------Red Green C.B",
            "Protanopia-----------Red Green C.B",
            "Tritanomaly----------Blue Yellow  C.B",
            "Tritanopia------------Blue Yellown C.B"});
            this.Color_Blindness_comboBox.Location = new System.Drawing.Point(142, 290);
            this.Color_Blindness_comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.Color_Blindness_comboBox.Name = "Color_Blindness_comboBox";
            this.Color_Blindness_comboBox.Size = new System.Drawing.Size(353, 33);
            this.Color_Blindness_comboBox.TabIndex = 71;
            this.Color_Blindness_comboBox.SelectedIndexChanged += new System.EventHandler(this.Color_Blindness_comboBox_SelectedIndexChanged);
            // 
            // cb_HideMouseCursor
            // 
            this.cb_HideMouseCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_HideMouseCursor.ForeColor = System.Drawing.SystemColors.Window;
            this.cb_HideMouseCursor.Location = new System.Drawing.Point(138, 181);
            this.cb_HideMouseCursor.Name = "cb_HideMouseCursor";
            this.cb_HideMouseCursor.Size = new System.Drawing.Size(164, 27);
            this.cb_HideMouseCursor.TabIndex = 70;
            this.cb_HideMouseCursor.Text = "Hide Mouse Cursor";
            this.cb_HideMouseCursor.CheckedChanged += new System.EventHandler(this.cb_HideMouseCursor_CheckedChanged);
            // 
            // lbl_ZoomFactor
            // 
            this.lbl_ZoomFactor.BackColor = System.Drawing.Color.Black;
            this.lbl_ZoomFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ZoomFactor.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_ZoomFactor.Location = new System.Drawing.Point(247, 25);
            this.lbl_ZoomFactor.Name = "lbl_ZoomFactor";
            this.lbl_ZoomFactor.Size = new System.Drawing.Size(67, 33);
            this.lbl_ZoomFactor.TabIndex = 68;
            this.lbl_ZoomFactor.Text = "?";
            // 
            // cb_Symmetry
            // 
            this.cb_Symmetry.BackColor = System.Drawing.Color.Black;
            this.cb_Symmetry.Checked = true;
            this.cb_Symmetry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Symmetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Symmetry.ForeColor = System.Drawing.SystemColors.Window;
            this.cb_Symmetry.Location = new System.Drawing.Point(677, 4);
            this.cb_Symmetry.Name = "cb_Symmetry";
            this.cb_Symmetry.Size = new System.Drawing.Size(148, 29);
            this.cb_Symmetry.TabIndex = 65;
            this.cb_Symmetry.Text = "keep symmetry";
            this.cb_Symmetry.UseVisualStyleBackColor = false;
            this.cb_Symmetry.CheckedChanged += new System.EventHandler(this.cb_Symmetry_CheckedChanged);
            // 
            // tb_ZoomFactor
            // 
            this.tb_ZoomFactor.BackColor = System.Drawing.Color.Black;
            this.tb_ZoomFactor.Location = new System.Drawing.Point(138, 61);
            this.tb_ZoomFactor.Name = "tb_ZoomFactor";
            this.tb_ZoomFactor.Size = new System.Drawing.Size(241, 45);
            this.tb_ZoomFactor.TabIndex = 64;
            this.tb_ZoomFactor.Scroll += new System.EventHandler(this.tb_ZoomFactor_Scroll);
            // 
            // tb_SpeedFactor
            // 
            this.tb_SpeedFactor.BackColor = System.Drawing.Color.Black;
            this.tb_SpeedFactor.Location = new System.Drawing.Point(63, 642);
            this.tb_SpeedFactor.Name = "tb_SpeedFactor";
            this.tb_SpeedFactor.Size = new System.Drawing.Size(220, 45);
            this.tb_SpeedFactor.TabIndex = 63;
            this.tb_SpeedFactor.Tag = "444";
            this.tb_SpeedFactor.Scroll += new System.EventHandler(this.tb_SpeedFactor_Scroll);
            // 
            // tb_Width
            // 
            this.tb_Width.BackColor = System.Drawing.Color.Black;
            this.tb_Width.Location = new System.Drawing.Point(621, 258);
            this.tb_Width.Name = "tb_Width";
            this.tb_Width.Size = new System.Drawing.Size(220, 45);
            this.tb_Width.TabIndex = 62;
            this.tb_Width.Scroll += new System.EventHandler(this.tb_Width_Scroll);
            this.tb_Width.MouseHover += new System.EventHandler(this.tb_Width_MouseHover);
            // 
            // tb_Height
            // 
            this.tb_Height.BackColor = System.Drawing.Color.Black;
            this.tb_Height.Enabled = false;
            this.tb_Height.Location = new System.Drawing.Point(578, 33);
            this.tb_Height.Name = "tb_Height";
            this.tb_Height.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tb_Height.Size = new System.Drawing.Size(45, 220);
            this.tb_Height.TabIndex = 61;
            this.tb_Height.Scroll += new System.EventHandler(this.tb_Height_Scroll);
            this.tb_Height.MouseHover += new System.EventHandler(this.tb_Height_MouseHover);
            // 
            // lbl_SpeedFactor
            // 
            this.lbl_SpeedFactor.BackColor = System.Drawing.Color.Black;
            this.lbl_SpeedFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SpeedFactor.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_SpeedFactor.Location = new System.Drawing.Point(133, 605);
            this.lbl_SpeedFactor.Name = "lbl_SpeedFactor";
            this.lbl_SpeedFactor.Size = new System.Drawing.Size(63, 34);
            this.lbl_SpeedFactor.TabIndex = 69;
            this.lbl_SpeedFactor.Text = "?";
            // 
            // lbl_Width
            // 
            this.lbl_Width.BackColor = System.Drawing.Color.Black;
            this.lbl_Width.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Width.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Width.Location = new System.Drawing.Point(835, 258);
            this.lbl_Width.Name = "lbl_Width";
            this.lbl_Width.Size = new System.Drawing.Size(64, 32);
            this.lbl_Width.TabIndex = 66;
            this.lbl_Width.Text = "?";
            // 
            // lbl_Height
            // 
            this.lbl_Height.BackColor = System.Drawing.Color.Black;
            this.lbl_Height.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Height.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Height.Location = new System.Drawing.Point(579, 4);
            this.lbl_Height.Name = "lbl_Height";
            this.lbl_Height.Size = new System.Drawing.Size(80, 26);
            this.lbl_Height.TabIndex = 67;
            this.lbl_Height.Text = "?";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(724, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 32);
            this.label3.TabIndex = 83;
            this.label3.Text = "x5";
            // 
            // HideMouseButton
            // 
            this.HideMouseButton.BackColor = System.Drawing.Color.White;
            this.HideMouseButton.Image = global::MagnifierSoftwareV_1.Properties.Resources.automatically_hide_mouse_cursor1;
            this.HideMouseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HideMouseButton.Location = new System.Drawing.Point(32, 133);
            this.HideMouseButton.Name = "HideMouseButton";
            this.HideMouseButton.Size = new System.Drawing.Size(100, 94);
            this.HideMouseButton.TabIndex = 81;
            this.HideMouseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HideMouseButton.UseVisualStyleBackColor = false;
            this.HideMouseButton.Click += new System.EventHandler(this.HideMouseButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MagnifierSoftwareV_1.Properties.Resources.Visually_Impaired_296x300;
            this.pictureBox1.Location = new System.Drawing.Point(32, 247);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 80;
            this.pictureBox1.TabStop = false;
            // 
            // Exit_button
            // 
            this.Exit_button.BackColor = System.Drawing.Color.DarkGray;
            this.Exit_button.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit_button.ForeColor = System.Drawing.Color.White;
            this.Exit_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_close_black_48dp;
            this.Exit_button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_button.Location = new System.Drawing.Point(749, 464);
            this.Exit_button.Margin = new System.Windows.Forms.Padding(2);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(102, 104);
            this.Exit_button.TabIndex = 79;
            this.Exit_button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_button.UseVisualStyleBackColor = false;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            this.Exit_button.MouseHover += new System.EventHandler(this.CloseButton_MouseHover);
            // 
            // traceBaarWidthButton
            // 
            this.traceBaarWidthButton.BackColor = System.Drawing.Color.White;
            this.traceBaarWidthButton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_remove_from_queue_black_48dp;
            this.traceBaarWidthButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.traceBaarWidthButton.Location = new System.Drawing.Point(631, 325);
            this.traceBaarWidthButton.Name = "traceBaarWidthButton";
            this.traceBaarWidthButton.Size = new System.Drawing.Size(102, 90);
            this.traceBaarWidthButton.TabIndex = 78;
            this.traceBaarWidthButton.UseVisualStyleBackColor = false;
            this.traceBaarWidthButton.Click += new System.EventHandler(this.traceBaarWidthButton_Click);
            this.traceBaarWidthButton.MouseHover += new System.EventHandler(this.traceBaarWidthButton_MouseOver);
            // 
            // traceBaarHightButton
            // 
            this.traceBaarHightButton.BackColor = System.Drawing.Color.White;
            this.traceBaarHightButton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_add_to_queue_black_48dp;
            this.traceBaarHightButton.Location = new System.Drawing.Point(749, 325);
            this.traceBaarHightButton.Name = "traceBaarHightButton";
            this.traceBaarHightButton.Size = new System.Drawing.Size(102, 90);
            this.traceBaarHightButton.TabIndex = 77;
            this.traceBaarHightButton.UseVisualStyleBackColor = false;
            this.traceBaarHightButton.Click += new System.EventHandler(this.traceBaarHightButton_Click);
            this.traceBaarHightButton.MouseHover += new System.EventHandler(this.traceBaarHightButton_mouseHover);
            // 
            // traceBaarSpeedDownbutton
            // 
            this.traceBaarSpeedDownbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarSpeedDownbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_more_black_48dp;
            this.traceBaarSpeedDownbutton.Location = new System.Drawing.Point(7, 642);
            this.traceBaarSpeedDownbutton.Name = "traceBaarSpeedDownbutton";
            this.traceBaarSpeedDownbutton.Size = new System.Drawing.Size(50, 38);
            this.traceBaarSpeedDownbutton.TabIndex = 76;
            this.traceBaarSpeedDownbutton.UseVisualStyleBackColor = false;
            this.traceBaarSpeedDownbutton.Click += new System.EventHandler(this.traceBaarSpeedbutton1_Click);
            this.traceBaarSpeedDownbutton.MouseHover += new System.EventHandler(this.SpeedDownButton_MouseHover);
            // 
            // traceBaarSpeedUpbutton
            // 
            this.traceBaarSpeedUpbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarSpeedUpbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_less_black_48dp;
            this.traceBaarSpeedUpbutton.Location = new System.Drawing.Point(289, 642);
            this.traceBaarSpeedUpbutton.Name = "traceBaarSpeedUpbutton";
            this.traceBaarSpeedUpbutton.Size = new System.Drawing.Size(52, 45);
            this.traceBaarSpeedUpbutton.TabIndex = 75;
            this.traceBaarSpeedUpbutton.UseVisualStyleBackColor = false;
            this.traceBaarSpeedUpbutton.Click += new System.EventHandler(this.traceBaarSpeedbutton2_Click);
            this.traceBaarSpeedUpbutton.MouseHover += new System.EventHandler(this.SpeedUpButton_MouseHover);
            // 
            // traceBaarZoomButton2
            // 
            this.traceBaarZoomButton2.BackColor = System.Drawing.Color.White;
            this.traceBaarZoomButton2.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_zoom_out_black_48dp;
            this.traceBaarZoomButton2.Location = new System.Drawing.Point(32, 26);
            this.traceBaarZoomButton2.Name = "traceBaarZoomButton2";
            this.traceBaarZoomButton2.Size = new System.Drawing.Size(100, 90);
            this.traceBaarZoomButton2.TabIndex = 74;
            this.traceBaarZoomButton2.UseVisualStyleBackColor = false;
            this.traceBaarZoomButton2.Click += new System.EventHandler(this.traceBaarZoomButton2_Click);
            this.traceBaarZoomButton2.MouseHover += new System.EventHandler(this.ZoomOutButton_MouseHover);
            // 
            // traceBaarZoomButton1
            // 
            this.traceBaarZoomButton1.BackColor = System.Drawing.Color.White;
            this.traceBaarZoomButton1.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_zoom_in_black_48dp;
            this.traceBaarZoomButton1.Location = new System.Drawing.Point(393, 26);
            this.traceBaarZoomButton1.Name = "traceBaarZoomButton1";
            this.traceBaarZoomButton1.Size = new System.Drawing.Size(102, 90);
            this.traceBaarZoomButton1.TabIndex = 73;
            this.traceBaarZoomButton1.UseVisualStyleBackColor = false;
            this.traceBaarZoomButton1.Click += new System.EventHandler(this.traceBaarZoomButton1_Click);
            this.traceBaarZoomButton1.MouseHover += new System.EventHandler(this.ZoomInButton_MouseHover);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(870, 582);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HideMouseButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Exit_button);
            this.Controls.Add(this.traceBaarWidthButton);
            this.Controls.Add(this.traceBaarHightButton);
            this.Controls.Add(this.traceBaarSpeedDownbutton);
            this.Controls.Add(this.traceBaarSpeedUpbutton);
            this.Controls.Add(this.traceBaarZoomButton2);
            this.Controls.Add(this.traceBaarZoomButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Color_Blindness_comboBox);
            this.Controls.Add(this.cb_HideMouseCursor);
            this.Controls.Add(this.lbl_ZoomFactor);
            this.Controls.Add(this.cb_Symmetry);
            this.Controls.Add(this.tb_ZoomFactor);
            this.Controls.Add(this.tb_SpeedFactor);
            this.Controls.Add(this.tb_Width);
            this.Controls.Add(this.tb_Height);
            this.Controls.Add(this.lbl_SpeedFactor);
            this.Controls.Add(this.lbl_Width);
            this.Controls.Add(this.lbl_Height);
            this.DoubleBuffered = true;
            this.Name = "ConfigurationForm";
            this.Text = "ConfigurationForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            ((System.ComponentModel.ISupportInitialize)(this.tb_ZoomFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SpeedFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button HideMouseButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.Button traceBaarWidthButton;
        private System.Windows.Forms.Button traceBaarHightButton;
        private System.Windows.Forms.Button traceBaarSpeedDownbutton;
        private System.Windows.Forms.Button traceBaarSpeedUpbutton;
        private System.Windows.Forms.Button traceBaarZoomButton2;
        private System.Windows.Forms.Button traceBaarZoomButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Color_Blindness_comboBox;
        private System.Windows.Forms.CheckBox cb_HideMouseCursor;
        private System.Windows.Forms.Label lbl_ZoomFactor;
        private System.Windows.Forms.CheckBox cb_Symmetry;
        private System.Windows.Forms.TrackBar tb_ZoomFactor;
        private System.Windows.Forms.TrackBar tb_SpeedFactor;
        private System.Windows.Forms.TrackBar tb_Width;
        private System.Windows.Forms.TrackBar tb_Height;
        private System.Windows.Forms.Label lbl_SpeedFactor;
        private System.Windows.Forms.Label lbl_Width;
        private System.Windows.Forms.Label lbl_Height;
        private System.Windows.Forms.Label label3;
    }
}