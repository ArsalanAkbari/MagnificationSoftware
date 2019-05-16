using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using Tobii.Interaction;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagnifierSoftwareV_1.EyeMove;
using MagnifierSoftwareV_1.MouseMove;
using MagnifierSoftwareV_1.EyeMove.Calibration;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using Tobii.Research;



namespace MagnifierSoftwareV_1 
{
    public partial class MagnifierMainForm : Form
    {
        //private Image mImageMagnifierMainControlPanel = null;
        private Point mPointMouseDown;
        private Point mLastCursorPosition;
        private Configuration mConfiguration = new Configuration();
        ConfigurationForm configForm;
        MagnifierForm magnifierForm;
        bool magnifierFormIsShown = false;
        public Timer mTimer;

       

        public MagnifierMainForm()
        {
            InitializeComponent();

            //CursorForm cF = new CursorForm();
           // cF.Show();

            GetConfiguration();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(HandleEsc);
            //--- My Init ---
            FormBorderStyle = FormBorderStyle.None;
            //TopMost = true;
            StartPosition = FormStartPosition.CenterScreen;

            //mTimer.Tick += new EventHandler(RefreshMagnifier);


            ShowInTaskbar = true;
        }


       

        private string mConfigFileName = "configData.xml";

        private void GetConfiguration()
        {
            try
            {
                mConfiguration = (Configuration)XmlUtility.Deserialize(mConfiguration.GetType(), mConfigFileName);
            }
            catch
            {
                mConfiguration = new Configuration();
            }
        }

        private void SaveConfiguration()
        {
            try
            {
                XmlUtility.Serialize(mConfiguration, mConfigFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Serialization problem: " + e.Message);
            }
        }

        private void Magnifier_Button_Click(object sender, EventArgs e)
        {
            int x = mLastCursorPosition.X;
            int y = mLastCursorPosition.Y;
            mLastCursorPosition = Cursor.Position;
            //EyeXWarpPointer eyeXWarpPointer = new EyeXWarpPointer(mConfiguration);

            magnifierForm = new MagnifierForm(mConfiguration, mLastCursorPosition);
            magnifierForm.Show();
            magnifierFormIsShown = true;

            //this.Hide();
        }

        private void Setup_button_Click(object sender, EventArgs e)
        {
            configForm = new ConfigurationForm(mConfiguration);
            configForm.ShowDialog(this);
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            Application.Exit();
        }
        /// <summary>
        /// /////////////////////////////////////////////////



        /*         // If mouse event handled by this hot-stop then return!
                 if (hotSpot.ProcessMouseMove(e))
                 {
                     Cursor = Cursors.Hand;
                     return;
                 }
             }
             Cursor = Cursors.SizeAll;

       */

        protected override void OnMouseUp(MouseEventArgs e)
        {

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            mPointMouseDown = new Point(e.X, e.Y);
            mLastCursorPosition = Cursor.Position;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EngineStateValue<Tobii.Interaction.Framework.GazeTracking> status = Program.EyeXHost.States.GetGazeTrackingAsync().Result;
            Console.WriteLine("XXXXX  " + Tobii.Interaction.Framework.GazeTracking.GazeTracked);

            /*var gazePointDataStream = host.Streams.CreateGazePointDataStream();
            gazePointDataStream.GazePoint((gazePointX, gazePointY, _) => Console.WriteLine("X: {0} Y:{1}", gazePointX, gazePointY));*/
            EytrackerInit eyI = new EytrackerInit();
            eyI.InitializeEyetracker();
        }

        private void MagnifierEye_Button_Click(object sender, EventArgs e)
        {
            Main main = new Main(mConfiguration);
            main.Show();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }

        }

        System.Windows.Forms.ToolTip toolTip_SetupButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_ExitButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_MouseMagnifierButto = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_EyeMagnifierButton = new System.Windows.Forms.ToolTip();

        private void ExitButtone_MouseHover(object sender, EventArgs e)
        {
            toolTip_ExitButton.SetToolTip(this.Exit_button, "Close");
        }

        private void SetupButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_SetupButton.SetToolTip(this.Setup_button, "Option");
        }

        private void MouseMagnifierButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_MouseMagnifierButto.SetToolTip(this.Magnifier_Button, "Magnifier Using Mouse");
        }

        private void EyeMagnifierButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_EyeMagnifierButton.SetToolTip(this.MagnifierEye_Button, "Magnifier Using EyeTracker");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            CreatMetaFile cMF = new CreatMetaFile();
                 Metafile mf = cMF.MakeMetafile(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, "test.emf");

                 // Draw on the metafile.
                 cMF.DrawOnMetafile(mf);


                 // Convert the metafile into a bitmap.
               //  Bitmap bm = cMF.MetafileToBitmap(mf);

                 // Display in various ways.
                // pictureBox1.Image = bm;  // Original size.
                // pictureBox2.Image = bm;  // Stretches pixelated.
                 pictureBox3.Image = mf;  // Stretches smoothly.
        }

        private void Calibration_Button_Click(object sender, EventArgs e)
        {
            var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();
            // EyeTrackerCollection eyeTrackers = EyeTrackingOperations.FindAllEyeTrackers();

            if (eyeTracker != null)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                     eyeTracker.Address,
                     eyeTracker.DeviceName,
                     eyeTracker.Model,
                     eyeTracker.SerialNumber,
                     eyeTracker.FirmwareVersion,
                     eyeTracker.RuntimeVersion);

                CalibrationForm t = new CalibrationForm(eyeTracker);
                t.Show();
                //CalibrationWindow cal = new CalibrationWindow(eyeTracker);
                //cal.Show();
            }
           
            
        }

        private void addUser_button_Click(object sender, EventArgs e)
        {
            if(mConfiguration.userNameList.Count == 0)
            {
                DialogResult result = MessageBox.Show("There is allready Profile in system."+"\nDo you want to add a new Profile?",
                    "Critical Warning",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button2);

                if(result == DialogResult.Yes)
                {
                    var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();
                    // EyeTrackerCollection eyeTrackers = EyeTrackingOperations.FindAllEyeTrackers();

                    if (eyeTracker == null)
                    {
                        MessageBox.Show("Cant find the Eye Tracker! \nPlease trye again.",
                        "Critical Warning",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
                    }


                    if (eyeTracker != null)
                    {
                        //FirstCalibrationWindow firstCalibrationWindow = new FirstCalibrationWindow();
                       // firstCalibrationWindow.Show();

                        CalibrationForm t = new CalibrationForm(eyeTracker);
                      
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = mLastCursorPosition.X;
            int y = mLastCursorPosition.Y;
            mLastCursorPosition = Cursor.Position;

            MouseMoveMagnifier mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition);
            mouseMove.Show();
        }
    }
}
