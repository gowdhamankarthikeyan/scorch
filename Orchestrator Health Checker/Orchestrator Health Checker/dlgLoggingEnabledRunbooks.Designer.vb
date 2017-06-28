<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgLoggingEnabledRunbooks
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
    Me.gridLoggingEnabledRunbooks = New System.Windows.Forms.DataGridView()
    Me.colRunbookName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colSpecificData = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colCommonData = New System.Windows.Forms.DataGridViewTextBoxColumn()
    CType(Me.gridLoggingEnabledRunbooks, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.OK_Button.Location = New System.Drawing.Point(666, 396)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'gridLoggingEnabledRunbooks
    '
    Me.gridLoggingEnabledRunbooks.AllowUserToAddRows = False
    Me.gridLoggingEnabledRunbooks.AllowUserToDeleteRows = False
    Me.gridLoggingEnabledRunbooks.AllowUserToOrderColumns = True
    Me.gridLoggingEnabledRunbooks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridLoggingEnabledRunbooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridLoggingEnabledRunbooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridLoggingEnabledRunbooks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRunbookName, Me.colPath, Me.colSpecificData, Me.colCommonData})
    Me.gridLoggingEnabledRunbooks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridLoggingEnabledRunbooks.Location = New System.Drawing.Point(12, 12)
    Me.gridLoggingEnabledRunbooks.Name = "gridLoggingEnabledRunbooks"
    Me.gridLoggingEnabledRunbooks.ReadOnly = True
    Me.gridLoggingEnabledRunbooks.RowHeadersVisible = False
    Me.gridLoggingEnabledRunbooks.Size = New System.Drawing.Size(721, 365)
    Me.gridLoggingEnabledRunbooks.TabIndex = 1
    '
    'colRunbookName
    '
    Me.colRunbookName.DataPropertyName = "Name"
    Me.colRunbookName.HeaderText = "Runbook"
    Me.colRunbookName.MinimumWidth = 150
    Me.colRunbookName.Name = "colRunbookName"
    Me.colRunbookName.ReadOnly = True
    '
    'colPath
    '
    Me.colPath.DataPropertyName = "Path"
    Me.colPath.HeaderText = "Path"
    Me.colPath.Name = "colPath"
    Me.colPath.ReadOnly = True
    '
    'colSpecificData
    '
    Me.colSpecificData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
    Me.colSpecificData.DataPropertyName = "LogSpecificData"
    Me.colSpecificData.FillWeight = 75.0!
    Me.colSpecificData.HeaderText = "Logging Specific Data"
    Me.colSpecificData.MinimumWidth = 75
    Me.colSpecificData.Name = "colSpecificData"
    Me.colSpecificData.ReadOnly = True
    Me.colSpecificData.Width = 75
    '
    'colCommonData
    '
    Me.colCommonData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
    Me.colCommonData.DataPropertyName = "LogCommonData"
    Me.colCommonData.FillWeight = 75.0!
    Me.colCommonData.HeaderText = "Logging Common Data"
    Me.colCommonData.MinimumWidth = 75
    Me.colCommonData.Name = "colCommonData"
    Me.colCommonData.ReadOnly = True
    Me.colCommonData.Width = 75
    '
    'dlgLoggingEnabledRunbooks
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(745, 435)
    Me.Controls.Add(Me.OK_Button)
    Me.Controls.Add(Me.gridLoggingEnabledRunbooks)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgLoggingEnabledRunbooks"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Logging Enabled Runbooks"
    CType(Me.gridLoggingEnabledRunbooks, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridLoggingEnabledRunbooks As System.Windows.Forms.DataGridView
  Friend WithEvents colRunbookName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colPath As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colSpecificData As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colCommonData As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
