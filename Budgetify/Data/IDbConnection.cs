using System.Data.SqlClient;

namespace Budgetify.Data;

public interface IDbConnection
{
    SqlConnection CreateConnection();
}