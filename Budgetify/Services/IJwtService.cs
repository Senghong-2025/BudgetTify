using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Services;

public interface IJwtService
{
    string GenerateToken(LoginResponse response);
}