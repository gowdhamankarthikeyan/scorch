<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgShowOrphans
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
    Me.gridOrphans = New System.Windows.Forms.DataGridView()
    Me.workerLoadProcess = New System.ComponentModel.BackgroundWorker()
    Me.progressLoading = New System.Windows.Forms.ProgressBar()
    Me.lblLoading = New System.Windows.Forms.Label()
    Me.colKill = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.colRunbookServer = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colPID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colIsOrphan = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.cmdKillSelected = New System.Windows.Forms.Button()
    CType(Me.gridOrphans, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(356, 277)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Close"
    '
    'gridOrphans
    '
    Me.gridOrphans.AllowUserToAddRows = False
    Me.gridOrphans.AllowUserToDeleteRows = False
    Me.gridOrphans.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridOrphans.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridOrphans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridOrphans.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colKill, Me.colRunbookServer, Me.colPID, Me.colIsOrphan})
    Me.gridOrphans.Enabled = False
    Me.gridOrphans.Location = New System.Drawing.Point(12, 12)
    Me.gridOrphans.Name = "gridOrphans"
    Me.gridOrphans.RowHeadersVisible = False
    Me.gridOrphans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridOrphans.Size = New System.Drawing.Size(411, 256)
    Me.gridOrphans.TabIndex = 1
    '
    'workerLoadProcess
    '
    '
    'progressLoading
    '
    Me.progressLoading.Location = New System.Drawing.Point(12, 277)
    Me.progressLoading.Name = "progressLoading"
    Me.progressLoading.Size = New System.Drawing.Size(100, 23)
    Me.progressLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
    Me.progressLoading.TabIndex = 2
    Me.progressLoading.Value = 25
    '
    'lblLoading
    '
    Me.lblLoading.AutoSize = True
    Me.lblLoading.Location = New System.Drawing.Point(119, 282)
    Me.lblLoading.Name = "lblLoading"
    Me.lblLoading.Size = New System.Drawing.Size(54, 13)
    Me.lblLoading.TabIndex = 3
    Me.lblLoading.Text = "Loading..."
    '
    'colKill
    '
    Me.colKill.FillWeight = 40.0!
    Me.colKill.HeaderText = "Kill"
    Me.colKill.Name = "colKill"
    '
    'colRunbookServer
    '
    Me.colRunbookServer.HeaderText = "Runbook Server"
    Me.colRunbookServer.Name = "colRunbookServer"
    Me.colRunbookServer.ReadOnly = True
    '
    'colPID
    '
    Me.colPID.HeaderText = "PID"
    Me.colPID.Name = "colPID"
    Me.colPID.ReadOnly = True
    '
    'colIsOrphan
    '
    Me.colIsOrphan.HeaderText = "Is Orphan?"
    Me.colIsOrphan.Name = "colIsOrphan"
    Me.colIsOrphan.ReadOnly = True
    '
    'cmdKillSelected
    '
    Me.cmdKillSelected.Enabled = False
    Me.cmdKillSelected.Location = New System.Drawing.Point(12, 277)
    Me.cmdKillSelected.Name = "cmdKillSelected"
    Me.cmdKillSelected.Size = New System.Drawing.Size(75, 23)
    Me.cmdKillSelected.TabIndex = 4
    Me.cmdKillSelected.Text = "Kill Selected"
    Me.cmdKillSelected.UseVisualStyleBackColor = True
    Me.cmdKillSelected.Visible = False
    '
    'dlgShowOrphans
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(435, 315)
    Me.Controls.Add(Me.cmdKillSelected)
    Me.Controls.Add(Me.OK_Button)
    Me.Controls.Add(Me.lblLoading)
    Me.Controls.Add(Me.progressLoading)
    Me.Controls.Add(Me.gridOrphans)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgShowOrphans"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Showing Orphans"
    CType(Me.gridOrphans, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridOrphans As System.Windows.Forms.DataGridView
  Friend WithEvents workerLoadProcess As System.ComponentModel.BackgroundWorker
  Friend WithEvents progressLoading As System.Windows.Forms.ProgressBar
  Friend WithEvents lblLoading As System.Windows.Forms.Label
  Friend WithEvents colKill As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents colRunbookServer As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colPID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colIsOrphan As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents cmdKillSelected As System.Windows.Forms.Button

End Class
