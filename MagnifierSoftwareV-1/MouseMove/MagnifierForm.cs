using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MagnifierSoftwareV_1.EyeMove.MagnifierWindow;
using System.Runtime.InteropServices;
using Tobii.Research;


namespace MagnifierSoftwareV_1.MouseMove
{
    public partial class MagnifierForm : Form
    {
        private System.Windows.Forms.Timer mTimer;
        private Configuration mConfiguration;
        private Image mBufferImage = null;
        private static Image mScreenImage = null;
        private Point mStartPoint;
        private PointF mTargetPoint;
        private PointF mCurrentPoint;
        private Point mOffset;
        private bool mFirstTime = true;
        private Point mLastMagnifierPosition = Cursor.Position;

       
        Metafile mf;


        public MagnifierForm(Configuration configuration, Point startPoint)
        {
            InitializeComponent();

            //esc key
            this.KeyDown += new KeyEventHandler(HandleEsc);

          //  this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Zoom_MouseWheel);

            //--- My Init ---
            mConfiguration = configuration;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = mConfiguration.TopMostWindow;
            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            mTimer = new Timer();
            // mTimer.Enabled = true;
            mTimer.Tick += new EventHandler(HandleTimer);
            mTimer.Interval = 33;
            mTimer.Start();


            mScreenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                     Screen.PrimaryScreen.Bounds.Height);

           

            if (mConfiguration.ShowInTaskbar)
                ShowInTaskbar = true;
            else
                ShowInTaskbar = false;

        }


      



        //#################################################################################################


        public void CaptureScreen2()
        {
            // Hide();
            //  System.Threading.Thread.Sleep(5);

         
            Graphics g = Graphics.FromImage(mScreenImage);
            g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenImage.Width, mScreenImage.Height), CopyPixelOperation.SourceCopy);

            // bmp.Save("test.png", ImageFormat.Png);
            g.Dispose();
            // Show();
        }


        //'##################################################################################################




        private void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {

            Console.WriteLine(e.LeftEye.GazePoint.PositionOnDisplayArea.X);
            Console.WriteLine(e.LeftEye.GazePoint.PositionOnDisplayArea.Y);
            Console.WriteLine();
            // mTargetPoint = 
            //mTimer.Enabled = true;
        }


        protected override void OnShown(EventArgs e)
        {
            RepositionAndShow();
        }

        private delegate void RepositionAndShowDelegate();

        Graphics g;
        private void RepositionAndShow()
        {
            
            if (InvokeRequired)
            {
                Invoke(new RepositionAndShowDelegate(RepositionAndShow));
            }
            else
            {
                // Capture the screen image now

                //CaptureScreen2();
                g = Graphics.FromImage(mScreenImage);
                g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenImage.Width, mScreenImage.Height), CopyPixelOperation.SourceCopy);
               // g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Dispose();

                if (mConfiguration.HideMouseCursor)
                    Cursor.Hide();
                else
                    Cursor = Cursors.Cross;

                Capture = true;

                if (mConfiguration.RememberLastPoint)
                {
                    mCurrentPoint = mLastMagnifierPosition;
                    Cursor.Position = mLastMagnifierPosition;
                    Left = (int)mCurrentPoint.X - Width / 2;
                    Top = (int)mCurrentPoint.Y - Height / 2;
                }
                else
                {
                    mCurrentPoint = Cursor.Position;
                }
                Show();
            }
        }

      
        private void HandleTimer(object sender, EventArgs e)
        {
            //CaptureScreen2();
            float dx = mConfiguration.SpeedFactor * (mTargetPoint.X - mCurrentPoint.X);
            float dy = mConfiguration.SpeedFactor * (mTargetPoint.Y - mCurrentPoint.Y);

            if (mFirstTime)
            {
                mFirstTime = false;

                mCurrentPoint.X = mTargetPoint.X;
                mCurrentPoint.Y = mTargetPoint.Y;

                Left = (int)mCurrentPoint.X - Width / 2;
                Top = (int)mCurrentPoint.Y - Height / 2;

                return;
            }

          
                mCurrentPoint.X += dx;
                mCurrentPoint.Y += dy;

            if (Math.Abs(dx) < 1 && Math.Abs(dy) < 1)
            {
                mTimer.Enabled = false;
            }
            else
            {
               // CaptureScreen();
                Left = (int)mCurrentPoint.X - Width / 2;
                Top = (int)mCurrentPoint.Y - Height / 2;

                mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);
            }

            this.Invalidate();
           // Refresh();
        }

   

        private void MagnifierForm_MouseMove(object sender, MouseEventArgs e)
        {

                mTargetPoint = PointToScreen(new Point(e.X, e.Y));
   

            // mTargetPoint = PointToScreen(new Point(e.X , e.Y ));

                mTimer.Enabled = true;
         
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (mConfiguration.DoubleBuffered)
            {
                // Do not paint background (required for double buffering)!
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  RefreshScreen();

            if (mBufferImage == null)
            {
                mBufferImage = new Bitmap(Width, Height);
            }
            Graphics bufferGrf = Graphics.FromImage(mBufferImage);

            Graphics g;


            if (mConfiguration.DoubleBuffered)
            {
                g = bufferGrf;
            }
            else
            {
                g = e.Graphics;
            }

            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // g.SmoothingMode = SmoothingMode.AntiAlias;

            //default
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                                                new float[] {1,  0,  0,  0, 0},  // red scaling factor of -1
                                                new float[] {0,  1,  0,  0, 0},  // green scaling factor of -1
                                                new float[] {0,  0,  1,  0, 0},  // blue scaling factor of -1
                                                new float[] {0,  0,  0,  1, 0},   // alpha scaling factor of 1
                                                new float[] {0,  0,  0,  0, 1}}); // three translations of 1;

            if (mScreenImage != null && mConfiguration.normal)
            {
                //Normal
                colorMatrix = new ColorMatrix(new float[][] {
                                                new float[] {1,  0,  0,  0, 0},  // red scaling factor of -1
                                                new float[] {0,  1,  0,  0, 0},  // green scaling factor of -1
                                                new float[] {0,  0,  1,  0, 0},  // blue scaling factor of -1
                                                new float[] {0,  0,  0,  1, 0},   // alpha scaling factor of 1
                                                new float[] {0,  0,  0,  0, 1}}); // three translations of 1
            }

            if (mScreenImage != null &&  mConfiguration.invertColors)
            {

                colorMatrix = new ColorMatrix(new float[][]{
                                                 new float[] {-1, 0, 0, 0, 0},
                                                 new float[] {0, -1, 0, 0, 0},
                                                 new float[] {0, 0, -1, 0, 0},
                                                 new float[] {0, 0, 0, 1, 0},
                                                 new float[] {1, 1, 1, 0, 1}});
            }

            if (mScreenImage != null && mConfiguration.protanopia)
            {
                //Protanopia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.567f, 0.433f ,  0,  0, 0},
                                               new float[] {0.558f ,0.442f ,  0,  0, 0},
                                               new float[] {0, 0.242f, 0.758f,  0, 0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mScreenImage != null && mConfiguration.protanomaly)
            {
                //Protanomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.817f,0.183f,0,0,0},
                                               new float[] {0.333f,0.667f,0,0,0},
                                               new float[] {0,0.125f,0.875f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mScreenImage != null && mConfiguration.deuteranopia)
            {
                //Deuteranopia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.625f,0.375f,0,0,0},
                                               new float[] {0.7f,0.3f,0,0,0},
                                               new float[] { 0,0.3f,0.7f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mScreenImage != null && mConfiguration.deuteranomaly)
            {
                //Deuteranomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.8f,0.2f,0,0,0},
                                               new float[] { 0.258f,0.742f,0,0,0},
                                               new float[] {0,0.142f,0.858f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mScreenImage != null && mConfiguration.tritanopia)
            {
                //Tritanopia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.95f,0.05f,0,0,0},
                                               new float[] {0,0.433f,0.567f,0,0},
                                               new float[] {0,0.475f,0.525f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mScreenImage != null && mConfiguration.tritanomaly)
            {
                //Tritanomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.967f,0.033f,0,0,0},
                                               new float[] {0,0.733f,0.267f,0,0},
                                               new float[] {0,0.183f,0.817f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mScreenImage != null && mConfiguration.achromatopsia)
            {
                //Achromatopsia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.299f,0.587f,0.114f,0,0},
                                               new float[] {0.299f,0.587f,0.114f,0,0},
                                               new float[] { 0.299f,0.587f,0.114f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mScreenImage != null && mConfiguration.achromatomaly)
            {
                //Achromatomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.618f,0.320f,0.062f,0,0},
                                               new float[] {0.163f,0.775f,0.062f,0,0},
                                               new float[] { 0.163f,0.320f,0.516f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }




            ImageAttributes imageAttributes = new ImageAttributes();
            //???????????????
            imageAttributes.SetWrapMode(WrapMode.TileFlipXY);

            imageAttributes.SetColorMatrix(colorMatrix);
            System.Drawing.Rectangle dest = new System.Drawing.Rectangle(0, 0, Width, Height);
            int w = (int)(Width / mConfiguration.ZoomFactor);
            int h = (int)(Height / mConfiguration.ZoomFactor);
            int x = Left - w / 2 + Width / 2;
            int y = Top - h / 2 + Height / 2;

            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawImage(
                mScreenImage,
                dest,
                x, y,
                w, h,
                GraphicsUnit.Pixel,
                imageAttributes);
        
            Pen pen = new Pen(Color.White, 3);
            pen.Alignment = PenAlignment.Inset;
            e.Graphics.DrawRectangle(pen, dest);


            e.Graphics.DrawImage(mBufferImage, 0, 0, Width, Height);
            Pen pen2 = new Pen(Color.Red, 10);
            pen2.Alignment = PenAlignment.Inset;
            e.Graphics.DrawRectangle(pen2, dest);


        }

            /*Point p;
            // Gaze point
            p = mEyeXWarpPointer.GetGazePoint();
            mTargetPoint = PointToScreen(new Point(p.X + mOffset.X, p.Y + mOffset.Y));
            mTimer.Enabled = true;*/

       // 

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();

                // mMainForm.Show();
            }

        }





        //*#################################################################################

       


        private void onClick(object sender, MouseEventArgs e)
        {

          //  ClickOnPoint(this.Handle, new Point(200 , 200));
        }

       //1##########################################################



    }
}
