Imports System.Windows.Forms

Public Class dlgRunningStatistics
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.Close()
  End Sub

  Private Sub dlgRunningStatistics_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    RefreshData()
  End Sub

  Private Sub RefreshData()
    Dim tmpServerUsagePercentage As Double = 0

    gridStatsByServer.Rows.Clear()
    gridStatsType.Rows.Clear()

    For Each objRow As DataRow In DB.GetStatsRunbookServers().Rows
      gridStatsByServer.Rows.Add(New Object() {objRow("Computer"), 0, objRow("MaxRunningPolicies"), "0%"})
    Next

    For Each objRow As DataRow In DB.GetRunbooksTypeCount().Rows
      gridStatsType.Rows.Add(New Object() {objRow("RunbookType"), 0, objRow("EnabledInstances"), objRow("DisabledInstances"), objRow("TotalInstances")})
    Next

    For Each objRow As DataRow In DB.GetStatsExecutingRunbooks().Rows
      SetRowValue(gridStatsByServer, objRow("RunbookServer"), objRow("Running"))

      If CBool(objRow("IsMonitor")) Or objRow("Name").ToString().ToUpper().StartsWith("MONITOR - ") Then
        SetRowValue(gridStatsType, "Monitor", objRow("Running"))
      Else
        SetRowValue(gridStatsType, "Ad-hoc", objRow("Running"))
      End If
    Next

    For Each objRow As DataGridViewRow In gridStatsByServer.Rows
      If objRow.Cells("colMaxRunningPolicies").Value > 0 And objRow.Cells("colRunningRunbooks").Value > 0 Then
        tmpServerUsagePercentage = objRow.Cells("colRunningRunbooks").Value * 100 / objRow.Cells("colMaxRunningPolicies").Value
      Else
        tmpServerUsagePercentage = 0
      End If

      objRow.Cells("colUsagePercent").Value = tmpServerUsagePercentage.ToString("F2") & "%"

      If tmpServerUsagePercentage <= 50 Then
        objRow.Cells("colUsagePercent").Style.BackColor = Color.FromArgb(219, 231, 195)
      ElseIf tmpServerUsagePercentage <= 75 Then
        objRow.Cells("colUsagePercent").Style.BackColor = Color.FromArgb(155, 187, 89)
      ElseIf tmpServerUsagePercentage <= 90 Then
        objRow.Cells("colUsagePercent").Style.BackColor = Color.FromArgb(255, 255, 129)
      Else
        objRow.Cells("colUsagePercent").Style.BackColor = Color.FromArgb(150, 0, 0)
        objRow.Cells("colUsagePercent").Style.ForeColor = Color.White
      End If
    Next

    For Each objRow As DataGridViewRow In gridStatsType.Rows
      If objRow.Cells("colRunbookType").Value = "Monitor" Then
        If objRow.Cells("colRunningRunbooks2").Value < objRow.Cells("colEnabledRunbooks").Value Then
          objRow.Cells("colRunningRunbooks2").Style.BackColor = Color.FromArgb(150, 0, 0)
          objRow.Cells("colRunningRunbooks2").Style.ForeColor = Color.White
        ElseIf objRow.Cells("colRunningRunbooks2").Value > objRow.Cells("colEnabledRunbooks").Value Then
          objRow.Cells("colRunningRunbooks2").Style.BackColor = Color.FromArgb(255, 255, 129)
        End If
      End If
    Next

    lblLastUpdate.Text = "Updated At: " & Now.ToString("MM/dd/yyyy HH:mm:ss")
  End Sub

  Private Sub SetRowValue(ByRef grid As DataGridView, Filter As String, Value As Integer)
    For Each objRow As DataGridViewRow In grid.Rows
      If objRow.Cells(0).Value.ToString().ToUpper() = Filter.ToUpper() Then
        objRow.Cells(1).Value = CInt(objRow.Cells(1).Value) + Value
        Exit For
      End If
    Next
  End Sub

  Private Sub cmdRefresh_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefresh.Click
    RefreshData()
  End Sub

  Private Sub RefreshToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles RefreshToolStripMenuItem1.Click
    RefreshData()
  End Sub

  Private Sub cmdMonitorRunbooksDetails_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles cmdMonitorRunbooksDetails.LinkClicked
    dlgRunningStatistics_MonitorRunbooks.ShowDialog()
  End Sub

  Private Sub MonitorToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MonitorToolStripMenuItem.Click
    dlgRunningStatistics_MonitorRunbooks.ShowDialog()
  End Sub
End Class
