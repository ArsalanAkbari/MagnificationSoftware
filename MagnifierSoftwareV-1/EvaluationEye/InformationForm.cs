using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace MagnifierSoftwareV_1.EvaluationEye
{
    public partial class InformationForm : Form
    {

       
        private string selectedPath = "";

        private int mSensitivityOfTest;
        private string mConfigFileName = "configData.xml";

        Configuration mConfiguration;
        private Timer timer = new Timer();

        MagnifierMainForm mMainForm;


        public InformationForm(MagnifierMainForm mainForm , Configuration Configuration)
        {
            InitializeComponent();
            this.Cursor = NativeMethods.LoadCustomCursor();
            this.KeyPreview = true;
            mMainForm = mainForm;
            this.KeyDown += new KeyEventHandler(HandleEsc);
            FormBorderStyle = FormBorderStyle.None;
            //this.TopMost = true;
            this.DoubleBuffered = true;
            mConfiguration = Configuration;
            lbl_SensitivityFactor.Text = mConfiguration.sensitivityOfTest.ToString();
            tb_SensitivityFactor.Value = mConfiguration.sensitivityOfTest;
            mSensitivityOfTest = mConfiguration.sensitivityOfTest;

            timer.Enabled = true;
            timer.Tick += new EventHandler(refreshScreen);
            timer.Interval = 40; // 40 images per second.
            timer.Start();

        }

        private void refreshScreen(object sender, System.EventArgs e)
        {
            this.Invalidate();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SaveConfiguration();
                timer.Stop();
                timer.Enabled = false;
                mMainForm.Enabled = true;
                mMainForm.Show();
                this.Close();
            }

        }

        private void choseFolder_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            fBD.Description = "Chose a path to save your Test Image.";
            if (fBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedPath = fBD.SelectedPath;
                MessageBox.Show(fBD.SelectedPath);
            }
        }

        private void startTestButton_Click(object sender, EventArgs e)
        {
            if (userName_textBox.Text != "")
            {
                /* if (BothEye_checkBox.Checked)
                 {
                     //test test = new test(mConfiguration, userName_textBox.Text, selectedPath ,"BothEye" );
                     //test.Show();
                     CalibrationForm calibration = new CalibrationForm(mConfiguration, userName_textBox.Text, selectedPath, "BothEye");
                     calibration.Show();
                 }

                 else if (RightEye_checkBox.Checked)
                 {
                     //test test = new test(mConfiguration, userName_textBox.Text, selectedPath, "RightEye" );
                     //test.Show();
                     CalibrationForm calibration = new CalibrationForm(mConfiguration, userName_textBox.Text, selectedPath, "RightEye");
                     calibration.Show();
                 }

                 else if (LeftEye_checkBox.Checked)
                 {
                     //test test = new test(mConfiguration, userName_textBox.Text, selectedPath, "LeftEye" );
                     //test.Show();
                     CalibrationForm calibration = new CalibrationForm(mConfiguration, userName_textBox.Text, selectedPath, "LeftEye");
                     calibration.Show();
                 }*/

                CalibrationForm calibration = new CalibrationForm(mConfiguration, userName_textBox.Text, selectedPath);
                calibration.Show();

            }
            else
            {
                DialogResult result = MessageBox.Show("Pleas Enter the User's Name and Select Test Mode!",
                 "Critical Warning",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2);
            }
        }

        System.Windows.Forms.ToolTip toolTip_StartTestButton = new System.Windows.Forms.ToolTip();

        private void startTestButton_MouseHover(object sender, EventArgs e)
        {

            toolTip_StartTestButton.SetToolTip(this.startTestButton, "Start the Test");
            startTestButton.FlatAppearance.BorderColor = Color.DeepSkyBlue;
        }

        private void startTestButton_MouseLeave(object sender, EventArgs e)
        {
            startTestButton.FlatAppearance.BorderColor = Color.White;

        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            mMainForm.Enabled = true;
            mMainForm.Show();
            timer.Stop();
            timer.Enabled = false;
            this.Close();
        }

        private void MiniMize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

      


        private void traceBaarSensitivityUpbutton_Click(object sender, EventArgs e)
        {
            if (mConfiguration.sensitivityOfTest != 70)
            {
                tb_SensitivityFactor.Value +=1;
                mConfiguration.sensitivityOfTest = tb_SensitivityFactor.Value +2;
                lbl_SensitivityFactor.Text = tb_SensitivityFactor.Value.ToString();
            }
        }

     

        private void traceBaarSensitivityDownbutton_Click(object sender, EventArgs e)
        {
            if (mConfiguration.sensitivityOfTest >= 10)
            {
                tb_SensitivityFactor.Value -= 1;
                mConfiguration.sensitivityOfTest = tb_SensitivityFactor.Value - 2;
                lbl_SensitivityFactor.Text = tb_SensitivityFactor.Value.ToString();
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

        private void tb_SensitivityFactor_Scroll(object sender, EventArgs e)
        {

            TrackBar tb = (TrackBar)sender;
            mConfiguration.sensitivityOfTest = tb.Value;
            lbl_SensitivityFactor.Text = tb_SensitivityFactor.Value.ToString();
        }

  
        private void InformationForm_Paint(object sender, PaintEventArgs e)
        {
            int radius = tb_SensitivityFactor.Value / 2;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(Brushes.Red, 130, 200, tb_SensitivityFactor.Value, tb_SensitivityFactor.Value);
            e.Graphics.FillEllipse(Brushes.Black, 130 + radius -3, 200 + radius-3 , 6, 6);

            //e.Graphics.FillRectangle(Brushes.Red, 550, 425, tb_SensitivityFactor.Value, tb_SensitivityFactor.Value);
            // e.Graphics.FillEllipse(Brushes.Black, 550 + radius - 3, 425 + radius - 3, 6, 6);
        }

        private void button_OpenTestFolder_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            bool existsMainDirectory = System.IO.Directory.Exists(path + "\\" + "Eyetracking-Test-Results");

            if (existsMainDirectory)
                System.Diagnostics.Process.Start(path + "\\" + "Eyetracking-Test-Results");


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

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
