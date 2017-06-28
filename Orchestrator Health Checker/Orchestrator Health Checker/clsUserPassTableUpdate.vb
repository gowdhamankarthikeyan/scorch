Public Class clsUserPassTableUpdate
  Private _DisplayName As String = ""
  Private _TableName As String = ""
  Private _DomainNameField As String = ""
  Private _UserNameField As String = ""
  Private _PasswordField As String = ""
  Private _WhereCondition As String = ""
  Private _Index As Integer = 0

#Region "Properties"
  Public Property DisplayName As String
    Get
      Return _DisplayName
    End Get
    Set(value As String)
      _DisplayName = value
    End Set
  End Property

  Public Property TableName As String
    Get
      Return _TableName
    End Get
    Set(value As String)
      _TableName = value

      If _DisplayName = "" Then
        _DisplayName = DB.GetObjectNameFromTable(value)
      End If
    End Set
  End Property

  Public Property DomainNameField As String
    Get
      Return _DomainNameField
    End Get
    Set(value As String)
      _DomainNameField = value
    End Set
  End Property

  Public Property UserNameField As String
    Get
      Return _UserNameField
    End Get
    Set(value As String)
      _UserNameField = value
    End Set
  End Property

  Public Property PasswordField As String
    Get
      Return _PasswordField
    End Get
    Set(value As String)
      _PasswordField = value
    End Set
  End Property

  Public Property WhereCondition As String
    Get
      Return _WhereCondition
    End Get
    Set(value As String)
      _WhereCondition = value
    End Set
  End Property

  Public ReadOnly Property Index As Integer
    Get
      Return _Index
    End Get
  End Property
#End Region

  Sub New(Index As Integer)
    _Index = Index
  End Sub

  Sub New(Index As Integer, Name As String)
    _Index = Index
    _DisplayName = Name
  End Sub

  Public Overrides Function ToString() As String
    Return _DisplayName
  End Function

  Public Function UpdatePassword(NewPassword As String, UserNameToLookUp As String, DomainToLookUp As String, ByRef objLog As IO.StreamWriter, Simulated As Boolean) As Boolean
    UpdatePassword = True

    Common.WriteLog(objLog, vbTab & "Table: " & _TableName)
    Common.WriteLog(objLog, vbTab & "User ID Field: " & _UserNameField)
    Common.WriteLog(objLog, vbTab & "Password Field: " & _PasswordField)
    Common.WriteLog(objLog, vbTab & "Domain Field: " & If(Not String.IsNullOrEmpty(_DomainNameField), _DomainNameField, "No Field"))
    Common.WriteLog(objLog, vbTab & "Searching Activities Using specified User ID")

    Dim dtUniqueIDs As DataTable = DB.GetUniqueIDsToUpdate(_TableName, _UserNameField, UserNameToLookUp, DomainToLookUp, _DomainNameField, _PasswordField, _WhereCondition)
    Dim dtActivityInfo As DataTable = Nothing

    Common.WriteLog(objLog, vbTab & "Activities Using Specified ID: " & dtUniqueIDs.Rows.Count)

    For Each rowUniqueID As DataRow In dtUniqueIDs.Rows
      dtActivityInfo = DB.GetActivityPath(rowUniqueID(0).ToString())

      If Not Simulated Then
        Common.WriteLog(objLog, vbTab & vbTab & "Updating Password On: " & If(dtActivityInfo.Rows.Count > 0, dtActivityInfo.Rows(0)("ActivityPath"), "Deleted Activity") & " (UniqueID=" & rowUniqueID(0).ToString() & ")")

        If DB.UpdatePassword(_TableName, rowUniqueID(0).ToString(), _PasswordField, NewPassword, rowUniqueID(1).ToString()) Then
          UpdatePassword = UpdatePassword And True
          Common.WriteLog(objLog, vbTab & vbTab & "Successfully Changed")

          If dtActivityInfo.Rows.Count > 0 Then
            If Not GlobalVariables.colUpdatedRunbooks.Contains(dtActivityInfo.Rows(0)("RunbookId").ToString()) Then
              GlobalVariables.colUpdatedRunbooks.Add(dtActivityInfo.Rows(0)("RunbookId").ToString())
            End If
          End If
        Else
          UpdatePassword = UpdatePassword And False
          Common.WriteLog(objLog, vbTab & vbTab & "Failed To Changed")
        End If
      Else
        Common.WriteLog(objLog, vbTab & vbTab & "Updating Password On: " & If(dtActivityInfo.Rows.Count > 0, dtActivityInfo.Rows(0)("ActivityPath"), "Deleted Activity") & " (UniqueID=" & rowUniqueID(0).ToString() & ") (Simulated - Sleep: 0.5 second)")
        Threading.Thread.Sleep(500)
      End If
    Next

    If dtActivityInfo IsNot Nothing Then
      dtActivityInfo.Dispose()
      GC.Collect()
    End If
  End Function
End Class
