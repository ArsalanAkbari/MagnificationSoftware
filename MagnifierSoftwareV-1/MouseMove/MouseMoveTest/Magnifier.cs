using System;
using System.Drawing;
using System.Windows.Forms;



namespace MagnifierSoftwareV_1.MouseMove.MouseMoveTest
{
    public class Magnifier : IDisposable
    {
        private Form form;
        private IntPtr hwndMag;
        private float magnification;
        private bool initialized;
        private RECT magWindowRect = new RECT();
        private System.Windows.Forms.Timer timer;
        private Configuration mConfiguration = new Configuration();
        private MouseMoveMagnifier mouseMoveMagnifierForm;

        public Magnifier(Form form, Configuration configuration, MouseMoveMagnifier mouseMoveMagnifier)
        {

            mConfiguration = configuration;
            mouseMoveMagnifierForm = mouseMoveMagnifier;

            if (form == null)
                throw new ArgumentNullException("form");

            magnification = mConfiguration.ZoomFactor;


            this.form = form;
            this.form.Resize += new EventHandler(form_Resize);
            this.form.FormClosing += new FormClosingEventHandler(form_FormClosing);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Tick += new EventHandler(mouseMoveMagnifierForm.HandleTimer);

            initialized = NativeMethods.MagInitialize();
            if (initialized)
            {
                SetupMagnifier();
                timer.Interval = NativeMethods.USER_TIMER_MINIMUM;
                timer.Enabled = true;
            }


        }




        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Enabled = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateMaginifier();
        }

        void form_Resize(object sender, EventArgs e)
        {
            ResizeMagnifier();
        }

        ~Magnifier()
        {
            Dispose(false);
        }

        protected virtual void ResizeMagnifier()
        {
            if (initialized && (hwndMag != IntPtr.Zero))
            {
                NativeMethods.GetClientRect(form.Handle, ref magWindowRect);
                // Resize the control to fill the window.
                NativeMethods.SetWindowPos(hwndMag, IntPtr.Zero, magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, 0);
            }
        }



        public virtual void UpdateMaginifier()
        {
            if ((!initialized) || (hwndMag == IntPtr.Zero))
                return;

            //*************************Take Mouse Point**********************************************//

            POINT mousePoint = new POINT();
            RECT sourceRect = new RECT();
            NativeMethods.GetCursorPos(ref mousePoint);
            mouseMoveMagnifierForm.mTargetPoint = new Point(mousePoint.x, mousePoint.y);

            //***************************************************************************************//



            //NativeMethods.GetCursorPos(ref mousePoint);

            int width = (int)((magWindowRect.right - magWindowRect.left) / mConfiguration.ZoomFactor);
            int height = (int)((magWindowRect.bottom - magWindowRect.top) / mConfiguration.ZoomFactor);

            sourceRect.left = mousePoint.x - width / 2;
            sourceRect.top = mousePoint.y - height / 2;

            // Set the magnification factor.
            NativeMethods.MagTransform matrix = new NativeMethods.MagTransform(mConfiguration.ZoomFactor);
            NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);

            // Don't scroll outside desktop area.
            if (sourceRect.left < 0)
            {
                sourceRect.left = 0;
            }
            if (sourceRect.left > NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN) - width)
            {
                sourceRect.left = NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN) - width;
            }
            sourceRect.right = sourceRect.left + width;

            if (sourceRect.top < 0)
            {
                sourceRect.top = 0;
            }
            if (sourceRect.top > NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN) - height)
            {
                sourceRect.top = NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN) - height;
            }
            sourceRect.bottom = sourceRect.top + height;

            if (this.form == null)
            {
                timer.Enabled = false;
                return;
            }

            if (this.form.IsDisposed)
            {
                timer.Enabled = false;
                return;
            }


            if (mConfiguration.HideMouseCursor == true)
                NativeMethods.MagShowSystemCursor(false);
            else if (mConfiguration.HideMouseCursor == false)
                NativeMethods.MagShowSystemCursor(true);

            setColor();


            // Set the source rectangle for the magnifier control.
            NativeMethods.MagSetWindowSource(hwndMag, sourceRect);


            //setWindowsPosition();

            //Reclaim topmost status, to prevent unmagnified menus from remaining in view. 
            NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0,
            (int)SetWindowPosFlags.SWP_NOACTIVATE | (int)SetWindowPosFlags.SWP_NOMOVE | (int)SetWindowPosFlags.SWP_NOSIZE);


            // Force redraw.
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true);
        }



        // bayaad Doros beshe
        public void setColor()
        {

            var defaultCollor = new NativeMethods.ColorEffect
            {
                transform = new[] {
                                        1.0f,        0 ,          0,         0,      0,
                                        0 ,         1.0f ,        0,         0,      0,
                                        0,          0,           1.0f,       0,      0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
            };
            NativeMethods.MagSetColorEffect(hwndMag, ref defaultCollor);



            //normal
            if (mConfiguration.normal)
            {
                var magEffectInvertNormal = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         1.0f,        0 ,          0,         0,      0,
                                         0 ,         1.0f ,        0,         0,      0,
                                         0,          0,           1.0f,       0,      0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertNormal);
            }

            //invertColors
            else if (mConfiguration.invertColors)
            {

                var magEffectInvertInvertColors = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                        -1f, 0, 0, 0, 0,
                                        0, -1f, 0, 0, 0,
                                        0, 0, -1f, 0, 0,
                                        0, 0, 0, 1f, 0,
                                        1f, 1f, 1f, 0, 1f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertInvertColors);
            }

            //Protanopia
            else if (mConfiguration.protanopia)
            {
                var magEffectInvertProtanopia = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.567f,     0.433f,     0,            0,      0,
                                         0.558f ,    0.442f  ,   0,            0,      0,
                                         0,          0.242f,     0.758f,       0,      0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertProtanopia);
            }


            else if (mConfiguration.protanomaly)
            {
                var magEffectInvertProtanomaly = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.817f,     0.183f,     0,            0,      0,
                                         0.333f ,    0.667f  ,   0,            0,      0,
                                         0,          0.125f,     0.875f,       0,      0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertProtanomaly);
                //Protanomaly
            }


            else if (mConfiguration.deuteranopia)
            {
                var magEffectInvertDeuteranopia = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.625f,     0.375f,     0,            0,      0,
                                         0.7f ,      0.3f  ,     0,            0,      0,
                                         0,          0.3f,       0.7f,       0,      0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertDeuteranopia);
                //Deuteranopia
            }


            else if (mConfiguration.deuteranomaly)
            {
                var magEffectInvertDeuteranomaly = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.8f,       0.2f,     0,            0,      0,
                                         0.258f ,    0.742f  ,     0,            0,      0,
                                         0,          0.142f,       0.858f,       0,      0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertDeuteranomaly);
                //Deuteranomaly

            }


            else if (mConfiguration.tritanopia)
            {
                var magEffectInvertTritanopia = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.95f,0.05f,0,0,0,
                                         0,0.433f,0.567f,0,0,
                                         0,0.475f,0.525f,0,0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertTritanopia);

                //Tritanopia
            }



            else if (mConfiguration.tritanomaly)
            {
                var magEffectInvertTritanomaly = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.967f,0.033f,0,0,0,
                                         0,0.733f,0.267f,0,0,
                                         0,0.183f,0.817f,0,0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertTritanomaly);

                //Tritanomaly
            }


            else if (mConfiguration.achromatopsia)
            {
                var magEffectInvertAchromatopsia = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.299f,0.587f,0.114f,0,0,
                                         0.299f,0.587f,0.114f,0,0,
                                         0.299f,0.587f,0.114f,0,0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertAchromatopsia);
                //Achromatopsia
            }



            else if (mConfiguration.achromatomaly)
            {
                var magEffectInvertAchromatomaly = new NativeMethods.ColorEffect
                {
                    transform = new[] {
                                         0.618f,0.320f,0.062f,0,0,
                                         0.163f,0.775f,0.062f,0,0,
                                         0.163f,0.320f,0.516f,0,0,
                                         0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                         0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                     }
                };
                NativeMethods.MagSetColorEffect(hwndMag, ref magEffectInvertAchromatomaly);
                //Achromatomaly
            }
        }


        protected void SetupMagnifier()
        {
            if (!initialized)
                return;

            IntPtr hInst;

            hInst = NativeMethods.GetModuleHandle(null);

            // Make the window opaque.
            form.AllowTransparency = true;
            form.TransparencyKey = Color.Empty;
            form.Opacity = 255;


            // Create a magnifier control that fills the client area.
            NativeMethods.GetClientRect(form.Handle, ref magWindowRect);
            hwndMag = NativeMethods.CreateWindow((int)ExtendedWindowStyles.WS_EX_CLIENTEDGE, NativeMethods.WC_MAGNIFIER,
                "MagnifierWindow", (int)WindowStyles.WS_CHILD | (int)MagnifierStyle.MS_SHOWMAGNIFIEDCURSOR |
                (int)WindowStyles.WS_VISIBLE,
                magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, form.Handle, IntPtr.Zero, hInst, IntPtr.Zero);

            if (hwndMag == IntPtr.Zero)
            {
                return;
            }

            // Set the magnification factor.
            Transformation matrix = new Transformation(magnification);
            NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
        }

        protected void RemoveMagnifier()
        {
            if (initialized)
                NativeMethods.MagUninitialize();
        }

        protected virtual void Dispose(bool disposing)
        {
            timer.Enabled = false;
            if (disposing)
                timer.Dispose();
            timer = null;
            form.Resize -= form_Resize;
            RemoveMagnifier();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
