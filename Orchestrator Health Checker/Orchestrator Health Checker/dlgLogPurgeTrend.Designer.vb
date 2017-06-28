<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgLogPurgeTrend
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.gridLogPurgeData = New System.Windows.Forms.DataGridView()
    Me.Day = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.StartedAt = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.EndedAt = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Duration = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Successful = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.lblAverage = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.gridLogPurgeData, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(491, 274)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(90, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OK_Button.Location = New System.Drawing.Point(11, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'gridLogPurgeData
    '
    Me.gridLogPurgeData.AllowUserToAddRows = False
    Me.gridLogPurgeData.AllowUserToDeleteRows = False
    Me.gridLogPurgeData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
    Me.gridLogPurgeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridLogPurgeData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Day, Me.StartedAt, Me.EndedAt, Me.Duration, Me.Successful})
    Me.gridLogPurgeData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    Me.gridLogPurgeData.Location = New System.Drawing.Point(13, 13)
    Me.gridLogPurgeData.Name = "gridLogPurgeData"
    Me.gridLogPurgeData.ReadOnly = True
    Me.gridLogPurgeData.RowHeadersVisible = False
    Me.gridLogPurgeData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.gridLogPurgeData.Size = New System.Drawing.Size(568, 205)
    Me.gridLogPurgeData.TabIndex = 1
    '
    'Day
    '
    Me.Day.DataPropertyName = "Day"
    Me.Day.HeaderText = "Day"
    Me.Day.Name = "Day"
    Me.Day.ReadOnly = True
    '
    'StartedAt
    '
    Me.StartedAt.DataPropertyName = "StartTime"
    DataGridViewCellStyle1.NullValue = "No Info"
    Me.StartedAt.DefaultCellStyle = DataGridViewCellStyle1
    Me.StartedAt.HeaderText = "Started At"
    Me.StartedAt.Name = "StartedAt"
    Me.StartedAt.ReadOnly = True
    '
    'EndedAt
    '
    Me.EndedAt.DataPropertyName = "EndTime"
    DataGridViewCellStyle2.NullValue = "No Info"
    Me.EndedAt.DefaultCellStyle = DataGridViewCellStyle2
    Me.EndedAt.HeaderText = "Ended At"
    Me.EndedAt.Name = "EndedAt"
    Me.EndedAt.ReadOnly = True
    '
    'Duration
    '
    Me.Duration.DataPropertyName = "Duration"
    DataGridViewCellStyle3.Format = "N2"
    DataGridViewCellStyle3.NullValue = "No Info"
    Me.Duration.DefaultCellStyle = DataGridViewCellStyle3
    Me.Duration.HeaderText = "Duration (mins)"
    Me.Duration.Name = "Duration"
    Me.Duration.ReadOnly = True
    '
    'Successful
    '
    Me.Successful.DataPropertyName = "Successful"
    DataGridViewCellStyle4.NullValue = "No Info"
    Me.Successful.DefaultCellStyle = DataGridViewCellStyle4
    Me.Successful.HeaderText = "Successful"
    Me.Successful.Name = "Successful"
    Me.Successful.ReadOnly = True
    '
    'lblAverage
    '
    Me.lblAverage.AutoSize = True
    Me.lblAverage.Location = New System.Drawing.Point(13, 234)
    Me.lblAverage.Name = "lblAverage"
    Me.lblAverage.Size = New System.Drawing.Size(39, 13)
    Me.lblAverage.TabIndex = 2
    Me.lblAverage.Text = "Label1"
    '
    'dlgLogPurgeTrend
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(593, 315)
    Me.Controls.Add(Me.lblAverage)
    Me.Controls.Add(Me.gridLogPurgeData)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgLogPurgeTrend"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Log Purge Trend"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.gridLogPurgeData, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents gridLogPurgeData As System.Windows.Forms.DataGridView
  Friend WithEvents Day As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents StartedAt As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents EndedAt As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Duration As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Successful As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents lblAverage As System.Windows.Forms.Label

End Class
