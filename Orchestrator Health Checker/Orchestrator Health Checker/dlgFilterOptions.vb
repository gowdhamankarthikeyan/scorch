Imports System.Windows.Forms

Public Class dlgFilterOptions
  Public ReadOnly Property RunbookServer As String
    Get
      If rbtRunbookServer.Checked Then
        Return cmbRunbookServer.SelectedItem.ToString()
      Else
        Return ""
      End If
    End Get
  End Property

  Public ReadOnly Property RunbookName As String
    Get
      If rbtRunbookName.Checked Then
        Return txtRunbookName.Text
      Else
        Return ""
      End If
    End Get
  End Property

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub rbtRunbookServer_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtRunbookServer.CheckedChanged
    If rbtRunbookServer.Checked Then
      cmbRunbookServer.Enabled = True
      txtRunbookName.Enabled = False
    End If
  End Sub

  Private Sub rbtRunbookName_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtRunbookName.CheckedChanged
    If rbtRunbookName.Checked Then
      cmbRunbookServer.Enabled = False
      txtRunbookName.Enabled = True
    End If
  End Sub

  Public Sub Reset()
    cmbRunbookServer.Items.Clear()
    cmbRunbookServer.Text = ""
    txtRunbookName.Text = ""
    rbtNoFilter.Checked = True

    cmbRunbookServer.Enabled = False
    txtRunbookName.Enabled = False
  End Sub

  Private Sub rbtNoFilter_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbtNoFilter.CheckedChanged
    cmbRunbookServer.Enabled = False
    txtRunbookName.Enabled = False
  End Sub
End Class
