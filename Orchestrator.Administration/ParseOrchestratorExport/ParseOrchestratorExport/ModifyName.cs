using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ParseOrchestratorExport
{
    public partial class ModifyName : Form
    {
        public string initName = string.Empty;
        public string returnName = string.Empty;
        public bool cancelled = false;
        public ModifyName(string currentName)
        {
            InitializeComponent();
            initName = currentName;
            boxName.Text = currentName;
        }

        public void btnOk_Click(object sender, EventArgs e)
        {
            returnName = boxName.Text;
            cancelled = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            returnName = initName;
            cancelled = true;
            this.Close();
        }
    }
}
