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
    public partial class test : Form
    {

        private float angle = 0f;
        private Timer timer = new Timer();
        private BufferedGraphics bufferedGraphics;
        private string userName;
        private string savingPath;

        private List<Point> gazePointListForLine = new List<Point>();
        private List<Point> shapPointListForLine = new List<Point>();
        private int lineTruePoints = 0;
        private int lineFalsePoints = 0;

        private List<Point> gazePointListForRectangle = new List<Point>();
        private List<Point> shapPointListForRectangle = new List<Point>();
        private int RectangleTruePoints = 0;
        private int RectangleFalsePoints = 0;

        private List<Point> gazePointListForElips = new List<Point>();
        private List<Point> shapPointListForElips = new List<Point>();
       //  private int ElipsTruePoints = 0;
       // private int ElipsFalsePoints = 0;

        private List<Point> gazePointListForText = new List<Point>();
        private List<Point> shapPointListForText = new List<Point>();
        private int TextTruePoints = 0;
        private int TextFalsePoints = 0;

        private Point gazePoint;
        private combineEyes combineEyeGaze;
        private OneEyeRight oneEyeRight;
        private OneEyeLeft oneEyeLeft;
        private Configuration mConfiguration;

        private bool drawGazePoints;
        private Image mScreenImage = null;

        private bool goExit = false;

        private bool doingLine = true;
        private bool doingRectangle = false;
        private bool beforDoElipse = false;
        private bool doingElipse = false;
        private bool doingText = false;
        private bool beforDoingText = false;
        private bool finishTesting = false;

        private int mDelayFactor;
        private int mSensitivityOfTest;
        private string mtestingMode = "BothEye";

        int counter = 0;

        public test(Configuration configuration, String userName , String savingPath , String testingMode)
        {
            InitializeComponent();

            Cursor.Hide();
            this.savingPath = savingPath;
            this.userName = userName;

            this.mtestingMode = testingMode;

            this.KeyDown += new KeyEventHandler(HandleEsc);
            this.drawGazePoints = false;

            mConfiguration = configuration;
            combineEyeGaze = new combineEyes(mConfiguration);
            oneEyeRight = new OneEyeRight(mConfiguration);
            oneEyeLeft =  new OneEyeLeft(mConfiguration);

            mDelayFactor = mConfiguration.delayFactor;
            mSensitivityOfTest = mConfiguration.sensitivityOfTest;


            FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            bufferedGraphics = context.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));

            mScreenImage = new Bitmap(this.Width,this.Height);

            timer.Enabled = true;
            timer.Tick += new EventHandler(doTesting);
            timer.Interval = 40; // 40 images per second.
            timer.Start();

        }

    
        private void doTesting(object sender, System.EventArgs e)
        {
          

            if (doingLine)
            {
                label1.Visible = true;
                counter++;
                label1.Text = counter.ToString() + " Seconds";
                System.Threading.Thread.Sleep(1300);
                if (counter > 3)
                {
                    counter = 0;
                    label1.Visible = false;
                    OnTimerDrwLine();
                    timer.Enabled = true;
                    label1.Text = "Second Test will be start after 3 seconds!";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingLine = false;
                    doingRectangle = true;
                }
            }

            //-------------------------------------------------------------------------
            if (doingRectangle)
            {

                label1.Visible = true;
                counter++;
                label1.Text = counter.ToString() + " Seconds";
                System.Threading.Thread.Sleep(1300);
                if (counter > 3)
                {
                    counter = 0;
                    label1.Visible = false;
                    OnTimerDrwRectangle();
                    timer.Enabled = true;
                    label1.Text = "Third Test will be start after 3 seconds!";
                    label1.Visible = true;
                    System.Threading.Thread.Sleep(1300);
                    doingRectangle = false;
                    beforDoingText = true;
                    //beforDoElipse = true;
                }
            }
            //-------------------------------------------------------------------------
            if (beforDoingText)
            {
                label1.Visible = true;
                counter++;
                label1.Text = counter.ToString() + " Seconds";
                System.Threading.Thread.Sleep(1300);
                if (counter > 3)
                {
                    counter = 0;
                    label1.Visible = false;
                    beforDoingText = false;
                    doingText = true;
                }
            }

            if (doingText)
            {
               // gazePointList = new List<Point>();
                OnTimerShowText();
            }

            //-------------------------------------------------------------------------

            if (beforDoElipse)
            {
                label1.Visible = true;
                counter++;
                label1.Text = counter.ToString() + " Seconds";
                System.Threading.Thread.Sleep(1300);
                if (counter > 3)
                {
                   // gazePointList = new List<Point>();
                    counter = 0;
                    label1.Visible = false;
                    beforDoElipse = false;
                    doingElipse = true;
                }
            }

            if (doingElipse)
            {
                OnTimerDrwElipse();
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
        private Point getGazePoint()
        {
            if(mtestingMode == "BothEye")
            {
                gazePoint = combineEyeGaze.GetGazePoint();
            }

            else if (mtestingMode == "RightEye")
            {
                gazePoint = oneEyeRight.GetGazePoint();
            }

            else if (mtestingMode == "LeftEye")
            {
                gazePoint = oneEyeLeft.GetGazePoint();
            }

            return gazePoint;
        }

        // draw Line--------------------------------------------------------------------------------------------------------------
        private void OnTimerDrwLine()
        {
            //gazePointListForLine.Add(getGazePoint());

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

                shapPointListForLine.Add(new Point(w+ radius - 3, h+ radius - 3));
                gazePointListForLine.Add(getGazePoint());
                
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
                gazePointListForLine.Add(getGazePoint());
               

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
                gazePointListForLine.Add(getGazePoint());
               

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

                g.FillEllipse(Brushes.Yellow, shapeX , shapeY , 10, 10);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

               
                //true gaze points calulate distance
                if (Math.Pow(gazeX - shapeX , 2) + Math.Pow(gazeY - shapeY ,2) <= Math.Pow(mSensitivityOfTest,2))
                {
                    lineTruePoints++;
                    g.FillEllipse(Brushes.Green, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }

                //false gaze points
                else if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) > Math.Pow(mSensitivityOfTest, 2))
                {
                    lineFalsePoints++;
                    g.FillEllipse(Brushes.Red, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }
            }

            g.Clear(Color.Black);
            timer.Enabled = false;
            saveTheTestingImage("Line-Test" , gazePointListForLine , shapPointListForLine);
            //clearScreen
        }


        // draw Rectangle--------------------------------------------------------------------------------------------------------------
        private void OnTimerDrwRectangle()
        {

            int radius = mSensitivityOfTest / 2;

            Graphics g = bufferedGraphics.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i= this.Width * 1 / 4; i<= this.Width * 3 / 4; i++)
            {
               
                g.FillEllipse(Brushes.Red, i, this.Height * 1 / 4, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, i + radius - 3, this.Height * 1 / 4 + radius -3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForRectangle.Add(new Point(i + radius - 3, this.Height * 1 / 4  + radius - 3));
                gazePointListForRectangle.Add(getGazePoint());

                System.Threading.Thread.Sleep(mDelayFactor);
                g.Clear(Color.Black);


            }

            for (int j = this.Height * 1 / 4; j <= this.Height * 3 / 4; j++)
            {
                
                g.FillEllipse(Brushes.Red, this.Width * 3 / 4, j , mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, this.Width * 3 / 4 + radius - 3, j + radius - 3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                shapPointListForRectangle.Add(new Point(this.Width * 3 / 4 + radius - 3, j + radius - 3));
                gazePointListForRectangle.Add(getGazePoint());


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
                gazePointListForRectangle.Add(getGazePoint());

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
                gazePointListForRectangle.Add(getGazePoint());

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
                if (Math.Pow(gazeX - shapeX , 2) + Math.Pow(gazeY - shapeY ,2) <= Math.Pow(mSensitivityOfTest,2))
                {
                    RectangleTruePoints++;
                    g.FillEllipse(Brushes.Green, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }

                //false gaze points
                else if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) > Math.Pow(mSensitivityOfTest, 2))
                {
                    RectangleFalsePoints++;
                    g.FillEllipse(Brushes.Red, gazeX, gazeY, 10, 10);
                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }
            }

            timer.Enabled = false;
            saveTheTestingImage("Rectangle-Test", gazePointListForRectangle, shapPointListForRectangle);
            //clearScreen
            g.Clear(Color.Black);
            doingRectangle = false;

        }




        // draw Elipse--------------------------------------------------------------------------------------------------------------
        private void OnTimerDrwElipse()
        {
            Graphics g = bufferedGraphics.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int radius = mSensitivityOfTest / 2;

            // for circle we need a 360' angle
            if (angle > 359)
            {

                for (int i = 0; i <= 359; i++)
                {
                    System.Drawing.Drawing2D.Matrix matrix2 = new System.Drawing.Drawing2D.Matrix();
                    matrix2.Rotate(i, System.Drawing.Drawing2D.MatrixOrder.Append);
                    matrix2.Translate(this.ClientSize.Width / 2,
                        this.ClientSize.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);

                    g.Transform = matrix2;
                    g.FillEllipse(Brushes.Yellow, -300, -300, 10, 10);

                    if (!goExit)
                        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                }


                angle = 0;
                 foreach (Point p in gazePointListForElips)
                 {
                     g.FillEllipse(Brushes.DeepSkyBlue, p.X - this.ClientSize.Width / 2 -5, p.Y - this.ClientSize.Height / 2 -5, 10, 10);
                     if (!goExit)
                         bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                     angle++;
                 }


                // Find the True and False gaze points according to shapes coordinate
                /* for (int i = 0; i < gazePointListForElips.Count; i++)
                 {
                     int gazeX = gazePointListForElips[i].X;
                     int gazeY = gazePointListForElips[i].Y;

                     int shapeX = shapPointListForElips[i].X;
                     int shapeY = shapPointListForElips[i].Y;

                     g.FillEllipse(Brushes.Blue, gazeX - this.ClientSize.Width / 2 - 5-radius, gazeY - this.ClientSize.Height / 2 - 5- radius, 10, 10);
                     if (!goExit)
                         bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                    /* //true gaze points
                     if (Math.Abs(gazeX - shapeX) <= mSensitivityOfTest && Math.Abs(gazeY - shapeY) <= mSensitivityOfTest)
                     {
                         ElipsTruePoints++;
                         g.FillEllipse(Brushes.Green, gazeX - this.ClientSize.Width / 2 - 5, gazeY - this.ClientSize.Height / 2 - 5, 10, 10);
                         if (!goExit)
                             bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                     }

                     //false gaze points
                     else
                     {
                         ElipsFalsePoints++;
                         g.FillEllipse(Brushes.Red, gazeX - this.ClientSize.Width / 2 - 5, gazeY - this.ClientSize.Height / 2 - 5, 10, 10);
                         if (!goExit)
                             bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                     }

                 }*/
                timer.Stop();
                //Save the Image
                finishTesting = true;
                saveTheTestingImage("Elipse-Test", gazePointListForElips, shapPointListForElips);
                g.Clear(Color.Black);
                doingElipse = false;
                
                // finishTesting = true;
            }

            // g.Clear(Color.Black);
            if (angle <= 359 && angle !=0)
            {
                /*
                 * X := originX + cos(angle)*radius;
                 * Y := originY + sin(angle)*radius; 
                 * (X, Y) is the center of your circle. radius is its radius
                 * */

                int x = (int)(this.Width / 2 + Math.Cos(angle) * 300 ) - this.ClientSize.Width / 2;
                int y = (int)(this.Height / 2 + Math.Sin(angle) * 300 ) - this.ClientSize.Height / 2;

                System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
                matrix.Rotate(angle, System.Drawing.Drawing2D.MatrixOrder.Append);
                matrix.Translate(this.ClientSize.Width / 2,
                    this.ClientSize.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);

                g.Transform = matrix;
                g.FillEllipse(Brushes.Red, -300, -300, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, -300 + radius - 3, -300 + radius - 3, 6, 6);

                //adding to list

                gazePointListForElips.Add(getGazePoint());
                shapPointListForElips.Add(new Point(x, y));

                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                g.Clear(Color.Black);
            }
            angle++;
            //clearScreen
            //this.Close();
        }



        // Testing On Text--------------------------------------------------------------------------------------------------------------

        string stringTest = "Mit Eye-Tracking (selten auch: Blickerfassung oder Okulographie) bezeichnet man dasT ieren , \n\n " +
                            " Aufzeichnen der hauptsächlich aus Fixationen (Punkte, die man genau betrachtet), Sakkaden  \n\n " +
                            "(schnellen Augenbewegungen) und Regressionen bestehenden Blickbewegungen einer Person.  \n\n " +
                            "Als Eyetracker werden Geräte und Systeme bezeichnet, die die Aufzeichnung vornehmen und eine  \n\n " +
                            "Analyse der Blickbewegungen ermöglichen. Das Eye-Tracking wird als wissenschaftliche Methode  \n\n " +
                            "in den Neurowissenschaften, der Wahrnehmungs-, Kognitions- und Werbepsychologie, der kognitiven bzw., \n\n " +
                            "klinischen Linguistik bei Usability-Tests, im Produktdesign und der Leseforschung eingesetzt.";
        private void OnTimerShowText()
        {
            Graphics g = bufferedGraphics.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            Rectangle rec = new Rectangle(this.Width/8, this.Height / 8, this.Width *3/ 4, this.Height );

            int startPositionY = this.Height / 8 + 25;

            int radius = mSensitivityOfTest / 2;


            for (int i = this.Width / 8 +5; i <= this.Width* 3 / 4; i++)
            {

                //gazePoint = combineEyeGaze.GetGazePoint();
                //gazePointList.Add(gazePoint);
                Font drawFont = new Font("Arial", 20, FontStyle.Bold);
                var format = new StringFormat(StringFormatFlags.LineLimit);

                g.DrawString(stringTest, drawFont, Brushes.White, rec,format);

                g.FillRectangle(Brushes.Red, i , startPositionY, mSensitivityOfTest, mSensitivityOfTest);
                g.FillEllipse(Brushes.Black, i + radius - 3, startPositionY + radius - 3, 6, 6);
                if (!goExit)
                    bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                
                shapPointListForText.Add(new Point(i+ radius - 3, startPositionY+ radius - 3));
                gazePointListForText.Add(getGazePoint());
                //System.Threading.Thread.Sleep();
                drawFont.Dispose();

                if (i == this.Width * 3 / 4 && startPositionY < this.Height / 2)
                {

                    i = this.Width / 8;
                    startPositionY += 65;

                }
                else if( startPositionY > this.Height* 3/10 )
                {
                    for (int j = 0; j < gazePointListForText.Count; j++)
                    {
                        int gazeX = gazePointListForText[j].X;
                        int gazeY = gazePointListForText[j].Y;

                        int shapeX = shapPointListForText[j].X;
                        int shapeY = shapPointListForText[j].Y;

                        g.FillEllipse(Brushes.Yellow, shapeX, shapeY, 20, 20);
                        if (!goExit)
                            bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

                        //true gaze points
                        //if (Math.Abs(gazeX - shapeX) <= mSensitivityOfTest && Math.Abs(gazeY - shapeY) <= mSensitivityOfTest)
                        if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) <= Math.Pow(mSensitivityOfTest, 2))
                        {
                            TextTruePoints++;
                            g.FillEllipse(Brushes.Green, gazeX , gazeY, 10, 10);
                            if (!goExit)
                                bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                        }

                        //false gaze points
                        else if (Math.Pow(gazeX - shapeX, 2) + Math.Pow(gazeY - shapeY, 2) > Math.Pow(mSensitivityOfTest, 2))
                        {
                            TextFalsePoints++;
                            g.FillEllipse(Brushes.Red, gazeX, gazeY, 10, 10);
                            if (!goExit)
                                bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
                        }
                    }

                    //Save the Image
                    saveTheTestingImage("Text-Test" , gazePointListForText, shapPointListForText);
                    g.Clear(Color.Black);
                    doingText = false;
                    beforDoElipse = true;
                    break;
                   // this.Close();
                }

                g.Clear(Color.Transparent);

            }
        }


        //saving the Test results
        private void saveTheTestingImage(string Mode , List<Point> gazePoints , List<Point> shapePoints )
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


            bool existsSecondSubDirectory = System.IO.Directory.Exists(userSubDirectoryPath + "\\" + userName + "-" + mtestingMode + " - Size -" + mSensitivityOfTest +" -"+ date);
            if (!existsSecondSubDirectory)
                System.IO.Directory.CreateDirectory(userSubDirectoryPath + "\\" + userName + "-" + mtestingMode + " - Size -" + mSensitivityOfTest + " -" + date);

            string subDirectoryPath = userSubDirectoryPath + "\\" + userName + "-" + mtestingMode + " - Size -" + mSensitivityOfTest + " -" + date;

            //---------------------------------------------------------------------------

            string str = string.Format(subDirectoryPath + @"\" + Mode + "-" +"VERSION= " + time + ".png");

            //string str = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\"+ "Rectangle-Test" + "  " + date + ".png");
            mScreenImage.Save(str, ImageFormat.Png);
            graphics.Dispose();


            if (!doingElipse)
            {
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
            }

            if (finishTesting)
            {
                string addressToSaveChart = subDirectoryPath + @"\" + "CHART" + "-" + "VERSION= " + time + ".png";
                ChartsForm ch = new ChartsForm(lineTruePoints, lineFalsePoints, RectangleTruePoints, RectangleFalsePoints, TextTruePoints, TextFalsePoints, addressToSaveChart);
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

            if (doingLine)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 8, this.Height * 5 / 8, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 8 + radius - 3, this.Height * 5 / 8 + radius -3, 6, 6);
            }

            else if (doingRectangle)
            {
                e.Graphics.FillEllipse(Brushes.Red, this.Width * 1 / 4, this.Height * 1 / 4, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, this.Width * 1 / 4 + radius - 3, this.Height * 1 / 4 + radius - 3, 6, 6);
            }

            else if (beforDoElipse)
            {
                  System.Drawing.Drawing2D.Matrix matrix2 = new System.Drawing.Drawing2D.Matrix();
                  matrix2.Rotate(0, System.Drawing.Drawing2D.MatrixOrder.Append);
                  matrix2.Translate(this.ClientSize.Width / 2,
                      this.ClientSize.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);

                  e.Graphics.Transform = matrix2;
                  e.Graphics.FillEllipse(Brushes.Red, -300, -300, mSensitivityOfTest, mSensitivityOfTest);
                  e.Graphics.FillEllipse(Brushes.Black, -300 + radius - 3, -300 + radius - 3, 6, 6);

                  if (!goExit)
                      bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

            }

            else if (beforDoingText)
            {
                e.Graphics.FillRectangle(Brushes.Red, this.Width / 8 + 5 , this.Height / 8 + 25, mSensitivityOfTest, mSensitivityOfTest);
                e.Graphics.FillEllipse(Brushes.Black, (int)(this.Width / 8 + 5 + radius - 3),(int) (this.Height / 8 + 25 + radius - 3), 6, 6);
            }
           
            
        }
    }
}
