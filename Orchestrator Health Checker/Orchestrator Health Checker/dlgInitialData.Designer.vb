<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgInitialData
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
    Me.lblRunbookName = New System.Windows.Forms.Label()
    Me.lblRunbookServer = New System.Windows.Forms.Label()
    Me.gridInitialData = New System.Windows.Forms.DataGridView()
    Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.cmdStop = New System.Windows.Forms.Button()
    Me.lblTimeStarted = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridInitialData, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(358, 363)
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
    Me.OK_Button.Text = "Close"
    '
    'lblRunbookName
    '
    Me.lblRunbookName.AutoSize = True
    Me.lblRunbookName.Location = New System.Drawing.Point(13, 13)
    Me.lblRunbookName.Name = "lblRunbookName"
    Me.lblRunbookName.Size = New System.Drawing.Size(54, 13)
    Me.lblRunbookName.TabIndex = 1
    Me.lblRunbookName.Text = "Runbook:"
    '
    'lblRunbookServer
    '
    Me.lblRunbookServer.AutoSize = True
    Me.lblRunbookServer.Location = New System.Drawing.Point(13, 44)
    Me.lblRunbookServer.Name = "lblRunbookServer"
    Me.lblRunbookServer.Size = New System.Drawing.Size(88, 13)
    Me.lblRunbookServer.TabIndex = 2
    Me.lblRunbookServer.Text = "Runbook Server:"
    '
    'gridInitialData
    '
    Me.gridInitialData.AllowUserToAddRows = False
    Me.gridInitialData.AllowUserToDeleteRows = False
    Me.gridInitialData.AllowUserToOrderColumns = True
    Me.gridInitialData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gridInitialData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridInitialData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridInitialData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colValue})
    Me.gridInitialData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridInitialData.Location = New System.Drawing.Point(13, 106)
    Me.gridInitialData.Name = "gridInitialData"
    Me.gridInitialData.ReadOnly = True
    Me.gridInitialData.RowHeadersVisible = False
    Me.gridInitialData.Size = New System.Drawing.Size(421, 246)
    Me.gridInitialData.TabIndex = 3
    '
    'colName
    '
    Me.colName.DataPropertyName = "Name"
    Me.colName.HeaderText = "Name"
    Me.colName.Name = "colName"
    Me.colName.ReadOnly = True
    '
    'colValue
    '
    Me.colValue.DataPropertyName = "Value"
    Me.colValue.HeaderText = "Value"
    Me.colValue.Name = "colValue"
    Me.colValue.ReadOnly = True
    '
    'cmdStop
    '
    Me.cmdStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdStop.Location = New System.Drawing.Point(12, 363)
    Me.cmdStop.Name = "cmdStop"
    Me.cmdStop.Size = New System.Drawing.Size(45, 37)
    Me.cmdStop.TabIndex = 5
    Me.cmdStop.Text = "Stop"
    Me.cmdStop.UseVisualStyleBackColor = True
    '
    'lblTimeStarted
    '
    Me.lblTimeStarted.AutoSize = True
    Me.lblTimeStarted.Location = New System.Drawing.Point(13, 75)
    Me.lblTimeStarted.Name = "lblTimeStarted"
    Me.lblTimeStarted.Size = New System.Drawing.Size(70, 13)
    Me.lblTimeStarted.TabIndex = 6
    Me.lblTimeStarted.Text = "Time Started:"
    '
    'dlgInitialData
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(446, 414)
    Me.Controls.Add(Me.lblTimeStarted)
    Me.Controls.Add(Me.cmdStop)
    Me.Controls.Add(Me.gridInitialData)
    Me.Controls.Add(Me.lblRunbookServer)
    Me.Controls.Add(Me.lblRunbookName)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgInitialData"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Initial Data For:"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridInitialData, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents lblRunbookName As System.Windows.Forms.Label
  Friend WithEvents lblRunbookServer As System.Windows.Forms.Label
  Friend WithEvents gridInitialData As System.Windows.Forms.DataGridView
  Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colValue As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents cmdStop As System.Windows.Forms.Button
  Friend WithEvents lblTimeStarted As System.Windows.Forms.Label

End Class
