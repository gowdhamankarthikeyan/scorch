Imports System.IO
Imports System.Configuration

Module Common
  Public Function DurationToText(value As Double, Optional boolShort As Boolean = False) As String
    DurationToText = ""
    Dim rest As Double = 0

    If value >= 60 Then
      DurationToText = (value \ 60) & If(boolShort, "h ", " hour(s) ")
      rest = value Mod 60
    Else
      rest = value
    End If

    If rest >= 1 Then
      DurationToText &= CInt(rest) & If(boolShort, "m ", " minute(s) ")

      If rest Mod 1 > 0 Then
        DurationToText &= CInt((rest Mod 1) * 60) & If(boolShort, "s ", " second(s) ")
      End If
    ElseIf rest > 0 Then
      DurationToText &= CInt(rest * 60) & If(boolShort, "s ", " second(s) ")
    End If

    If DurationToText = "" Then
      DurationToText = "No Info"
    End If
  End Function

  Public Function SIDtoUser(strSID As String) As String
    Try
      Dim objSID As New System.Security.Principal.SecurityIdentifier(strSID)
      SIDtoUser = objSID.Translate(GetType(System.Security.Principal.NTAccount)).ToString
    Catch ex As Exception
      SIDtoUser = strSID
    End Try
  End Function

  Public Function UserToSID(strUserID As String) As String
    Try
      Dim objAccount As New System.Security.Principal.NTAccount(strUserID)
      UserToSID = objAccount.Translate(GetType(System.Security.Principal.SecurityIdentifier)).ToString()
    Catch ex As Exception
      UserToSID = ""
    End Try
  End Function

  Public Function GetDefaultTimeZone() As String
    Try
      GetDefaultTimeZone = ConfigurationManager.AppSettings("TimeZoneToUse")
    Catch ex As Exception
      GetDefaultTimeZone = "Central Standard Time"
    End Try
  End Function

  Public Function LogFileFormat() As String
    Try
      LogFileFormat = ConfigurationManager.AppSettings("LogFileFormat")
    Catch ex As Exception
      LogFileFormat = "Plain-Text"
    End Try
  End Function

  Public Sub WriteLog(ByRef objLogFile As StreamWriter, Message As String)
    Dim tmpDate As DateTimeOffset = Now

    If LogFileFormat().ToLower() = "cmtrace" Then
      objLogFile.WriteLine("<![LOG[" & Message.Replace(vbTab, "--- ") & "]LOG]!><time=""" & tmpDate.ToString("HH:mm:ss.fff") & (tmpDate.Offset.Days * 60 + tmpDate.Offset.Minutes) & """ date=""" & tmpDate.ToString("d").Replace("/", "-") & """ component=""OrchestratorHealthChecker"" context="""" type=""1"" thread=""" & Threading.Thread.CurrentThread.ManagedThreadId & """ file="""">")
    Else
      objLogFile.WriteLine(Now.ToString("yyyyMMdd" & vbTab & "HHmmss") & vbTab & Message)
    End If
  End Sub
End Module
