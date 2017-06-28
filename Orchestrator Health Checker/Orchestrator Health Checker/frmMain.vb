Imports Orchestrator_Health_Checker.wsSCORCH
Imports System.IO
Imports System.Xml

Public Class frmMain
  Private objWS_SCORCH As OrchestratorContext = Nothing
  Private objProgress As dlgProgress = Nothing
  Private boolValidEnvironment As Boolean = True

  Private _FilterRunbookServer As String = ""
  Private _FilterRunbookName As String = ""

  Private Delegate Sub ProgressMoveNext()
  Private Delegate Sub ProgressShowError()

  Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    For Each item As Configuration.ConnectionStringSettings In Configuration.ConfigurationManager.ConnectionStrings
      If item.Name.Contains("_") Then
        cmbEnvironment.Items.Add(item.Name.Split("_")(0))
      End If
    Next

    cmbEnvironment.SelectedIndex = 0

    workerLoadIPs.RunWorkerAsync()
  End Sub

  Private Sub ChangeSCORCHWS()
    If Not boolValidEnvironment Then
      Return
    End If

    Dim tmpSCORCHUrl As Uri = Nothing
    Dim tmpSCORCHRB As String = ""

    Try
      tmpSCORCHUrl = New Uri(Configuration.ConfigurationManager.AppSettings(GlobalVariables.strCurrentConnectionSuffix & "_SCORCH Web Service"))
      tmpSCORCHRB = Configuration.ConfigurationManager.AppSettings(GlobalVariables.strCurrentConnectionSuffix & "_RB Start Monitors")
    Catch ex As Exception
      tmpSCORCHUrl = Nothing
      tmpSCORCHRB = ""
    End Try

    If tmpSCORCHUrl IsNot Nothing Then
      objWS_SCORCH = New OrchestratorContext(tmpSCORCHUrl)
      objWS_SCORCH.Credentials = System.Net.CredentialCache.DefaultCredentials
    Else
      objWS_SCORCH = Nothing
    End If

    If tmpSCORCHUrl Is Nothing Or tmpSCORCHRB = "" Then
      dlgAdminOptions.chkStartMonitoring.Enabled = False
      dlgAdminOptions.chkStartMonitoring.Checked = False
    Else
      dlgAdminOptions.chkStartMonitoring.Enabled = True
    End If
  End Sub

  Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
    Application.Exit()
  End Sub

  Private Sub UpdateGrid()
    If Not boolValidEnvironment Then
      Return
    End If

    Dim prevSortingColumn As String = Nothing
    Dim prevSortingOrder As Integer = Nothing
    Dim prevSelectedRow As String = Nothing
    Dim intTotalRunningRunbooks As Integer = 0
    Dim intQueuedRunnningRunbooks As Integer = 0

    If gridRunbooks.SortedColumn IsNot Nothing Then
      prevSortingColumn = gridRunbooks.SortedColumn.Name
      prevSortingOrder = gridRunbooks.SortOrder - 1

      If Not gridRunbooks.Columns("colRunbookServer").Visible And prevSortingColumn = "colRunbookServer" Then
        prevSortingColumn = Nothing
        prevSortingOrder = Nothing
      End If
    End If

    If gridRunbooks.SelectedRows.Count > 0 Then
      prevSelectedRow = gridRunbooks.SelectedRows(0).Cells("colRunbookName").Value
    End If

    gridRunbooks.DataSource = DB.GetExecutingRunbooks(ShowRunbookServerToolStripMenuItem.Checked, _FilterRunbookServer, _FilterRunbookName)
    gridRunbooks.Refresh()

    If Not String.IsNullOrEmpty(prevSortingColumn) Then
      gridRunbooks.Sort(gridRunbooks.Columns(prevSortingColumn), prevSortingOrder)
    End If

    If Not String.IsNullOrEmpty(prevSelectedRow) Then
      gridRunbooks.ClearSelection()
    End If

    For Each row As DataGridViewRow In gridRunbooks.Rows
      If Not String.IsNullOrEmpty(prevSelectedRow) Then
        If row.Cells("colRunbookName").Value = prevSelectedRow Then
          row.Selected = True
        End If
      End If

      intTotalRunningRunbooks += row.Cells("colRunning").Value
      intQueuedRunnningRunbooks += row.Cells("colQueued").Value
    Next

    If _FilterRunbookName <> "" Then
      SetFilter("Runbook Name CONTAINS """ & _FilterRunbookName & """")
    ElseIf _FilterRunbookServer <> "" Then
      SetFilter("Runbook Server IS " & _FilterRunbookServer)
    Else
      SetFilter("")
    End If

    lblLastUpdate.Text = "Last Updated At: " & Now.ToString("MM/dd/yyyy HH:mm:ss")
    lblRunningRunbooksCount.Text = "Running Runbooks: " & intTotalRunningRunbooks
    lblQueuedRunbooksCount.Text = "Queued Runbooks: " & intQueuedRunnningRunbooks
    frmMain_Resize(Nothing, Nothing)
  End Sub

  Private Sub chkAutoUpdate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAutoUpdate.CheckedChanged
    cmbEnvironment.Enabled = Not chkAutoUpdate.Checked

    If chkAutoUpdate.Checked Then
      autoUpdate.Interval = txtUpdateEvery.Value * 1000
      autoUpdate.Enabled = True
      autoUpdate.Start()
    Else
      autoUpdate.Stop()
      autoUpdate.Enabled = False
    End If
  End Sub

  Private Sub autoUpdate_Tick(sender As Object, e As System.EventArgs) Handles autoUpdate.Tick
    autoUpdate.Stop()
    UpdateGrid()
    autoUpdate.Start()
  End Sub

  Private Sub txtUpdateEvery_ValueChanged(sender As System.Object, e As System.EventArgs) Handles txtUpdateEvery.ValueChanged
    If autoUpdate.Enabled Then
      autoUpdate.Stop()
      autoUpdate.Interval = txtUpdateEvery.Value * 1000
      autoUpdate.Start()
    End If
  End Sub

  Private Sub worker_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles worker.DoWork
    Dim tmpActionsToTake As String = e.Argument
    Dim actMoveNext As New ProgressMoveNext(AddressOf objProgress.MoveToNext)
    Dim actShowError As New ProgressShowError(AddressOf objProgress.ShowError)

    If tmpActionsToTake.Contains(";stop;") Then
      If Not DB.StopAllRunningRunbooks() Then
        Me.Invoke(actShowError)
        Return
      End If

      If objProgress.CancelRequested Then
        Me.Invoke(actShowError)
        e.Cancel = True
        Return
      End If

      While DB.GetRunningRunbooksCount() <> 0
        Threading.Thread.Sleep(5000)

        If objProgress.CancelRequested Then
          Me.Invoke(actShowError)
          e.Cancel = True
          Return
        End If
      End While

      Me.Invoke(actMoveNext)

      If objProgress.CancelRequested Then
        Me.Invoke(actShowError)
        e.Cancel = True
        Return
      End If
    End If

    If tmpActionsToTake.Contains(";stopmonitor;") Then
      If Not DB.StopAllMonitoringRunbooks() Then
        Me.Invoke(actShowError)
        Return
      End If

      If objProgress.CancelRequested Then
        Me.Invoke(actShowError)
        e.Cancel = True
        Return
      End If

      While DB.GetRunningRunbooksCount(True) <> 0
        Threading.Thread.Sleep(5000)

        If objProgress.CancelRequested Then
          Me.Invoke(actShowError)
          e.Cancel = True
          Return
        End If
      End While

      Me.Invoke(actMoveNext)

      If objProgress.CancelRequested Then
        Me.Invoke(actShowError)
        e.Cancel = True
        Return
      End If
    End If

    If tmpActionsToTake.Contains(";clean;") Then
      If Not DB.Execute_ClearOrphanedRunbookInstances() Then
        Me.Invoke(actShowError)
        Return
      End If

      If objProgress.CancelRequested Then
        Me.Invoke(actShowError)
        e.Cancel = True
        Return
      End If

      Me.Invoke(actMoveNext)
    End If

    If tmpActionsToTake.Contains(";purge;") Then
      If Not DB.LogPurge() Then
        Me.Invoke(actShowError)
        Return
      End If

      Me.Invoke(actMoveNext)
    End If

    If tmpActionsToTake.Contains(";start;") Then
      Threading.Thread.Sleep(5000)

      If Not StartRunbook(Configuration.ConfigurationManager.AppSettings(GlobalVariables.strCurrentConnectionSuffix & "_RB Start Monitors")) Then
        Me.Invoke(actShowError)
        Return
      End If
    End If

    Me.Invoke(actMoveNext)
  End Sub

  Private Sub worker_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
    If e.Cancelled Then
      objProgress.cmdCancel.Enabled = True
      objProgress.cmdCancel.Text = "Close"
    End If
  End Sub

  Private Sub gridRunbooks_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gridRunbooks.KeyDown
    If e.KeyCode = Keys.F5 Then
      UpdateGrid()
    End If
  End Sub

  Private Sub AdminOptionsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AdminOptionsToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    If dlgAdminOptions.ShowDialog() = Windows.Forms.DialogResult.OK Then
      StartActions(dlgAdminOptions.ActionsToTake)
    End If

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub StartActions(strActionsToStart As String)
    objProgress = New dlgProgress(strActionsToStart)

    chkAutoUpdate.Enabled = False

    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
      autoUpdate.Enabled = False
    End If

    worker.RunWorkerAsync(strActionsToStart)

    objProgress.ShowDialog()

    UpdateGrid()

    If chkAutoUpdate.Checked Then
      autoUpdate.Enabled = True
      autoUpdate.Start()
    End If

    chkAutoUpdate.Enabled = True
  End Sub

  Private Function StartRunbook(strRunbookName As String) As Boolean
    Dim thisRunbook As Runbook = Nothing
    StartRunbook = True

    Try
      thisRunbook = objWS_SCORCH.Runbooks.Where(Function(runbookParam) runbookParam.Name = strRunbookName).FirstOrDefault
    Catch ex As Exception
      thisRunbook = Nothing
    End Try

    If thisRunbook IsNot Nothing Then
      Dim newJob As New Job
      newJob.RunbookId = thisRunbook.Id

      Try
        objWS_SCORCH.AddToJobs(newJob)
        objWS_SCORCH.SaveChanges()
        StartRunbook = True
      Catch ex As Exception
        StartRunbook = False
      End Try
    End If
  End Function

  Private Sub frmMain_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize
    If Me.WindowState <> FormWindowState.Minimized Then
      Dim objVScroll As VScrollBar = gridRunbooks.Controls.OfType(Of VScrollBar)().First()

      splitCont.Width = Me.Width - If(objVScroll.Visible, 10, 0)
      splitCont.Height = Me.Height - 107
    End If
  End Sub

  Private Sub cmbEnvironment_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbEnvironment.SelectedIndexChanged
    ChangeEnvironment()
    ChangeSCORCHWS()
    UpdateGrid()
  End Sub

  Private Sub ChangeEnvironment()
    Dim strTemp As String = ""

    GlobalVariables.strCurrentConnectionSuffix = cmbEnvironment.SelectedItem.ToString()
    strTemp = DB.CheckDBConnection

    If strTemp <> "OK" Then
      MessageBox.Show(strTemp, "DB Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      boolValidEnvironment = False
    Else
      boolValidEnvironment = True
    End If

    ChangeUI()

    If boolValidEnvironment Then
      dlgFilterOptions.Reset()

      dlgStopRunbooksOn.cmbRunbookServers.Items.Clear()
      dlgStopRunbooksOn.cmbRunbookServers.SelectedIndex = -1
      dlgStopRunbooksOn.cmbRunbookServers.Text = ""

      For Each objRow As DataRow In DB.GetRunbookServers().Rows
        dlgStopRunbooksOn.cmbRunbookServers.Items.Add(objRow("Computer"))
        dlgFilterOptions.cmbRunbookServer.Items.Add(objRow("Computer"))
      Next

      _FilterRunbookName = ""
      _FilterRunbookServer = ""
    End If
  End Sub

  Private Sub ChangeUI()
    ViewToolStripMenuItem.Enabled = boolValidEnvironment
    OptionsToolStripMenuItem.Enabled = boolValidEnvironment
    cmdRefresh.Enabled = boolValidEnvironment
    cmdStartMonitoringRunbooks.Enabled = boolValidEnvironment
    cmdStopAllRunbooks.Enabled = boolValidEnvironment
    cmdStopMonitoringRunbooks.Enabled = boolValidEnvironment
    chkAutoUpdate.Enabled = boolValidEnvironment
    txtUpdateEvery.Enabled = boolValidEnvironment
    gridRunbooks.Enabled = boolValidEnvironment
    AuditToolStripMenuItem.Enabled = boolValidEnvironment
  End Sub

  Private Sub SystemHealthToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SystemHealthToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgSystemHealth.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub LogPurgeTrendToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LogPurgeTrendToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgLogPurgeTrend.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub cmdRefresh_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefresh.Click
    UpdateGrid()
  End Sub

  Private Sub AboutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AboutToolStripMenuItem.Click
    AboutBox.ShowDialog()
  End Sub

  Private Sub cmdStartMonitoringRunbooks_Click(sender As System.Object, e As System.EventArgs) Handles cmdStartMonitoringRunbooks.Click
    StartActions(";start;")
  End Sub

  Private Sub cmdStopMonitoringRunbooks_Click(sender As System.Object, e As System.EventArgs) Handles cmdStopMonitoringRunbooks.Click
    StartActions(";stopmonitor;")
  End Sub

  Private Sub cmdStopAllRunbooks_Click(sender As System.Object, e As System.EventArgs) Handles cmdStopAllRunbooks.Click
    StartActions(";stop;")
  End Sub

  Private Sub ShowRunbookServerToolStripMenuItem_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ShowRunbookServerToolStripMenuItem.CheckedChanged
    gridRunbooks.Columns("colRunbookServer").Visible = ShowRunbookServerToolStripMenuItem.Checked

    If gridRunbooks.Columns("colRunbookServer").Visible Then
      gridRunbooks.Columns("colRunbookServer").DataPropertyName = "RunbookServer"
    Else
      gridRunbooks.Columns("colRunbookServer").DataPropertyName = Nothing
    End If

    UpdateGrid()
  End Sub

  Private Sub gridRunbooks_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridRunbooks.CellDoubleClick
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    Dim objDlg As New dlgRunbookStatus(gridRunbooks.Rows(e.RowIndex).Cells("colRunbookName").Value, gridRunbooks.Rows(e.RowIndex).Cells("colUniqueID").Value)
    objDlg.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub StopAllRunbookOnToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StopAllRunbookOnToolStripMenuItem.Click
    If dlgStopRunbooksOn.ShowDialog() = Windows.Forms.DialogResult.OK Then
      dlgStopRunbooksOn.cmbRunbookServers.SelectedIndex = -1
      dlgStopRunbooksOn.cmbRunbookServers.Text = ""
      UpdateGrid()
    End If
  End Sub

  Private Sub SearchByPIDToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SearchByPIDToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgPID.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub AuditRunbooksToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AuditRunbooksToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgRunbookAudit.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub ReindexTablesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReindexTablesToolStripMenuItem.Click
    If DB.ReIndexTables() Then
      MessageBox.Show("Tables were successfully reindexed", "Table Reindexed", MessageBoxButtons.OK, MessageBoxIcon.Information)
    Else
      MessageBox.Show("An unexpected error occurred while trying to reindex tables", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End If
  End Sub

  Private Sub UpdateStatisticsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UpdateStatisticsToolStripMenuItem.Click
    If DB.UpdateStatistics() Then
      MessageBox.Show("Runbook Statistics were successfully updated", "Runbook Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information)
    Else
      MessageBox.Show("An unexpected error occurred while trying to update Runbook Statistics", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End If
  End Sub

  Private Sub TableSizeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TableSizeToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgTablesSize.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub FileUsageAndSizeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FileUsageAndSizeToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgFileUsageSize.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub CheckFragmentationLevelsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CheckFragmentationLevelsToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgFragmentationLevels.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub ClientConnectionsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClientConnectionsToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgClientConnections.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub workerLoadIPs_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles workerLoadIPs.DoWork
    Dim tmpIP As clsUserPassTableUpdate = Nothing

    GlobalVariables.colUserPassTableUpdate = New List(Of clsUserPassTableUpdate)

    If File.Exists(Application.StartupPath & "\ActivitiesInfo.xml") Then
      Dim xmlIPs As New XmlDocument

      Try
        xmlIPs.Load(Application.StartupPath & "\ActivitiesInfo.xml")
      Catch ex As Exception

      End Try

      Dim xmlIPsList As XmlNode = Nothing

      Try
        xmlIPsList = xmlIPs.GetElementsByTagName("Activities")(0)
      Catch ex As Exception

      End Try

      If xmlIPsList Is Nothing Then
        Return
      End If

      For Each xmlIP As XmlNode In xmlIPsList.ChildNodes
        tmpIP = New clsUserPassTableUpdate(GlobalVariables.colUserPassTableUpdate.Count)

        If xmlIP.Attributes("Name") IsNot Nothing Then
          tmpIP.DisplayName = xmlIP.Attributes("Name").Value
        End If

        For Each xmlChildNode As XmlNode In xmlIP.ChildNodes
          Select Case xmlChildNode.Name.ToLower
            Case "tablename"
              tmpIP.TableName = xmlChildNode.InnerText
            Case "domainnamefield"
              tmpIP.DomainNameField = xmlChildNode.InnerText
            Case "usernamefield"
              tmpIP.UserNameField = xmlChildNode.InnerText
            Case "passwordfield"
              tmpIP.PasswordField = xmlChildNode.InnerText
            Case "condition"
              tmpIP.WhereCondition = xmlChildNode.InnerText
            Case Else
          End Select
        Next

        GlobalVariables.colUserPassTableUpdate.Add(tmpIP)
      Next
    End If
  End Sub

  Private Sub workerLoadIPs_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles workerLoadIPs.RunWorkerCompleted
    ChangeUserPasswordToolStripMenuItem.Enabled = True
  End Sub

  Private Sub ChangeUserPasswordToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ChangeUserPasswordToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    Dim objTmp As New dlgChangeUserPassword

    objTmp.ShowDialog()

    objTmp.Dispose()
    GC.Collect()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub SearchRunbookByActivityToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SearchObjectToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgSearchObject.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub InvokeActivityByPathToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InvokeActivityByPathToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgAudit_InvokeRunbookActByPath.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub QuickRebuildToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles QuickRebuildToolStripMenuItem.Click
    If MessageBox.Show("This action will force SCOrch Web Service to rebuild its Access Control (accomplished by Truncating the table: ""[Microsoft.SystemCenter.Orchestrator.Internal].[AuthorizationCache]""). Verify rights have been properly granted before executing this action. Are you sure you want to continue?", "Flush SCOrch Web Service", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
      If DB.FlushWebserviceCache() Then
        MessageBox.Show("SCOrch Web Service has been flushed", "Web Service Flushed", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Else
        MessageBox.Show("An unexpected error occurred while trying to flush SCOrch Web Service", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If
    End If
  End Sub

  Private Sub FullRebuildToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FullRebuildToolStripMenuItem.Click
    If MessageBox.Show("This action will force SCOrch Web Service to FULLY (this execution can last for several minutes) rebuild its Access Control (accomplished by Truncating the table: ""[Microsoft.SystemCenter.Orchestrator.Internal].[AuthorizationCache]""). Verify rights have been properly granted before executing this action. Are you sure you want to continue?", "Flush SCOrch Web Service", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
      If DB.FlushWebserviceCache(True) Then
        MessageBox.Show("SCOrch Web Service has been flushed", "Web Service Flushed", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Else
        MessageBox.Show("An unexpected error occurred while trying to flush SCOrch Web Service", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If
    End If
  End Sub

  Private Sub RunningStatisticsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RunningStatisticsToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgRunningStatistics.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub LoggingEnabledRunbookToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LoggingEnabledRunbookToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgLoggingEnabledRunbooks.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub ActivitiesUsingVariableMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ActivitiesUsingVariableMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    dlgSearchVariableUsage.ShowDialog()

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub

  Private Sub SetFilter(FilterOption)
    If FilterOption = "" Then
      lblFilter.Visible = False
    Else
      lblFilter.Visible = True
      lblFilter.Text = FilterOption
    End If
  End Sub

  Private Sub FilterViewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FilterViewToolStripMenuItem.Click
    If chkAutoUpdate.Checked Then
      autoUpdate.Stop()
    End If

    If dlgFilterOptions.ShowDialog() = Windows.Forms.DialogResult.OK Then
      _FilterRunbookName = dlgFilterOptions.RunbookName
      _FilterRunbookServer = dlgFilterOptions.RunbookServer

      UpdateGrid()
    End If

    If chkAutoUpdate.Checked Then
      UpdateGrid()
      autoUpdate.Start()
    End If
  End Sub
End Class
