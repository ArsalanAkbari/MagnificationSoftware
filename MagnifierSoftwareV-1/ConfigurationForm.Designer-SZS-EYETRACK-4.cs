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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
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
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Setting = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.ChangeWIMcolor_button = new System.Windows.Forms.Button();
            this.button_SaveConfiguration = new System.Windows.Forms.Button();
            this.MiniMize_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.HideMouseButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(141, 692);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 30);
            this.label2.TabIndex = 82;
            this.label2.Text = "Hide Mouse Curser";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(138, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 30);
            this.label1.TabIndex = 72;
            this.label1.Text = "Select Color Blindness (CB)";
            // 
            // Color_Blindness_comboBox
            // 
            this.Color_Blindness_comboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Color_Blindness_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Color_Blindness_comboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Color_Blindness_comboBox.FormattingEnabled = true;
            this.Color_Blindness_comboBox.Items.AddRange(new object[] {
            "Normal",
            "Invert all Colors",
            "Achromatomaly-----Complete CB",
            "Achromatopsia------Complete CB",
            "Deuteranopia--------Red Green CB",
            "Deuteranomaly------Red Green CB",
            "Protanomaly---------Red Green CB",
            "Protanopia-----------Red Green CB",
            "Tritanomaly----------Blue Yellow  CB",
            "Tritanopia------------Blue Yellown CB"});
            this.Color_Blindness_comboBox.Location = new System.Drawing.Point(142, 456);
            this.Color_Blindness_comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.Color_Blindness_comboBox.MaxDropDownItems = 20;
            this.Color_Blindness_comboBox.Name = "Color_Blindness_comboBox";
            this.Color_Blindness_comboBox.Size = new System.Drawing.Size(353, 33);
            this.Color_Blindness_comboBox.TabIndex = 71;
            this.Color_Blindness_comboBox.SelectedIndexChanged += new System.EventHandler(this.Color_Blindness_comboBox_SelectedIndexChanged);
            // 
            // cb_HideMouseCursor
            // 
            this.cb_HideMouseCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.cb_HideMouseCursor.ForeColor = System.Drawing.SystemColors.Window;
            this.cb_HideMouseCursor.Location = new System.Drawing.Point(141, 725);
            this.cb_HideMouseCursor.Name = "cb_HideMouseCursor";
            this.cb_HideMouseCursor.Size = new System.Drawing.Size(164, 27);
            this.cb_HideMouseCursor.TabIndex = 70;
            this.cb_HideMouseCursor.Text = "Hide Mouse Cursor";
            this.cb_HideMouseCursor.CheckedChanged += new System.EventHandler(this.cb_HideMouseCursor_CheckedChanged);
            // 
            // lbl_ZoomFactor
            // 
            this.lbl_ZoomFactor.BackColor = System.Drawing.Color.Black;
            this.lbl_ZoomFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_ZoomFactor.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_ZoomFactor.Location = new System.Drawing.Point(247, 84);
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
            this.cb_Symmetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.cb_Symmetry.ForeColor = System.Drawing.SystemColors.Window;
            this.cb_Symmetry.Location = new System.Drawing.Point(644, 53);
            this.cb_Symmetry.Name = "cb_Symmetry";
            this.cb_Symmetry.Size = new System.Drawing.Size(197, 29);
            this.cb_Symmetry.TabIndex = 65;
            this.cb_Symmetry.Text = "keep symmetry";
            this.cb_Symmetry.UseVisualStyleBackColor = false;
            this.cb_Symmetry.CheckedChanged += new System.EventHandler(this.cb_Symmetry_CheckedChanged);
            // 
            // tb_ZoomFactor
            // 
            this.tb_ZoomFactor.BackColor = System.Drawing.Color.Black;
            this.tb_ZoomFactor.Location = new System.Drawing.Point(138, 114);
            this.tb_ZoomFactor.Name = "tb_ZoomFactor";
            this.tb_ZoomFactor.Size = new System.Drawing.Size(241, 45);
            this.tb_ZoomFactor.TabIndex = 64;
            this.tb_ZoomFactor.Scroll += new System.EventHandler(this.tb_ZoomFactor_Scroll);
            // 
            // tb_SpeedFactor
            // 
            this.tb_SpeedFactor.BackColor = System.Drawing.Color.Black;
            this.tb_SpeedFactor.Location = new System.Drawing.Point(138, 213);
            this.tb_SpeedFactor.Name = "tb_SpeedFactor";
            this.tb_SpeedFactor.Size = new System.Drawing.Size(241, 45);
            this.tb_SpeedFactor.TabIndex = 63;
            this.tb_SpeedFactor.Tag = "444";
            this.tb_SpeedFactor.Scroll += new System.EventHandler(this.tb_SpeedFactor_Scroll);
            // 
            // tb_Width
            // 
            this.tb_Width.BackColor = System.Drawing.Color.Black;
            this.tb_Width.Location = new System.Drawing.Point(621, 311);
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
            this.tb_Height.Location = new System.Drawing.Point(578, 86);
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
            this.lbl_SpeedFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_SpeedFactor.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_SpeedFactor.Location = new System.Drawing.Point(242, 185);
            this.lbl_SpeedFactor.Name = "lbl_SpeedFactor";
            this.lbl_SpeedFactor.Size = new System.Drawing.Size(63, 34);
            this.lbl_SpeedFactor.TabIndex = 69;
            this.lbl_SpeedFactor.Text = "?";
            // 
            // lbl_Width
            // 
            this.lbl_Width.BackColor = System.Drawing.Color.Black;
            this.lbl_Width.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Width.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Width.Location = new System.Drawing.Point(836, 311);
            this.lbl_Width.Name = "lbl_Width";
            this.lbl_Width.Size = new System.Drawing.Size(55, 32);
            this.lbl_Width.TabIndex = 66;
            this.lbl_Width.Text = "?";
            // 
            // lbl_Height
            // 
            this.lbl_Height.BackColor = System.Drawing.Color.Black;
            this.lbl_Height.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Height.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Height.Location = new System.Drawing.Point(579, 57);
            this.lbl_Height.Name = "lbl_Height";
            this.lbl_Height.Size = new System.Drawing.Size(80, 26);
            this.lbl_Height.TabIndex = 67;
            this.lbl_Height.Text = "?";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(724, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 32);
            this.label3.TabIndex = 83;
            this.label3.Text = "x5";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(138, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(291, 30);
            this.label4.TabIndex = 85;
            this.label4.Text = "Select The Eyetracking Mode";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
            "Both Eye + Head",
            "Both Eye",
            "Left Eye + Head",
            "Left Eye",
            "Right Eye + Head",
            "Right Eye",
            "Just Head"});
            this.comboBox1.Location = new System.Drawing.Point(142, 345);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.MaxDropDownItems = 20;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(353, 33);
            this.comboBox1.TabIndex = 84;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.Setting);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(885, 43);
            this.panel1.TabIndex = 89;
            // 
            // Setting
            // 
            this.Setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.Setting.ForeColor = System.Drawing.Color.Black;
            this.Setting.Location = new System.Drawing.Point(3, 8);
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(246, 30);
            this.Setting.TabIndex = 93;
            this.Setting.Text = "Setting";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(579, 384);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(239, 20);
            this.label5.TabIndex = 90;
            this.label5.Text = "Change the Magnifier\'s  Size";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.Window;
            this.label6.Location = new System.Drawing.Point(141, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 30);
            this.label6.TabIndex = 91;
            this.label6.Text = "Speed of Magnifier Window";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(144, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 30);
            this.label7.TabIndex = 92;
            this.label7.Text = "Zoom Faktor";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel2.Location = new System.Drawing.Point(-48, 646);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(979, 10);
            this.panel2.TabIndex = 94;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.SystemColors.Window;
            this.label8.Location = new System.Drawing.Point(141, 561);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(368, 30);
            this.label8.TabIndex = 96;
            this.label8.Text = "Change the Color of \"Where I Am\" point";
            // 
            // ChangeWIMcolor_button
            // 
            this.ChangeWIMcolor_button.BackColor = System.Drawing.Color.White;
            this.ChangeWIMcolor_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ChangeWIMcolor_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.paint_palette__1_;
            this.ChangeWIMcolor_button.Location = new System.Drawing.Point(32, 525);
            this.ChangeWIMcolor_button.Name = "ChangeWIMcolor_button";
            this.ChangeWIMcolor_button.Size = new System.Drawing.Size(106, 101);
            this.ChangeWIMcolor_button.TabIndex = 95;
            this.ChangeWIMcolor_button.UseVisualStyleBackColor = false;
            this.ChangeWIMcolor_button.Click += new System.EventHandler(this.ChangeWIMcolor_button_Click);
            // 
            // button_SaveConfiguration
            // 
            this.button_SaveConfiguration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_SaveConfiguration.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button_SaveConfiguration.FlatAppearance.BorderSize = 3;
            this.button_SaveConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SaveConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.button_SaveConfiguration.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_save_black_48dp1;
            this.button_SaveConfiguration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_SaveConfiguration.Location = new System.Drawing.Point(764, 592);
            this.button_SaveConfiguration.Name = "button_SaveConfiguration";
            this.button_SaveConfiguration.Size = new System.Drawing.Size(106, 42);
            this.button_SaveConfiguration.TabIndex = 93;
            this.button_SaveConfiguration.Text = "Save";
            this.button_SaveConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_SaveConfiguration.UseVisualStyleBackColor = false;
            this.button_SaveConfiguration.Click += new System.EventHandler(this.button_SaveConfiguration_Click);
            // 
            // MiniMize_button
            // 
            this.MiniMize_button.BackColor = System.Drawing.SystemColors.Window;
            this.MiniMize_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.MiniMize_button.FlatAppearance.BorderSize = 3;
            this.MiniMize_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiniMize_button.Font = new System.Drawing.Font("Bodoni MT Condensed", 13.68F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiniMize_button.ForeColor = System.Drawing.Color.Transparent;
            this.MiniMize_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.minimize;
            this.MiniMize_button.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.MiniMize_button.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MiniMize_button.Location = new System.Drawing.Point(792, 0);
            this.MiniMize_button.Margin = new System.Windows.Forms.Padding(2);
            this.MiniMize_button.Name = "MiniMize_button";
            this.MiniMize_button.Size = new System.Drawing.Size(41, 41);
            this.MiniMize_button.TabIndex = 88;
            this.MiniMize_button.UseVisualStyleBackColor = false;
            this.MiniMize_button.Click += new System.EventHandler(this.MiniMize_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.BackColor = System.Drawing.SystemColors.Window;
            this.Close_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.Close_button.FlatAppearance.BorderSize = 3;
            this.Close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close_button.Font = new System.Drawing.Font("Bodoni MT Condensed", 13.68F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close_button.ForeColor = System.Drawing.Color.Transparent;
            this.Close_button.Image = global::MagnifierSoftwareV_1.Properties.Resources.delete1;
            this.Close_button.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Close_button.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Close_button.Location = new System.Drawing.Point(833, 0);
            this.Close_button.Margin = new System.Windows.Forms.Padding(2);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(41, 41);
            this.Close_button.TabIndex = 87;
            this.Close_button.UseVisualStyleBackColor = false;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(32, 302);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(106, 100);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 86;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            // 
            // HideMouseButton
            // 
            this.HideMouseButton.BackColor = System.Drawing.Color.White;
            this.HideMouseButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.HideMouseButton.FlatAppearance.BorderSize = 3;
            this.HideMouseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HideMouseButton.Image = global::MagnifierSoftwareV_1.Properties.Resources.cursor;
            this.HideMouseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HideMouseButton.Location = new System.Drawing.Point(32, 664);
            this.HideMouseButton.Name = "HideMouseButton";
            this.HideMouseButton.Size = new System.Drawing.Size(106, 100);
            this.HideMouseButton.TabIndex = 81;
            this.HideMouseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HideMouseButton.UseVisualStyleBackColor = false;
            this.HideMouseButton.Click += new System.EventHandler(this.HideMouseButton_Click);
            this.HideMouseButton.MouseLeave += new System.EventHandler(this.HideMouseButton_MouseLeave);
            this.HideMouseButton.MouseHover += new System.EventHandler(this.HideMouseButton_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MagnifierSoftwareV_1.Properties.Resources.Visually_Impaired_296x300;
            this.pictureBox1.Location = new System.Drawing.Point(32, 413);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(106, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 80;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // traceBaarWidthButton
            // 
            this.traceBaarWidthButton.BackColor = System.Drawing.Color.White;
            this.traceBaarWidthButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarWidthButton.FlatAppearance.BorderSize = 3;
            this.traceBaarWidthButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.traceBaarWidthButton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_remove_from_queue_black_48dp;
            this.traceBaarWidthButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.traceBaarWidthButton.Location = new System.Drawing.Point(582, 413);
            this.traceBaarWidthButton.Name = "traceBaarWidthButton";
            this.traceBaarWidthButton.Size = new System.Drawing.Size(106, 100);
            this.traceBaarWidthButton.TabIndex = 78;
            this.traceBaarWidthButton.UseVisualStyleBackColor = false;
            this.traceBaarWidthButton.Click += new System.EventHandler(this.traceBaarWidthButton_Click);
            this.traceBaarWidthButton.MouseLeave += new System.EventHandler(this.traceBaarWidthButton_MouseLeave);
            this.traceBaarWidthButton.MouseHover += new System.EventHandler(this.traceBaarWidthButton_MouseOver);
            // 
            // traceBaarHightButton
            // 
            this.traceBaarHightButton.BackColor = System.Drawing.Color.White;
            this.traceBaarHightButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarHightButton.FlatAppearance.BorderSize = 3;
            this.traceBaarHightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.traceBaarHightButton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_add_to_queue_black_48dp;
            this.traceBaarHightButton.Location = new System.Drawing.Point(749, 413);
            this.traceBaarHightButton.Name = "traceBaarHightButton";
            this.traceBaarHightButton.Size = new System.Drawing.Size(106, 100);
            this.traceBaarHightButton.TabIndex = 77;
            this.traceBaarHightButton.UseVisualStyleBackColor = false;
            this.traceBaarHightButton.Click += new System.EventHandler(this.traceBaarHightButton_Click);
            this.traceBaarHightButton.MouseLeave += new System.EventHandler(this.traceBaarHightButton_mouseLeave);
            this.traceBaarHightButton.MouseHover += new System.EventHandler(this.traceBaarHightButton_mouseHover);
            // 
            // traceBaarSpeedDownbutton
            // 
            this.traceBaarSpeedDownbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarSpeedDownbutton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarSpeedDownbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_more_black_48dp;
            this.traceBaarSpeedDownbutton.Location = new System.Drawing.Point(32, 190);
            this.traceBaarSpeedDownbutton.Name = "traceBaarSpeedDownbutton";
            this.traceBaarSpeedDownbutton.Size = new System.Drawing.Size(106, 101);
            this.traceBaarSpeedDownbutton.TabIndex = 76;
            this.traceBaarSpeedDownbutton.UseVisualStyleBackColor = false;
            this.traceBaarSpeedDownbutton.Click += new System.EventHandler(this.traceBaarSpeedbutton1_Click);
            this.traceBaarSpeedDownbutton.MouseHover += new System.EventHandler(this.SpeedDownButton_MouseHover);
            // 
            // traceBaarSpeedUpbutton
            // 
            this.traceBaarSpeedUpbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarSpeedUpbutton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarSpeedUpbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_less_black_48dp;
            this.traceBaarSpeedUpbutton.Location = new System.Drawing.Point(393, 190);
            this.traceBaarSpeedUpbutton.Name = "traceBaarSpeedUpbutton";
            this.traceBaarSpeedUpbutton.Size = new System.Drawing.Size(106, 101);
            this.traceBaarSpeedUpbutton.TabIndex = 75;
            this.traceBaarSpeedUpbutton.UseVisualStyleBackColor = false;
            this.traceBaarSpeedUpbutton.Click += new System.EventHandler(this.traceBaarSpeedbutton2_Click);
            this.traceBaarSpeedUpbutton.MouseHover += new System.EventHandler(this.SpeedUpButton_MouseHover);
            // 
            // traceBaarZoomButton2
            // 
            this.traceBaarZoomButton2.BackColor = System.Drawing.Color.White;
            this.traceBaarZoomButton2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarZoomButton2.FlatAppearance.BorderSize = 3;
            this.traceBaarZoomButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.traceBaarZoomButton2.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_zoom_out_black_48dp;
            this.traceBaarZoomButton2.Location = new System.Drawing.Point(32, 79);
            this.traceBaarZoomButton2.Name = "traceBaarZoomButton2";
            this.traceBaarZoomButton2.Size = new System.Drawing.Size(106, 100);
            this.traceBaarZoomButton2.TabIndex = 74;
            this.traceBaarZoomButton2.UseVisualStyleBackColor = false;
            this.traceBaarZoomButton2.Click += new System.EventHandler(this.traceBaarZoomButton2_Click);
            this.traceBaarZoomButton2.MouseLeave += new System.EventHandler(this.ZoomOutButton_MouseLeave);
            this.traceBaarZoomButton2.MouseHover += new System.EventHandler(this.ZoomOutButton_MouseHover);
            // 
            // traceBaarZoomButton1
            // 
            this.traceBaarZoomButton1.BackColor = System.Drawing.Color.White;
            this.traceBaarZoomButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarZoomButton1.FlatAppearance.BorderSize = 3;
            this.traceBaarZoomButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.traceBaarZoomButton1.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_zoom_in_black_48dp;
            this.traceBaarZoomButton1.Location = new System.Drawing.Point(393, 79);
            this.traceBaarZoomButton1.Name = "traceBaarZoomButton1";
            this.traceBaarZoomButton1.Size = new System.Drawing.Size(106, 100);
            this.traceBaarZoomButton1.TabIndex = 73;
            this.traceBaarZoomButton1.UseVisualStyleBackColor = false;
            this.traceBaarZoomButton1.Click += new System.EventHandler(this.traceBaarZoomButton1_Click);
            this.traceBaarZoomButton1.MouseLeave += new System.EventHandler(this.ZoomInButton_MouseLeav);
            this.traceBaarZoomButton1.MouseHover += new System.EventHandler(this.ZoomInButton_MouseHover);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(882, 656);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ChangeWIMcolor_button);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button_SaveConfiguration);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MiniMize_button);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HideMouseButton);
            this.Controls.Add(this.pictureBox1);
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
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "ConfigurationForm";
            this.Text = "ConfigurationForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            ((System.ComponentModel.ISupportInitialize)(this.tb_ZoomFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SpeedFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Height)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button HideMouseButton;
        private System.Windows.Forms.PictureBox pictureBox1;
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
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button MiniMize_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Setting;
        private System.Windows.Forms.Button button_SaveConfiguration;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChangeWIMcolor_button;
        private System.Windows.Forms.Label label8;
    }
}