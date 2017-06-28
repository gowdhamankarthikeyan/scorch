using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Data.SqlClient;
using SQL.Utility;

namespace SQL
{
    [Activity("Run SQL Query")]
    public class RunQuery : IActivity
    {
        private ConnectionCredentials credentials;
        private int numberOfResults = 0;
        private int maxParameterNumber = 50;
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            maxParameterNumber = credentials.MaxParameterCount;
            
            designer.AddInput(ResourceStrings.sqlQuery);
            designer.AddInput(ResourceStrings.Timeout).NotRequired();
            for (int i = 0; i < maxParameterNumber; i++)
            {
                designer.AddInput(i.ToString() + " Parameter Name").WithDefaultValue("@Param" + i.ToString()).NotRequired();
                designer.AddInput(i.ToString() + " Parameter Value").WithDefaultValue("Value").NotRequired();
            }

            designer.AddOutput(ResourceStrings.DatabaseServer).AsString().WithDescription("Database Server Name");
            designer.AddOutput(ResourceStrings.Query).AsString().WithDescription("Query Run");
            designer.AddOutput(ResourceStrings.NumberOfRows).AsString().WithDescription("Number of Rows Returned");
            designer.AddCorellatedData(typeof(QueryResult));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String databaseServer = credentials.DatabaseServer;
            String initialCatalog = credentials.InitialCatalog;

            String commandText = request.Inputs[ResourceStrings.sqlQuery].AsString();
            Dictionary<string, string> ParameterList = new Dictionary<string, string>();

            int timeout = -1;

            if (request.Inputs.Contains(ResourceStrings.Timeout)) { timeout = request.Inputs[ResourceStrings.Timeout].AsInt32(); }

            for (int i = 0; i < maxParameterNumber; i++)
            {
                if (request.Inputs.Contains(i.ToString() + " Parameter Name") && request.Inputs.Contains(i.ToString() + " Parameter Value"))
                {
                    string paramName = request.Inputs[i.ToString() + " Parameter Name"].AsString();
                    string paramValue = request.Inputs[i.ToString() + " Parameter Value"].AsString();

                    ParameterList.Add(paramName, paramValue);
                }
            }

            SqlConnectionStringBuilder conString = new SqlConnectionStringBuilder();
            conString.IntegratedSecurity = true;
            conString.DataSource = databaseServer;
            if (!initialCatalog.Equals(string.Empty)) { conString.InitialCatalog = initialCatalog; }
                       
            String connectionString = conString.ToString();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = commandText;

                if(timeout > 0)
                {
                    command.CommandTimeout = timeout;
                }

                foreach (String paramName in ParameterList.Keys)
                {
                    command.Parameters.AddWithValue(paramName, ParameterList[paramName]);
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                response.WithFiltering().PublishRange(parseResults(reader));
            }
            catch { throw; }
            finally { connection.Close(); }


            response.Publish(ResourceStrings.DatabaseServer, databaseServer);
            response.Publish(ResourceStrings.Query, commandText);
            response.Publish(ResourceStrings.NumberOfRows, numberOfResults);

        }
        
        private IEnumerable<QueryResult> parseResults(SqlDataReader reader)
        {
            while (reader.Read())
            {
                QueryResult result = new QueryResult();
                String resultString = String.Empty;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.setField(i, reader[i].ToString());
                    resultString += reader[i].ToString() + ';';
                }
                resultString = resultString.TrimEnd(';');
                numberOfResults++;

                result.QueryResultString = resultString;

                yield return result;
            }
        }
    }
}