using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Examples.IPCleaner
{
    public partial class HelpAbout : Form
    {
        public HelpAbout()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HelpAbout_Load(object sender, EventArgs e)
        {
            textBoxBody.Text = "Version: ";
        }
    }
}
