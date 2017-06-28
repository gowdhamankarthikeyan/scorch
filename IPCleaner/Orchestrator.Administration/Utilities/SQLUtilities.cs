using System.Data.SqlClient;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class SQLUtilities
    {
        public static int RunDeleteQuery(string queryString, SqlConnection scoConnection)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(queryString.ToString(), scoConnection);
            myReader = myCommand.ExecuteReader();

            int numAffected = myReader.RecordsAffected;
            myReader.Close();
            return numAffected;
        }

        public static int RunUpdateQuery(string queryString, SqlConnection scoConnection)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(queryString.ToString(), scoConnection);
            myReader = myCommand.ExecuteReader();

            int numAffected = myReader.RecordsAffected;
            myReader.Close();
            return numAffected;
        }
        
        
        private string _connectionString = string.Empty;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(_connectionString);
                myConnection.Open();
                return myConnection;
            }
            catch
            {
                throw;
            }

        }


    }
}
