using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

        public Magnifier(Form form ,  Configuration configuration)
        {

            mConfiguration = configuration;

            if (form == null)
                throw new ArgumentNullException("form");

            magnification = mConfiguration.ZoomFactor;


            this.form = form;
            this.form.Resize += new EventHandler(form_Resize);
            this.form.FormClosing += new FormClosingEventHandler(form_FormClosing);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);

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
                NativeMethods.SetWindowPos(hwndMag, IntPtr.Zero,
                    magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, 0);
            }
        }

        public virtual void UpdateMaginifier()
        {
            if ((!initialized) || (hwndMag == IntPtr.Zero))
                return;

            POINT mousePoint = new POINT();
            RECT sourceRect = new RECT();

            NativeMethods.GetCursorPos(ref mousePoint);

            int width = (int)((magWindowRect.right - magWindowRect.left) / mConfiguration.ZoomFactor);
            int height = (int)((magWindowRect.bottom - magWindowRect.top) / mConfiguration.ZoomFactor);

            sourceRect.left = mousePoint.x - width / 2;
            sourceRect.top = mousePoint.y - height / 2;


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

            // Set the source rectangle for the magnifier control.
            NativeMethods.MagSetWindowSource(hwndMag, sourceRect);

            //setWindowsPosition();

            //Reclaim topmost status, to prevent unmagnified menus from remaining in view. 
            NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0,
            (int)SetWindowPosFlags.SWP_NOACTIVATE | (int)SetWindowPosFlags.SWP_NOMOVE | (int)SetWindowPosFlags.SWP_NOSIZE);
        
            setColor();
            // Force redraw.
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true);
        }

        private void setWindowsPosition()
        {
            RECT winRect;
            NativeMethods.GetWindowRect(new HandleRef (form, form.Handle), out winRect);

            //POINT winCenter = { (winRect.left + winRect.right) / 2, (winRect.top + winRect.bottom) / 2 };
            int winWidth = (winRect.right - winRect.left);
            int winHeight = (winRect.bottom - winRect.top);

            // use float because we need to do divide
            float width = Screen.PrimaryScreen.Bounds.Width - winWidth;
            float height = Screen.PrimaryScreen.Bounds.Height - winHeight;

            if (winRect.top >= winRect.left * height / width && winRect.top <= height - winRect.left * height / width)
            {   // in the left triangle
                winRect.left = 0;
            }
            else if (winRect.top <= winRect.left * height / width && winRect.top >= height - winRect.left * height / width)
            {   // in the right triangle
                winRect.left =(int) width;
            }
            else if (winRect.top < height / 2)
            {
                winRect.top = 0;
            }
            else
            { //if (winRect.top > height / 2) 
                winRect.top = (int)height;
            }

            // Reclaim topmost status, to prevent unmagnified menus from remaining in view. 
            NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOPMOST, winRect.left, winRect.top, winWidth, winHeight,
                (int)SetWindowPosFlags.SWP_NOZORDER);
        }

        public void setColor()
        {
            //normal
            if (mConfiguration.normal)
            {
                var magEffectInvertNormal = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        1.0f,        0 ,          0,         0,      0,
                                        0 ,         1.0f ,        0,         0,      0,
                                        0,          0,           1.0f,       0,      0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertNormal);
            }

            //invertColors
            if (mConfiguration.invertColors)
            {

                var magEffectInvertInvertColors = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                       -1f, 0, 0, 0, 0,
                                       0, -1f, 0, 0, 0,
                                       0, 0, -1f, 0, 0,
                                       0, 0, 0, 1f, 0,
                                       1f, 1f, 1f, 0, 1f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertInvertColors);
            }

            //Protanopia
            if (mConfiguration.protanopia)
            {
                var magEffectInvertProtanopia = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.567f,     0.433f,     0,            0,      0,
                                        0.558f ,    0.442f  ,   0,            0,      0,
                                        0,          0.242f,     0.758f,       0,      0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertProtanopia);
            }


            if (mConfiguration.protanomaly)
            {
                var magEffectInvertProtanomaly = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.817f,     0.183f,     0,            0,      0,
                                        0.333f ,    0.667f  ,   0,            0,      0,
                                        0,          0.125f,     0.875f,       0,      0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertProtanomaly);
                //Protanomaly
            }


            if (mConfiguration.deuteranopia)
            {
                var magEffectInvertDeuteranopia = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.625f,     0.375f,     0,            0,      0,
                                        0.7f ,      0.3f  ,     0,            0,      0,
                                        0,          0.3f,       0.7f,       0,      0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertDeuteranopia);
                //Deuteranopia
            }


            if (mConfiguration.deuteranomaly)
            {
                var magEffectInvertDeuteranomaly = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.8f,       0.2f,     0,            0,      0,
                                        0.258f ,    0.742f  ,     0,            0,      0,
                                        0,          0.142f,       0.858f,       0,      0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertDeuteranomaly);
                //Deuteranomaly

            }

            if (mConfiguration.tritanopia)
            {
                var magEffectInvertTritanopia = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.95f,0.05f,0,0,0,
                                        0,0.433f,0.567f,0,0,
                                        0,0.475f,0.525f,0,0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertTritanopia);

                //Tritanopia
            }


            if (mConfiguration.tritanomaly)
            {
                var magEffectInvertTritanomaly = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.967f,0.033f,0,0,0,
                                        0,0.733f,0.267f,0,0,
                                        0,0.183f,0.817f,0,0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertTritanomaly);

                //Tritanomaly
            }

            if (mConfiguration.achromatopsia)
            {
                var magEffectInvertAchromatopsia = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.299f,0.587f,0.114f,0,0,
                                        0.299f,0.587f,0.114f,0,0,
                                        0.299f,0.587f,0.114f,0,0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertAchromatopsia);
                //Achromatopsia
            }


            if (mConfiguration.achromatomaly)
            {
                var magEffectInvertAchromatomaly = new NativeMethods.MAGCOLOREFFECT
                {
                    transform = new[] {
                                        0.618f,0.320f,0.062f,0,0,
                                        0.163f,0.775f,0.062f,0,0,
                                        0.163f,0.320f,0.516f,0,0,
                                        0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                        0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                                    }
                };
                NativeMethods.MagSetFullscreenColorEffect(ref magEffectInvertAchromatomaly);
                //Achromatomaly
            }
        }

        public float Magnification
        {
            get { return magnification; }
            set
            {
                if (magnification != value)
                {
                    magnification = value;
                    // Set the magnification factor.
                    Transformation matrix = new Transformation(magnification);
                    NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
                }
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
