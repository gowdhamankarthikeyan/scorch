Imports System.Windows.Forms

Public Class dlgRunbookAudit

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub cmdShow_Click(sender As System.Object, e As System.EventArgs) Handles cmdShow.Click
    LoadData()
  End Sub

  Private Sub LoadData()
    lblShowingInformationSince.Text = "Data shown since " & dtpAuditFrom.Value.ToShortDateString()

    Dim dtResult As DataTable = DB.GetAuditInformation(dtpAuditFrom.Value)

    gridRunbooksCreatedSince.DataSource = New DataView(dtResult, "Type='Created'", "", DataViewRowState.CurrentRows).ToTable
    gridRunbooksCreatedSince.Refresh()
    tabCreated.Text = "Created (" & gridRunbooksCreatedSince.RowCount & ")"

    gridRunbooksModifiedSince.DataSource = New DataView(dtResult, "Type='Modified'", "", DataViewRowState.CurrentRows).ToTable
    gridRunbooksModifiedSince.Refresh()
    tabModified.Text = "Modified (" & gridRunbooksModifiedSince.RowCount & ")"

    gridRunbooksCheckedOut.DataSource = New DataView(dtResult, "Type='Checked Out'", "", DataViewRowState.CurrentRows).ToTable
    gridRunbooksCheckedOut.Refresh()
    tabCheckedOut.Text = "Checked Out (" & gridRunbooksCheckedOut.RowCount & ")"

    gridRunbooksDeletedAfter.DataSource = New DataView(dtResult, "Type='Deleted'", "", DataViewRowState.CurrentRows).ToTable
    gridRunbooksDeletedAfter.Refresh()
    tabDeleted.Text = "Deleted (" & gridRunbooksDeletedAfter.RowCount & ")"
  End Sub

  Private Sub dlgRunbookAudit_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    dtpAuditFrom.Value = Today.AddDays(-1)
    LoadData()
  End Sub

  Private Sub gridRunbooks_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridRunbooksCheckedOut.CellDoubleClick, gridRunbooksCreatedSince.CellDoubleClick, gridRunbooksModifiedSince.CellDoubleClick, gridRunbooksDeletedAfter.CellDoubleClick
    If e.RowIndex >= 0 Then
      Dim grid As DataGridView = sender

      If Not IsDBNull(grid.Rows(e.RowIndex).Cells(1).Value) Then
        Dim objDlg As New dlgRunbookStatus(grid.Rows(e.RowIndex).Cells(2).Value, grid.Rows(e.RowIndex).Cells(1).Value.ToString())
        objDlg.ShowDialog()
      Else
        MessageBox.Show("Runbook """ & grid.Rows(e.RowIndex).Cells(2).Value & """ has been deleted. Therefore, there are no running instances", "Runbook Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
      End If
    End If
  End Sub
End Class
