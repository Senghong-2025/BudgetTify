using Budgetify.Models;
using Budgetify.Models.DTOs;
using Budgetify.Models.Response;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = Budgetify.Models.Request.LoginRequest;
using RegisterRequest = Budgetify.Models.Request.RegisterRequest;

namespace Budgetify.Repositories;

public interface IUserRepository
{
    UserDto UserRegister(RegisterRequest request);
    ApiResponse<List<GetAllUser>> GetAllUsers();
    string GetUserByUsername(string username);
    string GetUserByEmail(string email);
    ApiResponse<LoginResponse> Login (LoginRequest request);
}