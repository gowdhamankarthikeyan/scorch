Imports System.Windows.Forms

Public Class dlgSearchVariableUsage
  Private dictActivityList As Dictionary(Of String, String)
  Private objProgress As New dlgSearching

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgSearchVariableUsage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    cmbVariables.DataSource = DB.GetVariables
    cmbVariables.Refresh()
  End Sub

  Private Sub cmdSearch_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch.Click
    If cmbVariables.SelectedIndex > 0 Then
      workerSearch.RunWorkerAsync(cmbVariables.SelectedValue)
      objProgress.ShowDialog()
    End If
  End Sub

  Private Sub workerSearch_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles workerSearch.DoWork
    e.Result = DB.SearchVariableUsage(e.Argument.ToString().ToUpper())
  End Sub

  Private Sub workerSearch_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles workerSearch.RunWorkerCompleted
    objProgress.Close()
    gridResults.DataSource = e.Result
    gridResults.Refresh()
  End Sub
End Class
