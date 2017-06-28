Imports System.Windows.Forms

Public Class dlgAdminOptions
  Private boolStopRunningRunbooks As Boolean = False
  Private boolStopMonitoringRunbooks As Boolean = False

  Sub New()

    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call
    objHelp.SetHelpString(chkStopRunningRunbooks, "Stops all running runbooks by updating the database (no webservice is required). This action is accomplished by:" & vbNewLine & vbNewLine & _
                          " • Deleting all entries from [POLICY_PUBLISH_QUEUE] where AssignedActionServer is not set" & vbNewLine & _
                          " • Set StopRequested = 1 for any record in [POLICY_PUBLISH_QUEUE] where AssignedActionServer is set.")

    objHelp.SetHelpString(chkStopMonitoringRunbooks, "Stops all running monitoring runbooks by updating the database (no webservice is required). This action is accomplished by:" & vbNewLine & vbNewLine & _
                          " • Deleting all entries from [POLICY_PUBLISH_QUEUE] where AssignedActionServer is not set and the runbook name starts with ""Monitor """ & vbNewLine & _
                          " • Set StopRequested = 1 for any record in [POLICY_PUBLISH_QUEUE] where AssignedActionServer is set and the runbook name starts with ""Monitor """)

    objHelp.SetHelpString(chkCleanOrphans, "Cleans all orphaned runbook instances from the database by calling the ""[Microsoft.SystemCenter.Orchestrator.Runtime.Internal].[ClearOrphanedRunbookInstances]"" stored procedure.")

    objHelp.SetHelpString(chkLogPurge, "Purges the main Orchestrator database tables.  This is NOT the same as executing ""Log Purge"" from Runbook Designer. Instead, this step executes the following SQL commands:" & vbNewLine & vbNewLine & _
                          " • DELETE FROM POLICYINSTANCES" & vbNewLine & _
                          " • TRUNCATE TABLE OBJECTINSTANCES" & vbNewLine & _
                          " • TRUNCATE TABLE OBJECTINSTANCEDATA" & vbNewLine & _
                          " • TRUNCATE TABLE [Microsoft.SystemCenter.Orchestrator.Runtime.Internal].Jobs")

    objHelp.SetHelpString(chkStartMonitoring, "Starts all monitor runbooks by using the values ""_RB Start Monitors"" and ""_SCORCH Web Service"" set in the .config file for the current environment." & vbNewLine)
  End Sub

  Public ReadOnly Property ActionsToTake
    Get
      ActionsToTake = ""

      If chkStopRunningRunbooks.Checked Then
        ActionsToTake &= "stop"
      End If

      If chkStopMonitoringRunbooks.Checked Then
        If Not String.IsNullOrEmpty(ActionsToTake) Then
          ActionsToTake &= ";"
        End If

        ActionsToTake &= "stopmonitor"
      End If

      If chkCleanOrphans.Checked Then
        If Not String.IsNullOrEmpty(ActionsToTake) Then
          ActionsToTake &= ";"
        End If

        ActionsToTake &= "clean"
      End If

      If chkLogPurge.Checked Then
        If Not String.IsNullOrEmpty(ActionsToTake) Then
          ActionsToTake &= ";"
        End If

        ActionsToTake &= "purge"
      End If

      If chkStartMonitoring.Checked Then
        If Not String.IsNullOrEmpty(ActionsToTake) Then
          ActionsToTake &= ";"
        End If

        ActionsToTake &= "start"
      End If

      ActionsToTake = ";" & ActionsToTake & ";"
    End Get
  End Property

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If chkCleanOrphans.Checked Then
      If Not chkStopMonitoringRunbooks.Checked And Not chkStopRunningRunbooks.Checked Then
        If MessageBox.Show("Clean Orphan Runbooks option is checked; however, you haven't checked any option to STOP running Runbooks. Are you sure you want to continue?", "Clean Up Without Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
          Return
        End If
      End If
    End If

    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub chkStopRunningRunbooks_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkStopRunningRunbooks.CheckedChanged
    If chkStopRunningRunbooks.Enabled Then
      boolStopRunningRunbooks = chkStopRunningRunbooks.Checked

      chkStopMonitoringRunbooks.Enabled = Not chkStopRunningRunbooks.Checked
      chkStopMonitoringRunbooks.Checked = If(chkStopRunningRunbooks.Checked, False, boolStopMonitoringRunbooks)
    End If
  End Sub

  Private Sub chkLogPurge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLogPurge.CheckedChanged
    chkStopRunningRunbooks.Enabled = Not chkLogPurge.Checked
    chkStopMonitoringRunbooks.Enabled = Not chkLogPurge.Checked

    If chkLogPurge.Checked Then
      chkStopRunningRunbooks.Checked = True
      chkStopMonitoringRunbooks.Checked = False
    Else
      chkStopRunningRunbooks.Checked = boolStopRunningRunbooks
      chkStopMonitoringRunbooks.Checked = boolStopMonitoringRunbooks
    End If
  End Sub

  Private Sub chkStopMonitoringRunbooks_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkStopMonitoringRunbooks.CheckedChanged
    If chkStopMonitoringRunbooks.Enabled Then
      boolStopMonitoringRunbooks = chkStopMonitoringRunbooks.Checked
    End If
  End Sub
End Class
