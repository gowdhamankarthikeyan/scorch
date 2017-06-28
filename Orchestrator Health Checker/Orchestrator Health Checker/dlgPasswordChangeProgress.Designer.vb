<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgPasswordChangeProgress
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
    Me.lblUpdateUser = New System.Windows.Forms.Label()
    Me.progressBar = New System.Windows.Forms.ProgressBar()
    Me.lblUpdateStep = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'lblUpdateUser
    '
    Me.lblUpdateUser.AutoSize = True
    Me.lblUpdateUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblUpdateUser.Location = New System.Drawing.Point(12, 9)
    Me.lblUpdateUser.Name = "lblUpdateUser"
    Me.lblUpdateUser.Size = New System.Drawing.Size(159, 17)
    Me.lblUpdateUser.TabIndex = 0
    Me.lblUpdateUser.Text = "Updating Password For:"
    '
    'progressBar
    '
    Me.progressBar.Location = New System.Drawing.Point(12, 35)
    Me.progressBar.Name = "progressBar"
    Me.progressBar.Size = New System.Drawing.Size(411, 23)
    Me.progressBar.TabIndex = 1
    '
    'lblUpdateStep
    '
    Me.lblUpdateStep.Location = New System.Drawing.Point(12, 61)
    Me.lblUpdateStep.Name = "lblUpdateStep"
    Me.lblUpdateStep.Size = New System.Drawing.Size(411, 23)
    Me.lblUpdateStep.TabIndex = 2
    Me.lblUpdateStep.Text = "Label2"
    Me.lblUpdateStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'dlgPasswordChangeProgress
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(435, 98)
    Me.Controls.Add(Me.lblUpdateStep)
    Me.Controls.Add(Me.progressBar)
    Me.Controls.Add(Me.lblUpdateUser)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgPasswordChangeProgress"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Password Update Progress"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lblUpdateUser As System.Windows.Forms.Label
  Friend WithEvents progressBar As System.Windows.Forms.ProgressBar
  Friend WithEvents lblUpdateStep As System.Windows.Forms.Label

End Class
