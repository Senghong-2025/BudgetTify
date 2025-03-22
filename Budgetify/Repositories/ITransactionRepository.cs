using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Repositories;

public interface ITransactionRepository
{
    BaseResponse CreateTransaction(TransactionRequest request);
    ApiResponse<List<TranasactionResponse>> GetTransactions(int userId);
    BaseResponse UpdateTransaction(TransactionRequest request);
}