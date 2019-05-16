using System.Drawing;


namespace MagnifierSoftwareV_1.EyeMove
{
    public interface WarpPointer : System.IDisposable
    {

        // Whether it's started tracking gaze
        bool IsStarted();

        // Whether it's ready to warp to a new point
        bool IsWarpReady();

        // Smoothed point for drawing
        Point calculateSmoothedPoint();

        // Gaze point for drawing
        Point GetGazePoint();

        // Sample count for printing
        int GetSampleCount();

        // Warp threshold in pixels
        int GetWarpTreshold();

        int GetWarpThresholdHight();

        int GetWarpThresholdWidth();

        // Warp point for drawing, no update made
        Point GetWarpPoint();

        // Get the next warp point based on the current pointer location and gaze
        Point GetNextPoint(Point currentPoint);

        // Refresh the tracking buffer for a fresh start
        void RefreshTracking();
    }
}
