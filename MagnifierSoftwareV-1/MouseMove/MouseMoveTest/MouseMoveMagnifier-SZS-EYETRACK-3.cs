using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Gma.System.MouseKeyHook;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using MagnifierSoftwareV_1.EyeMove.MagnifierWindow;



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
        public bool checkForKeys;

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

           // this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;

            mStartPoint = startPoint;
            mTargetPoint = startPoint;

            mg = new Magnifier(this, mConfiguration, this);

            checkForKeys = true; // to check key inputs through runnig
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
            // mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);,

            if (mConfiguration.HideMouseCursor == true)
                NativeMethods.MagShowSystemCursor(false);
            else if (mConfiguration.HideMouseCursor == false)
                NativeMethods.MagShowSystemCursor(true);

            wWhereAmIPoint = new Point((int)mTargetPoint.X, (int)mTargetPoint.Y);

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


        public bool showWhereAmI = false;
        public Point wWhereAmIPoint;

        float temp = 0;
        string tempString;
        private bool wAIOn;



        private void CheckForKeys()
        {

            while (checkForKeys)
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

                        if (key == Keys.F2)
                        {

                            if (wAIOn == false)
                            {
                                // checkForKeys = false;
                                wAIOn = true;

                                BeginInvoke(new MethodInvoker(delegate
                                {

                                    WhereAmI wAI = new WhereAmI(wWhereAmIPoint, mConfiguration);
                                    wAI.Show();

                                    temp = mConfiguration.ZoomFactor;
                                    mConfiguration.ZoomFactor = 1;

                                    if (mConfiguration.invertColors == true)
                                    {
                                        mConfiguration.invertColors = false;
                                        tempString = "invertColors";
                                    }

                                    else if (mConfiguration.normal == true)
                                    {
                                        mConfiguration.normal = false;
                                        tempString = "normal";
                                    }

                                    else if (mConfiguration.achromatomaly == true)
                                    {
                                        mConfiguration.achromatomaly = false;
                                        tempString = "achromatomaly";
                                    }

                                    else if (mConfiguration.achromatopsia == true)
                                    {
                                        mConfiguration.achromatopsia = false;
                                        tempString = "achromatopsia";

                                    }
                                    else if (mConfiguration.deuteranopia == true)
                                    {
                                        mConfiguration.deuteranopia = false;
                                        tempString = "deuteranopia";

                                    }
                                    else if (mConfiguration.deuteranomaly == true)
                                    {
                                        mConfiguration.deuteranomaly = false;
                                        tempString = "deuteranomaly";

                                    }
                                    else if (mConfiguration.protanomaly == true)
                                    {
                                        mConfiguration.protanomaly = false;
                                        tempString = "protanomaly";

                                    }
                                    else if (mConfiguration.protanopia == true)
                                    {
                                        mConfiguration.protanopia = false;
                                        tempString = "protanopia";

                                    }
                                    else if (mConfiguration.tritanomaly == true)
                                    {
                                        mConfiguration.tritanomaly = false;
                                        tempString = "tritanomaly";

                                    }
                                    else if (mConfiguration.tritanopia == true)
                                    {
                                        mConfiguration.tritanopia = false;
                                        tempString = "tritanopia";
                                    }

                                    mConfiguration.normal = true;


                                }));

                                Thread.Sleep(500);

                            }
                            else if (wAIOn == true)
                            {
                                wAIOn = false;

                                BeginInvoke(new MethodInvoker(delegate
                                {

                                    List<Form> forms = new List<Form>();

                                    foreach (Form f in Application.OpenForms)
                                        if (f.Name == "WhereAmI")
                                            forms.Add(f);

                                    // Now let's close opened myForm instances from aother thread
                                    if (forms.Count > 0)
                                    {
                                        foreach (Form f in forms)
                                            f.Close();
                                    }

                                    mConfiguration.normal = false;
                                    mConfiguration.ZoomFactor = temp;

                                    if (tempString == "invertColors")
                                    {
                                        mConfiguration.invertColors = true;
                                    }

                                    else if (tempString == "normal")
                                    {
                                        mConfiguration.normal = true;

                                    }

                                    else if (tempString == "achromatomaly")
                                    {
                                        mConfiguration.achromatomaly = true;

                                    }

                                    else if (tempString == "achromatopsia")
                                    {
                                        mConfiguration.achromatopsia = true;


                                    }
                                    else if (tempString == "deuteranopia")
                                    {
                                        mConfiguration.deuteranopia = true;


                                    }
                                    else if (tempString == "deuteranomaly")
                                    {
                                        mConfiguration.deuteranomaly = true;

                                    }
                                    else if (tempString == "protanomaly")
                                    {
                                        mConfiguration.protanomaly = true;


                                    }
                                    else if (tempString == "protanopia")
                                    {
                                        mConfiguration.protanopia = true;


                                    }
                                    else if (tempString == "tritanomaly")
                                    {
                                        mConfiguration.tritanomaly = true;


                                    }
                                    else if (tempString == "tritanopia")
                                    {
                                        mConfiguration.tritanopia = true;

                                    }

                                }));

                                Thread.Sleep(500);

                            }
                        }

                        if (key == Keys.F3)
                        {
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                mMainForm.Location = Cursor.Position;
                                mMainForm.WindowState = FormWindowState.Normal;

                            }));

                            Thread.Sleep(500);
                        }


                        // exit form
                        if (key == Keys.Escape)
                        {
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                NativeMethods.MagShowSystemCursor(true);
                                mMainForm.FullScreenMouse_Button.Enabled = true;
                                mMainForm.FullscreenMaginfierEye_Button.Enabled = true;
                                mMainForm.MaginfierEye_Button.Enabled = true;
                                mMainForm.MagniferUsingMouse_button.Enabled = true;
                                mMainForm.Evaluation_button.Enabled = true;

                                checkForKeys = false;
                                mMainForm.button_BackToMain.Hide();
                                this.Close();
                                mg.Dispose();

                            }));
                            Thread.Sleep(500);
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

    
        public void exit()
        {
            mMainForm.button_BackToMain.Hide();
            this.Close();
            mg.Dispose();
            checkForKeys = false;

        }



        //??????????????????????????

        private Image mBufferImage = null;
        private void MouseMoveMagnifier_Paint(object sender, PaintEventArgs e)
        {
            mBufferImage = new Bitmap(Width+10 , Height+10);

            Graphics g = e.Graphics;
            System.Drawing.Rectangle dest = new System.Drawing.Rectangle(0, 0, Width, Height);
            Pen pen2 = new Pen(Color.Red, 10);
            pen2.Alignment = PenAlignment.Inset;
            g.DrawRectangle(pen2, dest);
        }
    }
}
