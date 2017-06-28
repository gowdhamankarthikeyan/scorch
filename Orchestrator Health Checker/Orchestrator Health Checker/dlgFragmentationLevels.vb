Imports System.Windows.Forms

Public Class dlgFragmentationLevels
  Private dtResult As DataTable = Nothing
  Private intCriticalLimit As Integer = 0
  Private intHighLimit As Integer = 0

  Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub BlockUI(shouldBlock As Boolean)
    txtPages.Enabled = Not shouldBlock
    txtRebuild.Enabled = Not shouldBlock
    txtReOrg.Enabled = Not shouldBlock
    cmdShow.Enabled = Not shouldBlock
    cmdClose.Enabled = Not shouldBlock
    cmdReIndexTables.Enabled = Not shouldBlock

    If shouldBlock Then
      lblStatus.Text = "Status: Generating Information..."
      progressBar.Enabled = True
      progressBar.Visible = True
      progressBar.Value = 15
      progressBar.Style = ProgressBarStyle.Marquee
    Else
      lblStatus.Text = "Status: Ready"
      progressBar.Enabled = False
      progressBar.Visible = False
    End If
  End Sub

  Private Sub cmdShow_Click(sender As System.Object, e As System.EventArgs) Handles cmdShow.Click
    dtResult = Nothing
    BlockUI(True)
    worker.RunWorkerAsync()
  End Sub

  Private Sub worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles worker.DoWork
    dtResult = DB.GetFragLevel(txtReOrg.Value, txtRebuild.Value, txtPages.Value, chkFilter.Checked)
  End Sub

  Private Sub worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
    gridFragLevels.DataSource = dtResult
    gridFragLevels.Refresh()
    BlockUI(False)
  End Sub

  Private Sub gridFragLevels_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles gridFragLevels.CellFormatting
    If gridFragLevels.Columns(e.ColumnIndex).Name = "colShouldRebuild" Then
      If e.Value = "Yes" Then
        e.CellStyle.BackColor = Color.DarkRed
        e.CellStyle.ForeColor = Color.White
      End If
    ElseIf gridFragLevels.Columns(e.ColumnIndex).Name = "colShouldReOrg" Then
      If e.Value = "Yes" Then
        e.CellStyle.BackColor = Color.DarkRed
        e.CellStyle.ForeColor = Color.White
      End If
    ElseIf gridFragLevels.Columns(e.ColumnIndex).Name = "colAVGFrag" Then
      If e.Value >= 90 Then
        e.CellStyle.BackColor = Color.DarkRed
        e.CellStyle.ForeColor = Color.White
        'e.CellStyle.Font = New Font(e.CellStyle.Font.FontFamily, e.CellStyle.Font.Size, FontStyle.Bold)
      ElseIf e.Value >= 80 Then
        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 221)
      End If
    End If
  End Sub

  Private Sub cmdReIndexTables_Click(sender As System.Object, e As System.EventArgs) Handles cmdReIndexTables.Click
    If MessageBox.Show("Are you sure you want to rebuild all indexes?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
      If DB.ReIndexTables() Then
        MessageBox.Show("Tables were successfully reindexed", "Table Reindexed", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Else
        MessageBox.Show("An unexpected error occurred while trying to reindex tables", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If
    End If
  End Sub
End Class
