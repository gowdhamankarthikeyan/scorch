<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgProgress
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
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.lblStopRunningRunbooks = New System.Windows.Forms.Label()
    Me.picStopRunningRunbooks = New System.Windows.Forms.PictureBox()
    Me.picCleanOrphans = New System.Windows.Forms.PictureBox()
    Me.lblCleanOrphans = New System.Windows.Forms.Label()
    Me.picLogPurge = New System.Windows.Forms.PictureBox()
    Me.lblLogPurge = New System.Windows.Forms.Label()
    Me.picStartMonitorRunbooks = New System.Windows.Forms.PictureBox()
    Me.lblStartMonitorRunbooks = New System.Windows.Forms.Label()
    Me.picStopMonitoringRunbooks = New System.Windows.Forms.PictureBox()
    Me.lblStopMonitoringRunbooks = New System.Windows.Forms.Label()
    CType(Me.picStopRunningRunbooks, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picCleanOrphans, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picLogPurge, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picStartMonitorRunbooks, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picStopMonitoringRunbooks, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdCancel
    '
    Me.cmdCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdCancel.Location = New System.Drawing.Point(279, 378)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(92, 39)
    Me.cmdCancel.TabIndex = 0
    Me.cmdCancel.Text = "Cancel"
    Me.cmdCancel.UseVisualStyleBackColor = True
    '
    'lblStopRunningRunbooks
    '
    Me.lblStopRunningRunbooks.AutoSize = True
    Me.lblStopRunningRunbooks.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblStopRunningRunbooks.Location = New System.Drawing.Point(68, 22)
    Me.lblStopRunningRunbooks.Name = "lblStopRunningRunbooks"
    Me.lblStopRunningRunbooks.Size = New System.Drawing.Size(242, 24)
    Me.lblStopRunningRunbooks.TabIndex = 1
    Me.lblStopRunningRunbooks.Text = "Stop All Running Runbooks"
    '
    'picStopRunningRunbooks
    '
    Me.picStopRunningRunbooks.Location = New System.Drawing.Point(12, 12)
    Me.picStopRunningRunbooks.Name = "picStopRunningRunbooks"
    Me.picStopRunningRunbooks.Size = New System.Drawing.Size(50, 44)
    Me.picStopRunningRunbooks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.picStopRunningRunbooks.TabIndex = 2
    Me.picStopRunningRunbooks.TabStop = False
    '
    'picCleanOrphans
    '
    Me.picCleanOrphans.Location = New System.Drawing.Point(12, 148)
    Me.picCleanOrphans.Name = "picCleanOrphans"
    Me.picCleanOrphans.Size = New System.Drawing.Size(50, 44)
    Me.picCleanOrphans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.picCleanOrphans.TabIndex = 4
    Me.picCleanOrphans.TabStop = False
    '
    'lblCleanOrphans
    '
    Me.lblCleanOrphans.AutoSize = True
    Me.lblCleanOrphans.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblCleanOrphans.Location = New System.Drawing.Point(68, 158)
    Me.lblCleanOrphans.Name = "lblCleanOrphans"
    Me.lblCleanOrphans.Size = New System.Drawing.Size(219, 24)
    Me.lblCleanOrphans.TabIndex = 3
    Me.lblCleanOrphans.Text = "Clean Orphan Runbooks"
    '
    'picLogPurge
    '
    Me.picLogPurge.Location = New System.Drawing.Point(12, 216)
    Me.picLogPurge.Name = "picLogPurge"
    Me.picLogPurge.Size = New System.Drawing.Size(50, 44)
    Me.picLogPurge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.picLogPurge.TabIndex = 6
    Me.picLogPurge.TabStop = False
    '
    'lblLogPurge
    '
    Me.lblLogPurge.AutoSize = True
    Me.lblLogPurge.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblLogPurge.Location = New System.Drawing.Point(68, 226)
    Me.lblLogPurge.Name = "lblLogPurge"
    Me.lblLogPurge.Size = New System.Drawing.Size(167, 24)
    Me.lblLogPurge.TabIndex = 5
    Me.lblLogPurge.Text = "Custom Log Purge"
    '
    'picStartMonitorRunbooks
    '
    Me.picStartMonitorRunbooks.Location = New System.Drawing.Point(12, 284)
    Me.picStartMonitorRunbooks.Name = "picStartMonitorRunbooks"
    Me.picStartMonitorRunbooks.Size = New System.Drawing.Size(50, 44)
    Me.picStartMonitorRunbooks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.picStartMonitorRunbooks.TabIndex = 8
    Me.picStartMonitorRunbooks.TabStop = False
    '
    'lblStartMonitorRunbooks
    '
    Me.lblStartMonitorRunbooks.AutoSize = True
    Me.lblStartMonitorRunbooks.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblStartMonitorRunbooks.Location = New System.Drawing.Point(68, 294)
    Me.lblStartMonitorRunbooks.Name = "lblStartMonitorRunbooks"
    Me.lblStartMonitorRunbooks.Size = New System.Drawing.Size(231, 24)
    Me.lblStartMonitorRunbooks.TabIndex = 7
    Me.lblStartMonitorRunbooks.Text = "Start All Monitor Runbooks"
    '
    'picStopMonitoringRunbooks
    '
    Me.picStopMonitoringRunbooks.Location = New System.Drawing.Point(12, 80)
    Me.picStopMonitoringRunbooks.Name = "picStopMonitoringRunbooks"
    Me.picStopMonitoringRunbooks.Size = New System.Drawing.Size(50, 44)
    Me.picStopMonitoringRunbooks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.picStopMonitoringRunbooks.TabIndex = 10
    Me.picStopMonitoringRunbooks.TabStop = False
    '
    'lblStopMonitoringRunbooks
    '
    Me.lblStopMonitoringRunbooks.AutoSize = True
    Me.lblStopMonitoringRunbooks.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblStopMonitoringRunbooks.Location = New System.Drawing.Point(68, 90)
    Me.lblStopMonitoringRunbooks.Name = "lblStopMonitoringRunbooks"
    Me.lblStopMonitoringRunbooks.Size = New System.Drawing.Size(259, 24)
    Me.lblStopMonitoringRunbooks.TabIndex = 9
    Me.lblStopMonitoringRunbooks.Text = "Stop All Monitoring Runbooks"
    '
    'dlgProgress
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(383, 429)
    Me.Controls.Add(Me.picStopMonitoringRunbooks)
    Me.Controls.Add(Me.lblStopMonitoringRunbooks)
    Me.Controls.Add(Me.picStartMonitorRunbooks)
    Me.Controls.Add(Me.lblStartMonitorRunbooks)
    Me.Controls.Add(Me.picLogPurge)
    Me.Controls.Add(Me.lblLogPurge)
    Me.Controls.Add(Me.picCleanOrphans)
    Me.Controls.Add(Me.lblCleanOrphans)
    Me.Controls.Add(Me.picStopRunningRunbooks)
    Me.Controls.Add(Me.lblStopRunningRunbooks)
    Me.Controls.Add(Me.cmdCancel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgProgress"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Working..."
    CType(Me.picStopRunningRunbooks, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picCleanOrphans, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picLogPurge, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picStartMonitorRunbooks, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picStopMonitoringRunbooks, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents lblStopRunningRunbooks As System.Windows.Forms.Label
  Friend WithEvents picStopRunningRunbooks As System.Windows.Forms.PictureBox
  Friend WithEvents picCleanOrphans As System.Windows.Forms.PictureBox
  Friend WithEvents lblCleanOrphans As System.Windows.Forms.Label
  Friend WithEvents picLogPurge As System.Windows.Forms.PictureBox
  Friend WithEvents lblLogPurge As System.Windows.Forms.Label
  Friend WithEvents picStartMonitorRunbooks As System.Windows.Forms.PictureBox
  Friend WithEvents lblStartMonitorRunbooks As System.Windows.Forms.Label
  Friend WithEvents picStopMonitoringRunbooks As System.Windows.Forms.PictureBox
  Friend WithEvents lblStopMonitoringRunbooks As System.Windows.Forms.Label

End Class
