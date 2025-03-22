using Budgetify.Enums;
using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Budgetify.Repositories;

namespace Budgetify.Services;

public class TransactionService: ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    public BaseResponse CreateTransaction(TransactionRequest request)
    {
        if (request.UserId <= 0 || request.CategoryId <= 0 || request.Amount <= 0)
        {
            return new BaseResponse
            {
                ErrorCode = (int)EnumErrorCode.InvalidRequest,
                ErrorMessage = "Invalid input data"
            };
        }
        return _transactionRepository.CreateTransaction(request);
    }

    public ApiResponse<List<TranasactionResponse>> GetTransactions(int userId)
    {
        return _transactionRepository.GetTransactions(userId);
    }

    public BaseResponse UpdateTransaction(TransactionRequest request)
    {
        return _transactionRepository.UpdateTransaction(request);
    }
}