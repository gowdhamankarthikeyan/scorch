Imports System.Windows.Forms

Public Class dlgLoggingEnabledRunbooks
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgLoggingEnabledRunbooks_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    gridLoggingEnabledRunbooks.DataSource = DB.GetLoggingEnabledRunbook
    gridLoggingEnabledRunbooks.Refresh()
  End Sub
End Class
