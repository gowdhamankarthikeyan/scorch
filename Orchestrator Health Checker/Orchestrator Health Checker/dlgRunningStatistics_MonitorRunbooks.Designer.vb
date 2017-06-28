<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgRunningStatistics_MonitorRunbooks
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
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.gridMonitorRunbooks = New System.Windows.Forms.DataGridView()
    Me.colRunbook = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
    Me.ActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.lblLastUpdate = New System.Windows.Forms.Label()
    CType(Me.gridMonitorRunbooks, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.MenuStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(602, 455)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(82, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'gridMonitorRunbooks
    '
    Me.gridMonitorRunbooks.AllowUserToAddRows = False
    Me.gridMonitorRunbooks.AllowUserToDeleteRows = False
    Me.gridMonitorRunbooks.AllowUserToOrderColumns = True
    Me.gridMonitorRunbooks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridMonitorRunbooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridMonitorRunbooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridMonitorRunbooks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRunbook, Me.colPath, Me.colStatus})
    Me.gridMonitorRunbooks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridMonitorRunbooks.Location = New System.Drawing.Point(13, 13)
    Me.gridMonitorRunbooks.Name = "gridMonitorRunbooks"
    Me.gridMonitorRunbooks.ReadOnly = True
    Me.gridMonitorRunbooks.RowHeadersVisible = False
    Me.gridMonitorRunbooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridMonitorRunbooks.Size = New System.Drawing.Size(671, 436)
    Me.gridMonitorRunbooks.TabIndex = 1
    '
    'colRunbook
    '
    Me.colRunbook.DataPropertyName = "Name"
    Me.colRunbook.HeaderText = "Runbook"
    Me.colRunbook.Name = "colRunbook"
    Me.colRunbook.ReadOnly = True
    '
    'colPath
    '
    Me.colPath.DataPropertyName = "Path"
    Me.colPath.HeaderText = "Path"
    Me.colPath.Name = "colPath"
    Me.colPath.ReadOnly = True
    '
    'colStatus
    '
    Me.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
    Me.colStatus.DataPropertyName = "RunbookStatus"
    Me.colStatus.HeaderText = "Status"
    Me.colStatus.Name = "colStatus"
    Me.colStatus.ReadOnly = True
    Me.colStatus.Width = 62
    '
    'MenuStrip1
    '
    Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActionsToolStripMenuItem})
    Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip1.Name = "MenuStrip1"
    Me.MenuStrip1.Size = New System.Drawing.Size(659, 24)
    Me.MenuStrip1.TabIndex = 2
    Me.MenuStrip1.Text = "MenuStrip1"
    Me.MenuStrip1.Visible = False
    '
    'ActionsToolStripMenuItem
    '
    Me.ActionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem})
    Me.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem"
    Me.ActionsToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
    Me.ActionsToolStripMenuItem.Text = "Actions"
    '
    'RefreshToolStripMenuItem
    '
    Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
    Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
    Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
    Me.RefreshToolStripMenuItem.Text = "Refresh"
    '
    'lblLastUpdate
    '
    Me.lblLastUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblLastUpdate.AutoSize = True
    Me.lblLastUpdate.Location = New System.Drawing.Point(15, 460)
    Me.lblLastUpdate.Name = "lblLastUpdate"
    Me.lblLastUpdate.Size = New System.Drawing.Size(178, 13)
    Me.lblLastUpdate.TabIndex = 7
    Me.lblLastUpdate.Text = "Updated At: MM/dd/yyyy HH:mm:ss"
    '
    'dlgRunningStatistics_MonitorRunbooks
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(696, 490)
    Me.Controls.Add(Me.lblLastUpdate)
    Me.Controls.Add(Me.gridMonitorRunbooks)
    Me.Controls.Add(Me.OK_Button)
    Me.Controls.Add(Me.MenuStrip1)
    Me.MainMenuStrip = Me.MenuStrip1
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgRunningStatistics_MonitorRunbooks"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Monitor Runbooks Execution"
    CType(Me.gridMonitorRunbooks, System.ComponentModel.ISupportInitialize).EndInit()
    Me.MenuStrip1.ResumeLayout(False)
    Me.MenuStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridMonitorRunbooks As System.Windows.Forms.DataGridView
  Friend WithEvents colRunbook As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colPath As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
  Friend WithEvents ActionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents lblLastUpdate As System.Windows.Forms.Label

End Class
