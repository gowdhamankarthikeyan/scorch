using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCORCHDev.PowerShell
{
    public partial class NotificationPopup : Form
    {
        public string PopupText
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public int ProgressValue
        {
            get { return progressBar1.Value; }
            set { progressBar1.Value = value; }
        }

        public NotificationPopup()
        {
            InitializeComponent();
        }
    }
}
