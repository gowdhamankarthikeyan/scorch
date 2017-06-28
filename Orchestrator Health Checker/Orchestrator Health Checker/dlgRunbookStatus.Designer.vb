<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgRunbookStatus
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
    Me.gridRunningOn = New System.Windows.Forms.DataGridView()
    Me.colRunbookServer = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunningInstances = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.gridProcessIDs = New System.Windows.Forms.DataGridView()
    Me.colServer = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colProcessID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colTimeStarted = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.lblRunbookName = New System.Windows.Forms.Label()
    Me.lblRunbookPath = New System.Windows.Forms.Label()
    Me.txtPath = New System.Windows.Forms.TextBox()
    Me.lblLastUpdate = New System.Windows.Forms.Label()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.cmdAnalyzeOrphans = New System.Windows.Forms.Button()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridRunningOn, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.gridProcessIDs, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(293, 479)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(70, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(64, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 61)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(67, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Running On:"
    '
    'gridRunningOn
    '
    Me.gridRunningOn.AllowUserToAddRows = False
    Me.gridRunningOn.AllowUserToDeleteRows = False
    Me.gridRunningOn.AllowUserToOrderColumns = True
    Me.gridRunningOn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridRunningOn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridRunningOn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRunbookServer, Me.colRunningInstances})
    Me.gridRunningOn.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridRunningOn.Location = New System.Drawing.Point(13, 78)
    Me.gridRunningOn.Name = "gridRunningOn"
    Me.gridRunningOn.ReadOnly = True
    Me.gridRunningOn.RowHeadersVisible = False
    Me.gridRunningOn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridRunningOn.Size = New System.Drawing.Size(350, 150)
    Me.gridRunningOn.TabIndex = 2
    '
    'colRunbookServer
    '
    Me.colRunbookServer.DataPropertyName = "RunbookServer"
    Me.colRunbookServer.HeaderText = "Runbook Server"
    Me.colRunbookServer.Name = "colRunbookServer"
    Me.colRunbookServer.ReadOnly = True
    '
    'colRunningInstances
    '
    Me.colRunningInstances.DataPropertyName = "Count"
    Me.colRunningInstances.HeaderText = "Running Instances"
    Me.colRunningInstances.Name = "colRunningInstances"
    Me.colRunningInstances.ReadOnly = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(13, 254)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(67, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Process IDs:"
    '
    'gridProcessIDs
    '
    Me.gridProcessIDs.AllowUserToAddRows = False
    Me.gridProcessIDs.AllowUserToDeleteRows = False
    Me.gridProcessIDs.AllowUserToOrderColumns = True
    Me.gridProcessIDs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridProcessIDs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridProcessIDs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colServer, Me.colProcessID, Me.colTimeStarted})
    Me.gridProcessIDs.Location = New System.Drawing.Point(13, 271)
    Me.gridProcessIDs.Name = "gridProcessIDs"
    Me.gridProcessIDs.ReadOnly = True
    Me.gridProcessIDs.RowHeadersVisible = False
    Me.gridProcessIDs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridProcessIDs.Size = New System.Drawing.Size(350, 183)
    Me.gridProcessIDs.TabIndex = 4
    '
    'colServer
    '
    Me.colServer.DataPropertyName = "Computer"
    Me.colServer.HeaderText = "Runbook Server"
    Me.colServer.Name = "colServer"
    Me.colServer.ReadOnly = True
    '
    'colProcessID
    '
    Me.colProcessID.DataPropertyName = "ProcessID"
    Me.colProcessID.HeaderText = "Process ID"
    Me.colProcessID.Name = "colProcessID"
    Me.colProcessID.ReadOnly = True
    '
    'colTimeStarted
    '
    Me.colTimeStarted.DataPropertyName = "TimeStarted"
    Me.colTimeStarted.HeaderText = "Started At"
    Me.colTimeStarted.Name = "colTimeStarted"
    Me.colTimeStarted.ReadOnly = True
    '
    'lblRunbookName
    '
    Me.lblRunbookName.AutoSize = True
    Me.lblRunbookName.Location = New System.Drawing.Point(13, 9)
    Me.lblRunbookName.Name = "lblRunbookName"
    Me.lblRunbookName.Size = New System.Drawing.Size(85, 13)
    Me.lblRunbookName.TabIndex = 5
    Me.lblRunbookName.Text = "Runbook Name:"
    '
    'lblRunbookPath
    '
    Me.lblRunbookPath.AutoSize = True
    Me.lblRunbookPath.Location = New System.Drawing.Point(13, 35)
    Me.lblRunbookPath.Name = "lblRunbookPath"
    Me.lblRunbookPath.Size = New System.Drawing.Size(32, 13)
    Me.lblRunbookPath.TabIndex = 6
    Me.lblRunbookPath.Text = "Path:"
    '
    'txtPath
    '
    Me.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtPath.Location = New System.Drawing.Point(52, 35)
    Me.txtPath.Name = "txtPath"
    Me.txtPath.ReadOnly = True
    Me.txtPath.Size = New System.Drawing.Size(311, 13)
    Me.txtPath.TabIndex = 7
    '
    'lblLastUpdate
    '
    Me.lblLastUpdate.AutoSize = True
    Me.lblLastUpdate.Location = New System.Drawing.Point(13, 508)
    Me.lblLastUpdate.Name = "lblLastUpdate"
    Me.lblLastUpdate.Size = New System.Drawing.Size(39, 13)
    Me.lblLastUpdate.TabIndex = 9
    Me.lblLastUpdate.Text = "Label3"
    '
    'cmdRefresh
    '
    Me.cmdRefresh.ForeColor = System.Drawing.Color.Maroon
    Me.cmdRefresh.Image = Global.Orchestrator_Health_Checker.My.Resources.Resources.refresh
    Me.cmdRefresh.Location = New System.Drawing.Point(13, 461)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(75, 39)
    Me.cmdRefresh.TabIndex = 8
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'cmdAnalyzeOrphans
    '
    Me.cmdAnalyzeOrphans.Location = New System.Drawing.Point(122, 461)
    Me.cmdAnalyzeOrphans.Name = "cmdAnalyzeOrphans"
    Me.cmdAnalyzeOrphans.Size = New System.Drawing.Size(75, 39)
    Me.cmdAnalyzeOrphans.TabIndex = 10
    Me.cmdAnalyzeOrphans.Text = "Analyze Orphans"
    Me.cmdAnalyzeOrphans.UseVisualStyleBackColor = True
    '
    'dlgRunbookStatus
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(378, 528)
    Me.Controls.Add(Me.cmdAnalyzeOrphans)
    Me.Controls.Add(Me.lblLastUpdate)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.txtPath)
    Me.Controls.Add(Me.lblRunbookPath)
    Me.Controls.Add(Me.lblRunbookName)
    Me.Controls.Add(Me.gridProcessIDs)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.gridRunningOn)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgRunbookStatus"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "dlgRunbookStatus"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridRunningOn, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.gridProcessIDs, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents gridRunningOn As System.Windows.Forms.DataGridView
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents gridProcessIDs As System.Windows.Forms.DataGridView
  Friend WithEvents colRunbookServer As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunningInstances As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents lblRunbookName As System.Windows.Forms.Label
  Friend WithEvents lblRunbookPath As System.Windows.Forms.Label
  Friend WithEvents txtPath As System.Windows.Forms.TextBox
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents lblLastUpdate As System.Windows.Forms.Label
  Friend WithEvents colServer As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colProcessID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colTimeStarted As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents cmdAnalyzeOrphans As System.Windows.Forms.Button

End Class
