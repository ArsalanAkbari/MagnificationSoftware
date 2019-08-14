﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text;
using System.Reflection;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace MagnifierSoftwareV_1.MouseMove
{
    public partial class ConfigurationForm : Form
    {
        Configuration mConfiguration;
        Rectangle MainWindowsRectangle;
        Rectangle magnifierWindowsRectangle;
        MagnifierMainForm mMagnifierMainForm;
        //MouseController controller;
        //OverlayEyeNew overlayEyeNew;
        Timer refreshTimer;
        private string mConfigFileName = "configData.xml";

        public ConfigurationForm(Configuration configuration, MagnifierMainForm magnifierMainForm)
        {
            InitializeComponent();

            this.Location = new Point(0,0);

            this.Cursor = NativeMethods.LoadCustomCursor();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(HandleEsc);

            mConfiguration = configuration;

            ChangeWIMcolor_button.BackColor = Color.FromName(mConfiguration.whereIAmColor);

            //  overlayEyeNew = new OverlayEyeNew(mConfiguration);
            // controller = new MouseController(overlayEyeNew, mConfiguration);
            mMagnifierMainForm = magnifierMainForm;
            FormBorderStyle = FormBorderStyle.None;

            Sensititivity_label.Text = mConfiguration.senitivityOfHeadTracking.ToString();
            Sensititvity_trackBar.Value = mConfiguration.senitivityOfHeadTracking;


            tb_ZoomFactor.Maximum = (int)Configuration.ZOOM_FACTOR_MAX;
            tb_ZoomFactor.Minimum = (int)Configuration.ZOOM_FACTOR_MIN;
            tb_ZoomFactor.Value = (int)mConfiguration.ZoomFactor;

            cb_Symmetry.Checked = mConfiguration.keepSymmetric;

            tb_SpeedFactor.Maximum = (int)(100 * Configuration.SPEED_FACTOR_MAX);
            tb_SpeedFactor.Minimum = (int)(100 * Configuration.SPEED_FACTOR_MIN);
            tb_SpeedFactor.Value = (int)(100 * mConfiguration.SpeedFactor);

            tb_Width.Maximum = 10;
            tb_Width.Minimum = 1;
            tb_Width.Value = mConfiguration.MagnifierWidth / (Screen.PrimaryScreen.Bounds.Width / 10);

            tb_Height.Maximum = 9;
            tb_Height.Minimum = 1;
            tb_Height.Value = mConfiguration.MagnifierHeight/ (Screen.PrimaryScreen.Bounds.Height / 10);


            lbl_ZoomFactor.Text = mConfiguration.ZoomFactor.ToString();
            lbl_SpeedFactor.Text = mConfiguration.SpeedFactor.ToString();
            lbl_Width.Text = mConfiguration.MagnifierWidth.ToString();
            lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();

            SetComboBox1Text();
            SetComboBoxColorBlindnessText();

            //--- Init Boolean Settings ---
           
            //cb_reverseColor.Checked = mConfiguration.reverseColor;

            refreshTimer = new Timer();
            refreshTimer.Tick += new EventHandler(RefreshScreen);
            refreshTimer.Interval = 33;
            refreshTimer.Start();

            ShowInTaskbar = true;
        }



        public void RefreshScreen(object sender, EventArgs e)
        {
            this.Invalidate();
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


        //Set ComboBox Text
        private void SetComboBox1Text()
        {
            if (mConfiguration.bothEye)
                comboBox1.Text = "Both eyes";
            else if (mConfiguration.bothEyeAndHead)
                comboBox1.Text = "Both eyes and head";

            else if (mConfiguration.rightEye)
                comboBox1.Text = "Right eye";
            else if (mConfiguration.rightEyeAndHead)
                comboBox1.Text = "Right eye and head";

            else if (mConfiguration.leftEye)
                comboBox1.Text = "Left eye";
            else if (mConfiguration.leftEyeAndHead)
                comboBox1.Text = "Left eye and head";

            else if (mConfiguration.justHead)
                comboBox1.Text = "Just head";
        }

        //Set ComboBox Text


        private void SetComboBoxColorBlindnessText()
        {
            if (mConfiguration.normal == true)
                Color_Blindness_comboBox.Text = "Normal";

            else if (mConfiguration.achromatomaly == true)
                Color_Blindness_comboBox.Text = "Achromatomaly";

            else if (mConfiguration.achromatopsia == true)
                Color_Blindness_comboBox.Text = "Achromatopsia";

            else if (mConfiguration.deuteranomaly == true)
                Color_Blindness_comboBox.Text = "Deuteranomaly";

            else if (mConfiguration.deuteranopia == true)
                Color_Blindness_comboBox.Text = "Deuteranopia";

            else if (mConfiguration.protanomaly == true)
                Color_Blindness_comboBox.Text = "Protanomaly";

            else if (mConfiguration.tritanomaly == true)
                Color_Blindness_comboBox.Text = "Tritanomaly";

            else if (mConfiguration.tritanopia == true)
                Color_Blindness_comboBox.Text = "Tritanopia";

            else if (mConfiguration.protanopia == true)
                Color_Blindness_comboBox.Text = "Protanopia";
        }

        private void tb_ZoomFactor_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.ZoomFactor = tb.Value;
            lbl_ZoomFactor.Text = tb_ZoomFactor.Value.ToString();
        }

        private void traceBaarZoomButton1_Click(object sender, EventArgs e)
        {
            if (mConfiguration.ZoomFactor != 10)
            {
                tb_ZoomFactor.Value += 1;
                mConfiguration.ZoomFactor = tb_ZoomFactor.Value;
                lbl_ZoomFactor.Text = tb_ZoomFactor.Value.ToString();
                mMagnifierMainForm.label_ZoomFaktor.Text = tb_ZoomFactor.Value.ToString();
            }
        }

        private void traceBaarZoomButton2_Click(object sender, EventArgs e)
        {
            if (mConfiguration.ZoomFactor != 1)
            {
                tb_ZoomFactor.Value -= 1;
                mConfiguration.ZoomFactor = tb_ZoomFactor.Value;
                lbl_ZoomFactor.Text = mConfiguration.ZoomFactor.ToString();
                mMagnifierMainForm.label_ZoomFaktor.Text = tb_ZoomFactor.Value.ToString();
            }
        }

        private void tb_SpeedFactor_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.SpeedFactor = tb.Value / 100.0f;
            lbl_SpeedFactor.Text = mConfiguration.SpeedFactor.ToString();
            // SaveConfiguration();
        }

        private void tb_Width_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.MagnifierWidth = (Screen.PrimaryScreen.Bounds.Width / 10) * tb.Value;
            lbl_Width.Text = mConfiguration.MagnifierWidth.ToString();

            if (cb_Symmetry.Checked)
            {
                tb_Height.Value = tb.Value;
                mConfiguration.MagnifierHeight = tb.Value;
                lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();
            }
            // SaveConfiguration();
        }

        private void tb_Height_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.MagnifierHeight = (Screen.PrimaryScreen.Bounds.Height / 10)* tb.Value; //90 %
            lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();
            // SaveConfiguration();
        }

        private void traceBaarHightButton_Click(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Height - mConfiguration.MagnifierHeight >= 10 && Screen.PrimaryScreen.Bounds.Width - mConfiguration.MagnifierWidth >= 10)
            {
                if(mConfiguration.MagnifierHeight != (Screen.PrimaryScreen.Bounds.Height/10)*9 && mConfiguration.MagnifierWidth != Screen.PrimaryScreen.Bounds.Width)
                {
                    tb_Height.Value += 1;
                    tb_Width.Value += 1;
                    mConfiguration.MagnifierHeight = (Screen.PrimaryScreen.Bounds.Height / 10) * tb_Height.Value;
                    mConfiguration.MagnifierWidth = (Screen.PrimaryScreen.Bounds.Width / 10) * tb_Width.Value;
                    lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();
                    lbl_Width.Text = mConfiguration.MagnifierWidth.ToString();
                }
               
            }
        }

        private void traceBaarWidthButton_Click(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.Bounds.Height / 10) * tb_Height.Value; 
            int y = (Screen.PrimaryScreen.Bounds.Width / 10) * tb_Width.Value;

            if (mConfiguration.MagnifierHeight > (Screen.PrimaryScreen.Bounds.Height / 10) && mConfiguration.MagnifierWidth > (Screen.PrimaryScreen.Bounds.Width / 10))
            {
                if (mConfiguration.MagnifierHeight != Screen.PrimaryScreen.Bounds.Height / 10 && mConfiguration.MagnifierWidth != Screen.PrimaryScreen.Bounds.Width/10)
                {
                    tb_Height.Value -= 1;
                    tb_Width.Value -= 1;
                    mConfiguration.MagnifierHeight = (Screen.PrimaryScreen.Bounds.Height / 10) * tb_Height.Value;
                    mConfiguration.MagnifierWidth = (Screen.PrimaryScreen.Bounds.Width / 10) * tb_Width.Value;
                    lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();
                    lbl_Width.Text = mConfiguration.MagnifierWidth.ToString();

                }
            }
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.senitivityOfHeadTracking = tb.Value ;
            Sensititivity_label.Text = mConfiguration.senitivityOfHeadTracking.ToString();
        }

        private void SensititivityUp_button_Click(object sender, EventArgs e)
        {
            if (mConfiguration.senitivityOfHeadTracking < 30)
            {
                Sensititvity_trackBar.Value += 1;
                mConfiguration.senitivityOfHeadTracking = Sensititvity_trackBar.Value;
                Sensititivity_label.Text = mConfiguration.senitivityOfHeadTracking.ToString();

            }
        }

        private void SensititivityDown_button_Click(object sender, EventArgs e)
        {
            if (mConfiguration.senitivityOfHeadTracking >2)
            {
                Sensititvity_trackBar.Value -= 1;
                mConfiguration.senitivityOfHeadTracking = Sensititvity_trackBar.Value;
                Sensititivity_label.Text = mConfiguration.senitivityOfHeadTracking.ToString();
                
            }

        }


        private void cb_HideMouseCursor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
                mConfiguration.HideMouseCursor = true;
            else
                mConfiguration.HideMouseCursor = false;
        }
       

        private void Color_Blindness_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            switch ((String)box.SelectedItem)
            {
                case "Normal":
                    mConfiguration.normal = true;
                    mConfiguration.currentColorBlindness = "normal";
                    mConfiguration.invertColors = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Invert all Colors":
                    mConfiguration.invertColors = true;
                    mConfiguration.currentColorBlindness = "invertColors";
                    mConfiguration.normal = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Achromatomaly-----Complete CB":
                    mConfiguration.achromatomaly = true;
                    mConfiguration.currentColorBlindness = "achromatomaly";
                    mConfiguration.invertColors = false;
                    mConfiguration.normal = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Achromatopsia------Complete CB":
                    mConfiguration.achromatopsia = true;
                    mConfiguration.currentColorBlindness = "achromatopsia";
                    mConfiguration.invertColors = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.normal = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Deuteranopia--------Red Green CB":
                    mConfiguration.deuteranopia = true;
                    mConfiguration.currentColorBlindness = "deuteranopia";
                    mConfiguration.invertColors = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.normal = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Deuteranomaly------Red Green CB":
                    mConfiguration.deuteranomaly = true;
                    mConfiguration.currentColorBlindness = "deuteranomaly";
                    mConfiguration.invertColors = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.normal = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Protanomaly---------Red Green CB":
                    mConfiguration.protanomaly = true;
                    mConfiguration.currentColorBlindness = "protanomaly";
                    mConfiguration.invertColors = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.normal = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Protanopia-----------Red Green CB":
                    mConfiguration.protanopia = true;
                    mConfiguration.currentColorBlindness = "protanopia";
                    mConfiguration.invertColors = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.normal = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Tritanomaly----------Blue Yellow  CB":
                    mConfiguration.tritanomaly = true;
                    mConfiguration.currentColorBlindness = "tritanomaly";
                    mConfiguration.invertColors = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.normal = false;
                    mConfiguration.tritanopia = false;
                    break;
                case "Tritanopia------------Blue Yellow CB":
                    mConfiguration.tritanopia = true;
                    mConfiguration.currentColorBlindness = "tritanopia";
                    mConfiguration.invertColors = false;
                    mConfiguration.tritanomaly = false;
                    mConfiguration.protanopia = false;
                    mConfiguration.protanomaly = false;
                    mConfiguration.deuteranomaly = false;
                    mConfiguration.deuteranopia = false;
                    mConfiguration.achromatopsia = false;
                    mConfiguration.achromatomaly = false;
                    mConfiguration.normal = false;
                    break;
                default:
                    break;
            }

            SetComboBoxColorBlindnessText();
        }



        private void Close_button_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            refreshTimer.Enabled = false;
            //zoom factor
            // magnifierMainForm.Invalidate();
            Close();
        }

        private void traceBaarSpeedbutton2_Click(object sender, EventArgs e)
        {
            if (mConfiguration.SpeedFactor != 1)
            {
                tb_SpeedFactor.Value++;
                mConfiguration.SpeedFactor = tb_SpeedFactor.Value / 100.0f;
                lbl_SpeedFactor.Text = mConfiguration.SpeedFactor.ToString();
            }

        }

        private void traceBaarSpeedbutton1_Click(object sender, EventArgs e)
        {
            if (mConfiguration.SpeedFactor != 0.05f)
            {
                tb_SpeedFactor.Value--;
                mConfiguration.SpeedFactor = tb_SpeedFactor.Value / 100.0f;
                lbl_SpeedFactor.Text = mConfiguration.SpeedFactor.ToString();
            }
        }


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SaveConfiguration();
                refreshTimer.Enabled = false;

                // magnifierMainForm.Refresh();
                Close();
            }
        }

        private void cb_Symmetry_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                // Symmetric: Don't enable
                tb_Height.Enabled = false;
                mConfiguration.keepSymmetric = true;
            }
            else
            {
                // Non-symmetric: Allow height to be controlled independently.
                tb_Height.Enabled = true;
                mConfiguration.keepSymmetric = false;
            }
        }

        // changing the color and the size of magnifier
        private void onPaint(object sender, PaintEventArgs e)
        {

            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                                                new float[] {1,  0,  0,  0, 0},  // red scaling factor of -1
                                                new float[] {0,  1,  0,  0, 0},  // green scaling factor of -1
                                                new float[] {0,  0,  1,  0, 0},  // blue scaling factor of -1
                                                new float[] {0,  0,  0,  1, 0},   // alpha scaling factor of 1
                                                new float[] {0,  0,  0,  0, 1}}); // three translations of 1;


            if (mConfiguration.invertColors)
            {

                colorMatrix = new ColorMatrix(new float[][]{
                                                 new float[] {-1, 0, 0, 0, 0},
                                                 new float[] {0, -1, 0, 0, 0},
                                                 new float[] {0, 0, -1, 0, 0},
                                                 new float[] {0, 0, 0, 1, 0},
                                                 new float[] {1, 1, 1, 0, 1}});
            }

            if (mConfiguration.normal)
            {
                //Normal
                colorMatrix = new ColorMatrix(new float[][] {
                                                new float[] {1,  0,  0,  0, 0},  // red scaling factor of -1
                                                new float[] {0,  1,  0,  0, 0},  // green scaling factor of -1
                                                new float[] {0,  0,  1,  0, 0},  // blue scaling factor of -1
                                                new float[] {0,  0,  0,  1, 0},   // alpha scaling factor of 1
                                                new float[] {0,  0,  0,  0, 1}}); // three translations of 1

            }
            if (mConfiguration.protanopia)
            {
                //Protanopia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.567f, 0.433f ,  0,  0, 0},
                                               new float[] {0.558f ,0.442f ,  0,  0, 0},
                                               new float[] {0, 0.242f, 0.758f,  0, 0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mConfiguration.protanomaly)
            {
                //Protanomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.817f,0.183f,0,0,0},
                                               new float[] {0.333f,0.667f,0,0,0},
                                               new float[] {0,0.125f,0.875f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mConfiguration.deuteranopia)
            {
                //Deuteranopia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.625f,0.375f,0,0,0},
                                               new float[] {0.7f,0.3f,0,0,0},
                                               new float[] { 0,0.3f,0.7f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mConfiguration.deuteranomaly)
            {
                //Deuteranomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.8f,0.2f,0,0,0},
                                               new float[] { 0.258f,0.742f,0,0,0},
                                               new float[] {0,0.142f,0.858f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mConfiguration.tritanopia)
            {
                //Tritanopia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.95f,0.05f,0,0,0},
                                               new float[] {0,0.433f,0.567f,0,0},
                                               new float[] {0,0.475f,0.525f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mConfiguration.tritanomaly)
            {
                //Tritanomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.967f,0.033f,0,0,0},
                                               new float[] {0,0.733f,0.267f,0,0},
                                               new float[] {0,0.183f,0.817f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mConfiguration.achromatopsia)
            {
                //Achromatopsia
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.299f,0.587f,0.114f,0,0},
                                               new float[] {0.299f,0.587f,0.114f,0,0},
                                               new float[] { 0.299f,0.587f,0.114f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }
            if (mConfiguration.achromatomaly)
            {
                //Achromatomaly
                colorMatrix = new ColorMatrix(new float[][] {
                                               new float[] {0.618f,0.320f,0.062f,0,0},
                                               new float[] {0.163f,0.775f,0.062f,0,0},
                                               new float[] { 0.163f,0.320f,0.516f,0,0},
                                               new float[] {0,  0,  0,  1, 0},
                                               new float[] {0,  0,  0,  0, 1}});
            }

            MainWindowsRectangle = new Rectangle(630, 90, 344, 200);

            //e.Graphics.FillRectangle(Brushes.Black, MainWindowsRectangle);

            magnifierWindowsRectangle = new Rectangle(630, 90, mConfiguration.MagnifierWidth / 6 + 10, mConfiguration.MagnifierHeight / 6 +30);
            // magnifierWindowsRectangle = new Rectangle(630, 45, mConfiguration.MagnifierWidth / 5, mConfiguration.MagnifierHeight / 5);
            //e.Graphics.FillRectangle(Brushes.Gray, magnifierWindowsRectangle);

            Graphics g = e.Graphics;
           // Graphics g1 = e.Graphics;
            Image mScreenImage = Properties.Resources.colors;


            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);

           // int x = Left - mConfiguration.MagnifierWidth / 5 + mConfiguration.MagnifierWidth / 5;
            //int y = Top - mConfiguration.MagnifierWidth / 5 + mConfiguration.MagnifierWidth / 5;

            // if not symetry
            if (tb_Height.Enabled && tb_Width.Enabled)
            {
                if (mConfiguration.normal)
                {
                    g.DrawImage(
                    mScreenImage,
                    MainWindowsRectangle,
                    0, 0,
                    200, 200,
                    GraphicsUnit.Pixel);

                    Pen pen = new Pen(Color.White, 4);
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawRectangle(pen, magnifierWindowsRectangle);
                    //e.Graphics.FillRectangle(Brushes.White, magnifierWindowsRectangle);
                }

                else
                {
                    //Background
                    g.DrawImage(
                    mScreenImage,
                    MainWindowsRectangle,
                    0, 0,
                    200, 200,
                    GraphicsUnit.Pixel);

                    //rectangle
                    g.DrawImage(
                    mScreenImage,
                    magnifierWindowsRectangle,
                    0, 0,
                    180,180,
                    GraphicsUnit.Pixel,
                    imageAttributes);

                    Pen pen = new Pen(Color.White, 4);
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawRectangle(pen, magnifierWindowsRectangle);

                }
            }

        
        }


        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox selectEyeComboBox = (ComboBox)sender;

            switch ((String)selectEyeComboBox.SelectedItem)
            {

                case "Both eyes + head":
                    // controller.setMode(MouseController.Mode.BOTH_EYE_AND_HEAD);
                    mConfiguration.bothEye = false;
                    mConfiguration.bothEyeAndHead = true;
                    mConfiguration.leftEye = false;
                    mConfiguration.leftEyeAndHead = false;
                    mConfiguration.rightEye = false;
                    mConfiguration.rightEyeAndHead = false;
                    mConfiguration.justHead = false;
                    break;

                case "Both eyes":
                    // controller.setMode(MouseController.Mode.LEFT_EYE_AND_HEAD);
                    mConfiguration.bothEye = true;
                    mConfiguration.bothEyeAndHead = false;
                    mConfiguration.leftEye = false;
                    mConfiguration.leftEyeAndHead = false;
                    mConfiguration.rightEye = false;
                    mConfiguration.rightEyeAndHead = false;
                    mConfiguration.justHead = false;
                    break;

                case "Left eye + head":
                    // controller.setMode(MouseController.Mode.LEFT_EYE_AND_HEAD);
                    mConfiguration.bothEye = false;
                    mConfiguration.bothEyeAndHead = false;
                    mConfiguration.leftEye = false;
                    mConfiguration.leftEyeAndHead = true;
                    mConfiguration.rightEye = false;
                    mConfiguration.rightEyeAndHead = false;
                    mConfiguration.justHead = false;
                    break;

                case "Left eye":
                    // controller.setMode(MouseController.Mode.LEFT_EYE_AND_HEAD);
                    mConfiguration.bothEye = false;
                    mConfiguration.bothEyeAndHead = false;
                    mConfiguration.leftEye = true;
                    mConfiguration.leftEyeAndHead = false;
                    mConfiguration.rightEye = false;
                    mConfiguration.rightEyeAndHead = false;
                    mConfiguration.justHead = false;
                    break;

                case "Right eye + head":
                    // controller.setMode(MouseController.Mode.RIGHT_EYE_AND_HEAD);
                    mConfiguration.bothEye = false;
                    mConfiguration.bothEyeAndHead = false;
                    mConfiguration.leftEye = false;
                    mConfiguration.leftEyeAndHead = false;
                    mConfiguration.rightEye = false;
                    mConfiguration.rightEyeAndHead = true;
                    mConfiguration.justHead = false;
                    break;

                case "Right eye":
                    // controller.setMode(MouseController.Mode.RIGHT_EYE_AND_HEAD);
                    mConfiguration.bothEye = false;
                    mConfiguration.bothEyeAndHead = false;
                    mConfiguration.leftEye = false;
                    mConfiguration.leftEyeAndHead = false;
                    mConfiguration.rightEye = true;
                    mConfiguration.rightEyeAndHead = false;
                    mConfiguration.justHead = false;
                    break;

                case "Just head":
                    // controller.setMode(MouseController.Mode.JUST_HEAD);
                    mConfiguration.bothEye = false;
                    mConfiguration.bothEyeAndHead = false;
                    mConfiguration.leftEye = false;
                    mConfiguration.leftEyeAndHead = false;
                    mConfiguration.rightEye = false;
                    mConfiguration.rightEyeAndHead = false;
                    mConfiguration.justHead = true;
                    break;
                default:
                    break;
            }

            SetComboBox1Text();
           
        }

        System.Windows.Forms.ToolTip toolTip_ZoomOutButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_ZoomInButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_SpeedDownButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_SpeedUpButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_tb_Width = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_tb_Height = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_traceBaarWidthButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_traceBaarHightButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_CloseButton = new System.Windows.Forms.ToolTip();
        System.Windows.Forms.ToolTip toolTip_MouseHidden = new System.Windows.Forms.ToolTip();



        private void ZoomOutButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_ZoomOutButton.SetToolTip(this.traceBaarZoomButton2, "Zoom Out Magnifier Window");
            traceBaarZoomButton2.FlatAppearance.BorderColor = Color.DeepSkyBlue;

        }

        private void ZoomOutButton_MouseLeave(object sender, EventArgs e)
        {
            traceBaarZoomButton2.FlatAppearance.BorderColor = Color.White;

        }

        private void ZoomInButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_ZoomInButton.SetToolTip(this.traceBaarZoomButton1, "Zoom In Magnifier Window");
            traceBaarZoomButton1.FlatAppearance.BorderColor = Color.DeepSkyBlue;

        }
        private void ZoomInButton_MouseLeav(object sender, EventArgs e)
        {
            traceBaarZoomButton1.FlatAppearance.BorderColor = Color.White;

        }

        private void SpeedDownButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_SpeedDownButton.SetToolTip(this.traceBaarSpeedDownbutton, "Speed Down Magnifier Window move");
        }

        private void SpeedUpButton_MouseHover(object sender, EventArgs e)
        {
            toolTip_SpeedUpButton.SetToolTip(this.traceBaarSpeedUpbutton, "Speed Up Magnifier Window move");
        }

        private void tb_Width_MouseHover(object sender, EventArgs e)
        {
            toolTip_ZoomOutButton.SetToolTip(this.tb_Width, "Magnifier Window Width");
        }

        private void tb_Height_MouseHover(object sender, EventArgs e)
        {
            toolTip_tb_Height.SetToolTip(this.tb_Height, "Magnifier Window Height");
        }

        private void traceBaarWidthButton_MouseOver(object sender, EventArgs e)
        {
            toolTip_traceBaarWidthButton.SetToolTip(this.traceBaarWidthButton, "Make Magnifier Window Smaller");
            traceBaarWidthButton.FlatAppearance.BorderColor = Color.DeepSkyBlue;

        }

        private void traceBaarWidthButton_MouseLeave(object sender, EventArgs e)
        {
            traceBaarWidthButton.FlatAppearance.BorderColor = Color.White;

        }

        private void traceBaarHightButton_mouseHover(object sender, EventArgs e)
        {
            toolTip_traceBaarHightButton.SetToolTip(this.traceBaarHightButton, "Make Magnifier Window Bigger");
            traceBaarHightButton.FlatAppearance.BorderColor = Color.DeepSkyBlue;

        }
        private void traceBaarHightButton_mouseLeave(object sender, EventArgs e)
        {
            traceBaarHightButton.FlatAppearance.BorderColor = Color.White;

        }

   
        private void MiniMize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button_SaveConfiguration_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            refreshTimer.Enabled = false;
            Close();
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
           if (comboBox1.DroppedDown)
               comboBox1.DroppedDown = false;
           else
               comboBox1.DroppedDown = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Color_Blindness_comboBox.DroppedDown = (!Color_Blindness_comboBox.DroppedDown);
        }

        //changing the color
        private void ChangeWIMcolor_button_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                mConfiguration.whereIAmColor = MyDialog.Color.Name;
                ChangeWIMcolor_button.BackColor = Color.FromName(mConfiguration.whereIAmColor);
            }

            


        }

        private void DefaultSetting_button_Click(object sender, EventArgs e)
        {
            mConfiguration.ZoomFactor = 2;
            tb_ZoomFactor.Value = (int)mConfiguration.ZoomFactor;
            lbl_ZoomFactor.Text = mConfiguration.ZoomFactor.ToString();

            mConfiguration.SpeedFactor = 1;
            tb_SpeedFactor.Value = (int)(100 * mConfiguration.SpeedFactor);
            lbl_SpeedFactor.Text = mConfiguration.SpeedFactor.ToString();

            mConfiguration.senitivityOfHeadTracking = 9;
            Sensititvity_trackBar.Value = mConfiguration.senitivityOfHeadTracking;
            Sensititivity_label.Text = mConfiguration.senitivityOfHeadTracking.ToString();


            mConfiguration.bothEye = true;
            mConfiguration.bothEyeAndHead = false;
            mConfiguration.leftEye = false;
            mConfiguration.leftEyeAndHead = false;
            mConfiguration.rightEye = false;
            mConfiguration.rightEyeAndHead = false;
            mConfiguration.justHead = false;

           

            mConfiguration.normal = true;
            mConfiguration.invertColors = false;
            mConfiguration.achromatomaly = false;
            mConfiguration.achromatopsia = false;
            mConfiguration.deuteranopia = false;
            mConfiguration.deuteranomaly = false;
            mConfiguration.protanomaly = false;
            mConfiguration.protanopia = false;
            mConfiguration.tritanomaly = false;
            mConfiguration.tritanopia = false;

            mConfiguration.MagnifierHeight = 750;
            tb_Height.Value = mConfiguration.MagnifierHeight / (Screen.PrimaryScreen.Bounds.Height / 10);
            lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();

            mConfiguration.MagnifierWidth = 1152;
            tb_Width.Value = mConfiguration.MagnifierWidth / (Screen.PrimaryScreen.Bounds.Width / 10);
            lbl_Width.Text = mConfiguration.MagnifierWidth.ToString();

            mMagnifierMainForm.label_ZoomFaktor.Text = tb_ZoomFactor.Value.ToString();

            SetComboBox1Text();
            SetComboBoxColorBlindnessText();

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

        private void Setting_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

  
    }
}
