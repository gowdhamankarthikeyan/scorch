<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgRunbookAudit
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
    Me.dtpAuditFrom = New System.Windows.Forms.DateTimePicker()
    Me.cmdShow = New System.Windows.Forms.Button()
    Me.gridRunbooksCreatedSince = New System.Windows.Forms.DataGridView()
    Me.UniqueID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colType = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunbook = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colUserID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.gridRunbooksModifiedSince = New System.Windows.Forms.DataGridView()
    Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.gridRunbooksCheckedOut = New System.Windows.Forms.DataGridView()
    Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.objTabs = New System.Windows.Forms.TabControl()
    Me.tabCreated = New System.Windows.Forms.TabPage()
    Me.tabModified = New System.Windows.Forms.TabPage()
    Me.tabCheckedOut = New System.Windows.Forms.TabPage()
    Me.tabDeleted = New System.Windows.Forms.TabPage()
    Me.gridRunbooksDeletedAfter = New System.Windows.Forms.DataGridView()
    Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.lblShowingInformationSince = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridRunbooksCreatedSince, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.gridRunbooksModifiedSince, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.gridRunbooksCheckedOut, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.objTabs.SuspendLayout()
    Me.tabCreated.SuspendLayout()
    Me.tabModified.SuspendLayout()
    Me.tabCheckedOut.SuspendLayout()
    Me.tabDeleted.SuspendLayout()
    CType(Me.gridRunbooksDeletedAfter, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(722, 486)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(80, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(6, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(72, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Activity since:"
    '
    'dtpAuditFrom
    '
    Me.dtpAuditFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.dtpAuditFrom.Location = New System.Drawing.Point(91, 9)
    Me.dtpAuditFrom.Name = "dtpAuditFrom"
    Me.dtpAuditFrom.Size = New System.Drawing.Size(106, 20)
    Me.dtpAuditFrom.TabIndex = 2
    '
    'cmdShow
    '
    Me.cmdShow.Location = New System.Drawing.Point(203, 8)
    Me.cmdShow.Name = "cmdShow"
    Me.cmdShow.Size = New System.Drawing.Size(75, 23)
    Me.cmdShow.TabIndex = 3
    Me.cmdShow.Text = "Show"
    Me.cmdShow.UseVisualStyleBackColor = True
    '
    'gridRunbooksCreatedSince
    '
    Me.gridRunbooksCreatedSince.AllowUserToAddRows = False
    Me.gridRunbooksCreatedSince.AllowUserToDeleteRows = False
    Me.gridRunbooksCreatedSince.AllowUserToResizeRows = False
    Me.gridRunbooksCreatedSince.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridRunbooksCreatedSince.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridRunbooksCreatedSince.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UniqueID, Me.colType, Me.colRunbook, Me.colPath, Me.colDate, Me.colUserID})
    Me.gridRunbooksCreatedSince.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gridRunbooksCreatedSince.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridRunbooksCreatedSince.Location = New System.Drawing.Point(3, 3)
    Me.gridRunbooksCreatedSince.Name = "gridRunbooksCreatedSince"
    Me.gridRunbooksCreatedSince.ReadOnly = True
    Me.gridRunbooksCreatedSince.RowHeadersVisible = False
    Me.gridRunbooksCreatedSince.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridRunbooksCreatedSince.Size = New System.Drawing.Size(775, 397)
    Me.gridRunbooksCreatedSince.TabIndex = 5
    '
    'UniqueID
    '
    Me.UniqueID.DataPropertyName = "Id"
    Me.UniqueID.HeaderText = "UniqueID"
    Me.UniqueID.Name = "UniqueID"
    Me.UniqueID.ReadOnly = True
    Me.UniqueID.Visible = False
    '
    'colType
    '
    Me.colType.DataPropertyName = "Type"
    Me.colType.HeaderText = "Type"
    Me.colType.Name = "colType"
    Me.colType.ReadOnly = True
    Me.colType.Visible = False
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
    'colDate
    '
    Me.colDate.DataPropertyName = "DateTime"
    Me.colDate.HeaderText = "Date"
    Me.colDate.Name = "colDate"
    Me.colDate.ReadOnly = True
    '
    'colUserID
    '
    Me.colUserID.DataPropertyName = "By"
    Me.colUserID.HeaderText = "By"
    Me.colUserID.Name = "colUserID"
    Me.colUserID.ReadOnly = True
    '
    'gridRunbooksModifiedSince
    '
    Me.gridRunbooksModifiedSince.AllowUserToAddRows = False
    Me.gridRunbooksModifiedSince.AllowUserToDeleteRows = False
    Me.gridRunbooksModifiedSince.AllowUserToResizeRows = False
    Me.gridRunbooksModifiedSince.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridRunbooksModifiedSince.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridRunbooksModifiedSince.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
    Me.gridRunbooksModifiedSince.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gridRunbooksModifiedSince.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridRunbooksModifiedSince.Location = New System.Drawing.Point(3, 3)
    Me.gridRunbooksModifiedSince.Name = "gridRunbooksModifiedSince"
    Me.gridRunbooksModifiedSince.ReadOnly = True
    Me.gridRunbooksModifiedSince.RowHeadersVisible = False
    Me.gridRunbooksModifiedSince.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridRunbooksModifiedSince.Size = New System.Drawing.Size(775, 397)
    Me.gridRunbooksModifiedSince.TabIndex = 7
    '
    'DataGridViewTextBoxColumn1
    '
    Me.DataGridViewTextBoxColumn1.DataPropertyName = "Id"
    Me.DataGridViewTextBoxColumn1.HeaderText = "UniqueID"
    Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
    Me.DataGridViewTextBoxColumn1.ReadOnly = True
    Me.DataGridViewTextBoxColumn1.Visible = False
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "Type"
    Me.Column1.HeaderText = "Column1"
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Visible = False
    '
    'DataGridViewTextBoxColumn2
    '
    Me.DataGridViewTextBoxColumn2.DataPropertyName = "Name"
    Me.DataGridViewTextBoxColumn2.HeaderText = "Runbook"
    Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
    Me.DataGridViewTextBoxColumn2.ReadOnly = True
    '
    'DataGridViewTextBoxColumn3
    '
    Me.DataGridViewTextBoxColumn3.DataPropertyName = "Path"
    Me.DataGridViewTextBoxColumn3.HeaderText = "Path"
    Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
    Me.DataGridViewTextBoxColumn3.ReadOnly = True
    '
    'DataGridViewTextBoxColumn4
    '
    Me.DataGridViewTextBoxColumn4.DataPropertyName = "DateTime"
    Me.DataGridViewTextBoxColumn4.HeaderText = "Date"
    Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
    Me.DataGridViewTextBoxColumn4.ReadOnly = True
    '
    'DataGridViewTextBoxColumn5
    '
    Me.DataGridViewTextBoxColumn5.DataPropertyName = "By"
    Me.DataGridViewTextBoxColumn5.HeaderText = "By"
    Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
    Me.DataGridViewTextBoxColumn5.ReadOnly = True
    '
    'gridRunbooksCheckedOut
    '
    Me.gridRunbooksCheckedOut.AllowUserToAddRows = False
    Me.gridRunbooksCheckedOut.AllowUserToDeleteRows = False
    Me.gridRunbooksCheckedOut.AllowUserToResizeRows = False
    Me.gridRunbooksCheckedOut.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridRunbooksCheckedOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridRunbooksCheckedOut.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.Column2, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10})
    Me.gridRunbooksCheckedOut.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gridRunbooksCheckedOut.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridRunbooksCheckedOut.Location = New System.Drawing.Point(3, 3)
    Me.gridRunbooksCheckedOut.Name = "gridRunbooksCheckedOut"
    Me.gridRunbooksCheckedOut.ReadOnly = True
    Me.gridRunbooksCheckedOut.RowHeadersVisible = False
    Me.gridRunbooksCheckedOut.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridRunbooksCheckedOut.Size = New System.Drawing.Size(775, 397)
    Me.gridRunbooksCheckedOut.TabIndex = 9
    '
    'DataGridViewTextBoxColumn6
    '
    Me.DataGridViewTextBoxColumn6.DataPropertyName = "Id"
    Me.DataGridViewTextBoxColumn6.HeaderText = "UniqueID"
    Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
    Me.DataGridViewTextBoxColumn6.ReadOnly = True
    Me.DataGridViewTextBoxColumn6.Visible = False
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "Type"
    Me.Column2.HeaderText = "Column2"
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Visible = False
    '
    'DataGridViewTextBoxColumn7
    '
    Me.DataGridViewTextBoxColumn7.DataPropertyName = "Name"
    Me.DataGridViewTextBoxColumn7.HeaderText = "Runbook"
    Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
    Me.DataGridViewTextBoxColumn7.ReadOnly = True
    '
    'DataGridViewTextBoxColumn8
    '
    Me.DataGridViewTextBoxColumn8.DataPropertyName = "Path"
    Me.DataGridViewTextBoxColumn8.HeaderText = "Path"
    Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
    Me.DataGridViewTextBoxColumn8.ReadOnly = True
    '
    'DataGridViewTextBoxColumn9
    '
    Me.DataGridViewTextBoxColumn9.DataPropertyName = "DateTime"
    Me.DataGridViewTextBoxColumn9.HeaderText = "Date"
    Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
    Me.DataGridViewTextBoxColumn9.ReadOnly = True
    '
    'DataGridViewTextBoxColumn10
    '
    Me.DataGridViewTextBoxColumn10.DataPropertyName = "By"
    Me.DataGridViewTextBoxColumn10.HeaderText = "By"
    Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
    Me.DataGridViewTextBoxColumn10.ReadOnly = True
    '
    'objTabs
    '
    Me.objTabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.objTabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
    Me.objTabs.Controls.Add(Me.tabCreated)
    Me.objTabs.Controls.Add(Me.tabModified)
    Me.objTabs.Controls.Add(Me.tabCheckedOut)
    Me.objTabs.Controls.Add(Me.tabDeleted)
    Me.objTabs.Location = New System.Drawing.Point(13, 37)
    Me.objTabs.Name = "objTabs"
    Me.objTabs.SelectedIndex = 0
    Me.objTabs.Size = New System.Drawing.Size(789, 432)
    Me.objTabs.TabIndex = 10
    '
    'tabCreated
    '
    Me.tabCreated.Controls.Add(Me.gridRunbooksCreatedSince)
    Me.tabCreated.Location = New System.Drawing.Point(4, 25)
    Me.tabCreated.Name = "tabCreated"
    Me.tabCreated.Padding = New System.Windows.Forms.Padding(3)
    Me.tabCreated.Size = New System.Drawing.Size(781, 403)
    Me.tabCreated.TabIndex = 0
    Me.tabCreated.Text = "Created"
    Me.tabCreated.UseVisualStyleBackColor = True
    '
    'tabModified
    '
    Me.tabModified.Controls.Add(Me.gridRunbooksModifiedSince)
    Me.tabModified.Location = New System.Drawing.Point(4, 25)
    Me.tabModified.Name = "tabModified"
    Me.tabModified.Padding = New System.Windows.Forms.Padding(3)
    Me.tabModified.Size = New System.Drawing.Size(781, 403)
    Me.tabModified.TabIndex = 1
    Me.tabModified.Text = "Modified"
    Me.tabModified.UseVisualStyleBackColor = True
    '
    'tabCheckedOut
    '
    Me.tabCheckedOut.Controls.Add(Me.gridRunbooksCheckedOut)
    Me.tabCheckedOut.Location = New System.Drawing.Point(4, 25)
    Me.tabCheckedOut.Name = "tabCheckedOut"
    Me.tabCheckedOut.Padding = New System.Windows.Forms.Padding(3)
    Me.tabCheckedOut.Size = New System.Drawing.Size(781, 403)
    Me.tabCheckedOut.TabIndex = 2
    Me.tabCheckedOut.Text = "Checked Out"
    Me.tabCheckedOut.UseVisualStyleBackColor = True
    '
    'tabDeleted
    '
    Me.tabDeleted.Controls.Add(Me.gridRunbooksDeletedAfter)
    Me.tabDeleted.Location = New System.Drawing.Point(4, 25)
    Me.tabDeleted.Name = "tabDeleted"
    Me.tabDeleted.Padding = New System.Windows.Forms.Padding(3)
    Me.tabDeleted.Size = New System.Drawing.Size(781, 403)
    Me.tabDeleted.TabIndex = 3
    Me.tabDeleted.Text = "Deleted"
    Me.tabDeleted.UseVisualStyleBackColor = True
    '
    'gridRunbooksDeletedAfter
    '
    Me.gridRunbooksDeletedAfter.AllowUserToAddRows = False
    Me.gridRunbooksDeletedAfter.AllowUserToDeleteRows = False
    Me.gridRunbooksDeletedAfter.AllowUserToResizeRows = False
    Me.gridRunbooksDeletedAfter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridRunbooksDeletedAfter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridRunbooksDeletedAfter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13, Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn16})
    Me.gridRunbooksDeletedAfter.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gridRunbooksDeletedAfter.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridRunbooksDeletedAfter.Location = New System.Drawing.Point(3, 3)
    Me.gridRunbooksDeletedAfter.Name = "gridRunbooksDeletedAfter"
    Me.gridRunbooksDeletedAfter.ReadOnly = True
    Me.gridRunbooksDeletedAfter.RowHeadersVisible = False
    Me.gridRunbooksDeletedAfter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridRunbooksDeletedAfter.Size = New System.Drawing.Size(775, 397)
    Me.gridRunbooksDeletedAfter.TabIndex = 6
    '
    'DataGridViewTextBoxColumn11
    '
    Me.DataGridViewTextBoxColumn11.DataPropertyName = "Id"
    Me.DataGridViewTextBoxColumn11.HeaderText = "UniqueID"
    Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
    Me.DataGridViewTextBoxColumn11.ReadOnly = True
    Me.DataGridViewTextBoxColumn11.Visible = False
    '
    'DataGridViewTextBoxColumn12
    '
    Me.DataGridViewTextBoxColumn12.DataPropertyName = "Type"
    Me.DataGridViewTextBoxColumn12.HeaderText = "Type"
    Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
    Me.DataGridViewTextBoxColumn12.ReadOnly = True
    Me.DataGridViewTextBoxColumn12.Visible = False
    '
    'DataGridViewTextBoxColumn13
    '
    Me.DataGridViewTextBoxColumn13.DataPropertyName = "Name"
    Me.DataGridViewTextBoxColumn13.HeaderText = "Runbook"
    Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
    Me.DataGridViewTextBoxColumn13.ReadOnly = True
    '
    'DataGridViewTextBoxColumn14
    '
    Me.DataGridViewTextBoxColumn14.DataPropertyName = "Path"
    Me.DataGridViewTextBoxColumn14.HeaderText = "Path"
    Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
    Me.DataGridViewTextBoxColumn14.ReadOnly = True
    '
    'DataGridViewTextBoxColumn15
    '
    Me.DataGridViewTextBoxColumn15.DataPropertyName = "DateTime"
    Me.DataGridViewTextBoxColumn15.HeaderText = "Date"
    Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
    Me.DataGridViewTextBoxColumn15.ReadOnly = True
    '
    'DataGridViewTextBoxColumn16
    '
    Me.DataGridViewTextBoxColumn16.DataPropertyName = "By"
    Me.DataGridViewTextBoxColumn16.HeaderText = "By"
    Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
    Me.DataGridViewTextBoxColumn16.ReadOnly = True
    '
    'lblShowingInformationSince
    '
    Me.lblShowingInformationSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblShowingInformationSince.AutoSize = True
    Me.lblShowingInformationSince.Location = New System.Drawing.Point(13, 468)
    Me.lblShowingInformationSince.Name = "lblShowingInformationSince"
    Me.lblShowingInformationSince.Size = New System.Drawing.Size(39, 13)
    Me.lblShowingInformationSince.TabIndex = 11
    Me.lblShowingInformationSince.Text = "Label2"
    '
    'dlgRunbookAudit
    '
    Me.AcceptButton = Me.cmdShow
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(812, 527)
    Me.Controls.Add(Me.lblShowingInformationSince)
    Me.Controls.Add(Me.objTabs)
    Me.Controls.Add(Me.cmdShow)
    Me.Controls.Add(Me.dtpAuditFrom)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgRunbookAudit"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Runbook Audit"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridRunbooksCreatedSince, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.gridRunbooksModifiedSince, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.gridRunbooksCheckedOut, System.ComponentModel.ISupportInitialize).EndInit()
    Me.objTabs.ResumeLayout(False)
    Me.tabCreated.ResumeLayout(False)
    Me.tabModified.ResumeLayout(False)
    Me.tabCheckedOut.ResumeLayout(False)
    Me.tabDeleted.ResumeLayout(False)
    CType(Me.gridRunbooksDeletedAfter, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents dtpAuditFrom As System.Windows.Forms.DateTimePicker
  Friend WithEvents cmdShow As System.Windows.Forms.Button
  Friend WithEvents gridRunbooksCreatedSince As System.Windows.Forms.DataGridView
  Friend WithEvents gridRunbooksModifiedSince As System.Windows.Forms.DataGridView
  Friend WithEvents gridRunbooksCheckedOut As System.Windows.Forms.DataGridView
  Friend WithEvents UniqueID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colType As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunbook As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colPath As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colUserID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents objTabs As System.Windows.Forms.TabControl
  Friend WithEvents tabCreated As System.Windows.Forms.TabPage
  Friend WithEvents tabModified As System.Windows.Forms.TabPage
  Friend WithEvents tabCheckedOut As System.Windows.Forms.TabPage
  Friend WithEvents lblShowingInformationSince As System.Windows.Forms.Label
  Friend WithEvents tabDeleted As System.Windows.Forms.TabPage
  Friend WithEvents gridRunbooksDeletedAfter As System.Windows.Forms.DataGridView
  Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
