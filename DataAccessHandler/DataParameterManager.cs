using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;

namespace DataAccessHandler
{
    public class DataParameterManager
    {
        public static IDbDataParameter CreateParameter(string providerName, string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            IDbDataParameter parameter = null;
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    return CreateSqlParameter(name, value, dbType, direction);
                case "system.data.oracleclient":
                    return CreateOracleParameter(name, value, dbType, direction);
                case "system.data.oleDb":
                    break;
                case "system.data.odbc":
                    break;
            }

            return parameter;
        }

        public static IDbDataParameter CreateParameter(string providerName, string name, int size, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            IDbDataParameter parameter = null;
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    return CreateSqlParameter(name, size, value, dbType, direction);
                case "system.data.oracleclient":
                    return CreateOracleParameter(name, size, value, dbType, direction);
                case "system.data.oleDb":
                    break;
                case "system.data.odbc":
                    break;
            }

            return parameter;
        }

        private static IDbDataParameter CreateSqlParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }
       
        private static IDbDataParameter CreateSqlParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        private static IDbDataParameter CreateOracleParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            return new OracleParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        private static IDbDataParameter CreateOracleParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new OracleParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }
    }
}
