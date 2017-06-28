Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Configuration
Imports System.Xml
Imports System.Text.RegularExpressions

Module DB
  Private objRegEx As New Regex("\\`d.T.~De/.*?\\`d.T.~De/")

#Region "Common"
  Private Function Query(ByVal sqlSelect As String, ByVal sqlFrom As String, Optional ByVal sqlWhere As String = "", Optional ByVal sqlGroupBy As String = "", Optional ByVal sqlOrderBy As String = "") As DataTable
    Dim sqlStatement As String = "SELECT " & sqlSelect & " FROM " & sqlFrom

    If Not String.IsNullOrEmpty(sqlWhere) Then
      sqlStatement = sqlStatement & " WHERE " & sqlWhere
    End If

    If Not String.IsNullOrEmpty(sqlGroupBy) Then
      sqlStatement = sqlStatement & " GROUP BY " & sqlGroupBy
    End If

    If Not String.IsNullOrEmpty(sqlOrderBy) Then
      sqlStatement = sqlStatement & " ORDER BY " & sqlOrderBy
    End If

    Return Query(sqlStatement)
  End Function

  Private Function Query(ByVal sqlQuery As String) As DataTable
    Dim oConn As New SqlConnection(ConfigurationManager.ConnectionStrings(GlobalVariables.strCurrentConnectionSuffix & "_SCORCH_DB").ConnectionString)
    Query = New DataTable
    Dim da As New SqlDataAdapter(sqlQuery, oConn)

    Try
      oConn.Open()
      da.SelectCommand.CommandTimeout = 900
      da.Fill(Query)
    Catch ex As Exception

    Finally
      If oConn.State <> ConnectionState.Closed Then
        oConn.Close()
      End If
    End Try
  End Function

  Private Function Execute(StoredProcedure As String, params() As Object, Optional OutputVariable As String = "@RETURN_VALUE") As String
    Dim oConn As New SqlConnection(ConfigurationManager.ConnectionStrings(GlobalVariables.strCurrentConnectionSuffix & "_SCORCH_DB").ConnectionString)
    Dim oCmd As New SqlCommand()

    oCmd.CommandType = CommandType.StoredProcedure
    oCmd.CommandText = StoredProcedure
    oCmd.Connection = oConn

    'Setting Timeout to 15 minutes
    oCmd.CommandTimeout = 900

    Try
      oConn.Open()

      SqlCommandBuilder.DeriveParameters(oCmd)

      If params IsNot Nothing Then
        For i As Byte = 0 To params.Length - 1
          If params(i) = Nothing Then
            oCmd.Parameters(i + 1).Value = DBNull.Value
          Else
            oCmd.Parameters(i + 1).Value = params(i)
          End If
        Next
      End If

      oCmd.ExecuteNonQuery()

      Execute = oCmd.Parameters(OutputVariable).Value
    Catch ex As Exception
      Execute = -1
    Finally
      If oConn.State <> ConnectionState.Closed Then
        oConn.Close()
      End If
    End Try
  End Function

  Private Function Execute(sqlCommand As String) As Boolean
    Dim oConn As New SqlConnection(ConfigurationManager.ConnectionStrings(GlobalVariables.strCurrentConnectionSuffix & "_SCORCH_DB").ConnectionString)
    Dim oCmd As New SqlCommand(sqlCommand, oConn)

    'Setting Timeout to 15 minutes
    oCmd.CommandTimeout = 900

    Try
      oConn.Open()

      oCmd.ExecuteNonQuery()
      Execute = True
    Catch ex As Exception
      Execute = False
    Finally
      If oConn.State <> ConnectionState.Closed Then
        oConn.Close()
      End If
    End Try
  End Function
#End Region

#Region "Database"
  Public Function GetTablesSizes() As DataTable
    Return Query("SELECT a2.name AS [tablename], (a1.reserved + ISNULL(a4.reserved,0))* 8 AS reserved,a1.rows as row_count, a1.data * 8 AS data, " & _
                  "(CASE WHEN (a1.used + ISNULL(a4.used,0)) > a1.data THEN (a1.used + ISNULL(a4.used,0)) - a1.data ELSE 0 END) * 8 AS index_size, " & _
                  "(CASE WHEN (a1.reserved + ISNULL(a4.reserved,0)) > a1.used THEN (a1.reserved + ISNULL(a4.reserved,0)) - a1.used ELSE 0 END) * 8 AS unused, " & _
                  "a3.name AS [schemaname] " & _
                  "FROM (SELECT ps.object_id, SUM (CASE WHEN (ps.index_id < 2) THEN row_count ELSE 0 END) AS [rows], " & _
                  "SUM (ps.reserved_page_count) AS reserved, " & _
                  "SUM (CASE WHEN (ps.index_id < 2) THEN (ps.in_row_data_page_count + ps.lob_used_page_count + ps.row_overflow_used_page_count) " & _
                  "ELSE (ps.lob_used_page_count + ps.row_overflow_used_page_count) END ) AS data, " & _
                  "SUM (ps.used_page_count) AS used " & _
                  "FROM sys.dm_db_partition_stats ps " & _
                  "GROUP BY ps.object_id) AS a1 " & _
                  "LEFT OUTER JOIN (SELECT it.parent_id, " & _
                  "SUM(ps.reserved_page_count) AS reserved, " & _
                  "SUM(ps.used_page_count) AS used " & _
                  "FROM sys.dm_db_partition_stats ps " & _
                  "INNER JOIN sys.internal_tables it ON (it.object_id = ps.object_id) " & _
                  "WHERE it.internal_type IN (202,204) " & _
                  "GROUP BY it.parent_id) AS a4 ON (a4.parent_id = a1.object_id) " & _
                  "INNER JOIN sys.all_objects a2  ON ( a1.object_id = a2.object_id ) " & _
                  "INNER JOIN sys.schemas a3 ON (a2.schema_id = a3.schema_id) " & _
                  "WHERE a2.type <> N'S' and a2.type <> N'IT'")
  End Function

  Public Function GetFileSizeUsage() As DataTable
    Return Query("select a.FILEID, " & _
                  "[SPACE_USED_MB]=convert(decimal(12,2),round(fileproperty(a.name,'SpaceUsed')/128.000,2)), " & _
                  "[FREE_SPACE_MB]=convert(decimal(12,2),round((a.size-fileproperty(a.name,'SpaceUsed'))/128.000,2)) , " & _
                  "[FILE_SIZE_MB]=convert(decimal(12,2),round(a.size/128.000,2)), " & _
                  "[GROWTH_MB]=convert(decimal(12,2),round(a.growth/128.000,2)), " & _
                  "NAME, " & _
                  "FILENAME " & _
                  "from dbo.sysfiles a")
  End Function

  Public Function ReIndexTables() As Boolean
    Return Execute("SET ANSI_NULLS ON " & vbNewLine & _
                    "SET ANSI_PADDING ON " & vbNewLine & _
                    "SET ANSI_WARNINGS ON " & vbNewLine & _
                    "SET ARITHABORT ON " & vbNewLine & _
                    "SET CONCAT_NULL_YIELDS_NULL ON " & vbNewLine & _
                    "SET QUOTED_IDENTIFIER ON " & vbNewLine & _
                    "SET NUMERIC_ROUNDABORT OFF " & vbNewLine & _
                    "EXEC SP_MSForEachTable ""Print 'Reindexing '+'?' DBCC DBREINDEX ('?')""")
  End Function

  Public Function UpdateStatistics() As Boolean
    Return Execute("EXEC sp_MSforeachtable 'UPDATE STATISTICS ? WITH FULLSCAN'")
  End Function

  Public Function GetFragLevel(MinFragLevelForReorg As Byte, MinFragLevelForRebuild As Byte, MinPageNumberToWorry As Int16, Optional ShowOnlyReorgRebuild As Boolean = True) As DataTable
    Return Query("DECLARE @DBName NVARCHAR(128) = ''" & vbNewLine & _
                  "DECLARE @ReorgLimit TINYINT = " & MinFragLevelForReorg & vbNewLine & _
                  "DECLARE @RebuildLimit TINYINT = " & MinFragLevelForRebuild & vbNewLine & _
                  "DECLARE @PageLimit SMALLINT = " & MinPageNumberToWorry & vbNewLine & _
                  "DECLARE @ShowAllIndexes BIT = " & If(ShowOnlyReorgRebuild, 0, 1) & vbNewLine & _
                  " " & vbNewLine & _
                  "SET NOCOUNT ON ; " & vbNewLine & _
                  "SET DEADLOCK_PRIORITY LOW ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "BEGIN TRY " & vbNewLine & _
                  " " & vbNewLine & _
                  "DECLARE @FullName NVARCHAR(400), @SQL NVARCHAR(1000), @Rebuild NVARCHAR(1000), @DBID SMALLINT ; " & vbNewLine & _
                  "DECLARE @Error INT, @TableName NVARCHAR(128), @SchemaName NVARCHAR(128), @HasLobs TINYINT ; " & vbNewLine & _
                  "DECLARE @object_id INT, @index_id INT, @partition_number INT, @AvgFragPercent TINYINT ; " & vbNewLine & _
                  "DECLARE @IndexName NVARCHAR(128), @Partitions INT, @Print NVARCHAR(1000) ; " & vbNewLine & _
                  "DECLARE @PartSQL NVARCHAR(600), @ReOrgFlag TINYINT, @IndexTypeDesc NVARCHAR(60) ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "-- Get the ID of the Database Catalog " & vbNewLine & _
                  "IF @DBName = '' SET @DBName = DB_NAME(); " & vbNewLine & _
                  "SET @DBID = DB_ID(@DBName) ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "IF OBJECT_ID('tempdb..#FragLevels') IS NOT NULL DROP TABLE #FragLevels " & vbNewLine & _
                  "" & vbNewLine & _
                  "-- Create a temporary table to store results " & vbNewLine & _
                  "CREATE TABLE #FragLevels ( " & vbNewLine & _
                  "[SchemaName] NVARCHAR(128) NULL, [TableName] NVARCHAR(128) NULL, [HasLOBs] TINYINT NULL, " & vbNewLine & _
                  "[ObjectID] [int] NOT NULL, [IndexID] [int] NOT NULL, [PartitionNumber] [int] NOT NULL,  " & vbNewLine & _
                  "[AvgFragPercent] [tinyint] NOT NULL, [IndexName] NVARCHAR(128) NULL, [IndexTypeDesc] NVARCHAR(60) NOT NULL ) ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "-- Get the initial list of indexes and partitions to work on filtering out heaps and meeting the specified thresholds " & vbNewLine & _
                  "INSERT INTO #FragLevels " & vbNewLine & _
                  "([ObjectID], [IndexID], [PartitionNumber], [AvgFragPercent], [IndexTypeDesc]) " & vbNewLine & _
                  "SELECT a.[object_id], a.[index_id], a.[partition_number], CAST(a.[avg_fragmentation_in_percent] AS TINYINT) AS [AvgFragPercent], a.[index_type_desc] " & vbNewLine & _
                  "FROM sys.dm_db_index_physical_stats(@DBID, NULL, NULL, NULL , 'LIMITED') AS a  " & vbNewLine & _
                  "WHERE  " & vbNewLine & _
                  "((@ShowAllIndexes = 0 AND a.[avg_fragmentation_in_percent] >= @ReorgLimit) OR (@ShowAllIndexes <> 0)) AND " & vbNewLine & _
                  "a.[page_count] >= @PageLimit AND  " & vbNewLine & _
                  "a.[index_id] > 0 " & vbNewLine & _
                  " " & vbNewLine & _
                  "-- Create an index to make some of the updates & lookups faster " & vbNewLine & _
                  "CREATE INDEX [IX_#FragLevels_OBJECTID] ON #FragLevels([ObjectID]) ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "-- Get the Schema and Table names for each " & vbNewLine & _
                  "UPDATE #FragLevels WITH (TABLOCK)  " & vbNewLine & _
                  "SET [SchemaName] = OBJECT_SCHEMA_NAME([ObjectID],@DBID), " & vbNewLine & _
                  "[TableName] = OBJECT_NAME([ObjectID],@DBID) ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "-- Determine if the index has a Large Object (LOB) datatype. " & vbNewLine & _
                  "-- LOBs prevent reindexing and rebuilding index online " & vbNewLine & _
                  "SET @SQL = N'UPDATE #FragLevels WITH (TABLOCK) SET [HasLOBs] = (SELECT TOP 1 CASE WHEN t.[lob_data_space_id] = 0 THEN 0 ELSE 1 END ' + " & vbNewLine & _
                  "N' FROM [' + @DBName + N'].[sys].[tables] AS t WHERE t.[type] = ''U'' AND t.[object_id] = #FragLevels.[ObjectID])' ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "EXEC(@SQL) ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "--  Get the index name " & vbNewLine & _
                  "SET @SQL = N'UPDATE #FragLevels SET [IndexName] = (SELECT TOP 1 t.[name] FROM [' + @DBName + N'].[sys].[indexes] AS t WHERE t.[object_id] = #FragLevels.[ObjectID] ' + " & vbNewLine & _
                  "' AND t.[index_id] = #FragLevels.[IndexID] )'; " & vbNewLine & _
                  " " & vbNewLine & _
                  "EXEC(@SQL) ; " & vbNewLine & _
                  " " & vbNewLine & _
                  "-- Return the results " & vbNewLine & _
                  "SELECT  " & vbNewLine & _
                  "F.SchemaName AS [Schema Name], " & vbNewLine & _
                  "F.TableName AS [Table Name], " & vbNewLine & _
                  "F.IndexName AS [Index Name], " & vbNewLine & _
                  "F.IndexTypeDesc AS [Index Type], " & vbNewLine & _
                  "F.AvgFragPercent AS [Avg Frag (%)], " & vbNewLine & _
                  "CASE  " & vbNewLine & _
                  "WHEN F.AvgFragPercent >= @RebuildLimit THEN 'Yes' " & vbNewLine & _
                  "ELSE 'No' " & vbNewLine & _
                  "END AS [Should Rebuild], " & vbNewLine & _
                  "CASE " & vbNewLine & _
                  "WHEN F.AvgFragPercent >= @ReorgLimit AND F.AvgFragPercent < @RebuildLimit THEN 'Yes' " & vbNewLine & _
                  "ELSE 'No' " & vbNewLine & _
                  "END AS [Should Reorg], " & vbNewLine & _
                  "CASE " & vbNewLine & _
                  "WHEN F.HasLOBs = 1 THEN 'Yes' " & vbNewLine & _
                  "ELSE 'No' " & vbNewLine & _
                  "END AS [Has LOBs], " & vbNewLine & _
                  "F.ObjectID AS [Object ID], " & vbNewLine & _
                  "F.IndexID AS [Index ID], " & vbNewLine & _
                  "F.PartitionNumber AS [Partition Number] " & vbNewLine & _
                  "FROM #FragLevels AS F " & vbNewLine & _
                  "ORDER BY AvgFragPercent DESC " & vbNewLine & _
                  " " & vbNewLine & _
                  "IF OBJECT_ID('tempdb..#FragLevels') IS NOT NULL DROP TABLE #FragLevels " & vbNewLine & _
                  " " & vbNewLine & _
                  "END TRY  " & vbNewLine & _
                  "BEGIN CATCH " & vbNewLine & _
                  " " & vbNewLine & _
                  "SELECT  " & vbNewLine & _
                  "ERROR_NUMBER() AS ErrorNumber, " & vbNewLine & _
                  "ERROR_SEVERITY() AS ErrorSeverity, " & vbNewLine & _
                  "ERROR_STATE() AS ErrorState, " & vbNewLine & _
                  "ERROR_PROCEDURE() AS ErrorProcedure, " & vbNewLine & _
                  "ERROR_LINE() AS ErrorLine, " & vbNewLine & _
                  "ERROR_MESSAGE() AS ErrorMessage; " & vbNewLine & _
                  " " & vbNewLine & _
                  "END CATCH ; ")
  End Function
#End Region

  Public Function CheckDBConnection() As String
    Dim oConn As New SqlConnection(ConfigurationManager.ConnectionStrings(GlobalVariables.strCurrentConnectionSuffix & "_SCORCH_DB").ConnectionString)

    Try
      oConn.Open()
      CheckDBConnection = "OK"
    Catch ex As Exception
      CheckDBConnection = ex.Message
    Finally
      If oConn.State <> ConnectionState.Closed Then
        oConn.Close()
      End If
    End Try
  End Function

  Public Function GetExecutingRunbooks(Optional boolShowRunbookServer As Boolean = False, Optional RunbookServer As String = "", Optional RunbookName As String = "") As DataTable
    Dim dtExecuting As DataTable
    Dim dtQueue As DataTable

    If boolShowRunbookServer Then
      dtExecuting = Query("POLICIES.UniqueID, POLICIES.Name, ACTIONSERVERS.Computer, COUNT(*) AS Running",
                                       "POLICYINSTANCES INNER JOIN POLICIES ON POLICYINSTANCES.PolicyID = POLICIES.UniqueID INNER JOIN ACTIONSERVERS ON ACTIONSERVERS.UniqueID = POLICYINSTANCES.ActionServer",
                                       "POLICYINSTANCES.TimeEnded IS NULL" & IIf(RunbookServer <> "", " AND ACTIONSERVERS.Computer = '" & RunbookServer & "'", "") & IIf(RunbookName <> "", " AND UPPER(POLICIES.Name) LIKE '%" & RunbookName.ToUpper() & "%'", ""),
                                       "POLICIES.UniqueID,POLICIES.Name,ACTIONSERVERS.Computer", "POLICIES.Name,ACTIONSERVERS.Computer")

      If RunbookServer = "" Then
        dtQueue = Query("POLICIES.UniqueID, [POLICIES].Name, COUNT(*) AS Queued",
                                     "[POLICY_PUBLISH_QUEUE] INNER JOIN [POLICIES] ON [POLICIES].[UniqueID]=[POLICY_PUBLISH_QUEUE].[PolicyID]",
                                     "AssignedActionServer is null" & IIf(RunbookName <> "", " AND UPPER(POLICIES.Name) LIKE '%" & RunbookName.ToUpper() & "%'", ""), "POLICIES.UniqueID,Name")
      Else
        dtQueue = New DataTable
      End If
    Else
      dtExecuting = Query("POLICIES.UniqueID, POLICIES.Name, COUNT(*) AS Running",
                                       "POLICYINSTANCES INNER JOIN POLICIES ON POLICYINSTANCES.PolicyID = POLICIES.UniqueID" & IIf(RunbookServer <> "", " INNER JOIN ACTIONSERVERS ON ACTIONSERVERS.UniqueID = POLICYINSTANCES.ActionServer", ""),
                                       "POLICYINSTANCES.TimeEnded IS NULL" & IIf(RunbookServer <> "", " AND ACTIONSERVERS.Computer = '" & RunbookServer & "'", "") & IIf(RunbookName <> "", " AND UPPER(POLICIES.Name) LIKE '%" & RunbookName.ToUpper() & "%'", ""),
                                       "POLICIES.UniqueID,POLICIES.Name", "POLICIES.Name")

      If RunbookServer = "" Then
        dtQueue = Query("POLICIES.UniqueID, [POLICIES].Name, COUNT(*) AS Queued",
                                     "[POLICY_PUBLISH_QUEUE] INNER JOIN [POLICIES] ON [POLICIES].[UniqueID]=[POLICY_PUBLISH_QUEUE].[PolicyID]",
                                     "AssignedActionServer is null" & IIf(RunbookName <> "", " AND UPPER(POLICIES.Name) LIKE '%" & RunbookName.ToUpper() & "%'", ""), "POLICIES.UniqueID,Name")
      Else
        dtQueue = New DataTable
      End If
    End If

    If dtExecuting.Columns.Count = 0 Then
      dtExecuting.Columns.Add("UniqueID")
      dtExecuting.Columns.Add("Name")

      If boolShowRunbookServer Then
        dtExecuting.Columns.Add("RunbookServer")
      End If

      dtExecuting.Columns.Add("Running")
    End If

    If dtQueue.Columns.Count = 0 Then
      dtQueue.Columns.Add("UniqueID")
      dtQueue.Columns.Add("Name")

      If boolShowRunbookServer Then
        dtQueue.Columns.Add("RunbookServer")
      End If

      dtQueue.Columns.Add("Queued")
    End If

    Dim newRow As DataRow = Nothing
    Dim tempRow() As DataRow = Nothing

    GetExecutingRunbooks = New DataTable
    GetExecutingRunbooks.Columns.Add("UniqueID")
    GetExecutingRunbooks.Columns.Add("RunbookName")

    If boolShowRunbookServer Then
      GetExecutingRunbooks.Columns.Add("RunbookServer")
    End If

    GetExecutingRunbooks.Columns.Add("Running", Type.GetType("System.Int32"))
    GetExecutingRunbooks.Columns.Add("Queued", Type.GetType("System.Int32"))
    GetExecutingRunbooks.AcceptChanges()

    For Each row As DataRow In dtExecuting.Rows
      newRow = GetExecutingRunbooks.NewRow

      newRow("UniqueID") = row("UniqueID")
      newRow("RunbookName") = row("Name")
      newRow("Running") = row("Running")
      newRow("Queued") = 0

      If boolShowRunbookServer Then
        newRow("RunbookServer") = row("Computer")
      Else
        tempRow = dtQueue.Select("UniqueID='" & row("UniqueID").ToString() & "'")

        If tempRow.Length = 1 Then
          newRow("Queued") = tempRow(0)("Queued")
        End If
      End If

      GetExecutingRunbooks.Rows.Add(newRow)
    Next

    For Each row As DataRow In dtQueue.Rows
      tempRow = dtExecuting.Select("UniqueID='" & row("UniqueID").ToString() & "'")

      If tempRow.Length = 0 Or boolShowRunbookServer Then
        newRow = GetExecutingRunbooks.NewRow

        newRow("UniqueID") = row("UniqueID")
        newRow("RunbookName") = row("Name")
        newRow("Running") = 0

        If boolShowRunbookServer Then
          newRow("RunbookServer") = DBNull.Value
        End If

        newRow("Queued") = row("Queued")

        GetExecutingRunbooks.Rows.Add(newRow)
      End If
    Next
  End Function

  Public Function GetRunningRunbooksCount(Optional boolMonitorOnly As Boolean = False) As Integer
    Try
      If Not boolMonitorOnly Then
        GetRunningRunbooksCount = Query("COUNT(*)", "[POLICY_PUBLISH_QUEUE]").Rows(0)(0)
      Else
        GetRunningRunbooksCount = Query("COUNT(*)", "[POLICY_PUBLISH_QUEUE] INNER JOIN [POLICIES] ON [POLICIES].UniqueID=[POLICY_PUBLISH_QUEUE].PolicyID", "[POLICIES].Name LIKE 'Monitor %'").Rows(0)(0)
      End If
    Catch ex As Exception
      GetRunningRunbooksCount = -1
    End Try
  End Function

#Region "Admin Options"
  Public Function Execute_ClearOrphanedRunbookInstances() As Boolean
    If Execute("[Microsoft.SystemCenter.Orchestrator.Runtime.Internal].[ClearOrphanedRunbookInstances]", Nothing) <> -1 Then
      Execute_ClearOrphanedRunbookInstances = True
    Else
      Execute_ClearOrphanedRunbookInstances = False
    End If
  End Function

  Public Function LogPurge() As Boolean
    LogPurge = Execute("DELETE FROM POLICYINSTANCES")

    If Not LogPurge Then
      Return False
    End If

    LogPurge = LogPurge And Execute("TRUNCATE TABLE OBJECTINSTANCES")

    If Not LogPurge Then
      Return False
    End If

    LogPurge = LogPurge And Execute("TRUNCATE TABLE OBJECTINSTANCEDATA")

    If Not LogPurge Then
      Return False
    End If

    LogPurge = LogPurge And Execute("TRUNCATE TABLE [Microsoft.SystemCenter.Orchestrator.Runtime.Internal].Jobs")
  End Function

  Public Function StopAllRunningRunbooks() As Boolean
    If Execute("DELETE FROM [POLICY_PUBLISH_QUEUE] WHERE AssignedActionServer is null") Then
      Return Execute("UPDATE [POLICY_PUBLISH_QUEUE] SET StopRequested=1 WHERE SeqNumber IN (SELECT DISTINCT SeqNumber FROM [POLICYINSTANCES] WHERE TimeEnded is NULL)")
    Else
      Return False
    End If
  End Function

  Public Function StopAllMonitoringRunbooks() As Boolean
    If Execute("DELETE [POLICY_PUBLISH_QUEUE] FROM [POLICY_PUBLISH_QUEUE] INNER JOIN [POLICIES] ON [POLICIES].UniqueID=[POLICY_PUBLISH_QUEUE].PolicyID AND [POLICY_PUBLISH_QUEUE].AssignedActionServer is null AND [POLICIES].Name LIKE 'Monitor %'") Then
      Return Execute("UPDATE [POLICY_PUBLISH_QUEUE] SET StopRequested=1 FROM [POLICY_PUBLISH_QUEUE] INNER JOIN [POLICIES] ON [POLICIES].UniqueID=[POLICY_PUBLISH_QUEUE].PolicyID WHERE SeqNumber IN (SELECT DISTINCT SeqNumber FROM [POLICYINSTANCES] WHERE TimeEnded is NULL) AND [POLICIES].Name LIKE 'Monitor %'")
    Else
      Return False
    End If
  End Function

  Public Function StopAllRunbooksOn(strRunbookServer As String) As Boolean
    Return Execute("UPDATE [POLICY_PUBLISH_QUEUE] SET StopRequested=1 FROM [POLICY_PUBLISH_QUEUE] INNER JOIN [ACTIONSERVERS] ON [ACTIONSERVERS].UniqueID=[POLICY_PUBLISH_QUEUE].AssignedActionServer WHERE [ACTIONSERVERS].Computer = '" & strRunbookServer & "'")
  End Function
#End Region

  Public Function GetTableCount(strTableName As String) As Integer
    Try
      Return Query("SELECT COUNT(*) FROM " & strTableName).Rows(0)(0)
    Catch ex As Exception
      Return -1
    End Try
  End Function

  Public Sub GetLastLogPurgeEvent(ByRef StartTime As DateTime, ByRef Endtime As DateTime, ByRef boolCompletedSuccessfuly As Boolean)
    StartTime = Nothing
    Endtime = Nothing
    boolCompletedSuccessfuly = False

    Dim tempDT As DataTable = Query("TOP 2 *", "[Microsoft.SystemCenter.Orchestrator.Runtime].Events", "Summary LIKE '%Log Cleanup%'", sqlOrderBy:="CreationTime DESC")

    If tempDT.Rows.Count <> 0 Then
      If tempDT.Rows(0)("Summary").ToString().ToLower() = "completed log cleanup" Then
        Endtime = TimeZoneInfo.ConvertTimeFromUtc(tempDT.Rows(0)("CreationTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
      ElseIf tempDT.Rows(0)("Type").ToString().ToLower() = "error" Then
        Endtime = TimeZoneInfo.ConvertTimeFromUtc(tempDT.Rows(0)("CreationTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
      End If

      If tempDT.Rows(1)("Summary").ToString().ToLower() = "started log cleanup" Then
        StartTime = TimeZoneInfo.ConvertTimeFromUtc(tempDT.Rows(1)("CreationTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
      End If

      If Endtime <> Nothing And StartTime <> Nothing And tempDT.Rows(0)("Type").ToString().ToLower() <> "error" Then
        boolCompletedSuccessfuly = True
      End If
    End If
  End Sub

  Public Function GetLogPurgeStatusForLast(byteDay As Byte) As DataTable
    GetLogPurgeStatusForLast = New DataTable

    GetLogPurgeStatusForLast.Columns.Add("Day")
    GetLogPurgeStatusForLast.Columns.Add("StartTime", Type.GetType("System.DateTime"))
    GetLogPurgeStatusForLast.Columns.Add("EndTime", Type.GetType("System.DateTime"))
    GetLogPurgeStatusForLast.Columns.Add("Duration", Type.GetType("System.Double"))
    GetLogPurgeStatusForLast.Columns.Add("Successful")
    GetLogPurgeStatusForLast.AcceptChanges()

    Dim tempDT As DataTable = Query("[Type],[CreationTime],[Summary]", "[Microsoft.SystemCenter.Orchestrator.Runtime].[Events]",
                                 "[Summary] LIKE '%Log Cleanup%' AND CreationTime >= '" & Today.AddDays(-1 * byteDay).ToString("yyyy-MM-dd") & " 00:00:00'", sqlOrderBy:="[CreationTime]")

    Dim newRow As DataRow = Nothing
    Dim tmpStartDate As DateTime = Nothing
    Dim tmpEndDate As DateTime = Nothing

    For i As Integer = 0 To tempDT.Rows.Count - 1
      tmpStartDate = Nothing
      tmpEndDate = Nothing

      If tempDT.Rows(i)("Summary").ToString().ToLower() = "started log cleanup" Then
        tmpStartDate = TimeZoneInfo.ConvertTimeFromUtc(tempDT.Rows(i)("CreationTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
      Else
        Continue For
      End If

      If i + 1 < tempDT.Rows.Count Then
        If tempDT.Rows(i + 1)("Summary").ToString().ToLower() = "completed log cleanup" Then
          tmpEndDate = TimeZoneInfo.ConvertTimeFromUtc(tempDT.Rows(i + 1)("CreationTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
        ElseIf tempDT.Rows(i + 1)("Type").ToString().ToLower() = "error" Then
          tmpEndDate = TimeZoneInfo.ConvertTimeFromUtc(tempDT.Rows(i + 1)("CreationTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
        Else
          tmpEndDate = Nothing
        End If
      Else
        tmpEndDate = Nothing
      End If

      newRow = GetLogPurgeStatusForLast.NewRow
      newRow("Day") = tmpStartDate.ToString("MM/dd/yyyy")
      newRow("StartTime") = tmpStartDate

      If tmpEndDate <> Nothing Then
        newRow("EndTime") = tmpEndDate
      Else
        newRow("EndTime") = DBNull.Value
      End If

      If tmpStartDate <> Nothing And tmpEndDate <> Nothing Then
        newRow("Duration") = New TimeSpan(tmpEndDate.Ticks).Subtract(New TimeSpan(tmpStartDate.Ticks)).TotalMinutes
        newRow("Successful") = If(tempDT.Rows(i + 1)("Type").ToString().ToLower() <> "error", "Yes", "No")
      Else
        newRow("Duration") = DBNull.Value
        newRow("Successful") = DBNull.Value
      End If

      GetLogPurgeStatusForLast.Rows.Add(newRow)
    Next

    For i As Byte = 0 To byteDay
      If GetLogPurgeStatusForLast.Select("Day='" & Today.AddDays(-1 * i).ToString("MM/dd/yyyy") & "'").Length = 0 Then
        newRow = GetLogPurgeStatusForLast.NewRow

        newRow("Day") = Today.AddDays(-1 * i).ToString("MM/dd/yyyy")
        newRow("StartTime") = DBNull.Value
        newRow("EndTime") = DBNull.Value
        newRow("Duration") = DBNull.Value
        newRow("Successful") = DBNull.Value

        GetLogPurgeStatusForLast.Rows.Add(newRow)
      End If
    Next
  End Function

  Public Function GetRunbookServerDetails(strRunbookID As String) As DataTable
    GetRunbookServerDetails = Query("ACTIONSERVERS.Computer, POLICYINSTANCES.ProcessID, POLICYINSTANCES.TimeStarted",
                 "POLICYINSTANCES INNER JOIN ACTIONSERVERS ON ACTIONSERVERS.UniqueID = POLICYINSTANCES.ActionServer",
                 "PolicyID='" & strRunbookID & "' AND POLICYINSTANCES.TimeEnded IS NULL", "", "ACTIONSERVERS.Computer, POLICYINSTANCES.ProcessID")

    For Each objRow As DataRow In GetRunbookServerDetails.Rows
      objRow("TimeStarted") = TimeZoneInfo.ConvertTimeFromUtc(objRow("TimeStarted"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
    Next
  End Function

  Public Function GetRunbookServers() As DataTable
    Return Query("DISTINCT Computer", "[ACTIONSERVERS]", sqlOrderBy:="Computer")
  End Function

  Public Function GetRunbookByPID(PID As Integer) As DataTable
    Return Query("POLICIES.UniqueID, POLICIES.Name, ACTIONSERVERS.Computer",
                 "POLICYINSTANCES INNER JOIN POLICIES ON POLICYINSTANCES.PolicyID = POLICIES.UniqueID INNER JOIN ACTIONSERVERS ON ACTIONSERVERS.UniqueID = POLICYINSTANCES.ActionServer",
                 "POLICYINSTANCES.TimeEnded IS NULL AND POLICYINSTANCES.ProcessID=" & PID)
  End Function

  Public Function GetVariables() As DataTable
    Return Query("OBJECTS.Name AS [VariableName], Variables.UniqueID AS [VariableID]", "OBJECTS INNER JOIN VARIABLES ON VARIABLES.UniqueID = OBJECTS.UniqueID AND OBJECTS.Deleted = 0", sqlOrderBy:="OBJECTS.Name")
  End Function

  Public Function GetActivityPath(strActivityID As String) As DataTable
    Return Query("'Runbooks' + [Runbooks].[Path] + '\' + [Activities].[Name] AS ""ActivityPath"",[Activities].[RunbookId]", "[Microsoft.SystemCenter.Orchestrator].[Activities] INNER JOIN [Microsoft.SystemCenter.Orchestrator].[Runbooks] ON [Runbooks].[Id]=[Activities].[RunbookId]", "[Activities].[Id]='" & strActivityID & "'")
  End Function

  Public Function GetActivityTypes() As DataTable
    Return Query("DISTINCT [TypeName]", "[Microsoft.SystemCenter.Orchestrator].[Activities]", sqlOrderBy:="[TypeName]")
  End Function

  Public Function GetActivities(Optional Types As List(Of String) = Nothing, Optional Name As String = "") As DataTable
    Dim strWhere As String = ""

    If Types IsNot Nothing Then
      For Each strType As String In Types
        If strWhere <> "" Then
          strWhere &= " OR "
        End If

        strWhere &= "[Activities].[TypeName]='" & strType & "'"
      Next
    End If

    If Name <> "" Then
      If strWhere <> "" Then
        strWhere = "(" & strWhere & ") AND "
      End If

      strWhere &= "[Activities].Name LIKE '%" & Name & "%'"
    End If

    Return Query("[Activities].Name AS ""Activity"", [Runbooks].Name AS ""Runbook"", 'Runbooks' + LEFT([Runbooks].Path,LEN([Runbooks].Path)-LEN([Runbooks].Name)-1) AS ""Path""",
                 "[Microsoft.SystemCenter.Orchestrator].[Activities] INNER JOIN [Microsoft.SystemCenter.Orchestrator].[Runbooks] ON [Runbooks].[Id]=[Activities].RunbookId",
                 strWhere, sqlOrderBy:="[Runbooks].Path,[Activities].Name")
  End Function

  Public Function GetObjects(GetActivities As Boolean, GetRunbooks As Boolean, GetFolders As Boolean, Optional Name As String = "", Optional ActivityTypes As List(Of String) = Nothing) As DataTable
    Dim strTSQL As String = ""
    Dim strWhere As String = ""

    If GetActivities Then
      strWhere = ""

      If Not String.IsNullOrEmpty(strTSQL) Then
        strTSQL &= " UNION "
      End If

      strTSQL &= "SELECT 'Activity' AS ""ObjectType"", [Activities].Name AS ""Activity"", [Runbooks].Name AS ""Runbook"", 'Runbooks' + LEFT([Runbooks].Path,LEN([Runbooks].Path)-LEN([Runbooks].Name)-1) AS ""Path"" " & _
                 "FROM [Microsoft.SystemCenter.Orchestrator].[Activities] INNER JOIN [Microsoft.SystemCenter.Orchestrator].[Runbooks] ON [Runbooks].[Id]=[Activities].RunbookId"

      If ActivityTypes IsNot Nothing Then
        For Each strType As String In ActivityTypes
          If strWhere <> "" Then
            strWhere &= " OR "
          End If

          strWhere &= "[Activities].[TypeName]='" & strType & "'"
        Next
      End If

      If Name <> "" Then
        If strWhere <> "" Then
          strWhere = "(" & strWhere & ") AND "
        End If

        strWhere &= "[Activities].Name LIKE '%" & Name & "%'"
      End If

      If Not String.IsNullOrEmpty(strWhere) Then
        strTSQL &= " WHERE " & strWhere
      End If
    End If

    If GetRunbooks Then
      strWhere = ""

      If Not String.IsNullOrEmpty(strTSQL) Then
        strTSQL &= " UNION "
      End If

      strTSQL &= "SELECT 'Runbook' AS ""ObjectType"", NULL AS ""Activity"", [Runbooks].Name AS ""Runbook"", 'Runbooks' + LEFT([Runbooks].Path,LEN([Runbooks].Path)-LEN([Runbooks].Name)-1) AS ""Path"" " & _
                 "FROM [Microsoft.SystemCenter.Orchestrator].[Runbooks]"

      If Name <> "" Then
        strTSQL &= " WHERE [Runbooks].Name LIKE '%" & Name & "%'"
      End If
    End If

    If GetFolders Then
      strWhere = ""

      If Not String.IsNullOrEmpty(strTSQL) Then
        strTSQL &= " UNION "
      End If

      strTSQL &= "SELECT 'Folder' AS ""ObjectType"", NULL AS ""Activity"", NULL AS ""Runbook"", [Path] " & _
                 "FROM [Microsoft.SystemCenter.Orchestrator].[Folders]"

      If Name <> "" Then
        strTSQL &= " WHERE [Folders].Name LIKE '%" & Name & "%'"
      End If
    End If

    strTSQL &= " ORDER BY 4,3,2"

    Return Query(strTSQL)
  End Function

  Public Function GetRunbookPath(strRunbookID As String) As String
    Dim tmpDT As DataTable = Query("[Path]", "[Microsoft.SystemCenter.Orchestrator].[Runbooks]", "Id='" & strRunbookID & "'")

    If tmpDT.Rows.Count > 0 Then
      GetRunbookPath = tmpDT.Rows(0)("Path")
    Else
      GetRunbookPath = ""
    End If
  End Function

  Public Function GetVariablePath(strVariableID As String) As String
    Dim tmpDT As DataTable = Query("[OBJECTS].ParentID,[OBJECTS].Name", "[OBJECTS]", "[OBJECTS].UniqueID='" & strVariableID & "'")

    If tmpDT.Rows.Count > 0 Then
      GetVariablePath = GetObjectPath(tmpDT.Rows(0)("ParentID").ToString()) & "\" & tmpDT.Rows(0)("Name")
    Else
      GetVariablePath = "Not Found"
    End If
  End Function

  Public Function GetObjectPath(strFolderID As String) As String
    Dim tmpDT As DataTable = Query("WITH MyCTE AS(" & _
                                 "SELECT t1.UniqueID,t1.ParentID,CAST(t1.Name AS NVARCHAR(1000)) AS ""Name"" " & _
                                 "FROM FOLDERS t1 INNER JOIN FOLDERS t2 ON t2.UniqueID=t1.ParentID " & _
                                 "UNION ALL " & _
                                 "SELECT MyCTE.UniqueID,FOLDERS.ParentID, CAST(FOLDERS.Name + '\' + MyCTE.Name AS nvarchar(1000)) " & _
                                 "FROM MyCTE INNER JOIN FOLDERS ON FOLDERS.UniqueID=MyCTE.ParentID " & _
                                 ") SELECT [Name] FROM MyCTE WHERE UniqueID='" & strFolderID & "' AND ParentID is null")

    If tmpDT.Rows.Count > 0 Then
      GetObjectPath = tmpDT.Rows(0)(0)
    Else
      GetObjectPath = "Variables"
    End If
  End Function

  Public Function GetConfigurationName(strConfigurationID As String) As String
    Dim tmpDT As DataTable = Query("[CAPS].Name", "[CONFIGURATION] INNER JOIN [CAPS] ON [CAPS].[UniqueID]=[CONFIGURATION].[TypeGUID]", "[CONFIGURATION].UniqueID='" & strConfigurationID & "'")

    If tmpDT.Rows.Count > 0 Then
      GetConfigurationName = tmpDT.Rows(0)(0)
    Else
      GetConfigurationName = "Not Found"
    End If
  End Function

  Public Function GetInitialParemetersFor(strRunbook As String, strRunbookServer As String, intProcessID As Integer) As DataTable
    GetInitialParemetersFor = New DataTable
    GetInitialParemetersFor.Columns.Add("Name")
    GetInitialParemetersFor.Columns.Add("Value")
    GetInitialParemetersFor.AcceptChanges()

    Dim tmpDT As DataTable = Query("[POLICY_PUBLISH_QUEUE].Data",
                                 "POLICYINSTANCES INNER JOIN POLICIES ON POLICYINSTANCES.PolicyID = POLICIES.UniqueID INNER JOIN ACTIONSERVERS ON ACTIONSERVERS.UniqueID = POLICYINSTANCES.ActionServer INNER JOIN [POLICY_PUBLISH_QUEUE] ON [POLICY_PUBLISH_QUEUE].SeqNumber = POLICYINSTANCES.SeqNumber",
                                 "POLICIES.Name='" & strRunbook & "' AND ACTIONSERVERS.Computer='" & strRunbookServer & "' AND POLICYINSTANCES.ProcessID=" & intProcessID & " AND POLICYINSTANCES.TimeEnded IS NULL")

    If tmpDT.Rows.Count > 0 Then
      If Not IsDBNull(tmpDT.Rows(0)("Data")) Then
        Dim objXML As New XmlDocument
        objXML.LoadXml(tmpDT.Rows(0)("Data"))

        For Each objParameter As XmlNode In objXML.ChildNodes(0).ChildNodes
          GetInitialParemetersFor.Rows.Add(New Object() {objParameter.SelectSingleNode("Name").InnerText, objParameter.SelectSingleNode("Value").InnerText})
        Next
      End If
    End If
  End Function

  Public Function StopSingleRunbook(strRunbook As String, strRunbookServer As String, intProcessID As Integer, Optional boolStopSubTree As Boolean = True) As Boolean
    StopSingleRunbook = False

    Dim tmpDT As DataTable = Query("[POLICYINSTANCES].SeqNumber",
                                 "POLICYINSTANCES INNER JOIN POLICIES ON POLICYINSTANCES.PolicyID = POLICIES.UniqueID INNER JOIN ACTIONSERVERS ON ACTIONSERVERS.UniqueID = POLICYINSTANCES.ActionServer",
                                 "POLICIES.Name='" & strRunbook & "' AND ACTIONSERVERS.Computer='" & strRunbookServer & "' AND POLICYINSTANCES.ProcessID=" & intProcessID & " AND POLICYINSTANCES.TimeEnded IS NULL")

    If tmpDT.Rows.Count > 0 Then
      StopSingleRunbook = Execute("UPDATE [POLICY_PUBLISH_QUEUE] SET StopRequested=1 WHERE SeqNumber IN (" & If(boolStopSubTree, FindRunbookExecutionTree(tmpDT.Rows(0)("SeqNumber")), tmpDT.Rows(0)("SeqNumber")) & ")")
    End If
  End Function

  Private Function FindRunbookExecutionTree(strSeqNumber As String) As String
    Dim dtSeqNumbers As DataTable = Query("SeqNumber", "[POLICY_PUBLISH_QUEUE]", "ParentSeqNumber IN (" & strSeqNumber & ")")
    Dim strChildSeqIDs As String = ""
    Dim strFoundTree As String = ""

    FindRunbookExecutionTree = strSeqNumber

    For Each rowSeqID As DataRow In dtSeqNumbers.Rows
      If Not String.IsNullOrEmpty(strChildSeqIDs) Then
        strChildSeqIDs &= ","
      End If

      strChildSeqIDs &= rowSeqID("SeqNumber")
    Next

    If Not String.IsNullOrEmpty(strChildSeqIDs) Then
      strFoundTree = FindRunbookExecutionTree(strChildSeqIDs)

      If Not String.IsNullOrEmpty(strFoundTree) Then
        FindRunbookExecutionTree &= "," & strFoundTree
      End If
    End If
  End Function

  Public Function FlushWebserviceCache(Optional FullReLoad As Boolean = False) As Boolean
    FlushWebserviceCache = Execute("TRUNCATE TABLE [Microsoft.SystemCenter.Orchestrator.Internal].[AuthorizationCache]")

    If FlushWebserviceCache Then
      Dim strSQLScript As String = ""

      If FullReLoad Then
        strSQLScript = "DECLARE @secToken int " & vbNewLine & _
                        "DECLARE tokenCursor CURSOR FOR SELECT Id FROM [Microsoft.SystemCenter.Orchestrator.Internal].SecurityTokens " & vbNewLine & _
                        "OPEN tokenCursor " & vbNewLine & _
                        "FETCH NEXT FROM tokenCursor INTO @secToken " & vbNewLine & _
                        "WHILE @@FETCH_STATUS = 0 " & vbNewLine & _
                        "BEGIN " & vbNewLine & _
                        "EXEC [Microsoft.SystemCenter.Orchestrator].ComputeAuthorizationCache @TokenId = @secToken " & vbNewLine & _
                        "FETCH NEXT FROM tokenCursor INTO @secToken " & vbNewLine & _
                        "END " & vbNewLine & _
                        "CLOSE tokenCursor " & vbNewLine & _
                        "DEALLOCATE tokenCursor"
      Else
        strSQLScript = "DECLARE @secToken int " & vbNewLine & _
                        "SELECT @secToken = Id FROM [Microsoft.SystemCenter.Orchestrator.Internal].SecurityTokens " & vbNewLine & _
                        "EXEC [Microsoft.SystemCenter.Orchestrator].ComputeAuthorizationCache @TokenId = @secToken "
      End If

      FlushWebserviceCache = Execute(strSQLScript)
    End If
  End Function

#Region "Audit"
  Public Function GetAuditInformation(FromDate As DateTime) As DataTable
    Dim UTCTime As DateTime = FromDate.ToUniversalTime

    GetAuditInformation = Query("SELECT 'Modified' AS ""Type"",[Id],[Name],[LastModifiedTime] AS ""DateTime"",[LastModifiedBy] AS ""By"",[Path] " & _
                 "FROM [Microsoft.SystemCenter.Orchestrator].[Runbooks] " & _
                 "WHERE [LastModifiedTime] >= '" & UTCTime.ToString("yyyy-MM-dd") & " 00:00:00' " & _
                 "UNION " & _
                 "SELECT 'Created' AS ""Type"",[Id],[Name],[CreationTime] AS ""DateTime"",[CreatedBy] AS ""By"",[Path] " & _
                 "FROM [Microsoft.SystemCenter.Orchestrator].[Runbooks] " & _
                 "WHERE [CreationTime] >= '" & UTCTime.ToString("yyyy-MM-dd") & " 00:00:00' " & _
                 "UNION " & _
                 "SELECT 'Checked Out' AS ""Type"",[Id],[Name],[CheckedOutTime] AS ""DateTime"",[CheckedOutBy] AS ""By"",[Path] " & _
                 "FROM [Microsoft.SystemCenter.Orchestrator].[Runbooks] " & _
                 "WHERE [CheckedOutTime] >= '" & UTCTime.ToString("yyyy-MM-dd") & " 00:00:00' " & _
                 "UNION " & _
                 "SELECT 'Deleted' AS ""Type"",NULL as UniqueID,pol.Name, pol.LastModified AS ""DateTime"", pol.LastModifiedBy AS ""By"", res.Path " & _
                 "FROM POLICIES AS pol INNER JOIN [Microsoft.SystemCenter.Orchestrator.Internal].Resources AS res ON pol.UniqueID = res.UniqueId " & _
                 "WHERE pol.LastModified >= '" & UTCTime.ToString("yyyy-MM-dd") & " 00:00:00' AND pol.Deleted=1 " & _
                 "ORDER BY 3 DESC, 2")

    For Each objAuditEntry As DataRow In GetAuditInformation.Rows
      If Not IsDBNull(objAuditEntry("By")) Then
        objAuditEntry("By") = Common.SIDtoUser(objAuditEntry("By"))
      End If

      If Not IsDBNull(objAuditEntry("DateTime")) Then
        objAuditEntry("DateTime") = TimeZoneInfo.ConvertTimeFromUtc(objAuditEntry("DateTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
      End If

      If Not IsDBNull(objAuditEntry("Path")) Then
        If objAuditEntry("Path").ToString().LastIndexOf("\") = 0 Then
          objAuditEntry("Path") = "Root Folder"
        Else
          objAuditEntry("Path") = objAuditEntry("Path").ToString().Substring(1, objAuditEntry("Path").ToString().LastIndexOf("\") - 1)
        End If
      End If
    Next
  End Function

  Public Function GetAuditClientConnections() As DataTable
    GetAuditClientConnections = Query("[ManagementServer],[ClientMachine],[ClientUser],[ClientVersion],[ConnectionTime],[LastActivity]", "[CLIENTCONNECTIONS]")

    For Each objEntry As DataRow In GetAuditClientConnections.Rows
      If Not IsDBNull(objEntry("ConnectionTime")) Then
        objEntry("ConnectionTime") = TimeZoneInfo.ConvertTimeFromUtc(objEntry("ConnectionTime"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
      End If

      If Not IsDBNull(objEntry("LastActivity")) Then
        objEntry("LastActivity") = TimeZoneInfo.ConvertTimeFromUtc(objEntry("LastActivity"), TimeZoneInfo.FindSystemTimeZoneById(GetDefaultTimeZone()))
      End If
    Next
  End Function

  Public Function GetInvokeRunbookActivityCalledByPath() As DataTable
    Return Query("SELECT [Activities].[Name] AS [ActivityName], [Runbooks].[Name] AS [RunbookName], [Runbooks].Path AS [RunbookPath] " & _
                 "FROM [Microsoft.SystemCenter.Orchestrator].[Activities] " & _
                 "INNER JOIN [Microsoft.SystemCenter.Orchestrator].[Runbooks] ON [Runbooks].[Id] = [Activities].[RunbookId] " & _
                 "WHERE [Activities].[Id] IN (SELECT [UniqueID] FROM [TRIGGER_POLICY] WHERE [TriggerByPolicyPath] = 1)")
  End Function

  Public Function GetLoggingEnabledRunbook() As DataTable
    Return Query("[Runbooks].[Name], [Runbooks].[Path], LogCommonData, LogSpecificData", "[Microsoft.SystemCenter.Orchestrator].[Runbooks] LEFT JOIN Policies ON Policies.UniqueID=[Runbooks].Id", "LogCommonData = 1 OR LogSpecificData = 1", sqlOrderBy:="[Runbooks].[Path]")
  End Function

  Public Function SearchVariableUsage(VariableID As String) As DataTable
    VariableID = "\`d.T.~Vb/{" & VariableID.ToUpper() & "}\`d.T.~Vb/"

    Dim TSQLStatement As String = ""

    TSQLStatement &= "DECLARE @TableList AS TABLE([ObjectType] uniqueidentifier, [ObjectTypeName] NVARCHAR(200), [TableName] NVARCHAR(200), [ColumnName] NVARCHAR(MAX))" & vbNewLine
    TSQLStatement &= "DECLARE @Results AS TABLE([UniqueID] uniqueidentifier, [Name] NVARCHAR(200), [ObjectTypeName] NVARCHAR(200))" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "INSERT INTO @TableList SELECT DISTINCT OBJECTS.ObjectType, OBJECTTYPES.Name, tables.name AS [Table], columns.name AS [Column]" & vbNewLine
    TSQLStatement &= "					   FROM OBJECTS" & vbNewLine
    TSQLStatement &= "					   INNER JOIN OBJECTTYPES ON OBJECTTYPES.UniqueID = OBJECTS.ObjectType" & vbNewLine
    TSQLStatement &= "					   INNER JOIN SYS.Tables ON tables.name = OBJECTTYPES.PrimaryDataTable OR tables.name = OBJECTTYPES.SecondaryDataTables" & vbNewLine
    TSQLStatement &= "					   INNER JOIN sys.columns ON columns.object_id = tables.object_id" & vbNewLine
    TSQLStatement &= "					   ORDER BY OBJECTTYPES.Name, tables.name, columns.name" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "DECLARE TablColumnList CURSOR FOR SELECT [ObjectTypeName], [TableName], [ColumnName] FROM @TableList" & vbNewLine
    TSQLStatement &= "DECLARE @Cur_ObjectTypeName NVARCHAR(200)" & vbNewLine
    TSQLStatement &= "DECLARE @Cur_Table NVARCHAR(200)" & vbNewLine
    TSQLStatement &= "DECLARE @Cur_Column NVARCHAR(MAX)" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "OPEN TablColumnList" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "FETCH NEXT FROM TablColumnList INTO @Cur_ObjectTypeName, @Cur_Table, @Cur_Table" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "WHILE @@FETCH_STATUS = 0" & vbNewLine
    TSQLStatement &= "	Begin" & vbNewLine
    TSQLStatement &= "		IF @Cur_Column IS NOT NULL" & vbNewLine
    TSQLStatement &= "				INSERT INTO @Results EXEC('SELECT [' + @Cur_Table + '].[UniqueID], '''', ''' + @Cur_ObjectTypeName + ''' FROM [' + @Cur_Table + '] LEFT JOIN [OBJECTS] ON [' + @Cur_Table + '].[UniqueID] = [OBJECTS].[UniqueID] AND [OBJECTS].[Deleted] = 0  WHERE [OBJECTS].[UniqueID] IS NOT NULL AND [' + @Cur_Table + '].[' + @Cur_Column + '] LIKE ''%" & VariableID & "%''')" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "		FETCH NEXT FROM TablColumnList INTO @Cur_ObjectTypeName, @Cur_Table, @Cur_Column	" & vbNewLine
    TSQLStatement &= "	End" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "CLOSE TablColumnList" & vbNewLine
    TSQLStatement &= "DEALLOCATE TablColumnList" & vbNewLine
    TSQLStatement &= "" & vbNewLine
    TSQLStatement &= "SELECT [Runbooks].[Path] AS [Runbook], [Activities].[Name] AS [Activity], [Activities].[TypeName] AS [Activity Type]" & vbNewLine
    TSQLStatement &= "FROM @Results t1 " & vbNewLine
    TSQLStatement &= "INNER JOIN [Microsoft.SystemCenter.Orchestrator].[Activities] ON [Activities].[Id] = t1.[UniqueID]" & vbNewLine
    TSQLStatement &= "INNER JOIN [Microsoft.SystemCenter.Orchestrator].[Runbooks] ON [Runbooks].[Id] = [Activities].[RunbookId]" & vbNewLine
    TSQLStatement &= "ORDER BY [Runbooks].[Path], [Activities].[Name]" & vbNewLine

    Return Query(TSQLStatement)
  End Function
#End Region

#Region "User and Password Change"
  Public Function GetObjectNameFromTable(TableName As String) As String
    GetObjectNameFromTable = "Not Found"

    Dim tempDT As DataTable = Query("[Name]", "[OBJECTTYPES]", "PrimaryDataTable='" & TableName & "' OR SecondaryDataTables LIKE '%" & TableName & "%'")

    If tempDT.Rows.Count = 1 Then
      GetObjectNameFromTable = tempDT.Rows(0)("Name")
    End If
  End Function

  Public Function GetPossibleVariablesWithPassword() As DataTable
    Return Query("[VARIABLES].[UniqueID],[OBJECTS].[Name]", "[VARIABLES] INNER JOIN [OBJECTS] ON [OBJECTS].UniqueID=[VARIABLES].[UniqueID]", "[VARIABLES].[Value] LIKE '\`d.T.~De/%'")
  End Function

  Public Function GetPossibleConfigurationsWithPassword() As DataTable
    Return Query("SELECT [CAPS].[UniqueID],[CAPS].[Name] + ' (v' + CAST([CAPS].[Version] AS VARCHAR(50)) + ')' AS DisplayName " & _
                  "FROM [CAPS] " & _
                  "INNER JOIN [CONFIGURATION] ON [CAPS].[UniqueID]=[CONFIGURATION].[TypeGUID] " & _
                  "WHERE [CONFIGURATION].[DataName]='Configurations' AND [CONFIGURATION].[DataValue] LIKE '%\`d.T.~De/%' " & _
                  "ORDER BY [CAPS].[Name]")
  End Function

  Public Function GetUniqueIDsToUpdate(TableName As String, UserIDField As String, UserIDToSearch As String, DomainToSearch As String, DomainField As String, PasswordField As String, Optional Filter As String = "") As DataTable
    Dim strWhereClause As String = ""

    For Each strUserIDValue As String In UserIDToSearch.Split(vbCr)
      If strWhereClause <> "" Then
        strWhereClause &= " OR "
      End If

      strWhereClause &= "(" & UserIDField & " LIKE '" & strUserIDValue.Replace(vbNewLine, "") & "'" & If(Not String.IsNullOrEmpty(DomainField), " AND " & DomainField & " LIKE '" & DomainToSearch & "%'", "") & ")"
    Next

    If strWhereClause <> "" Then
      strWhereClause = "(" & strWhereClause & ") AND "
    End If

    strWhereClause &= PasswordField & " LIKE '%\`d.T.~De/%'"

    If Filter <> "" Then
      strWhereClause &= " AND " & Filter
    End If

    Return Query("DISTINCT UniqueID," & PasswordField, TableName, strWhereClause)
  End Function

  Public Function UpdatePassword(TableName As String, UniqueID As String, PasswordField As String, NewPassword As String, PasswordFieldValue As String) As Boolean
    Dim strPasswordPieceOldValue As String = objRegEx.Match(PasswordFieldValue).Value
    Dim strPasswordPieceNewValue As String = PasswordFieldValue.Replace(strPasswordPieceOldValue, EncryptString(NewPassword))

    Return Execute("UPDATE " & TableName & " SET " & PasswordField & "='" & strPasswordPieceNewValue & "' WHERE UniqueID='" & UniqueID & "'")
  End Function

  Private Function EncryptString(Input As String) As String
    Return "\`d.T.~De/" & Execute("[Microsoft.SystemCenter.Orchestrator.Cryptography].[Encrypt]", New Object() {Input, Nothing}, "@EncryptedData") & "\`d.T.~De/"
  End Function

  Private Function DecryptString(Input As String) As String
    If Input.StartsWith("\`d.T.~De/") Then
      Input = Input.Replace("\`d.T.~De/", "")
    End If

    Return Execute("[Microsoft.SystemCenter.Orchestrator.Cryptography].[Decrypt]", New Object() {Input, Nothing}, "@DecryptedData")
  End Function

  Public Function UpdateVariables(OldPassword As String, NewPassword As String, objLog As IO.StreamWriter, Simulated As Boolean) As Boolean
    UpdateVariables = True

    Common.WriteLog(objLog, vbTab & "Table: Variables")
    Common.WriteLog(objLog, vbTab & "Value Field: Value")
    Common.WriteLog(objLog, vbTab & "Searching Encrypted Variables")

    Dim dtVariablesToUpdate As DataTable = Query("[UniqueID],[Value]", "[VARIABLES]", "[Value] LIKE '\`d.T.~De/%'")

    Common.WriteLog(objLog, vbTab & "Encrypted Variables Found: " & dtVariablesToUpdate.Rows.Count)

    For Each rowVariable As DataRow In dtVariablesToUpdate.Rows
      If DecryptString(rowVariable("Value").ToString()) = OldPassword Then
        If Not Simulated Then
          Common.WriteLog(objLog, vbTab & vbTab & "Updating Password On: " & GetVariablePath(rowVariable("UniqueID").ToString()))

          If Execute("UPDATE [VARIABLES] SET [Value]='" & EncryptString(NewPassword) & "' WHERE [UniqueID]='" & rowVariable("UniqueID").ToString() & "'") Then
            UpdateVariables = UpdateVariables And True
            Common.WriteLog(objLog, vbTab & vbTab & "Successfully Changed")
          Else
            UpdateVariables = UpdateVariables And False
            Common.WriteLog(objLog, vbTab & vbTab & "Failed To Changed")
          End If
        Else
          Common.WriteLog(objLog, vbTab & vbTab & "Updating Password On: " & GetVariablePath(rowVariable("UniqueID").ToString()) & " (Simulated - Sleep: 0.5 second)")
          Threading.Thread.Sleep(500)
        End If
      End If
    Next
  End Function

  Public Function UpdateConfigurations(OldPassword As String, NewPassword As String, objLog As IO.StreamWriter, Simulated As Boolean) As Boolean
    UpdateConfigurations = True

    Common.WriteLog(objLog, vbTab & "Table: Configuration")
    Common.WriteLog(objLog, vbTab & "Value Field: DataValue")
    Common.WriteLog(objLog, vbTab & "Searching Configurations With Passwords")

    Dim dtConfigurationsToUpdate As DataTable = Query("[UniqueID],[DataValue]", "[CONFIGURATION]", "[DataName]='Configurations' AND [DataValue] LIKE '%\`d.T.~De/%'")

    Common.WriteLog(objLog, vbTab & "Configurations With Password Found: " & dtConfigurationsToUpdate.Rows.Count)

    Dim strTempConfiguration As String = ""
    Dim boolUpdateTable As Boolean = False

    For Each rowConfiguration As DataRow In dtConfigurationsToUpdate.Rows
      strTempConfiguration = rowConfiguration("DataValue")
      boolUpdateTable = False

      For Each regexPwdMatch As Match In objRegEx.Matches(strTempConfiguration)
        If DecryptString(regexPwdMatch.Value) = OldPassword Then
          boolUpdateTable = True

          If Not Simulated Then
            strTempConfiguration = strTempConfiguration.Replace(regexPwdMatch.Value, EncryptString(NewPassword))
          End If
        End If
      Next

      If boolUpdateTable Then
        If Not Simulated Then
          Common.WriteLog(objLog, vbTab & vbTab & "Updating Password On: " & GetConfigurationName(rowConfiguration("UniqueID").ToString()))

          If Execute("UPDATE [CONFIGURATION] SET [DataValue]='" & strTempConfiguration & "' WHERE [UniqueID]='" & rowConfiguration("UniqueID").ToString() & "'") Then
            UpdateConfigurations = UpdateConfigurations And True
            Common.WriteLog(objLog, vbTab & vbTab & "Successfully Changed")
          Else
            UpdateConfigurations = UpdateConfigurations And False
            Common.WriteLog(objLog, vbTab & vbTab & "Failed To Changed")
          End If
        Else
          Common.WriteLog(objLog, vbTab & vbTab & "Updating Password On: " & GetConfigurationName(rowConfiguration("UniqueID").ToString()) & " (Simulated - Sleep: 0.5 second)")
          Threading.Thread.Sleep(500)
        End If
      End If
    Next
  End Function

  Public Function IncreaseRunbookVersion(strRunbookID As String) As Boolean
    Return Execute("UPDATE [POLICIES] SET [Version]=[Version]+1,[LastModified]=GETUTCDATE(),[LastModifiedBy]='" & GlobalVariables.strCurrentUserSID & "' WHERE UniqueID='" & strRunbookID & "'")
  End Function
#End Region

#Region "Running Statistics"
  Public Function GetStatsExecutingRunbooks() As DataTable
    Return Query("[Microsoft.SystemCenter.Orchestrator].Runbooks.Id, [Microsoft.SystemCenter.Orchestrator].Runbooks.Name, [Microsoft.SystemCenter.Orchestrator].Runbooks.IsMonitor , ACTIONSERVERS.Computer AS [RunbookServer], COUNT(*) AS [Running]",
                                       "POLICYINSTANCES INNER JOIN [Microsoft.SystemCenter.Orchestrator].Runbooks ON POLICYINSTANCES.PolicyID = [Microsoft.SystemCenter.Orchestrator].Runbooks.Id INNER JOIN ACTIONSERVERS ON ACTIONSERVERS.UniqueID = POLICYINSTANCES.ActionServer",
                                       "POLICYINSTANCES.TimeEnded IS NULL",
                                       "[Microsoft.SystemCenter.Orchestrator].Runbooks.Id,[Microsoft.SystemCenter.Orchestrator].Runbooks.Name,ACTIONSERVERS.Computer, [Microsoft.SystemCenter.Orchestrator].Runbooks.IsMonitor")
  End Function

  Public Function GetStatsRunbookServers() As DataTable
    Return Query("[Computer],[MaxRunningPolicies]", "[ACTIONSERVERS]", sqlOrderBy:="[Computer]")
  End Function

  Public Function GetRunbooksTypeCount() As DataTable
    Return Query("SELECT 'Monitor' AS [RunbookType], tDefinedInstance.[DefinedInstances] - tDisabledInstances.[MonitorDisabledCount] AS [EnabledInstances]  ,tDisabledInstances.[MonitorDisabledCount] AS [DisabledInstances], tDefinedInstance.[DefinedInstances] AS [TotalInstances] " & _
                 "FROM (SELECT Count(*) AS [DefinedInstances] FROM [Microsoft.SystemCenter.Orchestrator].[Runbooks] WHERE Name LIKE 'Monitor - %' OR IsMonitor = 1) AS tDefinedInstance, (SELECT COUNT(*) AS [MonitorDisabledCount] FROM [Microsoft.SystemCenter.Orchestrator].[Runbooks] WHERE IsMonitor = 1 AND Name LIKE 'Disable%') AS tDisabledInstances " & _
                 "UNION " & _
                 "SELECT 'Ad-hoc' AS [RunbookType], tDefinedInstance.[DefinedInstances], 0, tDefinedInstance.[DefinedInstances] AS [TotalInstances] " & _
                 "FROM (SELECT Count(*) AS [DefinedInstances] FROM [Microsoft.SystemCenter.Orchestrator].[Runbooks] WHERE Name NOT LIKE 'Monitor - %' OR IsMonitor = 0) AS tDefinedInstance")
  End Function

  Public Function GetMonitorRunbooksStats() As DataTable
    Return Query("Runbooks.[Name], Runbooks.[Path], (CASE COUNT(POLICY_PUBLISH_QUEUE.SeqNumber) WHEN 0 THEN 'Stopped' ELSE 'Running' END) AS [RunbookStatus]", _
                 "[Microsoft.SystemCenter.Orchestrator].[Runbooks] LEFT JOIN POLICY_PUBLISH_QUEUE ON POLICY_PUBLISH_QUEUE.PolicyID = [Microsoft.SystemCenter.Orchestrator].Runbooks.Id", _
                 "(Name LIKE 'Monitor - %' OR IsMonitor = 1) AND Name NOT LIKE 'Disable%'", "Runbooks.[Name], Runbooks.[Path]", "Runbooks.[Name]")
  End Function
#End Region
End Module
