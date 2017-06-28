using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQL.Utility
{
    class ResourceStrings
    {
        public static string sqlQuery = @"SQL Query";
        public static string Timeout = @"Timeout";
        public static string AlternateDataSource = @"Alternate Data Source";
        public static string ParameterName = @" Parameter Name";
        public static string ParameterValue = @" Parameter Value";
        public static string DatabaseServer = @"Database Server ";
        public static string Query = @"Query";
        public static string NumberOfRows = @"NumberOfRows";
        public static string ConnectionString = @"Connection String";
         
        public static string[] trueFalse = new string[2] { "True", "False" };
        public static string[] trueFalseDoNotModify = new string[3] { "True", "False", "Do Not Modify" };
        public static string[] connectionStrings = new string[] { "Data Source=SQLSERVER;Initial Catalog=SQLDATABASE;Integrated Security=true;", 
                                                                  "Data Source=SQLSERVER;Initial Catalog=SQLDATABASE;Integrated Security=false;User ID=USERID;Password=PASSWORD"};

    }
}

