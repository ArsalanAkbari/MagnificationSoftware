using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Tobii.Research;
using System.Windows.Forms;

namespace MagnifierSoftwareV_1.EyeMove.Calibration
{
    public partial class CalibrationForm : Form
    {

        private static IEyeTracker mEyeTracker;
        public static bool calibrationBegin = false;
        static int calibrationBegin2 = 0;

        public CalibrationForm(IEyeTracker eyeTracker)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(HandleEsc);
            mEyeTracker = eyeTracker;
        
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            FormBorderStyle = FormBorderStyle.None;


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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;


            //label1.ResetText = "Look at each dot until its color changes!";
            //label1.ForeColor = Color.White;

            //  label1.Text = "Look at each dot until its color changes!";
            RectangleF recBegin = new RectangleF(0.5f * w, 0.5f * h, 30, 30);
            e.Graphics.FillEllipse(Brushes.White, recBegin);

            /*RectangleF rec2 = new RectangleF(0.1f * w, 0.1f * h, 20, 20);
            e.Graphics.FillEllipse(Brushes.Black, rec2);

            RectangleF rec3 = new RectangleF(0.1f * w, 0.9f * h, 20, 20);
            e.Graphics.FillEllipse(Brushes.Black, rec3);

            RectangleF rec4 = new RectangleF(0.9f * w, 0.1f * h, 20, 20);
            e.Graphics.FillEllipse(Brushes.Black, rec4);

            RectangleF rec5 = new RectangleF(0.9f * w, 0.9f * h, 20, 20);
            e.Graphics.FillEllipse(Brushes.Black, rec5);*/

            if (calibrationBegin2 == 1)
            {

                //   label1.Text = "";
                e.Graphics.FillEllipse(Brushes.Yellow, recBegin);

                var calibration = new ScreenBasedCalibration(mEyeTracker);

                // Define the points on screen we should calibrate at.
                // The coordinates are normalized, i.e. (0.0f, 0.0f) is the upper left corner and (1.0f, 1.0f) is the lower right corner.
                 var pta = new PointF[5];

                  var pointsToCalibrate = new NormalizedPoint2D[5] {
                        new NormalizedPoint2D(0.5f, 0.5f),
                        new NormalizedPoint2D(0.1f, 0.1f),
                        new NormalizedPoint2D(0.1f, 0.9f),
                        new NormalizedPoint2D(0.9f, 0.1f),
                        new NormalizedPoint2D(0.9f, 0.9f),
                    };

                  // Enter calibration mode.
                 calibration.EnterCalibrationMode();

                  foreach (var point in pointsToCalibrate)
                  {
                      // Show an image on screen where you want to calibrate.
                      Console.WriteLine("Show point on screen at ({0}, {1})", point.X, point.Y);


                      RectangleF rec = new RectangleF(point.X * w, point.Y * h, 30, 30);
                      e.Graphics.FillEllipse(Brushes.White, rec);

                      // Collect data.,,
                      CalibrationStatus status = calibration.CollectData(point);
                      // Wait a little for user to focus.
                      System.Threading.Thread.Sleep(700);

                      if (status != CalibrationStatus.Success)
                      {
                          // Try again if it didn't go well the first time.
                          // Not all eye tracker models will fail at this point, but instead fail on ComputeAndApply.
                          //vaghti cheshmo nabine ghermez mishe
                          calibration.CollectData(point);
                          e.Graphics.FillEllipse(Brushes.Red, rec);
                      }

                      if (status == CalibrationStatus.Success)
                      {
                          e.Graphics.FillEllipse(Brushes.Black, rec);
                      }
                  }
                  // Compute and apply the calibration.
                  CalibrationResult calibrationResult = calibration.ComputeAndApply();
                  Console.WriteLine("Compute and apply returned {0} and collected at {1} points.", calibrationResult.Status, calibrationResult.CalibrationPoints.Count);

                  //textBox1.Text += "Compute and apply returned" + calibrationResult.Status+" and collected at " + calibrationResult.CalibrationPoints.Count+" points." +"\n";

                  // Analyze the data and maybe remove points that weren't good.
                  calibration.DiscardData(new NormalizedPoint2D(0.1f, 0.1f));
                  // Redo collection at the discarded point.
                  Console.WriteLine("Show point on screen at ({0}, {1})", 0.1f, 0.1f);
                  calibration.CollectData(new NormalizedPoint2D(0.1f, 0.1f));

                  RectangleF rec2 = new RectangleF(0.1f * w, 0.1f * h, 30, 30);
                  e.Graphics.FillEllipse(Brushes.Yellow, rec2);

                  // Compute and apply again.
                  calibrationResult = calibration.ComputeAndApply();

                  Console.WriteLine("Second compute and apply returned {0} and collected at {1} points.",
                     calibrationResult.Status, calibrationResult.CalibrationPoints.Count);
                  // textBox1.Text += "Compute and apply returned" + calibrationResult.Status + " and collected at " + calibrationResult.CalibrationPoints.Count + " points." + "\n";
                  // See that you're happy with the result.
                  // The calibration is done. Leave calibration mode.
                  calibration.LeaveCalibrationMode();

                calibrationBegin2 = 0;
                this.Close();
            }

            // calibrationBegin = false;


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


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

        }
    }
}
