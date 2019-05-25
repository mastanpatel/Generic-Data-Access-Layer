using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessHandler
{
    public class BulkInsertManager
    {
        public static void SqlBulkCopy(SqlConnection connection, DataTable table, string destinationTable)
        {
            connection.Open();

            try
            {
                var sqlBulkCopy = new SqlBulkCopy(connection);
                sqlBulkCopy.DestinationTableName = destinationTable;

                foreach (DataColumn column in table.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void SqlBulkCopy<T>(SqlConnection connection, IEnumerable<T> objects, string destinationTable)
        {
            try
            {
                var sqlBulkCopy = new SqlBulkCopy(connection);
                sqlBulkCopy.DestinationTableName = destinationTable;

                var table = ConvertToDataTable<T>(objects);

                foreach (DataColumn column in table.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        private static DataTable ConvertToDataTable<T>(IEnumerable<T> list)
        {
            var dataTable = new DataTable();
            return dataTable;
        }
    }
}
