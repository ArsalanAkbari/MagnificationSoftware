using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagnifierSoftwareV_1.MouseMove.MouseMoveTest
{
    public partial class MouseMoveMagnifier : Form
    {

        private System.Windows.Forms.Timer mTimer;
        private Configuration mConfiguration;
        private Image mBufferImage = null;
        private static Image mScreenImage = null;
        private Point mStartPoint;
        public PointF mTargetPoint;
        private PointF mCurrentPoint;
        private Point mOffset;
        private bool mFirstTime = true;
        private Point mLastMagnifierPosition = Cursor.Position;

        public MouseMoveMagnifier(Configuration configuration, Point startPoint)
        {
            InitializeComponent();
            mConfiguration = configuration;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = mConfiguration.TopMostWindow;
            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            //Width = Screen.PrimaryScreen.Bounds.Width;
            //Height = Screen.PrimaryScreen.Bounds.Height;

            this.DoubleBuffered = true;

            this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;

            mTimer = new Timer();
            mTimer.Enabled = true;
            mTimer.Tick += new EventHandler(HandleTimer);
            mTimer.Interval = 80;
            mTimer.Start();


            mStartPoint = startPoint;
            mTargetPoint = startPoint;

            Magnifier mg = new Magnifier(this , mConfiguration);
        }

  

        public void HandleTimer(object sender, EventArgs e)
        {
          
            float dx = mTargetPoint.X - mCurrentPoint.X;
            float dy = mTargetPoint.Y - mCurrentPoint.Y;


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

                if (Left < 0)
                {
                    Left = 0;
                }
                if (Left > Screen.PrimaryScreen.Bounds.Width - Width)
                {
                    Left = Screen.PrimaryScreen.Bounds.Width - Width;
                }
             //   this.Right = Left + Width;

                if (Top < 0)
                {
                    Top = 0;
                }
                if (Top >Screen.PrimaryScreen.Bounds.Height - Height)
                {
                    Top = Screen.PrimaryScreen.Bounds.Height - Height;
                }
               // Bottom = Top + Height;
                mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);
            }

            //this.Invalidate();
             Refresh();
        }


       
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            mTargetPoint = PointToScreen(new Point(e.X, e.Y));
            Console.WriteLine(e.X);
            mTimer.Enabled = true;
        }

      



        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                var magEffectInvert = new NativeMethods.MAGCOLOREFFECT
                {
                   transform = new[] {
                                        1.0f,        0 ,          0,         0,      0,
                                        0 ,         1.0f ,        0,         0,      0,
                                        0,          0,           1.0f,       0,      0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };

                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvert);
                NativeMethods.InvalidateRect(this.Handle, IntPtr.Zero, true);
                this.Close();

                // mMainForm.Show();
            }

        }
    }
}
