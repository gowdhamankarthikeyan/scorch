Imports System.Configuration
Imports System.Windows.Forms

Public Class dlgLogPurgeTrend

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgLogPurgeTrend_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    Dim tmpDT As DataTable = DB.GetLogPurgeStatusForLast(ConfigurationManager.AppSettings("LogPurgeTrendDays"))
    gridLogPurgeData.DataSource = New DataView(tmpDT, "", "Day", DataViewRowState.CurrentRows)
    gridLogPurgeData.Refresh()

    Dim i As Integer = 0
    Dim sum As Double = 0

    For Each objRow As DataRow In tmpDT.Rows
      If Not IsDBNull(objRow("Duration")) Then
        sum += objRow("Duration")
        i += 1
      End If
    Next

    If i > 0 Then
      lblAverage.Text = "Log Purge Average: " & Common.DurationToText(sum / i)
    Else
      lblAverage.Text = "Log Purge Average: No Info"
    End If
  End Sub
End Class
