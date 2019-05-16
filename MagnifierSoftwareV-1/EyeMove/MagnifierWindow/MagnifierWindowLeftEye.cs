using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MagnifierSoftwareV_1.EyeMove.WarpPointers;
using MagnifierSoftwareV_1.EyeMove;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MagnifierSoftwareV_1.MouseMove.MouseMoveTest
{
    public class MagnifierWindowLeftEye : IDisposable
    {
        private Form form;
        private IntPtr hwndMag;
        private float magnification;
        private bool initialized;
        private RECT magWindowRect = new RECT();
        private System.Windows.Forms.Timer timer;
        private Configuration mConfiguration = new Configuration();

        private oneEyeLeft leftEyeGaze;
        MouseController controller;
        float m_MAGFACTOR;

        OverlayEyeNew overlayEyeNewForm;


        public MagnifierWindowLeftEye(Form form, Configuration configuration, OverlayEyeNew overlayEyeNewForm, MouseController mController)
        {

            mConfiguration = configuration;

            if (form == null)
                throw new ArgumentNullException("form");

            magnification = mConfiguration.ZoomFactor;
            this.overlayEyeNewForm = overlayEyeNewForm;
            //mTargetPoint = gazePoint;

            m_MAGFACTOR = mConfiguration.ZoomFactor;

            leftEyeGaze = new oneEyeLeft(mConfiguration);

            controller = mController;


            this.form = form;
            this.form.Resize += new EventHandler(form_Resize);
            this.form.FormClosing += new FormClosingEventHandler(form_FormClosing);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);

            if (overlayEyeNewForm.fullScreen == false)  //if window mode
                timer.Tick += new EventHandler(overlayEyeNewForm.HandleTimer);

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
            //controller.UpdateMouse(Cursor.Position);

            UpdateMaginifier();
        }

        void form_Resize(object sender, EventArgs e)
        {
            ResizeMagnifier();
        }

        ~MagnifierWindowLeftEye()
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


        Point lastTarget = new Point(0, 0);	// the last mean position that we calculated base on the previous gaze points. Note that this point is not the real gaze point.

        public Point calculateTargetPoint(Point gazePoint)
        {
            RECT noMoveArea = new RECT();
            NativeMethods.MagGetWindowSource(hwndMag, ref noMoveArea);

            Point center = new Point((noMoveArea.left + noMoveArea.right) / 2, (noMoveArea.top + noMoveArea.bottom) / 2);

            Point retTarget = new Point(0, 0);

            float slowMargin = 0.30f;
            float fastMargin = 0.05f;



            int width = (int)((noMoveArea.right - noMoveArea.left) * m_MAGFACTOR);
            int height = (int)((noMoveArea.bottom - noMoveArea.top) * m_MAGFACTOR);

            noMoveArea = getInScreenRectFromTargetPoint(center, width, height);

            retTarget = lastTarget;



            if (gazePoint.X > noMoveArea.right - width * fastMargin)
                retTarget.X += (int)(Math.Abs((gazePoint.X - (noMoveArea.right - width * slowMargin))) / m_MAGFACTOR + 1);
            else if (gazePoint.X > noMoveArea.right - width * slowMargin)
                retTarget.X += (int)(Math.Abs((gazePoint.X - (noMoveArea.right - width * slowMargin))) / m_MAGFACTOR / m_MAGFACTOR + 1);

            if (gazePoint.X < noMoveArea.left + width * fastMargin)
                retTarget.X += -(int)(Math.Abs((gazePoint.X - (noMoveArea.left + width * slowMargin))) / m_MAGFACTOR + 1);
            else if (gazePoint.X < noMoveArea.left + width * slowMargin)
                retTarget.X += -(int)(Math.Abs((gazePoint.X - (noMoveArea.left + width * slowMargin))) / m_MAGFACTOR / m_MAGFACTOR + 1);

            if (gazePoint.Y > noMoveArea.bottom - height * fastMargin)
                retTarget.Y += (int)(Math.Abs((gazePoint.Y - (noMoveArea.bottom - height * slowMargin))) / m_MAGFACTOR + 1);
            else if (gazePoint.Y > noMoveArea.bottom - height * slowMargin)
                retTarget.Y += (int)(Math.Abs((gazePoint.Y - (noMoveArea.bottom - height * slowMargin))) / m_MAGFACTOR / m_MAGFACTOR + 1);

            if (gazePoint.Y < noMoveArea.top + height * fastMargin)
                retTarget.Y += -(int)(Math.Abs((gazePoint.Y - (noMoveArea.top + height * slowMargin))) / m_MAGFACTOR + 1);
            else if (gazePoint.Y < noMoveArea.top + height * slowMargin)
                retTarget.Y += -(int)(Math.Abs((gazePoint.Y - (noMoveArea.top + height * slowMargin))) / m_MAGFACTOR / m_MAGFACTOR + 1);

            // let the lastTarget stay in the screen;
            if (retTarget.X <= 0)
                retTarget.X = 0;
            if (retTarget.X >= Screen.PrimaryScreen.Bounds.Width)
                retTarget.X = Screen.PrimaryScreen.Bounds.Width;
            if (retTarget.Y <= 0)
                retTarget.Y = 0;
            if (retTarget.Y >= Screen.PrimaryScreen.Bounds.Height)
                retTarget.Y = Screen.PrimaryScreen.Bounds.Height;

            lastTarget = retTarget;

            return retTarget;
            //  }

        }

        private RECT getInScreenRectFromTargetPoint(PointF target, int width, int height)
        {
            RECT ret;

            ret.left = (int)target.X - width / 2;
            ret.top = (int)target.Y - height / 2;

            // Don't scroll outside desktop area.
            if (ret.left < 0)
            {
                ret.left = 0;
            }
            if (ret.left > Screen.PrimaryScreen.Bounds.Width - width)
            {
                ret.left = Screen.PrimaryScreen.Bounds.Width - width;
            }
            ret.right = ret.left + width;

            if (ret.top < 0)
            {
                ret.top = 0;
            }
            if (ret.top > Screen.PrimaryScreen.Bounds.Height - height)
            {
                ret.top = Screen.PrimaryScreen.Bounds.Height - height;
            }
            ret.bottom = ret.top + height;

            return ret;
        }


        //private WarpPointer warp;

        public virtual void UpdateMaginifier()
        {
            if ((!initialized) || (hwndMag == IntPtr.Zero))
                return;

            //*************************Take Eye Gaze From Both Eye**********************************************//

            Point gazePoint = leftEyeGaze.GetGazePoint();

            gazePoint = leftEyeGaze.GetWarpPoint();
            Point warpPoint = leftEyeGaze.GetNextPoint(gazePoint);

            //overlayEyeNewForm.mTargetPoint = warpPoint;

            //show where am I Point
            //overlayEyeNewForm.wWhereAmIPoint = warpPoint;
            Point target = calculateTargetPoint(warpPoint);

            overlayEyeNewForm.mTargetPoint = target;

            //***********************************************************************************************//
            RECT hostWindowRect = new RECT();
            NativeMethods.GetWindowRect(hwndMag, out hostWindowRect);

            int width = (int)((magWindowRect.right - magWindowRect.left) / m_MAGFACTOR);
            int height = (int)((magWindowRect.bottom - magWindowRect.top) / m_MAGFACTOR);

            RECT sourceRect = getInScreenRectFromTargetPoint(target, width, height);

            setColor();

            // Set the source rectangle for the magnifier control.
            bool ret = NativeMethods.MagSetWindowSource(hwndMag, sourceRect);

            POINT cursor = new POINT();
            NativeMethods.GetCursorPos(ref cursor);

            // The reason why "right" and "bottom" minus the 10 is, normally the image 
            // of the cursor is shown at the bottom right of the cursor position,
            // if they don't minus some pixel, the cursor will stay outside of the magnifier.

            if (cursor.x <= sourceRect.left)
            {
                cursor.x = sourceRect.left;
            }
            if (cursor.x >= sourceRect.right)
            {
                cursor.x = sourceRect.right - 10;
            }
            if (cursor.y <= sourceRect.top)
            {
                cursor.y = sourceRect.top;
            }
            if (cursor.y >= sourceRect.bottom)
            {
                cursor.y = sourceRect.bottom - 10;
            }

            NativeMethods.SetCursorPos(cursor.x, cursor.y);

            // NativeMethods.ShowCursor(true);
            NativeMethods.ShowCursor(true);

            //NativeMethods.MagShowSystemCursor(FALSE);



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


            //Reclaim topmost status, to prevent unmagnified menus from remaining in view. 
            NativeMethods.SetWindowPos(hwndMag, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, (int)SetWindowPosFlags.SWP_NOACTIVATE | (int)SetWindowPosFlags.SWP_NOMOVE | (int)SetWindowPosFlags.SWP_NOSIZE);

            //setWindowsPosition();

            // Force redraw.
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true);
        }

   

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
