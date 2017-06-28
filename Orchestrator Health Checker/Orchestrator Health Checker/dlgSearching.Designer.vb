<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSearching
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
    Me.progressSearching = New System.Windows.Forms.ProgressBar()
    Me.SuspendLayout()
    '
    'progressSearching
    '
    Me.progressSearching.Location = New System.Drawing.Point(13, 13)
    Me.progressSearching.Name = "progressSearching"
    Me.progressSearching.Size = New System.Drawing.Size(410, 23)
    Me.progressSearching.Style = System.Windows.Forms.ProgressBarStyle.Marquee
    Me.progressSearching.TabIndex = 0
    Me.progressSearching.Value = 25
    '
    'dlgSearching
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(435, 62)
    Me.Controls.Add(Me.progressSearching)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgSearching"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Searching..."
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents progressSearching As System.Windows.Forms.ProgressBar

End Class
