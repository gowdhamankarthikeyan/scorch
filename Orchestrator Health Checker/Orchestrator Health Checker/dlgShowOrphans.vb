Imports System.Windows.Forms

Public Class dlgShowOrphans
  Private _ServerPIDList As DataTable = Nothing
  Private _RunbookName As String = ""

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Sub New(RunbookName As String, ServerPIDList As DataTable)
    InitializeComponent()

    Me.Text = "Analyzing Orphans for " & RunbookName

    _ServerPIDList = ServerPIDList
    _RunbookName = RunbookName

    _ServerPIDList.Columns.Add("IsOrphan", Type.GetType("System.Boolean"))
    _ServerPIDList.AcceptChanges()
  End Sub

  Private Sub dlgShowOrphans_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    gridOrphans.Rows.Clear()
    workerLoadProcess.RunWorkerAsync()
  End Sub

  Private Sub workerLoadProcess_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles workerLoadProcess.DoWork
    For Each objPID As DataRow In _ServerPIDList.Rows
      objPID("IsOrphan") = False

      Try
        If My.Computer.Network.Ping(objPID("RunbookServer")) Then
          Process.GetProcessById(objPID("PID"), objPID("RunbookServer"))
        End If
      Catch ex As Exception
        If ex.Message.ToLower().StartsWith("process with an id of " & objPID("PID") & " is not running") Then
          objPID("IsOrphan") = True
        End If
      End Try
    Next
  End Sub

  Private Sub workerLoadProcess_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles workerLoadProcess.RunWorkerCompleted
    For Each objPID As DataRow In _ServerPIDList.Rows
      gridOrphans.Rows.Add(New Object() {objPID("IsOrphan"), objPID("RunbookServer"), objPID("PID"), objPID("IsOrphan")})
    Next

    lblLoading.Visible = False
    progressLoading.Visible = False

    gridOrphans.Enabled = True
    cmdKillSelected.Enabled = True
    cmdKillSelected.Visible = True
    OK_Button.Enabled = True
  End Sub

  Private Sub cmdKillSelected_Click(sender As System.Object, e As System.EventArgs) Handles cmdKillSelected.Click
    For Each objRow As DataGridViewRow In gridOrphans.Rows
      If CBool(objRow.Cells("colKill").Value) Then
        DB.StopSingleRunbook(_RunbookName, objRow.Cells("colRunbookServer").Value, objRow.Cells("colPID").Value, False)
      End If
    Next

    Me.Close()
  End Sub
End Class
