﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using MagnifierSoftwareV_1.MouseMove.MouseMoveTest;
using System.Windows.Forms;

namespace MagnifierSoftwareV_1.EvaluationEye
{
    public partial class ChartsForm : Form
    {

        private Image mScreenImage = null;
        private string mAddressToSave;
        private Configuration mConfiguration;
      
        public ChartsForm(int bothEyesTruePoints, int bothEyesFalsePoints, int rightEyesTruePoints, int rightEyesFalsePoints, int leftEyesTruePoints, int leftEyesFalsePoints, String addressToSave, Configuration configuration)
        {
            InitializeComponent();
            this.Cursor = NativeMethods.LoadCustomCursor();
            mScreenImage = new Bitmap(this.Width, this.Height);
            FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            mAddressToSave = addressToSave;

            mConfiguration = configuration;

            this.chart1.Series["Total Gaze Points"].Points.AddXY("BothEyes", bothEyesTruePoints + bothEyesFalsePoints);
            this.chart1.Series["True Gaze Points"].Points.AddXY("BothEyes", bothEyesTruePoints);
            this.chart1.Series["False Gaze Points"].Points.AddXY("BothEyes", bothEyesFalsePoints);

            this.chart1.Series["Total Gaze Points"].Points.AddXY("RightEye", rightEyesTruePoints + rightEyesFalsePoints);
            this.chart1.Series["True Gaze Points"].Points.AddXY("RightEye", rightEyesTruePoints);
            this.chart1.Series["False Gaze Points"].Points.AddXY("RightEye", rightEyesFalsePoints);

            this.chart1.Series["Total Gaze Points"].Points.AddXY("LeftEye", leftEyesTruePoints + leftEyesFalsePoints);
            this.chart1.Series["True Gaze Points"].Points.AddXY("LeftEye", leftEyesTruePoints);
            this.chart1.Series["False Gaze Points"].Points.AddXY("LeftEye", leftEyesFalsePoints);


            float totalBothEyes = bothEyesTruePoints + bothEyesFalsePoints;
            float totalLeftEyes = leftEyesTruePoints + leftEyesFalsePoints;
            float totalRightEyes = rightEyesTruePoints + rightEyesFalsePoints;

            float both = (bothEyesTruePoints / totalBothEyes )*100;
            float right = (rightEyesTruePoints / totalRightEyes)*100;
            float left = (leftEyesTruePoints / totalLeftEyes) * 100;

            Console.WriteLine(both);
            Console.WriteLine(right);
            Console.WriteLine(left);



            mConfiguration.bothEye = false;
            mConfiguration.bothEyeAndHead = false;
            mConfiguration.leftEye = false;
            mConfiguration.leftEyeAndHead = false;
            mConfiguration.rightEye = false;
            mConfiguration.rightEyeAndHead = false;
            mConfiguration.justHead = false;
            mConfiguration.whereIamPointFollowsEyes = true;


            if (both >= 80 && left >= 80 && right >= 80)
            {
                label1.Text = " You are a user with no problems in your eyes and you can chose any eye tracking options!";
                mConfiguration.bothEye = true;
                mConfiguration.whereIamPointFollowsEyes = true;
            }

            else if (both >= 70 && left >= 70 && right >= 70)
            {
                label1.Text = " You are a user with no problems in your eyes and you can chose any eye tracking options!";
                mConfiguration.bothEye = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (both >= 70 && left >= 70 && right < 70)
            {
                label1.Text = " The best choice for you is both eyes option or left eye option!";
                mConfiguration.bothEye = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (both >= 70 && left < 70 && right >= 70)
            {
                label1.Text = " The best choice for you is both eyes option or right eye option!";
                mConfiguration.bothEye = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (both < 70 && left < 70 && right >= 70)
            {
                label1.Text = " The best choice for you is right eye option!";
                mConfiguration.rightEye = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (both < 70 && left >= 70 && right< 70)
            {
                label1.Text = " The best choice for you is left eye option!";
                mConfiguration.leftEye = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (both < 10 && left < 10 && right < 10)
            {
                label1.Text = "Probably you should better to chose the head tracking option";
                mConfiguration.justHead = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (Math.Max(Math.Max(both, right), left) == both)
            {
                label1.Text = " The best choice for you is the both eyes option";
                mConfiguration.bothEye = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (Math.Max(Math.Max(both, right), left) == right)
            {
                label1.Text = " The best choice for you is the right eye option";
                mConfiguration.rightEye = true;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

            else if (Math.Max(Math.Max(both, right), left) == left)
            {
                label1.Text = " The best choice for you is the leftt eye option";
                mConfiguration.leftEye = false;
                mConfiguration.whereIamPointFollowsEyes = false;
            }

        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(mScreenImage);
            graphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, new System.Drawing.Size(this.Width, this.Height), CopyPixelOperation.SourceCopy);

            mScreenImage.Save(mAddressToSave, ImageFormat.Png);
            this.Close();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /* public ChartsForm(int lineTruePoints , int lineFalsePoints , int rectangleTruePoints , int rectangleFalsePoints, int textTruePoints, int textFalsePoints, String addressToSave)
         {
             InitializeComponent();
             this.Cursor = NativeMethods.LoadCustomCursor();
             mScreenImage = new Bitmap(this.Width, this.Height);
             FormBorderStyle = FormBorderStyle.None;
             this.TopMost = true;
             mAddressToSave = addressToSave;

             this.chart1.Series["Total Gaze Points"].Points.AddXY("Line", lineFalsePoints + lineTruePoints);
             this.chart1.Series["True Gaze Points"].Points.AddXY("Line" , lineTruePoints);
             this.chart1.Series["False Gaze Points"].Points.AddXY("Line", lineFalsePoints);

             this.chart1.Series["Total Gaze Points"].Points.AddXY("Rectangle", rectangleTruePoints+ rectangleFalsePoints);
             this.chart1.Series["True Gaze Points"].Points.AddXY("Rectangle", rectangleTruePoints);
             this.chart1.Series["False Gaze Points"].Points.AddXY("Rectangle", rectangleFalsePoints);

             this.chart1.Series["Total Gaze Points"].Points.AddXY("Text", textTruePoints+ textFalsePoints);
             this.chart1.Series["True Gaze Points"].Points.AddXY("Text", textTruePoints);
             this.chart1.Series["False Gaze Points"].Points.AddXY("Text", textFalsePoints);
         }

         private void button_Save_Click(object sender, EventArgs e)
         {
             Graphics graphics = Graphics.FromImage(mScreenImage);
             graphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, new System.Drawing.Size(this.Width, this.Height), CopyPixelOperation.SourceCopy);

             mScreenImage.Save(mAddressToSave, ImageFormat.Png);
             this.Close();
         }

         private void button_Close_Click(object sender, EventArgs e)
         {
             this.Close();
         }*/
    }
}
