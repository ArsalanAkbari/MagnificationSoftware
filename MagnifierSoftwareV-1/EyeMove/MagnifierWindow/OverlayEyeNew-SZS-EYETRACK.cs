using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using MagnifierSoftwareV_1.EyeMove.MagnifierWindow;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MagnifierSoftwareV_1.EyeMove
{
    public partial class OverlayEyeNew : Form
    {
        private Configuration mConfiguration;
        public PointF mTargetPoint;
        public PointF mCurrentPoint;
        private bool mFirstTime = true;
        private Point mLastMagnifierPosition = Cursor.Position;
        public Point targetGazePoint;
        MouseController controller;
        public bool fullScreen = false;
        MagnifierWindowsBothEye mgBothEye;
        MagnifierWindowsBothEyeAndHead mgBothEyeAndHead;
        MagnifierWindowLeftEyeAndHead mgLeftEyeAndHead;
        MagnifierWindowLeftEye mgLeftEye;
        MagnifierWindowRightEye mgRightEye;
        MagnifierWindowRightEyeAndHead mgRightEyeAndHead;
        MagnifierWindowHead mgJustHead;
        private string mConfigFileName = "configData.xml";
        MagnifierMainForm mMainForm;

     

        bool IsBothEyeAndHead = false;
        bool IsBothEye = false;

        bool IsLeftEyeAndHead = false;
        bool IsLeftEye = false;

        bool IsRightEyeAndHead = false;
        bool IsRightEye = false;

        bool IsJustHeadAndHead = false;

        

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

            controller = new MouseController(this, mConfiguration);
           // controller.setMovement(MouseController.Movement.HOTKEY);
            controller.Sensitivity = 7;
            controller.setMovement(MouseController.Movement.CONTINUOUS);
            controller.setMode(MouseController.Mode.BOTH_EYE_AND_HEAD);
            

            this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;

            setGazeControllerMode();
            //CheckForKeysJob();
        }

        public void SetMousePosition(Point p)
        {
            Cursor.Position = p;
        }

        private void setGazeControllerMode()
        {
            //Both Eye  ---------------------------------------------------------------------------------------------

            if (mConfiguration.bothEyeAndHead == true)
            {
                controller.setMode(MouseController.Mode.BOTH_EYE_AND_HEAD);
                IsBothEyeAndHead = true;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgBothEyeAndHead = new MagnifierWindowsBothEyeAndHead(this, mConfiguration, this, controller);
            }

            if (mConfiguration.bothEye == true)
            {
                controller.setMode(MouseController.Mode.BOTH_EYE);
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = true;
                IsLeftEye = false;
                IsRightEye = false;
                mgBothEye = new MagnifierWindowsBothEye(this, mConfiguration, this, controller);
            }

            //Left Eye  ---------------------------------------------------------------------------------------------
            if (mConfiguration.leftEyeAndHead == true)
            {
                controller.setMode(MouseController.Mode.LEFT_EYE_AND_HEAD);
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = true;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgLeftEyeAndHead = new MagnifierWindowLeftEyeAndHead(this, mConfiguration, this, controller );
            }

            if (mConfiguration.leftEye == true)
            {
                controller.setMode(MouseController.Mode.LEFT_EYE);
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = true;
                IsRightEye = false;
                mgLeftEye = new MagnifierWindowLeftEye(this, mConfiguration, this, controller);
            }

            //Right Eye  ---------------------------------------------------------------------------------------------

            if (mConfiguration.rightEyeAndHead == true)
            {
                controller.setMode(MouseController.Mode.RIGHT_EYE_AND_HEAD);
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = true;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgRightEyeAndHead = new MagnifierWindowRightEyeAndHead(this, mConfiguration, this, controller);
            }

            if (mConfiguration.rightEye == true)
            {
                controller.setMode(MouseController.Mode.RIGHT_EYE);
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = false;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = true;
                mgRightEye = new MagnifierWindowRightEye(this, mConfiguration, this, controller);
            }

            //Just Head  ---------------------------------------------------------------------------------------------

            if (mConfiguration.justHead == true)
            {
                controller.setMode(MouseController.Mode.JUST_HEAD);
                IsBothEyeAndHead = false;
                IsLeftEyeAndHead = false;
                IsRightEyeAndHead = false;
                IsJustHeadAndHead = true;
                IsBothEye = false;
                IsLeftEye = false;
                IsRightEye = false;
                mgJustHead = new MagnifierWindowHead(this, mConfiguration, this, controller);

            }
        }


        //Fullscreen Mode
        public OverlayEyeNew(Configuration configuration, bool fullscreen , MagnifierMainForm mainForm)
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

            controller = new MouseController(this ,mConfiguration);
            controller.setMovement(MouseController.Movement.HOTKEY);
            controller.Sensitivity = 7;
            controller.setMovement(MouseController.Movement.CONTINUOUS);
            controller.setMode(MouseController.Mode.BOTH_EYE_AND_HEAD);

            this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;
            setGazeControllerMode();
        }


        private bool IsNormal = true; // window is normal
        private bool IsHidden = true; // window is hidden

        //old Version
        public void HandleTimer(object sender, EventArgs e)
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

                Left = (int)mCurrentPoint.X - Width / 2 ;
                Top = (int)mCurrentPoint.Y - Height / 2 ;
                return;
            }

             mCurrentPoint.X += (int)dx;
             mCurrentPoint.Y += (int)dy;


             Left= (int)mCurrentPoint.X - Width / 2;
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

           
            wWhereAmIPoint = new Point((int)mTargetPoint.X, (int)mTargetPoint.Y);

        }

        //##########################################
        public bool showWhereAmI = false;
        public Point wWhereAmIPoint;
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    if (mConfiguration.ZoomFactor < 10f)
                    {
                        mConfiguration.ZoomFactor++;
                        mMainForm.label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
                    }

                }));
            }

            // zoom out
            if (e.KeyCode == Keys.Subtract)
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    if (mConfiguration.ZoomFactor > 1f)
                    {
                        mConfiguration.ZoomFactor--;
                        mMainForm.label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
                    }
                }));
            }

            // toggle fullscreen
            if (e.KeyCode == Keys.Multiply)
            {
                if (IsNormal)
                {
                    IsNormal = false;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        WindowState = FormWindowState.Maximized;
                    }));
                }
                else
                {
                    IsNormal = true;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        WindowState = FormWindowState.Normal;
                    }));
                }
            }

       
            if (e.KeyCode == Keys.Escape)
            {
                NativeMethods.MagShowSystemCursor(true);
                
                mMainForm.button_BackToMain.Hide();
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

                mMainForm.MagniferUsingMouse_button.Enabled = true;
                mMainForm.FullscreenMaginfierEye_Button.Enabled = true;
                mMainForm.MaginfierEye_Button.Enabled = true;
                mMainForm.FullScreenMouse_Button.Enabled = true;
                mMainForm.Evaluation_button.Enabled = true;


            }

            if (e.KeyCode == Keys.F1)
            {
                WhereAmI wAI = new WhereAmI(this, wWhereAmIPoint);
                this.Hide();
                wAI.Show();
            }

        }


        public void exit()
        {
            NativeMethods.MagShowSystemCursor(true);

            mMainForm.button_BackToMain.Hide();
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
            ExStyle = -20,

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
    }
}
