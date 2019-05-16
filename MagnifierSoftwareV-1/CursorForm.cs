using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace MagnifierSoftwareV_1
{
    public partial class CursorForm : Form
    {
        private Cursor newCursor = CursorHelper.FromByteArray(Properties.Resources.HiVis_LINK2);
        

        public CursorForm()
        {
            InitializeComponent();

           // FormBorderStyle = FormBorderStyle.None;
            

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;


            //changing the cursor
          
        }

        //changing the cursor
        public static class CursorHelper
        {
            public static Cursor FromByteArray(byte[] array)
            {
                using (MemoryStream memoryStream = new MemoryStream(array))
                {
                    return new Cursor(memoryStream);
                }
            }
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            this.Cursor = newCursor;

        }
    }
}
