namespace Microsoft.SystemCenter.Orchestrator.Integration.Examples.IPCleaner
{
    partial class IPCleanerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IPCleanerForm));
            this.buttonPerformActions = new System.Windows.Forms.Button();
            this.checkBoxUndeployIP = new System.Windows.Forms.CheckBox();
            this.checkBoxUnregisterIP = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumActivities = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumInstances = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataInstances = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registered = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Deployed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.textBoxSelectedIPInfo = new System.Windows.Forms.TextBox();
            this.groupBoxDeleteActions = new System.Windows.Forms.GroupBox();
            this.groupBoxIPActions = new System.Windows.Forms.GroupBox();
            this.groupBoxActivities = new System.Windows.Forms.GroupBox();
            this.radioRemoveAllActivities = new System.Windows.Forms.RadioButton();
            this.radioRemoveNoActivities = new System.Windows.Forms.RadioButton();
            this.comboBoxRemoveAllOptions = new System.Windows.Forms.ComboBox();
            this.radioRemoveDeletedActivities = new System.Windows.Forms.RadioButton();
            this.groupBoxJobHistory = new System.Windows.Forms.GroupBox();
            this.radioRemoveNoJobHistory = new System.Windows.Forms.RadioButton();
            this.radioRemoveAllJobHistory = new System.Windows.Forms.RadioButton();
            this.radioRemoveDeletedJobHistory = new System.Windows.Forms.RadioButton();
            this.checkBoxCleanAll = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureDatabaseConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonGetDetails = new System.Windows.Forms.Button();
            this.textBoxConnectedTo = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxLogging = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxDeleteActions.SuspendLayout();
            this.groupBoxIPActions.SuspendLayout();
            this.groupBoxActivities.SuspendLayout();
            this.groupBoxJobHistory.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPerformActions
            // 
            this.buttonPerformActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPerformActions.Enabled = false;
            this.buttonPerformActions.Location = new System.Drawing.Point(601, 731);
            this.buttonPerformActions.Name = "buttonPerformActions";
            this.buttonPerformActions.Size = new System.Drawing.Size(145, 23);
            this.buttonPerformActions.TabIndex = 5;
            this.buttonPerformActions.Text = "&Perform Selected Actions";
            this.buttonPerformActions.UseVisualStyleBackColor = true;
            this.buttonPerformActions.Click += new System.EventHandler(this.buttonPerformActions_Click);
            // 
            // checkBoxUndeployIP
            // 
            this.checkBoxUndeployIP.AutoSize = true;
            this.checkBoxUndeployIP.Location = new System.Drawing.Point(18, 47);
            this.checkBoxUndeployIP.Name = "checkBoxUndeployIP";
            this.checkBoxUndeployIP.Size = new System.Drawing.Size(105, 17);
            this.checkBoxUndeployIP.TabIndex = 1;
            this.checkBoxUndeployIP.Text = "Un-deploy the IP";
            this.checkBoxUndeployIP.UseVisualStyleBackColor = true;
            this.checkBoxUndeployIP.CheckedChanged += new System.EventHandler(this.checkBoxUndeployIP_CheckedChanged);
            // 
            // checkBoxUnregisterIP
            // 
            this.checkBoxUnregisterIP.AutoSize = true;
            this.checkBoxUnregisterIP.Location = new System.Drawing.Point(18, 24);
            this.checkBoxUnregisterIP.Name = "checkBoxUnregisterIP";
            this.checkBoxUnregisterIP.Size = new System.Drawing.Size(105, 17);
            this.checkBoxUnregisterIP.TabIndex = 0;
            this.checkBoxUnregisterIP.Text = "Unregister the IP";
            this.checkBoxUnregisterIP.UseVisualStyleBackColor = true;
            this.checkBoxUnregisterIP.CheckedChanged += new System.EventHandler(this.checkBoxUnregisterIP_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IPName,
            this.Type,
            this.NumActivities,
            this.NumInstances,
            this.DataInstances,
            this.Registered,
            this.Deployed});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(11, 64);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(817, 267);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // IPName
            // 
            this.IPName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPName.HeaderText = "IP Name";
            this.IPName.Name = "IPName";
            this.IPName.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.FillWeight = 50F;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Type.Width = 80;
            // 
            // NumActivities
            // 
            this.NumActivities.FillWeight = 50F;
            this.NumActivities.HeaderText = "Activities";
            this.NumActivities.MaxInputLength = 10;
            this.NumActivities.Name = "NumActivities";
            this.NumActivities.ReadOnly = true;
            this.NumActivities.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NumActivities.Width = 60;
            // 
            // NumInstances
            // 
            this.NumInstances.FillWeight = 50F;
            this.NumInstances.HeaderText = "Runbook Instances";
            this.NumInstances.MaxInputLength = 10;
            this.NumInstances.Name = "NumInstances";
            this.NumInstances.ReadOnly = true;
            this.NumInstances.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NumInstances.Width = 60;
            // 
            // DataInstances
            // 
            this.DataInstances.FillWeight = 50F;
            this.DataInstances.HeaderText = "Data Instances";
            this.DataInstances.Name = "DataInstances";
            this.DataInstances.ReadOnly = true;
            this.DataInstances.Width = 60;
            // 
            // Registered
            // 
            this.Registered.FillWeight = 40F;
            this.Registered.HeaderText = "Registered";
            this.Registered.Name = "Registered";
            this.Registered.ReadOnly = true;
            this.Registered.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Registered.Width = 75;
            // 
            // Deployed
            // 
            this.Deployed.FillWeight = 40F;
            this.Deployed.HeaderText = "Deployed";
            this.Deployed.Name = "Deployed";
            this.Deployed.ReadOnly = true;
            this.Deployed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Deployed.Width = 75;
            // 
            // textBoxSelectedIPInfo
            // 
            this.textBoxSelectedIPInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSelectedIPInfo.Enabled = false;
            this.textBoxSelectedIPInfo.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSelectedIPInfo.Location = new System.Drawing.Point(3, 3);
            this.textBoxSelectedIPInfo.Multiline = true;
            this.textBoxSelectedIPInfo.Name = "textBoxSelectedIPInfo";
            this.textBoxSelectedIPInfo.ReadOnly = true;
            this.textBoxSelectedIPInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSelectedIPInfo.Size = new System.Drawing.Size(506, 356);
            this.textBoxSelectedIPInfo.TabIndex = 24;
            this.textBoxSelectedIPInfo.TabStop = false;
            // 
            // groupBoxDeleteActions
            // 
            this.groupBoxDeleteActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDeleteActions.Controls.Add(this.groupBoxIPActions);
            this.groupBoxDeleteActions.Controls.Add(this.groupBoxActivities);
            this.groupBoxDeleteActions.Controls.Add(this.groupBoxJobHistory);
            this.groupBoxDeleteActions.Controls.Add(this.checkBoxCleanAll);
            this.groupBoxDeleteActions.Enabled = false;
            this.groupBoxDeleteActions.Location = new System.Drawing.Point(538, 337);
            this.groupBoxDeleteActions.Name = "groupBoxDeleteActions";
            this.groupBoxDeleteActions.Size = new System.Drawing.Size(290, 386);
            this.groupBoxDeleteActions.TabIndex = 4;
            this.groupBoxDeleteActions.TabStop = false;
            this.groupBoxDeleteActions.Text = "Available Actions";
            // 
            // groupBoxIPActions
            // 
            this.groupBoxIPActions.Controls.Add(this.checkBoxUndeployIP);
            this.groupBoxIPActions.Controls.Add(this.checkBoxUnregisterIP);
            this.groupBoxIPActions.Location = new System.Drawing.Point(6, 73);
            this.groupBoxIPActions.Name = "groupBoxIPActions";
            this.groupBoxIPActions.Size = new System.Drawing.Size(269, 79);
            this.groupBoxIPActions.TabIndex = 1;
            this.groupBoxIPActions.TabStop = false;
            this.groupBoxIPActions.Text = "IP Actions";
            // 
            // groupBoxActivities
            // 
            this.groupBoxActivities.Controls.Add(this.radioRemoveAllActivities);
            this.groupBoxActivities.Controls.Add(this.radioRemoveNoActivities);
            this.groupBoxActivities.Controls.Add(this.comboBoxRemoveAllOptions);
            this.groupBoxActivities.Controls.Add(this.radioRemoveDeletedActivities);
            this.groupBoxActivities.Location = new System.Drawing.Point(6, 158);
            this.groupBoxActivities.Name = "groupBoxActivities";
            this.groupBoxActivities.Size = new System.Drawing.Size(269, 122);
            this.groupBoxActivities.TabIndex = 2;
            this.groupBoxActivities.TabStop = false;
            this.groupBoxActivities.Text = "Activities in Runbooks";
            // 
            // radioRemoveAllActivities
            // 
            this.radioRemoveAllActivities.AutoSize = true;
            this.radioRemoveAllActivities.Location = new System.Drawing.Point(17, 21);
            this.radioRemoveAllActivities.Name = "radioRemoveAllActivities";
            this.radioRemoveAllActivities.Size = new System.Drawing.Size(79, 17);
            this.radioRemoveAllActivities.TabIndex = 0;
            this.radioRemoveAllActivities.Text = "Remove All";
            this.radioRemoveAllActivities.UseVisualStyleBackColor = true;
            this.radioRemoveAllActivities.CheckedChanged += new System.EventHandler(this.radioRemoveAllActivities_CheckedChanged);
            // 
            // radioRemoveNoActivities
            // 
            this.radioRemoveNoActivities.AutoSize = true;
            this.radioRemoveNoActivities.Checked = true;
            this.radioRemoveNoActivities.Location = new System.Drawing.Point(18, 94);
            this.radioRemoveNoActivities.Name = "radioRemoveNoActivities";
            this.radioRemoveNoActivities.Size = new System.Drawing.Size(94, 17);
            this.radioRemoveNoActivities.TabIndex = 3;
            this.radioRemoveNoActivities.TabStop = true;
            this.radioRemoveNoActivities.Text = "Remove None";
            this.radioRemoveNoActivities.UseVisualStyleBackColor = true;
            // 
            // comboBoxRemoveAllOptions
            // 
            this.comboBoxRemoveAllOptions.Enabled = false;
            this.comboBoxRemoveAllOptions.FormattingEnabled = true;
            this.comboBoxRemoveAllOptions.Items.AddRange(new object[] {
            "Replace with disabled unknown activity",
            "Leave empty space unfilled"});
            this.comboBoxRemoveAllOptions.Location = new System.Drawing.Point(38, 44);
            this.comboBoxRemoveAllOptions.Name = "comboBoxRemoveAllOptions";
            this.comboBoxRemoveAllOptions.Size = new System.Drawing.Size(225, 21);
            this.comboBoxRemoveAllOptions.TabIndex = 1;
            // 
            // radioRemoveDeletedActivities
            // 
            this.radioRemoveDeletedActivities.AutoSize = true;
            this.radioRemoveDeletedActivities.Location = new System.Drawing.Point(18, 71);
            this.radioRemoveDeletedActivities.Name = "radioRemoveDeletedActivities";
            this.radioRemoveDeletedActivities.Size = new System.Drawing.Size(129, 17);
            this.radioRemoveDeletedActivities.TabIndex = 2;
            this.radioRemoveDeletedActivities.Text = "Remove Deleted Only";
            this.radioRemoveDeletedActivities.UseVisualStyleBackColor = true;
            // 
            // groupBoxJobHistory
            // 
            this.groupBoxJobHistory.Controls.Add(this.radioRemoveNoJobHistory);
            this.groupBoxJobHistory.Controls.Add(this.radioRemoveAllJobHistory);
            this.groupBoxJobHistory.Controls.Add(this.radioRemoveDeletedJobHistory);
            this.groupBoxJobHistory.Location = new System.Drawing.Point(6, 286);
            this.groupBoxJobHistory.Name = "groupBoxJobHistory";
            this.groupBoxJobHistory.Size = new System.Drawing.Size(269, 94);
            this.groupBoxJobHistory.TabIndex = 3;
            this.groupBoxJobHistory.TabStop = false;
            this.groupBoxJobHistory.Text = "Activity Job History and Data";
            // 
            // radioRemoveNoJobHistory
            // 
            this.radioRemoveNoJobHistory.AutoSize = true;
            this.radioRemoveNoJobHistory.Checked = true;
            this.radioRemoveNoJobHistory.Location = new System.Drawing.Point(18, 68);
            this.radioRemoveNoJobHistory.Name = "radioRemoveNoJobHistory";
            this.radioRemoveNoJobHistory.Size = new System.Drawing.Size(94, 17);
            this.radioRemoveNoJobHistory.TabIndex = 2;
            this.radioRemoveNoJobHistory.TabStop = true;
            this.radioRemoveNoJobHistory.Text = "Remove None";
            this.radioRemoveNoJobHistory.UseVisualStyleBackColor = true;
            // 
            // radioRemoveAllJobHistory
            // 
            this.radioRemoveAllJobHistory.AutoSize = true;
            this.radioRemoveAllJobHistory.Location = new System.Drawing.Point(18, 21);
            this.radioRemoveAllJobHistory.Name = "radioRemoveAllJobHistory";
            this.radioRemoveAllJobHistory.Size = new System.Drawing.Size(79, 17);
            this.radioRemoveAllJobHistory.TabIndex = 0;
            this.radioRemoveAllJobHistory.Text = "Remove All";
            this.radioRemoveAllJobHistory.UseVisualStyleBackColor = true;
            // 
            // radioRemoveDeletedJobHistory
            // 
            this.radioRemoveDeletedJobHistory.AutoSize = true;
            this.radioRemoveDeletedJobHistory.Location = new System.Drawing.Point(18, 45);
            this.radioRemoveDeletedJobHistory.Name = "radioRemoveDeletedJobHistory";
            this.radioRemoveDeletedJobHistory.Size = new System.Drawing.Size(129, 17);
            this.radioRemoveDeletedJobHistory.TabIndex = 1;
            this.radioRemoveDeletedJobHistory.Text = "Remove Deleted Only";
            this.radioRemoveDeletedJobHistory.UseVisualStyleBackColor = true;
            // 
            // checkBoxCleanAll
            // 
            this.checkBoxCleanAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCleanAll.AutoSize = true;
            this.checkBoxCleanAll.Location = new System.Drawing.Point(23, 30);
            this.checkBoxCleanAll.Name = "checkBoxCleanAll";
            this.checkBoxCleanAll.Size = new System.Drawing.Size(148, 17);
            this.checkBoxCleanAll.TabIndex = 0;
            this.checkBoxCleanAll.Text = "Remove All IP Information";
            this.checkBoxCleanAll.UseVisualStyleBackColor = true;
            this.checkBoxCleanAll.CheckedChanged += new System.EventHandler(this.checkBoxCleanAll_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(840, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reconnectToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // reconnectToolStripMenuItem
            // 
            this.reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
            this.reconnectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reconnectToolStripMenuItem.Text = "&Reconnect";
            this.reconnectToolStripMenuItem.Click += new System.EventHandler(this.reconnectToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "E&xit";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureDatabaseConnectionToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // configureDatabaseConnectionToolStripMenuItem
            // 
            this.configureDatabaseConnectionToolStripMenuItem.Name = "configureDatabaseConnectionToolStripMenuItem";
            this.configureDatabaseConnectionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.configureDatabaseConnectionToolStripMenuItem.Text = "&Configure Connections";
            this.configureDatabaseConnectionToolStripMenuItem.Click += new System.EventHandler(this.configureDatabaseConnectionToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // buttonGetDetails
            // 
            this.buttonGetDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetDetails.Enabled = false;
            this.buttonGetDetails.Location = new System.Drawing.Point(11, 337);
            this.buttonGetDetails.Name = "buttonGetDetails";
            this.buttonGetDetails.Size = new System.Drawing.Size(167, 23);
            this.buttonGetDetails.TabIndex = 3;
            this.buttonGetDetails.Text = "&Get Details for Selected IP";
            this.buttonGetDetails.UseVisualStyleBackColor = true;
            this.buttonGetDetails.Click += new System.EventHandler(this.buttonGetDetails_Click);
            // 
            // textBoxConnectedTo
            // 
            this.textBoxConnectedTo.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxConnectedTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxConnectedTo.Location = new System.Drawing.Point(12, 40);
            this.textBoxConnectedTo.Name = "textBoxConnectedTo";
            this.textBoxConnectedTo.Size = new System.Drawing.Size(718, 13);
            this.textBoxConnectedTo.TabIndex = 1;
            this.textBoxConnectedTo.TabStop = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Location = new System.Drawing.Point(753, 35);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 2;
            this.buttonRefresh.Text = "&Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 366);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(520, 388);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxSelectedIPInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(512, 362);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Selected IP";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxLogging);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(512, 362);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Logging";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxLogging
            // 
            this.textBoxLogging.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLogging.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogging.Location = new System.Drawing.Point(3, 3);
            this.textBoxLogging.MaxLength = 3276700;
            this.textBoxLogging.Multiline = true;
            this.textBoxLogging.Name = "textBoxLogging";
            this.textBoxLogging.ReadOnly = true;
            this.textBoxLogging.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogging.Size = new System.Drawing.Size(506, 356);
            this.textBoxLogging.TabIndex = 0;
            this.textBoxLogging.TabStop = false;
            // 
            // IPCleanerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 766);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.textBoxConnectedTo);
            this.Controls.Add(this.buttonGetDetails);
            this.Controls.Add(this.groupBoxDeleteActions);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonPerformActions);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(725, 705);
            this.Name = "IPCleanerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Orchestrator IP Cleaner Utility";
            this.Load += new System.EventHandler(this.IPCleanerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxDeleteActions.ResumeLayout(false);
            this.groupBoxDeleteActions.PerformLayout();
            this.groupBoxIPActions.ResumeLayout(false);
            this.groupBoxIPActions.PerformLayout();
            this.groupBoxActivities.ResumeLayout(false);
            this.groupBoxActivities.PerformLayout();
            this.groupBoxJobHistory.ResumeLayout(false);
            this.groupBoxJobHistory.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPerformActions;
        private System.Windows.Forms.CheckBox checkBoxUndeployIP;
        private System.Windows.Forms.CheckBox checkBoxUnregisterIP;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumActivities;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumInstances;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataInstances;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Registered;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Deployed;
        private System.Windows.Forms.TextBox textBoxSelectedIPInfo;
        private System.Windows.Forms.GroupBox groupBoxDeleteActions;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureDatabaseConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button buttonGetDetails;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxCleanAll;
        private System.Windows.Forms.TextBox textBoxConnectedTo;
        private System.Windows.Forms.ComboBox comboBoxRemoveAllOptions;
        private System.Windows.Forms.RadioButton radioRemoveDeletedActivities;
        private System.Windows.Forms.RadioButton radioRemoveAllActivities;
        private System.Windows.Forms.RadioButton radioRemoveDeletedJobHistory;
        private System.Windows.Forms.RadioButton radioRemoveAllJobHistory;
        private System.Windows.Forms.GroupBox groupBoxActivities;
        private System.Windows.Forms.RadioButton radioRemoveNoActivities;
        private System.Windows.Forms.GroupBox groupBoxJobHistory;
        private System.Windows.Forms.RadioButton radioRemoveNoJobHistory;
        private System.Windows.Forms.GroupBox groupBoxIPActions;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxLogging;
    }
}

