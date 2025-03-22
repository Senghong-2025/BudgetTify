using System.Data;
using System.Data.SqlClient;
using Budgetify.Enums;
using Budgetify.Models;
using Dapper;

namespace Budgetify.Data
{
    public class DbConnection : IDbConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not set in the configuration.");
            }
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public IEnumerable<T> GetData<T>(string sp, object parameters = null)
        {
            try
            {
                using var connection = this.CreateConnection();
                return connection.Query<T>(sp, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error in {sp}: {ex.Message}");
                return null;
            }
        }
        public BaseResponse ExecuteNonQuery(string sp, object parameters = null)
        {
            var response = new BaseResponse();

            try
            {
                using var connection = this.CreateConnection();
                var result = connection.QuerySingleOrDefault<BaseResponse>(sp, parameters, commandType: CommandType.StoredProcedure);
                if (result != null)
                {
                    var errorCode = result.ErrorCode;
                    var errorMessage = result.ErrorMessage;
                    if (errorCode == 0)
                    {
                        response.ErrorCode = 0;
                        response.ErrorMessage = errorMessage;
                    }
                    else
                    {
                        response.ErrorCode = errorCode;
                        response.ErrorMessage = errorMessage;
                    }
                }
                else
                {
                    response.ErrorCode = (int)EnumErrorCode.DatabaseConnectionFailed;
                    response.ErrorMessage = "Unknown database error";
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = (int)EnumErrorCode.DatabaseConnectionFailed;
                response.ErrorMessage = $"Database error: {ex.Message}";
            }

            return response;
        }

        public T? GetSingleData<T>(string sp, object parameters = null)
        {
            try
            {
                using var connection = this.CreateConnection();
                return connection.QueryFirstOrDefault<T>(sp, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error in {sp}: {ex.Message}");
                return default; 
            }
        }
    }
}