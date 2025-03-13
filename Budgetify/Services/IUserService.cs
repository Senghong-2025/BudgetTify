using Budgetify.Models;
using Budgetify.Models.DTOs;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Services;

public interface IUserService
{
    BaseResponse UserRegister(RegisterRequest request);
    ApiResponse<List<GetAllUser>> GetAllUsers();
    ApiResponse<LoginResponse> Login (LoginRequest request);
}