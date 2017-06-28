<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgFileUsageSize
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
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.gridFileSizeUsage = New System.Windows.Forms.DataGridView()
    Me.colFileID = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colFullPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colSpaceUsed = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colFreeSpace = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colFileSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colGrowth = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridFileSizeUsage, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(584, 274)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(78, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(5, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'gridFileSizeUsage
    '
    Me.gridFileSizeUsage.AllowUserToAddRows = False
    Me.gridFileSizeUsage.AllowUserToDeleteRows = False
    Me.gridFileSizeUsage.AllowUserToOrderColumns = True
    Me.gridFileSizeUsage.AllowUserToResizeRows = False
    DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
    Me.gridFileSizeUsage.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
    Me.gridFileSizeUsage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridFileSizeUsage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridFileSizeUsage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridFileSizeUsage.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFileID, Me.colName, Me.colFullPath, Me.colSpaceUsed, Me.colFreeSpace, Me.colFileSize, Me.colGrowth})
    Me.gridFileSizeUsage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridFileSizeUsage.Location = New System.Drawing.Point(0, 0)
    Me.gridFileSizeUsage.Name = "gridFileSizeUsage"
    Me.gridFileSizeUsage.ReadOnly = True
    Me.gridFileSizeUsage.RowHeadersVisible = False
    Me.gridFileSizeUsage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridFileSizeUsage.Size = New System.Drawing.Size(674, 268)
    Me.gridFileSizeUsage.TabIndex = 1
    '
    'colFileID
    '
    Me.colFileID.DataPropertyName = "FILEID"
    Me.colFileID.HeaderText = "ID"
    Me.colFileID.Name = "colFileID"
    Me.colFileID.ReadOnly = True
    '
    'colName
    '
    Me.colName.DataPropertyName = "NAME"
    Me.colName.HeaderText = "Name"
    Me.colName.Name = "colName"
    Me.colName.ReadOnly = True
    '
    'colFullPath
    '
    Me.colFullPath.DataPropertyName = "FILENAME"
    Me.colFullPath.HeaderText = "Full Path"
    Me.colFullPath.Name = "colFullPath"
    Me.colFullPath.ReadOnly = True
    '
    'colSpaceUsed
    '
    Me.colSpaceUsed.DataPropertyName = "SPACE_USED_MB"
    DataGridViewCellStyle2.Format = "##,#.##"
    DataGridViewCellStyle2.NullValue = Nothing
    Me.colSpaceUsed.DefaultCellStyle = DataGridViewCellStyle2
    Me.colSpaceUsed.HeaderText = "Used Space (MB)"
    Me.colSpaceUsed.Name = "colSpaceUsed"
    Me.colSpaceUsed.ReadOnly = True
    '
    'colFreeSpace
    '
    Me.colFreeSpace.DataPropertyName = "FREE_SPACE_MB"
    DataGridViewCellStyle3.Format = "##,#.##"
    Me.colFreeSpace.DefaultCellStyle = DataGridViewCellStyle3
    Me.colFreeSpace.HeaderText = "Free Space (MB)"
    Me.colFreeSpace.Name = "colFreeSpace"
    Me.colFreeSpace.ReadOnly = True
    '
    'colFileSize
    '
    Me.colFileSize.DataPropertyName = "FILE_SIZE_MB"
    DataGridViewCellStyle4.Format = "##,#.##"
    Me.colFileSize.DefaultCellStyle = DataGridViewCellStyle4
    Me.colFileSize.HeaderText = "File Size (MB)"
    Me.colFileSize.Name = "colFileSize"
    Me.colFileSize.ReadOnly = True
    '
    'colGrowth
    '
    Me.colGrowth.DataPropertyName = "GROWTH_MB"
    DataGridViewCellStyle5.Format = "##,#.##"
    Me.colGrowth.DefaultCellStyle = DataGridViewCellStyle5
    Me.colGrowth.HeaderText = "Growth (MB)"
    Me.colGrowth.Name = "colGrowth"
    Me.colGrowth.ReadOnly = True
    '
    'dlgFileUsageSize
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(674, 315)
    Me.Controls.Add(Me.gridFileSizeUsage)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgFileUsageSize"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Database - Files Size & Usage"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridFileSizeUsage, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridFileSizeUsage As System.Windows.Forms.DataGridView
  Friend WithEvents colFileID As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colFullPath As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colSpaceUsed As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colFreeSpace As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colFileSize As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colGrowth As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
