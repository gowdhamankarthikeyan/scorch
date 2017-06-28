Imports System.Windows.Forms
Imports System.IO

Public Class dlgChangeUserPassword
  Private colObjectsToUpdate As List(Of clsUserPassTableUpdate) = Nothing
  Private strNewPassword As String = ""
  Private strOldPassword As String = ""
  Private strUserIDToLookFor As String = ""
  Private strDomainToLookFor As String = ""
  Private boolUpdateVariables As Boolean = False
  Private boolUpdateConfigurations As Boolean = False
  Private objProgress As dlgPasswordChangeProgress = Nothing
  Private objLog As StreamWriter = Nothing

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If MessageBox.Show("By continuing, Orchestrator database will be modified. Are you sure you want to continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
      ExecutePasswordUpdate(False)
    End If
  End Sub

  Private Sub Simulate_Button_Click(sender As System.Object, e As System.EventArgs) Handles Simulate_Button.Click
    ExecutePasswordUpdate(True)
  End Sub

  Private Sub ExecutePasswordUpdate(Simulated As Boolean)
    If Not ValidateRequiredFields(Simulated) Then
      Return
    End If

    GlobalVariables.colUpdatedRunbooks = New List(Of String)

    Dim strLogFile As String = ""

    Try
      strLogFile = Application.StartupPath & "\" & Now.ToString("yyyyMMdd_HHmmss") & "_Password Update Process.log"
      objLog = New StreamWriter(strLogFile, False)
    Catch ex As Exception
      strLogFile = Environment.ExpandEnvironmentVariables("%TEMP%") & "\" & Now.ToString("yyyyMMdd_HHmmss") & "_Password Update Process.log"
      objLog = New StreamWriter(strLogFile, False)
    End Try

    objLog.AutoFlush = True

    If Simulated Then
      Common.WriteLog(objLog, "!!! SIMULATED !!!")
    End If

    Common.WriteLog(objLog, "Running On: " & Environment.MachineName)
    Common.WriteLog(objLog, "Current User: " & Environment.UserDomainName & "\" & Environment.UserName)
    Common.WriteLog(objLog, "Orchestrator Environment: " & GlobalVariables.strCurrentConnectionSuffix)
    Common.WriteLog(objLog, "Process Started")
    Common.WriteLog(objLog, "Looking for User ID:")

    strUserIDToLookFor = ""
    For i As Byte = 0 To chkUserIDFormats.Items.Count - 1
      If chkUserIDFormats.GetItemChecked(i) Then
        If strUserIDToLookFor <> "" Then
          strUserIDToLookFor &= vbCr
        End If

        Common.WriteLog(objLog, vbTab & chkUserIDFormats.Items(i))
        strUserIDToLookFor &= chkUserIDFormats.Items(i)
      End If
    Next

    strDomainToLookFor = txtDomain.Text
    Common.WriteLog(objLog, "Looking for User Domain: " & strDomainToLookFor.ToUpper())

    Common.WriteLog(objLog, "Activities to Update:")
    colObjectsToUpdate = New List(Of clsUserPassTableUpdate)
    For i As Integer = 0 To chkUpdateObjects.Items.Count - 1
      If chkUpdateObjects.GetItemChecked(i) Then
        colObjectsToUpdate.Add(chkUpdateObjects.Items(i))
        Common.WriteLog(objLog, vbTab & chkUpdateObjects.Items(i).ToString())
      End If
    Next

    strNewPassword = txtNewPassword.Text
    strOldPassword = txtOldPassword.Text
    boolUpdateVariables = chkUpdateVariables.Checked
    boolUpdateConfigurations = chkUpdateConfigurations.Checked

    Common.WriteLog(objLog, "Update Variables: " & boolUpdateVariables)
    Common.WriteLog(objLog, "Update Configurations: " & boolUpdateConfigurations)
    Common.WriteLog(objLog, "Is Simulation?: " & Simulated)

    objProgress = New dlgPasswordChangeProgress
    objProgress.lblUpdateUser.Text = "Updating Password For: " & txtUserID.Text

    If Simulated Then
      objProgress.lblUpdateUser.Text &= " (SIMULATION)"
    End If

    objProgress.progressBar.Maximum = colObjectsToUpdate.Count + If(boolUpdateConfigurations, 1, 0) + If(boolUpdateVariables, 1, 0) + If(Simulated, 0, 1)
    objProgress.progressBar.Value = 0

    workerPasswordChanger.RunWorkerAsync(Simulated)

    objProgress.ShowDialog()

    Common.WriteLog(objLog, "Process Completed")
    objLog.Close()

    Process.Start(strLogFile)

    MessageBox.Show("Execution Completed" & vbNewLine & "Log File: """ & strLogFile & """", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information)
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.Close()
  End Sub

  Private Sub dlgChangeUserPassword_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    If String.IsNullOrEmpty(GlobalVariables.strCurrentUserSID) Then
      GlobalVariables.strCurrentUserSID = Common.UserToSID(Environment.UserDomainName & "\" & Environment.UserName)
    End If

    chkUpdateObjects.Items.Clear()

    For Each objObject As clsUserPassTableUpdate In GlobalVariables.colUserPassTableUpdate
      chkUpdateObjects.Items.Add(objObject, True)
    Next

    For i As Byte = 0 To chkUserIDFormats.Items.Count - 1
      chkUserIDFormats.SetItemChecked(i, True)
    Next

    'colObjectsToUpdate = Nothing
    'strNewPassword = ""
    'strOldPassword = ""
    'strUserIDToLookFor = ""
    'boolUpdateConfigurations = False
    'boolUpdateVariables = False
  End Sub

  Private Sub txtUserID_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtUserID.KeyUp
    If txtDomain.Enabled Then
      If txtUserID.Text.Contains("\") Then
        txtDomain.Enabled = False
        txtDomain.Text = txtUserID.Text.Substring(0, txtUserID.Text.IndexOf("\"))
      End If
    ElseIf Not txtUserID.Text.Contains("\") Then
      txtDomain.Enabled = True
    End If

    UpdateUserIDFormats()
  End Sub

  Private Sub UpdateUserIDFormats()
    If txtUserID.Text.Contains("\") Then
      chkUserIDFormats.Items(0) = txtUserID.Text
      chkUserIDFormats.Items(1) = txtUserID.Text.Substring(txtUserID.Text.IndexOf("\") + 1)
      chkUserIDFormats.Items(2) = chkUserIDFormats.Items(1) & "@" & txtUserID.Text.Substring(0, txtUserID.Text.IndexOf("\")) & ".%"
    Else
      chkUserIDFormats.Items(0) = If(txtDomain.Text.Contains("."), txtDomain.Text.Substring(0, txtDomain.Text.IndexOf(".")), txtDomain.Text) & "\" & txtUserID.Text
      chkUserIDFormats.Items(1) = txtUserID.Text
      chkUserIDFormats.Items(2) = chkUserIDFormats.Items(1) & "@" & txtDomain.Text & "%"
    End If
  End Sub

  Private Sub txtDomain_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDomain.KeyUp
    UpdateUserIDFormats()
  End Sub

  Private Sub chkUpdateVariables_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUpdateVariables.CheckedChanged
    If chkUpdateVariables.Checked Then
      txtOldPassword.Enabled = True
      txtOldPasswordRepeat.Enabled = True
    Else
      If chkUpdateConfigurations.Checked Then
        txtOldPassword.Enabled = True
        txtOldPasswordRepeat.Enabled = True
      Else
        txtOldPassword.Enabled = False
        txtOldPasswordRepeat.Enabled = False
        txtOldPassword.Text = ""
        txtOldPasswordRepeat.Text = ""
        txtOldPassword.BackColor = SystemColors.Window
        txtOldPasswordRepeat.BackColor = SystemColors.Window
      End If
    End If
  End Sub

  Private Sub chkUpdateConfigurations_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUpdateConfigurations.CheckedChanged
    If chkUpdateConfigurations.Checked Then
      txtOldPassword.Enabled = True
      txtOldPasswordRepeat.Enabled = True
    Else
      If chkUpdateVariables.Checked Then
        txtOldPassword.Enabled = True
        txtOldPasswordRepeat.Enabled = True
      Else
        txtOldPassword.Enabled = False
        txtOldPasswordRepeat.Enabled = False
        txtOldPassword.Text = ""
        txtOldPasswordRepeat.Text = ""
        txtOldPassword.BackColor = SystemColors.Window
        txtOldPasswordRepeat.BackColor = SystemColors.Window
      End If
    End If
  End Sub

  Private Sub ValidateOldPassword(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtOldPassword.KeyUp, txtOldPasswordRepeat.KeyUp
    If txtOldPassword.Enabled Then
      If txtOldPassword.Text <> txtOldPasswordRepeat.Text Then
        txtOldPassword.BackColor = Color.Red
        txtOldPasswordRepeat.BackColor = Color.Red
      Else
        txtOldPassword.BackColor = SystemColors.Window
        txtOldPasswordRepeat.BackColor = SystemColors.Window
      End If
    End If
  End Sub

  Private Sub ValidateNewPassword(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtNewPassword.KeyUp, txtNewPasswordRepeat.KeyUp
    If txtNewPassword.Text <> txtNewPasswordRepeat.Text Then
      txtNewPassword.BackColor = Color.Red
      txtNewPasswordRepeat.BackColor = Color.Red
    Else
      txtNewPassword.BackColor = SystemColors.Window
      txtNewPasswordRepeat.BackColor = SystemColors.Window
    End If
  End Sub

  Private Sub lnkSelectAll_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectAll.LinkClicked
    For i As Integer = 0 To chkUpdateObjects.Items.Count - 1
      chkUpdateObjects.SetItemChecked(i, True)
    Next
  End Sub

  Private Sub lnkSelectNone_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectNone.LinkClicked
    For i As Integer = 0 To chkUpdateObjects.Items.Count - 1
      chkUpdateObjects.SetItemChecked(i, False)
    Next
  End Sub

  Private Sub lnkSelectInvert_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectInvert.LinkClicked
    For i As Integer = 0 To chkUpdateObjects.Items.Count - 1
      chkUpdateObjects.SetItemChecked(i, Not chkUpdateObjects.GetItemChecked(i))
    Next
  End Sub

  Private Function ValidateRequiredFields(Simulated As Boolean) As Boolean
    ValidateRequiredFields = True

    If txtUserID.Text = "" Then
      MessageBox.Show("No User ID specified to Update. Please, verify!", "No User ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      txtUserID.Focus()
      txtUserID.SelectAll()
      Return False
    End If

    If txtOldPassword.Enabled Then
      If txtOldPassword.Text = "" Then
        MessageBox.Show("Old Password NOT Set. Please, verify!", "New Password Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        txtOldPassword.Focus()
        txtOldPassword.SelectAll()
        Return False
      End If
    End If

    If txtOldPassword.BackColor = Color.Red Then
      MessageBox.Show("Old Password does NOT MUCH. Please, verify!", "Old Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      txtOldPassword.Focus()
      txtOldPassword.SelectAll()
      Return False
    End If

    If Not Simulated Then
      If txtNewPassword.Text = "" Then
        MessageBox.Show("New Password NOT Set. Please, verify!", "New Password Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        txtNewPassword.Focus()
        txtNewPassword.SelectAll()
        Return False
      End If

      If txtNewPassword.BackColor = Color.Red Then
        MessageBox.Show("New Password does NOT MUCH. Please, verify!", "New Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        txtNewPassword.Focus()
        txtNewPassword.SelectAll()
        Return False
      End If
    End If

    If chkUserIDFormats.CheckedItems.Count = 0 And Not chkUpdateVariables.Checked And Not chkUpdateVariables.Checked Then
      MessageBox.Show("No Objects/Variables/Configurations selected to be updated.", "Nothing to work on", MessageBoxButtons.OK, MessageBoxIcon.Stop)
      Return False
    End If
  End Function

  Private Delegate Sub DelChangeStepDefinition(strNewDisplay As String)
  Private Sub ChangeStepDefinition(strNewDisplay As String)
    If objProgress.lblUpdateStep.InvokeRequired Then
      Dim temp As New DelChangeStepDefinition(AddressOf ChangeStepDefinition)
      Me.Invoke(temp, New Object() {strNewDisplay})
    Else
      objProgress.lblUpdateStep.Text = strNewDisplay
    End If
  End Sub

  Private Delegate Sub DelChangeStepProgress()
  Private Sub ChangeStepProgress()
    If objProgress.progressBar.InvokeRequired Then
      Dim temp As New DelChangeStepProgress(AddressOf ChangeStepProgress)
      Me.Invoke(temp)
    Else
      If objProgress.progressBar.Value + 1 <= objProgress.progressBar.Maximum Then
        objProgress.progressBar.Value += 1
      End If
    End If
  End Sub

  Private Sub workerPasswordChanger_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles workerPasswordChanger.DoWork
    Dim Simulated As Boolean = e.Argument

    Common.WriteLog(objLog, "Backgound Thread Started")

    For Each objObjectToUpdate As clsUserPassTableUpdate In colObjectsToUpdate
      Common.WriteLog(objLog, "Updating: " & objObjectToUpdate.ToString())
      ChangeStepDefinition("Updating: " & objObjectToUpdate.ToString())

      If objObjectToUpdate.UpdatePassword(strNewPassword, strUserIDToLookFor, strDomainToLookFor, objLog, Simulated) Then
        Common.WriteLog(objLog, "Successfully Updated")
      Else
        Common.WriteLog(objLog, "Failed To Updated (All or Some)")
      End If

      ChangeStepProgress()
    Next

    If boolUpdateVariables Then
      Common.WriteLog(objLog, "Updating Variables")
      ChangeStepDefinition("Updating Variables...")

      If DB.UpdateVariables(strOldPassword, strNewPassword, objLog, Simulated) Then
        Common.WriteLog(objLog, "Successfully Updated")
      Else
        Common.WriteLog(objLog, "Failed To Updated (All or Some)")
      End If

      ChangeStepProgress()
    End If

    If boolUpdateConfigurations Then
      Common.WriteLog(objLog, "Updating Configurations")
      ChangeStepDefinition("Updating Configurations...")

      If DB.UpdateConfigurations(strOldPassword, strNewPassword, objLog, Simulated) Then
        Common.WriteLog(objLog, "Successfully Updated")
      Else
        Common.WriteLog(objLog, "Failed To Updated (All or Some)")
      End If

      ChangeStepProgress()
    End If

    If GlobalVariables.colUpdatedRunbooks.Count > 0 Then
      Common.WriteLog(objLog, "Refreshing Runbooks")
      ChangeStepDefinition("Refreshing Impacted Runbooks...")

      For Each strRunbookID As String In GlobalVariables.colUpdatedRunbooks
        Common.WriteLog(objLog, vbTab & "Refreshing Runbook: Runbooks" & DB.GetRunbookPath(strRunbookID) & " (UniqueID=" & strRunbookID & ")")

        If DB.IncreaseRunbookVersion(strRunbookID) Then
          Common.WriteLog(objLog, "Successfully Updated")
        Else
          Common.WriteLog(objLog, "Failed To Updated (All or Some)")
        End If
      Next

      ChangeStepProgress()
    End If

    Common.WriteLog(objLog, "Backgound Thread Completed")
  End Sub

  Private Sub workerPasswordChanger_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles workerPasswordChanger.RunWorkerCompleted
    objProgress.Close()
  End Sub
End Class
