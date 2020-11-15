using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MVCDapperCRUD.Models
{
    public class DapperORM
    {
        private static string connectionString = @"Data Source=desktop-ig6tgp2\sqlexpress;Initial Catalog = DapperDB; Integrated Security = True";

        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            };

        }

        public static T ExcecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using( SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType:
                    CommandType.StoredProcedure), typeof(T)); 
            }
        }

        public static IEnumerable<T> ExecuteReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    } 
}
