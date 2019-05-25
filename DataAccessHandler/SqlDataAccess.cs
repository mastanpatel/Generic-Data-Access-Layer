using System.Data;
using System.Data.SqlClient;

namespace DataAccessHandler
{
    public class SqlDataAccess : IDatabaseHandler
    {
        private string ConnectionString { get; set; } 

        public SqlDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public void CloseConnection(IDbConnection connection)
        {
            var sqlConnection = (SqlConnection)connection;
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand
            {
                CommandText = commandText,
                Connection = (SqlConnection)connection,
                CommandType = commandType
            };
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            SqlCommand SQLcommand = (SqlCommand)command;
            return SQLcommand.CreateParameter();
        }
    }
}
