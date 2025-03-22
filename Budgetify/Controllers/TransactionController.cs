using Budgetify.Filters;
using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Budgetify.Services;
using Microsoft.AspNetCore.Mvc;

namespace Budgetify.Controllers

{
    [ApiController]
    [Route("api/transaction")]
    [ServiceFilter(typeof(JwtFilter))]
    public class TransactionController
    {
    
        private readonly ITransactionService _transactionService;
        private readonly IJwtService _jwtService;
        
        public TransactionController(ITransactionService transactionService, IJwtService jwtService)
        {
            _transactionService = transactionService;
            _jwtService = jwtService;
        }
        
        [HttpPost("create")]
        public BaseResponse CreateTransaction(TransactionRequest request)
        {
            var user = _jwtService.GetUserToken();
            request.UserId = user.UserId;
            return _transactionService.CreateTransaction(request);
        }
        [HttpGet("get")]
        public ApiResponse<List<TranasactionResponse>> GetTransactions()
        {
            var user = _jwtService.GetUserToken();
            return _transactionService.GetTransactions(user.UserId);
        }
        [HttpPost("update")]
        public BaseResponse UpdateTransaction(TransactionRequest request)
        {
            var user = _jwtService.GetUserToken();
            request.UserId = user.UserId;
            return _transactionService.UpdateTransaction(request);
        }
    }
}