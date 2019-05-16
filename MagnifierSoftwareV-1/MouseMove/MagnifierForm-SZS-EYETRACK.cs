using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
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

            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Zoom_MouseWheel);

            //--- My Init ---
            mConfiguration = configuration;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = mConfiguration.TopMostWindow;
            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            mTimer = new Timer();
            // mTimer.Enabled = true;
            mTimer.Tick += new EventHandler(HandleTimer);
            mTimer.Interval = 33;
            mTimer.Start();


            mScreenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                     Screen.PrimaryScreen.Bounds.Height);


          
           // mf = MakeMetafile(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, "test.emf");

            // g = Graphics.FromImage(mScreenImage);

            mStartPoint = startPoint;
            mTargetPoint = startPoint;

            if (mConfiguration.ShowInTaskbar)
                ShowInTaskbar = true;
            else
                ShowInTaskbar = false;

            /* var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();
             if(eyeTracker != null) { 
                  eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
                   // Wait for some data to be received.
                  System.Threading.Thread.Sleep(2000);
                  // Stop listening to gaze data.
                  eyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
             }*/

            //backgroundWorker1.RunWorkerAsync();
        }


        void RefreshScreen()
        {
           // g.Clear(Color.Black);
            //Refresh();
            //CaptureScreen();
        }


        public void CaptureScreen()
        {
           
                //g.Clear(Color.Black);
               // g = Graphics.FromImage(mScreenImage);
                g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenImage.Width, mScreenImage.Height));
               // g.Dispose();
                Refresh();
        }

    

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
                // Capture the screen image now!

                g = Graphics.FromImage(mScreenImage);
                g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenImage.Width, mScreenImage.Height));

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

        public void HandleTimer(object sender, EventArgs e)
        {
            RefreshScreen();
            float dx = mConfiguration.SPEED_FACTOR_MOUSE * (mTargetPoint.X - mCurrentPoint.X);
            float dy = mConfiguration.SPEED_FACTOR_MOUSE * (mTargetPoint.Y - mCurrentPoint.Y);

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
               // mTimer.Enabled = false;
            }
            else
            {
                // Update location
                Left = (int)mCurrentPoint.X - Width / 2;
                Top = (int)mCurrentPoint.Y - Height / 2;
                mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);
            }

           // this.Invalidate();
            Refresh();
        }

      


      
     
        private void MagnifierForm_MouseMove(object sender, MouseEventArgs e)
        {
            // if (e.Button == MouseButtons.Left)
            //{
        
            mTargetPoint = PointToScreen(new Point(e.X + mOffset.X, e.Y + mOffset.Y));
            //mTimer.Enabled = true;
            //}
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
            

            if (mConfiguration.DoubleBuffered)
            {
                e.Graphics.DrawImage(mBufferImage, 0, 0, Width, Height);
                Pen pen2 = new Pen(Color.DarkKhaki, 3);
                pen2.Alignment = PenAlignment.Inset;
                e.Graphics.DrawRectangle(pen2, dest);
            }
        }

            /*Point p;
            // Gaze point
            p = mEyeXWarpPointer.GetGazePoint();
            mTargetPoint = PointToScreen(new Point(p.X + mOffset.X, p.Y + mOffset.Y));
            mTimer.Enabled = true;*/

       // 


   
        //#############################################################################################


        /*  Bitmap _memoryImage1;
          Bitmap _memoryImage2;

         // Graphics pictureBox1 = Graphics.FromImage(mScreenImage);
          Image mScreenImage1 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Width);
          Image mScreenImage2 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Width);


          Color[,] _first;
          Color[,] _second;

          Graphics pictureBox2;
          private bool GetImage()
          {
              _memoryImage2 = _memoryImage1;

              CaptureScreen();

              mScreenImage2 = mScreenImage1;

              if (_memoryImage1 != null)
                  _first = Bitmap2Imagearray(_memoryImage1);

              mScreenImage1 = _memoryImage1;

              if (_memoryImage2 != null)
                  _second = Bitmap2Imagearray(_memoryImage2);

              return CompareArrays(_first, _second);
          }

          private bool CompareArrays(Color[,] imageArray1, Color[,] imageArray2)
          {
              for (var i = 0; i < _memoryImage1.Width; i++)
              {
                  for (var j = 0; j < _memoryImage1.Height; j++)
                  {
                      if (_first == null || _second == null) return false;
                      if (_memoryImage1.Width != _memoryImage2.Width ||
                          _memoryImage1.Height != _memoryImage2.Height)
                          return false;
                  }
              }
              return _first.Equals(_second);

          }

          private void CaptureScreen()
          {
              Size s;
              using (var myGraphics = CreateGraphics())
              {
                  s = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Width);
                  _memoryImage1 = new Bitmap(s.Width, s.Height, myGraphics);
              }

              var memoryGraphics = Graphics.FromImage(_memoryImage1);
              memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);


          }

          private static Color[,] Bitmap2Imagearray(Bitmap b)
          {
              var imgArray = new Color[b.Width, b.Height];
              for (var y = 0; y < b.Height; y++)
              {
                  for (var x = 0; x < b.Width; x++)
                  {
                      imgArray[x, y] = b.GetPixel(x, y);
                  }
              }
              return imgArray;
          }

          private void BackgroundWorker1DoWork(object sender, DoWorkEventArgs e)
          {
              e.Result = GetImage();
              if (GetImage())
                  Console.WriteLine("xxxxxx");
              Thread.Sleep(20);
          }

          private void BackgroundWorker1RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
          {

              backgroundWorker1.RunWorkerAsync();
          }


      */

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();

                // mMainForm.Show();
            }

        }

        //Zomming with mouse Wheel
        private void Zoom_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta <0) //ZoomIn
            {
                if (mConfiguration.ZoomFactor > 1)
                {
                    mConfiguration.ZoomFactor -= 1;
                    this.Refresh();
                }
            }
            else
            {
                if (mConfiguration.ZoomFactor < 10)
                {
                    mConfiguration.ZoomFactor += 1;
                    this.Refresh();
                }
            }
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            /*mOffset = new Point(Width / 2 - e.X, Height / 2 - e.Y);
            mCurrentPoint = PointToScreen(new Point(e.X + mOffset.X, e.Y + mOffset.Y));
            mTargetPoint = mCurrentPoint;*/
            // mTimer.Enabled = true;
            //ClickOnPoint(this.Handle, new Point(e.X, e.Y));
      
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            /* if (mConfiguration.CloseOnMouseUp)
             {
                 Close();
                 mScreenImage.Dispose();
             }

             Cursor.Show();
             Cursor.Position = mStartPoint;*/

        }


        private void onClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Location);
            Point x = PointToScreen(e.Location);
            // x.X -= Width ;
            //x.Y -= Height ;
            Console.WriteLine(x);
            ClickOnPoint(this.Handle, x);
        }



        //*#################################################################################


        //#################################################################################

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);


        internal struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        internal struct MOUSEINPUT
        {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

        public static void ClickOnPoint(IntPtr wndHandle, Point clientPoint)
        {
            var oldPos = Cursor.Position;
            Console.WriteLine(Cursor.Position);
            /// get screen coordinates
           // ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);
            Console.WriteLine(Cursor.Position);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseDown, inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            /// return mouse 
            Cursor.Position = oldPos;
        }

     
    }
}
