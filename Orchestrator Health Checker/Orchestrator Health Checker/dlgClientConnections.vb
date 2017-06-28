Imports System.Windows.Forms

Public Class dlgClientConnections

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgClientConnections_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    LoadData()
  End Sub

  Private Sub LoadData()
    gridClientConnections.DataSource = DB.GetAuditClientConnections
    gridClientConnections.Refresh()
  End Sub
End Class
