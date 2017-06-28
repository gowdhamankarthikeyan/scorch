namespace SCOM_Maintenance_Mode_2012
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pageStandard = new System.Windows.Forms.TabPage();
            this.btnStandardNameHelp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.boxMaintenanceType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.boxMaintenanceStartTime = new System.Windows.Forms.DateTimePicker();
            this.btnStopMaintenance = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.boxComments = new System.Windows.Forms.RichTextBox();
            this.boxManagementGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCheckMaintenance = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.boxDateTime = new System.Windows.Forms.DateTimePicker();
            this.btnMaintenanceMode = new System.Windows.Forms.Button();
            this.lblComputerName = new System.Windows.Forms.Label();
            this.boxComputerName = new System.Windows.Forms.TextBox();
            this.pageWindowsComputerSearch = new System.Windows.Forms.TabPage();
            this.btnWindowsComputerSearchHelp = new System.Windows.Forms.Button();
            this.btnWindowsComputerSearchSearchResultsUnselectAll = new System.Windows.Forms.Button();
            this.btnWindowsComputerSearchSearchResultsSelectAll = new System.Windows.Forms.Button();
            this.boxWindowsComputerSearchSearchResults = new System.Windows.Forms.CheckedListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.boxWindowsComputerSearchStartTime = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.boxWindowsComputerSearchEndTime = new System.Windows.Forms.DateTimePicker();
            this.btnWindowsComputerSearchStopMM = new System.Windows.Forms.Button();
            this.btnWindowsComputerSearchCheckMM = new System.Windows.Forms.Button();
            this.btnWindowsComputerSearchStartMM = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.boxWindowsComputerSearchReason = new System.Windows.Forms.RichTextBox();
            this.btnWindowsComputerSearchSearchResultsClear = new System.Windows.Forms.Button();
            this.btnWindowsComputerSearchSearchResultsSearch = new System.Windows.Forms.Button();
            this.boxWindowsComputerSearchReasonCategory = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.boxWindowsComputerSearchRMS = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.boxWindowsComputerSearchComputerSearch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pageWebApp = new System.Windows.Forms.TabPage();
            this.btnWebAppSearchHelp = new System.Windows.Forms.Button();
            this.btnWebAppUnselectAll = new System.Windows.Forms.Button();
            this.btnWebAppSelectAll = new System.Windows.Forms.Button();
            this.boxWebAppSearchResults = new System.Windows.Forms.CheckedListBox();
            this.label26 = new System.Windows.Forms.Label();
            this.boxWebAppStartTime = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.boxWebAppEndTime = new System.Windows.Forms.DateTimePicker();
            this.btnWebAppStopMM = new System.Windows.Forms.Button();
            this.btnWebAppCheckMM = new System.Windows.Forms.Button();
            this.btnWebAppStartMM = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.boxWebAppReason = new System.Windows.Forms.RichTextBox();
            this.btnWebAppClear = new System.Windows.Forms.Button();
            this.btnWebAppSearch = new System.Windows.Forms.Button();
            this.boxWebAppReasonCategory = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.boxWebAppRMS = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.boxWebAppSearch = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.pageNetworkDevice = new System.Windows.Forms.TabPage();
            this.btnNetworkDeviceSearchHelp = new System.Windows.Forms.Button();
            this.btnNetworkDeviceUnselectAll = new System.Windows.Forms.Button();
            this.btnNetworkDeviceSelectAll = new System.Windows.Forms.Button();
            this.boxNetworkDeviceSearchResults = new System.Windows.Forms.CheckedListBox();
            this.label33 = new System.Windows.Forms.Label();
            this.boxNetworkDeviceStartTime = new System.Windows.Forms.DateTimePicker();
            this.label34 = new System.Windows.Forms.Label();
            this.boxNetworkDeviceEndTime = new System.Windows.Forms.DateTimePicker();
            this.btnNetworkDeviceStopMM = new System.Windows.Forms.Button();
            this.btnNetworkDeviceCheckMM = new System.Windows.Forms.Button();
            this.btnNetworkDeviceStartMM = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.boxNetworkDeviceReason = new System.Windows.Forms.RichTextBox();
            this.btnNetworkDeviceClear = new System.Windows.Forms.Button();
            this.btnNetworkDeviceSearch = new System.Windows.Forms.Button();
            this.boxNetworkDeviceReasonCategory = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.boxNetworkDeviceRMS = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.boxNetworkDeviceSearch = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.pageUnixComputer = new System.Windows.Forms.TabPage();
            this.btnUnixSearchHelp = new System.Windows.Forms.Button();
            this.btnUnixUnselectAll = new System.Windows.Forms.Button();
            this.btnUnixSelectAll = new System.Windows.Forms.Button();
            this.boxUnixSearchResults = new System.Windows.Forms.CheckedListBox();
            this.label40 = new System.Windows.Forms.Label();
            this.boxUnixStartTime = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.boxUnixEndTime = new System.Windows.Forms.DateTimePicker();
            this.btnUnixStopMM = new System.Windows.Forms.Button();
            this.btnUnixCheckMM = new System.Windows.Forms.Button();
            this.btnUnixStartMM = new System.Windows.Forms.Button();
            this.label42 = new System.Windows.Forms.Label();
            this.boxUnixReason = new System.Windows.Forms.RichTextBox();
            this.btnUnixClear = new System.Windows.Forms.Button();
            this.btnUnixSearch = new System.Windows.Forms.Button();
            this.boxUnixReasonCategory = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.boxUnixRMS = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.boxUnixSearch = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.pageGroup = new System.Windows.Forms.TabPage();
            this.btnGroupSearchHelp = new System.Windows.Forms.Button();
            this.btnGroupUnselectAll = new System.Windows.Forms.Button();
            this.btnGroupSelectAll = new System.Windows.Forms.Button();
            this.boxGroupSearchResults = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.boxGroupStartTime = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.boxGroupEndTime = new System.Windows.Forms.DateTimePicker();
            this.btnGroupStopMM = new System.Windows.Forms.Button();
            this.btnGroupCheckMM = new System.Windows.Forms.Button();
            this.btnGroupStartMM = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.boxGroupReason = new System.Windows.Forms.RichTextBox();
            this.btnGroupClear = new System.Windows.Forms.Button();
            this.btnGroupSearch = new System.Windows.Forms.Button();
            this.boxGroupReasonCategory = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.boxGroupRMS = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.boxGroupSearch = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.pageGeneric = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.btnGenericLoadClasses = new System.Windows.Forms.Button();
            this.boxGenericClass = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnGenericUnselectAll = new System.Windows.Forms.Button();
            this.btnGenericSelectAll = new System.Windows.Forms.Button();
            this.boxGenericSearchResults = new System.Windows.Forms.CheckedListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.boxGenericStartTime = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.boxGenericEndTime = new System.Windows.Forms.DateTimePicker();
            this.btnGenericStopMM = new System.Windows.Forms.Button();
            this.btnGenericCheckMM = new System.Windows.Forms.Button();
            this.btnGenericStartMM = new System.Windows.Forms.Button();
            this.boxGenericReason = new System.Windows.Forms.RichTextBox();
            this.btnGenericClear = new System.Windows.Forms.Button();
            this.btnGenericSearch = new System.Windows.Forms.Button();
            this.boxGenericReasonCategory = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.boxGenericRMS = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.boxGenericSearch = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.pageSchedule = new System.Windows.Forms.TabPage();
            this.boxSchedulePassword = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.boxScheduleRemoteComputer = new System.Windows.Forms.TextBox();
            this.boxScheduleSearchResults = new System.Windows.Forms.CheckedListBox();
            this.boxScheduleReasonCategory = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.boxScheduleComments = new System.Windows.Forms.RichTextBox();
            this.boxScheduleUserName = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.panelDays = new System.Windows.Forms.Panel();
            this.boxScheduleSunday = new System.Windows.Forms.CheckBox();
            this.boxScheduleSaturday = new System.Windows.Forms.CheckBox();
            this.boxScheduleFriday = new System.Windows.Forms.CheckBox();
            this.label53 = new System.Windows.Forms.Label();
            this.boxScheduleThursday = new System.Windows.Forms.CheckBox();
            this.boxScheduleWednessday = new System.Windows.Forms.CheckBox();
            this.boxScheduleTuesday = new System.Windows.Forms.CheckBox();
            this.boxScheduleMonday = new System.Windows.Forms.CheckBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.boxScheduleStartTime = new System.Windows.Forms.DateTimePicker();
            this.label50 = new System.Windows.Forms.Label();
            this.radioScheduleWeekly = new System.Windows.Forms.RadioButton();
            this.radioScheduleDaily = new System.Windows.Forms.RadioButton();
            this.radioScheduleOnce = new System.Windows.Forms.RadioButton();
            this.boxScheduleRunMinutes = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.btnScheduleUncheckAll = new System.Windows.Forms.Button();
            this.btnScheduleCheckAll = new System.Windows.Forms.Button();
            this.BtnScheduleClear = new System.Windows.Forms.Button();
            this.btnScheduleSearch = new System.Windows.Forms.Button();
            this.btnScheduleCommonClass = new System.Windows.Forms.Button();
            this.btnScheduleSearchHelp = new System.Windows.Forms.Button();
            this.btnScheduleAllClasses = new System.Windows.Forms.Button();
            this.boxScheduleClass = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.btnScheduleGo = new System.Windows.Forms.Button();
            this.boxScheduleRMS = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.boxScheduleSearch = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.pageResults = new System.Windows.Forms.TabPage();
            this.btnResultsReturn = new System.Windows.Forms.Button();
            this.boxResults = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label62 = new System.Windows.Forms.Label();
            this.boxScheduleTaskName = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.pageStandard.SuspendLayout();
            this.pageWindowsComputerSearch.SuspendLayout();
            this.pageWebApp.SuspendLayout();
            this.pageNetworkDevice.SuspendLayout();
            this.pageUnixComputer.SuspendLayout();
            this.pageGroup.SuspendLayout();
            this.pageGeneric.SuspendLayout();
            this.pageSchedule.SuspendLayout();
            this.panelDays.SuspendLayout();
            this.pageResults.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(646, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBar
            // 
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(620, 16);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.pageStandard);
            this.tabControl.Controls.Add(this.pageWindowsComputerSearch);
            this.tabControl.Controls.Add(this.pageWebApp);
            this.tabControl.Controls.Add(this.pageNetworkDevice);
            this.tabControl.Controls.Add(this.pageUnixComputer);
            this.tabControl.Controls.Add(this.pageGroup);
            this.tabControl.Controls.Add(this.pageGeneric);
            this.tabControl.Controls.Add(this.pageSchedule);
            this.tabControl.Controls.Add(this.pageResults);
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(646, 534);
            this.tabControl.TabIndex = 8;
            // 
            // pageStandard
            // 
            this.pageStandard.Controls.Add(this.btnStandardNameHelp);
            this.pageStandard.Controls.Add(this.label5);
            this.pageStandard.Controls.Add(this.boxMaintenanceType);
            this.pageStandard.Controls.Add(this.label4);
            this.pageStandard.Controls.Add(this.boxMaintenanceStartTime);
            this.pageStandard.Controls.Add(this.btnStopMaintenance);
            this.pageStandard.Controls.Add(this.label3);
            this.pageStandard.Controls.Add(this.boxComments);
            this.pageStandard.Controls.Add(this.boxManagementGroup);
            this.pageStandard.Controls.Add(this.label2);
            this.pageStandard.Controls.Add(this.btnCheckMaintenance);
            this.pageStandard.Controls.Add(this.label1);
            this.pageStandard.Controls.Add(this.boxDateTime);
            this.pageStandard.Controls.Add(this.btnMaintenanceMode);
            this.pageStandard.Controls.Add(this.lblComputerName);
            this.pageStandard.Controls.Add(this.boxComputerName);
            this.pageStandard.Location = new System.Drawing.Point(4, 22);
            this.pageStandard.Name = "pageStandard";
            this.pageStandard.Padding = new System.Windows.Forms.Padding(3);
            this.pageStandard.Size = new System.Drawing.Size(638, 508);
            this.pageStandard.TabIndex = 0;
            this.pageStandard.Text = "Standard";
            this.pageStandard.UseVisualStyleBackColor = true;
            this.pageStandard.Enter += new System.EventHandler(this.pageStandard_Enter);
            // 
            // btnStandardNameHelp
            // 
            this.btnStandardNameHelp.Location = new System.Drawing.Point(208, 30);
            this.btnStandardNameHelp.Name = "btnStandardNameHelp";
            this.btnStandardNameHelp.Size = new System.Drawing.Size(17, 20);
            this.btnStandardNameHelp.TabIndex = 79;
            this.btnStandardNameHelp.Text = "?";
            this.btnStandardNameHelp.UseVisualStyleBackColor = true;
            this.btnStandardNameHelp.Click += new System.EventHandler(this.btnStandardNameHelp_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Maintenance Type";
            // 
            // boxMaintenanceType
            // 
            this.boxMaintenanceType.FormattingEnabled = true;
            this.boxMaintenanceType.Location = new System.Drawing.Point(289, 138);
            this.boxMaintenanceType.Name = "boxMaintenanceType";
            this.boxMaintenanceType.Size = new System.Drawing.Size(213, 21);
            this.boxMaintenanceType.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Maintenance Start Time";
            // 
            // boxMaintenanceStartTime
            // 
            this.boxMaintenanceStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxMaintenanceStartTime.Enabled = false;
            this.boxMaintenanceStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxMaintenanceStartTime.Location = new System.Drawing.Point(289, 56);
            this.boxMaintenanceStartTime.Name = "boxMaintenanceStartTime";
            this.boxMaintenanceStartTime.Size = new System.Drawing.Size(213, 20);
            this.boxMaintenanceStartTime.TabIndex = 1;
            // 
            // btnStopMaintenance
            // 
            this.btnStopMaintenance.Location = new System.Drawing.Point(386, 451);
            this.btnStopMaintenance.Name = "btnStopMaintenance";
            this.btnStopMaintenance.Size = new System.Drawing.Size(116, 40);
            this.btnStopMaintenance.TabIndex = 8;
            this.btnStopMaintenance.Text = "Stop Maintenance Mode";
            this.btnStopMaintenance.UseVisualStyleBackColor = true;
            this.btnStopMaintenance.Click += new System.EventHandler(this.btnStopMaintenance_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Maintenance Reason";
            // 
            // boxComments
            // 
            this.boxComments.Location = new System.Drawing.Point(119, 221);
            this.boxComments.Name = "boxComments";
            this.boxComments.Size = new System.Drawing.Size(383, 208);
            this.boxComments.TabIndex = 5;
            this.boxComments.Text = "";
            // 
            // boxManagementGroup
            // 
            this.boxManagementGroup.FormattingEnabled = true;
            this.boxManagementGroup.Location = new System.Drawing.Point(289, 108);
            this.boxManagementGroup.Name = "boxManagementGroup";
            this.boxManagementGroup.Size = new System.Drawing.Size(213, 21);
            this.boxManagementGroup.TabIndex = 3;
            this.boxManagementGroup.SelectedIndexChanged += new System.EventHandler(this.boxManagementGroup_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Root Management Server";
            // 
            // btnCheckMaintenance
            // 
            this.btnCheckMaintenance.Location = new System.Drawing.Point(250, 451);
            this.btnCheckMaintenance.Name = "btnCheckMaintenance";
            this.btnCheckMaintenance.Size = new System.Drawing.Size(116, 40);
            this.btnCheckMaintenance.TabIndex = 7;
            this.btnCheckMaintenance.Text = "Check Maintenance Mode";
            this.btnCheckMaintenance.UseVisualStyleBackColor = true;
            this.btnCheckMaintenance.Click += new System.EventHandler(this.btnCheckMaintenance_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Maintenance End Time";
            // 
            // boxDateTime
            // 
            this.boxDateTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxDateTime.Location = new System.Drawing.Point(289, 82);
            this.boxDateTime.Name = "boxDateTime";
            this.boxDateTime.Size = new System.Drawing.Size(213, 20);
            this.boxDateTime.TabIndex = 2;
            // 
            // btnMaintenanceMode
            // 
            this.btnMaintenanceMode.Location = new System.Drawing.Point(119, 451);
            this.btnMaintenanceMode.Name = "btnMaintenanceMode";
            this.btnMaintenanceMode.Size = new System.Drawing.Size(114, 40);
            this.btnMaintenanceMode.TabIndex = 6;
            this.btnMaintenanceMode.Text = "Start Maintenance Mode";
            this.btnMaintenanceMode.UseVisualStyleBackColor = true;
            this.btnMaintenanceMode.Click += new System.EventHandler(this.btnMaintenanceMode_Click);
            // 
            // lblComputerName
            // 
            this.lblComputerName.AutoSize = true;
            this.lblComputerName.Location = new System.Drawing.Point(119, 33);
            this.lblComputerName.Name = "lblComputerName";
            this.lblComputerName.Size = new System.Drawing.Size(83, 13);
            this.lblComputerName.TabIndex = 18;
            this.lblComputerName.Text = "Computer Name";
            // 
            // boxComputerName
            // 
            this.boxComputerName.Location = new System.Drawing.Point(289, 30);
            this.boxComputerName.Name = "boxComputerName";
            this.boxComputerName.Size = new System.Drawing.Size(213, 20);
            this.boxComputerName.TabIndex = 0;
            // 
            // pageWindowsComputerSearch
            // 
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchHelp);
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchSearchResultsUnselectAll);
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchSearchResultsSelectAll);
            this.pageWindowsComputerSearch.Controls.Add(this.boxWindowsComputerSearchSearchResults);
            this.pageWindowsComputerSearch.Controls.Add(this.label14);
            this.pageWindowsComputerSearch.Controls.Add(this.boxWindowsComputerSearchStartTime);
            this.pageWindowsComputerSearch.Controls.Add(this.label15);
            this.pageWindowsComputerSearch.Controls.Add(this.boxWindowsComputerSearchEndTime);
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchStopMM);
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchCheckMM);
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchStartMM);
            this.pageWindowsComputerSearch.Controls.Add(this.label12);
            this.pageWindowsComputerSearch.Controls.Add(this.boxWindowsComputerSearchReason);
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchSearchResultsClear);
            this.pageWindowsComputerSearch.Controls.Add(this.btnWindowsComputerSearchSearchResultsSearch);
            this.pageWindowsComputerSearch.Controls.Add(this.boxWindowsComputerSearchReasonCategory);
            this.pageWindowsComputerSearch.Controls.Add(this.label11);
            this.pageWindowsComputerSearch.Controls.Add(this.label8);
            this.pageWindowsComputerSearch.Controls.Add(this.boxWindowsComputerSearchRMS);
            this.pageWindowsComputerSearch.Controls.Add(this.label7);
            this.pageWindowsComputerSearch.Controls.Add(this.boxWindowsComputerSearchComputerSearch);
            this.pageWindowsComputerSearch.Controls.Add(this.label6);
            this.pageWindowsComputerSearch.Location = new System.Drawing.Point(4, 22);
            this.pageWindowsComputerSearch.Name = "pageWindowsComputerSearch";
            this.pageWindowsComputerSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pageWindowsComputerSearch.Size = new System.Drawing.Size(638, 508);
            this.pageWindowsComputerSearch.TabIndex = 1;
            this.pageWindowsComputerSearch.Text = "Windows Computer Search";
            this.pageWindowsComputerSearch.UseVisualStyleBackColor = true;
            this.pageWindowsComputerSearch.Enter += new System.EventHandler(this.pageSearch_Enter);
            // 
            // btnWindowsComputerSearchHelp
            // 
            this.btnWindowsComputerSearchHelp.Location = new System.Drawing.Point(103, 17);
            this.btnWindowsComputerSearchHelp.Name = "btnWindowsComputerSearchHelp";
            this.btnWindowsComputerSearchHelp.Size = new System.Drawing.Size(17, 20);
            this.btnWindowsComputerSearchHelp.TabIndex = 36;
            this.btnWindowsComputerSearchHelp.Text = "?";
            this.btnWindowsComputerSearchHelp.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchHelp.Click += new System.EventHandler(this.btnSearchHelp_Click);
            // 
            // btnWindowsComputerSearchSearchResultsUnselectAll
            // 
            this.btnWindowsComputerSearchSearchResultsUnselectAll.Location = new System.Drawing.Point(11, 194);
            this.btnWindowsComputerSearchSearchResultsUnselectAll.Name = "btnWindowsComputerSearchSearchResultsUnselectAll";
            this.btnWindowsComputerSearchSearchResultsUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnWindowsComputerSearchSearchResultsUnselectAll.TabIndex = 6;
            this.btnWindowsComputerSearchSearchResultsUnselectAll.Text = "Unselect All";
            this.btnWindowsComputerSearchSearchResultsUnselectAll.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchSearchResultsUnselectAll.Click += new System.EventHandler(this.btnWindowsComputerSearchSearchResultsUnselectAll_Click);
            // 
            // btnWindowsComputerSearchSearchResultsSelectAll
            // 
            this.btnWindowsComputerSearchSearchResultsSelectAll.Location = new System.Drawing.Point(11, 165);
            this.btnWindowsComputerSearchSearchResultsSelectAll.Name = "btnWindowsComputerSearchSearchResultsSelectAll";
            this.btnWindowsComputerSearchSearchResultsSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnWindowsComputerSearchSearchResultsSelectAll.TabIndex = 5;
            this.btnWindowsComputerSearchSearchResultsSelectAll.Text = "Select All";
            this.btnWindowsComputerSearchSearchResultsSelectAll.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchSearchResultsSelectAll.Click += new System.EventHandler(this.btnWindowsComputerSearchSearchResultsSelectAll_Click);
            // 
            // boxWindowsComputerSearchSearchResults
            // 
            this.boxWindowsComputerSearchSearchResults.FormattingEnabled = true;
            this.boxWindowsComputerSearchSearchResults.Location = new System.Drawing.Point(92, 94);
            this.boxWindowsComputerSearchSearchResults.Name = "boxWindowsComputerSearchSearchResults";
            this.boxWindowsComputerSearchSearchResults.Size = new System.Drawing.Size(529, 139);
            this.boxWindowsComputerSearchSearchResults.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 248);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Maintenance Start Time";
            // 
            // boxWindowsComputerSearchStartTime
            // 
            this.boxWindowsComputerSearchStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxWindowsComputerSearchStartTime.Enabled = false;
            this.boxWindowsComputerSearchStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxWindowsComputerSearchStartTime.Location = new System.Drawing.Point(170, 242);
            this.boxWindowsComputerSearchStartTime.Name = "boxWindowsComputerSearchStartTime";
            this.boxWindowsComputerSearchStartTime.Size = new System.Drawing.Size(451, 20);
            this.boxWindowsComputerSearchStartTime.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 274);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(117, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Maintenance End Time";
            // 
            // boxWindowsComputerSearchEndTime
            // 
            this.boxWindowsComputerSearchEndTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxWindowsComputerSearchEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxWindowsComputerSearchEndTime.Location = new System.Drawing.Point(170, 268);
            this.boxWindowsComputerSearchEndTime.Name = "boxWindowsComputerSearchEndTime";
            this.boxWindowsComputerSearchEndTime.Size = new System.Drawing.Size(451, 20);
            this.boxWindowsComputerSearchEndTime.TabIndex = 9;
            // 
            // btnWindowsComputerSearchStopMM
            // 
            this.btnWindowsComputerSearchStopMM.Location = new System.Drawing.Point(500, 454);
            this.btnWindowsComputerSearchStopMM.Name = "btnWindowsComputerSearchStopMM";
            this.btnWindowsComputerSearchStopMM.Size = new System.Drawing.Size(103, 40);
            this.btnWindowsComputerSearchStopMM.TabIndex = 14;
            this.btnWindowsComputerSearchStopMM.Text = "Stop Maintenance Mode";
            this.btnWindowsComputerSearchStopMM.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchStopMM.Click += new System.EventHandler(this.btnWindowsComputerSearchStopMM_Click);
            // 
            // btnWindowsComputerSearchCheckMM
            // 
            this.btnWindowsComputerSearchCheckMM.Location = new System.Drawing.Point(279, 454);
            this.btnWindowsComputerSearchCheckMM.Name = "btnWindowsComputerSearchCheckMM";
            this.btnWindowsComputerSearchCheckMM.Size = new System.Drawing.Size(110, 40);
            this.btnWindowsComputerSearchCheckMM.TabIndex = 13;
            this.btnWindowsComputerSearchCheckMM.Text = "Check Maintenance Mode";
            this.btnWindowsComputerSearchCheckMM.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchCheckMM.Click += new System.EventHandler(this.btnWindowsComputerSearchCheckMM_Click);
            // 
            // btnWindowsComputerSearchStartMM
            // 
            this.btnWindowsComputerSearchStartMM.Location = new System.Drawing.Point(34, 454);
            this.btnWindowsComputerSearchStartMM.Name = "btnWindowsComputerSearchStartMM";
            this.btnWindowsComputerSearchStartMM.Size = new System.Drawing.Size(103, 40);
            this.btnWindowsComputerSearchStartMM.TabIndex = 12;
            this.btnWindowsComputerSearchStartMM.Text = "Start Maintenance Mode";
            this.btnWindowsComputerSearchStartMM.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchStartMM.Click += new System.EventHandler(this.btnWindowsComputerSearchStartMM_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(315, 336);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Reason";
            // 
            // boxWindowsComputerSearchReason
            // 
            this.boxWindowsComputerSearchReason.Location = new System.Drawing.Point(11, 354);
            this.boxWindowsComputerSearchReason.Name = "boxWindowsComputerSearchReason";
            this.boxWindowsComputerSearchReason.Size = new System.Drawing.Size(610, 96);
            this.boxWindowsComputerSearchReason.TabIndex = 11;
            this.boxWindowsComputerSearchReason.Text = "";
            // 
            // btnWindowsComputerSearchSearchResultsClear
            // 
            this.btnWindowsComputerSearchSearchResultsClear.Location = new System.Drawing.Point(11, 136);
            this.btnWindowsComputerSearchSearchResultsClear.Name = "btnWindowsComputerSearchSearchResultsClear";
            this.btnWindowsComputerSearchSearchResultsClear.Size = new System.Drawing.Size(75, 23);
            this.btnWindowsComputerSearchSearchResultsClear.TabIndex = 4;
            this.btnWindowsComputerSearchSearchResultsClear.Text = "Clear";
            this.btnWindowsComputerSearchSearchResultsClear.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchSearchResultsClear.Click += new System.EventHandler(this.btnWindowsComputerSearchSearchResultsClear_Click);
            // 
            // btnWindowsComputerSearchSearchResultsSearch
            // 
            this.btnWindowsComputerSearchSearchResultsSearch.Location = new System.Drawing.Point(11, 107);
            this.btnWindowsComputerSearchSearchResultsSearch.Name = "btnWindowsComputerSearchSearchResultsSearch";
            this.btnWindowsComputerSearchSearchResultsSearch.Size = new System.Drawing.Size(75, 23);
            this.btnWindowsComputerSearchSearchResultsSearch.TabIndex = 3;
            this.btnWindowsComputerSearchSearchResultsSearch.Text = "Search";
            this.btnWindowsComputerSearchSearchResultsSearch.UseVisualStyleBackColor = true;
            this.btnWindowsComputerSearchSearchResultsSearch.Click += new System.EventHandler(this.btnWindowsComputerSearchSearchResultsSearch_Click);
            // 
            // boxWindowsComputerSearchReasonCategory
            // 
            this.boxWindowsComputerSearchReasonCategory.FormattingEnabled = true;
            this.boxWindowsComputerSearchReasonCategory.ItemHeight = 13;
            this.boxWindowsComputerSearchReasonCategory.Location = new System.Drawing.Point(170, 305);
            this.boxWindowsComputerSearchReasonCategory.Name = "boxWindowsComputerSearchReasonCategory";
            this.boxWindowsComputerSearchReasonCategory.Size = new System.Drawing.Size(451, 21);
            this.boxWindowsComputerSearchReasonCategory.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 314);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Maintenance Reason";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(298, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Search Results";
            // 
            // boxWindowsComputerSearchRMS
            // 
            this.boxWindowsComputerSearchRMS.FormattingEnabled = true;
            this.boxWindowsComputerSearchRMS.Location = new System.Drawing.Point(170, 45);
            this.boxWindowsComputerSearchRMS.Name = "boxWindowsComputerSearchRMS";
            this.boxWindowsComputerSearchRMS.Size = new System.Drawing.Size(451, 21);
            this.boxWindowsComputerSearchRMS.TabIndex = 2;
            this.boxWindowsComputerSearchRMS.SelectedIndexChanged += new System.EventHandler(this.boxWindowsComputerSearchRMS_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Root Management Server";
            // 
            // boxWindowsComputerSearchComputerSearch
            // 
            this.boxWindowsComputerSearchComputerSearch.Location = new System.Drawing.Point(170, 17);
            this.boxWindowsComputerSearchComputerSearch.Name = "boxWindowsComputerSearchComputerSearch";
            this.boxWindowsComputerSearchComputerSearch.Size = new System.Drawing.Size(451, 20);
            this.boxWindowsComputerSearchComputerSearch.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Computer Search";
            // 
            // pageWebApp
            // 
            this.pageWebApp.Controls.Add(this.btnWebAppSearchHelp);
            this.pageWebApp.Controls.Add(this.btnWebAppUnselectAll);
            this.pageWebApp.Controls.Add(this.btnWebAppSelectAll);
            this.pageWebApp.Controls.Add(this.boxWebAppSearchResults);
            this.pageWebApp.Controls.Add(this.label26);
            this.pageWebApp.Controls.Add(this.boxWebAppStartTime);
            this.pageWebApp.Controls.Add(this.label27);
            this.pageWebApp.Controls.Add(this.boxWebAppEndTime);
            this.pageWebApp.Controls.Add(this.btnWebAppStopMM);
            this.pageWebApp.Controls.Add(this.btnWebAppCheckMM);
            this.pageWebApp.Controls.Add(this.btnWebAppStartMM);
            this.pageWebApp.Controls.Add(this.label28);
            this.pageWebApp.Controls.Add(this.boxWebAppReason);
            this.pageWebApp.Controls.Add(this.btnWebAppClear);
            this.pageWebApp.Controls.Add(this.btnWebAppSearch);
            this.pageWebApp.Controls.Add(this.boxWebAppReasonCategory);
            this.pageWebApp.Controls.Add(this.label29);
            this.pageWebApp.Controls.Add(this.label30);
            this.pageWebApp.Controls.Add(this.boxWebAppRMS);
            this.pageWebApp.Controls.Add(this.label31);
            this.pageWebApp.Controls.Add(this.boxWebAppSearch);
            this.pageWebApp.Controls.Add(this.label32);
            this.pageWebApp.Location = new System.Drawing.Point(4, 22);
            this.pageWebApp.Name = "pageWebApp";
            this.pageWebApp.Size = new System.Drawing.Size(638, 508);
            this.pageWebApp.TabIndex = 6;
            this.pageWebApp.Text = "Web App";
            this.pageWebApp.UseVisualStyleBackColor = true;
            this.pageWebApp.Enter += new System.EventHandler(this.pageWebApp_Enter);
            // 
            // btnWebAppSearchHelp
            // 
            this.btnWebAppSearchHelp.Location = new System.Drawing.Point(103, 15);
            this.btnWebAppSearchHelp.Name = "btnWebAppSearchHelp";
            this.btnWebAppSearchHelp.Size = new System.Drawing.Size(17, 20);
            this.btnWebAppSearchHelp.TabIndex = 78;
            this.btnWebAppSearchHelp.Text = "?";
            this.btnWebAppSearchHelp.UseVisualStyleBackColor = true;
            this.btnWebAppSearchHelp.Click += new System.EventHandler(this.btnSearchHelp_Click);
            // 
            // btnWebAppUnselectAll
            // 
            this.btnWebAppUnselectAll.Location = new System.Drawing.Point(11, 192);
            this.btnWebAppUnselectAll.Name = "btnWebAppUnselectAll";
            this.btnWebAppUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnWebAppUnselectAll.TabIndex = 65;
            this.btnWebAppUnselectAll.Text = "Unselect All";
            this.btnWebAppUnselectAll.UseVisualStyleBackColor = true;
            this.btnWebAppUnselectAll.Click += new System.EventHandler(this.btnWebAppUnselectAll_Click);
            // 
            // btnWebAppSelectAll
            // 
            this.btnWebAppSelectAll.Location = new System.Drawing.Point(11, 163);
            this.btnWebAppSelectAll.Name = "btnWebAppSelectAll";
            this.btnWebAppSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnWebAppSelectAll.TabIndex = 63;
            this.btnWebAppSelectAll.Text = "Select All";
            this.btnWebAppSelectAll.UseVisualStyleBackColor = true;
            this.btnWebAppSelectAll.Click += new System.EventHandler(this.btnWebAppSelectAll_Click);
            // 
            // boxWebAppSearchResults
            // 
            this.boxWebAppSearchResults.FormattingEnabled = true;
            this.boxWebAppSearchResults.Location = new System.Drawing.Point(92, 92);
            this.boxWebAppSearchResults.Name = "boxWebAppSearchResults";
            this.boxWebAppSearchResults.Size = new System.Drawing.Size(529, 139);
            this.boxWebAppSearchResults.TabIndex = 66;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 246);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(120, 13);
            this.label26.TabIndex = 76;
            this.label26.Text = "Maintenance Start Time";
            // 
            // boxWebAppStartTime
            // 
            this.boxWebAppStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxWebAppStartTime.Enabled = false;
            this.boxWebAppStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxWebAppStartTime.Location = new System.Drawing.Point(170, 240);
            this.boxWebAppStartTime.Name = "boxWebAppStartTime";
            this.boxWebAppStartTime.Size = new System.Drawing.Size(451, 20);
            this.boxWebAppStartTime.TabIndex = 67;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 272);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(117, 13);
            this.label27.TabIndex = 75;
            this.label27.Text = "Maintenance End Time";
            // 
            // boxWebAppEndTime
            // 
            this.boxWebAppEndTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxWebAppEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxWebAppEndTime.Location = new System.Drawing.Point(170, 266);
            this.boxWebAppEndTime.Name = "boxWebAppEndTime";
            this.boxWebAppEndTime.Size = new System.Drawing.Size(451, 20);
            this.boxWebAppEndTime.TabIndex = 68;
            // 
            // btnWebAppStopMM
            // 
            this.btnWebAppStopMM.Location = new System.Drawing.Point(500, 454);
            this.btnWebAppStopMM.Name = "btnWebAppStopMM";
            this.btnWebAppStopMM.Size = new System.Drawing.Size(103, 40);
            this.btnWebAppStopMM.TabIndex = 73;
            this.btnWebAppStopMM.Text = "Stop Maintenance Mode";
            this.btnWebAppStopMM.UseVisualStyleBackColor = true;
            this.btnWebAppStopMM.Click += new System.EventHandler(this.btnWebAppStopMM_Click);
            // 
            // btnWebAppCheckMM
            // 
            this.btnWebAppCheckMM.Location = new System.Drawing.Point(279, 454);
            this.btnWebAppCheckMM.Name = "btnWebAppCheckMM";
            this.btnWebAppCheckMM.Size = new System.Drawing.Size(110, 40);
            this.btnWebAppCheckMM.TabIndex = 72;
            this.btnWebAppCheckMM.Text = "Check Maintenance Mode";
            this.btnWebAppCheckMM.UseVisualStyleBackColor = true;
            this.btnWebAppCheckMM.Click += new System.EventHandler(this.btnWebAppCheckMM_Click);
            // 
            // btnWebAppStartMM
            // 
            this.btnWebAppStartMM.Location = new System.Drawing.Point(34, 454);
            this.btnWebAppStartMM.Name = "btnWebAppStartMM";
            this.btnWebAppStartMM.Size = new System.Drawing.Size(103, 40);
            this.btnWebAppStartMM.TabIndex = 71;
            this.btnWebAppStartMM.Text = "Start Maintenance Mode";
            this.btnWebAppStartMM.UseVisualStyleBackColor = true;
            this.btnWebAppStartMM.Click += new System.EventHandler(this.btnWebAppStartMM_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(315, 336);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(44, 13);
            this.label28.TabIndex = 74;
            this.label28.Text = "Reason";
            // 
            // boxWebAppReason
            // 
            this.boxWebAppReason.Location = new System.Drawing.Point(11, 352);
            this.boxWebAppReason.Name = "boxWebAppReason";
            this.boxWebAppReason.Size = new System.Drawing.Size(610, 96);
            this.boxWebAppReason.TabIndex = 70;
            this.boxWebAppReason.Text = "";
            // 
            // btnWebAppClear
            // 
            this.btnWebAppClear.Location = new System.Drawing.Point(11, 134);
            this.btnWebAppClear.Name = "btnWebAppClear";
            this.btnWebAppClear.Size = new System.Drawing.Size(75, 23);
            this.btnWebAppClear.TabIndex = 62;
            this.btnWebAppClear.Text = "Clear";
            this.btnWebAppClear.UseVisualStyleBackColor = true;
            this.btnWebAppClear.Click += new System.EventHandler(this.btnWebAppClear_Click);
            // 
            // btnWebAppSearch
            // 
            this.btnWebAppSearch.Location = new System.Drawing.Point(11, 105);
            this.btnWebAppSearch.Name = "btnWebAppSearch";
            this.btnWebAppSearch.Size = new System.Drawing.Size(75, 23);
            this.btnWebAppSearch.TabIndex = 61;
            this.btnWebAppSearch.Text = "Search";
            this.btnWebAppSearch.UseVisualStyleBackColor = true;
            this.btnWebAppSearch.Click += new System.EventHandler(this.btnWebAppSearch_Click);
            // 
            // boxWebAppReasonCategory
            // 
            this.boxWebAppReasonCategory.FormattingEnabled = true;
            this.boxWebAppReasonCategory.ItemHeight = 13;
            this.boxWebAppReasonCategory.Location = new System.Drawing.Point(170, 303);
            this.boxWebAppReasonCategory.Name = "boxWebAppReasonCategory";
            this.boxWebAppReasonCategory.Size = new System.Drawing.Size(451, 21);
            this.boxWebAppReasonCategory.TabIndex = 69;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 312);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(109, 13);
            this.label29.TabIndex = 68;
            this.label29.Text = "Maintenance Reason";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(298, 76);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(79, 13);
            this.label30.TabIndex = 64;
            this.label30.Text = "Search Results";
            // 
            // boxWebAppRMS
            // 
            this.boxWebAppRMS.FormattingEnabled = true;
            this.boxWebAppRMS.Location = new System.Drawing.Point(170, 43);
            this.boxWebAppRMS.Name = "boxWebAppRMS";
            this.boxWebAppRMS.Size = new System.Drawing.Size(451, 21);
            this.boxWebAppRMS.TabIndex = 59;
            this.boxWebAppRMS.SelectedIndexChanged += new System.EventHandler(this.boxWebAppRMS_SelectedIndexChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(8, 52);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(129, 13);
            this.label31.TabIndex = 60;
            this.label31.Text = "Root Management Server";
            // 
            // boxWebAppSearch
            // 
            this.boxWebAppSearch.Location = new System.Drawing.Point(170, 15);
            this.boxWebAppSearch.Name = "boxWebAppSearch";
            this.boxWebAppSearch.Size = new System.Drawing.Size(451, 20);
            this.boxWebAppSearch.TabIndex = 58;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(8, 18);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(89, 13);
            this.label32.TabIndex = 57;
            this.label32.Text = "Web App Search";
            // 
            // pageNetworkDevice
            // 
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceSearchHelp);
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceUnselectAll);
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceSelectAll);
            this.pageNetworkDevice.Controls.Add(this.boxNetworkDeviceSearchResults);
            this.pageNetworkDevice.Controls.Add(this.label33);
            this.pageNetworkDevice.Controls.Add(this.boxNetworkDeviceStartTime);
            this.pageNetworkDevice.Controls.Add(this.label34);
            this.pageNetworkDevice.Controls.Add(this.boxNetworkDeviceEndTime);
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceStopMM);
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceCheckMM);
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceStartMM);
            this.pageNetworkDevice.Controls.Add(this.label35);
            this.pageNetworkDevice.Controls.Add(this.boxNetworkDeviceReason);
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceClear);
            this.pageNetworkDevice.Controls.Add(this.btnNetworkDeviceSearch);
            this.pageNetworkDevice.Controls.Add(this.boxNetworkDeviceReasonCategory);
            this.pageNetworkDevice.Controls.Add(this.label36);
            this.pageNetworkDevice.Controls.Add(this.label37);
            this.pageNetworkDevice.Controls.Add(this.boxNetworkDeviceRMS);
            this.pageNetworkDevice.Controls.Add(this.label38);
            this.pageNetworkDevice.Controls.Add(this.boxNetworkDeviceSearch);
            this.pageNetworkDevice.Controls.Add(this.label39);
            this.pageNetworkDevice.Location = new System.Drawing.Point(4, 22);
            this.pageNetworkDevice.Name = "pageNetworkDevice";
            this.pageNetworkDevice.Size = new System.Drawing.Size(638, 508);
            this.pageNetworkDevice.TabIndex = 7;
            this.pageNetworkDevice.Text = "Network Device";
            this.pageNetworkDevice.UseVisualStyleBackColor = true;
            this.pageNetworkDevice.Enter += new System.EventHandler(this.pageNetworkDevice_Enter);
            // 
            // btnNetworkDeviceSearchHelp
            // 
            this.btnNetworkDeviceSearchHelp.Location = new System.Drawing.Point(135, 16);
            this.btnNetworkDeviceSearchHelp.Name = "btnNetworkDeviceSearchHelp";
            this.btnNetworkDeviceSearchHelp.Size = new System.Drawing.Size(17, 20);
            this.btnNetworkDeviceSearchHelp.TabIndex = 57;
            this.btnNetworkDeviceSearchHelp.Text = "?";
            this.btnNetworkDeviceSearchHelp.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceSearchHelp.Click += new System.EventHandler(this.btnSearchHelp_Click);
            // 
            // btnNetworkDeviceUnselectAll
            // 
            this.btnNetworkDeviceUnselectAll.Location = new System.Drawing.Point(11, 193);
            this.btnNetworkDeviceUnselectAll.Name = "btnNetworkDeviceUnselectAll";
            this.btnNetworkDeviceUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnNetworkDeviceUnselectAll.TabIndex = 44;
            this.btnNetworkDeviceUnselectAll.Text = "Unselect All";
            this.btnNetworkDeviceUnselectAll.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceUnselectAll.Click += new System.EventHandler(this.btnNetworkDeviceUnselectAll_Click);
            // 
            // btnNetworkDeviceSelectAll
            // 
            this.btnNetworkDeviceSelectAll.Location = new System.Drawing.Point(11, 164);
            this.btnNetworkDeviceSelectAll.Name = "btnNetworkDeviceSelectAll";
            this.btnNetworkDeviceSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnNetworkDeviceSelectAll.TabIndex = 42;
            this.btnNetworkDeviceSelectAll.Text = "Select All";
            this.btnNetworkDeviceSelectAll.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceSelectAll.Click += new System.EventHandler(this.btnNetworkDeviceSelectAll_Click);
            // 
            // boxNetworkDeviceSearchResults
            // 
            this.boxNetworkDeviceSearchResults.FormattingEnabled = true;
            this.boxNetworkDeviceSearchResults.Location = new System.Drawing.Point(92, 93);
            this.boxNetworkDeviceSearchResults.Name = "boxNetworkDeviceSearchResults";
            this.boxNetworkDeviceSearchResults.Size = new System.Drawing.Size(529, 139);
            this.boxNetworkDeviceSearchResults.TabIndex = 45;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 247);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(120, 13);
            this.label33.TabIndex = 55;
            this.label33.Text = "Maintenance Start Time";
            // 
            // boxNetworkDeviceStartTime
            // 
            this.boxNetworkDeviceStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxNetworkDeviceStartTime.Enabled = false;
            this.boxNetworkDeviceStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxNetworkDeviceStartTime.Location = new System.Drawing.Point(170, 241);
            this.boxNetworkDeviceStartTime.Name = "boxNetworkDeviceStartTime";
            this.boxNetworkDeviceStartTime.Size = new System.Drawing.Size(451, 20);
            this.boxNetworkDeviceStartTime.TabIndex = 46;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(8, 273);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(117, 13);
            this.label34.TabIndex = 54;
            this.label34.Text = "Maintenance End Time";
            // 
            // boxNetworkDeviceEndTime
            // 
            this.boxNetworkDeviceEndTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxNetworkDeviceEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxNetworkDeviceEndTime.Location = new System.Drawing.Point(170, 267);
            this.boxNetworkDeviceEndTime.Name = "boxNetworkDeviceEndTime";
            this.boxNetworkDeviceEndTime.Size = new System.Drawing.Size(451, 20);
            this.boxNetworkDeviceEndTime.TabIndex = 47;
            // 
            // btnNetworkDeviceStopMM
            // 
            this.btnNetworkDeviceStopMM.Location = new System.Drawing.Point(500, 455);
            this.btnNetworkDeviceStopMM.Name = "btnNetworkDeviceStopMM";
            this.btnNetworkDeviceStopMM.Size = new System.Drawing.Size(103, 40);
            this.btnNetworkDeviceStopMM.TabIndex = 52;
            this.btnNetworkDeviceStopMM.Text = "Stop Maintenance Mode";
            this.btnNetworkDeviceStopMM.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceStopMM.Click += new System.EventHandler(this.btnNetworkDeviceStopMM_Click);
            // 
            // btnNetworkDeviceCheckMM
            // 
            this.btnNetworkDeviceCheckMM.Location = new System.Drawing.Point(279, 455);
            this.btnNetworkDeviceCheckMM.Name = "btnNetworkDeviceCheckMM";
            this.btnNetworkDeviceCheckMM.Size = new System.Drawing.Size(110, 40);
            this.btnNetworkDeviceCheckMM.TabIndex = 51;
            this.btnNetworkDeviceCheckMM.Text = "Check Maintenance Mode";
            this.btnNetworkDeviceCheckMM.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceCheckMM.Click += new System.EventHandler(this.btnNetworkDeviceCheckMM_Click);
            // 
            // btnNetworkDeviceStartMM
            // 
            this.btnNetworkDeviceStartMM.Location = new System.Drawing.Point(34, 455);
            this.btnNetworkDeviceStartMM.Name = "btnNetworkDeviceStartMM";
            this.btnNetworkDeviceStartMM.Size = new System.Drawing.Size(103, 40);
            this.btnNetworkDeviceStartMM.TabIndex = 50;
            this.btnNetworkDeviceStartMM.Text = "Start Maintenance Mode";
            this.btnNetworkDeviceStartMM.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceStartMM.Click += new System.EventHandler(this.btnNetworkDeviceStartMM_Click);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(315, 337);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(44, 13);
            this.label35.TabIndex = 53;
            this.label35.Text = "Reason";
            // 
            // boxNetworkDeviceReason
            // 
            this.boxNetworkDeviceReason.Location = new System.Drawing.Point(11, 353);
            this.boxNetworkDeviceReason.Name = "boxNetworkDeviceReason";
            this.boxNetworkDeviceReason.Size = new System.Drawing.Size(610, 96);
            this.boxNetworkDeviceReason.TabIndex = 49;
            this.boxNetworkDeviceReason.Text = "";
            // 
            // btnNetworkDeviceClear
            // 
            this.btnNetworkDeviceClear.Location = new System.Drawing.Point(11, 135);
            this.btnNetworkDeviceClear.Name = "btnNetworkDeviceClear";
            this.btnNetworkDeviceClear.Size = new System.Drawing.Size(75, 23);
            this.btnNetworkDeviceClear.TabIndex = 41;
            this.btnNetworkDeviceClear.Text = "Clear";
            this.btnNetworkDeviceClear.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceClear.Click += new System.EventHandler(this.btnNetworkDeviceClear_Click);
            // 
            // btnNetworkDeviceSearch
            // 
            this.btnNetworkDeviceSearch.Location = new System.Drawing.Point(11, 106);
            this.btnNetworkDeviceSearch.Name = "btnNetworkDeviceSearch";
            this.btnNetworkDeviceSearch.Size = new System.Drawing.Size(75, 23);
            this.btnNetworkDeviceSearch.TabIndex = 40;
            this.btnNetworkDeviceSearch.Text = "Search";
            this.btnNetworkDeviceSearch.UseVisualStyleBackColor = true;
            this.btnNetworkDeviceSearch.Click += new System.EventHandler(this.btnNetworkDeviceSearch_Click);
            // 
            // boxNetworkDeviceReasonCategory
            // 
            this.boxNetworkDeviceReasonCategory.FormattingEnabled = true;
            this.boxNetworkDeviceReasonCategory.ItemHeight = 13;
            this.boxNetworkDeviceReasonCategory.Location = new System.Drawing.Point(170, 304);
            this.boxNetworkDeviceReasonCategory.Name = "boxNetworkDeviceReasonCategory";
            this.boxNetworkDeviceReasonCategory.Size = new System.Drawing.Size(451, 21);
            this.boxNetworkDeviceReasonCategory.TabIndex = 48;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(8, 313);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(109, 13);
            this.label36.TabIndex = 47;
            this.label36.Text = "Maintenance Reason";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(298, 77);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(79, 13);
            this.label37.TabIndex = 43;
            this.label37.Text = "Search Results";
            // 
            // boxNetworkDeviceRMS
            // 
            this.boxNetworkDeviceRMS.FormattingEnabled = true;
            this.boxNetworkDeviceRMS.Location = new System.Drawing.Point(170, 44);
            this.boxNetworkDeviceRMS.Name = "boxNetworkDeviceRMS";
            this.boxNetworkDeviceRMS.Size = new System.Drawing.Size(451, 21);
            this.boxNetworkDeviceRMS.TabIndex = 38;
            this.boxNetworkDeviceRMS.SelectedIndexChanged += new System.EventHandler(this.boxNetworkDeviceRMS_SelectedIndexChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(8, 53);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(129, 13);
            this.label38.TabIndex = 39;
            this.label38.Text = "Root Management Server";
            // 
            // boxNetworkDeviceSearch
            // 
            this.boxNetworkDeviceSearch.Location = new System.Drawing.Point(170, 16);
            this.boxNetworkDeviceSearch.Name = "boxNetworkDeviceSearch";
            this.boxNetworkDeviceSearch.Size = new System.Drawing.Size(451, 20);
            this.boxNetworkDeviceSearch.TabIndex = 37;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(8, 19);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(121, 13);
            this.label39.TabIndex = 36;
            this.label39.Text = "Network Device Search";
            // 
            // pageUnixComputer
            // 
            this.pageUnixComputer.Controls.Add(this.btnUnixSearchHelp);
            this.pageUnixComputer.Controls.Add(this.btnUnixUnselectAll);
            this.pageUnixComputer.Controls.Add(this.btnUnixSelectAll);
            this.pageUnixComputer.Controls.Add(this.boxUnixSearchResults);
            this.pageUnixComputer.Controls.Add(this.label40);
            this.pageUnixComputer.Controls.Add(this.boxUnixStartTime);
            this.pageUnixComputer.Controls.Add(this.label41);
            this.pageUnixComputer.Controls.Add(this.boxUnixEndTime);
            this.pageUnixComputer.Controls.Add(this.btnUnixStopMM);
            this.pageUnixComputer.Controls.Add(this.btnUnixCheckMM);
            this.pageUnixComputer.Controls.Add(this.btnUnixStartMM);
            this.pageUnixComputer.Controls.Add(this.label42);
            this.pageUnixComputer.Controls.Add(this.boxUnixReason);
            this.pageUnixComputer.Controls.Add(this.btnUnixClear);
            this.pageUnixComputer.Controls.Add(this.btnUnixSearch);
            this.pageUnixComputer.Controls.Add(this.boxUnixReasonCategory);
            this.pageUnixComputer.Controls.Add(this.label43);
            this.pageUnixComputer.Controls.Add(this.label44);
            this.pageUnixComputer.Controls.Add(this.boxUnixRMS);
            this.pageUnixComputer.Controls.Add(this.label45);
            this.pageUnixComputer.Controls.Add(this.boxUnixSearch);
            this.pageUnixComputer.Controls.Add(this.label46);
            this.pageUnixComputer.Location = new System.Drawing.Point(4, 22);
            this.pageUnixComputer.Name = "pageUnixComputer";
            this.pageUnixComputer.Size = new System.Drawing.Size(638, 508);
            this.pageUnixComputer.TabIndex = 8;
            this.pageUnixComputer.Text = "Unix/Linux";
            this.pageUnixComputer.UseVisualStyleBackColor = true;
            this.pageUnixComputer.Enter += new System.EventHandler(this.pageUnix_Enter);
            // 
            // btnUnixSearchHelp
            // 
            this.btnUnixSearchHelp.Location = new System.Drawing.Point(79, 14);
            this.btnUnixSearchHelp.Name = "btnUnixSearchHelp";
            this.btnUnixSearchHelp.Size = new System.Drawing.Size(17, 20);
            this.btnUnixSearchHelp.TabIndex = 78;
            this.btnUnixSearchHelp.Text = "?";
            this.btnUnixSearchHelp.UseVisualStyleBackColor = true;
            this.btnUnixSearchHelp.Click += new System.EventHandler(this.btnSearchHelp_Click);
            // 
            // btnUnixUnselectAll
            // 
            this.btnUnixUnselectAll.Location = new System.Drawing.Point(11, 192);
            this.btnUnixUnselectAll.Name = "btnUnixUnselectAll";
            this.btnUnixUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnixUnselectAll.TabIndex = 65;
            this.btnUnixUnselectAll.Text = "Unselect All";
            this.btnUnixUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnixUnselectAll.Click += new System.EventHandler(this.btnUnixUnselectAll_Click);
            // 
            // btnUnixSelectAll
            // 
            this.btnUnixSelectAll.Location = new System.Drawing.Point(11, 163);
            this.btnUnixSelectAll.Name = "btnUnixSelectAll";
            this.btnUnixSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnixSelectAll.TabIndex = 63;
            this.btnUnixSelectAll.Text = "Select All";
            this.btnUnixSelectAll.UseVisualStyleBackColor = true;
            this.btnUnixSelectAll.Click += new System.EventHandler(this.btnUnixSelectAll_Click);
            // 
            // boxUnixSearchResults
            // 
            this.boxUnixSearchResults.FormattingEnabled = true;
            this.boxUnixSearchResults.Location = new System.Drawing.Point(92, 92);
            this.boxUnixSearchResults.Name = "boxUnixSearchResults";
            this.boxUnixSearchResults.Size = new System.Drawing.Size(529, 139);
            this.boxUnixSearchResults.TabIndex = 66;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(8, 246);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(120, 13);
            this.label40.TabIndex = 76;
            this.label40.Text = "Maintenance Start Time";
            // 
            // boxUnixStartTime
            // 
            this.boxUnixStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxUnixStartTime.Enabled = false;
            this.boxUnixStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxUnixStartTime.Location = new System.Drawing.Point(170, 240);
            this.boxUnixStartTime.Name = "boxUnixStartTime";
            this.boxUnixStartTime.Size = new System.Drawing.Size(451, 20);
            this.boxUnixStartTime.TabIndex = 67;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(8, 272);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(117, 13);
            this.label41.TabIndex = 75;
            this.label41.Text = "Maintenance End Time";
            // 
            // boxUnixEndTime
            // 
            this.boxUnixEndTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxUnixEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxUnixEndTime.Location = new System.Drawing.Point(170, 266);
            this.boxUnixEndTime.Name = "boxUnixEndTime";
            this.boxUnixEndTime.Size = new System.Drawing.Size(451, 20);
            this.boxUnixEndTime.TabIndex = 68;
            // 
            // btnUnixStopMM
            // 
            this.btnUnixStopMM.Location = new System.Drawing.Point(500, 454);
            this.btnUnixStopMM.Name = "btnUnixStopMM";
            this.btnUnixStopMM.Size = new System.Drawing.Size(103, 40);
            this.btnUnixStopMM.TabIndex = 73;
            this.btnUnixStopMM.Text = "Stop Maintenance Mode";
            this.btnUnixStopMM.UseVisualStyleBackColor = true;
            this.btnUnixStopMM.Click += new System.EventHandler(this.btnUnixStopMM_Click);
            // 
            // btnUnixCheckMM
            // 
            this.btnUnixCheckMM.Location = new System.Drawing.Point(279, 454);
            this.btnUnixCheckMM.Name = "btnUnixCheckMM";
            this.btnUnixCheckMM.Size = new System.Drawing.Size(110, 40);
            this.btnUnixCheckMM.TabIndex = 72;
            this.btnUnixCheckMM.Text = "Check Maintenance Mode";
            this.btnUnixCheckMM.UseVisualStyleBackColor = true;
            this.btnUnixCheckMM.Click += new System.EventHandler(this.btnUnixCheckMM_Click);
            // 
            // btnUnixStartMM
            // 
            this.btnUnixStartMM.Location = new System.Drawing.Point(34, 454);
            this.btnUnixStartMM.Name = "btnUnixStartMM";
            this.btnUnixStartMM.Size = new System.Drawing.Size(103, 40);
            this.btnUnixStartMM.TabIndex = 71;
            this.btnUnixStartMM.Text = "Start Maintenance Mode";
            this.btnUnixStartMM.UseVisualStyleBackColor = true;
            this.btnUnixStartMM.Click += new System.EventHandler(this.btnUnixStartMM_Click);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(315, 336);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(44, 13);
            this.label42.TabIndex = 74;
            this.label42.Text = "Reason";
            // 
            // boxUnixReason
            // 
            this.boxUnixReason.Location = new System.Drawing.Point(11, 352);
            this.boxUnixReason.Name = "boxUnixReason";
            this.boxUnixReason.Size = new System.Drawing.Size(610, 96);
            this.boxUnixReason.TabIndex = 70;
            this.boxUnixReason.Text = "";
            // 
            // btnUnixClear
            // 
            this.btnUnixClear.Location = new System.Drawing.Point(11, 134);
            this.btnUnixClear.Name = "btnUnixClear";
            this.btnUnixClear.Size = new System.Drawing.Size(75, 23);
            this.btnUnixClear.TabIndex = 62;
            this.btnUnixClear.Text = "Clear";
            this.btnUnixClear.UseVisualStyleBackColor = true;
            this.btnUnixClear.Click += new System.EventHandler(this.btnUnixClear_Click);
            // 
            // btnUnixSearch
            // 
            this.btnUnixSearch.Location = new System.Drawing.Point(11, 105);
            this.btnUnixSearch.Name = "btnUnixSearch";
            this.btnUnixSearch.Size = new System.Drawing.Size(75, 23);
            this.btnUnixSearch.TabIndex = 61;
            this.btnUnixSearch.Text = "Search";
            this.btnUnixSearch.UseVisualStyleBackColor = true;
            this.btnUnixSearch.Click += new System.EventHandler(this.btnUnixSearch_Click);
            // 
            // boxUnixReasonCategory
            // 
            this.boxUnixReasonCategory.FormattingEnabled = true;
            this.boxUnixReasonCategory.ItemHeight = 13;
            this.boxUnixReasonCategory.Location = new System.Drawing.Point(170, 303);
            this.boxUnixReasonCategory.Name = "boxUnixReasonCategory";
            this.boxUnixReasonCategory.Size = new System.Drawing.Size(451, 21);
            this.boxUnixReasonCategory.TabIndex = 69;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(8, 312);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(109, 13);
            this.label43.TabIndex = 68;
            this.label43.Text = "Maintenance Reason";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(298, 76);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(79, 13);
            this.label44.TabIndex = 64;
            this.label44.Text = "Search Results";
            // 
            // boxUnixRMS
            // 
            this.boxUnixRMS.FormattingEnabled = true;
            this.boxUnixRMS.Location = new System.Drawing.Point(170, 43);
            this.boxUnixRMS.Name = "boxUnixRMS";
            this.boxUnixRMS.Size = new System.Drawing.Size(451, 21);
            this.boxUnixRMS.TabIndex = 59;
            this.boxUnixRMS.SelectedIndexChanged += new System.EventHandler(this.boxUnixRMS_SelectedIndexChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(8, 52);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(129, 13);
            this.label45.TabIndex = 60;
            this.label45.Text = "Root Management Server";
            // 
            // boxUnixSearch
            // 
            this.boxUnixSearch.Location = new System.Drawing.Point(170, 15);
            this.boxUnixSearch.Name = "boxUnixSearch";
            this.boxUnixSearch.Size = new System.Drawing.Size(451, 20);
            this.boxUnixSearch.TabIndex = 58;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(8, 18);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(65, 13);
            this.label46.TabIndex = 57;
            this.label46.Text = "Unix Search";
            // 
            // pageGroup
            // 
            this.pageGroup.Controls.Add(this.btnGroupSearchHelp);
            this.pageGroup.Controls.Add(this.btnGroupUnselectAll);
            this.pageGroup.Controls.Add(this.btnGroupSelectAll);
            this.pageGroup.Controls.Add(this.boxGroupSearchResults);
            this.pageGroup.Controls.Add(this.label9);
            this.pageGroup.Controls.Add(this.boxGroupStartTime);
            this.pageGroup.Controls.Add(this.label10);
            this.pageGroup.Controls.Add(this.boxGroupEndTime);
            this.pageGroup.Controls.Add(this.btnGroupStopMM);
            this.pageGroup.Controls.Add(this.btnGroupCheckMM);
            this.pageGroup.Controls.Add(this.btnGroupStartMM);
            this.pageGroup.Controls.Add(this.label16);
            this.pageGroup.Controls.Add(this.boxGroupReason);
            this.pageGroup.Controls.Add(this.btnGroupClear);
            this.pageGroup.Controls.Add(this.btnGroupSearch);
            this.pageGroup.Controls.Add(this.boxGroupReasonCategory);
            this.pageGroup.Controls.Add(this.label17);
            this.pageGroup.Controls.Add(this.label18);
            this.pageGroup.Controls.Add(this.boxGroupRMS);
            this.pageGroup.Controls.Add(this.label47);
            this.pageGroup.Controls.Add(this.boxGroupSearch);
            this.pageGroup.Controls.Add(this.label48);
            this.pageGroup.Location = new System.Drawing.Point(4, 22);
            this.pageGroup.Name = "pageGroup";
            this.pageGroup.Size = new System.Drawing.Size(638, 508);
            this.pageGroup.TabIndex = 9;
            this.pageGroup.Text = "Group";
            this.pageGroup.UseVisualStyleBackColor = true;
            this.pageGroup.Enter += new System.EventHandler(this.pageGroup_Enter);
            // 
            // btnGroupSearchHelp
            // 
            this.btnGroupSearchHelp.Location = new System.Drawing.Point(87, 15);
            this.btnGroupSearchHelp.Name = "btnGroupSearchHelp";
            this.btnGroupSearchHelp.Size = new System.Drawing.Size(17, 20);
            this.btnGroupSearchHelp.TabIndex = 99;
            this.btnGroupSearchHelp.Text = "?";
            this.btnGroupSearchHelp.UseVisualStyleBackColor = true;
            this.btnGroupSearchHelp.Click += new System.EventHandler(this.btnSearchHelp_Click);
            // 
            // btnGroupUnselectAll
            // 
            this.btnGroupUnselectAll.Location = new System.Drawing.Point(11, 192);
            this.btnGroupUnselectAll.Name = "btnGroupUnselectAll";
            this.btnGroupUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnGroupUnselectAll.TabIndex = 86;
            this.btnGroupUnselectAll.Text = "Unselect All";
            this.btnGroupUnselectAll.UseVisualStyleBackColor = true;
            this.btnGroupUnselectAll.Click += new System.EventHandler(this.btnGroupUnselectAll_Click);
            // 
            // btnGroupSelectAll
            // 
            this.btnGroupSelectAll.Location = new System.Drawing.Point(11, 163);
            this.btnGroupSelectAll.Name = "btnGroupSelectAll";
            this.btnGroupSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnGroupSelectAll.TabIndex = 84;
            this.btnGroupSelectAll.Text = "Select All";
            this.btnGroupSelectAll.UseVisualStyleBackColor = true;
            this.btnGroupSelectAll.Click += new System.EventHandler(this.btnGroupSelectAll_Click);
            // 
            // boxGroupSearchResults
            // 
            this.boxGroupSearchResults.FormattingEnabled = true;
            this.boxGroupSearchResults.Location = new System.Drawing.Point(92, 92);
            this.boxGroupSearchResults.Name = "boxGroupSearchResults";
            this.boxGroupSearchResults.Size = new System.Drawing.Size(529, 139);
            this.boxGroupSearchResults.TabIndex = 87;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 13);
            this.label9.TabIndex = 97;
            this.label9.Text = "Maintenance Start Time";
            // 
            // boxGroupStartTime
            // 
            this.boxGroupStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxGroupStartTime.Enabled = false;
            this.boxGroupStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxGroupStartTime.Location = new System.Drawing.Point(170, 240);
            this.boxGroupStartTime.Name = "boxGroupStartTime";
            this.boxGroupStartTime.Size = new System.Drawing.Size(451, 20);
            this.boxGroupStartTime.TabIndex = 88;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 13);
            this.label10.TabIndex = 96;
            this.label10.Text = "Maintenance End Time";
            // 
            // boxGroupEndTime
            // 
            this.boxGroupEndTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxGroupEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxGroupEndTime.Location = new System.Drawing.Point(170, 266);
            this.boxGroupEndTime.Name = "boxGroupEndTime";
            this.boxGroupEndTime.Size = new System.Drawing.Size(451, 20);
            this.boxGroupEndTime.TabIndex = 89;
            // 
            // btnGroupStopMM
            // 
            this.btnGroupStopMM.Location = new System.Drawing.Point(500, 454);
            this.btnGroupStopMM.Name = "btnGroupStopMM";
            this.btnGroupStopMM.Size = new System.Drawing.Size(103, 40);
            this.btnGroupStopMM.TabIndex = 94;
            this.btnGroupStopMM.Text = "Stop Maintenance Mode";
            this.btnGroupStopMM.UseVisualStyleBackColor = true;
            this.btnGroupStopMM.Click += new System.EventHandler(this.btnGroupStopMM_Click);
            // 
            // btnGroupCheckMM
            // 
            this.btnGroupCheckMM.Location = new System.Drawing.Point(279, 454);
            this.btnGroupCheckMM.Name = "btnGroupCheckMM";
            this.btnGroupCheckMM.Size = new System.Drawing.Size(110, 40);
            this.btnGroupCheckMM.TabIndex = 93;
            this.btnGroupCheckMM.Text = "Check Maintenance Mode";
            this.btnGroupCheckMM.UseVisualStyleBackColor = true;
            this.btnGroupCheckMM.Click += new System.EventHandler(this.btnGroupCheckMM_Click);
            // 
            // btnGroupStartMM
            // 
            this.btnGroupStartMM.Location = new System.Drawing.Point(34, 454);
            this.btnGroupStartMM.Name = "btnGroupStartMM";
            this.btnGroupStartMM.Size = new System.Drawing.Size(103, 40);
            this.btnGroupStartMM.TabIndex = 92;
            this.btnGroupStartMM.Text = "Start Maintenance Mode";
            this.btnGroupStartMM.UseVisualStyleBackColor = true;
            this.btnGroupStartMM.Click += new System.EventHandler(this.btnGroupStartMM_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(315, 336);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 95;
            this.label16.Text = "Reason";
            // 
            // boxGroupReason
            // 
            this.boxGroupReason.Location = new System.Drawing.Point(11, 352);
            this.boxGroupReason.Name = "boxGroupReason";
            this.boxGroupReason.Size = new System.Drawing.Size(610, 96);
            this.boxGroupReason.TabIndex = 91;
            this.boxGroupReason.Text = "";
            // 
            // btnGroupClear
            // 
            this.btnGroupClear.Location = new System.Drawing.Point(11, 134);
            this.btnGroupClear.Name = "btnGroupClear";
            this.btnGroupClear.Size = new System.Drawing.Size(75, 23);
            this.btnGroupClear.TabIndex = 83;
            this.btnGroupClear.Text = "Clear";
            this.btnGroupClear.UseVisualStyleBackColor = true;
            this.btnGroupClear.Click += new System.EventHandler(this.btnGroupClear_Click);
            // 
            // btnGroupSearch
            // 
            this.btnGroupSearch.Location = new System.Drawing.Point(11, 105);
            this.btnGroupSearch.Name = "btnGroupSearch";
            this.btnGroupSearch.Size = new System.Drawing.Size(75, 23);
            this.btnGroupSearch.TabIndex = 82;
            this.btnGroupSearch.Text = "Search";
            this.btnGroupSearch.UseVisualStyleBackColor = true;
            this.btnGroupSearch.Click += new System.EventHandler(this.btnGroupSearch_Click);
            // 
            // boxGroupReasonCategory
            // 
            this.boxGroupReasonCategory.FormattingEnabled = true;
            this.boxGroupReasonCategory.ItemHeight = 13;
            this.boxGroupReasonCategory.Location = new System.Drawing.Point(170, 303);
            this.boxGroupReasonCategory.Name = "boxGroupReasonCategory";
            this.boxGroupReasonCategory.Size = new System.Drawing.Size(451, 21);
            this.boxGroupReasonCategory.TabIndex = 90;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 312);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(109, 13);
            this.label17.TabIndex = 89;
            this.label17.Text = "Maintenance Reason";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(298, 76);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 13);
            this.label18.TabIndex = 85;
            this.label18.Text = "Search Results";
            // 
            // boxGroupRMS
            // 
            this.boxGroupRMS.FormattingEnabled = true;
            this.boxGroupRMS.Location = new System.Drawing.Point(170, 43);
            this.boxGroupRMS.Name = "boxGroupRMS";
            this.boxGroupRMS.Size = new System.Drawing.Size(451, 21);
            this.boxGroupRMS.TabIndex = 80;
            this.boxGroupRMS.SelectedIndexChanged += new System.EventHandler(this.boxGroupRMS_SelectedIndexChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(8, 52);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(129, 13);
            this.label47.TabIndex = 81;
            this.label47.Text = "Root Management Server";
            // 
            // boxGroupSearch
            // 
            this.boxGroupSearch.Location = new System.Drawing.Point(170, 15);
            this.boxGroupSearch.Name = "boxGroupSearch";
            this.boxGroupSearch.Size = new System.Drawing.Size(451, 20);
            this.boxGroupSearch.TabIndex = 79;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(8, 18);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(73, 13);
            this.label48.TabIndex = 78;
            this.label48.Text = "Group Search";
            // 
            // pageGeneric
            // 
            this.pageGeneric.Controls.Add(this.button5);
            this.pageGeneric.Controls.Add(this.btnGenericLoadClasses);
            this.pageGeneric.Controls.Add(this.boxGenericClass);
            this.pageGeneric.Controls.Add(this.label22);
            this.pageGeneric.Controls.Add(this.label20);
            this.pageGeneric.Controls.Add(this.label25);
            this.pageGeneric.Controls.Add(this.btnGenericUnselectAll);
            this.pageGeneric.Controls.Add(this.btnGenericSelectAll);
            this.pageGeneric.Controls.Add(this.boxGenericSearchResults);
            this.pageGeneric.Controls.Add(this.label13);
            this.pageGeneric.Controls.Add(this.boxGenericStartTime);
            this.pageGeneric.Controls.Add(this.label19);
            this.pageGeneric.Controls.Add(this.boxGenericEndTime);
            this.pageGeneric.Controls.Add(this.btnGenericStopMM);
            this.pageGeneric.Controls.Add(this.btnGenericCheckMM);
            this.pageGeneric.Controls.Add(this.btnGenericStartMM);
            this.pageGeneric.Controls.Add(this.boxGenericReason);
            this.pageGeneric.Controls.Add(this.btnGenericClear);
            this.pageGeneric.Controls.Add(this.btnGenericSearch);
            this.pageGeneric.Controls.Add(this.boxGenericReasonCategory);
            this.pageGeneric.Controls.Add(this.label21);
            this.pageGeneric.Controls.Add(this.boxGenericRMS);
            this.pageGeneric.Controls.Add(this.label23);
            this.pageGeneric.Controls.Add(this.boxGenericSearch);
            this.pageGeneric.Controls.Add(this.label24);
            this.pageGeneric.Location = new System.Drawing.Point(4, 22);
            this.pageGeneric.Name = "pageGeneric";
            this.pageGeneric.Size = new System.Drawing.Size(638, 508);
            this.pageGeneric.TabIndex = 5;
            this.pageGeneric.Text = "Generic Search";
            this.pageGeneric.UseVisualStyleBackColor = true;
            this.pageGeneric.Enter += new System.EventHandler(this.pageGeneric_Enter);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(123, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(17, 20);
            this.button5.TabIndex = 76;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // btnGenericLoadClasses
            // 
            this.btnGenericLoadClasses.Location = new System.Drawing.Point(537, 24);
            this.btnGenericLoadClasses.Name = "btnGenericLoadClasses";
            this.btnGenericLoadClasses.Size = new System.Drawing.Size(84, 23);
            this.btnGenericLoadClasses.TabIndex = 36;
            this.btnGenericLoadClasses.Text = "Load Classes";
            this.btnGenericLoadClasses.UseVisualStyleBackColor = true;
            this.btnGenericLoadClasses.Click += new System.EventHandler(this.btnGenericLoadClasses_Click);
            // 
            // boxGenericClass
            // 
            this.boxGenericClass.FormattingEnabled = true;
            this.boxGenericClass.Location = new System.Drawing.Point(170, 26);
            this.boxGenericClass.Name = "boxGenericClass";
            this.boxGenericClass.Size = new System.Drawing.Size(366, 21);
            this.boxGenericClass.TabIndex = 37;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(315, 336);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 13);
            this.label22.TabIndex = 75;
            this.label22.Text = "Reason";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(298, 76);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 65;
            this.label20.Text = "Search Results";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 30);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(32, 13);
            this.label25.TabIndex = 57;
            this.label25.Text = "Class";
            // 
            // btnGenericUnselectAll
            // 
            this.btnGenericUnselectAll.Location = new System.Drawing.Point(11, 193);
            this.btnGenericUnselectAll.Name = "btnGenericUnselectAll";
            this.btnGenericUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnGenericUnselectAll.TabIndex = 44;
            this.btnGenericUnselectAll.Text = "Unselect All";
            this.btnGenericUnselectAll.UseVisualStyleBackColor = true;
            this.btnGenericUnselectAll.Click += new System.EventHandler(this.btnGenericUnselectAll_Click);
            // 
            // btnGenericSelectAll
            // 
            this.btnGenericSelectAll.Location = new System.Drawing.Point(11, 164);
            this.btnGenericSelectAll.Name = "btnGenericSelectAll";
            this.btnGenericSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnGenericSelectAll.TabIndex = 42;
            this.btnGenericSelectAll.Text = "Select All";
            this.btnGenericSelectAll.UseVisualStyleBackColor = true;
            this.btnGenericSelectAll.Click += new System.EventHandler(this.btnGenericSelectAll_Click);
            // 
            // boxGenericSearchResults
            // 
            this.boxGenericSearchResults.FormattingEnabled = true;
            this.boxGenericSearchResults.Location = new System.Drawing.Point(92, 93);
            this.boxGenericSearchResults.Name = "boxGenericSearchResults";
            this.boxGenericSearchResults.Size = new System.Drawing.Size(529, 139);
            this.boxGenericSearchResults.TabIndex = 45;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 247);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 13);
            this.label13.TabIndex = 55;
            this.label13.Text = "Maintenance Start Time";
            // 
            // boxGenericStartTime
            // 
            this.boxGenericStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxGenericStartTime.Enabled = false;
            this.boxGenericStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxGenericStartTime.Location = new System.Drawing.Point(170, 241);
            this.boxGenericStartTime.Name = "boxGenericStartTime";
            this.boxGenericStartTime.Size = new System.Drawing.Size(451, 20);
            this.boxGenericStartTime.TabIndex = 46;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 273);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(117, 13);
            this.label19.TabIndex = 54;
            this.label19.Text = "Maintenance End Time";
            // 
            // boxGenericEndTime
            // 
            this.boxGenericEndTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxGenericEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxGenericEndTime.Location = new System.Drawing.Point(170, 267);
            this.boxGenericEndTime.Name = "boxGenericEndTime";
            this.boxGenericEndTime.Size = new System.Drawing.Size(451, 20);
            this.boxGenericEndTime.TabIndex = 47;
            // 
            // btnGenericStopMM
            // 
            this.btnGenericStopMM.Location = new System.Drawing.Point(500, 454);
            this.btnGenericStopMM.Name = "btnGenericStopMM";
            this.btnGenericStopMM.Size = new System.Drawing.Size(103, 40);
            this.btnGenericStopMM.TabIndex = 52;
            this.btnGenericStopMM.Text = "Stop Maintenance Mode";
            this.btnGenericStopMM.UseVisualStyleBackColor = true;
            this.btnGenericStopMM.Click += new System.EventHandler(this.btnGenericStopMM_Click);
            // 
            // btnGenericCheckMM
            // 
            this.btnGenericCheckMM.Location = new System.Drawing.Point(279, 454);
            this.btnGenericCheckMM.Name = "btnGenericCheckMM";
            this.btnGenericCheckMM.Size = new System.Drawing.Size(110, 40);
            this.btnGenericCheckMM.TabIndex = 51;
            this.btnGenericCheckMM.Text = "Check Maintenance Mode";
            this.btnGenericCheckMM.UseVisualStyleBackColor = true;
            this.btnGenericCheckMM.Click += new System.EventHandler(this.btnGenericCheckMM_Click);
            // 
            // btnGenericStartMM
            // 
            this.btnGenericStartMM.Location = new System.Drawing.Point(34, 454);
            this.btnGenericStartMM.Name = "btnGenericStartMM";
            this.btnGenericStartMM.Size = new System.Drawing.Size(103, 40);
            this.btnGenericStartMM.TabIndex = 50;
            this.btnGenericStartMM.Text = "Start Maintenance Mode";
            this.btnGenericStartMM.UseVisualStyleBackColor = true;
            this.btnGenericStartMM.Click += new System.EventHandler(this.btnGenericStartMM_Click);
            // 
            // boxGenericReason
            // 
            this.boxGenericReason.Location = new System.Drawing.Point(11, 353);
            this.boxGenericReason.Name = "boxGenericReason";
            this.boxGenericReason.Size = new System.Drawing.Size(610, 96);
            this.boxGenericReason.TabIndex = 49;
            this.boxGenericReason.Text = "";
            // 
            // btnGenericClear
            // 
            this.btnGenericClear.Location = new System.Drawing.Point(11, 135);
            this.btnGenericClear.Name = "btnGenericClear";
            this.btnGenericClear.Size = new System.Drawing.Size(75, 23);
            this.btnGenericClear.TabIndex = 41;
            this.btnGenericClear.Text = "Clear";
            this.btnGenericClear.UseVisualStyleBackColor = true;
            this.btnGenericClear.Click += new System.EventHandler(this.btnGenericClear_Click);
            // 
            // btnGenericSearch
            // 
            this.btnGenericSearch.Location = new System.Drawing.Point(11, 106);
            this.btnGenericSearch.Name = "btnGenericSearch";
            this.btnGenericSearch.Size = new System.Drawing.Size(75, 23);
            this.btnGenericSearch.TabIndex = 40;
            this.btnGenericSearch.Text = "Search";
            this.btnGenericSearch.UseVisualStyleBackColor = true;
            this.btnGenericSearch.Click += new System.EventHandler(this.btnGenericSearch_Click);
            // 
            // boxGenericReasonCategory
            // 
            this.boxGenericReasonCategory.FormattingEnabled = true;
            this.boxGenericReasonCategory.ItemHeight = 13;
            this.boxGenericReasonCategory.Location = new System.Drawing.Point(170, 304);
            this.boxGenericReasonCategory.Name = "boxGenericReasonCategory";
            this.boxGenericReasonCategory.Size = new System.Drawing.Size(451, 21);
            this.boxGenericReasonCategory.TabIndex = 48;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 313);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(109, 13);
            this.label21.TabIndex = 47;
            this.label21.Text = "Maintenance Reason";
            // 
            // boxGenericRMS
            // 
            this.boxGenericRMS.FormattingEnabled = true;
            this.boxGenericRMS.Location = new System.Drawing.Point(170, 53);
            this.boxGenericRMS.Name = "boxGenericRMS";
            this.boxGenericRMS.Size = new System.Drawing.Size(451, 21);
            this.boxGenericRMS.TabIndex = 38;
            this.boxGenericRMS.SelectedIndexChanged += new System.EventHandler(this.boxGenericRMS_SelectedIndexChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 62);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(129, 13);
            this.label23.TabIndex = 39;
            this.label23.Text = "Root Management Server";
            // 
            // boxGenericSearch
            // 
            this.boxGenericSearch.Location = new System.Drawing.Point(170, 3);
            this.boxGenericSearch.Name = "boxGenericSearch";
            this.boxGenericSearch.Size = new System.Drawing.Size(451, 20);
            this.boxGenericSearch.TabIndex = 35;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(109, 13);
            this.label24.TabIndex = 36;
            this.label24.Text = "Display Name Search";
            // 
            // pageSchedule
            // 
            this.pageSchedule.Controls.Add(this.boxScheduleTaskName);
            this.pageSchedule.Controls.Add(this.label62);
            this.pageSchedule.Controls.Add(this.boxSchedulePassword);
            this.pageSchedule.Controls.Add(this.label61);
            this.pageSchedule.Controls.Add(this.boxScheduleRemoteComputer);
            this.pageSchedule.Controls.Add(this.boxScheduleSearchResults);
            this.pageSchedule.Controls.Add(this.boxScheduleReasonCategory);
            this.pageSchedule.Controls.Add(this.label60);
            this.pageSchedule.Controls.Add(this.label59);
            this.pageSchedule.Controls.Add(this.boxScheduleComments);
            this.pageSchedule.Controls.Add(this.boxScheduleUserName);
            this.pageSchedule.Controls.Add(this.label58);
            this.pageSchedule.Controls.Add(this.label57);
            this.pageSchedule.Controls.Add(this.panelDays);
            this.pageSchedule.Controls.Add(this.label54);
            this.pageSchedule.Controls.Add(this.label52);
            this.pageSchedule.Controls.Add(this.boxScheduleStartTime);
            this.pageSchedule.Controls.Add(this.label50);
            this.pageSchedule.Controls.Add(this.radioScheduleWeekly);
            this.pageSchedule.Controls.Add(this.radioScheduleDaily);
            this.pageSchedule.Controls.Add(this.radioScheduleOnce);
            this.pageSchedule.Controls.Add(this.boxScheduleRunMinutes);
            this.pageSchedule.Controls.Add(this.label49);
            this.pageSchedule.Controls.Add(this.btnScheduleUncheckAll);
            this.pageSchedule.Controls.Add(this.btnScheduleCheckAll);
            this.pageSchedule.Controls.Add(this.BtnScheduleClear);
            this.pageSchedule.Controls.Add(this.btnScheduleSearch);
            this.pageSchedule.Controls.Add(this.btnScheduleCommonClass);
            this.pageSchedule.Controls.Add(this.btnScheduleSearchHelp);
            this.pageSchedule.Controls.Add(this.btnScheduleAllClasses);
            this.pageSchedule.Controls.Add(this.boxScheduleClass);
            this.pageSchedule.Controls.Add(this.label51);
            this.pageSchedule.Controls.Add(this.btnScheduleGo);
            this.pageSchedule.Controls.Add(this.boxScheduleRMS);
            this.pageSchedule.Controls.Add(this.label55);
            this.pageSchedule.Controls.Add(this.boxScheduleSearch);
            this.pageSchedule.Controls.Add(this.label56);
            this.pageSchedule.Location = new System.Drawing.Point(4, 22);
            this.pageSchedule.Name = "pageSchedule";
            this.pageSchedule.Size = new System.Drawing.Size(638, 508);
            this.pageSchedule.TabIndex = 10;
            this.pageSchedule.Text = "Schedule";
            this.pageSchedule.UseVisualStyleBackColor = true;
            this.pageSchedule.Enter += new System.EventHandler(this.pageSchedule_Enter);
            // 
            // boxSchedulePassword
            // 
            this.boxSchedulePassword.Location = new System.Drawing.Point(139, 429);
            this.boxSchedulePassword.Name = "boxSchedulePassword";
            this.boxSchedulePassword.PasswordChar = '*';
            this.boxSchedulePassword.Size = new System.Drawing.Size(189, 20);
            this.boxSchedulePassword.TabIndex = 137;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(13, 383);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(108, 13);
            this.label61.TabIndex = 136;
            this.label61.Text = "Scheduling Computer";
            // 
            // boxScheduleRemoteComputer
            // 
            this.boxScheduleRemoteComputer.Location = new System.Drawing.Point(139, 380);
            this.boxScheduleRemoteComputer.Name = "boxScheduleRemoteComputer";
            this.boxScheduleRemoteComputer.Size = new System.Drawing.Size(189, 20);
            this.boxScheduleRemoteComputer.TabIndex = 135;
            // 
            // boxScheduleSearchResults
            // 
            this.boxScheduleSearchResults.FormattingEnabled = true;
            this.boxScheduleSearchResults.Location = new System.Drawing.Point(128, 101);
            this.boxScheduleSearchResults.Name = "boxScheduleSearchResults";
            this.boxScheduleSearchResults.Size = new System.Drawing.Size(497, 109);
            this.boxScheduleSearchResults.TabIndex = 134;
            // 
            // boxScheduleReasonCategory
            // 
            this.boxScheduleReasonCategory.FormattingEnabled = true;
            this.boxScheduleReasonCategory.ItemHeight = 13;
            this.boxScheduleReasonCategory.Location = new System.Drawing.Point(139, 353);
            this.boxScheduleReasonCategory.Name = "boxScheduleReasonCategory";
            this.boxScheduleReasonCategory.Size = new System.Drawing.Size(189, 21);
            this.boxScheduleReasonCategory.TabIndex = 133;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(13, 355);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(109, 13);
            this.label60.TabIndex = 132;
            this.label60.Text = "Maintenance Reason";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(470, 274);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(56, 13);
            this.label59.TabIndex = 131;
            this.label59.Text = "Comments";
            // 
            // boxScheduleComments
            // 
            this.boxScheduleComments.Location = new System.Drawing.Point(357, 304);
            this.boxScheduleComments.Name = "boxScheduleComments";
            this.boxScheduleComments.Size = new System.Drawing.Size(268, 184);
            this.boxScheduleComments.TabIndex = 130;
            this.boxScheduleComments.Text = "";
            // 
            // boxScheduleUserName
            // 
            this.boxScheduleUserName.Location = new System.Drawing.Point(139, 406);
            this.boxScheduleUserName.Name = "boxScheduleUserName";
            this.boxScheduleUserName.Size = new System.Drawing.Size(189, 20);
            this.boxScheduleUserName.TabIndex = 129;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(13, 432);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(53, 13);
            this.label58.TabIndex = 127;
            this.label58.Text = "Password";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(13, 406);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(57, 13);
            this.label57.TabIndex = 126;
            this.label57.Text = "UserName";
            // 
            // panelDays
            // 
            this.panelDays.Controls.Add(this.boxScheduleSunday);
            this.panelDays.Controls.Add(this.boxScheduleSaturday);
            this.panelDays.Controls.Add(this.boxScheduleFriday);
            this.panelDays.Controls.Add(this.label53);
            this.panelDays.Controls.Add(this.boxScheduleThursday);
            this.panelDays.Controls.Add(this.boxScheduleWednessday);
            this.panelDays.Controls.Add(this.boxScheduleTuesday);
            this.panelDays.Controls.Add(this.boxScheduleMonday);
            this.panelDays.Location = new System.Drawing.Point(8, 216);
            this.panelDays.Name = "panelDays";
            this.panelDays.Size = new System.Drawing.Size(627, 31);
            this.panelDays.TabIndex = 125;
            // 
            // boxScheduleSunday
            // 
            this.boxScheduleSunday.AutoSize = true;
            this.boxScheduleSunday.Location = new System.Drawing.Point(555, 5);
            this.boxScheduleSunday.Name = "boxScheduleSunday";
            this.boxScheduleSunday.Size = new System.Drawing.Size(62, 17);
            this.boxScheduleSunday.TabIndex = 124;
            this.boxScheduleSunday.Text = "Sunday";
            this.boxScheduleSunday.UseVisualStyleBackColor = true;
            // 
            // boxScheduleSaturday
            // 
            this.boxScheduleSaturday.AutoSize = true;
            this.boxScheduleSaturday.Location = new System.Drawing.Point(481, 5);
            this.boxScheduleSaturday.Name = "boxScheduleSaturday";
            this.boxScheduleSaturday.Size = new System.Drawing.Size(68, 17);
            this.boxScheduleSaturday.TabIndex = 115;
            this.boxScheduleSaturday.Text = "Saturday";
            this.boxScheduleSaturday.UseVisualStyleBackColor = true;
            // 
            // boxScheduleFriday
            // 
            this.boxScheduleFriday.AutoSize = true;
            this.boxScheduleFriday.Location = new System.Drawing.Point(422, 5);
            this.boxScheduleFriday.Name = "boxScheduleFriday";
            this.boxScheduleFriday.Size = new System.Drawing.Size(54, 17);
            this.boxScheduleFriday.TabIndex = 114;
            this.boxScheduleFriday.Text = "Friday";
            this.boxScheduleFriday.UseVisualStyleBackColor = true;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(5, 6);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(66, 13);
            this.label53.TabIndex = 123;
            this.label53.Text = "Days to Run";
            // 
            // boxScheduleThursday
            // 
            this.boxScheduleThursday.AutoSize = true;
            this.boxScheduleThursday.Location = new System.Drawing.Point(349, 6);
            this.boxScheduleThursday.Name = "boxScheduleThursday";
            this.boxScheduleThursday.Size = new System.Drawing.Size(67, 17);
            this.boxScheduleThursday.TabIndex = 113;
            this.boxScheduleThursday.Text = "Thusday";
            this.boxScheduleThursday.UseVisualStyleBackColor = true;
            // 
            // boxScheduleWednessday
            // 
            this.boxScheduleWednessday.AutoSize = true;
            this.boxScheduleWednessday.Location = new System.Drawing.Point(255, 5);
            this.boxScheduleWednessday.Name = "boxScheduleWednessday";
            this.boxScheduleWednessday.Size = new System.Drawing.Size(88, 17);
            this.boxScheduleWednessday.TabIndex = 112;
            this.boxScheduleWednessday.Text = "Wednessday";
            this.boxScheduleWednessday.UseVisualStyleBackColor = true;
            // 
            // boxScheduleTuesday
            // 
            this.boxScheduleTuesday.AutoSize = true;
            this.boxScheduleTuesday.Location = new System.Drawing.Point(190, 5);
            this.boxScheduleTuesday.Name = "boxScheduleTuesday";
            this.boxScheduleTuesday.Size = new System.Drawing.Size(67, 17);
            this.boxScheduleTuesday.TabIndex = 111;
            this.boxScheduleTuesday.Text = "Tuesday";
            this.boxScheduleTuesday.UseVisualStyleBackColor = true;
            // 
            // boxScheduleMonday
            // 
            this.boxScheduleMonday.AutoSize = true;
            this.boxScheduleMonday.Location = new System.Drawing.Point(120, 6);
            this.boxScheduleMonday.Name = "boxScheduleMonday";
            this.boxScheduleMonday.Size = new System.Drawing.Size(64, 17);
            this.boxScheduleMonday.TabIndex = 110;
            this.boxScheduleMonday.Text = "Monday";
            this.boxScheduleMonday.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(13, 250);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(52, 13);
            this.label54.TabIndex = 124;
            this.label54.Text = "Schedule";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(293, 83);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(79, 13);
            this.label52.TabIndex = 122;
            this.label52.Text = "Search Results";
            // 
            // boxScheduleStartTime
            // 
            this.boxScheduleStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt";
            this.boxScheduleStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.boxScheduleStartTime.Location = new System.Drawing.Point(139, 301);
            this.boxScheduleStartTime.Name = "boxScheduleStartTime";
            this.boxScheduleStartTime.Size = new System.Drawing.Size(189, 20);
            this.boxScheduleStartTime.TabIndex = 121;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(13, 304);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(55, 13);
            this.label50.TabIndex = 120;
            this.label50.Text = "Start Time";
            // 
            // radioScheduleWeekly
            // 
            this.radioScheduleWeekly.AutoSize = true;
            this.radioScheduleWeekly.Location = new System.Drawing.Point(263, 248);
            this.radioScheduleWeekly.Name = "radioScheduleWeekly";
            this.radioScheduleWeekly.Size = new System.Drawing.Size(61, 17);
            this.radioScheduleWeekly.TabIndex = 119;
            this.radioScheduleWeekly.TabStop = true;
            this.radioScheduleWeekly.Text = "Weekly";
            this.radioScheduleWeekly.UseVisualStyleBackColor = true;
            this.radioScheduleWeekly.CheckedChanged += new System.EventHandler(this.radioScheduleWeekly_CheckedChanged);
            // 
            // radioScheduleDaily
            // 
            this.radioScheduleDaily.AutoSize = true;
            this.radioScheduleDaily.Location = new System.Drawing.Point(198, 248);
            this.radioScheduleDaily.Name = "radioScheduleDaily";
            this.radioScheduleDaily.Size = new System.Drawing.Size(48, 17);
            this.radioScheduleDaily.TabIndex = 118;
            this.radioScheduleDaily.TabStop = true;
            this.radioScheduleDaily.Text = "Daily";
            this.radioScheduleDaily.UseVisualStyleBackColor = true;
            this.radioScheduleDaily.CheckedChanged += new System.EventHandler(this.radioScheduleDaily_CheckedChanged);
            // 
            // radioScheduleOnce
            // 
            this.radioScheduleOnce.AutoSize = true;
            this.radioScheduleOnce.Location = new System.Drawing.Point(128, 248);
            this.radioScheduleOnce.Name = "radioScheduleOnce";
            this.radioScheduleOnce.Size = new System.Drawing.Size(51, 17);
            this.radioScheduleOnce.TabIndex = 117;
            this.radioScheduleOnce.TabStop = true;
            this.radioScheduleOnce.Text = "Once";
            this.radioScheduleOnce.UseVisualStyleBackColor = true;
            this.radioScheduleOnce.CheckedChanged += new System.EventHandler(this.radioScheduleOnce_CheckedChanged);
            // 
            // boxScheduleRunMinutes
            // 
            this.boxScheduleRunMinutes.Location = new System.Drawing.Point(139, 327);
            this.boxScheduleRunMinutes.Name = "boxScheduleRunMinutes";
            this.boxScheduleRunMinutes.Size = new System.Drawing.Size(189, 20);
            this.boxScheduleRunMinutes.TabIndex = 109;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(13, 330);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(96, 13);
            this.label49.TabIndex = 108;
            this.label49.Text = "Number of Minutes";
            // 
            // btnScheduleUncheckAll
            // 
            this.btnScheduleUncheckAll.Location = new System.Drawing.Point(26, 187);
            this.btnScheduleUncheckAll.Name = "btnScheduleUncheckAll";
            this.btnScheduleUncheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnScheduleUncheckAll.TabIndex = 107;
            this.btnScheduleUncheckAll.Text = "Unselect All";
            this.btnScheduleUncheckAll.UseVisualStyleBackColor = true;
            this.btnScheduleUncheckAll.Click += new System.EventHandler(this.btnScheduleUncheckAll_Click);
            // 
            // btnScheduleCheckAll
            // 
            this.btnScheduleCheckAll.Location = new System.Drawing.Point(26, 158);
            this.btnScheduleCheckAll.Name = "btnScheduleCheckAll";
            this.btnScheduleCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnScheduleCheckAll.TabIndex = 106;
            this.btnScheduleCheckAll.Text = "Select All";
            this.btnScheduleCheckAll.UseVisualStyleBackColor = true;
            this.btnScheduleCheckAll.Click += new System.EventHandler(this.btnScheduleCheckAll_Click);
            // 
            // BtnScheduleClear
            // 
            this.BtnScheduleClear.Location = new System.Drawing.Point(26, 129);
            this.BtnScheduleClear.Name = "BtnScheduleClear";
            this.BtnScheduleClear.Size = new System.Drawing.Size(75, 23);
            this.BtnScheduleClear.TabIndex = 105;
            this.BtnScheduleClear.Text = "Clear";
            this.BtnScheduleClear.UseVisualStyleBackColor = true;
            this.BtnScheduleClear.Click += new System.EventHandler(this.BtnScheduleClear_Click);
            // 
            // btnScheduleSearch
            // 
            this.btnScheduleSearch.Location = new System.Drawing.Point(26, 100);
            this.btnScheduleSearch.Name = "btnScheduleSearch";
            this.btnScheduleSearch.Size = new System.Drawing.Size(75, 23);
            this.btnScheduleSearch.TabIndex = 104;
            this.btnScheduleSearch.Text = "Search";
            this.btnScheduleSearch.UseVisualStyleBackColor = true;
            this.btnScheduleSearch.Click += new System.EventHandler(this.btnScheduleSearch_Click);
            // 
            // btnScheduleCommonClass
            // 
            this.btnScheduleCommonClass.Location = new System.Drawing.Point(456, 32);
            this.btnScheduleCommonClass.Name = "btnScheduleCommonClass";
            this.btnScheduleCommonClass.Size = new System.Drawing.Size(95, 23);
            this.btnScheduleCommonClass.TabIndex = 102;
            this.btnScheduleCommonClass.Text = "Common Classes";
            this.btnScheduleCommonClass.UseVisualStyleBackColor = true;
            this.btnScheduleCommonClass.Click += new System.EventHandler(this.btnScheduleCommonClass_Click);
            // 
            // btnScheduleSearchHelp
            // 
            this.btnScheduleSearchHelp.Location = new System.Drawing.Point(128, 9);
            this.btnScheduleSearchHelp.Name = "btnScheduleSearchHelp";
            this.btnScheduleSearchHelp.Size = new System.Drawing.Size(17, 20);
            this.btnScheduleSearchHelp.TabIndex = 101;
            this.btnScheduleSearchHelp.Text = "?";
            this.btnScheduleSearchHelp.UseVisualStyleBackColor = true;
            this.btnScheduleSearchHelp.Click += new System.EventHandler(this.btnSearchHelp_Click);
            // 
            // btnScheduleAllClasses
            // 
            this.btnScheduleAllClasses.Location = new System.Drawing.Point(557, 32);
            this.btnScheduleAllClasses.Name = "btnScheduleAllClasses";
            this.btnScheduleAllClasses.Size = new System.Drawing.Size(69, 23);
            this.btnScheduleAllClasses.TabIndex = 79;
            this.btnScheduleAllClasses.Text = "All Classes";
            this.btnScheduleAllClasses.UseVisualStyleBackColor = true;
            this.btnScheduleAllClasses.Click += new System.EventHandler(this.btnScheduleAllClasses_Click);
            // 
            // boxScheduleClass
            // 
            this.boxScheduleClass.FormattingEnabled = true;
            this.boxScheduleClass.Location = new System.Drawing.Point(175, 32);
            this.boxScheduleClass.Name = "boxScheduleClass";
            this.boxScheduleClass.Size = new System.Drawing.Size(267, 21);
            this.boxScheduleClass.TabIndex = 80;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(13, 36);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(32, 13);
            this.label51.TabIndex = 98;
            this.label51.Text = "Class";
            // 
            // btnScheduleGo
            // 
            this.btnScheduleGo.Location = new System.Drawing.Point(75, 458);
            this.btnScheduleGo.Name = "btnScheduleGo";
            this.btnScheduleGo.Size = new System.Drawing.Size(171, 30);
            this.btnScheduleGo.TabIndex = 94;
            this.btnScheduleGo.Text = "Schedule Maintenance Mode";
            this.btnScheduleGo.UseVisualStyleBackColor = true;
            this.btnScheduleGo.Click += new System.EventHandler(this.btnScheduleGo_Click);
            // 
            // boxScheduleRMS
            // 
            this.boxScheduleRMS.FormattingEnabled = true;
            this.boxScheduleRMS.Location = new System.Drawing.Point(175, 59);
            this.boxScheduleRMS.Name = "boxScheduleRMS";
            this.boxScheduleRMS.Size = new System.Drawing.Size(451, 21);
            this.boxScheduleRMS.TabIndex = 81;
            this.boxScheduleRMS.SelectedIndexChanged += new System.EventHandler(this.boxScheduleRMS_SelectedIndexChanged);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(13, 62);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(129, 13);
            this.label55.TabIndex = 82;
            this.label55.Text = "Root Management Server";
            // 
            // boxScheduleSearch
            // 
            this.boxScheduleSearch.Location = new System.Drawing.Point(175, 9);
            this.boxScheduleSearch.Name = "boxScheduleSearch";
            this.boxScheduleSearch.Size = new System.Drawing.Size(451, 20);
            this.boxScheduleSearch.TabIndex = 77;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(13, 12);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(109, 13);
            this.label56.TabIndex = 78;
            this.label56.Text = "Display Name Search";
            // 
            // pageResults
            // 
            this.pageResults.Controls.Add(this.btnResultsReturn);
            this.pageResults.Controls.Add(this.boxResults);
            this.pageResults.Location = new System.Drawing.Point(4, 22);
            this.pageResults.Name = "pageResults";
            this.pageResults.Size = new System.Drawing.Size(638, 508);
            this.pageResults.TabIndex = 4;
            this.pageResults.Text = "Results";
            this.pageResults.UseVisualStyleBackColor = true;
            // 
            // btnResultsReturn
            // 
            this.btnResultsReturn.Location = new System.Drawing.Point(279, 471);
            this.btnResultsReturn.Name = "btnResultsReturn";
            this.btnResultsReturn.Size = new System.Drawing.Size(75, 23);
            this.btnResultsReturn.TabIndex = 1;
            this.btnResultsReturn.Text = "Return";
            this.btnResultsReturn.UseVisualStyleBackColor = true;
            this.btnResultsReturn.Click += new System.EventHandler(this.btnResultsReturn_Click);
            // 
            // boxResults
            // 
            this.boxResults.Location = new System.Drawing.Point(8, 3);
            this.boxResults.Name = "boxResults";
            this.boxResults.Size = new System.Drawing.Size(613, 452);
            this.boxResults.TabIndex = 0;
            this.boxResults.Text = "";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Location = new System.Drawing.Point(170, 304);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(267, 21);
            this.comboBox1.TabIndex = 48;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(11, 353);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(426, 96);
            this.richTextBox1.TabIndex = 49;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(646, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(13, 278);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(62, 13);
            this.label62.TabIndex = 138;
            this.label62.Text = "Task Name";
            // 
            // boxScheduleTaskName
            // 
            this.boxScheduleTaskName.Location = new System.Drawing.Point(139, 275);
            this.boxScheduleTaskName.Name = "boxScheduleTaskName";
            this.boxScheduleTaskName.Size = new System.Drawing.Size(189, 20);
            this.boxScheduleTaskName.TabIndex = 139;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 584);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SCOM Maintenance Mode Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.pageStandard.ResumeLayout(false);
            this.pageStandard.PerformLayout();
            this.pageWindowsComputerSearch.ResumeLayout(false);
            this.pageWindowsComputerSearch.PerformLayout();
            this.pageWebApp.ResumeLayout(false);
            this.pageWebApp.PerformLayout();
            this.pageNetworkDevice.ResumeLayout(false);
            this.pageNetworkDevice.PerformLayout();
            this.pageUnixComputer.ResumeLayout(false);
            this.pageUnixComputer.PerformLayout();
            this.pageGroup.ResumeLayout(false);
            this.pageGroup.PerformLayout();
            this.pageGeneric.ResumeLayout(false);
            this.pageGeneric.PerformLayout();
            this.pageSchedule.ResumeLayout(false);
            this.pageSchedule.PerformLayout();
            this.panelDays.ResumeLayout(false);
            this.panelDays.PerformLayout();
            this.pageResults.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar statusBar;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage pageStandard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox boxMaintenanceType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker boxMaintenanceStartTime;
        private System.Windows.Forms.Button btnStopMaintenance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox boxComments;
        private System.Windows.Forms.ComboBox boxManagementGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCheckMaintenance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker boxDateTime;
        private System.Windows.Forms.Button btnMaintenanceMode;
        private System.Windows.Forms.Label lblComputerName;
        private System.Windows.Forms.TextBox boxComputerName;
        private System.Windows.Forms.TabPage pageWindowsComputerSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox boxWindowsComputerSearchComputerSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnWindowsComputerSearchSearchResultsClear;
        private System.Windows.Forms.Button btnWindowsComputerSearchSearchResultsSearch;
        private System.Windows.Forms.ComboBox boxWindowsComputerSearchReasonCategory;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox boxWindowsComputerSearchRMS;
        private System.Windows.Forms.Button btnWindowsComputerSearchStopMM;
        private System.Windows.Forms.Button btnWindowsComputerSearchCheckMM;
        private System.Windows.Forms.Button btnWindowsComputerSearchStartMM;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox boxWindowsComputerSearchReason;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker boxWindowsComputerSearchStartTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker boxWindowsComputerSearchEndTime;
        private System.Windows.Forms.CheckedListBox boxWindowsComputerSearchSearchResults;
        private System.Windows.Forms.Button btnWindowsComputerSearchSearchResultsUnselectAll;
        private System.Windows.Forms.Button btnWindowsComputerSearchSearchResultsSelectAll;
        private System.Windows.Forms.TabPage pageResults;
        private System.Windows.Forms.RichTextBox boxResults;
        private System.Windows.Forms.Button btnResultsReturn;
        private System.Windows.Forms.TabPage pageGeneric;
        private System.Windows.Forms.Button btnGenericUnselectAll;
        private System.Windows.Forms.Button btnGenericSelectAll;
        private System.Windows.Forms.CheckedListBox boxGenericSearchResults;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker boxGenericStartTime;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker boxGenericEndTime;
        private System.Windows.Forms.Button btnGenericStopMM;
        private System.Windows.Forms.Button btnGenericCheckMM;
        private System.Windows.Forms.Button btnGenericStartMM;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnGenericClear;
        private System.Windows.Forms.Button btnGenericSearch;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox boxGenericSearch;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.RichTextBox boxGenericReason;
        private System.Windows.Forms.ComboBox boxGenericReasonCategory;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TabPage pageWebApp;
        private System.Windows.Forms.TabPage pageNetworkDevice;
        private System.Windows.Forms.TabPage pageUnixComputer;
        private System.Windows.Forms.Button btnNetworkDeviceUnselectAll;
        private System.Windows.Forms.Button btnNetworkDeviceSelectAll;
        private System.Windows.Forms.CheckedListBox boxNetworkDeviceSearchResults;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DateTimePicker boxNetworkDeviceStartTime;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.DateTimePicker boxNetworkDeviceEndTime;
        private System.Windows.Forms.Button btnNetworkDeviceStopMM;
        private System.Windows.Forms.Button btnNetworkDeviceCheckMM;
        private System.Windows.Forms.Button btnNetworkDeviceStartMM;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.RichTextBox boxNetworkDeviceReason;
        private System.Windows.Forms.Button btnNetworkDeviceClear;
        private System.Windows.Forms.Button btnNetworkDeviceSearch;
        private System.Windows.Forms.ComboBox boxNetworkDeviceReasonCategory;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox boxNetworkDeviceRMS;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox boxNetworkDeviceSearch;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnWebAppUnselectAll;
        private System.Windows.Forms.Button btnWebAppSelectAll;
        private System.Windows.Forms.CheckedListBox boxWebAppSearchResults;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker boxWebAppStartTime;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.DateTimePicker boxWebAppEndTime;
        private System.Windows.Forms.Button btnWebAppStopMM;
        private System.Windows.Forms.Button btnWebAppCheckMM;
        private System.Windows.Forms.Button btnWebAppStartMM;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.RichTextBox boxWebAppReason;
        private System.Windows.Forms.Button btnWebAppClear;
        private System.Windows.Forms.Button btnWebAppSearch;
        private System.Windows.Forms.ComboBox boxWebAppReasonCategory;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox boxWebAppRMS;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox boxWebAppSearch;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Button btnUnixUnselectAll;
        private System.Windows.Forms.Button btnUnixSelectAll;
        private System.Windows.Forms.CheckedListBox boxUnixSearchResults;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker boxUnixStartTime;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.DateTimePicker boxUnixEndTime;
        private System.Windows.Forms.Button btnUnixStopMM;
        private System.Windows.Forms.Button btnUnixCheckMM;
        private System.Windows.Forms.Button btnUnixStartMM;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.RichTextBox boxUnixReason;
        private System.Windows.Forms.Button btnUnixClear;
        private System.Windows.Forms.Button btnUnixSearch;
        private System.Windows.Forms.ComboBox boxUnixReasonCategory;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.ComboBox boxUnixRMS;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox boxUnixSearch;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TabPage pageGroup;
        private System.Windows.Forms.Button btnGroupUnselectAll;
        private System.Windows.Forms.Button btnGroupSelectAll;
        private System.Windows.Forms.CheckedListBox boxGroupSearchResults;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker boxGroupStartTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker boxGroupEndTime;
        private System.Windows.Forms.Button btnGroupStopMM;
        private System.Windows.Forms.Button btnGroupCheckMM;
        private System.Windows.Forms.Button btnGroupStartMM;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RichTextBox boxGroupReason;
        private System.Windows.Forms.Button btnGroupClear;
        private System.Windows.Forms.Button btnGroupSearch;
        private System.Windows.Forms.ComboBox boxGroupReasonCategory;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox boxGroupRMS;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox boxGroupSearch;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox boxGenericRMS;
        private System.Windows.Forms.ComboBox boxGenericClass;
        private System.Windows.Forms.Button btnGenericLoadClasses;
        private System.Windows.Forms.Button btnWindowsComputerSearchHelp;
        private System.Windows.Forms.Button btnWebAppSearchHelp;
        private System.Windows.Forms.Button btnNetworkDeviceSearchHelp;
        private System.Windows.Forms.Button btnUnixSearchHelp;
        private System.Windows.Forms.Button btnGroupSearchHelp;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnStandardNameHelp;
        private System.Windows.Forms.TabPage pageSchedule;
        private System.Windows.Forms.Button btnScheduleCommonClass;
        private System.Windows.Forms.Button btnScheduleSearchHelp;
        private System.Windows.Forms.Button btnScheduleAllClasses;
        private System.Windows.Forms.ComboBox boxScheduleClass;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Button btnScheduleGo;
        private System.Windows.Forms.ComboBox boxScheduleRMS;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox boxScheduleSearch;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.RadioButton radioScheduleDaily;
        private System.Windows.Forms.RadioButton radioScheduleOnce;
        private System.Windows.Forms.CheckBox boxScheduleSaturday;
        private System.Windows.Forms.CheckBox boxScheduleFriday;
        private System.Windows.Forms.CheckBox boxScheduleThursday;
        private System.Windows.Forms.CheckBox boxScheduleWednessday;
        private System.Windows.Forms.CheckBox boxScheduleTuesday;
        private System.Windows.Forms.CheckBox boxScheduleMonday;
        private System.Windows.Forms.TextBox boxScheduleRunMinutes;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Button btnScheduleUncheckAll;
        private System.Windows.Forms.Button btnScheduleCheckAll;
        private System.Windows.Forms.Button BtnScheduleClear;
        private System.Windows.Forms.Button btnScheduleSearch;
        private System.Windows.Forms.DateTimePicker boxScheduleStartTime;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.RadioButton radioScheduleWeekly;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel panelDays;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.RichTextBox boxScheduleComments;
        private System.Windows.Forms.TextBox boxScheduleUserName;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox boxScheduleReasonCategory;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.CheckBox boxScheduleSunday;
        private System.Windows.Forms.CheckedListBox boxScheduleSearchResults;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox boxScheduleRemoteComputer;
        private System.Windows.Forms.TextBox boxSchedulePassword;
        private System.Windows.Forms.TextBox boxScheduleTaskName;
        private System.Windows.Forms.Label label62;
    }
}

