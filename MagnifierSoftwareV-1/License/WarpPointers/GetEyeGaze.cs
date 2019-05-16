using System;
using System.Drawing;
using Tobii.Research;
using System.Windows.Forms;
using Tobii.Interaction;

namespace MagnifierSoftwareV_1.EyeMove.WarpPointers
{
    class GetEyeGaze : WarpPointer
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

        public GetEyeGaze(Configuration configuration)
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

            /* if(mConfiguration.bothEyeAndHead || mConfiguration.bothEye)
                eyeTracker.GazeDataReceived += EyeTrackerBothEye_GazeDataReceived;

            else if (mConfiguration.leftEye || mConfiguration.leftEyeAndHead)
                eyeTracker.GazeDataReceived += EyeTrackerLeftEye_GazeDataReceived;

            else if (mConfiguration.rightEye || mConfiguration.rightEyeAndHead)
                eyeTracker.GazeDataReceived += EyeTrackerRightEye_GazeDataReceived;

           /* else
            {
                DialogResult result = MessageBox.Show("Cant Find any Eyetracker.\n Do you want To retry?",
                   "Critical Warning",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    Execute();
                }

                if (result == DialogResult.No)
                {
                    Console.WriteLine("Problem is in get Gaze Class");
                    Application.Exit();
                }
            }*/
        }

        private void EyeTrackerBothEye_GazeDataReceived(object sender, GazeDataEventArgs e)
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

        private void EyeTrackerLeftEye_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            if (e.LeftEye.GazePoint.Validity == Validity.Valid)
            {
                sampleCount++;
                sampleIndex++;
                if (sampleIndex >= samples.Length)
                    sampleIndex = 0;
                samples[sampleIndex] = new Point((int)(e.LeftEye.GazePoint.PositionOnDisplayArea.X * w), (int)(e.LeftEye.GazePoint.PositionOnDisplayArea.Y * h));
            }
        }

        private void EyeTrackerRightEye_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            if (e.RightEye.GazePoint.Validity == Validity.Valid)
            {
                sampleCount++;
                sampleIndex++;
                if (sampleIndex >= samples.Length)
                    sampleIndex = 0;
                samples[sampleIndex] = new Point((int)(e.RightEye.GazePoint.PositionOnDisplayArea.X * w), (int)(e.RightEye.GazePoint.PositionOnDisplayArea.Y * h));
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
            double distance = Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2));
            //???????????????????????
            if (!setNewWarp && distance > (GetWarpThresholdWidth() + GetWarpThresholdHight()) / 4)
            {
                sampleCount = 0;
                setNewWarp = true;
            }

            if (setNewWarp && IsWarpReady())
            {
                warpPoint = smoothedPoint;
                setNewWarp = false;
            }

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