using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagnifierSoftwareV_1.EyeMove.MagnifierWindow
{
    public partial class WhereAmI : Form
    {
        private OverlayEyeNew overlayEyeNew;
        private PointF mCurrentPoint;
        public WhereAmI(OverlayEyeNew form, PointF currentPoint)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;

            mCurrentPoint = currentPoint;
            overlayEyeNew = form;
            this.KeyDown += new KeyEventHandler(HandleEsc);
          
        }


        private void onPaint(object sender, PaintEventArgs e)
        {
            Rectangle rec;
            rec = new Rectangle((int)mCurrentPoint.X - 5, (int)mCurrentPoint.Y - 5, 50, 50);
            e.Graphics.FillEllipse(Brushes.Gold, rec);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                overlayEyeNew.Show();
                this.Close();
            }

            if (e.KeyCode == Keys.Escape)
            {
                overlayEyeNew.Show();
                this.Close();
            }

        }

     
    }
}
