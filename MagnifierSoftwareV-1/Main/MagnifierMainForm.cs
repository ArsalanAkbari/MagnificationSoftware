using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using Tobii.Interaction;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;
using MagnifierSoftwareV_1.EyeMove;
using MagnifierSoftwareV_1.MouseMove;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using MagnifierSoftwareV_1.EvaluationEye;
using MagnifierSoftwareV_1.Calibration;
using Tobii.Research;



namespace MagnifierSoftwareV_1 
{
    public partial class MagnifierMainForm : Form
    {
        //private Image mImageMagnifierMainControlPanel = null;
        private Point mPointMouseDown;
        private Point mLastCursorPosition;
        private Configuration mConfiguration = new Configuration();
        private ConfigurationForm configForm;
        private bool magnifierFormIsShown = false;
        private OverlayEyeNew mg;
        private bool mgIsOpen = false;
        private MouseMoveMagnifier mouseMove;
        private bool mouseMoveIsOpen = false;




        public MagnifierMainForm()
        {

            InitializeComponent();

            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            this.Cursor = NativeMethods.LoadCustomCursor();
            

            GetConfiguration();
            label_ZoomFaktor.Text = mConfiguration.ZoomFactor.ToString();

            if (mConfiguration.HideMouseCursor == false)
            {
                MouseHidenMainForm_button.Image = Properties.Resources.icons8_mauszeiger_50;
                MouseHidenMainForm_button.Text = "Cursor hide";
            }
            else
            {
                MouseHidenMainForm_button.Image = Properties.Resources.rrrr;
                MouseHidenMainForm_button.Text = "Cursor show";
            }
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(HandleEsc);
            //--- My Init ---
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
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
            SaveConfiguration();
            Application.Exit();
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


      


        //nun Fullscreeen Mouse Move
        private void MagniferUsingMouse_button_Click(object sender, EventArgs e)
        {
            //MouseMoveMagnifier mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, false, this);
            mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, false, this);
            mouseMove.Show();
            mouseMoveIsOpen = true;

            TobiiCalibration_button.Enabled = false;
            MagniferUsingMouse_button.Enabled = false;
            FullscreenMaginfierEye_Button.Enabled = false;
            MaginfierEye_Button.Enabled = false;
            FullScreenMouse_Button.Enabled = false;
            Evaluation_button.Enabled = false;



        }

        //Fullscreeen Mouse Move
        private void FullScreenMouse_Click(object sender, EventArgs e)
        {
            //MouseMoveMagnifier mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, true, this);
            mouseMove = new MouseMoveMagnifier(mConfiguration, mLastCursorPosition, true, this);
            mouseMove.Show();
            mouseMoveIsOpen = true;

            TobiiCalibration_button.Enabled = false;
            MagniferUsingMouse_button.Enabled = false;
            FullscreenMaginfierEye_Button.Enabled = false;
            MaginfierEye_Button.Enabled = false;
            FullScreenMouse_Button.Enabled = false;
            Evaluation_button.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mgIsOpen = true;
            //MouseMoveMagnifier2 mg = new MouseMoveMagnifier2(mConfiguration , this);
            mg = new OverlayEyeNew(mConfiguration, this);
            mg.Show();

            TobiiCalibration_button.Enabled = false;
            MagniferUsingMouse_button.Enabled = false;
            FullscreenMaginfierEye_Button.Enabled = false;
            MaginfierEye_Button.Enabled = false;
            FullScreenMouse_Button.Enabled = false;
            Evaluation_button.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            // OverlayEyeNew mg = new OverlayEyeNew(mConfiguration, true, this);
            mg = new OverlayEyeNew(mConfiguration, true ,this);
            mg.Show();
            mgIsOpen = true;

            TobiiCalibration_button.Enabled = false;
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

        private void TobiiCalibration_button_MouseHover(object sender, EventArgs e)
        {
            toolTip_Calibration.SetToolTip(this.TobiiCalibration_button, "Tobii Calibration");
            TobiiCalibration_button.FlatAppearance.BorderColor = Color.DeepSkyBlue;
        }

        private void TobiiCalibration_button_MouseLeave(object sender, EventArgs e)
        {
            TobiiCalibration_button.FlatAppearance.BorderColor = Color.Black;
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
                //MouseHidenMainForm_button.Image = Properties.Resources.cursor__1_;
                MouseHidenMainForm_button.Image = Properties.Resources.rrrr;
                MouseHidenMainForm_button.Text = "Cursor show";
            }
            else if (mConfiguration.HideMouseCursor == true)
            {
                mConfiguration.HideMouseCursor = false;
                //MouseHidenMainForm_button.Image = Properties.Resources.cursor2;
                MouseHidenMainForm_button.Image = Properties.Resources.icons8_mauszeiger_50;
                MouseHidenMainForm_button.Text = "Cursor hide";
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


      //eyetracking init
        private void button2_Click(object sender, EventArgs e)
        {
            /* NativeMethods.MagShowSystemCursor(true);

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
              Evaluation_button.Enabled = true;*/

            EytrackerInit init = new EytrackerInit();
            
        }

        private void HelpToturial_button_Click(object sender, EventArgs e)
        {
            Tutorial tutorialForm = new Tutorial();
            tutorialForm.Show();
        }



        const int HTCAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0xA1;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void Label6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void Button1_Click_2(object sender, EventArgs e)
        {
            CalibrationTobiiInformations calibrationTobii = new CalibrationTobiiInformations(mConfiguration);
            calibrationTobii.ShowDialog(this);
        }

    }
}
