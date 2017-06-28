<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSearchVariableUsage
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
    Me.cmbVariables = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.cmdSearch = New System.Windows.Forms.Button()
    Me.gridResults = New System.Windows.Forms.DataGridView()
    Me.colRunbookPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colActivity = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colActivityType = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.workerSearch = New System.ComponentModel.BackgroundWorker()
    CType(Me.gridResults, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(680, 451)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Close"
    '
    'cmbVariables
    '
    Me.cmbVariables.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.cmbVariables.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.cmbVariables.DisplayMember = "VariableName"
    Me.cmbVariables.FormattingEnabled = True
    Me.cmbVariables.Location = New System.Drawing.Point(67, 9)
    Me.cmbVariables.Name = "cmbVariables"
    Me.cmbVariables.Size = New System.Drawing.Size(396, 21)
    Me.cmbVariables.TabIndex = 1
    Me.cmbVariables.ValueMember = "VariableID"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(48, 13)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "Variable:"
    '
    'cmdSearch
    '
    Me.cmdSearch.Location = New System.Drawing.Point(472, 8)
    Me.cmdSearch.Name = "cmdSearch"
    Me.cmdSearch.Size = New System.Drawing.Size(73, 23)
    Me.cmdSearch.TabIndex = 3
    Me.cmdSearch.Text = "Search"
    Me.cmdSearch.UseVisualStyleBackColor = True
    '
    'gridResults
    '
    Me.gridResults.AllowUserToAddRows = False
    Me.gridResults.AllowUserToDeleteRows = False
    Me.gridResults.AllowUserToOrderColumns = True
    Me.gridResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRunbookPath, Me.colActivity, Me.colActivityType})
    Me.gridResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridResults.Location = New System.Drawing.Point(12, 36)
    Me.gridResults.Name = "gridResults"
    Me.gridResults.ReadOnly = True
    Me.gridResults.RowHeadersVisible = False
    Me.gridResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridResults.Size = New System.Drawing.Size(735, 400)
    Me.gridResults.TabIndex = 4
    '
    'colRunbookPath
    '
    Me.colRunbookPath.DataPropertyName = "Runbook"
    Me.colRunbookPath.HeaderText = "Runbook Path"
    Me.colRunbookPath.Name = "colRunbookPath"
    Me.colRunbookPath.ReadOnly = True
    '
    'colActivity
    '
    Me.colActivity.DataPropertyName = "Activity"
    Me.colActivity.HeaderText = "Activity"
    Me.colActivity.Name = "colActivity"
    Me.colActivity.ReadOnly = True
    '
    'colActivityType
    '
    Me.colActivityType.DataPropertyName = "Activity Type"
    Me.colActivityType.HeaderText = "Activity Type"
    Me.colActivityType.Name = "colActivityType"
    Me.colActivityType.ReadOnly = True
    '
    'workerSearch
    '
    '
    'dlgSearchVariableUsage
    '
    Me.AcceptButton = Me.cmdSearch
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(759, 486)
    Me.Controls.Add(Me.gridResults)
    Me.Controls.Add(Me.cmdSearch)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cmbVariables)
    Me.Controls.Add(Me.OK_Button)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgSearchVariableUsage"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Audit Variable On Activities/Runbooks"
    CType(Me.gridResults, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents cmbVariables As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents cmdSearch As System.Windows.Forms.Button
  Friend WithEvents gridResults As System.Windows.Forms.DataGridView
  Friend WithEvents colRunbookPath As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colActivity As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colActivityType As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents workerSearch As System.ComponentModel.BackgroundWorker

End Class
