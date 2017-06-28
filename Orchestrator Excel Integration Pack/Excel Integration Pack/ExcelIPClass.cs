using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;

// Testing Notes: When replacing the .dll file and using an existing runbook activity, you must open up the activity and re-save it,
// otherwise Orchestrator will not re-load the .dll to get the updated version.

// Debug Notes: To debug this integration pack, create a runbook that calls the activity.  Executing it using the runbook tester
// and connect to the local policymodule.exe process.  Ensure that the pqb files exist with the .dll file.  Debug using Managed (v2.0, v1.1, v1.0) code


namespace Excel_IP
{
        [Activity ("Read Excel Document",Description="Used to read the contents of an xls, xlsx or csv file.",ShowInputs=true,ShowFilters=true)]
    public class Read_Excel_Document : IActivity
    {
        int[] TypeGuessValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        string FieldValue_ExcelFilePath = "";
        string FieldValue_HasHeaderRow = "True";
        string FieldValue_ForceMixedData = "True";
        int FieldValue_TypeGuessRows = 8;
        string FieldValue_CSVDelimiter = ",";
        string FieldValue_SheetName = "Sheet1";
        string FieldValue_SemiColonReplacement = "_";
        Excel ExcelObj = new Excel();

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Excel File Path").WithDefaultValue(FieldValue_ExcelFilePath);
            designer.AddInput("Has Header Row").WithBooleanBrowser().WithDefaultValue(FieldValue_HasHeaderRow);
            designer.AddInput("Force mixed data to be converted to text").WithBooleanBrowser().WithDefaultValue(FieldValue_ForceMixedData).NotRequired();
            designer.AddInput("TypeGuessRows").WithListBrowser(TypeGuessValues).WithDefaultValue(FieldValue_TypeGuessRows).NotRequired();
            designer.AddInput("; Replacement").WithDefaultValue(FieldValue_SemiColonReplacement).NotRequired();
            designer.AddInput("CSV Delimiter").WithDefaultValue(FieldValue_CSVDelimiter).NotRequired();
            designer.AddInput("Sheet Name").WithDefaultValue(FieldValue_SheetName).NotRequired();

            designer.AddOutput("Excel File Path").AsString();
            designer.AddOutput("Full line as string with fields separated by ;").AsString();
            designer.AddOutput("Number of rows").AsNumber();
            designer.AddOutput("Has Header Row").AsString();
            designer.AddOutput("Force mixed data to be converted to text").AsString();
            designer.AddOutput("TypeGuessRows").AsNumber();
            designer.AddOutput("Select filter").AsString();
            designer.AddOutput("Where filter").AsString();
            designer.AddOutput("; Replacement").AsString();
            designer.AddOutput("CSV Delimiter").AsString();
            designer.AddOutput("Sheet Name").AsString();

            designer.AddFilter("Select filter").WithRelations( Relation.EqualTo);
            designer.AddFilter("Where filter").WithRelations(Relation.EqualTo);
        }  
      
        public void Execute(IActivityRequest request, IActivityResponse response)
        {

            string FilePath = request.Inputs["Excel File Path"].AsString();
            string SheetName = request.Inputs["Sheet Name"].AsString();
            if (SheetName == "") { SheetName = FieldValue_SheetName; }
            bool HasHeaderRow = request.Inputs["Has Header Row"].AsBoolean();
            bool ForceMixedDataAsText;
            string CSVDelimiter = request.Inputs["CSV Delimiter"].AsString();
            if (CSVDelimiter == "") { CSVDelimiter = FieldValue_CSVDelimiter; }
            if (request.Inputs["Force mixed data to be converted to text"].ToString() != "")
            {
                ForceMixedDataAsText = request.Inputs["Force mixed data to be converted to text"].AsBoolean();
            }
            else{
                ForceMixedDataAsText = true;
            }

            int TypeGuessRows;
            if (request.Inputs["TypeGuessRows"].ToString() != "")
            {
                TypeGuessRows = request.Inputs["TypeGuessRows"].AsInt16();
            }
            else{
                TypeGuessRows = 8;
            }
            string SemiColonReplacement = request.Inputs["; Replacement"].AsString();
            if (SemiColonReplacement == "")
            {
                SemiColonReplacement = "_";
            }

            //Read Filter Values
            string SelectFilter = "Select *";
            string WhereFilter = "";
            foreach (IFilterCriteria filter in request.Filters)
            {
                switch (filter.Name)
                {
                    case "Select filter":
                        switch (filter.Relation)
                        {
                            case Relation.EqualTo:
                                SelectFilter = filter.Value.AsString();
                                break;
                        }
                        break;
                    case "Where filter":
                        switch (filter.Relation)
                        {
                            case Relation.EqualTo:
                                WhereFilter = filter.Value.AsString();
                                break;
                        }
                        break;
                }
            }
            if (SelectFilter == "") { SelectFilter = "Select *"; }
            else if (SelectFilter.ToLower().StartsWith ("select ") == false) { SelectFilter = "Select " + SelectFilter; }
            if (WhereFilter != "" && WhereFilter.ToLower().StartsWith ("where ") == false) { WhereFilter = "Where " + WhereFilter; }

            System.Collections.IEnumerable FullLines = null;
            int RowCount = 0;
            DataSet ds = null;
            OleDbConnection con = null;
            ExcelObj = new Excel();
            Excel.ExcelFileType FileType = Excel.GetFileType(FilePath);

            try
            {
                con = ExcelObj.OpenExcelFile(FilePath, SheetName, FileType, HasHeaderRow, ForceMixedDataAsText, TypeGuessRows, CSVDelimiter);
                ds = ExcelObj.ReadSheetFromExcel(FilePath, SheetName, FileType, ref con, SelectFilter, WhereFilter);
            }
            catch(System.Exception ex)
            {
                ExcelObj.CloseExcelFile(ref con);   //Ensure the excel file is closed before throwing the error.
                throw (ex);
            }
            ExcelObj.CloseExcelFile(ref con);
            
            
            if (ds.Tables.Count > 0)
            {
                RowCount = ds.Tables[0].Rows.Count;
                FullLines = GetFullLines(ds, SemiColonReplacement);
            }            
                        
            response.Publish("Excel File Path", FilePath);
            response.Publish("Sheet Name", SheetName);
            response.Publish("Number of rows", RowCount);
            response.Publish("Has Header Row", HasHeaderRow);
            response.Publish("Force mixed data to be converted to text", ForceMixedDataAsText);
            response.Publish("TypeGuessRows", TypeGuessRows);
            response.Publish("CSV Delimiter", CSVDelimiter);
            response.Publish("; Replacement", SemiColonReplacement);
            response.Publish("Select filter", SelectFilter );
            response.Publish("Where filter", WhereFilter );
            
            if (FullLines != null)
            {
                response.PublishRange("Full line as string with fields separated by ;", FullLines);
            }
            else
            {
                response.Publish("Full line as string with fields separated by ;", "No data returned");
            }

        }

        //Used to create an IEnumerable type used for publishing a collection of values
        private static System.Collections.IEnumerable GetFullLines(DataSet ds, string SemiColonReplacement)
        {
            DataRow Row;
            String Line;
                       
  
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Row = ds.Tables[0].Rows[i];
                Line = "";

                foreach (Object Value in Row.ItemArray)
                {
                    Line = Line + Regex.Replace(Value.ToString().Trim(), ";", SemiColonReplacement) + ";";  //Replace all ; characters to _ so that the data publishes correctly to the data bus
                }

                Line = Line.Substring(0, Line.Length - 1);  //Remove trailing ; character    
                yield return Line; 
            }
        }
            
    }
    
        [Activity("Insert Row Excel Document",Description="Used to insert a row into an xls, xlsx or csv file.",ShowInputs=true,ShowFilters=false)]
    public class Insert_Row_Excel_Document : IActivity
        {
            int[] TypeGuessValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            string FieldValue_ExcelFilePath = "";
            string FieldValue_HasHeaderRow = "True";
            string FieldValue_ForceMixedData = "False";
            int FieldValue_TypeGuessRows = 8;
            string FieldValue_CSVDelimiter = ",";
            string FieldValue_SheetName = "Sheet1";
            string FieldValue_ColumnName1 = "";
            string FieldValue_ColumnValue1 = "";
            string FieldValue_ColumnName2 = "";
            string FieldValue_ColumnValue2 = "";
            string FieldValue_ColumnName3 = "";
            string FieldValue_ColumnValue3 = "";
            string FieldValue_ColumnName4 = "";
            string FieldValue_ColumnValue4 = "";
            string FieldValue_ColumnName5 = "";
            string FieldValue_ColumnValue5 = "";
            string FieldValue_ColumnName6 = "";
            string FieldValue_ColumnValue6 = "";
            string FieldValue_ColumnName7 = "";
            string FieldValue_ColumnValue7 = "";
            string FieldValue_ColumnName8 = "";
            string FieldValue_ColumnValue8 = "";
            string FieldValue_ColumnName9 = "";
            string FieldValue_ColumnValue9 = "";
            string FieldValue_ColumnName10 = "";
            string FieldValue_ColumnValue10 = "";
            string FieldValue_ColumnName11 = "";
            string FieldValue_ColumnValue11 = "";
            string FieldValue_ColumnName12 = "";
            string FieldValue_ColumnValue12 = "";
            string FieldValue_ColumnName13 = "";
            string FieldValue_ColumnValue13 = "";
            string FieldValue_ColumnName14 = "";
            string FieldValue_ColumnValue14 = "";
            string FieldValue_ColumnName15 = "";
            string FieldValue_ColumnValue15 = "";
            string FieldValue_ColumnName16 = "";
            string FieldValue_ColumnValue16 = "";
            string FieldValue_ColumnName17 = "";
            string FieldValue_ColumnValue17 = "";
            string FieldValue_ColumnName18 = "";
            string FieldValue_ColumnValue18 = "";
            string FieldValue_ColumnName19 = "";
            string FieldValue_ColumnValue19 = "";
            string FieldValue_ColumnName20 = "";
            string FieldValue_ColumnValue20 = "";
            Excel ExcelObj = new Excel();

            public void Design(IActivityDesigner designer)
            {
                designer.AddInput("Excel File Path").WithDefaultValue(FieldValue_ExcelFilePath);
                designer.AddInput("Has Header Row").WithBooleanBrowser().WithDefaultValue(FieldValue_HasHeaderRow);
                designer.AddInput("Force mixed data to be converted to text").WithBooleanBrowser().WithDefaultValue(FieldValue_ForceMixedData).NotRequired();
                designer.AddInput("TypeGuessRows").WithListBrowser(TypeGuessValues).WithDefaultValue(FieldValue_TypeGuessRows).NotRequired();
                designer.AddInput("Column Name 1").WithDefaultValue(FieldValue_ColumnName1);
                designer.AddInput("Column Value 1").WithDefaultValue(FieldValue_ColumnValue1);
                designer.AddInput("Column Name 2").WithDefaultValue(FieldValue_ColumnName2);
                designer.AddInput("Column Value 2").WithDefaultValue(FieldValue_ColumnValue2);
                designer.AddInput("Column Name 3").WithDefaultValue(FieldValue_ColumnName3);
                designer.AddInput("Column Value 3").WithDefaultValue(FieldValue_ColumnValue3);
                designer.AddInput("Column Name 4").WithDefaultValue(FieldValue_ColumnName4);
                designer.AddInput("Column Value 4").WithDefaultValue(FieldValue_ColumnValue4);
                designer.AddInput("Column Name 5").WithDefaultValue(FieldValue_ColumnName5);
                designer.AddInput("Column Value 5").WithDefaultValue(FieldValue_ColumnValue5);
                designer.AddInput("Column Name 6").WithDefaultValue(FieldValue_ColumnName6);
                designer.AddInput("Column Value 6").WithDefaultValue(FieldValue_ColumnValue6);
                designer.AddInput("Column Name 7").WithDefaultValue(FieldValue_ColumnName7);
                designer.AddInput("Column Value 7").WithDefaultValue(FieldValue_ColumnValue7);
                designer.AddInput("Column Name 8").WithDefaultValue(FieldValue_ColumnName8);
                designer.AddInput("Column Value 8").WithDefaultValue(FieldValue_ColumnValue8);
                designer.AddInput("Column Name 9").WithDefaultValue(FieldValue_ColumnName9);
                designer.AddInput("Column Value 9").WithDefaultValue(FieldValue_ColumnValue9);
                designer.AddInput("Column Name 10").WithDefaultValue(FieldValue_ColumnName10);
                designer.AddInput("Column Value 10").WithDefaultValue(FieldValue_ColumnValue10);
                designer.AddInput("Column Name 11").WithDefaultValue(FieldValue_ColumnName11);
                designer.AddInput("Column Value 11").WithDefaultValue(FieldValue_ColumnValue11);
                designer.AddInput("Column Name 12").WithDefaultValue(FieldValue_ColumnName12);
                designer.AddInput("Column Value 12").WithDefaultValue(FieldValue_ColumnValue12);
                designer.AddInput("Column Name 13").WithDefaultValue(FieldValue_ColumnName13);
                designer.AddInput("Column Value 13").WithDefaultValue(FieldValue_ColumnValue13);
                designer.AddInput("Column Name 14").WithDefaultValue(FieldValue_ColumnName14);
                designer.AddInput("Column Value 14").WithDefaultValue(FieldValue_ColumnValue14);
                designer.AddInput("Column Name 15").WithDefaultValue(FieldValue_ColumnName15);
                designer.AddInput("Column Value 15").WithDefaultValue(FieldValue_ColumnValue15);
                designer.AddInput("Column Name 16").WithDefaultValue(FieldValue_ColumnName16);
                designer.AddInput("Column Value 16").WithDefaultValue(FieldValue_ColumnValue16);
                designer.AddInput("Column Name 17").WithDefaultValue(FieldValue_ColumnName17);
                designer.AddInput("Column Value 17").WithDefaultValue(FieldValue_ColumnValue17);
                designer.AddInput("Column Name 18").WithDefaultValue(FieldValue_ColumnName18);
                designer.AddInput("Column Value 18").WithDefaultValue(FieldValue_ColumnValue18);
                designer.AddInput("Column Name 19").WithDefaultValue(FieldValue_ColumnName19);
                designer.AddInput("Column Value 19").WithDefaultValue(FieldValue_ColumnValue19);
                designer.AddInput("Column Name 20").WithDefaultValue(FieldValue_ColumnName20);
                designer.AddInput("Column Value 20").WithDefaultValue(FieldValue_ColumnValue20);
                designer.AddInput("CSV Delimiter").WithDefaultValue(FieldValue_CSVDelimiter).NotRequired();                
                designer.AddInput("Sheet Name").WithDefaultValue(FieldValue_SheetName).NotRequired();                

                designer.AddOutput("Excel File Path").AsString();
                designer.AddOutput("Has Header Row").AsString();
                designer.AddOutput("Force mixed data to be converted to text").AsString();
                designer.AddOutput("TypeGuessRows").AsNumber();
                designer.AddOutput("CSV Delimiter").AsString();
                designer.AddOutput("Sheet Name").AsString();
            }                                 

            private void UpdateFieldValues(IActivityRequest request)
            {
                FieldValue_ExcelFilePath = request.Inputs["Excel File Path"].AsString();
                FieldValue_HasHeaderRow = request.Inputs["Has Header Row"].AsString();
                FieldValue_ForceMixedData = request.Inputs["Force mixed data to be converted to text"].AsString();
                if (FieldValue_ForceMixedData == "") { FieldValue_ForceMixedData = "False"; }
                try { FieldValue_TypeGuessRows = request.Inputs["TypeGuessRows"].AsInt16(); }
                catch { FieldValue_TypeGuessRows = 8; }
                FieldValue_CSVDelimiter = request.Inputs["CSV Delimiter"].AsString();
                FieldValue_SheetName = request.Inputs["Sheet Name"].AsString();
                FieldValue_ColumnName1 = request.Inputs["Column Name 1"].AsString();
                FieldValue_ColumnValue1 = request.Inputs["Column Value 1"].AsString();
                FieldValue_ColumnName2 = request.Inputs["Column Name 2"].AsString();
                FieldValue_ColumnValue2 = request.Inputs["Column Value 2"].AsString();
                FieldValue_ColumnName3 = request.Inputs["Column Name 3"].AsString();
                FieldValue_ColumnValue3 = request.Inputs["Column Value 3"].AsString();
                FieldValue_ColumnName4 = request.Inputs["Column Name 4"].AsString();
                FieldValue_ColumnValue4 = request.Inputs["Column Value 4"].AsString();
                FieldValue_ColumnName5 = request.Inputs["Column Name 5"].AsString();
                FieldValue_ColumnValue5 = request.Inputs["Column Value 5"].AsString();
                FieldValue_ColumnName6 = request.Inputs["Column Name 6"].AsString();
                FieldValue_ColumnValue6 = request.Inputs["Column Value 6"].AsString();
                FieldValue_ColumnName7 = request.Inputs["Column Name 7"].AsString();
                FieldValue_ColumnValue7 = request.Inputs["Column Value 7"].AsString();
                FieldValue_ColumnName8 = request.Inputs["Column Name 8"].AsString();
                FieldValue_ColumnValue8 = request.Inputs["Column Value 8"].AsString();
                FieldValue_ColumnName9 = request.Inputs["Column Name 9"].AsString();
                FieldValue_ColumnValue9 = request.Inputs["Column Value 9"].AsString();
                FieldValue_ColumnName10 = request.Inputs["Column Name 10"].AsString();
                FieldValue_ColumnValue10 = request.Inputs["Column Value 10"].AsString();
                FieldValue_ColumnName11 = request.Inputs["Column Name 11"].AsString();
                FieldValue_ColumnValue11 = request.Inputs["Column Value 11"].AsString();
                FieldValue_ColumnName12 = request.Inputs["Column Name 12"].AsString();
                FieldValue_ColumnValue12 = request.Inputs["Column Value 12"].AsString();
                FieldValue_ColumnName13 = request.Inputs["Column Name 13"].AsString();
                FieldValue_ColumnValue13 = request.Inputs["Column Value 13"].AsString();
                FieldValue_ColumnName14 = request.Inputs["Column Name 14"].AsString();
                FieldValue_ColumnValue14 = request.Inputs["Column Value 14"].AsString();
                FieldValue_ColumnName15 = request.Inputs["Column Name 15"].AsString();
                FieldValue_ColumnValue15 = request.Inputs["Column Value 15"].AsString();
                FieldValue_ColumnName16 = request.Inputs["Column Name 16"].AsString();
                FieldValue_ColumnValue16 = request.Inputs["Column Value 16"].AsString();
                FieldValue_ColumnName17 = request.Inputs["Column Name 17"].AsString();
                FieldValue_ColumnValue17 = request.Inputs["Column Value 17"].AsString();
                FieldValue_ColumnName18 = request.Inputs["Column Name 18"].AsString();
                FieldValue_ColumnValue18 = request.Inputs["Column Value 18"].AsString();
                FieldValue_ColumnName19 = request.Inputs["Column Name 19"].AsString();
                FieldValue_ColumnValue19 = request.Inputs["Column Value 19"].AsString();
                FieldValue_ColumnName20 = request.Inputs["Column Name 20"].AsString();
                FieldValue_ColumnValue20 = request.Inputs["Column Value 20"].AsString();                  
            }

            public void Execute(IActivityRequest request, IActivityResponse response)
            {
                OleDbConnection con = null;
                UpdateFieldValues(request);
                Excel.ExcelFileType FileType = Excel.GetFileType(FieldValue_ExcelFilePath);
                
                try
                {                    
                    con = ExcelObj.OpenExcelFile(FieldValue_ExcelFilePath, FieldValue_SheetName, FileType, Convert.ToBoolean(FieldValue_HasHeaderRow), Convert.ToBoolean(FieldValue_ForceMixedData), FieldValue_TypeGuessRows, FieldValue_CSVDelimiter);
                    ExcelObj.InsertRowIntoExcel(FieldValue_ExcelFilePath, FieldValue_SheetName, FileType, ref con, 
                        FieldValue_ColumnName1, FieldValue_ColumnValue1, FieldValue_ColumnName2, FieldValue_ColumnValue2, 
                        FieldValue_ColumnName3, FieldValue_ColumnValue3, FieldValue_ColumnName4, FieldValue_ColumnValue4, 
                        FieldValue_ColumnName5, FieldValue_ColumnValue5, FieldValue_ColumnName6, FieldValue_ColumnValue6, 
                        FieldValue_ColumnName7, FieldValue_ColumnValue7, FieldValue_ColumnName8, FieldValue_ColumnValue8, 
                        FieldValue_ColumnName9, FieldValue_ColumnValue9, FieldValue_ColumnName10, FieldValue_ColumnValue10, 
                        FieldValue_ColumnName11, FieldValue_ColumnValue11, FieldValue_ColumnName12, FieldValue_ColumnValue12, 
                        FieldValue_ColumnName13, FieldValue_ColumnValue13, FieldValue_ColumnName14, FieldValue_ColumnValue14, 
                        FieldValue_ColumnName15, FieldValue_ColumnValue15, FieldValue_ColumnName16, FieldValue_ColumnValue16, 
                        FieldValue_ColumnName17, FieldValue_ColumnValue17, FieldValue_ColumnName18, FieldValue_ColumnValue18, 
                        FieldValue_ColumnName19, FieldValue_ColumnValue19, FieldValue_ColumnName20, FieldValue_ColumnValue20);
                }
                catch (System.Exception ex)
                {
                    ExcelObj.CloseExcelFile(ref con);   //Ensure the excel file is closed before throwing the error.
                    throw (ex);
                }
                ExcelObj.CloseExcelFile(ref con);


                response.Publish("Excel File Path", FieldValue_ExcelFilePath);
                response.Publish("Sheet Name", FieldValue_SheetName);
                response.Publish("Has Header Row", FieldValue_HasHeaderRow);
                response.Publish("Force mixed data to be converted to text", FieldValue_ForceMixedData);
                response.Publish("TypeGuessRows", FieldValue_TypeGuessRows);
                response.Publish("CSV Delimiter", FieldValue_CSVDelimiter);

            }

        } 

        [Activity("Update Excel Document",Description="Used to modify the contents of an xls or xlsx file.",ShowInputs=true,ShowFilters=false)]
    public class Update_Excel_Document : IActivity
        {
        int[] TypeGuessValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        string FieldValue_ExcelFilePath = "";
        string FieldValue_HasHeaderRow = "True";
        string FieldValue_ForceMixedData = "False";
        int FieldValue_TypeGuessRows = 8;
        string FieldValue_CSVDelimiter = ",";
        string FieldValue_SheetName = "Sheet1";
        string FieldValue_UpdateStatement = "Set [ColumnName] = 'Value' WHERE [ColumnName] = 'OldValue'";
        Excel ExcelObj = new Excel();

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Excel File Path").WithDefaultValue(FieldValue_ExcelFilePath);
            designer.AddInput("Has Header Row").WithBooleanBrowser().WithDefaultValue(FieldValue_HasHeaderRow);
            designer.AddInput("Force mixed data to be converted to text").WithBooleanBrowser().WithDefaultValue(FieldValue_ForceMixedData).NotRequired();
            designer.AddInput("TypeGuessRows").WithListBrowser(TypeGuessValues).WithDefaultValue(FieldValue_TypeGuessRows).NotRequired();
            designer.AddInput("Update statement").WithDefaultValue(FieldValue_UpdateStatement);
            designer.AddInput("CSV Delimiter").WithDefaultValue(FieldValue_CSVDelimiter).NotRequired();            
            designer.AddInput("Sheet Name").WithDefaultValue(FieldValue_SheetName).NotRequired();            

            designer.AddOutput("Excel File Path").AsString();
            designer.AddOutput("Full line as string with fields separated by ;").AsString();
            designer.AddOutput("Number of rows").AsNumber();
            designer.AddOutput("Has Header Row").AsString();
            designer.AddOutput("Force mixed data to be converted to text").AsString();
            designer.AddOutput("TypeGuessRows").AsNumber();
            designer.AddOutput("Update statement").AsString();
            designer.AddOutput("CSV Delimiter").AsString();
            designer.AddOutput("Sheet Name").AsString();
        }    

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string FilePath = request.Inputs["Excel File Path"].AsString();
            string SheetName = request.Inputs["Sheet Name"].AsString();
            if (SheetName == "") { SheetName = FieldValue_SheetName; }
            bool HasHeaderRow = request.Inputs["Has Header Row"].AsBoolean();
            bool ForceMixedDataAsText;
            string CSVDelimiter = request.Inputs["CSV Delimiter"].AsString();
            if (CSVDelimiter == "") { CSVDelimiter = FieldValue_CSVDelimiter; }
            string UpdateStatement = request.Inputs["Update statement"].AsString();

            if (request.Inputs["Force mixed data to be converted to text"].ToString() != "")
            {
                ForceMixedDataAsText = request.Inputs["Force mixed data to be converted to text"].AsBoolean();
            }
            else
            {
                ForceMixedDataAsText = false;
            }

            int TypeGuessRows;
            if (request.Inputs["TypeGuessRows"].ToString() != "")
            {
                TypeGuessRows = request.Inputs["TypeGuessRows"].AsInt16();
            }
            else
            {
                TypeGuessRows = 8;
            }

            OleDbConnection con = null;
            ExcelObj = new Excel();
            Excel.ExcelFileType FileType = Excel.GetFileType(FilePath);

            try
            {
                con = ExcelObj.OpenExcelFile(FilePath, SheetName, FileType, HasHeaderRow, ForceMixedDataAsText, TypeGuessRows, CSVDelimiter);
                ExcelObj.UpdateExcelDocument(FilePath, SheetName, FileType, ref con, UpdateStatement);
            }
            catch (System.Exception ex)
            {
                ExcelObj.CloseExcelFile(ref con);   //Ensure the excel file is closed before throwing the error.
                throw (ex);
            }
            ExcelObj.CloseExcelFile(ref con);
                                             
            response.Publish("Excel File Path", FilePath);
            response.Publish("Sheet Name", SheetName);
            response.Publish("Has Header Row", HasHeaderRow);
            response.Publish("Force mixed data to be converted to text", ForceMixedDataAsText);
            response.Publish("TypeGuessRows", TypeGuessRows);
            response.Publish("CSV Delimiter", CSVDelimiter);
            response.Publish("Update statement", UpdateStatement);  
                            
        }

        //Used to create an IEnumerable type used for publishing a collection of values
        private static System.Collections.IEnumerable GetFullLines(DataSet ds)
        {
            DataRow Row;
            String Line;


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Row = ds.Tables[0].Rows[i];
                Line = "";

                foreach (Object Value in Row.ItemArray)
                {
                    Line = Line + Regex.Replace(Value.ToString().Trim(), ";", "_") + ";";  //Replace all ; characters to _ so that the data publishes correctly to the data bus
                }

                Line = Line.Substring(0, Line.Length - 1);  //Remove trailing ; character    
                yield return Line;
            }
        }

        }

     public class Excel
     {
         public enum ExcelFileType { unknown, xls, xlsx, csv }

         public static ExcelFileType GetFileType(string FilePath)
         {
             ExcelFileType FileType = ExcelFileType.unknown;
             if (FilePath != null & FilePath.Length > 3)
             {
                 if (FilePath.Substring(FilePath.Length - 3).ToLower() == "xls") { FileType = ExcelFileType.xls; }
                 if (FilePath.Substring(FilePath.Length - 4).ToLower() == "xlsx") { FileType = ExcelFileType.xlsx; }
                 if (FilePath.Substring(FilePath.Length - 3).ToLower() == "csv") { FileType = ExcelFileType.csv; }
             }

             return FileType;
         }

         //Returns the connection object
         public OleDbConnection OpenExcelFile(string ExcelFilePath, string SheetName, ExcelFileType FileType, bool HasHeaderRow, bool ForceMixedDataAsText, int TypeGuessRows, string CSVDelimiter){
             string ConnectionStringOptions = "";
             DataTable sheet1 = new DataTable();
             OleDbDataAdapter adapter;
             DataSet ds = new DataSet();
             adapter = new OleDbDataAdapter();

             string ParentDirectory = ExcelFilePath.Substring(0,ExcelFilePath.LastIndexOf('\\'));
             string FileName = ExcelFilePath.Split('\\').Last();
             
             if (HasHeaderRow) { ConnectionStringOptions += "HDR=YES;";  }
             else { ConnectionStringOptions += "HDR=NO;"; }
             if (ForceMixedDataAsText) { ConnectionStringOptions += "IMEX=1;"; }
             ConnectionStringOptions += "TypeGuessRows=" + TypeGuessRows + ";";
             ConnectionStringOptions = ConnectionStringOptions.Substring(0, ConnectionStringOptions.Length - 1);    //remove trailing ; character

             OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
             if (FileType == ExcelFileType.xlsx)
             {
                 csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
                 csbuilder.DataSource = ExcelFilePath;
                 csbuilder.Add("Extended Properties", "Excel 12.0 Xml;" + ConnectionStringOptions);
             }
             else if (FileType == ExcelFileType.xls)
             {
                 csbuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                 csbuilder.DataSource = ExcelFilePath;
                 csbuilder.Add("Extended Properties", "Excel 8.0;" + ConnectionStringOptions);
             }
             else if (FileType == ExcelFileType.csv)
             {
                 csbuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                 csbuilder.DataSource = ParentDirectory;
                 csbuilder.Add("Extended Properties", "text;FMT=Delimited(" + CSVDelimiter + ");" + ConnectionStringOptions);
             }
             else{
                 throw new System.ArgumentException ("Invalid file type specified.  File must end with .xls, .xlsx or .csv");
             }


             OleDbConnection con = new OleDbConnection(csbuilder.ConnectionString);
             con.Open();   
             return con;               
         }

         //Closes the specified connection
         public void CloseExcelFile(ref OleDbConnection con){
             if (con != null)
             {
                 con.Close();
                 con.Dispose();
             }
         }
         
         //Read all data from the specified Excel file.
         public System.Data.DataSet ReadSheetFromExcel(string ExcelFilePath, string SheetName, ExcelFileType FileType, ref OleDbConnection con, string SelectStatement, string WhereStatement)
         {             
             OleDbDataAdapter adapter;
             DataSet ds = new DataSet();
             adapter = new OleDbDataAdapter();
             string ParentDirectory = ExcelFilePath.Substring(0, ExcelFilePath.LastIndexOf('\\'));
             string FileName = ExcelFilePath.Split('\\').Last();

             
             if (FileType == ExcelFileType.xlsx)
             {
                 SelectStatement = SelectStatement + " from [" + SheetName + "$]";
             }
             else if (FileType == ExcelFileType.xls)
             {
                 SelectStatement = SelectStatement + " from [" + SheetName + "$]";
             }
             else if (FileType == ExcelFileType.csv)
             {
                 SelectStatement = SelectStatement + " from [" + FileName + "]";
             }
             

             if (WhereStatement != "")
             {
                 SelectStatement += " " + WhereStatement;
             }
             

            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = SelectStatement;
                DataTable dtItems = new DataTable();
                dtItems.TableName = SheetName;                     
                adapter.SelectCommand = cmd;
                adapter.Fill(dtItems);
                ds.Tables.Add(dtItems);
            }
            catch(Exception ex)
            {
            
            if (ds != null) { ds.Dispose(); }
            if (adapter != null) { adapter.Dispose(); }
            throw (ex);
            } 

            return ds;
         }

         //Get the names of the tables within the spreadsheet
         public System.Collections.IEnumerable GetTableNames(OleDbConnection con)
         {
             DataTable Schema;
             Schema = con.GetSchema("Tables");
             foreach (System.Data.DataRow  Row in Schema.Rows){
                 yield return Row.Table.TableName;
             }

         }

         //Insert row into spreadsheet
         public void InsertRowIntoExcel(string ExcelFilePath, string SheetName, ExcelFileType FileType, ref OleDbConnection con,
             string ColName1, string ColVal1, string ColName2, string ColVal2, string ColName3, string ColVal3, string ColName4, string ColVal4,
             string ColName5, string ColVal5, string ColName6, string ColVal6, string ColName7, string ColVal7, string ColName8, string ColVal8,
             string ColName9, string ColVal9, string ColName10, string ColVal10, string ColName11, string ColVal11, string ColName12, string ColVal12,
             string ColName13, string ColVal13, string ColName14, string ColVal14, string ColName15, string ColVal15, string ColName16, string ColVal16,
             string ColName17, string ColVal17, string ColName18, string ColVal18, string ColName19, string ColVal19, string ColName20, string ColVal20)
         {
             string ParentDirectory = ExcelFilePath.Substring(0, ExcelFilePath.LastIndexOf('\\'));
             string FileName = ExcelFilePath.Split('\\').Last();
             string InsertStatement = "";

             if (FileType == ExcelFileType.xlsx)
             {
                 InsertStatement = "Insert INTO [" + SheetName + "$] ([" + ColName1 + "]";
             }
             else if (FileType == ExcelFileType.xls)
             {
                 InsertStatement = "Insert INTO [" + SheetName + "$] ([" + ColName1 + "]";
             }
             else if (FileType == ExcelFileType.csv)
             {
                 InsertStatement = "Insert INTO [" + FileName + "] ([" + ColName1 + "]";
             }

             if (ColName2 != "") { InsertStatement += ",[" + ColName2 + "]"; }
             if (ColName3 != "") { InsertStatement += ",[" + ColName3 + "]"; }
             if (ColName4 != "") { InsertStatement += ",[" + ColName4 + "]"; }
             if (ColName5 != "") { InsertStatement += ",[" + ColName5 + "]"; }
             if (ColName6 != "") { InsertStatement += ",[" + ColName6 + "]"; }
             if (ColName7 != "") { InsertStatement += ",[" + ColName7 + "]"; }
             if (ColName8 != "") { InsertStatement += ",[" + ColName8 + "]"; }
             if (ColName9 != "") { InsertStatement += ",[" + ColName9 + "]"; }
             if (ColName10 != "") { InsertStatement += ",[" + ColName10 + "]"; }
             if (ColName11 != "") { InsertStatement += ",[" + ColName11 + "]"; }
             if (ColName12 != "") { InsertStatement += ",[" + ColName12 + "]"; }
             if (ColName13 != "") { InsertStatement += ",[" + ColName13 + "]"; }
             if (ColName14 != "") { InsertStatement += ",[" + ColName14 + "]"; }
             if (ColName15 != "") { InsertStatement += ",[" + ColName15 + "]"; }
             if (ColName16 != "") { InsertStatement += ",[" + ColName16 + "]"; }
             if (ColName17 != "") { InsertStatement += ",[" + ColName17 + "]"; }
             if (ColName18 != "") { InsertStatement += ",[" + ColName18 + "]"; }
             if (ColName19 != "") { InsertStatement += ",[" + ColName19 + "]"; }
             if (ColName20 != "") { InsertStatement += ",[" + ColName20 + "]"; }

             InsertStatement += ") VALUES('" + ColVal1 + "'";
             if (ColName2 != "") { InsertStatement += ",'" + ColVal2 + "'"; }
             if (ColName3 != "") { InsertStatement += ",'" + ColVal3 + "'"; }
             if (ColName4 != "") { InsertStatement += ",'" + ColVal4 + "'"; }
             if (ColName5 != "") { InsertStatement += ",'" + ColVal5 + "'"; }
             if (ColName6 != "") { InsertStatement += ",'" + ColVal6 + "'"; }
             if (ColName7 != "") { InsertStatement += ",'" + ColVal7 + "'"; }
             if (ColName8 != "") { InsertStatement += ",'" + ColVal8 + "'"; }
             if (ColName9 != "") { InsertStatement += ",'" + ColVal9 + "'"; }
             if (ColName10 != "") { InsertStatement += ",'" + ColVal10 + "'"; }
             if (ColName11 != "") { InsertStatement += ",'" + ColVal11 + "'"; }
             if (ColName12 != "") { InsertStatement += ",'" + ColVal12 + "'"; }
             if (ColName13 != "") { InsertStatement += ",'" + ColVal13 + "'"; }
             if (ColName14 != "") { InsertStatement += ",'" + ColVal13 + "'"; }
             if (ColName15 != "") { InsertStatement += ",'" + ColVal15 + "'"; }
             if (ColName16 != "") { InsertStatement += ",'" + ColVal16 + "'"; }
             if (ColName17 != "") { InsertStatement += ",'" + ColVal17 + "'"; }
             if (ColName18 != "") { InsertStatement += ",'" + ColVal18 + "'"; }
             if (ColName19 != "") { InsertStatement += ",'" + ColVal19 + "'"; }
             if (ColName20 != "") { InsertStatement += ",'" + ColVal20 + "'"; }
             InsertStatement += ")";            
           

             OleDbCommand cmd = new OleDbCommand();
             cmd.Connection = con;
             cmd.CommandText = InsertStatement;
             cmd.ExecuteNonQuery();             
         }

         //Insert row into spreadsheet
         public void UpdateExcelDocument(string ExcelFilePath, string SheetName, ExcelFileType FileType, ref OleDbConnection con, string UpdateString)
         {
             string ParentDirectory = ExcelFilePath.Substring(0, ExcelFilePath.LastIndexOf('\\'));
             string FileName = ExcelFilePath.Split('\\').Last();
             string UpdateCommand = "";

             if (FileType == ExcelFileType.xlsx)
             {
                 UpdateCommand = "Update [" + SheetName + "$] " + UpdateString;
             }
             else if (FileType == ExcelFileType.xls)
             {
                 UpdateCommand = "Update [" + SheetName + "$] " + UpdateString;
             }
             else if (FileType == ExcelFileType.csv)
             {
                 UpdateCommand = "Update [" + FileName + "] " + UpdateString;
             }
             
             OleDbCommand cmd = new OleDbCommand();
             cmd.Connection = con;
             cmd.CommandText = UpdateCommand;
             cmd.ExecuteNonQuery();
         }

     }
}
