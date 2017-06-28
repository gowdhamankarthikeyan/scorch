Imports System.Windows.Forms

Public Class dlgSystemHealth

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgSystemHealth_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    lblPolicyInstances.Text = "POLICYINSTANCES Table = " & DB.GetTableCount("POLICYINSTANCES").ToString("##,#") & " records"
    lblObjectInstances.Text = "OBJECTINSTANCES Table = " & DB.GetTableCount("OBJECTINSTANCES").ToString("##,#") & " records"
    lblObjectData.Text = "OBJECTINSTANCEDATA Table = " & DB.GetTableCount("OBJECTINSTANCEDATA").ToString("##,#") & " records"
    lblJobs.Text = "Jobs Table = " & DB.GetTableCount("[Microsoft.SystemCenter.Orchestrator.Runtime.Internal].Jobs").ToString("##,#") & " records"

    Dim dtStart As DateTime = Nothing
    Dim dtEnd As DateTime = Nothing
    Dim boolCompleted As Boolean = False

    DB.GetLastLogPurgeEvent(dtStart, dtEnd, boolCompleted)

    If dtStart <> Nothing Then
      lblLogPurgeStart.Text = "Start Time: " & dtStart.ToString("MM/dd/yyyy HH:mm:ss")
    Else
      lblLogPurgeStart.Text = "Start Time: No Info"
    End If

    If dtEnd <> Nothing Then
      lblLogPurgeEnd.Text = "End Time: " & dtEnd.ToString("MM/dd/yyyy HH:mm:ss")
    Else
      lblLogPurgeEnd.Text = "End Time: No Info"
    End If

    If dtStart <> Nothing And dtEnd <> Nothing Then
      lblLogPurgeDuration.Text = "Duration: " & Common.DurationToText(New TimeSpan(dtEnd.Ticks).Subtract(New TimeSpan(dtStart.Ticks)).TotalMinutes.ToString("N2"), True)
      lblLogPurgeCompleted.Text = "Completed Successfully: " & If(boolCompleted, "Yes", "No")
    Else
      lblLogPurgeDuration.Text = "Duration: No Info"
      lblLogPurgeCompleted.Text = "Completed Successfully: No Info"
    End If
  End Sub
End Class
