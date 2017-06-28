<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSystemHealth
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
    Me.lblPolicyInstances = New System.Windows.Forms.Label()
    Me.lblObjectInstances = New System.Windows.Forms.Label()
    Me.lblObjectData = New System.Windows.Forms.Label()
    Me.lblJobs = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.lblLogPurgeCompleted = New System.Windows.Forms.Label()
    Me.lblLogPurgeDuration = New System.Windows.Forms.Label()
    Me.lblLogPurgeEnd = New System.Windows.Forms.Label()
    Me.lblLogPurgeStart = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 1
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(217, 308)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(79, 29)
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
    'lblPolicyInstances
    '
    Me.lblPolicyInstances.AutoSize = True
    Me.lblPolicyInstances.Location = New System.Drawing.Point(12, 22)
    Me.lblPolicyInstances.Name = "lblPolicyInstances"
    Me.lblPolicyInstances.Size = New System.Drawing.Size(39, 13)
    Me.lblPolicyInstances.TabIndex = 1
    Me.lblPolicyInstances.Text = "Label1"
    '
    'lblObjectInstances
    '
    Me.lblObjectInstances.AutoSize = True
    Me.lblObjectInstances.Location = New System.Drawing.Point(12, 58)
    Me.lblObjectInstances.Name = "lblObjectInstances"
    Me.lblObjectInstances.Size = New System.Drawing.Size(39, 13)
    Me.lblObjectInstances.TabIndex = 2
    Me.lblObjectInstances.Text = "Label2"
    '
    'lblObjectData
    '
    Me.lblObjectData.AutoSize = True
    Me.lblObjectData.Location = New System.Drawing.Point(12, 94)
    Me.lblObjectData.Name = "lblObjectData"
    Me.lblObjectData.Size = New System.Drawing.Size(39, 13)
    Me.lblObjectData.TabIndex = 3
    Me.lblObjectData.Text = "Label3"
    '
    'lblJobs
    '
    Me.lblJobs.AutoSize = True
    Me.lblJobs.Location = New System.Drawing.Point(12, 130)
    Me.lblJobs.Name = "lblJobs"
    Me.lblJobs.Size = New System.Drawing.Size(39, 13)
    Me.lblJobs.TabIndex = 4
    Me.lblJobs.Text = "Label4"
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.lblLogPurgeCompleted)
    Me.GroupBox1.Controls.Add(Me.lblLogPurgeDuration)
    Me.GroupBox1.Controls.Add(Me.lblLogPurgeEnd)
    Me.GroupBox1.Controls.Add(Me.lblLogPurgeStart)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 157)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(284, 145)
    Me.GroupBox1.TabIndex = 5
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Last Log Purge"
    '
    'lblLogPurgeCompleted
    '
    Me.lblLogPurgeCompleted.AutoSize = True
    Me.lblLogPurgeCompleted.Location = New System.Drawing.Point(10, 113)
    Me.lblLogPurgeCompleted.Name = "lblLogPurgeCompleted"
    Me.lblLogPurgeCompleted.Size = New System.Drawing.Size(39, 13)
    Me.lblLogPurgeCompleted.TabIndex = 3
    Me.lblLogPurgeCompleted.Text = "Label4"
    '
    'lblLogPurgeDuration
    '
    Me.lblLogPurgeDuration.AutoSize = True
    Me.lblLogPurgeDuration.Location = New System.Drawing.Point(10, 82)
    Me.lblLogPurgeDuration.Name = "lblLogPurgeDuration"
    Me.lblLogPurgeDuration.Size = New System.Drawing.Size(39, 13)
    Me.lblLogPurgeDuration.TabIndex = 2
    Me.lblLogPurgeDuration.Text = "Label3"
    '
    'lblLogPurgeEnd
    '
    Me.lblLogPurgeEnd.AutoSize = True
    Me.lblLogPurgeEnd.Location = New System.Drawing.Point(10, 51)
    Me.lblLogPurgeEnd.Name = "lblLogPurgeEnd"
    Me.lblLogPurgeEnd.Size = New System.Drawing.Size(39, 13)
    Me.lblLogPurgeEnd.TabIndex = 1
    Me.lblLogPurgeEnd.Text = "Label2"
    '
    'lblLogPurgeStart
    '
    Me.lblLogPurgeStart.AutoSize = True
    Me.lblLogPurgeStart.Location = New System.Drawing.Point(10, 20)
    Me.lblLogPurgeStart.Name = "lblLogPurgeStart"
    Me.lblLogPurgeStart.Size = New System.Drawing.Size(39, 13)
    Me.lblLogPurgeStart.TabIndex = 0
    Me.lblLogPurgeStart.Text = "Label1"
    '
    'dlgSystemHealth
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.OK_Button
    Me.ClientSize = New System.Drawing.Size(308, 349)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.lblJobs)
    Me.Controls.Add(Me.lblObjectData)
    Me.Controls.Add(Me.lblObjectInstances)
    Me.Controls.Add(Me.lblPolicyInstances)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgSystemHealth"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "System Health"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents lblPolicyInstances As System.Windows.Forms.Label
  Friend WithEvents lblObjectInstances As System.Windows.Forms.Label
  Friend WithEvents lblObjectData As System.Windows.Forms.Label
  Friend WithEvents lblJobs As System.Windows.Forms.Label
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents lblLogPurgeCompleted As System.Windows.Forms.Label
  Friend WithEvents lblLogPurgeDuration As System.Windows.Forms.Label
  Friend WithEvents lblLogPurgeEnd As System.Windows.Forms.Label
  Friend WithEvents lblLogPurgeStart As System.Windows.Forms.Label

End Class
