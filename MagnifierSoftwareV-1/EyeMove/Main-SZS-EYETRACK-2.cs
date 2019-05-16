using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using System.Windows.Forms;


namespace MagnifierSoftwareV_1.EyeMove
{
    public partial class Main : Form
    {
        MouseController controller;
        OverlayForm overlay;
        GlobalKeyboardHook _globalKeyboardHook;
        Keys movementHotKey;
        Keys clickHotKey;
        Keys pauseHotKey;
        Configuration mConfiguration;
        Timer refreshTimer;
        bool isFirstTime = true;

        OverlayEye overlayEyeForm;

        public Main(Configuration configuration)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(HandleEsc);
            TopMost = true;
            mConfiguration = configuration;
            controller = new MouseController(this, mConfiguration);

            // controller.setMode(MouseController.Mode.EYEX_AND_EVIACAM);
            controller.setMovement(MouseController.Movement.HOTKEY);
            controller.Sensitivity = SensitivityInput.Value;

            controller.setMovement(MouseController.Movement.CONTINUOUS);
            //movementHotKey = (Keys)Enum.Parse(typeof(Keys), Properties.Settings.Default.MovementKey);
            //clickHotKey = (Keys)Enum.Parse(typeof(Keys), Properties.Settings.Default.ClickOnKey);
            //pauseHotKey = (Keys)Enum.Parse(typeof(Keys), Properties.Settings.Default.PauseOnKey);

            _globalKeyboardHook = new GlobalKeyboardHook();
            //  _globalKeyboardHook.KeyboardPressed += OnKeyPressed;

            controller.setMode(MouseController.Mode.EYEX_ONLY);
            //warpBar.Enabled = true;
            gazeTracker.Enabled = true;

            OverlayEye overlayEyeForm = new OverlayEye(mConfiguration, controller);
            //overlayEyeForm.Show();
            controller.Sensitivity = 7;

           OverlayEyeNew overlayEyeNew = new OverlayEyeNew(mConfiguration, MousePosition);
            overlayEyeNew.Show();

            overlay = new OverlayForm(controller, mConfiguration, overlayEyeForm );
            overlay.ShowWarpBar = true;
            overlay.ShowGazeTracker = false;
            overlay.ShowIfTracking();


            refreshTimer = new Timer();
            refreshTimer.Tick += new EventHandler(RefreshScreen);
            refreshTimer.Tick += new EventHandler(overlayEyeForm.HandleTimer);
            //refreshTimer.Tick += new EventHandler(overlayEyeNew.HandleTimer);
            refreshTimer.Interval = 33;
            refreshTimer.Start();
        }


        private void RefreshScreen(Object o, EventArgs e)
        {
            GetConfiguration();
            controller.UpdateMouse(Cursor.Position);
            this.Invalidate();
            overlay.Invalidate();
            //overlayEyeForm.Refresh();


            // Point p = controller.WarpPointer.GetWarpPoint();
            // overlayEyeForm.mTargetPoint = PointToScreen(new Point(p.X , p.Y ));

            //overlayEyeForm.CaptureScreen();
            // overlayEyeForm.Invalidate();
        }

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


        private void ModeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (controller == null)
                return;

            System.Windows.Forms.ComboBox box = (System.Windows.Forms.ComboBox)sender;
            switch ((String)box.SelectedItem)
            {
                case "EyeX Only":
                    controller.setMode(MouseController.Mode.EYEX_ONLY);
                    gazeTracker.Enabled = true;
                    overlay.ShowIfTracking();
                    break;

                case "EyeX Only Left Eye":
                    controller.setMode(MouseController.Mode.EYEX_ONLY_LeftEye);
                    gazeTracker.Enabled = true;
                    overlay.ShowIfTracking();                        
                    break;

                case "EyeX Only Right Eye":
                    controller.setMode(MouseController.Mode.EYEX_ONLY_RightEye);
                    gazeTracker.Enabled = true;
                    overlay.ShowIfTracking();
                    break;

                case "Eye and Head Combine":
                    controller.setMode(MouseController.Mode.EYE_HEAD_COMBINE);
                    gazeTracker.Enabled = true;
                    overlay.ShowIfTracking();
                    break;

                case "EyeX Only Head":
                    controller.setMode(MouseController.Mode.EYEX_ONLY_Head);
                    gazeTracker.Enabled = false;
                    overlay.Hide();
                    break;
                
                default:
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Update Labels
            PositionLabel.Text = controller.WarpPointer.ToString();
            HeadRotationLabel.Text = controller.PrecisionPointer.ToString();

        }
        public static Rectangle GetScreenSize()
        {
            return Screen.PrimaryScreen.Bounds;
        }

        public void SetMousePosition(Point p)
        {
            Cursor.Position = p;
        }

        /* private void MovementOnKeyPressButton_Click(object sender, KeyEventArgs e)
         {
             e.SuppressKeyPress = true;
             MovementOnKeyPressInput.Text = e.KeyCode.ToString();
             movementHotKey = e.KeyCode;
         }*/


        private void SensitivityInput_Scroll(object sender, EventArgs e)
        {
            controller.Sensitivity = SensitivityInput.Value;

        }

        private string mConfigFileName = "configData.xml";

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

        private void gazeTracker_CheckedChanged_1(object sender, EventArgs e)
        {
            overlay.ShowGazeTracker = gazeTracker.Checked;
            overlay.ShowIfTracking();
        }

        private void ContinuousButton_CheckedChanged_1(object sender, EventArgs e)
        {
            //OnKeyPressButton.Checked = false;
            controller.setMovement(MouseController.Movement.CONTINUOUS);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //controller.setMovement(MouseController.Movement.HOTKEY);
                SaveConfiguration();
                // overlayEyeForm.Close();
                overlay.Close();              
                this.Close();
            }

        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
           // overlayEyeForm.Close();
            overlay.Close();

            this.Close();
        }

    }
}
