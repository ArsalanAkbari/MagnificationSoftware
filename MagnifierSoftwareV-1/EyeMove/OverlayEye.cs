using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Linq;


namespace MagnifierSoftwareV_1.EyeMove
{
    public partial class OverlayEye : Form
    {

        private Configuration mConfiguration;
        private Image mBufferImage = null;
        public static Bitmap mScreenImage = null;
        private static Point mStartPoint;
        public PointF mTargetPoint;
        private PointF mCurrentPoint;
        private bool mFirstTime = true;
        private static Point mLastMagnifierPosition = Cursor.Position;
        private static MagnifierMainForm mMainForm;
        private static EyeXWarpPointer mEyeXWarpPointer;

       
        MouseController controller;

        public bool isFirstTime = true;


        public OverlayEye(Configuration configuration, MouseController controller)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(HandleEsc);

            this.MouseClick += mouseClick;

            this.controller = controller;
            //--- My Init ---
            mConfiguration = configuration;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = mConfiguration.TopMostWindow;
            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            this.DoubleBuffered = true;


           /* Timer mTimer = new Timer();
            // mTimer.Enabled = true;
            mTimer.Tick += new EventHandler(HandleTimer);
            mTimer.Interval = 33;
            mTimer.Start();*/


            mScreenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                     Screen.PrimaryScreen.Bounds.Height);

            mStartPoint = new Point(500, 500);
            //mTargetPoint = Cursor.Position;

            if (mConfiguration.ShowInTaskbar)
                ShowInTaskbar = true;
            else
                ShowInTaskbar = false;
        }
        
        
    
        private void mouseClick(object sender, MouseEventArgs e)
        {
            Hide();
            System.Threading.Thread.Sleep(50);
            CaptureScreen1();
            Show();
        }


        Graphics gCaptureScreen1;

        public void CaptureScreen1()
        {
           // gCaptureScreen1.Clear(SystemColors.Control);
            gCaptureScreen1 = Graphics.FromImage(mScreenImage);
            gCaptureScreen1.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenImage.Width, mScreenImage.Height));
            gCaptureScreen1.Dispose();
            // Refresh();
        }

        protected override void OnShown(EventArgs e)
        {
            RepositionAndShow();
        }

        private delegate void RepositionAndShowDelegate();

        private void RepositionAndShow()
        {

            if (InvokeRequired)
            {
                Invoke(new RepositionAndShowDelegate(RepositionAndShow));
            }
            else
            {
                // Capture the screen image now!
                gCaptureScreen1 = Graphics.FromImage(mScreenImage);
                gCaptureScreen1.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenImage.Width, mScreenImage.Height));
                gCaptureScreen1.Dispose();
                Show();
            }
        }


        public void HandleTimer(object sender, EventArgs e)
        {

            float dx = mConfiguration.SPEED_FACTOR_EYE * (mTargetPoint.X - mCurrentPoint.X);
            float dy = mConfiguration.SPEED_FACTOR_EYE * (mTargetPoint.Y - mCurrentPoint.Y);

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

            // Update location
            Left = (int)mCurrentPoint.X - Width / 2;
            Top = (int)mCurrentPoint.Y - Height / 2;
         
            mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);

            this.Invalidate();
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

            //
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

            if (mScreenImage != null && mConfiguration.invertColors)
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
            imageAttributes.SetColorMatrix(colorMatrix);
            System.Drawing.Rectangle dest = new System.Drawing.Rectangle(0, 0, Width, Height);
            int w = (int)(Width / mConfiguration.ZoomFactor);
            int h = (int)(Height / mConfiguration.ZoomFactor);
            int x = Left - w / 2 + Width / 2;
            int y = Top - h / 2 + Height / 2;

            g.DrawImage(
                mScreenImage,
                dest,
                x, y,
                w, h,
                GraphicsUnit.Pixel,
                imageAttributes);

            if (mConfiguration.DoubleBuffered)
            {
                e.Graphics.DrawImage(mBufferImage, 0, 0, Width, Height);
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
