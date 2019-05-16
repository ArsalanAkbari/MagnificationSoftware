using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MagnifierSoftwareV_1.EyeMove;
using MagnifierSoftwareV_1.EyeMove.WarpPointers;
using System.Drawing.Imaging;
using System.IO;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;

namespace MagnifierSoftwareV_1.EvaluationEye
{
    public partial class CalibrationForm : Form
    {

        private float angle = 0f;
        private Timer timer = new Timer();
        private BufferedGraphics bufferedGraphics;
        private string userName;
        private string savingPath;

        private List<Point> gazePointListForLine;
        private List<Point> shapPointListForLine;

        private int bothEyesTruePoints = 0;
        private int bothEyesFalsePoints = 0;

        private int leftEyesTruePoints = 0;
        private int leftEyesFalsePoints = 0;

        private int rightEyesTruePoints = 0;
        private int rightEyesFalsePoints = 0;


        private List<Point> gazePointListForRectangle;
        private List<Point> shapPointListForRectangle;
       


        private Point gazePoint;
        private combineEyes combineEyeGaze;
        private OneEyeRight oneEyeRight;
        private OneEyeLeft oneEyeLeft;
        private Configuration mConfiguration;

        private bool drawGazePoints;
        private Image mScreenImage = null;

        private bool goExit = false;

        private bool doingLineBothEye = true;
        private bool doingLineLeftEye = false;
        private bool doingLineRightEye = false;

        private bool doingRectangleBothEye = false;
        private bool doingRectangleLeftEye = false;
        private bool doingRectangleRightEye = false;

        private bool finishTesting = false;

        private int mDelayFactor;
        private int mSensitivityOfTest;
        private string mtestingMode = "BothEye";

        int counter = 3;

        public CalibrationForm(Configuration configuration, String userName, String savingPath)
        {
            InitializeComponent();

            Cursor.Hide();
            this.savingPath = savingPath;
            this.userName = userName;

           // this.mtestingMode = testingMode;

            this.KeyDown += new KeyEventHandler(HandleEsc);
            this.drawGazePoints = false;

            mConfiguration = configuration;
            combineEyeGaze = new combineEyes(mConfiguration);
            oneEyeRight = new OneEyeRight(mConfiguration);
            oneEyeLeft = new OneEyeLeft(mConfiguration);

            mDelayFactor = mConfiguration.delayFactor;
            mSensitivityOfTest = mConfiguration.sensitivityOfTest;


            FormBorderStyle = FormBorderStyle.None;
            this.TopMost = false;
            this.WindowState = FormWindowState.Maximized;

            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            bufferedGraphics = context.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));

            mScreenImage = new Bitmap(this.Width, this.Height);

            timer.Enabled = true;
            timer.Tick += new EventHandler(doTesting);
            timer.Interval = 40; // 40 images per second.
            timer.Start();

        }


        private void doTesting(object sender, System.EventArgs e)
        {


            if (doingLineBothEye)
            {
                label1.Visible = true;
                label1.Text = counter.ToString() + " Seconds";
                counter--;
                System.Threading.Thread.Sleep(1300);
                if (counter < 0)
                {
                    counter = 3;
                    label1.Visible = false;
                    OnTimerDrwLine("BothEye");
                    timer.Enabled = true;
                    label1.Text = "Second Test will be start in 3 seconds!";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingLineBothEye = false;
                    doingLineLeftEye = true;
                }
            }

            //-------------------------------------------------------------------------
            if (doingLineLeftEye)
            {
                label1.Visible = true;
                label1.Text = counter.ToString() + " Seconds";
                counter--;
                System.Threading.Thread.Sleep(1300);
                if (counter < 0)
                {
                    counter = 3;
                    label1.Visible = false;
                    OnTimerDrwLine("LeftEye");
                    timer.Enabled = true;
                    label1.Text = "Second Test will be start in 3 seconds!";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingLineLeftEye = false;
                    doingLineRightEye = true;
                }
            }
            //-------------------------------------------------------------------------

            if (doingLineRightEye)
            {
                label1.Visible = true;
                label1.Text = counter.ToString() + " Seconds";
                counter--;
                System.Threading.Thread.Sleep(1300);
                if (counter < 0)
                {
                    counter = 3;
                    label1.Visible = false;
                    OnTimerDrwLine("RightEye");
                    timer.Enabled = true;
                    label1.Text = "Second Test will be start in 3 seconds!";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingLineRightEye = false;
                    doingRectangleBothEye = true;
                }
            }
            //-------------------------------------------------------------------------
            
            if (doingRectangleBothEye)
            {
                label1.Visible = true;
                label1.Text = counter.ToString() + " Seconds";
                counter--;
                System.Threading.Thread.Sleep(1300);
                if (counter < 0)
                {
                    counter = 3;
                    label1.Visible = false;
                    OnTimerDrwRectangle("BothEye");
                    timer.Enabled = true;
                    label1.Text = "Second Test will be start in 3 seconds!";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingRectangleBothEye = false;
                    doingRectangleLeftEye = true;
                }
            }
            //-------------------------------------------------------------------------
            if (doingRectangleLeftEye)
            {
                label1.Visible = true;
                label1.Text = counter.ToString() + " Seconds";
                counter--;
                System.Threading.Thread.Sleep(1300);
                if (counter < 0)
                {
                    counter = 3;
                    label1.Visible = false;
                    OnTimerDrwRectangle("LeftEye");
                    timer.Enabled = true;
                    label1.Text = "Second Test will be start in 3 seconds!";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingRectangleLeftEye = false;
                    doingRectangleRightEye = true;
                }
            }
            //-------------------------------------------------------------------------
            if (doingRectangleRightEye)
            {
                label1.Visible = true;
                label1.Text = counter.ToString() + " Seconds";
                counter--;
                System.Threading.Thread.Sleep(1300);
                if (counter < 0)
                {
                    counter = 3;
                    label1.Visible = false;
                    OnTimerDrwRectangle("RightEye");
                    timer.Enabled = true;
                    label1.Text = " Well Don!\n  You finished all the Tests.";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingRectangleRightEye = false;

                    finishTesting = true;


                }
            }
            //-------------------------------------------------------------------------

            if (finishTesting)
            {
                label1.Text = " Well Don!\n  You finished all the Tests.";
                label1.Visible = true;
                System.Threading.Thread.Sleep(1300);

                // ChartsForm ch = new ChartsForm(lineTruePoints, lineFalsePoints , RectangleTruePoints,RectangleFalsePoints);
                //ch.Show();

                Cursor.Show();
                this.Cursor = NativeMethods.LoadCustomCursor();
                timer.Stop();
                this.Close();

            }


        }

        //chose betweeen 3 test mode
        private Point getGazePoint(String testingMode)
        {
            if (testingMode == "BothEye")
            {
                gazePoint = combineEyeGaze.GetGazePoint();
            }

            else if (testingMode == "RightEye")
            {
                gazePoint = oneEyeRight.GetGazePoint();
            }

            else if (testingMode == "LeftEye")
            {
                gazePoint = oneEyeLeft.GetGazePoint();
            }

            return gazePoint;
        }

        // draw Line--------------------------------------------------------------------------------------------------------------
        private void OnTimerDrwLine(String testingMode)
        {
            //gazePointListForLine.Add(getGazePoint());

            gazePointListForLine = new List<Point>();
            shapPointListForLine = new List<Point>();


            Graphics g = bufferedGraphics.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int w = this.Width * 1 / 8;
            int h = this.Height * 5 / 8;

            /*
             * radius
             * 
             *to detect the actual point of the shape
             *Actually the user will be see the center of the Ball , but this is not the actual point of the shape
             *we shoud add the point with radius of the ball to detect the point( center of the Ball).
             * 
             * */
            int radius = mSensitivityOfTest / 2;

            while (w != this.Width * 3 / 8 && h != 4 / 8)
            {
                g.FillEllipse(Brushes.Red, w, h, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, w + radius - 3, h + radius - 3, 6, 6); // 3 is the radius of ball with 6
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForLine.Add(new Point(w + radius - 3, h + radius - 3));
                gazePointListForLine.Add(getGazePoint(testingMode));
               

                System.Threading.Thread.Sleep(mDelayFactor);
                w++;
                h--;
                g.Clear(Color.Black);
            }

            while (w != this.Width * 5 / 8 && h != 1 / 8)
            {
                g.FillEllipse(Brushes.Red, w, h, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, w + radius - 3, h + radius - 3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForLine.Add(new Point(w + radius - 3, h + radius - 3));
                gazePointListForLine.Add(getGazePoint(testingMode));


                System.Threading.Thread.Sleep(mDelayFactor);
                w++;
                h++;
                g.Clear(Color.Black);
            }

            while (w != this.Width * 7 / 8 && h != 1 / 8)
            {
                g.FillEllipse(Brushes.Red, w, h, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, w + radius - 3, h + radius - 3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForLine.Add(new Point(w + radius - 3, h + radius - 3));
                gazePointListForLine.Add(getGazePoint(testingMode));


                System.Threading.Thread.Sleep(mDelayFactor);
                w++;
                h--;
                g.Clear(Color.Black);
            }

            //statistisch
            // Find the True and False gaze points according to shapes coordinate
            for (int i = 0; i < gazePointListForLine.Count; i++)
            {
                int gazeX = gazePointListForLine[i].X;
                int gazeY = gazePointListForLine[i].Y;

                int shapeX = shapPointListForLine[i].X;
                int shapeY = shapPointListForLine[i].Y;

                g.FillEllipse(Brushes.Yellow, shapeX, shapeY, 10, 10);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));


                //true gaze points calulate distance
                if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) <= Math.Pow(mSensitivityOfTest, 2))
                {
                    CountTruePoints(testingMode);
                    g.FillEllipse(Brushes.Green, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }

                //false gaze points
                else if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) > Math.Pow(mSensitivityOfTest, 2))
                {
                    CountFalsePoints(testingMode);
                    g.FillEllipse(Brushes.Red, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }
            }

            g.Clear(Color.Black);
            timer.Enabled = false;
            saveTheTestingImage("Line-Test", gazePointListForLine, shapPointListForLine);
            //clearScreen
        }


        private void CountTruePoints(String eyeMode)
        {
            if (eyeMode == "BothEye")
            {
                bothEyesTruePoints++;
            }

            else if (eyeMode == "RightEye")
            {
                rightEyesTruePoints++;
            }

            else if (eyeMode == "LeftEye")
            {
                leftEyesTruePoints++;
            }
        }

        private void CountFalsePoints(String eyeMode)
        {
            if (eyeMode == "BothEye")
            {
                bothEyesFalsePoints++;
            }

            else if (eyeMode == "RightEye")
            {
                rightEyesFalsePoints++;
            }

            else if (eyeMode == "LeftEye")
            {
                leftEyesFalsePoints++;
            }
        }


        // draw Rectangle--------------------------------------------------------------------------------------------------------------
        private void OnTimerDrwRectangle(String testingMode)
        {
            gazePointListForRectangle = new List<Point>();
            shapPointListForRectangle = new List<Point>();


            int radius = mSensitivityOfTest / 2;

            Graphics g = bufferedGraphics.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = this.Width * 1 / 4; i <= this.Width * 3 / 4; i++)
            {

                g.FillEllipse(Brushes.Red, i, this.Height * 1 / 4, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, i + radius - 3, this.Height * 1 / 4 + radius - 3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForRectangle.Add(new Point(i + radius - 3, this.Height * 1 / 4 + radius - 3));
                gazePointListForRectangle.Add(getGazePoint(testingMode));

                System.Threading.Thread.Sleep(mDelayFactor);
                g.Clear(Color.Black);


            }

            for (int j = this.Height * 1 / 4; j <= this.Height * 3 / 4; j++)
            {

                g.FillEllipse(Brushes.Red, this.Width * 3 / 4, j, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, this.Width * 3 / 4 + radius - 3, j + radius - 3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForRectangle.Add(new Point(this.Width * 3 / 4 + radius - 3, j + radius - 3));
                gazePointListForRectangle.Add(getGazePoint(testingMode));

                System.Threading.Thread.Sleep(mDelayFactor);
                g.Clear(Color.Black);
            }

            for (int i = this.Width * 3 / 4; i >= this.Width * 1 / 4; i--)
            {

                g.FillEllipse(Brushes.Red, i, this.Height * 3 / 4, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, i + radius - 3, this.Height * 3 / 4 + radius - 3, 6, 6);


                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForRectangle.Add(new Point(i + radius - 3, this.Height * 3 / 4 + radius - 3));
                gazePointListForRectangle.Add(getGazePoint(testingMode));

                System.Threading.Thread.Sleep(mDelayFactor);
                g.Clear(Color.Black);

            }

            for (int j = this.Height * 3 / 4; j >= this.Height * 1 / 4; j--)
            {

                g.FillEllipse(Brushes.Red, this.Width * 1 / 4, j, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, this.Width * 1 / 4 + radius - 3, j + radius - 3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForRectangle.Add(new Point(this.Width * 1 / 4 + radius - 3, j + radius - 3));
                gazePointListForRectangle.Add(getGazePoint(testingMode));

                System.Threading.Thread.Sleep(mDelayFactor);
                g.Clear(Color.Black);
            }

            // Find the True and False gaze points according to shapes coordinate
            for (int i = 0; i < gazePointListForRectangle.Count; i++)
            {
                int gazeX = gazePointListForRectangle[i].X;
                int gazeY = gazePointListForRectangle[i].Y;

                int shapeX = shapPointListForRectangle[i].X;
                int shapeY = shapPointListForRectangle[i].Y;

                g.FillEllipse(Brushes.Yellow, shapeX, shapeY, 20, 20);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                //true gaze points
                //if (Math.Abs(gazeX - shapeX) <= mSensitivityOfTest && Math.Abs(gazeY - shapeY) <= mSensitivityOfTest)
                if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) <= Math.Pow(mSensitivityOfTest, 2))
                {
                    CountTruePoints(testingMode);
                    g.FillEllipse(Brushes.Green, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }

                //false gaze points
                else if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) > Math.Pow(mSensitivityOfTest, 2))
                {
                    CountFalsePoints(testingMode);
                    g.FillEllipse(Brushes.Red, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }
            }

           // timer.Enabled = false;
            if (testingMode == "RightEye")
            {
                
                finishTesting = true;
                label1.Text = " Well Don!\n  You finished all the Tests.";
                label1.Visible = true;
                System.Threading.Thread.Sleep(1300);
                timer.Stop();
            }
            saveTheTestingImage("Rectangle-Test" + testingMode, gazePointListForRectangle, shapPointListForRectangle);
            //clearScreen
            g.Clear(Color.Black);
            // doingRectangle = false;
        }

        //saving the Test results
        private void saveTheTestingImage(string Mode, List<Point> gazePoints, List<Point> shapePoints)
        {
            Graphics graphics = Graphics.FromImage(mScreenImage);
            graphics.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(this.Width, this.Height), CopyPixelOperation.SourceCopy);

            string date = DateTime.Now.ToString("M/d/yyyy");
            string time = DateTime.Now.ToString("HH.mm.ss");

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //---------------------------------------------------------------------------

            bool existsMainDirectory = System.IO.Directory.Exists(path + "\\" + "Eyetracking-Test-Results");
            if (!existsMainDirectory)
                System.IO.Directory.CreateDirectory(path + "\\" + "Eyetracking-Test-Results");

            string mainDirectoryPath = path + "\\" + "Eyetracking-Test-Results";

            //---------------------------------------------------------------------------

            bool existsUserSubDirectory = System.IO.Directory.Exists(mainDirectoryPath + "\\" + userName);
            if (!existsUserSubDirectory)
                System.IO.Directory.CreateDirectory(mainDirectoryPath + "\\" + userName);

            string userSubDirectoryPath = mainDirectoryPath + "\\" + userName;

            //---------------------------------------------------------------------------


            bool existsSecondSubDirectory = System.IO.Directory.Exists(userSubDirectoryPath + "\\" + userName + "-" + mtestingMode + " - Size -" + mSensitivityOfTest + " -" + date);
            if (!existsSecondSubDirectory)
                System.IO.Directory.CreateDirectory(userSubDirectoryPath + "\\" + userName + "-" + mtestingMode + " - Size -" + mSensitivityOfTest + " -" + date);

            string subDirectoryPath = userSubDirectoryPath + "\\" + userName + "-" + mtestingMode + " - Size -" + mSensitivityOfTest + " -" + date;

            //---------------------------------------------------------------------------

            string str = string.Format(subDirectoryPath + @"\" + Mode + "-" + "VERSION= " + time + ".png");

            //string str = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\"+ "Rectangle-Test" + "  " + date + ".png");
            mScreenImage.Save(str, ImageFormat.Png);
            graphics.Dispose();


            //adding list to CSV file-----------------------------------------------------
            using (var file = File.CreateText(subDirectoryPath + @"\" + Mode + "-" + "Shape Points VERSION= " + time + ".csv"))
            {
                foreach (Point point in shapePoints)
                {
                    file.WriteLine(string.Join(",", point));
                }
            }

            using (var file = File.CreateText(subDirectoryPath + @"\" + Mode + "-" + "Gaze Points VERSION= " + time + ".csv"))
            {
                foreach (Point point in gazePoints)
                {
                    file.WriteLine(string.Join(",", point));
                }
            }


            if (finishTesting)
            {
                System.Threading.Thread.Sleep(1300);
                string addressToSaveChart = subDirectoryPath + @"\" + "CHART" + "-" + "VERSION= " + time + ".png";
                ChartsForm ch = new ChartsForm(bothEyesTruePoints,bothEyesFalsePoints,rightEyesTruePoints,rightEyesFalsePoints,leftEyesTruePoints,leftEyesFalsePoints, addressToSaveChart, mConfiguration);
                ch.Show();
            }
        }


        // for text create a video and show the animation after that clike of printscreen

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                goExit = true;
                Cursor.Show();
                this.Cursor = NativeMethods.LoadCustomCursor();
                timer.Stop();
                this.Close();
            }

        }

        private void test_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int radius = mSensitivityOfTest / 2;

            if (doingLineBothEye)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 8, this.Height * 5 / 8, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 8 + radius - 3, this.Height * 5 / 8 + radius - 3, 6, 6);
            }

            else if (doingLineLeftEye)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 8, this.Height * 5 / 8, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 8 + radius - 3, this.Height * 5 / 8 + radius - 3, 6, 6);
            }

            else if (doingLineRightEye)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 8, this.Height * 5 / 8, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 8 + radius - 3, this.Height * 5 / 8 + radius - 3, 6, 6);
            }


            else if (doingRectangleBothEye)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 4, this.Height * 1 / 4, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 4 + radius - 3, this.Height * 1 / 4 + radius - 3, 6, 6);
            }

            else if (doingRectangleLeftEye)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 4, this.Height * 1 / 4, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 4 + radius - 3, this.Height * 1 / 4 + radius - 3, 6, 6);
            }


            else if (doingRectangleRightEye)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 4, this.Height * 1 / 4, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 4 + radius - 3, this.Height * 1 / 4 + radius - 3, 6, 6);
            }



        }
    }
}




