using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagnifierSoftwareV_1
{
    public partial class Tutorial : Form
    {
        public Tutorial()
        {
            InitializeComponent();


            axAcroPDF1.src = Application.StartupPath + "\\Presentation1.pdf";
        }
    }
}
