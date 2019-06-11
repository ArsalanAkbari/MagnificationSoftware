 using System;
using System.Drawing;
using System.Windows.Forms;
using Tobii.Interaction;

namespace MagnifierSoftwareV_1.EyeMove
{
    class EyeXPrecisionPointer
    {
        enum PrecisionPointerMode
        {
            ROTATION,
            TRANSLATION,
            BOTH
        }

        PrecisionPointerMode mode;
        bool started;
        HeadPoseStream headPoseStream;
        bool hasCalibrated;
        Vector3 calibrationPoint;
        Vector3[] samples;
        

        int sampleIndex;
        int sampleCount;

      

        Point currentPoint;
        int sensitivity;

        public EyeXPrecisionPointer(int sensitivity)
        {
            mode = PrecisionPointerMode.ROTATION;
            samples = new Vector3[5];


            this.sensitivity = sensitivity;

            headPoseStream = Program.EyeXHost.Streams.CreateHeadPoseStream();
            if (headPoseStream != null)
            {
                headPoseStream.IsEnabled = true;
                headPoseStream.Next += OnNextHeadPose;
                started = true;
            }
        }

        private void OnNextHeadPose(object sender, StreamData<HeadPoseData> headPose)
        {
            if (headPose.Data.HasRotation.HasRotationY || headPose.Data.HasRotation.HasRotationX)
             {
                 sampleCount++;
                 sampleIndex++;
                 if (sampleIndex >= samples.Length)
                    sampleIndex = 0;
                 samples[sampleIndex] = headPose.Data.HeadRotation;
             }


            /*   else if (headPose.Data.HasRotation.HasRotationY)
               {
                   sampleCountY++;
                   sampleIndexY++;
                   if (sampleIndexY >= samples.Length)
                       sampleIndexY = 0;
                   samplesY[sampleIndexY] = headPose.Data.HeadRotation;
               }
               else if ( headPose.Data.HasRotation.HasRotationZ)
               {
                   sampleCountZ++;
                   sampleIndexZ++;
                   if (sampleIndexZ >= samples.Length)
                       sampleIndexZ = 0;
                   samplesZ[sampleIndexZ] = headPose.Data.HeadRotation;
               }

               if ((sampleIndexX >= samples.Length) ||  (sampleIndexY >= samples.Length) ||   (sampleIndexZ >= samples.Length))
               {

                   sampleCount = Math.Max(Math.Max(sampleCountX , sampleCountY) , sampleCountZ);

                   sampleIndex = Math.Max(Math.Max(sampleIndexX, sampleIndexY), sampleIndexZ);

                   if (sampleCount == sampleCountX)
                       samples[sampleIndex] = samplesX[sampleIndexX];

                   else if (sampleCount == sampleCountY)
                       samples[sampleIndex] = samplesY[sampleIndexY];

                   else if (sampleCount == sampleCountZ)
                       samples[sampleIndex] = samplesZ[sampleIndexZ];
               }*/

        }

        public int Sensitivity
        {
            get { return sensitivity; }
            set { sensitivity = value; }
        }

        public override string ToString()
        {
            switch (mode)
            {
                case (PrecisionPointerMode.ROTATION):
                    if (sampleCount < 5)
                        return "No rotation";
                    else
                        return String.Format("({0:f}, {1:f})", currentPoint.X, currentPoint.Y);
                case (PrecisionPointerMode.TRANSLATION):
                    /*
                    if (trans == null)
                        return "";
                    else
                        return String.Format("({0:0}, {1:0})", trans.x, trans.y);*/
                    break;
            }

            return "";
        }

        public Vector3 GetHeadPoint()
        {
            return samples[sampleIndex];
        }

        public Point GetNextPoint(Point warpPoint)
        {

            System.Drawing.Rectangle screenSize = OverlayEyeNew.GetScreenSize();
            if (sampleCount >= 5)
            {
                currentPoint = calculateSmoothedCalibratedPoint();

                double basePitch = (warpPoint.Y - screenSize.Height / 2.0) / (screenSize.Height / 2.0) * 50.0;
                int yOffset = (int)((currentPoint.Y - basePitch) * sensitivity / 5);

                double baseYaw = (warpPoint.X - screenSize.Width / 2.0) / (screenSize.Width / 2.0) * 150.0;
                int xOffset = (int)((currentPoint.X - baseYaw) * sensitivity / 5);

                warpPoint.Offset(xOffset, yOffset);

                return warpPoint;
            }
            return warpPoint;

        }

        public bool IsStarted()
        {
            return started;
        }

        public Point calculateSmoothedCalibratedPoint()
        {
            if (!hasCalibrated)
            {
                calibrationPoint = samples[sampleIndex];
                hasCalibrated = true;
            }

            Point p = new Point(0, 0);
            for (int i = 0; i < samples.Length; i++)
            {
                p.X += (int)((samples[i].Y - calibrationPoint.Y) * -600);
                p.Y += (int)((samples[i].X - calibrationPoint.X) * -1200);
            }
            p.X /= samples.Length;
            p.Y /= samples.Length;

            return p;
        }

        public void Dispose()
        {
            headPoseStream.IsEnabled = false;
        }
    }
}
