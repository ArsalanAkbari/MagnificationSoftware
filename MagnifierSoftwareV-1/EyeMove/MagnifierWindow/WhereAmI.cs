using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MagnifierSoftwareV_1.EyeMove.MagnifierWindow
{
    public partial class WhereAmI : Form
    {
       
        private Configuration mConfiguration;
        private bool mfollowEyes = false;
        private combineEyes combineEyes;

        public WhereAmI(bool followEyes, Configuration configuration)
        {

            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            mfollowEyes = followEyes;
            mConfiguration = configuration;
            KeyDown += new KeyEventHandler(HandleEsc);

            combineEyes = new combineEyes(mConfiguration);

        }

     


        /*
         * if the users result by calibration is more than 80% in each of eyes, we can use followEyes option, in other 
         * cases just mouse position.
         */


        private void onPaint(object sender, PaintEventArgs e)
        {

           //mConfiguration.ZoomFactor = 1;

            SolidBrush newBrush;

            if (mConfiguration.whereIAmColor != "0")
                newBrush = new SolidBrush(Color.FromName(mConfiguration.whereIAmColor));
            else
                newBrush = new SolidBrush(Color.Red);

            Rectangle rec;
            
            Rectangle rec2;
            if (!mfollowEyes)
            {
                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;

                //stay in screen
                if (x - 100 <= 0)
                    x = 100;
                if (x - 100 >= Screen.PrimaryScreen.Bounds.Width)
                    x = Screen.PrimaryScreen.Bounds.Width - 100;
                if (y - 100 <= 0)
                    y = 100;
                if (y - 100 >= Screen.PrimaryScreen.Bounds.Height)
                    y = Screen.PrimaryScreen.Bounds.Height - 100;

                rec = new Rectangle(x - 100, y - 100, 200, 200);
                e.Graphics.FillEllipse(newBrush, rec);
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                rec = new Rectangle(x - 25, y - 25, 50, 50);
                e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), rec);
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                this.Invalidate();


            }

            else if (mfollowEyes)
            {
                
                Point gazePoint = combineEyes.GetWarpPoint();
                Point warpPoint = combineEyes.GetNextPoint(gazePoint);

                //stay in primary screen
                if (warpPoint.X-100 <= 0)
                    warpPoint.X = 100;
                if (warpPoint.X - 100 >= Screen.PrimaryScreen.Bounds.Width)
                    warpPoint.X = Screen.PrimaryScreen.Bounds.Width -100;
                if (warpPoint.Y - 100 <= 0)
                    warpPoint.Y = 100;
                if (warpPoint.Y - 100 >= Screen.PrimaryScreen.Bounds.Height)
                    warpPoint.Y = Screen.PrimaryScreen.Bounds.Height-100;

                //center of the circel


                int x = warpPoint.X;
                int y = warpPoint.Y
                    ;
                rec = new Rectangle(x - 100, y - 100, 200, 200);
                e.Graphics.FillEllipse(newBrush, rec);
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                rec = new Rectangle(x - 25, y - 25, 50, 50);
                e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), rec);
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                this.Invalidate();
            }
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
