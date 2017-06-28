Imports System.Windows.Forms

Public Class dlgProgress
  Private _CancelRequested As Boolean = False
  Private byteCurrentPhase As Byte
  Private colPhases As List(Of String)

  Public ReadOnly Property CancelRequested As Boolean
    Get
      Return _CancelRequested
    End Get
  End Property

  Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
    Get
      Dim cp As CreateParams = MyBase.CreateParams
      Const CS_DBLCLKS As Int32 = &H8
      Const CS_NOCLOSE As Int32 = &H200
      cp.ClassStyle = CS_DBLCLKS Or CS_NOCLOSE
      Return cp
    End Get
  End Property

  Sub New(enableOptions As String)
    InitializeComponent()

    colPhases = New List(Of String)
    byteCurrentPhase = 0

    If Not enableOptions.Contains(";stop;") Then
      picStopRunningRunbooks.Enabled = False
      lblStopRunningRunbooks.Enabled = False
      lblStopRunningRunbooks.ForeColor = SystemColors.InactiveCaptionText
    Else
      colPhases.Add("StopRunningRunbooks")
    End If

    If Not enableOptions.Contains(";stopmonitor;") Then
      picStopMonitoringRunbooks.Enabled = False
      lblStopMonitoringRunbooks.Enabled = False
      lblStopMonitoringRunbooks.ForeColor = SystemColors.InactiveCaptionText
    Else
      colPhases.Add("StopMonitoringRunbooks")
    End If

    If Not enableOptions.Contains(";clean;") Then
      picCleanOrphans.Enabled = False
      lblCleanOrphans.Enabled = False
      lblCleanOrphans.ForeColor = SystemColors.InactiveCaptionText
    Else
      colPhases.Add("CleanOrphans")
    End If

    If Not enableOptions.Contains(";purge;") Then
      picLogPurge.Enabled = False
      lblLogPurge.Enabled = False
      lblLogPurge.ForeColor = SystemColors.InactiveCaptionText
    Else
      colPhases.Add("LogPurge")
    End If

    If Not enableOptions.Contains(";start;") Then
      picStartMonitorRunbooks.Enabled = False
      lblStartMonitorRunbooks.Enabled = False
      lblStartMonitorRunbooks.ForeColor = SystemColors.InactiveCaptionText
    Else
      colPhases.Add("StartMonitorRunbooks")
    End If
  End Sub

  Public Sub MoveToNext()
    Dim currentPicBox As PictureBox = Me.Controls("pic" & colPhases(byteCurrentPhase))
    Dim nextPicBox As PictureBox = Nothing

    If byteCurrentPhase + 1 < colPhases.Count Then
      nextPicBox = Me.Controls("pic" & colPhases(byteCurrentPhase + 1))
    End If

    currentPicBox.Image = My.Resources.OK

    If nextPicBox IsNot Nothing Then
      nextPicBox.Image = My.Resources.Working
      byteCurrentPhase += 1
    Else
      cmdCancel.Text = "Close"
    End If
  End Sub

  Public Sub ShowError()
    Dim currentPicBox As PictureBox = Me.Controls("pic" & colPhases(byteCurrentPhase))
    currentPicBox.Image = My.Resources._Error
    cmdCancel.Text = "Close"
  End Sub

  Private Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
    If cmdCancel.Text = "Cancel" Then
      _CancelRequested = True
      cmdCancel.Enabled = False
    Else
      Me.Close()
    End If
  End Sub

  Private Sub dlgProgress_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    CType(Me.Controls("pic" & colPhases(byteCurrentPhase)), PictureBox).Image = My.Resources.Working
  End Sub
End Class
