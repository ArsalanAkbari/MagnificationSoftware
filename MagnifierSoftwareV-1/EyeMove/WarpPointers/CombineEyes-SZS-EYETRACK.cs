using System;
using System.Drawing;
using Tobii.Research;
using System.Windows.Forms;
using Tobii.Interaction;
using MagnifierSoftwareV_1;
using MagnifierSoftwareV_1.MouseMove;
using MagnifierSoftwareV_1.EyeMove.Calibration;


namespace MagnifierSoftwareV_1.EyeMove
{
    public class combineEyes : WarpPointer
    {
        // GazePointDataStream stream;
        //FixationDataStream stream;
        Point warpPoint;
        Point[] samples;
        int sampleIndex;
        int sampleCount;
        bool setNewWarp;
        int warpThreshold;

        int warpThresholdHight;
        int warpThresholdWidth;

        Configuration mConfiguration;


        //constructor

        public combineEyes(Configuration configuration)
        {
            // tracking Head
           

            //tracking Eye
            mConfiguration = configuration;
            samples = new Point[10];
            warpThreshold = 400;
            warpThresholdWidth = mConfiguration.MagnifierWidth;
            warpThresholdHight = mConfiguration.MagnifierHeight;

            Execute();
         
         }

        bool eyetrackerFind = false;
        public void Execute()
        {
            //tobi pro
            EyeTrackerCollection eyeTrackers = EyeTrackingOperations.FindAllEyeTrackers();
            //var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers();
            foreach (IEyeTracker eyeTracker in eyeTrackers)
            {
                Console.WriteLine("xxx" + eyeTracker.Address);
                if (eyeTracker != null)
                    GazeData(eyeTracker);

            }
            
        }
        //############################################################################################
        // <BeginExample>
        private void GazeData(IEyeTracker eyeTracker)
        {
            eyetrackerFind = true;
            eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
        }

        private void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            //if we become information from two Eye
            if (e.LeftEye.GazePoint.Validity == Validity.Valid && e.RightEye.GazePoint.Validity == Validity.Valid)
            {
                sampleCount++;
                sampleIndex++;
                if (sampleIndex >= samples.Length)
                    sampleIndex = 0;
                //combine to eye using tobii pro
                int x = ((int)(e.LeftEye.GazePoint.PositionOnDisplayArea.X * w) + (int)(e.RightEye.GazePoint.PositionOnDisplayArea.X * w)) / 2;
                int y = ((int)(e.LeftEye.GazePoint.PositionOnDisplayArea.Y * h) + (int)(e.RightEye.GazePoint.PositionOnDisplayArea.Y * h)) / 2;
                samples[sampleIndex] = new Point(x, y);
            }

            else if (e.LeftEye.GazePoint.Validity == Validity.Valid && e.RightEye.GazePoint.Validity == Validity.Invalid)
            {
                sampleCount++;
                sampleIndex++;
                if (sampleIndex >= samples.Length)
                    sampleIndex = 0;
                //combine to eye using tobii pro
                int x = (int)(e.LeftEye.GazePoint.PositionOnDisplayArea.X * w);
                int y = (int)(e.LeftEye.GazePoint.PositionOnDisplayArea.Y * h);
                samples[sampleIndex] = new Point(x, y);
            }

            else if (e.LeftEye.GazePoint.Validity == Validity.Invalid && e.RightEye.GazePoint.Validity == Validity.Valid)
            {
                sampleCount++;
                sampleIndex++;
                if (sampleIndex >= samples.Length)
                    sampleIndex = 0;
                //combine to eye using tobii pro
                int x = (int)(e.RightEye.GazePoint.PositionOnDisplayArea.X * w);
                int y = (int)(e.RightEye.GazePoint.PositionOnDisplayArea.Y * h);
                samples[sampleIndex] = new Point(x, y);
            }
        }

        //############################################################################################
        public bool IsStarted()
        {

            EngineStateValue<Tobii.Interaction.Framework.GazeTracking> status = Program.EyeXHost.States.GetGazeTrackingAsync().Result;
            return status.Value == Tobii.Interaction.Framework.GazeTracking.GazeTracked;

        }

        public bool IsWarpReady()
        {
            return sampleCount > 10;
        }



        public Point calculateSmoothedPoint()
        {
            return calculateTrimmedMean();
        }

        private Point calculateMean()
        {
            Point p = new Point(0, 0);
            for (int i = 0; i < samples.Length; i++)
            {
                p.X += samples[i].X;
                p.Y += samples[i].Y;
            }
            p.X /= samples.Length;
            p.Y /= samples.Length;

            return p;
        }

        private Point calculateTrimmedMean()
        {
            Point p = calculateMean();

            // Find the furthest point from the mean
            double maxDist = 0;
            int maxIndex = 0;
            for (int i = 0; i < samples.Length; i++)
            {
                double dist = Math.Pow(samples[i].X - p.X, 2) + Math.Pow(samples[i].Y - p.Y, 2);
                if (dist > maxDist)
                {
                    maxDist = dist;
                    maxIndex = i;
                }
            }

            // Calculate a new mean without the furthest point
            p = new Point(0, 0);
            for (int i = 0; i < samples.Length; i++)
            {
                if (i != maxIndex)
                {
                    p.X += samples[i].X;
                    p.Y += samples[i].Y;
                }
            }
            p.X /= (samples.Length - 1);
            p.Y /= (samples.Length - 1);

            return p;
        }

        private double calculateStdDev()
        {
            Point u = calculateMean();

            double o = 0;
            for (int i = 0; i < samples.Length; i++)
            {
                Point delta = Point.Subtract(samples[i], new System.Drawing.Size(u));
                o += Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2);
            }
            return Math.Sqrt(o / samples.Length);
        }

        public override String ToString()
        {
            return String.Format("({0:0}, {1:0})", samples[sampleIndex].X, samples[sampleIndex].Y);
        }

        public Point GetGazePoint()
        {
            return samples[sampleIndex];
        }

        public int GetSampleCount()
        {
            return sampleCount;
        }

        public int GetWarpTreshold()
        {
            return warpThreshold;
        }

        public int GetWarpThresholdHight()
        {
            return warpThresholdHight;
        }

        public int GetWarpThresholdWidth()
        {
            return warpThresholdWidth;
        }


        public Point GetWarpPoint()
        {
            return warpPoint;
        }

        public Point GetNextPoint(Point currentPoint)
        {
            Point smoothedPoint = calculateSmoothedPoint();
             Point delta = Point.Subtract(smoothedPoint, new System.Drawing.Size(warpPoint)); // whenever there is a big change from the past
            /* double distance = Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2));

             //???????????????????????
             if (!setNewWarp && distance > (GetWarpThresholdWidth() + GetWarpThresholdHight()) / 8)
             {
                 sampleCount = 0;
                 setNewWarp = true;
             }

            if (setNewWarp && IsWarpReady())
            {
                warpPoint = smoothedPoint;
                setNewWarp = false;
            }*/

             warpPoint = smoothedPoint;
           
            return warpPoint;
        }

        public void Dispose()
        {
            //stream.IsEnabled = false;
        }

        public void RefreshTracking()
        {
            sampleCount = 0;
            setNewWarp = true;
        }
    }
}