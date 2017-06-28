<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgClientConnections
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
    Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.gridClientConnections = New System.Windows.Forms.DataGridView()
    Me.colManagementServer = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colClientMachine = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colUserID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colClientVersion = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colConnectionTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colLastActivity = New System.Windows.Forms.DataGridViewTextBoxColumn()
    CType(Me.gridClientConnections, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(629, 317)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'gridClientConnections
    '
    Me.gridClientConnections.AllowUserToAddRows = False
    Me.gridClientConnections.AllowUserToDeleteRows = False
    Me.gridClientConnections.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridClientConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridClientConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridClientConnections.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colManagementServer, Me.colClientMachine, Me.colUserID, Me.colClientVersion, Me.colConnectionTime, Me.colLastActivity})
    Me.gridClientConnections.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridClientConnections.Location = New System.Drawing.Point(13, 13)
    Me.gridClientConnections.Name = "gridClientConnections"
    Me.gridClientConnections.ReadOnly = True
    Me.gridClientConnections.RowHeadersVisible = False
    Me.gridClientConnections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridClientConnections.Size = New System.Drawing.Size(683, 298)
    Me.gridClientConnections.TabIndex = 1
    '
    'colManagementServer
    '
    Me.colManagementServer.DataPropertyName = "ManagementServer"
    Me.colManagementServer.HeaderText = "Management Server"
    Me.colManagementServer.Name = "colManagementServer"
    Me.colManagementServer.ReadOnly = True
    '
    'colClientMachine
    '
    Me.colClientMachine.DataPropertyName = "ClientMachine"
    Me.colClientMachine.HeaderText = "Client Machine"
    Me.colClientMachine.Name = "colClientMachine"
    Me.colClientMachine.ReadOnly = True
    '
    'colUserID
    '
    Me.colUserID.DataPropertyName = "ClientUser"
    Me.colUserID.HeaderText = "User ID"
    Me.colUserID.Name = "colUserID"
    Me.colUserID.ReadOnly = True
    '
    'colClientVersion
    '
    Me.colClientVersion.DataPropertyName = "ClientVersion"
    DataGridViewCellStyle2.NullValue = "N/A"
    Me.colClientVersion.DefaultCellStyle = DataGridViewCellStyle2
    Me.colClientVersion.HeaderText = "Client Version"
    Me.colClientVersion.Name = "colClientVersion"
    Me.colClientVersion.ReadOnly = True
    '
    'colConnectionTime
    '
    Me.colConnectionTime.DataPropertyName = "ConnectionTime"
    Me.colConnectionTime.HeaderText = "First Connection Time"
    Me.colConnectionTime.Name = "colConnectionTime"
    Me.colConnectionTime.ReadOnly = True
    '
    'colLastActivity
    '
    Me.colLastActivity.DataPropertyName = "LastActivity"
    Me.colLastActivity.HeaderText = "Last Activity"
    Me.colLastActivity.Name = "colLastActivity"
    Me.colLastActivity.ReadOnly = True
    '
    'dlgClientConnections
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(708, 352)
    Me.Controls.Add(Me.gridClientConnections)
    Me.Controls.Add(Me.OK_Button)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgClientConnections"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Audit - Client Connections"
    CType(Me.gridClientConnections, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridClientConnections As System.Windows.Forms.DataGridView
  Friend WithEvents colManagementServer As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colClientMachine As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colUserID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colClientVersion As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colConnectionTime As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colLastActivity As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
