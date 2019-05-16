using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;

namespace MagnifierSoftwareV_1
{
    public class Configuration
    {

        public Configuration()
        {
        }

        public int LocationX = -1;
        public int LocationY = -1;

        public bool CloseOnMouseUp = true;
        public bool DoubleBuffered = true;
        public bool HideMouseCursor = false;
        public bool RememberLastPoint = true;
        public bool ReturnToOrigin = true;
        public bool ShowInTaskbar = false;
        public bool TopMostWindow = true;

        //Eye tracking calibration
        public bool calibrationDone = false;
        public ArrayList userNameList = new ArrayList();  //list of userNames
        public String lastUserName = "Gast";                   //last user name(profile)


        //Color blindness
        public bool invertColors = false;
        public bool normal = false;
        public bool protanopia = false;
        public bool protanomaly = false;
        public bool deuteranopia = false;
        public bool deuteranomaly = false;
        public bool tritanopia = false;
        public bool tritanomaly = false;
        public bool achromatopsia = false;
        public bool achromatomaly = false;

        public int MagnifierWidth = 150;
        public int MagnifierHeight = 150;

        public static readonly float ZOOM_FACTOR_MAX = 10.0f;
        public static readonly float ZOOM_FACTOR_MIN = 1.0f;
        public static readonly float ZOOM_FACTOR_DEFAULT = 3.0f;

        public static readonly float SPEED_FACTOR_MAX = 1.0f;
        public static readonly float SPEED_FACTOR_MIN = 0.05f;
        public static readonly float SPEED_FACTOR_DEFAULT = 0.35f;
        public float SPEED_FACTOR_MOUSE = 0.3f;
        public float SPEED_FACTOR_EYE = 0.3f;

        private float mZoomFactor = ZOOM_FACTOR_DEFAULT;
        public float ZoomFactor
        {
            get { return mZoomFactor; }
            set
            {
                if (value > ZOOM_FACTOR_MAX)
                {
                    mZoomFactor = ZOOM_FACTOR_MAX;
                }
                else if (value < ZOOM_FACTOR_MIN)
                {
                    mZoomFactor = ZOOM_FACTOR_MIN;
                }
                else
                {
                    mZoomFactor = value;
                }
            }
        }


        private float mSpeedFactor = SPEED_FACTOR_DEFAULT;
        public float SpeedFactor
        {
            get { return mSpeedFactor; }
            set
            {
                if (value > SPEED_FACTOR_MAX)
                {
                    mSpeedFactor = SPEED_FACTOR_MAX;
                }
                else if (value < SPEED_FACTOR_MIN)
                {
                    mSpeedFactor = SPEED_FACTOR_MIN;
                }
                else
                {
                    mSpeedFactor = value;
                }
            }
        }
    }
}