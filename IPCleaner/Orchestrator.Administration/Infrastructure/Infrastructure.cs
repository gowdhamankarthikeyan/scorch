using System.Collections.Generic;
using System.Data.SqlClient;


namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class Infrastructure
    {
        public static List<string> GetRunbookDesigners(SqlConnection scoConnection)
        {
            List<string> designers = new List<string>();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select ClientMachine from [Orchestrator].[dbo].[CLIENTCONNECTIONS]", scoConnection);
            myReader = myCommand.ExecuteReader();
            //read the list
            while (myReader.Read())
            {
                designers.Add(myReader[0].ToString());
            }
            myReader.Close();

            return designers;
        }

        public static List<string> GetRunbookServers(SqlConnection scoConnection)
        {
            List<string> servers = new List<string>();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select Computer from [Orchestrator].[dbo].[ACTIONSERVERS]", scoConnection);
            myReader = myCommand.ExecuteReader();
            //read the list
            while (myReader.Read())
            {
                servers.Add(myReader[0].ToString());
            }
            myReader.Close();

            return servers;
        }

    }

}
