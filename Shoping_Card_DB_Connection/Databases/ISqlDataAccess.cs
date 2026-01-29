
namespace Shoping_Card_DB_Connection.Databases
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName, bool isStoredProcedure);
        Task SaveData<T>(string sqlstatement, T parameters, string connectionStringName, bool isStoredProcedure);
    }
}