using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration.Infrastructure
{
    public class ActionServer
    {
        public Guid unqiueID { get; set; }
        public string Computer { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string IsPAS { get; set; }
        public DateTime Heartbeat { get; set; }
        public string LocalSystemAccount { get; set; }
        public string ThisAccount { get; set; }
        public string Account { get; set; }
        public string MonitorCPU { get; set; }
        public string MonitorCPUValue { get; set; }
        public string MonitorMemory { get; set; }
        public string MonitorMemoryValue { get; set; }
        public string Version { get; set; }
        public string Role { get; set; }
        public DateTime DeploymentTime { get; set; }
        public string MaxRunningPolicies { get; set; }
        public string numberOfActivePolicies { get; set; }
        public string numberOfActiveMonitors { get; set; }

        public ActionServer(SqlConnection scoConnection, string databaseName, string serverName)
        {
            SqlDataReader myReader = null;
            SqlCommand newCommand = new SqlCommand(string.Format("select * from [{0}].[dbo].[ACTIONSERVERS] where Computer = @ComputerName", databaseName), scoConnection);
            newCommand.Parameters.AddWithValue("@ComputerName", serverName);
            myReader = newCommand.ExecuteReader();
            //read the list
            while (myReader.Read())
            {
                this.unqiueID = getGuidValue(myReader, "unqiueID");
                this.Computer = getStringValue(myReader, "Computer");
                this.Description = getStringValue(myReader, "Description");
                this.Type = getStringValue(myReader, "Type");
                this.Priority = getStringValue(myReader, "Priority");
                this.IsPAS = getStringValue(myReader, "IsPAS");
                this.Heartbeat = getDateTimeValue(myReader, "Heartbeat");
                this.LocalSystemAccount = getStringValue(myReader, "LocalSystemAccount");
                this.ThisAccount = getStringValue(myReader, "ThisAccount");
                this.Account = getStringValue(myReader, "Account");
                this.MonitorCPU = getStringValue(myReader, "MonitorCPU");
                this.MonitorCPUValue = getStringValue(myReader, "MonitorCPUValue");
                this.MonitorMemory = getStringValue(myReader, "MonitorMemory");
                this.MonitorMemoryValue = getStringValue(myReader, "MonitorMemoryValue");
                this.Version = getStringValue(myReader, "Version");
                this.Role = getStringValue(myReader, "Role");
                this.DeploymentTime = getDateTimeValue(myReader, "DeploymentTime");
                this.MaxRunningPolicies = getStringValue(myReader, "MaxRunningPolicies");
            }
            myReader.Close();

            updateRunningJobCount(scoConnection, databaseName);
            updateRunningMonitorJobCount(scoConnection, databaseName);

        }

        public void updateRunningJobCount(SqlConnection scoConnection, string databaseName)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select COUNT(*) from [{0}].[Microsoft.SystemCenter.Orchestrator.Runtime].Jobs " +
                                                                "where Status = 'Running' and RunbookServerID = @RunbookServerID", databaseName), scoConnection);
            myCommand.Parameters.AddWithValue("@RunbookServerID", this.unqiueID);
            myReader = myCommand.ExecuteReader();
            //read the list
            while (myReader.Read())
            {
                this.numberOfActivePolicies = Convert.ToString(myReader[0]);
            }
            myReader.Close();
        }

        public void updateRunningMonitorJobCount(SqlConnection scoConnection, string databaseName)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select COUNT(*) from [{0}].[Microsoft.SystemCenter.Orchestrator.Runtime].Jobs jb\n" +
                                                                "inner join [Orchestrator].[Microsoft.SystemCenter.Orchestrator].Runbooks rb on rb.Id = jb.RunbookId\n" +
                                                                "where jb.Status = 'Running' and rb.isMonitor = 1 and RunbookServerID = @RunbookServerID", databaseName), scoConnection);
            myCommand.Parameters.AddWithValue("@RunbookServerID", this.unqiueID);
            myReader = myCommand.ExecuteReader();
            //read the list
            while (myReader.Read())
            {
                this.numberOfActiveMonitors = Convert.ToString(myReader[0]);
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
