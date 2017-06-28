Imports System.Windows.Forms

Public Class dlgRunningStatistics_MonitorRunbooks

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub FormatGrid()
    For Each objRow As DataGridViewRow In gridMonitorRunbooks.Rows
      If objRow.Cells(2).Value = "Stopped" Then
        For Each objCell As DataGridViewCell In objRow.Cells
          objCell.Style.BackColor = Color.FromArgb(150, 0, 0)
          objCell.Style.ForeColor = Color.White
        Next
      End If
    Next
  End Sub

  Private Sub gridMonitorRunbooks_DataBindingComplete(sender As Object, e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles gridMonitorRunbooks.DataBindingComplete
    FormatGrid()
  End Sub

  Private Sub dlgRunningStatistics_MonitorRunbooks_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    RefreshGrid()
  End Sub

  Private Sub RefreshToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
    RefreshGrid()
  End Sub

  Private Sub RefreshGrid()
    gridMonitorRunbooks.DataSource = DB.GetMonitorRunbooksStats
    gridMonitorRunbooks.Refresh()
    lblLastUpdate.Text = "Updated At: " & Now.ToString("MM/dd/yyyy HH:mm:ss")
  End Sub
End Class
