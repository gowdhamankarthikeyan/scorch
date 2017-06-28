using System.Collections.Generic;
using System.Data.SqlClient;


namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration.Infrastructure
{
    public class Infrastructure
    {
        public static List<RunbookDesigner> GetRunbookDesigners(SqlConnection scoConnection, string databaseName)
        {
            List<RunbookDesigner> designers = new List<RunbookDesigner>();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select ClientMachine from [{0}].[dbo].[CLIENTCONNECTIONS]", databaseName), scoConnection);
            myReader = myCommand.ExecuteReader();
            //read the list
            List<string> designerNames = new List<string>(0);
            while (myReader.Read())
            {
                designerNames.Add(myReader[0].ToString());
            }
            
            myReader.Close();

            foreach (string designerName in designerNames)
            {
                designers.Add(new RunbookDesigner(scoConnection, databaseName, designerName));
            }

            return designers;
        }

        public static List<ActionServer> GetRunbookServers(SqlConnection scoConnection, string databaseName)
        {
            List<ActionServer> servers = new List<ActionServer>();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select Computer from [{0}].[dbo].[ACTIONSERVERS]", databaseName), scoConnection);
            myReader = myCommand.ExecuteReader();
            //read the list
            List<string> serverNames = new List<string>();
            while (myReader.Read())
            {
                serverNames.Add(myReader[0].ToString());
            }
            myReader.Close();

            foreach (string servername in serverNames)
            {
                servers.Add(new ActionServer(scoConnection, databaseName, servername));
            }

            return servers;
        }

    }

}
