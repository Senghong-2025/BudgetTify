using Budgetify.Data;
using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Repositories;

public class TransactionRepository: ITransactionRepository
{
    private readonly IDbConnection _dbConnection;

    public TransactionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public BaseResponse CreateTransaction(TransactionRequest request)
    {
        var result = _dbConnection.ExecuteNonQuery("[dbo].[CreateTransaction_1.0]", new
        {
            UserId = request.UserId,
            CategoryId = request.CategoryId,
            Amount = request.Amount,
            TransactionDate = request.TransactionDate,
            Description = request.Description,
            Status = request.Status
        });
        return result;
    }

    public ApiResponse<List<TranasactionResponse>> GetTransactions(int userId)
    {
        var result = _dbConnection.GetData<TranasactionResponse>("[dbo].[GetTransaction_1.0]", new { UserId = userId });
        return new ApiResponse<List<TranasactionResponse>>(result.ToList());
    }

    public BaseResponse UpdateTransaction(TransactionRequest request)
    {
        var result = _dbConnection.ExecuteNonQuery("[dbo].[UpdateTransaction_1.0]", new
        {
            TransactionId = request.Id,
            UserId = request.UserId,
            CategoryId = request.CategoryId,
            Amount = request.Amount,
            TransactionDate = request.TransactionDate,
            Description = request.Description,
            Status = request.Status
        });
        return result;
    }
}