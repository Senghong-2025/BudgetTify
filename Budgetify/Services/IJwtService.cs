using System.Security.Claims;
using Budgetify.Models.DTOs;
using Budgetify.Models.Response;

namespace Budgetify.Services;

public interface IJwtService
{
    string GenerateToken(LoginResponse response);
    ClaimsPrincipal ValidateToken(string token);
    UserDto GetUserToken();
}