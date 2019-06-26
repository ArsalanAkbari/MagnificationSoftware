using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Linq;
using MagnifierSoftwareV_1.EyeMove;
using MagnifierSoftwareV_1.MouseMove;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using Tobii.Research;
using System.Windows.Forms;

namespace MagnifierSoftwareV_1.Calibration
{
    public partial class CalibrationTobiiInformations : Form
    {

        private Configuration mConfiguration;

        private Timer refreshTimer;

        private string mConfigFileName = "configData.xml";


        public CalibrationTobiiInformations(Configuration configuration)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            this.Cursor = NativeMethods.LoadCustomCursor();
          
            mConfiguration = configuration;
            mConfiguration.CalibrationColor = Color.Red;
            //TopMost = true;

            lbl_PointSize.Text = mConfiguration.calibrationPointSize.ToString();
            tb_calibrationPointSize.Value = mConfiguration.calibrationPointSize;


            refreshTimer = new Timer();
            refreshTimer.Tick += new EventHandler(RefreshScreen);
            refreshTimer.Interval = 33;
            refreshTimer.Start();

        }

        private void RefreshScreen(object sender, EventArgs e)
        {
            this.Invalidate();
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

        private void ChooseColor_button_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.SolidColorOnly = false;
            MyDialog.AnyColor = false;
            

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                //mConfiguration.CalibrationColor1 = MyDialog.Color.ToKnownColor().ToString();
                mConfiguration.CalibrationColor = MyDialog.Color;
                Console.WriteLine(MyDialog.Color.Name);
                //chooseColor_button.BackColor = mConfiguration.CalibrationColor;

            }

        }

        private void CalibrationTobiiInformations_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush newBrush = new SolidBrush(mConfiguration.CalibrationColor); 
            int radius = tb_calibrationPointSize.Value / 2;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(newBrush, 200, 250, tb_calibrationPointSize.Value, tb_calibrationPointSize.Value);
            //e.Graphics.FillEllipse(Brushes.Black, 130 + radius - 3, 200 + radius - 3, 6, 6);
        }

        private void Tb_calibrationPointSize_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.calibrationPointSize = tb.Value;
            lbl_PointSize.Text = tb_calibrationPointSize.Value.ToString();
        }

        private void TraceBaarSensitivityUpbutton_Click(object sender, EventArgs e)
        {
            if (mConfiguration.calibrationPointSize != 200 && tb_calibrationPointSize.Value + 2<200)
            {
                tb_calibrationPointSize.Value += 1;
                mConfiguration.calibrationPointSize = tb_calibrationPointSize.Value + 2;
                tb_calibrationPointSize.Text = tb_calibrationPointSize.Value.ToString();
                lbl_PointSize.Text = tb_calibrationPointSize.Value.ToString();
            }
        }

        private void TraceBaarSensitivityDownbutton_Click(object sender, EventArgs e)
        {
            if (mConfiguration.calibrationPointSize != 150 && tb_calibrationPointSize.Value - 2 > 150)
            {
                tb_calibrationPointSize.Value -= 1;
                mConfiguration.calibrationPointSize = tb_calibrationPointSize.Value - 2;
                tb_calibrationPointSize.Text = tb_calibrationPointSize.Value.ToString();
                lbl_PointSize.Text = tb_calibrationPointSize.Value.ToString();
            }
        }

        private void StartCalibration_button_Click(object sender, EventArgs e)
        {
            var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();
            // EyeTrackerCollection eyeTrackers = EyeTrackingOperations.FindAllEyeTrackers();

            if (eyeTracker != null)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                     eyeTracker.Address,
                     eyeTracker.DeviceName,
                     eyeTracker.Model,
                     eyeTracker.SerialNumber,
                     eyeTracker.FirmwareVersion,
                     eyeTracker.RuntimeVersion);


                if (eyeTracker != null)
                {
                    if (BothEye_checkBox.Checked)
                    {
                        CalibrationTobii t = new CalibrationTobii(eyeTracker,mConfiguration, "BothEyes");
                        t.Show();

                    }

                    else if (RightEye_checkBox.Checked)
                    {
                        CalibrationTobii t = new CalibrationTobii(eyeTracker, mConfiguration, "RightEye");
                        t.Show();
                    }

                    else if (LeftEye_checkBox.Checked)
                    {
                        CalibrationTobii t = new CalibrationTobii(eyeTracker, mConfiguration, "LeftEye");
                        t.Show();
                    }
                   
                }

            }
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            this.Close();
        }

        private void MiniMize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }




        const int HTCAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void LeftEye_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cb.ForeColor = Color.Red;
                BothEye_checkBox.Checked = false;
                BothEye_checkBox.ForeColor = Color.White;

                RightEye_checkBox.Checked = false;
                RightEye_checkBox.ForeColor = Color.White;

            }
        }

        private void RightEye_checkBox_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cb.ForeColor = Color.Red;

                LeftEye_checkBox.Checked = false;
                LeftEye_checkBox.ForeColor = Color.White;

                BothEye_checkBox.Checked = false;
                BothEye_checkBox.ForeColor = Color.White;

            }
        }

        private void BothEye_checkBox_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cb.ForeColor = Color.Red;
                LeftEye_checkBox.Checked = false;
                LeftEye_checkBox.ForeColor = Color.White;
                RightEye_checkBox.Checked = false;
                RightEye_checkBox.ForeColor = Color.White;

            }

        }
    }
}
