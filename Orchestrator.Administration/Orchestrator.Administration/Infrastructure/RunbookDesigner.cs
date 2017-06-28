using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration.Infrastructure
{
    public class RunbookDesigner
    {
        public Guid unqiueID { get; set; }
        public string ManagementServer { get; set; }
        public string ClientMachine { get; set; }
        public string ClientUser { get; set; }
        public string ClientVersion { get; set; }
        public DateTime ConnectionTime { get; set; }
        public DateTime LastActivity { get; set; }

        public RunbookDesigner(SqlConnection scoConnection, string databaseName, string clientMachine)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select * from [{0}].[dbo].[CLIENTCONNECTIONS] where ClientMachine = @ClientMachine", databaseName), scoConnection);
            myCommand.Parameters.AddWithValue("@ClientMachine", clientMachine);
            myReader = myCommand.ExecuteReader();
            //read the list
            while (myReader.Read())
            {
                this.unqiueID = getGuidValue(myReader, "unqiueID");
                this.ManagementServer = getStringValue(myReader, "ManagementServer");
                this.ClientMachine = getStringValue(myReader, "ClientMachine");
                this.ClientUser = getStringValue(myReader, "ClientUser");
                this.ClientVersion = getStringValue(myReader, "ClientVersion");
                this.ConnectionTime = getDateTimeValue(myReader, "ConnectionTime");
                this.LastActivity = getDateTimeValue(myReader, "LastActivity");
            }
            myReader.Close();


        }

        

        private static DateTime getDateTimeValue(SqlDataReader reader, string fieldName)
        {
            DateTime value = DateTime.MinValue;

            try
            {
                value = Convert.ToDateTime(reader[fieldName]);
            }
            catch { }

            return value;
        }

        private static string getStringValue(SqlDataReader reader, string fieldName)
        {
            string value = string.Empty;

            try
            {
                value = reader[fieldName].ToString();
            }
            catch { }

            return value;
        }
        private static Guid getGuidValue(SqlDataReader reader, string fieldName)
        {
            Guid value = new Guid();

            try
            {
                value = (Guid)reader[fieldName];
            }
            catch { }

            return value;
        }
    }
}
