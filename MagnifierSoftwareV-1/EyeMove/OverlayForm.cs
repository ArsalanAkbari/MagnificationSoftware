using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using System.Runtime.InteropServices;


namespace MagnifierSoftwareV_1.EyeMove
{
    public partial class OverlayForm : Form
    {
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_SHOWWINDOW = 0x0040;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        Configuration mConfiguration;

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        const int BAR_LENGTH = 25;
        const int BAR_DIST_BELOW = 40;
        const int BAR_HEIGHT = 2;
        const int BAR_OFFSETX = 12;

        MouseController controller;
        bool gazeTracker;
        bool warpBar;

        private OverlayEye overlayEyeForm;
        private OverlayEyeNew overlayEyeNewForm;



        public OverlayForm(MouseController controller, Configuration configuration, OverlayEye overlayEyeForm )
        {

            InitializeComponent();

            this.KeyPreview = true;
            this.overlayEyeForm = overlayEyeForm;

            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, (SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW));
            this.controller = controller;
            this.mConfiguration = configuration;
            this.KeyDown += new KeyEventHandler(HandleEsc);

        }
        public OverlayForm(MouseController controller, Configuration configuration, OverlayEyeNew overlayEyeNew)
        {

            InitializeComponent();

            this.KeyPreview = true;

            this.overlayEyeNewForm = overlayEyeNew;

            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, (SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW));
            this.controller = controller;
            this.mConfiguration = configuration;
            this.KeyDown += new KeyEventHandler(HandleEsc);

        }

        public bool ShowWarpBar
        {
            get { return warpBar; }
            set { warpBar = value; }
        }

        public bool ShowGazeTracker
        {
            get { return gazeTracker; }
            set { gazeTracker = value; }
        }

        public void ShowIfTracking()
        {
            if (gazeTracker || warpBar)
                Show();
            else
                Hide();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Point p;
            Point px;
            Rectangle rec;

            // Skip if no warp to paint
            //  int threshold = controller.WarpPointer.GetWarpTreshold();
            int thresholdWidth = controller.WarpPointer.GetWarpThresholdWidth();
            int thresholdHight = controller.WarpPointer.GetWarpThresholdHight();
            //if (threshold == 0)
            //  return;

            //point of overlay Eye
            p = controller.WarpPointer.GetWarpPoint();
            overlayEyeForm.mTargetPoint = PointToScreen(new Point(p.X, p.Y));
            Console.WriteLine(p);
            //overlayEyeNewForm.mTargetPoint = PointToScreen(new Point(px.X, px.Y));


            // overlayEyeNewForm.targetGazePoint = controller.WarpPointer.GetGazePoint();



            if (gazeTracker || !gazeTracker)
            {
                // Warp threshold
                /* p = controller.WarpPointer.GetWarpPoint();
                rec = new Rectangle(p.X - thresholdWidth / 2, p.Y - thresholdHight / 2, thresholdWidth, thresholdHight);
                //e.Graphics.DrawEllipse(Pens.Gray, rec);
                e.Graphics.DrawRectangle(Pens.Gray, rec);*/

                // Gaze point
                p = controller.WarpPointer.GetGazePoint();
                rec = new Rectangle(p.X - 5, p.Y - 5, 10, 10);
                // e.Graphics.FillEllipse(Brushes.Gray, rec);
                e.Graphics.FillRectangle(Brushes.Gray, rec);
            }

            // data points for calibration
            List<Event> events = controller.GazeCalibrator.GetEvents();
            foreach (Event evt in events)
            {
                p = evt.location;
                rec = new Rectangle(p.X - 5, p.Y - 5, 10, 10);
                e.Graphics.FillRectangle(Brushes.Blue, rec);
                e.Graphics.DrawLine(Pens.Blue, p, Point.Add(p, new Size(evt.delta)));
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();

                // mMainForm.Show();
            }

        }
    }
}
