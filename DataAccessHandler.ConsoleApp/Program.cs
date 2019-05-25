using System;
using System.Data;
using System.Collections.Generic;

namespace DataAccessHandler.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================ Using Database Factory =================\n\n\n");
            UsingDatabaseFactory();
            Console.ReadKey();
        }

        private static void UsingDatabaseFactory()
        {
            var dbManager = new DBManager("DBConnection");

            var user = new User
            {
                FirstName = "First",
                LastName = "Last",
                Dob = DateTime.Now.AddDays(-3000),
                IsActive = true
            };

            var parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@FirstName", 50, user.FirstName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@LastName", user.LastName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Dob", user.Dob, DbType.DateTime));
            parameters.Add(dbManager.CreateParameter("@IsActive", 50, user.IsActive, DbType.Boolean));

            //INSERT
            int lastId = 0;
            dbManager.Insert("DAH_User_Insert", CommandType.StoredProcedure, parameters.ToArray(), out lastId);
            Console.WriteLine("\nINSERTED ID: " + lastId);

            //DATATABLE
            var dataTable = dbManager.GetDataTable("DAH_User_GetAll", CommandType.StoredProcedure);
            Console.WriteLine("\nTOTAL ROWS IN TABLE: " + dataTable.Rows.Count);

            //DATAREADER
            IDbConnection connection = null;
            var dataReader = dbManager.GetDataReader("DAH_User_GetAll", CommandType.StoredProcedure, null, out connection);
            try
            {
                user = new User();
                while (dataReader.Read())
                {
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.LastName = dataReader["LastName"].ToString();
                }

                Console.WriteLine(string.Format("\nDATA READER VALUES FirstName: {0} LastName: {1}", user.FirstName, user.LastName));
            }
            catch (Exception)
            {
                
            }
            finally
            {
                dataReader.Close();
                dbManager.CloseConnection(connection);
            }

            //SCALAR
            object scalar = dbManager.GetScalarValue("DAH_User_Scalar", CommandType.StoredProcedure);
            Console.WriteLine("\nSCALAR VALUE: " + scalar.ToString());

            //UPDATE
            user = new User
            {
                Id = lastId,
                FirstName = "First1",
                LastName = "Last1",
                Dob = DateTime.Now.AddDays(-5000)
            };

            parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", user.Id, DbType.Int32));
            parameters.Add(dbManager.CreateParameter("@FirstName", 50, user.FirstName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@LastName", user.LastName, DbType.String));
            parameters.Add(dbManager.CreateParameter("@Dob", user.Dob, DbType.DateTime));
            dbManager.Update("DAH_User_Update", CommandType.StoredProcedure, parameters.ToArray());

            //DATATABLE
            dataTable = dbManager.GetDataTable("DAH_User_GetAll", CommandType.StoredProcedure);
            Console.WriteLine(string.Format("\nUPADTED VALUES FirstName: {0} LastName: {1}", dataTable.Rows[0]["FirstName"].ToString(), dataTable.Rows[0]["LastName"].ToString()));

            //DELETE
            parameters = new List<IDbDataParameter>();
            parameters.Add(dbManager.CreateParameter("@Id", user.Id, DbType.Int32));
            dbManager.Delete("DAH_User_Delete", CommandType.StoredProcedure, parameters.ToArray());
            Console.WriteLine("\nDELETED RECORD FOR ID: " + user.Id);

            //DATATABLE
            dataTable = dbManager.GetDataTable("DAH_User_GetAll", CommandType.StoredProcedure);
            Console.WriteLine("\nTOTAL ROWS IN TABLE: " + dataTable.Rows.Count);
        }
    }
}
