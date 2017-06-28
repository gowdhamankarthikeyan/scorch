<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgRunningStatistics
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.gridStatsByServer = New System.Windows.Forms.DataGridView()
    Me.colRunbookServer = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunningRunbooks = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colMaxRunningPolicies = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colUsagePercent = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.gridStatsType = New System.Windows.Forms.DataGridView()
    Me.colRunbookType = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunningRunbooks2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colEnabledRunbooks = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colDisabledRunbooks = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colTotalRunbooks = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.lblLastUpdate = New System.Windows.Forms.Label()
    Me.KeyboardShortcuts = New System.Windows.Forms.MenuStrip()
    Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.RefreshToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    Me.MonitorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.cmdMonitorRunbooksDetails = New System.Windows.Forms.LinkLabel()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridStatsByServer, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.gridStatsType, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.KeyboardShortcuts.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(518, 411)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(82, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(7, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(103, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "By Runbook Server:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 207)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(96, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "By Runbook Type:"
    '
    'gridStatsByServer
    '
    Me.gridStatsByServer.AllowUserToAddRows = False
    Me.gridStatsByServer.AllowUserToDeleteRows = False
    Me.gridStatsByServer.AllowUserToOrderColumns = True
    Me.gridStatsByServer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridStatsByServer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridStatsByServer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRunbookServer, Me.colRunningRunbooks, Me.colMaxRunningPolicies, Me.colUsagePercent})
    Me.gridStatsByServer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridStatsByServer.Location = New System.Drawing.Point(15, 25)
    Me.gridStatsByServer.Name = "gridStatsByServer"
    Me.gridStatsByServer.ReadOnly = True
    Me.gridStatsByServer.RowHeadersVisible = False
    Me.gridStatsByServer.Size = New System.Drawing.Size(585, 150)
    Me.gridStatsByServer.TabIndex = 3
    '
    'colRunbookServer
    '
    Me.colRunbookServer.HeaderText = "Runbook Server"
    Me.colRunbookServer.Name = "colRunbookServer"
    Me.colRunbookServer.ReadOnly = True
    '
    'colRunningRunbooks
    '
    Me.colRunningRunbooks.HeaderText = "Running Runbooks"
    Me.colRunningRunbooks.Name = "colRunningRunbooks"
    Me.colRunningRunbooks.ReadOnly = True
    '
    'colMaxRunningPolicies
    '
    Me.colMaxRunningPolicies.HeaderText = "Max Running Runbooks"
    Me.colMaxRunningPolicies.Name = "colMaxRunningPolicies"
    Me.colMaxRunningPolicies.ReadOnly = True
    '
    'colUsagePercent
    '
    Me.colUsagePercent.HeaderText = "Usage Percent"
    Me.colUsagePercent.Name = "colUsagePercent"
    Me.colUsagePercent.ReadOnly = True
    '
    'gridStatsType
    '
    Me.gridStatsType.AllowUserToAddRows = False
    Me.gridStatsType.AllowUserToDeleteRows = False
    Me.gridStatsType.AllowUserToOrderColumns = True
    Me.gridStatsType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridStatsType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridStatsType.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRunbookType, Me.colRunningRunbooks2, Me.colEnabledRunbooks, Me.colDisabledRunbooks, Me.colTotalRunbooks})
    Me.gridStatsType.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridStatsType.Location = New System.Drawing.Point(15, 232)
    Me.gridStatsType.Name = "gridStatsType"
    Me.gridStatsType.ReadOnly = True
    Me.gridStatsType.RowHeadersVisible = False
    Me.gridStatsType.Size = New System.Drawing.Size(585, 150)
    Me.gridStatsType.TabIndex = 4
    '
    'colRunbookType
    '
    Me.colRunbookType.HeaderText = "Runbook Type"
    Me.colRunbookType.Name = "colRunbookType"
    Me.colRunbookType.ReadOnly = True
    '
    'colRunningRunbooks2
    '
    Me.colRunningRunbooks2.HeaderText = "Running Runbooks"
    Me.colRunningRunbooks2.Name = "colRunningRunbooks2"
    Me.colRunningRunbooks2.ReadOnly = True
    '
    'colEnabledRunbooks
    '
    Me.colEnabledRunbooks.HeaderText = "Enabled Runbooks"
    Me.colEnabledRunbooks.Name = "colEnabledRunbooks"
    Me.colEnabledRunbooks.ReadOnly = True
    '
    'colDisabledRunbooks
    '
    Me.colDisabledRunbooks.HeaderText = "Disabled Runbooks"
    Me.colDisabledRunbooks.Name = "colDisabledRunbooks"
    Me.colDisabledRunbooks.ReadOnly = True
    '
    'colTotalRunbooks
    '
    Me.colTotalRunbooks.HeaderText = "Total Runbooks"
    Me.colTotalRunbooks.Name = "colTotalRunbooks"
    Me.colTotalRunbooks.ReadOnly = True
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.ForeColor = System.Drawing.Color.Maroon
    Me.cmdRefresh.Image = Global.Orchestrator_Health_Checker.My.Resources.Resources.refresh
    Me.cmdRefresh.Location = New System.Drawing.Point(15, 401)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(75, 39)
    Me.cmdRefresh.TabIndex = 5
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'lblLastUpdate
    '
    Me.lblLastUpdate.AutoSize = True
    Me.lblLastUpdate.Location = New System.Drawing.Point(96, 414)
    Me.lblLastUpdate.Name = "lblLastUpdate"
    Me.lblLastUpdate.Size = New System.Drawing.Size(178, 13)
    Me.lblLastUpdate.TabIndex = 6
    Me.lblLastUpdate.Text = "Updated At: MM/dd/yyyy HH:mm:ss"
    '
    'KeyboardShortcuts
    '
    Me.KeyboardShortcuts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem})
    Me.KeyboardShortcuts.Location = New System.Drawing.Point(0, 0)
    Me.KeyboardShortcuts.Name = "KeyboardShortcuts"
    Me.KeyboardShortcuts.Size = New System.Drawing.Size(612, 24)
    Me.KeyboardShortcuts.TabIndex = 7
    Me.KeyboardShortcuts.Text = "MenuStrip1"
    Me.KeyboardShortcuts.Visible = False
    '
    'RefreshToolStripMenuItem
    '
    Me.RefreshToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem1, Me.MonitorToolStripMenuItem})
    Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
    Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
    Me.RefreshToolStripMenuItem.Text = "Actions"
    '
    'RefreshToolStripMenuItem1
    '
    Me.RefreshToolStripMenuItem1.Name = "RefreshToolStripMenuItem1"
    Me.RefreshToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F5
    Me.RefreshToolStripMenuItem1.Size = New System.Drawing.Size(136, 22)
    Me.RefreshToolStripMenuItem1.Text = "Refresh"
    '
    'MonitorToolStripMenuItem
    '
    Me.MonitorToolStripMenuItem.Name = "MonitorToolStripMenuItem"
    Me.MonitorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8
    Me.MonitorToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
    Me.MonitorToolStripMenuItem.Text = "Monitor"
    '
    'cmdMonitorRunbooksDetails
    '
    Me.cmdMonitorRunbooksDetails.AutoSize = True
    Me.cmdMonitorRunbooksDetails.Location = New System.Drawing.Point(456, 385)
    Me.cmdMonitorRunbooksDetails.Name = "cmdMonitorRunbooksDetails"
    Me.cmdMonitorRunbooksDetails.Size = New System.Drawing.Size(144, 13)
    Me.cmdMonitorRunbooksDetails.TabIndex = 8
    Me.cmdMonitorRunbooksDetails.TabStop = True
    Me.cmdMonitorRunbooksDetails.Text = "Monitor Runbooks Execution"
    '
    'dlgRunningStatistics
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(612, 452)
    Me.Controls.Add(Me.KeyboardShortcuts)
    Me.Controls.Add(Me.cmdMonitorRunbooksDetails)
    Me.Controls.Add(Me.lblLastUpdate)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.gridStatsType)
    Me.Controls.Add(Me.gridStatsByServer)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MainMenuStrip = Me.KeyboardShortcuts
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgRunningStatistics"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Running Statistics"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridStatsByServer, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.gridStatsType, System.ComponentModel.ISupportInitialize).EndInit()
    Me.KeyboardShortcuts.ResumeLayout(False)
    Me.KeyboardShortcuts.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents gridStatsByServer As System.Windows.Forms.DataGridView
  Friend WithEvents gridStatsType As System.Windows.Forms.DataGridView
  Friend WithEvents colRunbookServer As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunningRunbooks As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colMaxRunningPolicies As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colUsagePercent As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents lblLastUpdate As System.Windows.Forms.Label
  Friend WithEvents colRunbookType As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunningRunbooks2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colEnabledRunbooks As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colDisabledRunbooks As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colTotalRunbooks As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents KeyboardShortcuts As System.Windows.Forms.MenuStrip
  Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RefreshToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents cmdMonitorRunbooksDetails As System.Windows.Forms.LinkLabel
  Friend WithEvents MonitorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
