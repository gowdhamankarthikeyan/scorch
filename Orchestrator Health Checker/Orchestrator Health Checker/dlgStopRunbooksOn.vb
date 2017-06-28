Imports System.Windows.Forms

Public Class dlgStopRunbooksOn

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If cmbRunbookServers.SelectedIndex < 0 Then
      MessageBox.Show("No Runbook Server was Selected", "No Server", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Return
    End If

    If DB.StopAllRunbooksOn(cmbRunbookServers.SelectedItem) Then
      Me.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.Close()
    Else
      MessageBox.Show("Unable to Stop All Runbooks Running On: " & cmbRunbookServers.SelectedItem, "Cannot Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    End If
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub
End Class
