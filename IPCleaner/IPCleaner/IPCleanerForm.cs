using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;
using System.Xml;
using System.Management;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.IntegrationPack; 

namespace Microsoft.SystemCenter.Orchestrator.Integration.Examples.IPCleaner
{
    public partial class IPCleanerForm : Form
    {
        private SqlConnection _myConnection;

        private Dictionary<string, string> _ipList = new Dictionary<string,string>();
        private string _computerName;
        private IPUtilities _ipInfo;

        private string _dbName;
        private string _dbServer;
        private string _dbUserID;
        private string _dbPassword;

        private string _managementServerName;
        private string _managementServerUser;
        private string _managementServerPassword;
        private string SelectionWindowText
        {
            get {return textBoxSelectedIPInfo.Text;}
            set { textBoxSelectedIPInfo.Text =  value; }
        }

        private string LoggingWindowText
        {
            get { return textBoxLogging.Text; }
            set { textBoxLogging.Text = value; }
        }

        private ConnectionOptions _connectionOptions;


        

        public IPCleanerForm()
        {
            InitializeComponent();
        }

        private void ConnectToManagementServer()
        {
            if (!Utilities.IsLocalComputer(_managementServerName))
            {

            }
        }
        
        private void ConnectToDatabase()
        {
            SQLUtilities scoQuery = new SQLUtilities();
            //scoQuery.ConnectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3}; Trusted_Connection = False;", _dbServer, _dbName, _dbUserID, _dbPassword);
            scoQuery.ConnectionString = string.Format("Data Source = {0}; Initial Catalog = {1}; Integrated Security=SSPI", _dbServer, _dbName);
            _myConnection = scoQuery.OpenConnection();
            buttonGetDetails.Enabled = false;
            SelectionWindowText =  string.Empty;

            if (!String.IsNullOrEmpty(IPUtilities.ExtensionsPath))
            {
                dataGridView1.Rows.Clear();
                _ipList.Clear();

                textBoxConnectedTo.Text = string.Format("Connected to Management Server: [{0}]  /  SQL Server: [{1}]", _managementServerName, _dbServer);
                EnableDependentControls();
                LoadGridView();
            }
        }

        private void LoadGridView()
        {
            //format grid header
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Rows.Clear();

            try
            {
                _ipList = IPUtilities.GetAllIpInfo(_myConnection);

                foreach (string key in _ipList.Keys)
                {
                    //create new instances of the objects to create the row
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewTextBoxCell ipNameCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell ipTypeCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell activitiesCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell runbookInstanceCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell dataInstanceCell = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell isRegistered = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell isDeployed = new DataGridViewCheckBoxCell();

                    string value = string.Empty;
                    ipNameCell.Value = key;
                    _ipList.TryGetValue(key, out value);
                    ipNameCell.Tag = value;

                    //add formatting
                    ipTypeCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    activitiesCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    runbookInstanceCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataInstanceCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    //add the cells to the row
                    row.Cells.Add(ipNameCell);
                    row.Cells.Add(ipTypeCell);
                    row.Cells.Add(activitiesCell);
                    row.Cells.Add(runbookInstanceCell);
                    row.Cells.Add(dataInstanceCell);
                    row.Cells.Add(isRegistered);
                    row.Cells.Add(isDeployed);
                    
                    //add the row to the gridview
                    dataGridView1.Rows.Add(row);
                }


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        string packId = row.Cells[0].Tag.ToString();
                        IPType ipType = IPUtilities.GetIPType(packId, _myConnection);

                        row.Cells[1].Value = ipType.ToString();
                        row.Cells[2].Value = IPUtilities.GetNumberOfActivitiesInIP(packId, _myConnection, ipType).ToString();
                        row.Cells[3].Value = IPUtilities.GetActivityInstancesCountForIP(packId, _myConnection, ipType).ToString();
                        row.Cells[4].Value = IPUtilities.GetActivityInstanceDataCountForIP(packId, _myConnection, ipType).ToString();

                        if (ipType == IPType.Toolkit)
                        {

                        }
                        else if (ipType == IPType.Native)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                        else  //unknown type
                        {
                            row.DefaultCellStyle.BackColor = Color.Cornsilk;
                        }
                        string productCode = IPUtilities.GetProductCodeForIP(packId, _myConnection);
                        bool isDeployed = IPUtilities.IpIsDeployed(productCode, _computerName);
                        bool isRegistered = IPUtilities.IpIsRegistered(packId);

                        row.Cells[5].Value = isRegistered;
                        row.Cells[6].Value = isDeployed;

                        if ((isRegistered == false) && (isDeployed == false))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                    }
                    catch { }
                }
                //re-sort columns to prioritize non-deployed, non-registered IPs
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
                dataGridView1.Sort(dataGridView1.Columns[6], ListSortDirection.Ascending);
                dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
                
                //prevent the first row from being selected by default
                dataGridView1.SelectedRows[0].Selected = false;
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void DisplayIPDetails()
        {
            textBoxSelectedIPInfo.Enabled = true;
            SelectionWindowText =  "Loading IP Detail Information...";

            StringBuilder sb = new StringBuilder();
            string ipName = dataGridView1.SelectedCells[0].Value.ToString();
            string packID = GetSelectedPackID();
            string productCode = IPUtilities.GetProductCodeForIP(packID, _myConnection);
            IPType ipType = (IPType)Enum.Parse(typeof(IPType), dataGridView1.SelectedCells[1].Value.ToString());

            sb.AppendLine("IP Name            : " + ipName);
            sb.AppendLine("Registered Version : " + IPUtilities.GetRegisteredIPVersion(packID, _myConnection));
            sb.AppendLine("IP Type            : " + ipType.ToString());
            if (dataGridView1.SelectedCells[5].Value.ToString() == "True")
            {
                sb.AppendLine("IP Registered?     : Yes");
                checkBoxUnregisterIP.Enabled = true;
            }
            else
            {
                sb.AppendLine("IP Registered?     : No");
            }

            sb.AppendLine("Runbook Designers  :");
            List<string> designers = Infrastructure.GetRunbookDesigners(_myConnection);
            foreach (string designer in designers)
            {
                string ipVersion = IPUtilities.GetDeployedIpVersion(productCode, designer);
                if (!string.IsNullOrEmpty(ipVersion))
                {
                    sb.AppendLine("\t" + designer.PadRight(20) + "\tVersion " + ipVersion + " deployed");
                    checkBoxUndeployIP.Enabled = true;
                }
                else
                {
                    sb.AppendLine("\t" + designer.PadRight(20) + "\tIP not deployed");
                }
            }
            sb.AppendLine("Runbook Servers:");
            List<string> rbServers = Infrastructure.GetRunbookDesigners(_myConnection);
            foreach (string rbServer in rbServers)
            {
                string ipVersion = IPUtilities.GetDeployedIpVersion(productCode, rbServer);
                if (!string.IsNullOrEmpty(ipVersion))
                {
                    sb.AppendLine("\t" + rbServer.PadRight(20) + "\tVersion " + ipVersion + " deployed");
                    checkBoxUndeployIP.Enabled = true;
                }
                else
                {
                    sb.AppendLine("\t" + rbServer.PadRight(20) + "\tIP not deployed");
                }
            }
            
            sb.AppendLine("");
            int numActivities = IPUtilities.GetNumberOfActivitiesInIP(packID, _myConnection, ipType);
            sb.AppendLine("Number of Activities in the IP                   : " + numActivities);
                
                
            int numActivitiesInRunbooks = IPUtilities.GetActivityInstancesCountForIP(packID, _myConnection, ipType);
            sb.AppendLine("# of Times the Activities are Used in Runbooks   : " + numActivitiesInRunbooks);
            radioRemoveAllActivities.Enabled = (numActivitiesInRunbooks > 0);

            int numActivitiesInJobs = IPUtilities.GetActivityInstanceDataCountForIP(packID, _myConnection, ipType);
            sb.AppendLine("# of Times the Activities were Run in Jobs       : " + numActivitiesInJobs);
            radioRemoveAllJobHistory.Enabled = (numActivitiesInJobs > 0);
            sb.AppendLine("");

            int numDeletedActivitiesInRunbooks = IPUtilities.GetActivityInstancesCountForIP(packID, _myConnection, ipType, true);
            sb.AppendLine("# of 'Deleted' Activities in Runbooks            : " + numDeletedActivitiesInRunbooks);
            radioRemoveDeletedActivities.Enabled = (numDeletedActivitiesInRunbooks > 0);

            int numDeletedActivitiesInJobs = IPUtilities.GetActivityInstanceDataCountForIP(packID, _myConnection, ipType, true);
            sb.AppendLine("# of Job Data Items for 'Deleted' Activities     : " + numDeletedActivitiesInJobs);
            radioRemoveDeletedJobHistory.Enabled = (numDeletedActivitiesInJobs > 0);

            //only get list of activities for Toolkit-based IPs.
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("List of Activities in the IP and the runbooks where they are used:");
            sb.AppendLine("");
            Dictionary<string, string> activitiesList = IPUtilities.GetActivitesInIP(packID);
            sb.AppendLine("   Activity Name    /   Runbooks Where Used");
            sb.AppendLine("-------------------------------------------------------------------------");
            if (null != activitiesList)
            {
                foreach (string activity in activitiesList.Keys)
                {
                    sb.AppendLine("  " + activity);
                    string activityType = string.Empty;
                    if (activitiesList.TryGetValue(activity, out activityType))
                    {
                        List<string> runbookNames = IPUtilities.GetRunbookNamesWhereActivityTypeIsUsed(activityType, _myConnection);
                        if (runbookNames.Count > 0)
                        {
                            foreach (string name in runbookNames)
                            {
                                sb.AppendLine("                 " + name);
                            }
                            sb.AppendLine("");
                        }
                    }
                }
            }

            sb.AppendLine("");
            SelectionWindowText =  sb.ToString();


        }

        private void LoadConfigurationSettings()
        {
            ConfigurationForm cfgForm = new ConfigurationForm();
            DialogResult res = cfgForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                _dbServer = cfgForm.SqlServer;
                _dbName = cfgForm.DBName;
                _dbUserID = cfgForm.DBUserName;
                _dbPassword = cfgForm.DBPassword;
                _managementServerName = cfgForm.ManagementServer;
                _managementServerUser = cfgForm.ManagementServerUsername;
                _managementServerPassword = cfgForm.ManagementServerPassword;
                _connectionOptions = cfgForm.WmiConnectionOptions;
               
                ConnectToDatabase();
            }
        }

        private void EnableDependentControls()
        {
            dataGridView1.Enabled = true;
            textBoxSelectedIPInfo.Enabled = true;
            LoadToolTips();
        }

        private void LoadToolTips()
        {
            ToolTip toolTipCheckBoxCleanAll = new System.Windows.Forms.ToolTip();
            toolTipCheckBoxCleanAll.SetToolTip(this.checkBoxCleanAll, Properties.Resources.CleanAllToolTip);

            ToolTip toolTipCheckBoxUnregisterIP = new System.Windows.Forms.ToolTip();
            toolTipCheckBoxCleanAll.SetToolTip(this.checkBoxUnregisterIP, Properties.Resources.UnregisterTooltip);

        }

        private string GetSelectedPackID()
        {
            return dataGridView1.SelectedCells[0].Tag.ToString();
        }

        private void DisableActionsControls()
        {
            checkBoxUndeployIP.Enabled = false;
            checkBoxUnregisterIP.Enabled = false;
            radioRemoveAllActivities.Enabled = false;
            radioRemoveAllJobHistory.Enabled = false;
            radioRemoveDeletedActivities.Enabled = false;
            radioRemoveDeletedJobHistory.Enabled = false;
        }

        private DialogResult DisplayConfirmationDialog()
        {
            StringBuilder sb = new StringBuilder();
            if (checkBoxCleanAll.Checked)
            {
                sb.AppendLine("Remove all data related to the IP.");
                sb.AppendLine("");
            }
            if (checkBoxUnregisterIP.Checked)
            {
                sb.AppendLine("Unregister the IP");
                sb.AppendLine("");
            }
            if (checkBoxUndeployIP.Checked)
            {
                sb.AppendLine("Undeploy the IP");
                sb.AppendLine("");
            }
            if (radioRemoveAllActivities.Checked)
            {
                sb.AppendLine("Remove all activities and their configurations");
                if (comboBoxRemoveAllOptions.SelectedIndex == 0)
                {
                    sb.AppendLine(" - Activities will be replaced in runbooks with disabled 'unknown' activities");
                }
                else
                {
                    sb.AppendLine(" - Activities will be removed from runbooks and runbooks will be broken.");
                }
                sb.AppendLine("");
            }
            else if (radioRemoveDeletedActivities.Checked)
            {
                sb.AppendLine("Only data from activities that were deleted from runbooks will be deleted.");
                sb.AppendLine("");
            }
            else
            {
                sb.AppendLine("No activities in runbooks will be deleted.");
                sb.AppendLine("");
            }
            if (radioRemoveAllJobHistory.Checked)
            {
                sb.AppendLine("Job history for all activities in the IP will be deleted.");
                sb.AppendLine("");
            }
            else if (radioRemoveDeletedJobHistory.Checked)
            {
                sb.AppendLine("Job history will be removed for activities that were deleted from runbooks.");
                sb.AppendLine("");
            }
            else
            {
                sb.AppendLine("No job history will be removed.");
                sb.AppendLine("");
            }


            StartActionConfirmationDialog dlg = new StartActionConfirmationDialog();
            dlg.ActionsToPerformText = sb.ToString();
            DialogResult res = dlg.ShowDialog();
            return res;
           
        }

        public void PerformRemovalActions(string packID)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            StringBuilder sb = new StringBuilder();
            LoggingWindowText = String.Empty;
            sb.AppendLine("");
            sb.AppendLine("Starting removal of selected items...");
            sb.AppendLine("");
            Dictionary<string, string> activitiesList = IPUtilities.GetActivitesInIP(packID);
            LoggingWindowText = sb.ToString();

            bool removeOnlyDeletedJobHistory = (radioRemoveDeletedJobHistory.Checked);
            if (!radioRemoveNoJobHistory.Checked)
            {
                // actions start at the lowest level first to avoid any dependency / foreign key errors
                if (radioRemoveAllJobHistory.Checked)
                {
                    sb.AppendLine("Deleting job history for all activities in the IP...");
                }
                else if (radioRemoveDeletedJobHistory.Checked)
                {
                    sb.AppendLine("Deleting job history for all deleted activities in the IP...");
                }
                sb.AppendLine("");
                LoggingWindowText = sb.ToString();
                foreach (string activityName in activitiesList.Keys)
                {
                    sb.AppendLine(string.Format("   Activity: {0}", activityName));
                    string activityID = string.Empty;
                    if (activitiesList.TryGetValue(activityName, out activityID))
                    {
                        sb.AppendLine(string.Format("    Table: [OBJECTINSTANCEDATA]   Records deleted: {0}", IPUtilities.RemoveJobDataForActivityType(activityID, _myConnection, removeOnlyDeletedJobHistory).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine(string.Format("    Table: [OBJECTINSTANCES]      Records deleted: {0}", IPUtilities.RemoveInstancesOfActivityType(activityID, _myConnection, removeOnlyDeletedJobHistory).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine("");
                    }
                }
            }
            else
            {
                sb.AppendLine("Skipping job history deletion...");
                sb.AppendLine("");
            }


            bool removeOnlyDeletedActivities = (radioRemoveDeletedActivities.Checked);
            if (!radioRemoveNoActivities.Checked)
            {
                //TODO:  Add code to validate that any dependent table data has already been removed before doing this.

                if (radioRemoveAllActivities.Checked)
                {
                    sb.AppendLine("Deleting all activities in the IP from all runbooks...");
                }
                else if (radioRemoveDeletedActivities.Checked)
                {
                    sb.AppendLine("Deleting all activities in the IP that are marked 'Deleted'...");
                }
                sb.AppendLine("");
                LoggingWindowText = sb.ToString();
                foreach (string activityName in activitiesList.Keys)
                {
                    sb.AppendLine(string.Format("   Activity: {0}", activityName));
                    string activityID = string.Empty;
                    if (activitiesList.TryGetValue(activityName, out activityID))
                    {
                        sb.AppendLine(string.Format("    Table: [LINKTRIGGERS]   Records deleted: {0}", IPUtilities.RemoveJobDataForActivityType(activityID, _myConnection, removeOnlyDeletedActivities).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine(string.Format("    Table: [LINKS]          Records deleted: {0}", IPUtilities.RemoveInstancesOfActivityType(activityID, _myConnection, removeOnlyDeletedActivities).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine(string.Format("    Table: [OBJECT_AUDIT]   Records deleted: {0}", IPUtilities.RemoveInstancesOfActivityType(activityID, _myConnection, removeOnlyDeletedActivities).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine(string.Format("    Table: [OBJECTLOOPING]  Records deleted: {0}", IPUtilities.RemoveInstancesOfActivityType(activityID, _myConnection, removeOnlyDeletedActivities).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine(string.Format("    Table: [OBJECTS]        Records deleted: {0}", IPUtilities.RemoveInstancesOfActivityType(activityID, _myConnection, removeOnlyDeletedActivities).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine(string.Format("    Table: [CONFIGURATION]  Records deleted: {0}", IPUtilities.RemoveInstancesOfActivityType(activityID, _myConnection, removeOnlyDeletedActivities).ToString()));
                        LoggingWindowText = sb.ToString();
                        sb.AppendLine("");
                    }
                }
            }
            else
            {
                sb.AppendLine("Skipping activity deletion...");
                sb.AppendLine("");
            }
            LoggingWindowText = sb.ToString();





            //if (checkBoxCleanAll.Checked)
            //{
            //    sb.AppendLine("Remove all data related to the IP.");
            //    sb.AppendLine("");
            //}
            //if (checkBoxUnregisterIP.Checked)
            //{
            //    sb.AppendLine("Unregister the IP");
            //    sb.AppendLine("");
            //}
            //if (checkBoxUndeployIP.Checked)
            //{
            //    sb.AppendLine("Undeploy the IP");
            //    sb.AppendLine("");
            //}
          
            


        }

    #region Event Handlers

        private void IPCleanerForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_dbName))
            {
                LoadConfigurationSettings();
                if (string.IsNullOrEmpty(_dbName))
                {
                    DialogResult res = MessageBox.Show("Database information not set. Cannot continue.", "Cannot Connect", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                    if (res == DialogResult.Retry)
                    {
                        LoadConfigurationSettings();
                    }
                }
                if (!string.IsNullOrEmpty(_dbName))
                {
                    ConnectToDatabase();
                }
               
            }
        }

        private void configureDatabaseConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadConfigurationSettings();
            ConnectToDatabase();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpAbout aboutDlg = new HelpAbout();
            aboutDlg.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToDatabase();
        }

        
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ConnectToDatabase();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisableActionsControls();
            buttonGetDetails.Enabled = true;
            SelectionWindowText =  string.Empty;
        }


        private void buttonGetDetails_Click(object sender, EventArgs e)
        {
            DisableActionsControls();
            DisplayIPDetails();
            groupBoxDeleteActions.Enabled = true;
            buttonPerformActions.Enabled = true;
            
        }


        private void checkBoxCleanAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxUnregisterIP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxUndeployIP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioRemoveAllActivities_CheckedChanged(object sender, EventArgs e)
        {
            if (radioRemoveAllActivities.Checked)
            {
                comboBoxRemoveAllOptions.Enabled = true;
                comboBoxRemoveAllOptions.SelectedIndex = 0;
            }
            else
            {
                comboBoxRemoveAllOptions.Enabled = false;
            }
        }

        private void buttonPerformActions_Click(object sender, EventArgs e)
        {
            DialogResult res = DisplayConfirmationDialog();
            if (res != DialogResult.OK)
            {
                //cancelled
                return;
            }

            PerformRemovalActions(GetSelectedPackID());
        }

        


    #endregion






    }
}
