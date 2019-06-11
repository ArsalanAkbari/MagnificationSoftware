using System;
using System.Drawing;
using Tobii.Research;
using System.Windows.Forms;
using Tobii.Interaction;


namespace MagnifierSoftwareV_1.EyeMove.WarpPointers
{
    public class OneEyeRight
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

        public OneEyeRight(Configuration configuration)
        {
            mConfiguration = configuration;

            samples = new Point[10];
            warpThreshold = 400;
            warpThresholdWidth = mConfiguration.MagnifierWidth;
            warpThresholdHight = mConfiguration.MagnifierHeight;

            //tobi pro
            Execute();

            /* stream = Program.EyeXHost.Streams.CreateGazePointDataStream();
             if (stream != null)
             {
                 stream.IsEnabled = true;
                 stream.Next += UpdateGazePosition;
             }*/
        }


        /* protected void UpdateGazePosition(object s, StreamData<GazePointData> streamData)
       {
           sampleCount++;
           sampleIndex++;
           if (sampleIndex >= samples.Length)
               sampleIndex = 0;
           samples[sampleIndex] = new Point((int)streamData.Data.X, (int)streamData.Data.Y);
       }*/

        //############################################################################################

        public void Execute()
        {
            EyeTrackerCollection eyeTrackers = EyeTrackingOperations.FindAllEyeTrackers();
            //var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers();
            foreach (IEyeTracker eyeTracker in eyeTrackers)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", eyeTracker.Address, eyeTracker.DeviceName, eyeTracker.Model, eyeTracker.SerialNumber, eyeTracker.FirmwareVersion, eyeTracker.RuntimeVersion);

                GazeData(eyeTracker);

            }
        }
        // <BeginExample>
        private void GazeData(IEyeTracker eyeTracker)
        {
            // Start listening to gaze data.
            eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;

            // Wait for some data to be received.
            //System.Threading.Thread.Sleep(2000);
            // Stop listening to gaze data.
            // eyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
        }
        private void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
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

            /* Console.WriteLine(
                 "Got gaze data with {0} left eye origin at point ({1}, {2}) in the user coordinate system.",
                 e.RightEye.GazePoint.Validity,
                 e.RightEye.GazePoint.PositionOnDisplayArea.X*w,
                 e.RightEye.GazePoint.PositionOnDisplayArea.Y*h);*/

        }

        //############################################################################################
 
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
          
            warpPoint = smoothedPoint;
         

            return warpPoint;
        }

    }
}