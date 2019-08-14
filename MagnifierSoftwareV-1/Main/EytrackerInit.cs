using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tobii.Research;

namespace MagnifierSoftwareV_1
{
    class EytrackerInit
    {

        public EytrackerInit()
        {
            InitializeEyetracker();
        }

        public void InitializeEyetracker()
        {
            var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();
            getEyetrackerInformation();
            ApplyLicense(eyeTracker, "C:\\Users\\Eyetracker\\OneDrive\\Eye tracking\\Magnifier Projects\\Experimentssss\\MagnifierLast\\MagnifierLast\\license\\license_key_00454713__-__Karlsruhe_Institute_of_IS404-100108245121");
            //ApplyLicense(eyeTracker, "C:\\Users\\......Path to license file.....");
           
        }


        static EyeTrackerCollection getEyetrackerInformation()
        {
            // <BeginExample>
            Console.WriteLine("\nSearching for all eye trackers");
            EyeTrackerCollection eyeTrackers = EyeTrackingOperations.FindAllEyeTrackers();
            foreach (IEyeTracker eyeTracker in eyeTrackers)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                    eyeTracker.Address,
                    eyeTracker.DeviceName,
                    eyeTracker.Model,
                    eyeTracker.SerialNumber,
                    eyeTracker.FirmwareVersion,
                    eyeTracker.RuntimeVersion);
            }
            // <EndExample>
            return eyeTrackers;
        }


        void ApplyLicense(IEyeTracker eyeTracker, string licensePath)
        {
            // Create a collection with the license.
            var licenseCollection = new LicenseCollection(new System.Collections.Generic.List<LicenseKey>{
                new LicenseKey(System.IO.File.ReadAllBytes(licensePath))
                });
            // See if we can apply the license.
            FailedLicenseCollection failedLicenses;
            if (eyeTracker.TryApplyLicenses(licenseCollection, out failedLicenses))
            {
                Console.WriteLine(
                "Successfully applied license from {0} on eye tracker with serial number {1}.",
                licensePath, eyeTracker.SerialNumber);
            }
            else
            {
                Console.WriteLine(
                "Failed to apply license from {0} on eye tracker with serial number {1}.\n" +
                "The validation result is {2}.",
                licensePath, eyeTracker.SerialNumber, failedLicenses[0].ValidationResult);
            }
        }
    }
}