using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Tobii.Research;
using System.Windows.Forms;

namespace MagnifierSoftwareV_1.create_Profile
{
    public partial class FirstCalibrationWindow : Form
    {
        private static IEyeTracker mEyeTracker;
        static int calibrationBegin2 = 0;
        public FirstCalibrationWindow(IEyeTracker eyeTracker)
        {
            InitializeComponent();

            mEyeTracker = eyeTracker;

            Timer refreshTimer = new Timer();
            refreshTimer.Tick += new EventHandler(RefreshScreen);
            refreshTimer.Interval = 33;
            refreshTimer.Enabled = true;
            refreshTimer.Start();

            getGazeData(0.5f, 0.5f);

        }

        private void RefreshScreen(Object o, EventArgs e)
        {
            // this.Invalidate();
            Refresh();
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.02;
            }

            //else
          
        }

        private void getGazeData(float x, float y)
        {
            // Start listening to gaze data.
            mEyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
            // Wait for some data to be received.
            System.Threading.Thread.Sleep(2000);
            // Stop listening to gaze data.
            //mEyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
        }
        private static void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            if (e.LeftEye.GazePoint.PositionOnDisplayArea.X >= 0.49f && e.LeftEye.GazePoint.PositionOnDisplayArea.X <= 0.51f &&
                e.LeftEye.GazePoint.PositionOnDisplayArea.Y >= 0.49f && e.LeftEye.GazePoint.PositionOnDisplayArea.Y <= 0.51f)
            {
                Console.WriteLine(
              "Got gaze data with {0} left eye origin at point ({1}, {2}, ) in the user coordinate system.",
              e.LeftEye.GazePoint.Validity,
              e.LeftEye.GazePoint.PositionOnDisplayArea.X,
              e.LeftEye.GazePoint.PositionOnDisplayArea.Y
             );
                calibrationBegin2++;
                mEyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            RectangleF recBegin = new RectangleF(0.5f * w, 0.5f * h, 30, 30);
            e.Graphics.FillEllipse(Brushes.White, recBegin);

            if (calibrationBegin2 == 1)
            {
                e.Graphics.FillEllipse(Brushes.Yellow, recBegin);
                this.Close();
            }
        }
    }
}
