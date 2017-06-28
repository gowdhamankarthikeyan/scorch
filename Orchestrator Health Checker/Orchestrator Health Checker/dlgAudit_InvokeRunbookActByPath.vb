Imports System.Windows.Forms

Public Class dlgAudit_InvokeRunbookActByPath

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgAudit_InvokeRunbookActByPath_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    gridActivitiesList.DataSource = DB.GetInvokeRunbookActivityCalledByPath
    gridActivitiesList.Refresh()
  End Sub
End Class
