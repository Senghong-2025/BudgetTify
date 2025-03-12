using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Services;

public interface IUserService
{
    string? UserRegister(RegisterRequest request);
    BaseApiResponse<List<GetAllUser>> GetAllUsers();
}