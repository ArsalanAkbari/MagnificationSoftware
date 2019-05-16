﻿ using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MagnifierSoftwareV_1.EyeMove.WarpPointers;


/// <summary>
/// based on https://precisiongazemouse.com/ 
/// </summary>

namespace MagnifierSoftwareV_1.EyeMove
{
    public class MouseController
    {
        WarpPointer warp;
        PrecisionPointer prec;
        Point finalPoint;
        DateTime pauseTime;
        Point lastCursorPosition;
        GazeCalibrator calibrator;
        Main form;
        OverlayEyeNew formMagnifier;
        bool updatedAtLeastOnce;
        Configuration mConfiguration;

        bool mainFormIsThere = false;
        bool overlayEyeNewFormIsThere = false;

        public enum Mode
        {
            EYEX_ONLY,
            EYEX_ONLY_LeftEye,
            EYEX_ONLY_RightEye,
            EYEX_ONLY_Head,
            EYE_HEAD_COMBINE,
            BOTH_EYE,
            BOTH_EYE_AND_HEAD,
            LEFT_EYE,
            LEFT_EYE_AND_HEAD,
            RIGHT_EYE,
            RIGHT_EYE_AND_HEAD,
            JUST_HEAD,
        };
        Mode mode;

        public enum Movement
        {
            CONTINUOUS,
            HOTKEY
        };
        Movement movement;
        bool movementHotKeyDown = false;

        bool clickHotKeyDown = false;
        bool dragging = false;
        DateTime? timeSinceClickKeyUp;
        Point? lastClick;
        bool pauseMode = false;

        enum TrackingState
        {
            STARTING,
            PAUSED,
            RUNNING,
            ERROR
        };
        TrackingState state;

        int sensitivity;
        public int Sensitivity
        {
            get { return sensitivity; }
            set
            {
                sensitivity = value;
                if (prec != null)
                {
                    prec.Sensitivity = value;
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public MouseController(Main form, Configuration configuration)
        {
            this.form = form;
            mConfiguration = configuration;
            mainFormIsThere = true;
        }

        public MouseController(OverlayEyeNew form, Configuration configuration)
        {
            this.formMagnifier = form;
            mConfiguration = configuration;
            overlayEyeNewFormIsThere = true;
        }

        public void setMovement(Movement movement)
        {
            this.movement = movement;

        }

        public void setMode(Mode mode)
        {
            if (warp != null)
                warp.Dispose();
            if (prec != null)
                prec.Dispose();
            switch (mode)
            {
               /* case Mode.EYEX_ONLY:
                    warp = new EyeXWarpPointer(mConfiguration);
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.EYEX_ONLY_LeftEye:
                    warp = new oneEyeLeft(mConfiguration);
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.EYEX_ONLY_RightEye:
                    warp = new OneEyeRight(mConfiguration);
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.EYEX_ONLY_Head:
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.EYE_HEAD_COMBINE:
                    warp = new combineEyes(mConfiguration);
                   // prec = new EyeXPrecisionPointer(sensitivity);
                    break;*/

                case Mode.BOTH_EYE_AND_HEAD:
                    warp = new combineEyes(mConfiguration);
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.BOTH_EYE:
                    warp = new combineEyes(mConfiguration);
                   // prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.LEFT_EYE_AND_HEAD:
                    warp = new OneEyeLeft(mConfiguration);
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.LEFT_EYE:
                    warp = new OneEyeLeft(mConfiguration);
                   // prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.RIGHT_EYE_AND_HEAD:
                    warp = new OneEyeRight(mConfiguration);
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.RIGHT_EYE:
                    warp = new OneEyeRight(mConfiguration);
                   // prec = new EyeXPrecisionPointer(sensitivity);
                    break;

                case Mode.JUST_HEAD:
                    prec = new EyeXPrecisionPointer(sensitivity);
                    break;
            }


            calibrator = new GazeCalibrator(this, warp);

          /*  if (!warp.IsStarted())
                state = TrackingState.ERROR;

            if (!prec.IsStarted())
                state = TrackingState.ERROR;*/
        }

        public void MovementHotKeyDown()
        {
            if (movement != Movement.HOTKEY || state == TrackingState.ERROR || state == TrackingState.PAUSED)
                return;

            if (!movementHotKeyDown)
            {
                if (!dragging)
                {
                    /*if (mode == Mode.EYEX_AND_EVIACAM || mode == Mode.EVIACAM_ONLY)
                    {
                        SendKeys.Send("{F11}"); // trigger eViacam to start tracking
                    }*/
                    warp.RefreshTracking();
                    state = TrackingState.STARTING;
                    updatedAtLeastOnce = false;
                }
            }

            movementHotKeyDown = true;
        }

        public void PauseHotKeyDown()
        {
            if (pauseMode)
            {
                pauseMode = false;
                if (state != TrackingState.ERROR)
                {
                    warp.RefreshTracking();
                    state = TrackingState.STARTING;
                    updatedAtLeastOnce = false;
                }
            }
            else
            {
                pauseMode = true;
                if (state == TrackingState.STARTING || state == TrackingState.RUNNING)
                    state = TrackingState.PAUSED;
            }
        }

        public void MovementHotKeyUp()
        {
            if (state == TrackingState.ERROR || state == TrackingState.PAUSED)
                return;

            movementHotKeyDown = false;

           /* if (movement == Movement.HOTKEY && (mode == Mode.EYEX_AND_EVIACAM || mode == Mode.EVIACAM_ONLY))
            {
                SendKeys.Send("{F11}"); // trigger eViacam to stop tracking
            }*/
        }

        public void ClickHotKeyDown()
        {
            if (state == TrackingState.ERROR || state == TrackingState.PAUSED)
                return;

            if (!clickHotKeyDown)
            {
                if (!dragging && movementHotKeyDown && timeSinceClickKeyUp != null && System.DateTime.Now.Subtract(timeSinceClickKeyUp.Value).TotalMilliseconds < 250)
                {
                    // it's a drag so click down and hold
                    uint X = (uint)lastClick.Value.X;
                    uint Y = (uint)lastClick.Value.Y;
                    mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                    dragging = true;
                }
            }
            clickHotKeyDown = true;
        }

        public void ClickHotKeyUp()
        {
            if (state == TrackingState.ERROR || state == TrackingState.PAUSED)
                return;

            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;

            if (dragging)
            {
                mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                dragging = false;
            }
            else
            {
                if (timeSinceClickKeyUp != null && System.DateTime.Now.Subtract(timeSinceClickKeyUp.Value).TotalMilliseconds < 500)
                {
                    // it's a double click so use the original click position
                    X = (uint)lastClick.Value.X;
                    Y = (uint)lastClick.Value.Y;
                }
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                lastClick = Cursor.Position;
            }

            timeSinceClickKeyUp = System.DateTime.Now;
            clickHotKeyDown = false;
        }

        public WarpPointer WarpPointer
        {
            get { return warp; }
        }

        public PrecisionPointer PrecisionPointer
        {
            get { return prec; }
        }

        public Point GetFinalPoint()
        {
            return finalPoint;
        }

        public GazeCalibrator GazeCalibrator
        {
            get { return calibrator; }
        }

        public String GetTrackingStatus()
        {
            switch (state)
            {
                case TrackingState.STARTING:
                    return "Starting";
                case TrackingState.RUNNING:
                    return "Running";
                case TrackingState.PAUSED:
                    return "Paused";
                case TrackingState.ERROR:
                    if (!warp.IsStarted())
                        return "No EyeX connection";
                    if (!prec.IsStarted())
                        return "No TrackIR connection";
                    return "Error";
            }
            return "";
        }


        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);


        public void UpdateMouse(Point currentPoint)
        {
            switch (state)
            {
                case TrackingState.STARTING:
                    if (warp.IsWarpReady())
                    {
                        state = TrackingState.RUNNING;
                        finalPoint =  currentPoint;
                    }
                   
                    break;
                case TrackingState.RUNNING:
                    if (movement == Movement.HOTKEY)
                    {
                        if (updatedAtLeastOnce && !movementHotKeyDown)
                            break;
                    }
                    Point warpPoint = warp.GetNextPoint(currentPoint);
                    /* if (mode == Mode.EYEX_AND_SMARTNAV || mode == Mode.EYEX_AND_EVIACAM || mode == Mode.EVIACAM_ONLY)
                     {
                         warpPoint = limitToScreenBounds(warpPoint);
                         if (warpPoint != finalPoint)
                         {
                             finalPoint = warpPoint;
                             form.SetMousePosition(finalPoint);
                             Cursor.Position = finalPoint;
                         }
                     }*/


                    if (Main.MousePosition != finalPoint)
                    {
                        state = TrackingState.PAUSED;
                        pauseTime = System.DateTime.Now;
                    }
                    finalPoint = prec.GetNextPoint(warpPoint);
                    finalPoint = limitToScreenBounds(finalPoint);

                   /* if (mainFormIsThere)
                        form.SetMousePosition(finalPoint);
                    if (overlayEyeNewFormIsThere)
                        formMagnifier.SetMousePosition(finalPoint);*/

                    // formMagnifier.SetCursorPos(finalPoint.X,finalPoint);
                    Cursor.Position = finalPoint;
                    
                    updatedAtLeastOnce = true;
                    break;
                case TrackingState.PAUSED:
                    // Keep pausing if the user is still moving the mouse
                    if (lastCursorPosition != currentPoint)
                    {
                        lastCursorPosition = currentPoint;
                        pauseTime = System.DateTime.Now;
                    }
                    if (!pauseMode && System.DateTime.Now.CompareTo(pauseTime.AddSeconds(1)) > 0)
                        state = TrackingState.STARTING;
                    break;
                case TrackingState.ERROR:
                    if (warp.IsStarted() && prec.IsStarted())
                        state = TrackingState.STARTING;
                    break;
            }
        }

        private Point getScreenCenter()
        {
            Rectangle screenSize = Main.GetScreenSize();
            return new Point(screenSize.Width / 2, screenSize.Height / 2);
        }

        private Point limitToScreenBounds(Point p)
        {
            Rectangle screenSize = Main.GetScreenSize();
            int margin = 10;

            if (p.X < margin)
                p.X = margin;
            if (p.Y < margin)
                p.Y = margin;
            if (p.X >= screenSize.Width - margin)
                p.X = screenSize.Width - margin;
            if (p.Y >= screenSize.Height - margin - 5)
                p.Y = screenSize.Height - margin - 5;

            return p;
        }
    }
}

