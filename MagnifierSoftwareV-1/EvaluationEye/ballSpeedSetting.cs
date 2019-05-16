using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Reflection;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using System.Drawing.Drawing2D;


namespace MagnifierSoftwareV_1.EvaluationEye
{
    public partial class ballSpeedSetting : Form
    {
        private string mConfigFileName = "configData.xml";
        Configuration mConfiguration;
        private int mBallSize; // related to sensitivity

        public ballSpeedSetting(Configuration configuration , int ballSize)
        {
            InitializeComponent();
            this.Cursor = NativeMethods.LoadCustomCursor();

            mConfiguration = configuration;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;

            mBallSize = ballSize;

            tb_DelayFactor.Value = mConfiguration.testingBallDelay;
            lbl_DelayFactor.Text = (16-mConfiguration.testingBallDelay).ToString();

        }


        bool showTest = false;
        private void onPaint(object sender, PaintEventArgs e)
        {
            if(showTest == true)
            {
                label_FirstInfo.Hide();
                label_Save.Hide();

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                Font font = new Font(FontFamily.GenericSansSerif, 19, FontStyle.Bold);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                for (int i = this.Width * 1 / 4; i <= this.Width * 3 / 4; i++)
                {
                    e.Graphics.DrawString("Please Wait until the Line is Complete!", font, Brushes.White, ClientRectangle , format);
                    e.Graphics.FillEllipse(Brushes.Red, i, this.Height * 1 / 2 + 20, mBallSize, mBallSize);
                    System.Threading.Thread.Sleep(mConfiguration.testingBallDelay);
                }
                e.Graphics.Clear(Color.Black);
                label_Save.Show();
                showTest = false;
            }
           
        }

        private void tb_SpeedFactor_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.testingBallDelay =  tb.Value;
            lbl_DelayFactor.Text = (16 - tb_DelayFactor.Value).ToString();
        }

        private void traceBaarDelayUpbutton_Click(object sender, EventArgs e)
        {
            if (mConfiguration.testingBallDelay != 15)
            {
                tb_DelayFactor.Value++;
                mConfiguration.testingBallDelay = tb_DelayFactor.Value +1;
                lbl_DelayFactor.Text = (16-tb_DelayFactor.Value).ToString();
            }

        }

        private void traceBaarDelayDownbutton_Click(object sender, EventArgs e)
        {
            if (mConfiguration.testingBallDelay != 1)
            {
                tb_DelayFactor.Value--;
                mConfiguration.testingBallDelay = tb_DelayFactor.Value -1;
                lbl_DelayFactor.Text = (16 - tb_DelayFactor.Value).ToString();
            }
        }

        private void SaveConfiguration()
        {
            try
            {
                XmlUtility.Serialize(mConfiguration, mConfigFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Serialization problem: " + e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showTest = true;
            this.Invalidate();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            this.Close();
        }

    }
}
