using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Examples.IPCleaner
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
            if (File.Exists("IPCleanerSettings.dat"))
            {
                //TODO:  insert saved settings from file
            }
            else
            {
                //set defaults
                ManagementServer = Environment.MachineName;
                ManagementServerUsername = @"scx-rh\administrator";
                ManagementServerPassword = "P@ssw0rd1";
                SqlServer = Environment.MachineName;
                DBName = "Orchestrator";
                DBUserName = @"sa";
                DBPassword = "P@ssw0rd1";
            }
        }

        public ConnectionOptions WmiConnectionOptions
        {
            get 
            {
                ConnectionOptions conn = new ConnectionOptions();
                conn.Authentication = (AuthenticationLevel)Enum.Parse(typeof(AuthenticationLevel), cboAuthenticationLevel.SelectedItem.ToString());
                conn.EnablePrivileges = checkBoxEnablePrivileges.Checked;
                conn.Impersonation = (ImpersonationLevel)Enum.Parse(typeof(ImpersonationLevel), cboImpersonationLevel.SelectedItem.ToString());
                conn.Timeout = TimeSpan.Parse(numericTimeout.Value.ToString());
                
                if (!string.IsNullOrEmpty(textBoxWMIUserName.Text))
                {
                    conn.Username = textBoxWMIUserName.Text;
                }
                else if (!string.IsNullOrEmpty(textBoxMSUserName.Text))
                {
                    conn.Username = textBoxMSUserName.Text;
                }

                if (!string.IsNullOrEmpty(textBoxWMIPassword.Text))
                {
                    conn.Password = textBoxWMIPassword.Text;
                }
                else if (!string.IsNullOrEmpty(textBoxMSPassword.Text))
                {
                    conn.Password = textBoxMSPassword.Text;
                }

                return conn;
            }
        }


        public string SqlServer
        {
            get { return textBoxSQLServer.Text; }
            set { textBoxSQLServer.Text = value; }
        }

        public string DBName
        {
            get { return textBoxDatabase.Text; }
            set { textBoxDatabase.Text = value; }
        }

        public string DBUserName
        {
            get 
            {
                if (!string.IsNullOrEmpty(textBoxDBUserName.Text))
                {
                    return textBoxDBUserName.Text;
                }
                else if (!string.IsNullOrEmpty(textBoxMSUserName.Text))
                {
                    return textBoxMSUserName.Text;
                }
                else
                {
                    return string.Empty;
                }
            }
            set { textBoxDBUserName.Text = value; }
        }

        public string DBPassword
        {
            get 
            {
                if (!string.IsNullOrEmpty(textBoxDBPassword.Text))
                {
                    return textBoxDBPassword.Text;
                }
                else if (!string.IsNullOrEmpty(textBoxMSPassword.Text))
                {
                    return textBoxMSPassword.Text;
                }
                else
                {
                    return string.Empty;
                }
            }
            set { textBoxDBPassword.Text = value; }
        }

        public string ManagementServer
        {
            get { return textBoxManagementServer.Text; }
            set { textBoxManagementServer.Text = value; }
        }

        public string ManagementServerUsername
        {
            get { return textBoxMSUserName.Text; }
            set { textBoxMSUserName.Text = value; }
        }

        public string ManagementServerPassword
        {
            get { return textBoxMSPassword.Text; }
            set { textBoxMSPassword.Text = value; }
        }



        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkBoxSaveSettings.Checked)
            {
                //TODO: save settings to file

                if (File.Exists("IPCleanerSettings.dat"))
                {
                    

                }

            }
        }

       
       
        

    }
}
