<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgAdminOptions
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
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.chkStopRunningRunbooks = New System.Windows.Forms.CheckBox()
    Me.chkCleanOrphans = New System.Windows.Forms.CheckBox()
    Me.chkLogPurge = New System.Windows.Forms.CheckBox()
    Me.chkStartMonitoring = New System.Windows.Forms.CheckBox()
    Me.chkStopMonitoringRunbooks = New System.Windows.Forms.CheckBox()
    Me.objHelp = New System.Windows.Forms.HelpProvider()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(144, 224)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Cancel"
    '
    'chkStopRunningRunbooks
    '
    Me.chkStopRunningRunbooks.AutoSize = True
    Me.objHelp.SetHelpString(Me.chkStopRunningRunbooks, "")
    Me.chkStopRunningRunbooks.Location = New System.Drawing.Point(13, 13)
    Me.chkStopRunningRunbooks.Name = "chkStopRunningRunbooks"
    Me.objHelp.SetShowHelp(Me.chkStopRunningRunbooks, True)
    Me.chkStopRunningRunbooks.Size = New System.Drawing.Size(157, 17)
    Me.chkStopRunningRunbooks.TabIndex = 1
    Me.chkStopRunningRunbooks.Text = "Stop All Running Runbooks"
    Me.chkStopRunningRunbooks.UseVisualStyleBackColor = True
    '
    'chkCleanOrphans
    '
    Me.chkCleanOrphans.AutoSize = True
    Me.chkCleanOrphans.Location = New System.Drawing.Point(13, 99)
    Me.chkCleanOrphans.Name = "chkCleanOrphans"
    Me.chkCleanOrphans.Size = New System.Drawing.Size(143, 17)
    Me.chkCleanOrphans.TabIndex = 2
    Me.chkCleanOrphans.Text = "Clean Orphan Runbooks"
    Me.chkCleanOrphans.UseVisualStyleBackColor = True
    '
    'chkLogPurge
    '
    Me.chkLogPurge.AutoSize = True
    Me.chkLogPurge.Location = New System.Drawing.Point(13, 142)
    Me.chkLogPurge.Name = "chkLogPurge"
    Me.chkLogPurge.Size = New System.Drawing.Size(113, 17)
    Me.chkLogPurge.TabIndex = 3
    Me.chkLogPurge.Text = "Custom Log Purge"
    Me.chkLogPurge.UseVisualStyleBackColor = True
    '
    'chkStartMonitoring
    '
    Me.chkStartMonitoring.AutoSize = True
    Me.chkStartMonitoring.Location = New System.Drawing.Point(13, 185)
    Me.chkStartMonitoring.Name = "chkStartMonitoring"
    Me.chkStartMonitoring.Size = New System.Drawing.Size(166, 17)
    Me.chkStartMonitoring.TabIndex = 4
    Me.chkStartMonitoring.Text = "Start All Monitoring Runbooks"
    Me.chkStartMonitoring.UseVisualStyleBackColor = True
    '
    'chkStopMonitoringRunbooks
    '
    Me.chkStopMonitoringRunbooks.AutoSize = True
    Me.chkStopMonitoringRunbooks.Location = New System.Drawing.Point(13, 56)
    Me.chkStopMonitoringRunbooks.Name = "chkStopMonitoringRunbooks"
    Me.chkStopMonitoringRunbooks.Size = New System.Drawing.Size(166, 17)
    Me.chkStopMonitoringRunbooks.TabIndex = 5
    Me.chkStopMonitoringRunbooks.Text = "Stop All Monitoring Runbooks"
    Me.chkStopMonitoringRunbooks.UseVisualStyleBackColor = True
    '
    'dlgAdminOptions
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(302, 265)
    Me.Controls.Add(Me.chkStopMonitoringRunbooks)
    Me.Controls.Add(Me.chkStartMonitoring)
    Me.Controls.Add(Me.chkLogPurge)
    Me.Controls.Add(Me.chkCleanOrphans)
    Me.Controls.Add(Me.chkStopRunningRunbooks)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.HelpButton = True
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgAdminOptions"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Admin Options"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents chkStopRunningRunbooks As System.Windows.Forms.CheckBox
  Friend WithEvents chkCleanOrphans As System.Windows.Forms.CheckBox
  Friend WithEvents chkLogPurge As System.Windows.Forms.CheckBox
  Friend WithEvents chkStartMonitoring As System.Windows.Forms.CheckBox
  Friend WithEvents chkStopMonitoringRunbooks As System.Windows.Forms.CheckBox
  Friend WithEvents objHelp As System.Windows.Forms.HelpProvider

End Class
