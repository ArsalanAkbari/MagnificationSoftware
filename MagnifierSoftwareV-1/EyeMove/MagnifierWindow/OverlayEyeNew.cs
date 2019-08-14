using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using MagnifierSoftwareV_1.EyeMove.MagnifierWindow;


namespace MagnifierSoftwareV_1.EyeMove
{
    public partial class OverlayEyeNew : Form
    {
        private Configuration mConfiguration;
        private bool mFirstTime = true;
        private MagnifierWindowsBothEye mgBothEye;
        private MagnifierWindowsBothEyeAndHead mgBothEyeAndHead;
        private MagnifierWindowLeftEyeAndHead mgLeftEyeAndHead;
        private MagnifierWindowLeftEye mgLeftEye;
        private MagnifierWindowRightEye mgRightEye;
        private MagnifierWindowRightEyeAndHead mgRightEyeAndHead;
        private MagnifierWindowHead mgJustHead;
        private MagnifierMainForm mMainForm;
        private bool IsBothEyeAndHead = false;
        private bool IsBothEye = false;
        private bool IsLeftEyeAndHead = false;
        private bool IsLeftEye = false;
        private bool IsRightEyeAndHead = false;
        private bool IsRightEye = false;
        private bool IsJustHeadAndHead = false;
        private bool IsNormal; // window is normal
        public bool wAIOn;

        public bool getLastMousePosition = false; // use to get the last point of curser during calling WhereIam point
        public PointF lastMousePosition;


        private PointF mTargetPoint;
        private PointF mCurrentPoint;
        private bool fullScreen = false;
        private bool checkForKeys;
        private bool freeze = false; //stop magnification window by pressing the f4

        private Point whereAmIPoint; // through using f2 button
        private float temp = 0;
        private string tempString;

        private string tempF9;

        int count = 0;

        public void setTargetPoint(PointF value)
        {
            this.mTargetPoint = value;
        }

        public bool getScreenMode()
        {
            return this.fullScreen;
        }

        public bool getFreezeMode()
        {
            return this.freeze;
        }

        public void setWhereAmIPoint(Point value)
        {
            this.whereAmIPoint = value;
        }


        //wnidow Mode
        public OverlayEyeNew(Configuration configuration , MagnifierMainForm mainForm)
        {
            InitializeComponent();
            mConfiguration = configuration;
            this.Cursor = NativeMethods.LoadCustomCursor();
            fullScreen = false;


            mMainForm = mainForm;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = true;

            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            this.DoubleBuffered = true;

            IsNormal = true; // window is normal
            wAIOn = false;

            checkForKeys = true;
            CheckForKeysJob();
            setGazeControllerMode();
          


        }


        //Fullscreen Mode
        public OverlayEyeNew(Configuration configuration, bool fullscreen, MagnifierMainForm mainForm)
        {
            InitializeComponent();
            mConfiguration = configuration;
            this.Cursor = NativeMethods.LoadCustomCursor();

            mMainForm = mainForm;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = true;

            fullScreen = true;

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;


            //this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;


            IsNormal = false; // window is normal
            wAIOn = false;

            checkForKeys = true;
            CheckForKeysJob();
            setGazeControllerMode();
        }




        private void setGazeControllerMode()
        {
            //Both Eye  ---------------------------------------------------------------------------------------------

            if (mConfiguration.bothEyeAndHead == true)
            {

                IsBothEyeAndHead = true;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgBothEyeAndHead = new MagnifierWindowsBothEyeAndHead(this, mConfiguration, this);
            }

            if (mConfiguration.bothEye == true)
            {
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = true;
                IsLeftEye = false;
                IsRightEye = false;
                mgBothEye = new MagnifierWindowsBothEye(this, mConfiguration, this);
            }

            //Left Eye  ---------------------------------------------------------------------------------------------
            if (mConfiguration.leftEyeAndHead == true)
            {
    
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = true;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgLeftEyeAndHead = new MagnifierWindowLeftEyeAndHead(this, mConfiguration, this);
            }

            if (mConfiguration.leftEye == true)
            {
            
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = true;
                IsRightEye = false;
                mgLeftEye = new MagnifierWindowLeftEye(this, mConfiguration, this);
            }

            //Right Eye  ---------------------------------------------------------------------------------------------

            if (mConfiguration.rightEyeAndHead == true)
            {
           
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = true;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgRightEyeAndHead = new MagnifierWindowRightEyeAndHead(this, mConfiguration, this);
            }

            if (mConfiguration.rightEye == true)
            {
            
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = true;
                mgRightEye = new MagnifierWindowRightEye(this, mConfiguration, this);
            }

            //Just Head  ---------------------------------------------------------------------------------------------

            if (mConfiguration.justHead == true)
            {
            
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = true;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgJustHead = new MagnifierWindowHead(this, mConfiguration, this);

            }
        }


        public static Rectangle GetScreenSize()
        {
            return Screen.PrimaryScreen.Bounds;
        }

        public void HandleTimer(object sender, EventArgs e)
        {
            showWhereIam();
            if (freeze == false && fullScreen == false)
            {
                Width = mConfiguration.MagnifierWidth;
                Height = mConfiguration.MagnifierHeight;

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

                mCurrentPoint.X += (int)dx;
                mCurrentPoint.Y += (int)dy;


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


                if (Top < 0)
                {
                    Top = 0;
                }
                if (Top > Screen.PrimaryScreen.Bounds.Height - Height)
                {
                    Top = Screen.PrimaryScreen.Bounds.Height - Height;
                }

                if (mConfiguration.HideMouseCursor == true)
                    NativeMethods.MagShowSystemCursor(false);
                else if (mConfiguration.HideMouseCursor == false)
                    NativeMethods.MagShowSystemCursor(true);

              
            }
            count++;
            if (count == 10)//It use for WherIam point
                getLastMousePosition = false;
        }

        //##########################################


        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private Thread BackgroundJob;
        public void CheckForKeysJob()
        {
            BackgroundJob = new Thread(() => CheckForKeys())
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };

            BackgroundJob.Start();
        }


       
       


        //to have a dynamic where i am Point
        /*
        * if the users result by calibration is more than 80% in each of eyes, we can use followEyes option, in other 
        * cases just mouse position.
        */

        private void showWhereIam()
        {

            if (wAIOn == true)
            {
                WhereAmI wAI = new WhereAmI(mConfiguration.whereIamPointFollowsEyes , mConfiguration);
                wAI.Show();
            }

        }


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
                        if (key == Keys.F6)
                        {
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                if (mConfiguration.ZoomFactor < 10f)
                                {
                                    mConfiguration.ZoomFactor++;
                                    mMainForm.label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
                                }

                            }));
                            Thread.Sleep(500);

                        }

                        // zoom out
                        if (key == Keys.F5)
                        {
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                if (mConfiguration.ZoomFactor > 1f)
                                {
                                    mConfiguration.ZoomFactor--;
                                    mMainForm.label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
                                }
                            }));
                            Thread.Sleep(500);

                        }

                        // toggle fullscreen
                        if (key == Keys.Multiply)
                        {
                            if (IsNormal == true)
                            {
                                IsNormal = false;
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    WindowState = FormWindowState.Maximized;
                                    fullScreen = true;
                                }));
                                Thread.Sleep(500);

                            }
                            else if (IsNormal == false)
                            {
                                IsNormal = true;
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    WindowState = FormWindowState.Normal;
                                    fullScreen = false;
                                }));
                                Thread.Sleep(500);

                            }
                        }

                        if (key == Keys.F4)
                        {
                            if (freeze == false)
                            {
                                freeze = true;
                                Thread.Sleep(500);

                            }
                            else if (freeze == true)
                            {
                                freeze = false;
                                Thread.Sleep(500);

                            }
                        }



                        if (key == Keys.F2)
                        {
                          
                            if (wAIOn == false)
                            {
                               // checkForKeys = false;
                               
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                  
                                    // WhereAmI wAI = new WhereAmI(wWhereAmIPoint, mConfiguration);

                                    //wAI.Show();


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
                                wAIOn = true;
                                count = 0;
                                getLastMousePosition = true;

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

                                NativeMethods.SetWindowPos(mMainForm.Handle, 0, 0, 0, 0, 0, 0x0002 | 0x0001);
                                NativeMethods.ShowWindow(mMainForm.Handle, 5);
                                NativeMethods.SetForegroundWindow(mMainForm.Handle);
                                NativeMethods.SetFocus(mMainForm.Handle);

                                mMainForm.Left = (int)Cursor.Position.X - mMainForm.Width / 2;
                                mMainForm.Top = (int)Cursor.Position.Y - mMainForm.Height / 2;

                                if (mMainForm.Left < 0)
                                {
                                    mMainForm.Left = 0;
                                }
                                if (mMainForm.Left > Screen.PrimaryScreen.Bounds.Width - mMainForm.Width)
                                {
                                    mMainForm.Left = Screen.PrimaryScreen.Bounds.Width - mMainForm.Width;
                                }


                                if (mMainForm.Top < 0)
                                {
                                    mMainForm.Top = 0;
                                }
                                if (mMainForm.Top > Screen.PrimaryScreen.Bounds.Height - mMainForm.Height)
                                {
                                    mMainForm.Top = Screen.PrimaryScreen.Bounds.Height - mMainForm.Height;
                                }


                                mMainForm.WindowState = FormWindowState.Normal;

                            }));

                            Thread.Sleep(500);
                        }

                        if (key == Keys.F9)
                        {

                            if (mConfiguration.invertColors == true)
                            {
                                tempF9 = "invertColors";
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    mConfiguration.normal = true;
                                    mConfiguration.invertColors = false;
                                    mConfiguration.achromatomaly = false;
                                    mConfiguration.achromatopsia = false;
                                    mConfiguration.deuteranopia = false;
                                    mConfiguration.deuteranomaly = false;
                                    mConfiguration.protanomaly = false;
                                    mConfiguration.protanopia = false;
                                    mConfiguration.tritanomaly = false;
                                    mConfiguration.tritanopia = false;
                                }));
                                Thread.Sleep(500);
                            }

                            else if (mConfiguration.invertColors == false)
                            {
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    mConfiguration.normal = false;
                                    mConfiguration.invertColors = true;
                                    mConfiguration.achromatomaly = false;
                                    mConfiguration.achromatopsia = false;
                                    mConfiguration.deuteranopia = false;
                                    mConfiguration.deuteranomaly = false;
                                    mConfiguration.protanomaly = false;
                                    mConfiguration.protanopia = false;
                                    mConfiguration.tritanomaly = false;
                                    mConfiguration.tritanopia = false;
                                }));
                                Thread.Sleep(500);
                            }


                        }


                        //Windows Magnification Size
                        if (key == Keys.F8)
                        {

                            if (Screen.PrimaryScreen.Bounds.Height - mConfiguration.MagnifierHeight >= 10 && Screen.PrimaryScreen.Bounds.Width - mConfiguration.MagnifierWidth >= 10)
                            {
                                if (mConfiguration.MagnifierHeight <= (Screen.PrimaryScreen.Bounds.Height / 10) * 9 && mConfiguration.MagnifierWidth <= Screen.PrimaryScreen.Bounds.Width - 30)
                                {
                                    BeginInvoke(new MethodInvoker(delegate
                                    {
                                        mConfiguration.MagnifierHeight += 10;
                                        mConfiguration.MagnifierWidth += 10;

                                    }));
                                    Thread.Sleep(500);

                                }

                            }

                        }


                        if (key == Keys.F7)
                        {

                            if (mConfiguration.MagnifierHeight > (Screen.PrimaryScreen.Bounds.Height / 10) && mConfiguration.MagnifierWidth > (Screen.PrimaryScreen.Bounds.Width / 10))
                            {
                                if (mConfiguration.MagnifierHeight > Screen.PrimaryScreen.Bounds.Height / 9 && mConfiguration.MagnifierWidth != Screen.PrimaryScreen.Bounds.Width / 9)
                                {
                                    BeginInvoke(new MethodInvoker(delegate
                                    {
                                        mConfiguration.MagnifierHeight -= 10;
                                        mConfiguration.MagnifierWidth -= 10;

                                    }));
                                    Thread.Sleep(500);

                                }

                            }

                        }


                        if (key == Keys.Escape)
                        {
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                NativeMethods.MagShowSystemCursor(true);

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

                                this.Close();

                                if (IsBothEyeAndHead)
                                    mgBothEyeAndHead.Dispose();
                                if (IsBothEye)
                                    mgBothEye.Dispose();

                                if (IsLeftEyeAndHead)
                                    mgLeftEyeAndHead.Dispose();
                                if (IsLeftEye)
                                    mgLeftEye.Dispose();

                                if (IsRightEyeAndHead)
                                    mgRightEyeAndHead.Dispose();
                                if (IsRightEye)
                                    mgRightEye.Dispose();

                                if (IsJustHeadAndHead)
                                    mgJustHead.Dispose();

                                checkForKeys = false;


                                //mgBothEye.close = true;

                                mMainForm.TobiiCalibration_button.Enabled = true;
                                mMainForm.MagniferUsingMouse_button.Enabled = true;
                                mMainForm.FullscreenMaginfierEye_Button.Enabled = true;
                                mMainForm.MaginfierEye_Button.Enabled = true;
                                mMainForm.FullScreenMouse_Button.Enabled = true;
                                mMainForm.Evaluation_button.Enabled = true;
                            }));
                            Thread.Sleep(500);
                        }
                    }
                }
            }
        }

       

        public void exit()
        {
            NativeMethods.MagShowSystemCursor(true);

            this.Close();

            if (IsBothEyeAndHead)
                mgBothEyeAndHead.Dispose();
            if (IsBothEye)
                mgBothEye.Dispose();

            if (IsLeftEyeAndHead)
                mgLeftEyeAndHead.Dispose();
            if (IsLeftEye)
                mgLeftEye.Dispose();

            if (IsRightEyeAndHead)
                mgRightEyeAndHead.Dispose();
            if (IsRightEye)
                mgRightEye.Dispose();

            if (IsJustHeadAndHead)
                mgJustHead.Dispose();

            checkForKeys = false;

            mMainForm.TobiiCalibration_button.Enabled = true;
            mMainForm.MagniferUsingMouse_button.Enabled = true;
            mMainForm.FullscreenMaginfierEye_Button.Enabled = true;
            mMainForm.MaginfierEye_Button.Enabled = true;
            mMainForm.FullScreenMouse_Button.Enabled = true;
            mMainForm.Evaluation_button.Enabled = true;
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



        //activate mouse events in magnification window
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int wl = GetWindowLong(Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(Handle, 0, 128, LWA.Alpha);


            Panel pnlTop = new Panel() { Height = 10, Dock = DockStyle.Top, BackColor = Color.Red };
            this.Controls.Add(pnlTop);

            Panel pnlTop2 = new Panel() { Height = 10, Dock = DockStyle.Bottom, BackColor = Color.Red };
            this.Controls.Add(pnlTop2);

            Panel pnlTop3 = new Panel() { Width = 10, Dock = DockStyle.Left, BackColor = Color.Red };
            this.Controls.Add(pnlTop3);

            Panel pnlTop4 = new Panel() { Width = 10, Dock = DockStyle.Right, BackColor = Color.Red };
            this.Controls.Add(pnlTop4);

        }

      
    }
}
