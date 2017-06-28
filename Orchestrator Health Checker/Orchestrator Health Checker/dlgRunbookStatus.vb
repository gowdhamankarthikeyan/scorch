Imports System.Windows.Forms

Public Class dlgRunbookStatus
  Private _RunbookName As String = ""
  Private _RunbookID As String = ""

  Sub New(RunbookName As String, RunbookID As String)
    InitializeComponent()

    _RunbookName = RunbookName
    _RunbookID = RunbookID

    Me.Text = "Runbook: " & _RunbookName
  End Sub

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgRunbookStatus_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    LoadGrids()

    lblRunbookName.Text = "Runbook: " & _RunbookName
    txtPath.Text = DB.GetRunbookPath(_RunbookID)
    txtPath.Text = txtPath.Text.Substring(1, txtPath.Text.LastIndexOf("\") - 1)
  End Sub

  Private Sub LoadGrids()
    Dim prevSortingColumn As String = Nothing
    Dim prevSortingOrder As Integer = Nothing

    If gridProcessIDs.SortedColumn IsNot Nothing Then
      prevSortingColumn = gridProcessIDs.SortedColumn.Name
      prevSortingOrder = gridProcessIDs.SortOrder - 1
    End If

    Dim dtResult As DataTable = DB.GetRunbookServerDetails(_RunbookID)
    Dim dtServerSummary As New DataTable

    dtServerSummary.Columns.Add("RunbookServer")
    dtServerSummary.Columns.Add("Count", Type.GetType("System.Int32"))
    dtServerSummary.AcceptChanges()

    For Each objProcess As DataRow In dtResult.Rows
      If dtServerSummary.Select("RunbookServer='" & objProcess("Computer") & "'").Length = 0 Then
        dtServerSummary.Rows.Add(New Object() {objProcess("Computer"), 1})
      Else
        dtServerSummary.Select("RunbookServer='" & objProcess("Computer") & "'")(0)("Count") += 1
      End If
    Next

    gridRunningOn.DataSource = dtServerSummary
    gridRunningOn.Refresh()

    gridProcessIDs.DataSource = dtResult
    gridProcessIDs.Refresh()

    If Not String.IsNullOrEmpty(prevSortingColumn) Then
      gridProcessIDs.Sort(gridProcessIDs.Columns(prevSortingColumn), prevSortingOrder)
    End If

    lblLastUpdate.Text = "Updated: " & Now.ToString("MM/dd/yyyy HH:mm:ss")
  End Sub

  Private Sub gridProcessIDs_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridProcessIDs.CellDoubleClick
    Dim objInitData As New dlgInitialData(_RunbookName, gridProcessIDs.Rows(e.RowIndex).Cells("colServer").Value, gridProcessIDs.Rows(e.RowIndex).Cells("colProcessID").Value, gridProcessIDs.Rows(e.RowIndex).Cells("colTimeStarted").Value)
    objInitData.ShowDialog()
  End Sub

  Private Sub cmdRefresh_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefresh.Click
    LoadGrids()
  End Sub

  Private Sub cmdAnalyzeOrphans_Click(sender As System.Object, e As System.EventArgs) Handles cmdAnalyzeOrphans.Click
    Dim tmpDT As New DataTable

    tmpDT.Columns.Add("RunbookServer")
    tmpDT.Columns.Add("PID")

    For Each objGridRow As DataGridViewRow In gridProcessIDs.Rows
      tmpDT.Rows.Add(New Object() {objGridRow.Cells("colServer").Value, objGridRow.Cells("colProcessID").Value})
    Next

    Dim tmp As New dlgShowOrphans(_RunbookName, tmpDT)

    tmp.ShowDialog()
    tmp.Dispose()
    GC.Collect()
  End Sub
End Class
