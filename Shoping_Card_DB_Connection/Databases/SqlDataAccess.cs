using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Shoping_Card_DB_Connection.Databases
{
    public class SqlDataAccess :    ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sqlStatement,
                                     U parameters,
                                     string connectionStringName,
                                     bool isStoredProcedure)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
                commandType = CommandType.StoredProcedure;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return (List<T>)await connection.QueryAsync<T>(sqlStatement, parameters, commandType: commandType);
            }
        }

        public async Task SaveData<T>(string sqlstatement,
                                T parameters,
                                string connectionStringName,
                                bool isStoredProcedure)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sqlstatement, parameters, commandType: commandType);
            }
        }
    }
}
