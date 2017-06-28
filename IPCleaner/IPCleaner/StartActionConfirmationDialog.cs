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
    public partial class StartActionConfirmationDialog : Form
    {
        public StartActionConfirmationDialog()
        {
            InitializeComponent();
        }

        private void textBoxConfirm_TextChanged(object sender, EventArgs e)
        {
            if (textBoxConfirm.Text.ToUpper() == "ERASE DATA")
            {
                buttonOK.Enabled = true;
            }
            else
            {
                buttonOK.Enabled = false;
            }

        }

        public string ActionsToPerformText
        {
            set { textBoxActionsToPerform.Text = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
