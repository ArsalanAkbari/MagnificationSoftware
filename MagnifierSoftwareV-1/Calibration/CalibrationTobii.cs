using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagnifierSoftwareV_1.EyeMove.WarpPointers;
using MagnifierSoftwareV_1.EyeMove;
using Tobii.Research;

namespace MagnifierSoftwareV_1.Calibration
{



    public partial class CalibrationTobii : Form
    {

        private static IEyeTracker mEyeTracker;
        public static bool calibrationBegin = false;
        static int calibrationBegin2 = 0;

        private static BufferedGraphics bufferedGraphics;

        private String mCalibrationMode = "BothEyes";
        private combineEyes combineEyeGaze;
        private OneEyeRight oneEyeRight;
        private OneEyeLeft oneEyeLeft;
        private Configuration mConfiguration;
        private List<Point> gazePointList;
        private Timer timer = new Timer();

        bool justOneTime = false;

        public CalibrationTobii(IEyeTracker eyeTracker , Configuration configuration , String calibrationMode)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;
            mEyeTracker = eyeTracker;
            mConfiguration = configuration;
            combineEyeGaze = new combineEyes(mConfiguration);
            oneEyeRight = new OneEyeRight(mConfiguration);
            oneEyeLeft = new OneEyeLeft(mConfiguration);

            mCalibrationMode = calibrationMode;

            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            bufferedGraphics = context.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            FormBorderStyle = FormBorderStyle.None;

            timer.Enabled = true;
           // timer.Tick += new EventHandler(doCalibration);
            timer.Interval = 40; // 40 images per second.
            timer.Start();

        }

        bool gazeIsRight = false;


      
       


        private Point getGazePoint()
        {
            //return combineEyeGaze.GetGazePoint();

             if (mCalibrationMode == "BothEyes")
             {
                return combineEyeGaze.GetGazePoint();
             }

             else if (mCalibrationMode == "RightEye")
             {
                return  oneEyeRight.GetGazePoint();
             }

             else if (mCalibrationMode == "LeftEye")
             {
                return oneEyeLeft.GetGazePoint();
             }

            return combineEyeGaze.GetGazePoint();
        }



        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

        }

        private void CalibrationTobii_Paint(object sender, PaintEventArgs e)
        {
            if (!justOneTime)
            {
                justOneTime = true;
                gazePointList = new List<Point>();
                SolidBrush newBrush; // to have a new color that we choose

                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;


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
                    RectangleF rec = new RectangleF(point.X * w - mConfiguration.calibrationPointSize/2, point.Y * h - mConfiguration.calibrationPointSize/2, mConfiguration.calibrationPointSize, mConfiguration.calibrationPointSize);
                    //  RectangleF rec = new RectangleF(point.X * w - 70, point.Y * h - 70, 150, 150);

                    newBrush = new SolidBrush(mConfiguration.CalibrationColor);
                    e.Graphics.FillEllipse(newBrush, rec);

                    // Wait a little for user to focus.


                    // Collect data.,,

                    // Wait a little for user to focus.
                    // System.Threading.Thread.Sleep(700);

                    // getGazeData((int)point.X * w, (int)point.Y * h);

                    int X = (int)(point.X * w );// circle with r=70 ,
                    int Y = (int)(point.Y * h );

                    //compareGazeWithShape(X, Y);



                    CalibrationStatus status = CalibrationStatus.Failure;



                    while (status != CalibrationStatus.Success && gazeIsRight == false)
                    {
                        gazePointList = new List<Point>();
                        gazePointList.Add(getGazePoint());
                        //compareGazeWithShape(X, Y);
                        //e.Graphics.FillEllipse(Brushes.Red, rec);
                        // Try again if it didn't go well the first time.
                        // Not all eye tracker models will fail at this point, but instead fail on ComputeAndApply.
                        //vaghti cheshmo nabine ghermez mishe

                        for (int i = 0; i < gazePointList.Count; i++)
                        {
                            //Console.WriteLine(new Point(X, Y));
                            //Console.WriteLine(gazePointList[i]);
                            //Console.WriteLine("+++");

                            int gazeX = gazePointList[i].X;
                            int gazeY = gazePointList[i].Y;
                            if (Math.Pow(gazeX - X, 2) + Math.Pow(gazeY - Y, 2) <= Math.Pow(80, 2))//150 circel
                            {
                                e.Graphics.FillEllipse(Brushes.Green, gazeX, gazeY, 10, 10);
                                gazeIsRight = true;
                                System.Threading.Thread.Sleep(1700);
                                status = calibration.CollectData(point);
                                break;
                            }

                            else
                            {
                                e.Graphics.FillEllipse(Brushes.Red, gazeX, gazeY, 10, 10);
                                gazeIsRight = false;
                            }
                        }
                       
                    }

                    if (status == CalibrationStatus.Success && gazeIsRight == true)
                    {
                        e.Graphics.Clear(Color.Black);
                        e.Graphics.FillEllipse(Brushes.Black, rec);
                    }
                    gazeIsRight = false;
                    Invalidate();
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


                this.Close();

            }
            this.Close();
            //  }

            // calibrationBegin = false;  
        }
    }
}

