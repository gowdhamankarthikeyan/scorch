using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.DirectoryServices;
using System.Windows.Forms;
using System.Management;
using Microsoft.EnterpriseManagement;
using Microsoft.EnterpriseManagement.Administration;
using Microsoft.EnterpriseManagement.Common;
using Microsoft.EnterpriseManagement.Configuration;
using Microsoft.EnterpriseManagement.Monitoring;
using Microsoft.Win32;


namespace SCOM_Maintenance_Mode_2012
{
    public partial class MainForm : Form
    {
        private ManagementGroup mg;
        private int RESULTS_INDEX = 8;
        private int OLD_INDEX = 0;
        private ArrayList ManagementServers = new ArrayList();
        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey componentsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\v3.0");
                componentsKey.GetSubKeyNames();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The current .NET version is not supported.  Run from a machine with .NET 3.5", ex.Message.ToString());
                MainForm.ActiveForm.Close();
            }
            try
            {
                /*
                DirectorySearcher searcher = new DirectorySearcher();
                searcher.Filter = "CN=SDKServiceSCP";

                SearchResultCollection resultCollection = searcher.FindAll();
                String RMSName;
                foreach (SearchResult result in resultCollection)
                {
                    RMSName = result.Properties["servicednsname"][0].ToString();
                    boxManagementGroup.Items.Add(RMSName);
                    boxWindowsComputerSearchRMS.Items.Add(RMSName);
                    boxGenericRMS.Items.Add(RMSName);
                    boxWebAppRMS.Items.Add(RMSName);
                    boxNetworkDeviceRMS.Items.Add(RMSName);
                    boxUnixRMS.Items.Add(RMSName);
                    boxGroupRMS.Items.Add(RMSName);
                    boxScheduleRMS.Items.Add(RMSName);
                }*/
                String RMSName = "mgoscmmsp1.genmills.com";
                boxManagementGroup.Items.Add(RMSName);
                boxWindowsComputerSearchRMS.Items.Add(RMSName);
                boxGenericRMS.Items.Add(RMSName);
                boxWebAppRMS.Items.Add(RMSName);
                boxNetworkDeviceRMS.Items.Add(RMSName);
                boxUnixRMS.Items.Add(RMSName);
                boxGroupRMS.Items.Add(RMSName);
                boxScheduleRMS.Items.Add(RMSName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failure Looking up SCOM RMS in AD\n" + ex.Message);
            }

            if (boxManagementGroup.Items.Count < 1)
            {
                MessageBox.Show("Did not find any registered Management Groups in Active Directory");
            }
            else
            {
                boxManagementGroup.SelectedIndex = 0;
                boxWindowsComputerSearchRMS.SelectedIndex = 0;
                boxGenericRMS.SelectedIndex = 0;
                boxWebAppRMS.SelectedIndex = 0;
                boxNetworkDeviceRMS.SelectedIndex = 0;
                boxUnixRMS.SelectedIndex = 0;
                boxGroupRMS.SelectedIndex = 0;
                boxScheduleRMS.SelectedIndex = 0;
                boxScheduleRemoteComputer.Text = boxScheduleRMS.Text;
                boxScheduleStartTime.Value = DateTime.Now;
                radioScheduleOnce.Select();
            }


            try
            {
                statusBar.Minimum = 0;
                statusBar.Maximum = 100;
                statusBar.Step = 10;

                boxMaintenanceStartTime.Value = DateTime.Now;
                boxDateTime.Value = DateTime.Now.AddMinutes(60);

                boxWindowsComputerSearchStartTime.Value = DateTime.Now;
                boxWindowsComputerSearchEndTime.Value = DateTime.Now.AddMinutes(60);

                boxGenericStartTime.Value = DateTime.Now;
                boxGenericEndTime.Value = DateTime.Now.AddMinutes(60);

                boxWebAppStartTime.Value = DateTime.Now;
                boxWebAppEndTime.Value = DateTime.Now.AddMinutes(60);

                boxNetworkDeviceStartTime.Value = DateTime.Now;
                boxNetworkDeviceEndTime.Value = DateTime.Now.AddMinutes(60);

                boxUnixStartTime.Value = DateTime.Now;
                boxUnixEndTime.Value = DateTime.Now.AddMinutes(60);

                boxGroupStartTime.Value = DateTime.Now;
                boxGroupEndTime.Value = DateTime.Now.AddMinutes(60);

                boxMaintenanceType.Items.Add("ApplicationInstallation");
                boxMaintenanceType.Items.Add("ApplicationUnresponsive");
                boxMaintenanceType.Items.Add("ApplicationUnstable");
                boxMaintenanceType.Items.Add("LossOfNetworkConnectivity");
                boxMaintenanceType.Items.Add("PlannedApplicationMaintenance");
                boxMaintenanceType.Items.Add("PlannedHardwareInstallation");
                boxMaintenanceType.Items.Add("PlannedHardwareMaintenance");
                boxMaintenanceType.Items.Add("PlannedOperatingSystemReconfiguration");
                boxMaintenanceType.Items.Add("PlannedOther");
                boxMaintenanceType.Items.Add("SecurityIssue");
                boxMaintenanceType.Items.Add("UnplannedApplicationMaintenance");
                boxMaintenanceType.Items.Add("UnplannedHardwareInstallation");
                boxMaintenanceType.Items.Add("UnplannedHardwareMaintenance");
                boxMaintenanceType.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxMaintenanceType.Items.Add("UnplannedOther");
                boxMaintenanceType.SelectedIndex = 8;

                boxWindowsComputerSearchReasonCategory.Items.Add("ApplicationInstallation");
                boxWindowsComputerSearchReasonCategory.Items.Add("ApplicationUnresponsive");
                boxWindowsComputerSearchReasonCategory.Items.Add("ApplicationUnstable");
                boxWindowsComputerSearchReasonCategory.Items.Add("LossOfNetworkConnectivity");
                boxWindowsComputerSearchReasonCategory.Items.Add("PlannedApplicationMaintenance");
                boxWindowsComputerSearchReasonCategory.Items.Add("PlannedHardwareInstallation");
                boxWindowsComputerSearchReasonCategory.Items.Add("PlannedHardwareMaintenance");
                boxWindowsComputerSearchReasonCategory.Items.Add("PlannedOperatingSystemReconfiguration");
                boxWindowsComputerSearchReasonCategory.Items.Add("PlannedOther");
                boxWindowsComputerSearchReasonCategory.Items.Add("SecurityIssue");
                boxWindowsComputerSearchReasonCategory.Items.Add("UnplannedApplicationMaintenance");
                boxWindowsComputerSearchReasonCategory.Items.Add("UnplannedHardwareInstallation");
                boxWindowsComputerSearchReasonCategory.Items.Add("UnplannedHardwareMaintenance");
                boxWindowsComputerSearchReasonCategory.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxWindowsComputerSearchReasonCategory.Items.Add("UnplannedOther");
                boxWindowsComputerSearchReasonCategory.SelectedIndex = 8;

                boxGenericReasonCategory.Items.Add("ApplicationInstallation");
                boxGenericReasonCategory.Items.Add("ApplicationUnresponsive");
                boxGenericReasonCategory.Items.Add("ApplicationUnstable");
                boxGenericReasonCategory.Items.Add("LossOfNetworkConnectivity");
                boxGenericReasonCategory.Items.Add("PlannedApplicationMaintenance");
                boxGenericReasonCategory.Items.Add("PlannedHardwareInstallation");
                boxGenericReasonCategory.Items.Add("PlannedHardwareMaintenance");
                boxGenericReasonCategory.Items.Add("PlannedOperatingSystemReconfiguration");
                boxGenericReasonCategory.Items.Add("PlannedOther");
                boxGenericReasonCategory.Items.Add("SecurityIssue");
                boxGenericReasonCategory.Items.Add("UnplannedApplicationMaintenance");
                boxGenericReasonCategory.Items.Add("UnplannedHardwareInstallation");
                boxGenericReasonCategory.Items.Add("UnplannedHardwareMaintenance");
                boxGenericReasonCategory.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxGenericReasonCategory.Items.Add("UnplannedOther");
                boxGenericReasonCategory.SelectedIndex = 8;

                boxWebAppReasonCategory.Items.Add("ApplicationInstallation");
                boxWebAppReasonCategory.Items.Add("ApplicationUnresponsive");
                boxWebAppReasonCategory.Items.Add("ApplicationUnstable");
                boxWebAppReasonCategory.Items.Add("LossOfNetworkConnectivity");
                boxWebAppReasonCategory.Items.Add("PlannedApplicationMaintenance");
                boxWebAppReasonCategory.Items.Add("PlannedHardwareInstallation");
                boxWebAppReasonCategory.Items.Add("PlannedHardwareMaintenance");
                boxWebAppReasonCategory.Items.Add("PlannedOperatingSystemReconfiguration");
                boxWebAppReasonCategory.Items.Add("PlannedOther");
                boxWebAppReasonCategory.Items.Add("SecurityIssue");
                boxWebAppReasonCategory.Items.Add("UnplannedApplicationMaintenance");
                boxWebAppReasonCategory.Items.Add("UnplannedHardwareInstallation");
                boxWebAppReasonCategory.Items.Add("UnplannedHardwareMaintenance");
                boxWebAppReasonCategory.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxWebAppReasonCategory.Items.Add("UnplannedOther");
                boxWebAppReasonCategory.SelectedIndex = 8;

                boxNetworkDeviceReasonCategory.Items.Add("ApplicationInstallation");
                boxNetworkDeviceReasonCategory.Items.Add("ApplicationUnresponsive");
                boxNetworkDeviceReasonCategory.Items.Add("ApplicationUnstable");
                boxNetworkDeviceReasonCategory.Items.Add("LossOfNetworkConnectivity");
                boxNetworkDeviceReasonCategory.Items.Add("PlannedApplicationMaintenance");
                boxNetworkDeviceReasonCategory.Items.Add("PlannedHardwareInstallation");
                boxNetworkDeviceReasonCategory.Items.Add("PlannedHardwareMaintenance");
                boxNetworkDeviceReasonCategory.Items.Add("PlannedOperatingSystemReconfiguration");
                boxNetworkDeviceReasonCategory.Items.Add("PlannedOther");
                boxNetworkDeviceReasonCategory.Items.Add("SecurityIssue");
                boxNetworkDeviceReasonCategory.Items.Add("UnplannedApplicationMaintenance");
                boxNetworkDeviceReasonCategory.Items.Add("UnplannedHardwareInstallation");
                boxNetworkDeviceReasonCategory.Items.Add("UnplannedHardwareMaintenance");
                boxNetworkDeviceReasonCategory.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxNetworkDeviceReasonCategory.Items.Add("UnplannedOther");
                boxNetworkDeviceReasonCategory.SelectedIndex = 8;

                boxUnixReasonCategory.Items.Add("ApplicationInstallation");
                boxUnixReasonCategory.Items.Add("ApplicationUnresponsive");
                boxUnixReasonCategory.Items.Add("ApplicationUnstable");
                boxUnixReasonCategory.Items.Add("LossOfNetworkConnectivity");
                boxUnixReasonCategory.Items.Add("PlannedApplicationMaintenance");
                boxUnixReasonCategory.Items.Add("PlannedHardwareInstallation");
                boxUnixReasonCategory.Items.Add("PlannedHardwareMaintenance");
                boxUnixReasonCategory.Items.Add("PlannedOperatingSystemReconfiguration");
                boxUnixReasonCategory.Items.Add("PlannedOther");
                boxUnixReasonCategory.Items.Add("SecurityIssue");
                boxUnixReasonCategory.Items.Add("UnplannedApplicationMaintenance");
                boxUnixReasonCategory.Items.Add("UnplannedHardwareInstallation");
                boxUnixReasonCategory.Items.Add("UnplannedHardwareMaintenance");
                boxUnixReasonCategory.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxUnixReasonCategory.Items.Add("UnplannedOther");
                boxUnixReasonCategory.SelectedIndex = 8;

                boxGroupReasonCategory.Items.Add("ApplicationInstallation");
                boxGroupReasonCategory.Items.Add("ApplicationUnresponsive");
                boxGroupReasonCategory.Items.Add("ApplicationUnstable");
                boxGroupReasonCategory.Items.Add("LossOfNetworkConnectivity");
                boxGroupReasonCategory.Items.Add("PlannedApplicationMaintenance");
                boxGroupReasonCategory.Items.Add("PlannedHardwareInstallation");
                boxGroupReasonCategory.Items.Add("PlannedHardwareMaintenance");
                boxGroupReasonCategory.Items.Add("PlannedOperatingSystemReconfiguration");
                boxGroupReasonCategory.Items.Add("PlannedOther");
                boxGroupReasonCategory.Items.Add("SecurityIssue");
                boxGroupReasonCategory.Items.Add("UnplannedApplicationMaintenance");
                boxGroupReasonCategory.Items.Add("UnplannedHardwareInstallation");
                boxGroupReasonCategory.Items.Add("UnplannedHardwareMaintenance");
                boxGroupReasonCategory.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxGroupReasonCategory.Items.Add("UnplannedOther");
                boxGenericReasonCategory.SelectedIndex = 8;


                boxScheduleReasonCategory.Items.Add("ApplicationInstallation");
                boxScheduleReasonCategory.Items.Add("ApplicationUnresponsive");
                boxScheduleReasonCategory.Items.Add("ApplicationUnstable");
                boxScheduleReasonCategory.Items.Add("LossOfNetworkConnectivity");
                boxScheduleReasonCategory.Items.Add("PlannedApplicationMaintenance");
                boxScheduleReasonCategory.Items.Add("PlannedHardwareInstallation");
                boxScheduleReasonCategory.Items.Add("PlannedHardwareMaintenance");
                boxScheduleReasonCategory.Items.Add("PlannedOperatingSystemReconfiguration");
                boxScheduleReasonCategory.Items.Add("PlannedOther");
                boxScheduleReasonCategory.Items.Add("SecurityIssue");
                boxScheduleReasonCategory.Items.Add("UnplannedApplicationMaintenance");
                boxScheduleReasonCategory.Items.Add("UnplannedHardwareInstallation");
                boxScheduleReasonCategory.Items.Add("UnplannedHardwareMaintenance");
                boxScheduleReasonCategory.Items.Add("UnplannedOperatingSystemReconfiguration");
                boxScheduleReasonCategory.Items.Add("UnplannedOther");
                boxScheduleReasonCategory.SelectedIndex = 8;

                boxComputerName.Text = System.Net.Dns.GetHostEntry("LocalHost").HostName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initalization Failed\n" + ex.Message.ToString());
            }
        }

        private void boxManagementGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxManagementGroup.Text);
            mg.Reconnect();
        }
        private void btnMaintenanceMode_Click(object sender, EventArgs e)
        {
            statusBar.Value = 10;
            btnMaintenanceMode.Text = "Running...";
            btnMaintenanceMode.Enabled = false;

            statusBar.Value = 50;
            ArrayList results = putObjectInMM(boxComputerName.Text, "Microsoft.Windows.Computer", boxMaintenanceStartTime.Value, boxDateTime.Value, boxMaintenanceType.Text, boxComments.Text, true, true);


            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = results[0].ToString();

            statusBar.Value = 100;

            btnMaintenanceMode.Text = "Start Maintenance Mode";
            btnMaintenanceMode.Enabled = true;
            statusBar.Value = 0;

        }
        private void btnCheckMaintenance_Click(object sender, EventArgs e)
        {
            statusBar.Value = 10;
            btnCheckMaintenance.Text = "Running...";
            btnCheckMaintenance.Enabled = false;

            statusBar.Value = 50;
            ArrayList results = checkObjectMM(boxComputerName.Text, "Microsoft.Windows.Computer", true);
            statusBar.Value = 100;


            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;

            boxResults.Text = "";
            foreach (String result in results)
            {
                boxResults.Text += result + "\n";
            }

            btnCheckMaintenance.Text = "Check Maintenance Mode";
            btnCheckMaintenance.Enabled = true;
            statusBar.Value = 0;
        }
        private void btnStopMaintenance_Click(object sender, EventArgs e)
        {

            statusBar.Value = 10;
            btnStopMaintenance.Text = "Running...";
            btnStopMaintenance.Enabled = false;

            statusBar.Value = 50;
            ArrayList results = stopObjectMM(boxComputerName.Text, "Microsoft.Windows.Computer", true);
            statusBar.Value = 100;


            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;

            boxResults.Text = results[0].ToString();

            btnStopMaintenance.Text = "Stop Maintenance Mode";
            btnStopMaintenance.Enabled = true;
            statusBar.Value = 0;
        }

        private void boxWindowsComputerSearchRMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxWindowsComputerSearchRMS.Text);
            mg.Reconnect();
        }
        private void btnWindowsComputerSearchStartMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWindowsComputerSearchStartMM.Text = "Running...";
            btnWindowsComputerSearchStartMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxWindowsComputerSearchSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxWindowsComputerSearchSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedComputer in boxWindowsComputerSearchSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = putObjectInMM(selectedComputer, "Microsoft.Windows.Computer", boxWindowsComputerSearchStartTime.Value, boxWindowsComputerSearchEndTime.Value, boxWindowsComputerSearchReasonCategory.Text, boxWindowsComputerSearchReason.Text, true, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnWindowsComputerSearchStartMM.Text = "Start Maintenance Mode";
            btnWindowsComputerSearchStartMM.Enabled = true;
        }
        private void btnWindowsComputerSearchCheckMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWindowsComputerSearchCheckMM.Text = "Running...";
            btnWindowsComputerSearchCheckMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxWindowsComputerSearchSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxWindowsComputerSearchSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedComputer in boxWindowsComputerSearchSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = checkObjectMM(selectedComputer, "Microsoft.Windows.Computer", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnWindowsComputerSearchCheckMM.Text = "Check Maintenance Mode";
            btnWindowsComputerSearchCheckMM.Enabled = true;
        }
        private void btnWindowsComputerSearchStopMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWindowsComputerSearchStopMM.Text = "Running...";
            btnWindowsComputerSearchStopMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxWindowsComputerSearchSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxWindowsComputerSearchSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedComputer in boxWindowsComputerSearchSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = stopObjectMM(selectedComputer, "Microsoft.Windows.Computer", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnWindowsComputerSearchStopMM.Text = "Stop Maintenance Mode";
            btnWindowsComputerSearchStopMM.Enabled = true;
        }
        private void btnWindowsComputerSearchSearchResultsClear_Click(object sender, EventArgs e)
        {
            boxWindowsComputerSearchSearchResults.Items.Clear();
        }
        private void btnWindowsComputerSearchSearchResultsSearch_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWindowsComputerSearchSearchResultsSearch.Text = "Running...";
            btnWindowsComputerSearchSearchResultsSearch.Enabled = false;
            statusBar.Value = 50;

            ArrayList results = searchClass(boxWindowsComputerSearchComputerSearch.Text, "Microsoft.Windows.Computer", true);
            foreach (String result in results)
            {
                if (!boxWindowsComputerSearchSearchResults.Items.Contains(result))
                    boxWindowsComputerSearchSearchResults.Items.Add(result);
            }
            statusBar.Value = 100;
            btnWindowsComputerSearchSearchResultsSearch.Text = "Search";
            btnWindowsComputerSearchSearchResultsSearch.Enabled = true;
            statusBar.Value = 0;
        }
        private void btnWindowsComputerSearchSearchResultsSelectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxWindowsComputerSearchSearchResults.Items.Count; index++)
            {
                boxWindowsComputerSearchSearchResults.SetItemChecked(index, true);
            }
        }
        private void btnWindowsComputerSearchSearchResultsUnselectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxWindowsComputerSearchSearchResults.Items.Count; index++)
            {
                boxWindowsComputerSearchSearchResults.SetItemChecked(index, false);
            }
        }

        private void boxGenericRMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxGenericRMS.Text);
            mg.Reconnect();
        }
        private void btnGenericStartMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnGenericStartMM.Text = "Running...";
            btnGenericStartMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxGenericSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxGenericSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = DateTime.UtcNow.AddMinutes(60);

            foreach (String selectedClassObject in boxGenericSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = putObjectInMM(selectedClassObject, boxGenericClass.Text, startTime, endTime, boxGenericReasonCategory.Text, boxGenericReason.Text, true, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnGenericStartMM.Text = "Start Maintenance Mode";
            btnGenericStartMM.Enabled = true;
        }
        private void btnGenericCheckMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnGenericCheckMM.Text = "Running...";
            btnGenericCheckMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxGenericSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxGenericSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxGenericSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = checkObjectMM(selectedClassObject, boxGenericClass.Text, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnGenericCheckMM.Text = "Check Maintenance Mode";
            btnGenericCheckMM.Enabled = true;
        }
        private void btnGenericStopMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnGenericStopMM.Text = "Running...";
            btnGenericStopMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxGenericSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxGenericSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxGenericSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = stopObjectMM(selectedClassObject, boxGenericClass.Text, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnGenericStopMM.Text = "Stop Maintenance Mode";
            btnGenericStopMM.Enabled = true;
        }
        private void btnGenericClear_Click(object sender, EventArgs e)
        {
            boxGenericSearchResults.Items.Clear();
        }
        private void btnGenericSearch_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnGenericSearch.Text = "Running...";
            btnGenericSearch.Enabled = false;
            statusBar.Value = 50;

            ArrayList results = searchClass(boxGenericSearch.Text, boxGenericClass.Text, true);
            foreach (String result in results)
            {
                if (!boxGenericSearchResults.Items.Contains(result))
                    boxGenericSearchResults.Items.Add(result);
            }
            statusBar.Value = 100;
            btnGenericSearch.Text = "Search";
            btnGenericSearch.Enabled = true;
            statusBar.Value = 0;
        }
        private void btnGenericSelectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxGenericSearchResults.Items.Count; index++)
            {
                boxGenericSearchResults.SetItemChecked(index, true);
            }
        }
        private void btnGenericUnselectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxGenericSearchResults.Items.Count; index++)
            {
                boxGenericSearchResults.SetItemChecked(index, false);
            }
        }
        private void btnGenericLoadClasses_Click(object sender, EventArgs e)
        {
            btnGenericLoadClasses.Text = "Loading...";
            btnGenericLoadClasses.Enabled = false;
            System.Collections.ObjectModel.ReadOnlyCollection<MonitoringClass> mcc = mg.GetMonitoringClasses();
            ArrayList items = new ArrayList();
            foreach (MonitoringClass mc in mcc)
            {
                items.Add(mc.Name);
            }
            if (items.Count > 0)
            {
                items.Sort();
                boxScheduleClass.Items.Clear();
                foreach (String item in items)
                {
                    boxGenericClass.Items.Add(item);
                }
                boxGenericClass.SelectedIndex = 0;
            }
            btnGenericLoadClasses.Text = "Load Classes";
            btnGenericLoadClasses.Enabled = true;
        }

        private void boxWebAppRMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxWebAppRMS.Text);
            mg.Reconnect();
        }
        private void btnWebAppSearch_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWebAppSearch.Text = "Running...";
            btnWebAppSearch.Enabled = false;
            statusBar.Value = 50;

            ArrayList results = searchClass(boxWebAppSearch.Text, "Microsoft.SystemCenter.WebApplication.Perspective", true);
            foreach (String result in results)
            {
                if (!boxWebAppSearchResults.Items.Contains(result))
                    boxWebAppSearchResults.Items.Add(result);
            }
            statusBar.Value = 100;
            btnWebAppSearch.Text = "Search";
            btnWebAppSearch.Enabled = true;
            statusBar.Value = 0;
        }
        private void btnWebAppClear_Click(object sender, EventArgs e)
        {
            boxWebAppSearchResults.Items.Clear();
        }
        private void btnWebAppSelectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxWebAppSearchResults.Items.Count; index++)
            {
                boxWebAppSearchResults.SetItemChecked(index, true);
            }
        }
        private void btnWebAppUnselectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxWebAppSearchResults.Items.Count; index++)
            {
                boxWebAppSearchResults.SetItemChecked(index, false);
            }
        }
        private void btnWebAppStartMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWebAppStartMM.Text = "Running...";
            btnWebAppStartMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxWebAppSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxWebAppSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedObject in boxWebAppSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = putObjectInMM(selectedObject, "Microsoft.SystemCenter.WebApplication.Perspective", boxWebAppStartTime.Value, boxWebAppEndTime.Value, boxWebAppReasonCategory.Text, boxWebAppReason.Text, true, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnWebAppStartMM.Text = "Start Maintenance Mode";
            btnWebAppStartMM.Enabled = true;
        }
        private void btnWebAppCheckMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWebAppCheckMM.Text = "Running...";
            btnWebAppCheckMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxWebAppSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxWebAppSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxWebAppSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = checkObjectMM(selectedClassObject, "Microsoft.SystemCenter.WebApplication.Perspective", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnWebAppCheckMM.Text = "Check Maintenance Mode";
            btnWebAppCheckMM.Enabled = true;
        }
        private void btnWebAppStopMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnWebAppStopMM.Text = "Running...";
            btnWebAppStopMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxWebAppSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxWebAppSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxWebAppSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = stopObjectMM(selectedClassObject, "Microsoft.SystemCenter.WebApplication.Perspective", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnWebAppStopMM.Text = "Stop Maintenance Mode";
            btnWebAppStopMM.Enabled = true;
        }

        private void boxNetworkDeviceRMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxNetworkDeviceRMS.Text);
            mg.Reconnect();
        }
        private void btnNetworkDeviceSearch_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnNetworkDeviceSearch.Text = "Running...";
            btnNetworkDeviceSearch.Enabled = false;
            statusBar.Value = 50;

            ArrayList results = searchClass(boxNetworkDeviceSearch.Text, "System.NetworkDevice", true);
            foreach (String result in results)
            {
                if (!boxNetworkDeviceSearchResults.Items.Contains(result))
                    boxNetworkDeviceSearchResults.Items.Add(result);
            }
            statusBar.Value = 100;
            btnNetworkDeviceSearch.Text = "Search";
            btnNetworkDeviceSearch.Enabled = true;
            statusBar.Value = 0;
        }
        private void btnNetworkDeviceClear_Click(object sender, EventArgs e)
        {
            boxNetworkDeviceSearchResults.Items.Clear();
        }
        private void btnNetworkDeviceSelectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxNetworkDeviceSearchResults.Items.Count; index++)
            {
                boxNetworkDeviceSearchResults.SetItemChecked(index, true);
            }
        }
        private void btnNetworkDeviceUnselectAll_Click(object sender, EventArgs e)
        {

            for (int index = 0; index < boxNetworkDeviceSearchResults.Items.Count; index++)
            {
                boxNetworkDeviceSearchResults.SetItemChecked(index, false);
            }
        }
        private void btnNetworkDeviceStartMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnNetworkDeviceStartMM.Text = "Running...";
            btnNetworkDeviceStartMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxNetworkDeviceSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxNetworkDeviceSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedObject in boxNetworkDeviceSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = putObjectInMM(selectedObject, "System.NetworkDevice", boxNetworkDeviceStartTime.Value, boxNetworkDeviceEndTime.Value, boxNetworkDeviceReasonCategory.Text, boxNetworkDeviceReason.Text, true, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnNetworkDeviceStartMM.Text = "Start Maintenance Mode";
            btnNetworkDeviceStartMM.Enabled = true;
        }
        private void btnNetworkDeviceCheckMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnNetworkDeviceCheckMM.Text = "Running...";
            btnNetworkDeviceCheckMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxNetworkDeviceSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxNetworkDeviceSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxNetworkDeviceSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = checkObjectMM(selectedClassObject, "System.NetworkDevice", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnNetworkDeviceCheckMM.Text = "Check Maintenance Mode";
            btnNetworkDeviceCheckMM.Enabled = true;
        }
        private void btnNetworkDeviceStopMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnNetworkDeviceStopMM.Text = "Running...";
            btnNetworkDeviceStopMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxNetworkDeviceSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxNetworkDeviceSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxNetworkDeviceSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = stopObjectMM(selectedClassObject, "System.NetworkDevice", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnNetworkDeviceStopMM.Text = "Stop Maintenance Mode";
            btnNetworkDeviceStopMM.Enabled = true;
        }

        private void boxUnixRMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxUnixRMS.Text);
            mg.Reconnect();
        }
        private void btnUnixSearch_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnUnixSearch.Text = "Running...";
            btnUnixSearch.Enabled = false;
            statusBar.Value = 50;

            ArrayList results = searchClass(boxUnixSearch.Text, "Microsoft.Unix.Computer", true);
            foreach (String result in results)
            {
                if (!boxUnixSearchResults.Items.Contains(result))
                    boxUnixSearchResults.Items.Add(result);
            }
            statusBar.Value = 100;
            btnUnixSearch.Text = "Search";
            btnUnixSearch.Enabled = true;
            statusBar.Value = 0;
        }
        private void btnUnixClear_Click(object sender, EventArgs e)
        {
            boxUnixSearchResults.Items.Clear();
        }
        private void btnUnixSelectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxUnixSearchResults.Items.Count; index++)
            {
                boxUnixSearchResults.SetItemChecked(index, true);
            }
        }
        private void btnUnixUnselectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxUnixSearchResults.Items.Count; index++)
            {
                boxUnixSearchResults.SetItemChecked(index, false);
            }
        }
        private void btnUnixStartMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnUnixStartMM.Text = "Running...";
            btnUnixStartMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxUnixSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxUnixSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedObject in boxUnixSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = putObjectInMM(selectedObject, "Microsoft.Unix.Computer", boxUnixStartTime.Value, boxUnixEndTime.Value, boxUnixReasonCategory.Text, boxUnixReason.Text, true, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnUnixStartMM.Text = "Start Maintenance Mode";
            btnUnixStartMM.Enabled = true;
        }
        private void btnUnixCheckMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnUnixCheckMM.Text = "Running...";
            btnUnixCheckMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxUnixSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxUnixSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxUnixSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = checkObjectMM(selectedClassObject, "Microsoft.Unix.Computer", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnUnixCheckMM.Text = "Check Maintenance Mode";
            btnUnixCheckMM.Enabled = true;
        }
        private void btnUnixStopMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnUnixStopMM.Text = "Running...";
            btnUnixStopMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxUnixSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxUnixSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxUnixSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = stopObjectMM(selectedClassObject, "Microsoft.Unix.Computer", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnUnixStopMM.Text = "Stop Maintenance Mode";
            btnUnixStopMM.Enabled = true;
        }

        private void boxGroupRMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxGroupRMS.Text);
            mg.Reconnect();
        }
        private void btnGroupSearch_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnGroupSearch.Text = "Running...";
            btnGroupSearch.Enabled = false;
            statusBar.Value = 50;

            ArrayList results = searchClass(boxGroupSearch.Text, "System.Group", true);
            foreach (String result in results)
            {
                if (!boxGroupSearchResults.Items.Contains(result))
                    boxGroupSearchResults.Items.Add(result);
            }
            statusBar.Value = 100;
            btnGroupSearch.Text = "Search";
            btnGroupSearch.Enabled = true;
            statusBar.Value = 0;
        }
        private void btnGroupClear_Click(object sender, EventArgs e)
        {
            boxGroupSearchResults.Items.Clear();
        }
        private void btnGroupSelectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxGroupSearchResults.Items.Count; index++)
            {
                boxGroupSearchResults.SetItemChecked(index, true);
            }
        }
        private void btnGroupUnselectAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxGroupSearchResults.Items.Count; index++)
            {
                boxGroupSearchResults.SetItemChecked(index, false);
            }
        }
        private void btnGroupStartMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnGroupStartMM.Text = "Running...";
            btnGroupStartMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxGroupSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxGroupSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedObject in boxGroupSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = putObjectInMM(selectedObject, "System.Group", boxGroupStartTime.Value, boxGroupEndTime.Value, boxGroupReasonCategory.Text, boxGroupReason.Text, true, true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnGroupStartMM.Text = "Start Maintenance Mode";
            btnGroupStartMM.Enabled = true;
        }
        private void btnGroupCheckMM_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnGroupCheckMM.Text = "Running...";
            btnGroupCheckMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxGroupSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxGroupSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxGroupSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = checkObjectMM(selectedClassObject, "System.Group", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnGroupCheckMM.Text = "Check Maintenance Mode";
            btnGroupCheckMM.Enabled = true;
        }
        private void btnGroupStopMM_Click(object sender, EventArgs e)
        {

            statusBar.Value = 30;
            btnGroupStopMM.Text = "Running...";
            btnGroupStopMM.Enabled = false;
            statusBar.Value = 50;

            ArrayList Results = new ArrayList();
            if (boxGroupSearchResults.CheckedItems.Count > 0)
                statusBar.Step = 50 / (boxGroupSearchResults.CheckedItems.Count * 2);
            else
                statusBar.Step = 100;

            foreach (String selectedClassObject in boxGroupSearchResults.CheckedItems)
            {
                statusBar.PerformStep();
                ArrayList Temp = stopObjectMM(selectedClassObject, "System.Group", true);
                Results.Add(Temp);
            }

            String outputString = "";

            foreach (ArrayList entry in Results)
            {
                statusBar.PerformStep();
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                foreach (String line in entry)
                {
                    outputString += line + "\n";
                }
                outputString += "--------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
            }

            OLD_INDEX = tabControl.SelectedIndex;
            tabControl.SelectedIndex = RESULTS_INDEX;
            boxResults.Text = outputString;

            statusBar.Value = 0;
            btnGroupStopMM.Text = "Stop Maintenance Mode";
            btnGroupStopMM.Enabled = true;
        }

        private void pageSearch_Enter(object sender, EventArgs e)
        {
            try
            {
                mg = new ManagementGroup(boxWindowsComputerSearchRMS.Text);
                mg.Reconnect();
            }
            catch (Exception)
            {
            }
        }
        private void pageStandard_Enter(object sender, EventArgs e)
        {
            try
            {
                mg = new ManagementGroup(boxManagementGroup.Text);
                mg.Reconnect();
            }
            catch (Exception)
            {
            }
        }
        private void pageGeneric_Enter(object sender, EventArgs e)
        {
            try
            {
                mg = new ManagementGroup(boxGenericRMS.Text);
                mg.Reconnect();
            }
            catch (Exception)
            {
            }
        }
        private void pageWebApp_Enter(object sender, EventArgs e)
        {
            try
            {
                mg = new ManagementGroup(boxWebAppRMS.Text);
                mg.Reconnect();
            }
            catch (Exception)
            {
            }
        }
        private void pageNetworkDevice_Enter(object sender, EventArgs e)
        {
            try
            {
                mg = new ManagementGroup(boxNetworkDeviceRMS.Text);
                mg.Reconnect();
            }
            catch (Exception)
            {
            }
        }
        private void pageUnix_Enter(object sender, EventArgs e)
        {
            try
            {
                mg = new ManagementGroup(boxUnixRMS.Text);
                mg.Reconnect();
            }
            catch (Exception)
            {
            }
        }
        private void pageGroup_Enter(object sender, EventArgs e)
        {
            try
            {
                mg = new ManagementGroup(boxGroupRMS.Text);
                mg.Reconnect();
            }
            catch (Exception)
            {
            }
        }

        private ArrayList putObjectInMM(String DisplayName, String Class, DateTime StartTime, DateTime EndTime, String ReasonCategory, String Reason, bool recurse, bool match)
        {
            ArrayList Results = new ArrayList();
            try
            {
                if (match)
                {
                    MonitoringClass mc = mg.GetMonitoringClass(SystemMonitoringClass.WindowsComputer);
                    ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(Class);
                    if (MonClassColl.Count > 0)
                        mc = MonClassColl[0];
                    else
                        MessageBox.Show("Could not find class " + Class);
                    String query = "DisplayName = '" + DisplayName + "'";

                    MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                    List<MonitoringObject> monObjects = new List<MonitoringObject>();
                    monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                    if (monObjects.Count == 1)
                    {
                        foreach (MonitoringObject monObject in monObjects)
                        {
                            if (monObject.InMaintenanceMode)
                            {
                                Results.Add(monObject.DisplayName + " is already in Maintenance Mode.");
                            }
                            else
                            {
                                if (ManagementServers.Contains(monObject.DisplayName))
                                {
                                    Results.Add(monObject.DisplayName + " is a Management Server, Cannot Place in MM");
                                }
                                else
                                {
                                    StartTime = StartTime.ToUniversalTime();
                                    EndTime = EndTime.ToUniversalTime();

                                    MaintenanceModeReason reason = MaintenanceModeReason.UnplannedOther;
                                    switch (ReasonCategory)
                                    {
                                        case "ApplicationInstallation":
                                            reason = MaintenanceModeReason.ApplicationInstallation;
                                            break;
                                        case "ApplicationUnresponsive":
                                            reason = MaintenanceModeReason.ApplicationUnresponsive;
                                            break;
                                        case "ApplicationUnstable":
                                            reason = MaintenanceModeReason.ApplicationUnstable;
                                            break;
                                        case "LossOfNetworkConnectivity":
                                            reason = MaintenanceModeReason.LossOfNetworkConnectivity;
                                            break;
                                        case "PlannedApplicationMaintenance":
                                            reason = MaintenanceModeReason.PlannedApplicationMaintenance;
                                            break;
                                        case "PlannedHardwareInstallation":
                                            reason = MaintenanceModeReason.PlannedHardwareInstallation;
                                            break;
                                        case "PlannedHardwareMaintenance":
                                            reason = MaintenanceModeReason.PlannedHardwareMaintenance;
                                            break;
                                        case "PlannedOperatingSystemReconfiguration":
                                            reason = MaintenanceModeReason.PlannedOperatingSystemReconfiguration;
                                            break;
                                        case "PlannedOther":
                                            reason = MaintenanceModeReason.PlannedOther;
                                            break;
                                        case "SecurityIssue":
                                            reason = MaintenanceModeReason.SecurityIssue;
                                            break;
                                        case "UnplannedApplicationMaintenance":
                                            reason = MaintenanceModeReason.UnplannedApplicationMaintenance;
                                            break;
                                        case "UnplannedHardwareInstallation":
                                            reason = MaintenanceModeReason.UnplannedHardwareInstallation;
                                            break;
                                        case "UnplannedHardwareMaintenance":
                                            reason = MaintenanceModeReason.UnplannedHardwareMaintenance;
                                            break;
                                        case "UnplannedOperatingSystemReconfiguration":
                                            reason = MaintenanceModeReason.UnplannedOperatingSystemReconfiguration;
                                            break;
                                        case "UnplannedOther":
                                            reason = MaintenanceModeReason.UnplannedOther;
                                            break;
                                    }
                                    if (recurse)
                                    {
                                        monObject.ScheduleMaintenanceMode(StartTime, EndTime, reason, Reason, TraversalDepth.Recursive);
                                    }
                                    else
                                        monObject.ScheduleMaintenanceMode(StartTime, EndTime, reason, Reason);

                                    Results.Add(monObject.DisplayName + " is now in Maintenance Mode");
                                }
                            }
                        }
                    }
                    else
                    {
                        Results.Add("Did not find " + DisplayName);
                    }
                }
                else
                {
                    MonitoringClassCriteria MCC = new MonitoringClassCriteria("Name LIKE '" + Class + "'");
                    System.Collections.ObjectModel.ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(MCC);
                    if (MonClassColl.Count == 0)
                        MessageBox.Show("Could not find class " + Class);
                    else
                    {

                        String query = "DisplayName = '" + DisplayName + "'";

                        foreach (MonitoringClass mc in MonClassColl)
                        {
                            MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                            List<MonitoringObject> monObjects = new List<MonitoringObject>();
                            monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                            if (monObjects.Count == 1)
                            {
                                foreach (MonitoringObject monObject in monObjects)
                                {
                                    if (monObject.InMaintenanceMode)
                                    {
                                        Results.Add(monObject.DisplayName + " is already in Maintenance Mode.");
                                    }
                                    else
                                    {
                                        StartTime = StartTime.ToUniversalTime();
                                        EndTime = EndTime.ToUniversalTime();

                                        MaintenanceModeReason reason = MaintenanceModeReason.UnplannedOther;
                                        switch (ReasonCategory)
                                        {
                                            case "ApplicationInstallation":
                                                reason = MaintenanceModeReason.ApplicationInstallation;
                                                break;
                                            case "ApplicationUnresponsive":
                                                reason = MaintenanceModeReason.ApplicationUnresponsive;
                                                break;
                                            case "ApplicationUnstable":
                                                reason = MaintenanceModeReason.ApplicationUnstable;
                                                break;
                                            case "LossOfNetworkConnectivity":
                                                reason = MaintenanceModeReason.LossOfNetworkConnectivity;
                                                break;
                                            case "PlannedApplicationMaintenance":
                                                reason = MaintenanceModeReason.PlannedApplicationMaintenance;
                                                break;
                                            case "PlannedHardwareInstallation":
                                                reason = MaintenanceModeReason.PlannedHardwareInstallation;
                                                break;
                                            case "PlannedHardwareMaintenance":
                                                reason = MaintenanceModeReason.PlannedHardwareMaintenance;
                                                break;
                                            case "PlannedOperatingSystemReconfiguration":
                                                reason = MaintenanceModeReason.PlannedOperatingSystemReconfiguration;
                                                break;
                                            case "PlannedOther":
                                                reason = MaintenanceModeReason.PlannedOther;
                                                break;
                                            case "SecurityIssue":
                                                reason = MaintenanceModeReason.SecurityIssue;
                                                break;
                                            case "UnplannedApplicationMaintenance":
                                                reason = MaintenanceModeReason.UnplannedApplicationMaintenance;
                                                break;
                                            case "UnplannedHardwareInstallation":
                                                reason = MaintenanceModeReason.UnplannedHardwareInstallation;
                                                break;
                                            case "UnplannedHardwareMaintenance":
                                                reason = MaintenanceModeReason.UnplannedHardwareMaintenance;
                                                break;
                                            case "UnplannedOperatingSystemReconfiguration":
                                                reason = MaintenanceModeReason.UnplannedOperatingSystemReconfiguration;
                                                break;
                                            case "UnplannedOther":
                                                reason = MaintenanceModeReason.UnplannedOther;
                                                break;
                                        }
                                        if (recurse)
                                            monObject.ScheduleMaintenanceMode(StartTime, EndTime, reason, Reason, TraversalDepth.Recursive);
                                        else
                                            monObject.ScheduleMaintenanceMode(StartTime, EndTime, reason, Reason, TraversalDepth.OneLevel);

                                        Results.Add(monObject.DisplayName + " is now in Maintenance Mode");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Results.Add("Failed to put " + DisplayName + " into Maintenance Mode \n" + ex.Message.ToString());
            }
            return Results;
        }
        private ArrayList checkObjectMM(String DisplayName, String Class, bool match)
        {
            ArrayList Results = new ArrayList();
            try
            {
                if (match)
                {
                    MonitoringClass mc = mg.GetMonitoringClass(SystemMonitoringClass.WindowsComputer);
                    ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(Class);
                    if (MonClassColl.Count > 0)
                        mc = MonClassColl[0];
                    else
                        MessageBox.Show("Could not find class " + Class);

                    String query = "DisplayName = '" + DisplayName + "'";

                    MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                    List<MonitoringObject> monObjects = new List<MonitoringObject>();
                    monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                    if (monObjects.Count == 1)
                    {
                        foreach (MonitoringObject monObject in monObjects)
                        {
                            if (monObject.InMaintenanceMode)
                            {
                                MaintenanceWindow window = monObject.GetMaintenanceWindow();
                                DateTime schedEndTime = window.ScheduledEndTime;
                                DateTime startTime = window.StartTime;
                                String user = window.User;
                                String comments = window.Comments;
                                statusBar.Value = 100;
                                Results.Add(monObject.DisplayName + " in maintenance mode" + "\n");
                                Results.Add("User:\t\t" + user);
                                Results.Add("Start Time:\t" + startTime.ToLocalTime().ToShortDateString() + " " + startTime.ToLocalTime().ToLongTimeString());
                                Results.Add("End Time:\t" + schedEndTime.ToLocalTime().ToShortDateString() + " " + schedEndTime.ToLocalTime().ToLongTimeString());
                                Results.Add("Comments:\t" + comments);
                            }
                            else
                            {
                                Results.Add(monObject.DisplayName + " is not in Maintenance mode");
                            }
                        }
                    }
                    else
                    {
                        Results.Add("Did not find " + DisplayName);
                    }
                }
                else
                {
                    MonitoringClassCriteria MCC = new MonitoringClassCriteria("Name LIKE '" + Class + "'");
                    System.Collections.ObjectModel.ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(MCC);
                    if (MonClassColl.Count == 0)
                        MessageBox.Show("Could not find class " + Class);
                    else
                    {
                        String query = "DisplayName = '" + DisplayName + "'";

                        foreach (MonitoringClass mc in MonClassColl)
                        {
                            MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                            List<MonitoringObject> monObjects = new List<MonitoringObject>();
                            monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                            if (monObjects.Count == 1)
                            {
                                foreach (MonitoringObject monObject in monObjects)
                                {
                                    if (monObject.InMaintenanceMode)
                                    {
                                        MaintenanceWindow window = monObject.GetMaintenanceWindow();
                                        DateTime schedEndTime = window.ScheduledEndTime;
                                        DateTime startTime = window.StartTime;
                                        String user = window.User;
                                        String comments = window.Comments;
                                        statusBar.Value = 100;
                                        Results.Add(monObject.DisplayName + " in maintenance mode" + "\n");
                                        Results.Add("User:\t\t" + user);
                                        Results.Add("Start Time:\t" + startTime.ToLocalTime().ToShortDateString() + " " + startTime.ToLocalTime().ToLongTimeString());
                                        Results.Add("End Time:\t" + schedEndTime.ToLocalTime().ToShortDateString() + " " + schedEndTime.ToLocalTime().ToLongTimeString());
                                        Results.Add("Comments:\t" + comments);
                                    }
                                    else
                                    {
                                        Results.Add(monObject.DisplayName + " is not in Maintenance mode");
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Results.Add("Failed to get Maintenance Mode information \n" + ex.Message.ToString());
            }
            return Results;
        }
        private ArrayList stopObjectMM(String DisplayName, String Class, bool match)
        {
            ArrayList Results = new ArrayList();
            String query = "DisplayName = '" + DisplayName + "'";
            try
            {
                if (match)
                {
                    MonitoringClass mc = mg.GetMonitoringClass(SystemMonitoringClass.WindowsComputer);
                    ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(Class);
                    if (MonClassColl.Count > 0)
                        mc = MonClassColl[0];
                    else
                        MessageBox.Show("Could not find class " + Class);

                    MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                    List<MonitoringObject> monObjects = new List<MonitoringObject>();
                    monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                    if (monObjects.Count == 1)
                    {
                        foreach (MonitoringObject monObject in monObjects)
                        {
                            if (monObject.InMaintenanceMode)
                            {
                                monObject.StopMaintenanceMode(DateTime.UtcNow, TraversalDepth.Recursive);

                                Results.Add(monObject.DisplayName + " is now out of Maintenance Mode.");
                            }
                            else
                            {
                                Results.Add(monObject.DisplayName + " was not in Maintenance Mode.");
                            }
                        }
                    }
                    else
                    {
                        Results.Add("Did not find " + DisplayName);
                    }
                }
                else
                {
                    MonitoringClassCriteria MCC = new MonitoringClassCriteria("Name LIKE '" + Class + "'");
                    System.Collections.ObjectModel.ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(MCC);
                    if (MonClassColl.Count == 0)
                        MessageBox.Show("Could not find class " + Class);
                    else
                    {
                        foreach (MonitoringClass mc in MonClassColl)
                        {
                            MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                            List<MonitoringObject> monObjects = new List<MonitoringObject>();
                            monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                            if (monObjects.Count == 1)
                            {
                                foreach (MonitoringObject monObject in monObjects)
                                {
                                    if (monObject.InMaintenanceMode)
                                    {
                                        monObject.StopMaintenanceMode(DateTime.UtcNow, TraversalDepth.Recursive);

                                        Results.Add(monObject.DisplayName + " is now out of Maintenance Mode.");
                                    }
                                    else
                                    {
                                        Results.Add(monObject.DisplayName + " was not in Maintenance Mode.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Results.Add("Failed to Stop Maintenance Mode on " + DisplayName + "\n" + ex.Message.ToString());
            }
            return Results;
        }

        private ArrayList searchClass(String queryInput, String Class, bool match)
        {
            ArrayList results = new ArrayList();

            try
            {
                if (match)
                {
                    MonitoringClass mc = mg.GetMonitoringClass(SystemMonitoringClass.WindowsComputer);
                    ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(Class);
                    if (MonClassColl.Count > 0)
                        mc = MonClassColl[0];
                    else
                        MessageBox.Show("Could not find class " + Class);

                    String query = "DisplayName like '" + queryInput + "'";

                    MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                    List<MonitoringObject> monObjects = new List<MonitoringObject>();
                    monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                    if (monObjects.Count > 0)
                    {
                        foreach (MonitoringObject monObject in monObjects)
                        {
                            results.Add(monObject.DisplayName);
                        }
                        results.Sort();
                    }
                    else
                    {
                        MessageBox.Show("Did not find any match");
                    }
                }
                else
                {
                    MonitoringClassCriteria MCC = new MonitoringClassCriteria("Name LIKE '" + Class + "'");
                    System.Collections.ObjectModel.ReadOnlyCollection<MonitoringClass> MonClassColl = mg.GetMonitoringClasses(MCC);
                    if (MonClassColl.Count == 0)
                        MessageBox.Show("Could not find class " + Class);
                    else
                    {
                        String query = "DisplayName like '" + queryInput + "'";
                        foreach (MonitoringClass mc in MonClassColl)
                        {
                            MonitoringObjectGenericCriteria criteria = new MonitoringObjectGenericCriteria(query);
                            List<MonitoringObject> monObjects = new List<MonitoringObject>();
                            monObjects.AddRange(mg.GetMonitoringObjects(criteria, mc));

                            if (monObjects.Count > 0)
                            {
                                foreach (MonitoringObject monObject in monObjects)
                                {
                                    results.Add(monObject.DisplayName);
                                }
                            }
                        }
                        if (results.Count == 0)
                        {
                            MessageBox.Show("Did not find any match");
                        }
                        else
                        {
                            results.Sort();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Search \n" + ex.Message.ToString());
            }
            return results;
        }

        private void btnResultsReturn_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = OLD_INDEX;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.ActiveForm.Close();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author:\tRyan Andorfer\nVersion:\t1.2\nContact:\tRyan.Andorfer@gmail.com\nDisclaimer:\tProvided 'AS IS' with no warranties, and confers no rights");
        }

        private void btnSearchHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use SQL 'Like' syntax for search\n\nExamples:\n\n%.contoso.com\tReturns all objects with names ending in \".contoso.com\"\ntest%\t\tReturns all objects with names starting with \"test\"\n%mtvcsp%\tReturns all results with \"mtvcsp\" in their name");
        }
        private void btnStandardNameHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please use Fully Qualified Domain Name (FQDN)\nExample:\n\nTEST.CONTOSO.COM");
        }

        private void btnScheduleCommonClass_Click(object sender, EventArgs e)
        {
            boxScheduleClass.Items.Clear();
            boxScheduleClass.Items.Add("Microsoft.Windows.Computer");
            boxScheduleClass.Items.Add("Microsoft.Windows.Cluster");
            boxScheduleClass.Items.Add("System.Group");
            boxScheduleClass.Items.Add("System.NetworkDevice");
            boxScheduleClass.Items.Add("Microsoft.Unix.Computer");
            boxScheduleClass.Items.Add("Microsoft.SystemCenter.WebApplication.Perspective");
            boxScheduleClass.SelectedIndex = 0;
        }
        private void btnScheduleAllClasses_Click(object sender, EventArgs e)
        {
            btnScheduleAllClasses.Enabled = false;
            System.Collections.ObjectModel.ReadOnlyCollection<MonitoringClass> mcc = mg.GetMonitoringClasses();
            ArrayList items = new ArrayList();
            foreach (MonitoringClass mc in mcc)
            {
                items.Add(mc.Name);
            }
            if (items.Count > 0)
            {
                items.Sort();
                boxScheduleClass.Items.Clear();
                foreach (String item in items)
                {
                    boxScheduleClass.Items.Add(item);
                }
                boxScheduleClass.SelectedIndex = 0;
            }

            btnScheduleAllClasses.Text = "Load Classes";
            btnScheduleAllClasses.Enabled = true;
        }
        private void pageSchedule_Enter(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxScheduleRMS.Text);
            mg.Reconnect();
        }
        private void boxScheduleRMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg = new ManagementGroup(boxScheduleRMS.Text);
            mg.Reconnect();
        }
        private void btnScheduleSearch_Click(object sender, EventArgs e)
        {
            statusBar.Value = 30;
            btnScheduleSearch.Text = "Running...";
            btnScheduleSearch.Enabled = false;
            statusBar.Value = 50;

            ArrayList results = searchClass(boxScheduleSearch.Text, boxScheduleClass.Text, true);
            foreach (String result in results)
            {
                if (!boxScheduleSearchResults.Items.Contains(result))
                    boxScheduleSearchResults.Items.Add(result);
            }
            statusBar.Value = 100;
            btnScheduleSearch.Text = "Search";
            btnScheduleSearch.Enabled = true;
            statusBar.Value = 0;
        }
        private void BtnScheduleClear_Click(object sender, EventArgs e)
        {
            boxScheduleSearchResults.Items.Clear();
        }
        private void btnScheduleCheckAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxScheduleSearchResults.Items.Count; index++)
            {
                boxScheduleSearchResults.SetItemChecked(index, true);
            }
        }
        private void btnScheduleUncheckAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < boxScheduleSearchResults.Items.Count; index++)
            {
                boxScheduleSearchResults.SetItemChecked(index, false);
            }
        }
        private void btnScheduleGo_Click(object sender, EventArgs e)
        {
            String names = "";
            foreach (String dName in boxScheduleSearchResults.CheckedItems)
            {
                names += dName + ",";
            }

            try
            {
                names = names.Substring(0, names.Length - 1);
                String taskCommand = "/Create /S " + boxScheduleRemoteComputer.Text + " /U " + boxScheduleUserName.Text + " /P " + boxSchedulePassword.Text + " /SC ";
                if (radioScheduleOnce.Checked)
                {
                    taskCommand += " ONCE ";
                }
                else
                {
                    if (radioScheduleDaily.Checked)
                    {

                    }
                    else
                    {

                    }
                }
                taskCommand += " /TN " + boxScheduleTaskName.Text + " /SD ";
                String TempDateTime = boxScheduleStartTime.Value.ToShortDateString();

                string temp = TempDateTime.Split('/')[0];
                if (temp.Length != 2)
                {
                    TempDateTime = TempDateTime.Insert(0, "0");
                }

                taskCommand += TempDateTime + " /ST " + boxScheduleStartTime.Value.Hour + ":" + boxScheduleStartTime.Value.Minute + ":" + boxScheduleStartTime.Value.Second + " /TR \"\"C:\\SCOMMaintenance Mode\\scomConsoleMM.exe\" startObjectMM " + names + " " + boxScheduleRMS.Text + " " + boxScheduleClass.Text + " " + boxScheduleRunMinutes.Text + " " + boxScheduleReasonCategory.Text + " \"" + boxScheduleComments.Text + "\" true\"";
                MessageBox.Show(taskCommand);
                boxScheduleComments.Text = taskCommand;
                System.Diagnostics.ProcessStartInfo PSI = new System.Diagnostics.ProcessStartInfo("C:\\Windows\\System32\\SCHTASKS.exe", taskCommand);
                PSI.UseShellExecute = false;
                System.Diagnostics.Process.Start(PSI);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create Scheduled Maintenance Mode\n" + ex.Message.ToString());
            }
        }

        private string ToDMTFTime(DateTime dateParam)
        {
            string tempString = dateParam.ToString("********HHmmss.ffffff");
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(dateParam);
            tempString += (tickOffset.Ticks >= 0) ? '+' : '-';
            tempString += (Math.Abs(tickOffset.Ticks) / System.TimeSpan.TicksPerMinute).ToString("d3");
            return tempString;
        }

        private void radioScheduleOnce_CheckedChanged(object sender, EventArgs e)
        {
            panelDays.Visible = false;
        }

        private void radioScheduleDaily_CheckedChanged(object sender, EventArgs e)
        {
            panelDays.Visible = false;
        }

        private void radioScheduleWeekly_CheckedChanged(object sender, EventArgs e)
        {
            panelDays.Visible = true;
        }
    }
}