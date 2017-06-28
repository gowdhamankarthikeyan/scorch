<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgAudit_InvokeRunbookActByPath
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
    Me.gridActivitiesList = New System.Windows.Forms.DataGridView()
    Me.colActivityName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunbookName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRunbookPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridActivitiesList, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(523, 312)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(76, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(4, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'gridActivitiesList
    '
    Me.gridActivitiesList.AllowUserToAddRows = False
    Me.gridActivitiesList.AllowUserToDeleteRows = False
    Me.gridActivitiesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridActivitiesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridActivitiesList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colActivityName, Me.colRunbookName, Me.colRunbookPath})
    Me.gridActivitiesList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridActivitiesList.Location = New System.Drawing.Point(13, 13)
    Me.gridActivitiesList.Name = "gridActivitiesList"
    Me.gridActivitiesList.ReadOnly = True
    Me.gridActivitiesList.RowHeadersVisible = False
    Me.gridActivitiesList.Size = New System.Drawing.Size(586, 280)
    Me.gridActivitiesList.TabIndex = 1
    '
    'colActivityName
    '
    Me.colActivityName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
    Me.colActivityName.DataPropertyName = "ActivityName"
    Me.colActivityName.HeaderText = "Activity Name"
    Me.colActivityName.Name = "colActivityName"
    Me.colActivityName.ReadOnly = True
    Me.colActivityName.Width = 89
    '
    'colRunbookName
    '
    Me.colRunbookName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
    Me.colRunbookName.DataPropertyName = "RunbookName"
    Me.colRunbookName.HeaderText = "Runbook Name"
    Me.colRunbookName.Name = "colRunbookName"
    Me.colRunbookName.ReadOnly = True
    Me.colRunbookName.Width = 98
    '
    'colRunbookPath
    '
    Me.colRunbookPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
    Me.colRunbookPath.DataPropertyName = "RunbookPath"
    Me.colRunbookPath.HeaderText = "Runbook Path"
    Me.colRunbookPath.Name = "colRunbookPath"
    Me.colRunbookPath.ReadOnly = True
    '
    'dlgAudit_InvokeRunbookActByPath
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(611, 353)
    Me.Controls.Add(Me.gridActivitiesList)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgAudit_InvokeRunbookActByPath"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = """Invoke Runbook"" Activities invoking by Path"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridActivitiesList, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridActivitiesList As System.Windows.Forms.DataGridView
  Friend WithEvents colActivityName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunbookName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRunbookPath As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
