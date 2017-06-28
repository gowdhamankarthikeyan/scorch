<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSearchObject
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
    Me.chkActivityType = New System.Windows.Forms.CheckedListBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txtObjectName = New System.Windows.Forms.TextBox()
    Me.cmdSearch = New System.Windows.Forms.Button()
    Me.gridResult = New System.Windows.Forms.DataGridView()
    Me.colObjectType = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunbookName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunbookPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colActivityName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.lnkSelectAll = New System.Windows.Forms.LinkLabel()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.lnkSelectNone = New System.Windows.Forms.LinkLabel()
    Me.lnkSelectInvert = New System.Windows.Forms.LinkLabel()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.chkActivities = New System.Windows.Forms.CheckBox()
    Me.chkRunbooks = New System.Windows.Forms.CheckBox()
    Me.chkFolders = New System.Windows.Forms.CheckBox()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridResult, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(739, 395)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(79, 29)
    Me.TableLayoutPanel1.TabIndex = 10
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(6, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Close"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 54)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(71, 13)
    Me.Label1.TabIndex = 11
    Me.Label1.Text = "Activity Type:"
    '
    'chkActivityType
    '
    Me.chkActivityType.FormattingEnabled = True
    Me.chkActivityType.Location = New System.Drawing.Point(89, 13)
    Me.chkActivityType.Name = "chkActivityType"
    Me.chkActivityType.Size = New System.Drawing.Size(277, 94)
    Me.chkActivityType.TabIndex = 0
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(419, 41)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(72, 13)
    Me.Label2.TabIndex = 13
    Me.Label2.Text = "Object Name:"
    '
    'txtObjectName
    '
    Me.txtObjectName.Location = New System.Drawing.Point(500, 37)
    Me.txtObjectName.Name = "txtObjectName"
    Me.txtObjectName.Size = New System.Drawing.Size(229, 20)
    Me.txtObjectName.TabIndex = 4
    '
    'cmdSearch
    '
    Me.cmdSearch.Location = New System.Drawing.Point(737, 36)
    Me.cmdSearch.Name = "cmdSearch"
    Me.cmdSearch.Size = New System.Drawing.Size(75, 23)
    Me.cmdSearch.TabIndex = 8
    Me.cmdSearch.Text = "Search"
    Me.cmdSearch.UseVisualStyleBackColor = True
    '
    'gridResult
    '
    Me.gridResult.AllowUserToAddRows = False
    Me.gridResult.AllowUserToDeleteRows = False
    Me.gridResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridResult.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colObjectType, Me.colRunbookName, Me.colRunbookPath, Me.colActivityName})
    Me.gridResult.Location = New System.Drawing.Point(13, 130)
    Me.gridResult.Name = "gridResult"
    Me.gridResult.ReadOnly = True
    Me.gridResult.RowHeadersVisible = False
    Me.gridResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridResult.Size = New System.Drawing.Size(799, 242)
    Me.gridResult.TabIndex = 9
    '
    'colObjectType
    '
    Me.colObjectType.DataPropertyName = "ObjectType"
    Me.colObjectType.HeaderText = "Type"
    Me.colObjectType.Name = "colObjectType"
    Me.colObjectType.ReadOnly = True
    '
    'colRunbookName
    '
    Me.colRunbookName.DataPropertyName = "Runbook"
    Me.colRunbookName.HeaderText = "Runbook Name"
    Me.colRunbookName.Name = "colRunbookName"
    Me.colRunbookName.ReadOnly = True
    '
    'colRunbookPath
    '
    Me.colRunbookPath.DataPropertyName = "Path"
    Me.colRunbookPath.HeaderText = "Object Path"
    Me.colRunbookPath.Name = "colRunbookPath"
    Me.colRunbookPath.ReadOnly = True
    '
    'colActivityName
    '
    Me.colActivityName.DataPropertyName = "Activity"
    Me.colActivityName.HeaderText = "Activity Name"
    Me.colActivityName.Name = "colActivityName"
    Me.colActivityName.ReadOnly = True
    '
    'lnkSelectAll
    '
    Me.lnkSelectAll.AutoSize = True
    Me.lnkSelectAll.Location = New System.Drawing.Point(262, 109)
    Me.lnkSelectAll.Name = "lnkSelectAll"
    Me.lnkSelectAll.Size = New System.Drawing.Size(18, 13)
    Me.lnkSelectAll.TabIndex = 1
    Me.lnkSelectAll.TabStop = True
    Me.lnkSelectAll.Text = "All"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(216, 109)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(40, 13)
    Me.Label3.TabIndex = 12
    Me.Label3.Text = "Select:"
    '
    'lnkSelectNone
    '
    Me.lnkSelectNone.AutoSize = True
    Me.lnkSelectNone.Location = New System.Drawing.Point(289, 109)
    Me.lnkSelectNone.Name = "lnkSelectNone"
    Me.lnkSelectNone.Size = New System.Drawing.Size(33, 13)
    Me.lnkSelectNone.TabIndex = 2
    Me.lnkSelectNone.TabStop = True
    Me.lnkSelectNone.Text = "None"
    '
    'lnkSelectInvert
    '
    Me.lnkSelectInvert.AutoSize = True
    Me.lnkSelectInvert.Location = New System.Drawing.Point(331, 109)
    Me.lnkSelectInvert.Name = "lnkSelectInvert"
    Me.lnkSelectInvert.Size = New System.Drawing.Size(34, 13)
    Me.lnkSelectInvert.TabIndex = 3
    Me.lnkSelectInvert.TabStop = True
    Me.lnkSelectInvert.Text = "Invert"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(429, 69)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(62, 13)
    Me.Label4.TabIndex = 14
    Me.Label4.Text = "Search For:"
    '
    'chkActivities
    '
    Me.chkActivities.AutoSize = True
    Me.chkActivities.Checked = True
    Me.chkActivities.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkActivities.Location = New System.Drawing.Point(506, 67)
    Me.chkActivities.Name = "chkActivities"
    Me.chkActivities.Size = New System.Drawing.Size(68, 17)
    Me.chkActivities.TabIndex = 5
    Me.chkActivities.Text = "Activities"
    Me.chkActivities.UseVisualStyleBackColor = True
    '
    'chkRunbooks
    '
    Me.chkRunbooks.AutoSize = True
    Me.chkRunbooks.Checked = True
    Me.chkRunbooks.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkRunbooks.Location = New System.Drawing.Point(589, 67)
    Me.chkRunbooks.Name = "chkRunbooks"
    Me.chkRunbooks.Size = New System.Drawing.Size(75, 17)
    Me.chkRunbooks.TabIndex = 6
    Me.chkRunbooks.Text = "Runbooks"
    Me.chkRunbooks.UseVisualStyleBackColor = True
    '
    'chkFolders
    '
    Me.chkFolders.AutoSize = True
    Me.chkFolders.Location = New System.Drawing.Point(679, 67)
    Me.chkFolders.Name = "chkFolders"
    Me.chkFolders.Size = New System.Drawing.Size(60, 17)
    Me.chkFolders.TabIndex = 7
    Me.chkFolders.Text = "Folders"
    Me.chkFolders.UseVisualStyleBackColor = True
    '
    'dlgSearchObject
    '
    Me.AcceptButton = Me.cmdSearch
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(830, 436)
    Me.Controls.Add(Me.chkFolders)
    Me.Controls.Add(Me.chkRunbooks)
    Me.Controls.Add(Me.chkActivities)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.lnkSelectInvert)
    Me.Controls.Add(Me.lnkSelectNone)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.lnkSelectAll)
    Me.Controls.Add(Me.gridResult)
    Me.Controls.Add(Me.cmdSearch)
    Me.Controls.Add(Me.txtObjectName)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.chkActivityType)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgSearchObject"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Search Runbook by Activity"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridResult, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chkActivityType As System.Windows.Forms.CheckedListBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtObjectName As System.Windows.Forms.TextBox
  Friend WithEvents cmdSearch As System.Windows.Forms.Button
  Friend WithEvents gridResult As System.Windows.Forms.DataGridView
  Friend WithEvents lnkSelectAll As System.Windows.Forms.LinkLabel
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lnkSelectNone As System.Windows.Forms.LinkLabel
  Friend WithEvents lnkSelectInvert As System.Windows.Forms.LinkLabel
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents chkActivities As System.Windows.Forms.CheckBox
  Friend WithEvents chkRunbooks As System.Windows.Forms.CheckBox
  Friend WithEvents chkFolders As System.Windows.Forms.CheckBox
  Friend WithEvents colObjectType As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunbookName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunbookPath As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colActivityName As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
