using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace MagnifierSoftwareV_1
{
    class CreatMetaFile
    {
        Image mScreenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        public Metafile MakeMetafile(float width, float height,string filename)
        {
            // Make a reference bitmap.
            using (Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics gr = Graphics.FromImage(bm))
                {
                    RectangleF bounds = new RectangleF(0, 0, width*5, height*5);

                    Metafile mf;
                    if ((filename != null) && (filename.Length > 0))
                        mf = new Metafile(filename, gr.GetHdc(), bounds , MetafileFrameUnit.Pixel);
                    else
                        mf = new Metafile(gr.GetHdc(), bounds ,MetafileFrameUnit.Pixel);

                    gr.ReleaseHdc();
                    return mf;
                }
            }
        }

        public void DrawOnMetafile(Metafile mf)
        {
            using (Graphics gr = Graphics.FromImage(mf))
            {
                gr.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(mScreenImage.Width, mScreenImage.Height));
                gr.CompositingMode = CompositingMode.SourceCopy;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.Dispose();

                GraphicsUnit unit = GraphicsUnit.Pixel;
                RectangleF source = mf.GetBounds(ref unit);


                PointF[] dest =
               {
            new PointF(0, 0),
            new PointF(source.Width/5, 0),
            new PointF(0, source.Height/5),
                };
             gr.DrawImage(mf, dest, source, GraphicsUnit.Pixel);
            }
        }



        public Bitmap MetafileToBitmap(Metafile mf)
        {
            Bitmap bm = new Bitmap(mf.Width, mf.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                GraphicsUnit unit = GraphicsUnit.Pixel;
                RectangleF source = mf.GetBounds(ref unit);

                PointF[] dest =
                {
            new PointF(0, 0),
            new PointF(source.Width* 5, 0),
            new PointF(0, source.Height* 5),
        };


                gr.DrawImage(mf, dest, source, GraphicsUnit.Pixel);
  
            }
            return bm;
        }



    }
}
