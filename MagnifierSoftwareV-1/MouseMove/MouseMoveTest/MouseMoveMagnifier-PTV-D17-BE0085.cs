using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using MagnifierSoftwareV_1;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AutoIt;

namespace MagnifierSoftwareV_1.MouseMove.MouseMoveTest
{
    public partial class MouseMoveMagnifier : Form
    {

        private System.Windows.Forms.Timer mTimer;
        private Configuration mConfiguration;


        private Point mStartPoint;
        public PointF mTargetPoint;
        //public PointF mTargetPoint;
        private PointF mCurrentPoint;

        private bool mFirstTime = true;
        private Point mLastMagnifierPosition = Cursor.Position;
        private Magnifier mg;
        public bool isfullScreen = false;

        public MouseMoveMagnifier(Configuration configuration, Point startPoint , bool fullscreen)
        {
            InitializeComponent();
            mConfiguration = configuration;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = mConfiguration.TopMostWindow;

            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            isfullScreen = fullscreen;

            //fullscreen mode
            if (isfullScreen)
                this.WindowState = FormWindowState.Maximized;

            this.DoubleBuffered = true;

            this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;

           /* mTimer = new Timer();
            mTimer.Enabled = true;
            mTimer.Tick += new EventHandler(HandleTimer);
            mTimer.Interval = 80;
            mTimer.Start();*/


            mStartPoint = startPoint;
            mTargetPoint = startPoint;

            mg = new Magnifier(this , mConfiguration ,this);
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


            //this.Invalidate();
            Refresh();
        }


       
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            /*   mTargetPoint = PointToScreen(new Point(e.X, e.Y));
               Console.WriteLine(e.X);
               mTimer.Enabled = true;*/
            
        }


        //This simulates a left mouse click
        public static void LeftMouseClick(Point position)
        {
            Cursor.Position = position;

        }


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
            Configuration mConfiguration = new Configuration();

            var oldPos = Cursor.Position;

          

            Console.WriteLine("New : " + clientPoint);
            /// get screen coordinates
            //ClientToScreen(wndHandle, ref clientPoint);
            Console.WriteLine("New : " + clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = clientPoint;
            //Cursor.Position = new Point(clientPoint.X - mConfiguration.MagnifierWidth | clientPoint.Y - mConfiguration.MagnifierHeight << 0x10);
          

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


        private void MouseMoveMagnifier_MouseClick(object sender, MouseEventArgs e)
        {

            // LeftMouseClick(Cursor.Position);
            Console.WriteLine(Cursor.Position);

            Control control = (Control)sender;

            // Calculate the startPoint by using the PointToScreen 
            // method.
            control.PointToScreen(new Point(e.X, e.Y));

            ClickOnPoint(this.Handle, control.PointToScreen(new Point(e.X, e.Y)));

        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            // LeftMouseClick(Cursor.Position);
            // Console.WriteLine(Cursor.Position);
            //ClickOnPoint(this.Handle, Cursor.Position);
        }


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                NativeMethods.MagShowSystemCursor(true);
                this.Close();
                mg.Dispose();

                // mMainForm.Show();
            }

        }

    }
}
