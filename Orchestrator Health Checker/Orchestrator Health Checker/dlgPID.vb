Imports System.Windows.Forms

Public Class dlgPID

  Private dlgRunbook As dlgRunbookStatus = Nothing

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub cmdSearch_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch.Click
    Dim intPID As Integer = -1

    Integer.TryParse(txtPID.Text.Trim(), intPID)

    If intPID > 0 Then
      Dim tmpDT As DataTable = DB.GetRunbookByPID(intPID)

      If tmpDT.Rows.Count > 0 Then
        lblRunbookName.Text = "Runbook: " & tmpDT.Rows(0)("Name")
        lblServerName.Text = "Runbook Server: " & tmpDT.Rows(0)("Computer")
        dlgRunbook = New dlgRunbookStatus(tmpDT.Rows(0)("Name"), tmpDT.Rows(0)("UniqueID").ToString())
      Else
        lblRunbookName.Text = "Runbook: Not Found"
        lblServerName.Text = "Runbook Server: Not Found"
        dlgRunbook = Nothing
      End If
    Else
      MessageBox.Show("PID doesn't seem to be a Process ID. PIDs are numeric values", "PID Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End If
  End Sub

  Private Sub cmdOpen_Click(sender As Object, e As System.EventArgs) Handles cmdOpen.Click
    If dlgRunbook IsNot Nothing Then
      dlgRunbook.ShowDialog()
    End If
  End Sub
End Class
