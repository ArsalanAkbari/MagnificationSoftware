using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using Tobii.Interaction;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MagnifierSoftwareV_1.EyeMove;
using MagnifierSoftwareV_1.MouseMove;
using MagnifierSoftwareV_1.EyeMove.Calibration;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using MagnifierSoftwareV_1.EvaluationEye;
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

        OverlayEyeNew mg;
        bool mgIsOpen = false;
        MouseMoveMagnifier mouseMove;
        bool mouseMoveIsOpen = false;




        public MagnifierMainForm()
        {
            InitializeComponent();

            this.Cursor = NativeMethods.LoadCustomCursor();
            //CursorForm cF = new CursorForm();
            // cF.Show();

            button_BackToMain.Hide();

            GetConfiguration();
            label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();

            if (mConfiguration.HideMouseCursor == false)
            {
                MouseHidenMainForm_button.Image = Properties.Resources.cursor2;
                MouseHidenMainForm_button.Text = "Cursor Hide";
            }
            else
            {
                MouseHidenMainForm_button.Image = Properties.Resources.cursor__1_;
                MouseHidenMainForm_button.Text = "Cursor Show";
            }
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

      

        private void Setup_button_Click(object sender, EventArgs e)
        {
            configForm = new ConfigurationForm(mConfiguration, this);
            configForm.ShowDialog(this);
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            button_BackToMain.Hide();

            SaveConfiguration();
            Application.Exit();
        }
       

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
                SaveConfiguration();
                //Application.Exit();
            }

        }

      

        private void button1_Click_1(object sender, EventArgs e)
        {

            CreatMetaFile cMF = new CreatMetaFile();
                 Metafile mf = cMF.MakeMetafile(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, "test.emf");

                 // Draw on the metafile.
                 cMF.DrawOnMetafile(mf);
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



        //nun Fullscreeen Mouse Move
        private void MagniferUsingMouse_button_Click(object sender, EventArgs e)
        {
            button_BackToMain.Show();
            //MouseMoveMagnifier mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, false, this);
            mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, false, this);
            mouseMove.Show();
            mouseMoveIsOpen = true;

            MagniferUsingMouse_button.Enabled = false;
            FullscreenMaginfierEye_Button.Enabled = false;
            MaginfierEye_Button.Enabled = false;
            FullScreenMouse_Button.Enabled = false;
            Evaluation_button.Enabled = false;



        }

        //Fullscreeen Mouse Move
        private void FullScreenMouse_Click(object sender, EventArgs e)
        {
            button_BackToMain.Show();
            //MouseMoveMagnifier mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, true, this);
            mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, true, this);
            mouseMove.Show();
            mouseMoveIsOpen = true;


            MagniferUsingMouse_button.Enabled = false;
            FullscreenMaginfierEye_Button.Enabled = false;
            MaginfierEye_Button.Enabled = false;
            FullScreenMouse_Button.Enabled = false;
            Evaluation_button.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mgIsOpen = true;
            //Window Mode
            button_BackToMain.Show();
           // OverlayEyeNew mg = new OverlayEyeNew(mConfiguration , this);
            mg = new OverlayEyeNew(mConfiguration, this);
            mg.Show();

            MagniferUsingMouse_button.Enabled = false;
            FullscreenMaginfierEye_Button.Enabled = false;
            MaginfierEye_Button.Enabled = false;
            FullScreenMouse_Button.Enabled = false;
            Evaluation_button.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //fullscreen Mode= true
            button_BackToMain.Show();
            // OverlayEyeNew mg = new OverlayEyeNew(mConfiguration, true, this);
            mg = new OverlayEyeNew(mConfiguration, true ,this);
            mg.Show();
            mgIsOpen = true;

            MagniferUsingMouse_button.Enabled = false;
            FullscreenMaginfierEye_Button.Enabled = false;
            MaginfierEye_Button.Enabled = false;
            FullScreenMouse_Button.Enabled = false;
            Evaluation_button.Enabled = false;


        }

        private void Evaluation_button_Click(object sender, EventArgs e)
        {
            InformationForm infoEvaluationForm = new InformationForm(this ,mConfiguration);
            this.Enabled = false;
            this.Hide();
            infoEvaluationForm.Show();
        }



        System.Windows.Forms.ToolTip toolTip_SetupButton = new System.Windows.Forms.ToolTip();
        //System.Windows.Forms.ToolTip toolTip_ExitButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_MouseMagnifierButto = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_EyeMagnifierButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_TestingButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_MagnifyingGlass = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_FullscreenMaginfierEye = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_MagniferUsingMouse = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_FullScreenMouse = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_Calibration = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_MouseHidenMainForm = new System.Windows.Forms.ToolTip();



        private void SetupButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_SetupButton.SetToolTip(this.Setup_button, "Option");
            Setup_button.FlatAppearance.BorderColor = Color.DeepSkyBlue;
        }
        private void SetupButton_MouseLeave(object sender, EventArgs e)
        {
            Setup_button.FlatAppearance.BorderColor = Color.DarkGray;
        }
       

        private void MouseMagnifierButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_MouseMagnifierButto.SetToolTip(this.Magnifier_Button, "Magnifier Using Mouse");
        }

        private void EyeMagnifierButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_EyeMagnifierButton.SetToolTip(this.MagnifierEye1_Button, "Magnifier Using EyeTracker");
        }

        private void TestingEyetracker__MouseHover(object sender, EventArgs e)
        {
            toolTip_TestingButton.SetToolTip(this.Evaluation_button, "Testing EyeTracker");
            Evaluation_button.FlatAppearance.BorderColor = Color.DeepSkyBlue;
        }

        private void TestingEyetracker__MouseLeave(object sender, EventArgs e)
        {
            Evaluation_button.FlatAppearance.BorderColor = Color.DarkGray;

        }


        private void MagnifyingGlass_MouseHover(object sender, EventArgs e)
        {
            toolTip_MagnifyingGlass.SetToolTip(this.MaginfierEye_Button, "Magnifying Glass");
            MaginfierEye_Button.FlatAppearance.BorderColor = Color.DeepSkyBlue;


        }

        private void MagnifyingGlass_MouseLeave(object sender, EventArgs e)
        {
            MaginfierEye_Button.FlatAppearance.BorderColor = Color.Black;

        }

        private void FullscreenMaginfierEye_MouseHover(object sender, EventArgs e)
        {
            toolTip_FullscreenMaginfierEye.SetToolTip(this.FullscreenMaginfierEye_Button, "Fullscreen Magnifying");
            FullscreenMaginfierEye_Button.FlatAppearance.BorderColor = Color.DeepSkyBlue;


        }
        private void FullscreenMaginfierEye_MouseLeave(object sender, EventArgs e)
        {
            FullscreenMaginfierEye_Button.FlatAppearance.BorderColor = Color.Black;

        }

        private void MagniferUsingMouse_MouseHover(object sender, EventArgs e)
        {
            toolTip_MagniferUsingMouse.SetToolTip(this.MagniferUsingMouse_button, "Magnifying Glass");
            MagniferUsingMouse_button.FlatAppearance.BorderColor = Color.DeepSkyBlue;

        }
        private void MagniferUsingMouse_MouseLeave(object sender, EventArgs e)
        {
            MagniferUsingMouse_button.FlatAppearance.BorderColor = Color.Black;

        }


        private void FullScreenMouse_MouseHover(object sender, EventArgs e)
        {
            toolTip_FullScreenMouse.SetToolTip(this.FullScreenMouse_Button, "Fullscreen Magnifying");
            FullScreenMouse_Button.FlatAppearance.BorderColor = Color.DeepSkyBlue;


        }
        private void FullScreenMouse_MouseLeave(object sender, EventArgs e)
        {
            FullScreenMouse_Button.FlatAppearance.BorderColor = Color.Black;

        }


        private void Calibration_MouseHover(object sender, EventArgs e)
        {
            toolTip_Calibration.SetToolTip(this.Calibration_Button, "Calibration");
            Calibration_Button.FlatAppearance.BorderColor = Color.DeepSkyBlue;

        }
        private void Calibration_MouseLeave(object sender, EventArgs e)
        {
            Calibration_Button.FlatAppearance.BorderColor = Color.White;

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderColor = Color.Blue;

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderColor = Color.White;

        }

       

        private void MiniMize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MouseHidenMainForm_MouseHover(object sender, EventArgs e)
        {
            toolTip_EyeMagnifierButton.SetToolTip(this.MouseHidenMainForm_button, "Hide Pointer");
            MouseHidenMainForm_button.FlatAppearance.BorderColor = Color.DeepSkyBlue;

        }

        private void MouseHidenMainForm_MouseLeave(object sender, EventArgs e)
        {
            MouseHidenMainForm_button.FlatAppearance.BorderColor = Color.DarkGray;

        }

        private void MouseHidenMainForm_button_Click(object sender, EventArgs e)
        {
            if (mConfiguration.HideMouseCursor == false)
            {
                mConfiguration.HideMouseCursor = true;
                MouseHidenMainForm_button.Image = Properties.Resources.cursor__1_;
                MouseHidenMainForm_button.Text = "Cursor Show";
            }
            else
            {
                mConfiguration.HideMouseCursor = false;
                MouseHidenMainForm_button.Image = Properties.Resources.cursor2;
                MouseHidenMainForm_button.Text = "Cursor Hide";
            }
        }

        private void ZoomIn_button_Click(object sender, EventArgs e)
        {
            if (mConfiguration.ZoomFactor != 10)
            {
                mConfiguration.ZoomFactor += 1;
                label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
            }
        }

        private void ZoomOutbutton_Click(object sender, EventArgs e)
        {
            if (mConfiguration.ZoomFactor != 1)
            {
                mConfiguration.ZoomFactor -= 1;
                label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            NativeMethods.MagShowSystemCursor(true);

             if (mgIsOpen)
             {
                 //mg.Dispose();
                 mg.exit();
                // mg.Close();
                 mgIsOpen = false;
                 button_BackToMain.Hide();
             }

              if (mouseMoveIsOpen)
             {
                 mouseMove.exit();
                 mouseMoveIsOpen = false;
                 button_BackToMain.Hide();
             }

             FullScreenMouse_Button.Enabled = true;
             FullscreenMaginfierEye_Button.Enabled = true;
             MaginfierEye_Button.Enabled = true;
             MagniferUsingMouse_button.Enabled = true;
             Evaluation_button.Enabled = true;
            
        }

        private void HelpToturial_button_Click(object sender, EventArgs e)
        {
            Tutorial tutorialForm = new Tutorial();
            tutorialForm.Show();
        }
    }
}
