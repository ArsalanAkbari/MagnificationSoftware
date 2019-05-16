namespace MagnifierSoftwareV_1.EyeMove
{
    partial class Main
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
            this.gazeTracker = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SensitivityInput = new System.Windows.Forms.TrackBar();
            this.QuitButton = new System.Windows.Forms.Button();
            this.HeadRotationLabel = new System.Windows.Forms.Label();
            this.PositionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ContinuousButton = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.ModeBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityInput)).BeginInit();
            this.SuspendLayout();
            // 
            // gazeTracker
            // 
            this.gazeTracker.AutoSize = true;
            this.gazeTracker.Location = new System.Drawing.Point(112, 67);
            this.gazeTracker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gazeTracker.Name = "gazeTracker";
            this.gazeTracker.Size = new System.Drawing.Size(121, 17);
            this.gazeTracker.TabIndex = 45;
            this.gazeTracker.Text = "Show Gaze Tracker";
            this.gazeTracker.UseVisualStyleBackColor = true;
            this.gazeTracker.CheckedChanged += new System.EventHandler(this.gazeTracker_CheckedChanged_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 133);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Sensitivity";
            // 
            // SensitivityInput
            // 
            this.SensitivityInput.Location = new System.Drawing.Point(67, 126);
            this.SensitivityInput.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SensitivityInput.Maximum = 20;
            this.SensitivityInput.Name = "SensitivityInput";
            this.SensitivityInput.Size = new System.Drawing.Size(201, 45);
            this.SensitivityInput.TabIndex = 43;
            this.SensitivityInput.Scroll += new System.EventHandler(this.SensitivityInput_Scroll);
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(7, 258);
            this.QuitButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(56, 36);
            this.QuitButton.TabIndex = 42;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // HeadRotationLabel
            // 
            this.HeadRotationLabel.AutoSize = true;
            this.HeadRotationLabel.Location = new System.Drawing.Point(94, 212);
            this.HeadRotationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.HeadRotationLabel.Name = "HeadRotationLabel";
            this.HeadRotationLabel.Size = new System.Drawing.Size(31, 13);
            this.HeadRotationLabel.TabIndex = 41;
            this.HeadRotationLabel.Text = "(0, 0)";
            // 
            // PositionLabel
            // 
            this.PositionLabel.AutoSize = true;
            this.PositionLabel.Location = new System.Drawing.Point(94, 184);
            this.PositionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PositionLabel.Name = "PositionLabel";
            this.PositionLabel.Size = new System.Drawing.Size(31, 13);
            this.PositionLabel.TabIndex = 40;
            this.PositionLabel.Text = "(0, 0)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 212);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Head Rotation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 184);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Gaze Position";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Movement";
            // 
            // ContinuousButton
            // 
            this.ContinuousButton.AutoSize = true;
            this.ContinuousButton.Location = new System.Drawing.Point(190, 268);
            this.ContinuousButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ContinuousButton.Name = "ContinuousButton";
            this.ContinuousButton.Size = new System.Drawing.Size(78, 17);
            this.ContinuousButton.TabIndex = 36;
            this.ContinuousButton.Text = "Continuous";
            this.ContinuousButton.UseVisualStyleBackColor = true;
            this.ContinuousButton.CheckedChanged += new System.EventHandler(this.ContinuousButton_CheckedChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 19);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Tracker Mode";
            // 
            // ModeBox
            // 
            this.ModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeBox.FormattingEnabled = true;
            this.ModeBox.Items.AddRange(new object[] {
            "EyeX Only",
            "EyeX Only One Eye",
            "EyeX Only Head           "});
            this.ModeBox.Location = new System.Drawing.Point(112, 11);
            this.ModeBox.Margin = new System.Windows.Forms.Padding(2);
            this.ModeBox.Name = "ModeBox";
            this.ModeBox.Size = new System.Drawing.Size(154, 21);
            this.ModeBox.TabIndex = 47;
            this.ModeBox.SelectedIndexChanged += new System.EventHandler(this.ModeBox_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 303);
            this.Controls.Add(this.ModeBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gazeTracker);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SensitivityInput);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.HeadRotationLabel);
            this.Controls.Add(this.PositionLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ContinuousButton);
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox gazeTracker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar SensitivityInput;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.Label HeadRotationLabel;
        public System.Windows.Forms.Label PositionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton ContinuousButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ModeBox;
    }
}