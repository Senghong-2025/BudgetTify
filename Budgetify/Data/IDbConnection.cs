using System.Data.SqlClient;
using Budgetify.Models;

namespace Budgetify.Data;

public interface IDbConnection
{
    SqlConnection CreateConnection();
    IEnumerable<T> GetData<T>(string sp, object parameters = null);
    T? GetSingleData<T>(string sp, object parameters = null);
    BaseResponse ExecuteNonQuery(string sp, object parameters = null);
}