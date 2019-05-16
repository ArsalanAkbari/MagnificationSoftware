using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Gma.System.MouseKeyHook;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace MagnifierSoftwareV_1.MouseMove.MouseMoveTest
{
    public partial class MouseMoveMagnifier : Form
    {


        private System.Windows.Forms.Timer mTimer;
        private Configuration mConfiguration;

        private IKeyboardMouseEvents m_GlobalHook;
        private string mConfigFileName = "configData.xml";

        private Point mStartPoint;
        public Point mTargetPoint;
        //public PointF mTargetPoint;
        public Point mCurrentPoint;

        private bool mFirstTime = true;
        private Point mLastMagnifierPosition = Cursor.Position;
        private Magnifier mg;
        public bool isfullScreen = false;

        MagnifierMainForm mMainForm;


        public MouseMoveMagnifier(Configuration configuration, Point startPoint, bool fullscreen, MagnifierMainForm mainForm)
        {
            InitializeComponent();
            mConfiguration = configuration;
            this.Cursor = NativeMethods.LoadCustomCursor();

            mMainForm = mainForm;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = true;

            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            isfullScreen = fullscreen;

            //fullscreen mode
            if (isfullScreen)
                this.WindowState = FormWindowState.Maximized;

            this.DoubleBuffered = true;

            this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;

            mStartPoint = startPoint;
            mTargetPoint = startPoint;

            mg = new Magnifier(this, mConfiguration, this);
            CheckForKeysJob();

        }



        public void HandleTimer(object sender, EventArgs e)
        {

            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

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

            mCurrentPoint.X += (int)dx;
            mCurrentPoint.Y += (int)dy;


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
            if (Top > Screen.PrimaryScreen.Bounds.Height - Height)
            {
                Top = Screen.PrimaryScreen.Bounds.Height - Height;
            }
            // Bottom = Top + Height;
            mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);
            this.Invalidate();
            
        }


        private bool IsNormal = true; // window is normal
        private bool IsHidden = true; // window is hidden

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private Thread BackgroundJob;
        private void CheckForKeysJob()
        {
            BackgroundJob = new Thread(() => CheckForKeys())
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };

            BackgroundJob.Start();
        }

        private void CheckForKeys()
        {

            while (true)
            {
                //sleeping for while, this will reduce load on cpu
                Thread.Sleep(10);

                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    // if key is pressed
                    if (((GetAsyncKeyState(key) & (1 << 15)) != 0))
                    {
                        
                        // zoom in
                        if (key == Keys.Add)
                        {
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                if (mConfiguration.ZoomFactor <10f)
                                {
                                    mConfiguration.ZoomFactor++;
                                    mMainForm.label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
                                }
                               
                            }));
                            Thread.Sleep(500);
                        }

                        // zoom out
                        if (key == Keys.Subtract)
                        {
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                if (mConfiguration.ZoomFactor > 1f)
                                {
                                    mConfiguration.ZoomFactor-- ;
                                    mMainForm.label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
                                }
                            }));
                            Thread.Sleep(500);
                        }

                        // toggle fullscreen
                        if (key == Keys.Multiply)
                        {
                            if (IsNormal)
                            {
                                IsNormal = false;
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    WindowState = FormWindowState.Maximized;
                                }));
                                Thread.Sleep(500);
                            }
                            else
                            {
                                IsNormal = true;
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    WindowState = FormWindowState.Normal;
                                }));
                                Thread.Sleep(500);
                            }
                        }

                        // exit form
                        if (key == Keys.F10)
                        {

                        }
                       
                    }
                }
            }
        }


        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);
        private enum GWL
        {
            ExStyle = -20
        }
        private enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x1
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int wl = GetWindowLong(Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(Handle, 0, 128, LWA.Alpha);
        }
      

        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct RECT
        {
            public uint Left;
            public uint Top;
            public uint Right;
            public uint Bottom;
        };

        [StructLayout(LayoutKind.Sequential)]    // Required by user32.dll
        public struct GuiThreadInfo
        {
            public uint cbSize;
            public uint flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public RECT rcCaret;
        };

    

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                NativeMethods.MagShowSystemCursor(true);
                mMainForm.FullScreenMouse_Button.Enabled = true;
                mMainForm.FullscreenMaginfierEye_Button.Enabled = true;
                mMainForm.MaginfierEye_Button.Enabled = true;
                mMainForm.MagniferUsingMouse_button.Enabled = true;
                mMainForm.Evaluation_button.Enabled = true;

                mMainForm.button_BackToMain.Hide();
                this.Close();
                mg.Dispose();
            }

        }


        public void exit()
        {
            mMainForm.button_BackToMain.Hide();
            this.Close();
            mg.Dispose();

        }







    }
}
