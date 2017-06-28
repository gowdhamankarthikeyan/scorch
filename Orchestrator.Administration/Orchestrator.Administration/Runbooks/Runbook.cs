using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration.Runbooks
{
    public class Runbook
    {
        Guid _id;
        public Runbook(Guid id)
        {
            _id = id;
        }

        public List<Guid> GetRunJobIDs(int numberOfMinutes, string databaseServer, string initialCatalog)
        {
            List<Guid> jobArray = new List<Guid>();

            String commandText = "select pv.Name, COUNT(pi.UniqueID)\n" +
                                 "from POLICIES_VIEW pv\n" +
                                 "inner join POLICYINSTANCES pi on pv.UniqueID = pi.PolicyID\n" +
                                 "where pi.TimeEnded >= DATEADD(\"MI\", @MinuteOffset, GETUTCDATE())\n" +
                                 "group by pv.Name";

            string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI", databaseServer, initialCatalog);
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = commandText;

                command.Parameters.AddWithValue("@MinuteOffset", Math.Abs(numberOfMinutes) * -1);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    jobArray.Add(new Guid(reader[0].ToString()));
                }
            }
            catch { throw; }
            finally { connection.Close(); }

            return jobArray;
        }
    }
}
