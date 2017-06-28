<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
    Me.splitCont = New System.Windows.Forms.SplitContainer()
    Me.gridRunbooks = New System.Windows.Forms.DataGridView()
    Me.colUniqueID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunbookName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunbookServer = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunning = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colQueued = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtUpdateEvery = New System.Windows.Forms.NumericUpDown()
    Me.chkAutoUpdate = New System.Windows.Forms.CheckBox()
    Me.autoUpdate = New System.Windows.Forms.Timer(Me.components)
    Me.worker = New System.ComponentModel.BackgroundWorker()
    Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
    Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ShowRunbookServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.SearchByPIDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.SearchObjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
    Me.RunningStatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.AuditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.AuditRunbooksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ClientConnectionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
    Me.InvokeActivityByPathToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.LoggingEnabledRunbookToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
    Me.ActivitiesUsingVariableMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.AdminOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.StopAllRunbookOnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
    Me.SystemHealthToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.LogPurgeTrendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
    Me.FlushWebserviceCacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.QuickRebuildToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.FullRebuildToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
    Me.ChangeUserPasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
    Me.DatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.TableSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.FileUsageAndSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ReindexTablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.UpdateStatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.CheckFragmentationLevelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
    Me.lblEnvironment = New System.Windows.Forms.ToolStripLabel()
    Me.cmbEnvironment = New System.Windows.Forms.ToolStripComboBox()
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
    Me.cmdStartMonitoringRunbooks = New System.Windows.Forms.ToolStripButton()
    Me.cmdStopMonitoringRunbooks = New System.Windows.Forms.ToolStripButton()
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
    Me.cmdStopAllRunbooks = New System.Windows.Forms.ToolStripButton()
    Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
    Me.lblRunningRunbooksCount = New System.Windows.Forms.ToolStripLabel()
    Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
    Me.lblQueuedRunbooksCount = New System.Windows.Forms.ToolStripLabel()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.lblLastUpdate = New System.Windows.Forms.ToolStripStatusLabel()
    Me.workerLoadIPs = New System.ComponentModel.BackgroundWorker()
    Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
    Me.FilterViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.lblFilter = New System.Windows.Forms.Label()
    CType(Me.splitCont, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.splitCont.Panel1.SuspendLayout()
    Me.splitCont.Panel2.SuspendLayout()
    Me.splitCont.SuspendLayout()
    CType(Me.gridRunbooks, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.txtUpdateEvery, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.MenuStrip1.SuspendLayout()
    Me.ToolStrip1.SuspendLayout()
    Me.StatusStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'splitCont
    '
    Me.splitCont.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
    Me.splitCont.Location = New System.Drawing.Point(0, 52)
    Me.splitCont.Name = "splitCont"
    Me.splitCont.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'splitCont.Panel1
    '
    Me.splitCont.Panel1.Controls.Add(Me.gridRunbooks)
    '
    'splitCont.Panel2
    '
    Me.splitCont.Panel2.Controls.Add(Me.lblFilter)
    Me.splitCont.Panel2.Controls.Add(Me.cmdRefresh)
    Me.splitCont.Panel2.Controls.Add(Me.Label1)
    Me.splitCont.Panel2.Controls.Add(Me.txtUpdateEvery)
    Me.splitCont.Panel2.Controls.Add(Me.chkAutoUpdate)
    Me.splitCont.Size = New System.Drawing.Size(677, 440)
    Me.splitCont.SplitterDistance = 384
    Me.splitCont.TabIndex = 1
    '
    'gridRunbooks
    '
    Me.gridRunbooks.AllowUserToAddRows = False
    Me.gridRunbooks.AllowUserToDeleteRows = False
    Me.gridRunbooks.AllowUserToOrderColumns = True
    Me.gridRunbooks.AllowUserToResizeRows = False
    Me.gridRunbooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridRunbooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridRunbooks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colUniqueID, Me.colRunbookName, Me.colRunbookServer, Me.colRunning, Me.colQueued})
    Me.gridRunbooks.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gridRunbooks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridRunbooks.Location = New System.Drawing.Point(0, 0)
    Me.gridRunbooks.Name = "gridRunbooks"
    Me.gridRunbooks.ReadOnly = True
    Me.gridRunbooks.RowHeadersVisible = False
    Me.gridRunbooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridRunbooks.Size = New System.Drawing.Size(677, 384)
    Me.gridRunbooks.TabIndex = 1
    '
    'colUniqueID
    '
    Me.colUniqueID.DataPropertyName = "UniqueID"
    Me.colUniqueID.HeaderText = "UniqueID"
    Me.colUniqueID.Name = "colUniqueID"
    Me.colUniqueID.ReadOnly = True
    Me.colUniqueID.Visible = False
    '
    'colRunbookName
    '
    Me.colRunbookName.DataPropertyName = "RunbookName"
    Me.colRunbookName.HeaderText = "Runbook Name"
    Me.colRunbookName.Name = "colRunbookName"
    Me.colRunbookName.ReadOnly = True
    '
    'colRunbookServer
    '
    Me.colRunbookServer.HeaderText = "Runbook Server"
    Me.colRunbookServer.Name = "colRunbookServer"
    Me.colRunbookServer.ReadOnly = True
    Me.colRunbookServer.Visible = False
    '
    'colRunning
    '
    Me.colRunning.DataPropertyName = "Running"
    Me.colRunning.HeaderText = "Running Instances"
    Me.colRunning.Name = "colRunning"
    Me.colRunning.ReadOnly = True
    '
    'colQueued
    '
    Me.colQueued.DataPropertyName = "Queued"
    Me.colQueued.HeaderText = "Queued Instances"
    Me.colQueued.Name = "colQueued"
    Me.colQueued.ReadOnly = True
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.ForeColor = System.Drawing.Color.Maroon
    Me.cmdRefresh.Image = Global.Orchestrator_Health_Checker.My.Resources.Resources.refresh
    Me.cmdRefresh.Location = New System.Drawing.Point(590, 6)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(75, 39)
    Me.cmdRefresh.TabIndex = 3
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(205, 19)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(47, 13)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "seconds"
    '
    'txtUpdateEvery
    '
    Me.txtUpdateEvery.Increment = New Decimal(New Integer() {5, 0, 0, 0})
    Me.txtUpdateEvery.Location = New System.Drawing.Point(133, 15)
    Me.txtUpdateEvery.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
    Me.txtUpdateEvery.Name = "txtUpdateEvery"
    Me.txtUpdateEvery.Size = New System.Drawing.Size(65, 20)
    Me.txtUpdateEvery.TabIndex = 1
    Me.txtUpdateEvery.Value = New Decimal(New Integer() {5, 0, 0, 0})
    '
    'chkAutoUpdate
    '
    Me.chkAutoUpdate.AutoSize = True
    Me.chkAutoUpdate.Location = New System.Drawing.Point(12, 17)
    Me.chkAutoUpdate.Name = "chkAutoUpdate"
    Me.chkAutoUpdate.Size = New System.Drawing.Size(115, 17)
    Me.chkAutoUpdate.TabIndex = 0
    Me.chkAutoUpdate.Text = "Auto-Update every"
    Me.chkAutoUpdate.UseVisualStyleBackColor = True
    '
    'autoUpdate
    '
    '
    'worker
    '
    Me.worker.WorkerSupportsCancellation = True
    '
    'MenuStrip1
    '
    Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem, Me.AuditToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.HelpToolStripMenuItem})
    Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip1.Name = "MenuStrip1"
    Me.MenuStrip1.Size = New System.Drawing.Size(684, 24)
    Me.MenuStrip1.TabIndex = 3
    Me.MenuStrip1.Text = "MenuStrip1"
    '
    'FileToolStripMenuItem
    '
    Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
    Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
    Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
    Me.FileToolStripMenuItem.Text = "&File"
    '
    'ExitToolStripMenuItem
    '
    Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
    Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
    Me.ExitToolStripMenuItem.Text = "E&xit"
    '
    'ViewToolStripMenuItem
    '
    Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowRunbookServerToolStripMenuItem, Me.FilterViewToolStripMenuItem, Me.ToolStripSeparator10, Me.SearchByPIDToolStripMenuItem, Me.SearchObjectToolStripMenuItem, Me.ToolStripMenuItem2, Me.RunningStatisticsToolStripMenuItem})
    Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
    Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
    Me.ViewToolStripMenuItem.Text = "&View"
    '
    'ShowRunbookServerToolStripMenuItem
    '
    Me.ShowRunbookServerToolStripMenuItem.CheckOnClick = True
    Me.ShowRunbookServerToolStripMenuItem.Name = "ShowRunbookServerToolStripMenuItem"
    Me.ShowRunbookServerToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
    Me.ShowRunbookServerToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
    Me.ShowRunbookServerToolStripMenuItem.Text = "&Show Runbook Server"
    '
    'SearchByPIDToolStripMenuItem
    '
    Me.SearchByPIDToolStripMenuItem.Name = "SearchByPIDToolStripMenuItem"
    Me.SearchByPIDToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
    Me.SearchByPIDToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
    Me.SearchByPIDToolStripMenuItem.Text = "Search By &PID..."
    '
    'SearchObjectToolStripMenuItem
    '
    Me.SearchObjectToolStripMenuItem.Name = "SearchObjectToolStripMenuItem"
    Me.SearchObjectToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
    Me.SearchObjectToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
    Me.SearchObjectToolStripMenuItem.Text = "Search &Object..."
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(227, 6)
    '
    'RunningStatisticsToolStripMenuItem
    '
    Me.RunningStatisticsToolStripMenuItem.Name = "RunningStatisticsToolStripMenuItem"
    Me.RunningStatisticsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8
    Me.RunningStatisticsToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
    Me.RunningStatisticsToolStripMenuItem.Text = "&Running Statistics"
    '
    'AuditToolStripMenuItem
    '
    Me.AuditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AuditRunbooksToolStripMenuItem, Me.ClientConnectionsToolStripMenuItem, Me.ToolStripMenuItem1, Me.InvokeActivityByPathToolStripMenuItem, Me.LoggingEnabledRunbookToolStripMenuItem, Me.ToolStripSeparator9, Me.ActivitiesUsingVariableMenuItem})
    Me.AuditToolStripMenuItem.Name = "AuditToolStripMenuItem"
    Me.AuditToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
    Me.AuditToolStripMenuItem.Text = "&Audit"
    '
    'AuditRunbooksToolStripMenuItem
    '
    Me.AuditRunbooksToolStripMenuItem.Name = "AuditRunbooksToolStripMenuItem"
    Me.AuditRunbooksToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.AuditRunbooksToolStripMenuItem.Text = "&Runbooks"
    '
    'ClientConnectionsToolStripMenuItem
    '
    Me.ClientConnectionsToolStripMenuItem.Name = "ClientConnectionsToolStripMenuItem"
    Me.ClientConnectionsToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.ClientConnectionsToolStripMenuItem.Text = "&Client Connections"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(216, 6)
    '
    'InvokeActivityByPathToolStripMenuItem
    '
    Me.InvokeActivityByPathToolStripMenuItem.Name = "InvokeActivityByPathToolStripMenuItem"
    Me.InvokeActivityByPathToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.InvokeActivityByPathToolStripMenuItem.Text = """Invoke Activity"" by &Path"
    '
    'LoggingEnabledRunbookToolStripMenuItem
    '
    Me.LoggingEnabledRunbookToolStripMenuItem.Name = "LoggingEnabledRunbookToolStripMenuItem"
    Me.LoggingEnabledRunbookToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.LoggingEnabledRunbookToolStripMenuItem.Text = "&Logging Enabled Runbooks"
    '
    'ToolStripSeparator9
    '
    Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
    Me.ToolStripSeparator9.Size = New System.Drawing.Size(216, 6)
    '
    'ActivitiesUsingVariableMenuItem
    '
    Me.ActivitiesUsingVariableMenuItem.Name = "ActivitiesUsingVariableMenuItem"
    Me.ActivitiesUsingVariableMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.ActivitiesUsingVariableMenuItem.Text = "Activities using &Variable"
    '
    'OptionsToolStripMenuItem
    '
    Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AdminOptionsToolStripMenuItem, Me.StopAllRunbookOnToolStripMenuItem, Me.ToolStripSeparator3, Me.SystemHealthToolStripMenuItem, Me.LogPurgeTrendToolStripMenuItem, Me.ToolStripSeparator4, Me.FlushWebserviceCacheToolStripMenuItem, Me.ToolStripSeparator6, Me.ChangeUserPasswordToolStripMenuItem, Me.ToolStripSeparator5, Me.DatabaseToolStripMenuItem})
    Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
    Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
    Me.OptionsToolStripMenuItem.Text = "&Options"
    '
    'AdminOptionsToolStripMenuItem
    '
    Me.AdminOptionsToolStripMenuItem.Name = "AdminOptionsToolStripMenuItem"
    Me.AdminOptionsToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
    Me.AdminOptionsToolStripMenuItem.Text = "&Admin Options..."
    '
    'StopAllRunbookOnToolStripMenuItem
    '
    Me.StopAllRunbookOnToolStripMenuItem.Name = "StopAllRunbookOnToolStripMenuItem"
    Me.StopAllRunbookOnToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
    Me.StopAllRunbookOnToolStripMenuItem.Text = "S&top All Runbook On..."
    '
    'ToolStripSeparator3
    '
    Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    Me.ToolStripSeparator3.Size = New System.Drawing.Size(248, 6)
    '
    'SystemHealthToolStripMenuItem
    '
    Me.SystemHealthToolStripMenuItem.Name = "SystemHealthToolStripMenuItem"
    Me.SystemHealthToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
    Me.SystemHealthToolStripMenuItem.Text = "&System Health"
    '
    'LogPurgeTrendToolStripMenuItem
    '
    Me.LogPurgeTrendToolStripMenuItem.Name = "LogPurgeTrendToolStripMenuItem"
    Me.LogPurgeTrendToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
    Me.LogPurgeTrendToolStripMenuItem.Text = "&Log Purge Trend"
    '
    'ToolStripSeparator4
    '
    Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
    Me.ToolStripSeparator4.Size = New System.Drawing.Size(248, 6)
    '
    'FlushWebserviceCacheToolStripMenuItem
    '
    Me.FlushWebserviceCacheToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QuickRebuildToolStripMenuItem, Me.FullRebuildToolStripMenuItem})
    Me.FlushWebserviceCacheToolStripMenuItem.Name = "FlushWebserviceCacheToolStripMenuItem"
    Me.FlushWebserviceCacheToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
    Me.FlushWebserviceCacheToolStripMenuItem.Text = "&Flush Web Service Cache"
    '
    'QuickRebuildToolStripMenuItem
    '
    Me.QuickRebuildToolStripMenuItem.Name = "QuickRebuildToolStripMenuItem"
    Me.QuickRebuildToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
    Me.QuickRebuildToolStripMenuItem.Text = "Quick Re-build"
    '
    'FullRebuildToolStripMenuItem
    '
    Me.FullRebuildToolStripMenuItem.Name = "FullRebuildToolStripMenuItem"
    Me.FullRebuildToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
    Me.FullRebuildToolStripMenuItem.Text = "Full Re-build"
    '
    'ToolStripSeparator6
    '
    Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
    Me.ToolStripSeparator6.Size = New System.Drawing.Size(248, 6)
    '
    'ChangeUserPasswordToolStripMenuItem
    '
    Me.ChangeUserPasswordToolStripMenuItem.Enabled = False
    Me.ChangeUserPasswordToolStripMenuItem.Name = "ChangeUserPasswordToolStripMenuItem"
    Me.ChangeUserPasswordToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
    Me.ChangeUserPasswordToolStripMenuItem.Text = "&Update User Password on Objects"
    '
    'ToolStripSeparator5
    '
    Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
    Me.ToolStripSeparator5.Size = New System.Drawing.Size(248, 6)
    '
    'DatabaseToolStripMenuItem
    '
    Me.DatabaseToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TableSizeToolStripMenuItem, Me.FileUsageAndSizeToolStripMenuItem, Me.ReindexTablesToolStripMenuItem, Me.UpdateStatisticsToolStripMenuItem, Me.CheckFragmentationLevelsToolStripMenuItem})
    Me.DatabaseToolStripMenuItem.Name = "DatabaseToolStripMenuItem"
    Me.DatabaseToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
    Me.DatabaseToolStripMenuItem.Text = "&Database..."
    '
    'TableSizeToolStripMenuItem
    '
    Me.TableSizeToolStripMenuItem.Name = "TableSizeToolStripMenuItem"
    Me.TableSizeToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
    Me.TableSizeToolStripMenuItem.Text = "&Table Size..."
    '
    'FileUsageAndSizeToolStripMenuItem
    '
    Me.FileUsageAndSizeToolStripMenuItem.Name = "FileUsageAndSizeToolStripMenuItem"
    Me.FileUsageAndSizeToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
    Me.FileUsageAndSizeToolStripMenuItem.Text = "F&ile Usage and Size..."
    '
    'ReindexTablesToolStripMenuItem
    '
    Me.ReindexTablesToolStripMenuItem.Name = "ReindexTablesToolStripMenuItem"
    Me.ReindexTablesToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
    Me.ReindexTablesToolStripMenuItem.Text = "&Reindex Tables"
    '
    'UpdateStatisticsToolStripMenuItem
    '
    Me.UpdateStatisticsToolStripMenuItem.Name = "UpdateStatisticsToolStripMenuItem"
    Me.UpdateStatisticsToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
    Me.UpdateStatisticsToolStripMenuItem.Text = "&Update Statistics"
    '
    'CheckFragmentationLevelsToolStripMenuItem
    '
    Me.CheckFragmentationLevelsToolStripMenuItem.Name = "CheckFragmentationLevelsToolStripMenuItem"
    Me.CheckFragmentationLevelsToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
    Me.CheckFragmentationLevelsToolStripMenuItem.Text = "&Check Fragmentation Levels..."
    '
    'HelpToolStripMenuItem
    '
    Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
    Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
    Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
    Me.HelpToolStripMenuItem.Text = "&Help"
    '
    'AboutToolStripMenuItem
    '
    Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
    Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
    Me.AboutToolStripMenuItem.Text = "&About..."
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblEnvironment, Me.cmbEnvironment, Me.ToolStripSeparator1, Me.cmdStartMonitoringRunbooks, Me.cmdStopMonitoringRunbooks, Me.ToolStripSeparator2, Me.cmdStopAllRunbooks, Me.ToolStripSeparator7, Me.lblRunningRunbooksCount, Me.ToolStripSeparator8, Me.lblQueuedRunbooksCount})
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(684, 25)
    Me.ToolStrip1.TabIndex = 4
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'lblEnvironment
    '
    Me.lblEnvironment.Name = "lblEnvironment"
    Me.lblEnvironment.Size = New System.Drawing.Size(81, 22)
    Me.lblEnvironment.Text = "Environment: "
    '
    'cmbEnvironment
    '
    Me.cmbEnvironment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.cmbEnvironment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.cmbEnvironment.Name = "cmbEnvironment"
    Me.cmbEnvironment.Size = New System.Drawing.Size(121, 25)
    Me.cmbEnvironment.Sorted = True
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'cmdStartMonitoringRunbooks
    '
    Me.cmdStartMonitoringRunbooks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.cmdStartMonitoringRunbooks.Image = CType(resources.GetObject("cmdStartMonitoringRunbooks.Image"), System.Drawing.Image)
    Me.cmdStartMonitoringRunbooks.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.cmdStartMonitoringRunbooks.Name = "cmdStartMonitoringRunbooks"
    Me.cmdStartMonitoringRunbooks.Size = New System.Drawing.Size(23, 22)
    Me.cmdStartMonitoringRunbooks.Text = "ToolStripButton1"
    Me.cmdStartMonitoringRunbooks.ToolTipText = "Start Monitoring Runbooks"
    '
    'cmdStopMonitoringRunbooks
    '
    Me.cmdStopMonitoringRunbooks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.cmdStopMonitoringRunbooks.Image = CType(resources.GetObject("cmdStopMonitoringRunbooks.Image"), System.Drawing.Image)
    Me.cmdStopMonitoringRunbooks.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.cmdStopMonitoringRunbooks.Name = "cmdStopMonitoringRunbooks"
    Me.cmdStopMonitoringRunbooks.Size = New System.Drawing.Size(23, 22)
    Me.cmdStopMonitoringRunbooks.Text = "ToolStripButton2"
    Me.cmdStopMonitoringRunbooks.ToolTipText = "Stop Monitoring Runbooks"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
    '
    'cmdStopAllRunbooks
    '
    Me.cmdStopAllRunbooks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.cmdStopAllRunbooks.Image = CType(resources.GetObject("cmdStopAllRunbooks.Image"), System.Drawing.Image)
    Me.cmdStopAllRunbooks.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.cmdStopAllRunbooks.Name = "cmdStopAllRunbooks"
    Me.cmdStopAllRunbooks.Size = New System.Drawing.Size(23, 22)
    Me.cmdStopAllRunbooks.Text = "Stop All Running Runbooks"
    '
    'ToolStripSeparator7
    '
    Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
    Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
    '
    'lblRunningRunbooksCount
    '
    Me.lblRunningRunbooksCount.Name = "lblRunningRunbooksCount"
    Me.lblRunningRunbooksCount.Size = New System.Drawing.Size(138, 22)
    Me.lblRunningRunbooksCount.Text = "Running Runbooks: 1234"
    '
    'ToolStripSeparator8
    '
    Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
    Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
    '
    'lblQueuedRunbooksCount
    '
    Me.lblQueuedRunbooksCount.Name = "lblQueuedRunbooksCount"
    Me.lblQueuedRunbooksCount.Size = New System.Drawing.Size(135, 22)
    Me.lblQueuedRunbooksCount.Text = "Queued Runbooks: 1234"
    '
    'StatusStrip1
    '
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblLastUpdate})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 495)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(684, 22)
    Me.StatusStrip1.TabIndex = 5
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'lblLastUpdate
    '
    Me.lblLastUpdate.Name = "lblLastUpdate"
    Me.lblLastUpdate.Size = New System.Drawing.Size(0, 17)
    '
    'workerLoadIPs
    '
    '
    'ToolStripSeparator10
    '
    Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
    Me.ToolStripSeparator10.Size = New System.Drawing.Size(227, 6)
    '
    'FilterViewToolStripMenuItem
    '
    Me.FilterViewToolStripMenuItem.Name = "FilterViewToolStripMenuItem"
    Me.FilterViewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
    Me.FilterViewToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
    Me.FilterViewToolStripMenuItem.Text = "Filter &View..."
    '
    'lblFilter
    '
    Me.lblFilter.AutoSize = True
    Me.lblFilter.Location = New System.Drawing.Point(291, 19)
    Me.lblFilter.Name = "lblFilter"
    Me.lblFilter.Size = New System.Drawing.Size(29, 13)
    Me.lblFilter.TabIndex = 4
    Me.lblFilter.Text = "Filter"
    Me.lblFilter.Visible = False
    '
    'frmMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(684, 517)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.ToolStrip1)
    Me.Controls.Add(Me.MenuStrip1)
    Me.Controls.Add(Me.splitCont)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmMain"
    Me.Text = "Orchestrator Health Checker"
    Me.splitCont.Panel1.ResumeLayout(False)
    Me.splitCont.Panel2.ResumeLayout(False)
    Me.splitCont.Panel2.PerformLayout()
    CType(Me.splitCont, System.ComponentModel.ISupportInitialize).EndInit()
    Me.splitCont.ResumeLayout(False)
    CType(Me.gridRunbooks, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.txtUpdateEvery, System.ComponentModel.ISupportInitialize).EndInit()
    Me.MenuStrip1.ResumeLayout(False)
    Me.MenuStrip1.PerformLayout()
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents splitCont As System.Windows.Forms.SplitContainer
  Friend WithEvents gridRunbooks As System.Windows.Forms.DataGridView
  Friend WithEvents chkAutoUpdate As System.Windows.Forms.CheckBox
  Friend WithEvents txtUpdateEvery As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents autoUpdate As System.Windows.Forms.Timer
  Friend WithEvents worker As System.ComponentModel.BackgroundWorker
  Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
  Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AdminOptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents lblLastUpdate As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents lblEnvironment As System.Windows.Forms.ToolStripLabel
  Friend WithEvents cmbEnvironment As System.Windows.Forms.ToolStripComboBox
  Friend WithEvents SystemHealthToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LogPurgeTrendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents cmdStartMonitoringRunbooks As System.Windows.Forms.ToolStripButton
  Friend WithEvents cmdStopMonitoringRunbooks As System.Windows.Forms.ToolStripButton
  Friend WithEvents cmdStopAllRunbooks As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ShowRunbookServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents colUniqueID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunbookName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunbookServer As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunning As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colQueued As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents StopAllRunbookOnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SearchByPIDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FlushWebserviceCacheToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents DatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TableSizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FileUsageAndSizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ReindexTablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents UpdateStatisticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CheckFragmentationLevelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents lblRunningRunbooksCount As System.Windows.Forms.ToolStripLabel
  Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents lblQueuedRunbooksCount As System.Windows.Forms.ToolStripLabel
  Friend WithEvents AuditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AuditRunbooksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ClientConnectionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents workerLoadIPs As System.ComponentModel.BackgroundWorker
  Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ChangeUserPasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SearchObjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents InvokeActivityByPathToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents QuickRebuildToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FullRebuildToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RunningStatisticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LoggingEnabledRunbookToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ActivitiesUsingVariableMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FilterViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents lblFilter As System.Windows.Forms.Label

End Class
