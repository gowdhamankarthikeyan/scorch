Imports System.Windows.Forms

Public Class dlgInitialData
  Private _Runbook As String = ""
  Private _RunbookServer As String = ""
  Private _PID As Integer = 0
  Private _TimeStarted As DateTime = New DateTime(1900, 1, 1)

  Sub New(Runbook As String, RunbookServer As String, PID As Integer, TimeStarted As DateTime)
    InitializeComponent()

    _Runbook = Runbook
    _RunbookServer = RunbookServer
    _PID = PID
    _TimeStarted = TimeStarted

    Me.Text = "Initial Data For: " & _Runbook
    lblRunbookName.Text = "Runbook: " & _Runbook
    lblRunbookServer.Text = "Runbook Server: " & _RunbookServer
    lblTimeStarted.Text = "Time Started: " & TimeStarted.ToString()
  End Sub

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub dlgInitialData_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    gridInitialData.DataSource = DB.GetInitialParemetersFor(_Runbook, _RunbookServer, _PID)
    gridInitialData.Refresh()
  End Sub

  Private Sub cmdStop_Click(sender As System.Object, e As System.EventArgs) Handles cmdStop.Click
    If MessageBox.Show("This action will stop this Runbook and all its child tree (including grandchild runbooks, etc). Are you sure you want to continue?", "Stop Runbook Tree", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
      If Not DB.StopSingleRunbook(_Runbook, _RunbookServer, _PID, True) Then
        MessageBox.Show("An error ocurred while trying to stop the Runbook Tree", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Else
        MessageBox.Show("The Runbook Tree was successfully set to Stop", "Stop Requested", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
      End If
    End If
  End Sub
End Class
