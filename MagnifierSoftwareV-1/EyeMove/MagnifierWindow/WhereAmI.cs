using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;

namespace MagnifierSoftwareV_1.EyeMove.MagnifierWindow
{
    public partial class WhereAmI : Form
    {
        private PointF mCurrentPoint;
        bool checkForKeys;
        Configuration mConfiguration;

 


        public WhereAmI(PointF currentPoint, Configuration configuration)
        {

            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            mConfiguration = configuration;
            mCurrentPoint = currentPoint;
            KeyDown += new KeyEventHandler(HandleEsc);
        }

        private void onPaint(object sender, PaintEventArgs e)
        {


          /*  mConfiguration.ZoomFactor = 1;

            SolidBrush newBrush;

            if (mConfiguration.whereIAmColor != "0")
                newBrush = new SolidBrush(Color.FromName(mConfiguration.whereIAmColor));
            else
                newBrush = new SolidBrush(Color.Red);

            Rectangle rec = new Rectangle((int) Cursor.Position.X , (int)Cursor.Position.Y, 200, 200);

            e.Graphics.FillEllipse(newBrush, rec);
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            System.Threading.Thread.Sleep(20);

            //this.Invalidate();

            /*  System.Threading.Thread.Sleep(200);

              e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), rec);
              e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
              e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

              System.Threading.Thread.Sleep(200);

              this.Invalidate();*/
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
