<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTablesSize
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
    Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.gridTablesSize = New System.Windows.Forms.DataGridView()
    Me.colTable = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRowCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colData = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colUnused = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colIndexSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colReserved = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colSchema = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridTablesSize, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'gridTablesSize
    '
    Me.gridTablesSize.AllowUserToAddRows = False
    Me.gridTablesSize.AllowUserToDeleteRows = False
    Me.gridTablesSize.AllowUserToOrderColumns = True
    Me.gridTablesSize.AllowUserToResizeRows = False
    DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
    Me.gridTablesSize.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
    Me.gridTablesSize.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridTablesSize.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridTablesSize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridTablesSize.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTable, Me.colRowCount, Me.colData, Me.colUnused, Me.colIndexSize, Me.colReserved, Me.colSchema})
    Me.gridTablesSize.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridTablesSize.Location = New System.Drawing.Point(0, 0)
    Me.gridTablesSize.Name = "gridTablesSize"
    Me.gridTablesSize.ReadOnly = True
    Me.gridTablesSize.RowHeadersVisible = False
    Me.gridTablesSize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridTablesSize.Size = New System.Drawing.Size(674, 268)
    Me.gridTablesSize.TabIndex = 1
    '
    'colTable
    '
    Me.colTable.DataPropertyName = "tablename"
    Me.colTable.HeaderText = "Table"
    Me.colTable.Name = "colTable"
    Me.colTable.ReadOnly = True
    '
    'colRowCount
    '
    Me.colRowCount.DataPropertyName = "row_count"
    DataGridViewCellStyle2.Format = "##,#"
    DataGridViewCellStyle2.NullValue = "0"
    Me.colRowCount.DefaultCellStyle = DataGridViewCellStyle2
    Me.colRowCount.HeaderText = "# Rows"
    Me.colRowCount.Name = "colRowCount"
    Me.colRowCount.ReadOnly = True
    '
    'colData
    '
    Me.colData.DataPropertyName = "data"
    DataGridViewCellStyle3.Format = "##,#"
    DataGridViewCellStyle3.NullValue = "0"
    Me.colData.DefaultCellStyle = DataGridViewCellStyle3
    Me.colData.HeaderText = "Data"
    Me.colData.Name = "colData"
    Me.colData.ReadOnly = True
    '
    'colUnused
    '
    Me.colUnused.DataPropertyName = "unused"
    DataGridViewCellStyle4.Format = "##,#"
    DataGridViewCellStyle4.NullValue = "0"
    Me.colUnused.DefaultCellStyle = DataGridViewCellStyle4
    Me.colUnused.HeaderText = "Unused"
    Me.colUnused.Name = "colUnused"
    Me.colUnused.ReadOnly = True
    '
    'colIndexSize
    '
    Me.colIndexSize.DataPropertyName = "index_size"
    DataGridViewCellStyle5.Format = "##,#"
    DataGridViewCellStyle5.NullValue = "0"
    Me.colIndexSize.DefaultCellStyle = DataGridViewCellStyle5
    Me.colIndexSize.HeaderText = "Index Size"
    Me.colIndexSize.Name = "colIndexSize"
    Me.colIndexSize.ReadOnly = True
    '
    'colReserved
    '
    Me.colReserved.DataPropertyName = "reserved"
    DataGridViewCellStyle6.Format = "##,#"
    DataGridViewCellStyle6.NullValue = "0"
    Me.colReserved.DefaultCellStyle = DataGridViewCellStyle6
    Me.colReserved.HeaderText = "Reserved"
    Me.colReserved.Name = "colReserved"
    Me.colReserved.ReadOnly = True
    '
    'colSchema
    '
    Me.colSchema.DataPropertyName = "schemaname"
    Me.colSchema.HeaderText = "Schema Name"
    Me.colSchema.Name = "colSchema"
    Me.colSchema.ReadOnly = True
    '
    'dlgTablesSize
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(674, 315)
    Me.Controls.Add(Me.gridTablesSize)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgTablesSize"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Database - Tables Size"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridTablesSize, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridTablesSize As System.Windows.Forms.DataGridView
  Friend WithEvents colTable As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRowCount As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colData As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colUnused As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colIndexSize As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colReserved As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colSchema As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
