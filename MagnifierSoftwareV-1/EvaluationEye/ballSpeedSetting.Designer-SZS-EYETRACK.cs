namespace MagnifierSoftwareV_1.EvaluationEye
{
    partial class ballSpeedSetting
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
            this.label6 = new System.Windows.Forms.Label();
            this.tb_DelayFactor = new System.Windows.Forms.TrackBar();
            this.lbl_DelayFactor = new System.Windows.Forms.Label();
            this.label_Save = new System.Windows.Forms.Label();
            this.label_FirstInfo = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_ShowDelay = new System.Windows.Forms.Button();
            this.traceBaarDelayDownbutton = new System.Windows.Forms.Button();
            this.traceBaarDelayUpbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DelayFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(113, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 30);
            this.label6.TabIndex = 96;
            this.label6.Text = "Speed Of Testing Ball";
            // 
            // tb_DelayFactor
            // 
            this.tb_DelayFactor.BackColor = System.Drawing.Color.Black;
            this.tb_DelayFactor.Location = new System.Drawing.Point(84, 83);
            this.tb_DelayFactor.Maximum = 15;
            this.tb_DelayFactor.Minimum = 1;
            this.tb_DelayFactor.Name = "tb_DelayFactor";
            this.tb_DelayFactor.Size = new System.Drawing.Size(241, 45);
            this.tb_DelayFactor.TabIndex = 92;
            this.tb_DelayFactor.Tag = "444";
            this.tb_DelayFactor.Value = 1;
            this.tb_DelayFactor.Scroll += new System.EventHandler(this.tb_SpeedFactor_Scroll);
            // 
            // lbl_DelayFactor
            // 
            this.lbl_DelayFactor.BackColor = System.Drawing.Color.Black;
            this.lbl_DelayFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_DelayFactor.ForeColor = System.Drawing.Color.White;
            this.lbl_DelayFactor.Location = new System.Drawing.Point(194, 57);
            this.lbl_DelayFactor.Name = "lbl_DelayFactor";
            this.lbl_DelayFactor.Size = new System.Drawing.Size(63, 34);
            this.lbl_DelayFactor.TabIndex = 93;
            this.lbl_DelayFactor.Text = "?";
            // 
            // label_Save
            // 
            this.label_Save.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Save.AutoSize = true;
            this.label_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label_Save.ForeColor = System.Drawing.Color.White;
            this.label_Save.Location = new System.Drawing.Point(154, 9);
            this.label_Save.Name = "label_Save";
            this.label_Save.Size = new System.Drawing.Size(691, 31);
            this.label_Save.TabIndex = 100;
            this.label_Save.Text = "If you happy with the Ball Speed Press Save Button.";
            this.label_Save.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_FirstInfo
            // 
            this.label_FirstInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_FirstInfo.AutoSize = true;
            this.label_FirstInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label_FirstInfo.ForeColor = System.Drawing.Color.White;
            this.label_FirstInfo.Location = new System.Drawing.Point(13, 9);
            this.label_FirstInfo.Name = "label_FirstInfo";
            this.label_FirstInfo.Size = new System.Drawing.Size(897, 31);
            this.label_FirstInfo.TabIndex = 101;
            this.label_FirstInfo.Text = "Please select the desired Speed and Click on \"Show Speed\" Button!";
            this.label_FirstInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button_Save
            // 
            this.button_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.button_Save.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_save_black_48dp1;
            this.button_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Save.Location = new System.Drawing.Point(9, 228);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(97, 35);
            this.button_Save.TabIndex = 98;
            this.button_Save.Text = "Save";
            this.button_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Save.UseVisualStyleBackColor = false;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_ShowDelay
            // 
            this.button_ShowDelay.BackColor = System.Drawing.Color.White;
            this.button_ShowDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.button_ShowDelay.Image = global::MagnifierSoftwareV_1.Properties.Resources.speedometer;
            this.button_ShowDelay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ShowDelay.Location = new System.Drawing.Point(9, 181);
            this.button_ShowDelay.Name = "button_ShowDelay";
            this.button_ShowDelay.Size = new System.Drawing.Size(181, 35);
            this.button_ShowDelay.TabIndex = 97;
            this.button_ShowDelay.Text = "Show The Speed";
            this.button_ShowDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_ShowDelay.UseVisualStyleBackColor = false;
            this.button_ShowDelay.Click += new System.EventHandler(this.button1_Click);
            // 
            // traceBaarDelayDownbutton
            // 
            this.traceBaarDelayDownbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarDelayDownbutton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarDelayDownbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_more_black_48dp;
            this.traceBaarDelayDownbutton.Location = new System.Drawing.Point(9, 83);
            this.traceBaarDelayDownbutton.Name = "traceBaarDelayDownbutton";
            this.traceBaarDelayDownbutton.Size = new System.Drawing.Size(69, 67);
            this.traceBaarDelayDownbutton.TabIndex = 95;
            this.traceBaarDelayDownbutton.UseVisualStyleBackColor = false;
            this.traceBaarDelayDownbutton.Click += new System.EventHandler(this.traceBaarDelayDownbutton_Click);
            // 
            // traceBaarDelayUpbutton
            // 
            this.traceBaarDelayUpbutton.BackColor = System.Drawing.Color.White;
            this.traceBaarDelayUpbutton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.traceBaarDelayUpbutton.Image = global::MagnifierSoftwareV_1.Properties.Resources.ic_expand_less_black_48dp;
            this.traceBaarDelayUpbutton.Location = new System.Drawing.Point(331, 83);
            this.traceBaarDelayUpbutton.Name = "traceBaarDelayUpbutton";
            this.traceBaarDelayUpbutton.Size = new System.Drawing.Size(69, 67);
            this.traceBaarDelayUpbutton.TabIndex = 94;
            this.traceBaarDelayUpbutton.UseVisualStyleBackColor = false;
            this.traceBaarDelayUpbutton.Click += new System.EventHandler(this.traceBaarDelayUpbutton_Click);
            // 
            // ballSpeedSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(922, 486);
            this.Controls.Add(this.label_FirstInfo);
            this.Controls.Add(this.label_Save);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_ShowDelay);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.traceBaarDelayDownbutton);
            this.Controls.Add(this.traceBaarDelayUpbutton);
            this.Controls.Add(this.tb_DelayFactor);
            this.Controls.Add(this.lbl_DelayFactor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ballSpeedSetting";
            this.Text = "ballSpeedSetting";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            ((System.ComponentModel.ISupportInitialize)(this.tb_DelayFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button traceBaarDelayDownbutton;
        private System.Windows.Forms.Button traceBaarDelayUpbutton;
        private System.Windows.Forms.TrackBar tb_DelayFactor;
        private System.Windows.Forms.Label lbl_DelayFactor;
        private System.Windows.Forms.Button button_ShowDelay;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Label label_Save;
        private System.Windows.Forms.Label label_FirstInfo;
    }
}