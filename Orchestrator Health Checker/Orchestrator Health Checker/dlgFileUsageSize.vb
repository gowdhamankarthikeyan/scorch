Imports System.Windows.Forms

Public Class dlgFileUsageSize

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgTablesSize_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    gridFileSizeUsage.DataSource = DB.GetFileSizeUsage()
    gridFileSizeUsage.Refresh()
  End Sub
End Class
