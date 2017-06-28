Imports System.Windows.Forms

Public Class dlgSearchObject

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub dlgSearchRunbookByActivity_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    chkActivityType.Items.Clear()

    For Each objActivityType As DataRow In DB.GetActivityTypes().Rows
      chkActivityType.Items.Add(objActivityType("TypeName"))
    Next

    gridResult.DataSource = Nothing
    gridResult.Refresh()
  End Sub

  Private Sub lnkSelectAll_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectAll.LinkClicked
    For i As Integer = 0 To chkActivityType.Items.Count - 1
      chkActivityType.SetItemChecked(i, True)
    Next
  End Sub

  Private Sub lnkSelectNone_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectNone.LinkClicked
    For i As Integer = 0 To chkActivityType.Items.Count - 1
      chkActivityType.SetItemChecked(i, False)
    Next
  End Sub

  Private Sub lnkSelectInvert_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectInvert.LinkClicked
    For i As Integer = 0 To chkActivityType.Items.Count - 1
      chkActivityType.SetItemChecked(i, Not chkActivityType.GetItemChecked(i))
    Next
  End Sub

  Private Sub cmdSearch_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch.Click
    Dim colType As List(Of String) = Nothing

    If chkActivities.Checked Then
      If chkActivityType.CheckedItems.Count > 0 And chkActivityType.CheckedItems.Count <> chkActivityType.Items.Count Then
        colType = New List(Of String)

        For Each objItem As String In chkActivityType.CheckedItems
          colType.Add(objItem)
        Next
      End If
    End If

    'gridResult.DataSource = DB.GetActivities(colType, txtActivityName.Text)
    gridResult.DataSource = DB.GetObjects(chkActivities.Checked, chkRunbooks.Checked, chkFolders.Checked, txtObjectName.Text, colType)
    gridResult.Refresh()

    If gridResult.Rows.Count = 0 Then
      MessageBox.Show("There are no Activities matching the criteria", "No Activities", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End If
  End Sub
End Class
