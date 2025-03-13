using Budgetify.Models;
using Budgetify.Models.DTOs;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Repositories;

public interface IUserRepository
{
    UserDto UserRegister(RegisterRequest request);
    BaseApiResponse<List<GetAllUser>> GetAllUsers();
    string GetUserByUsername(string username);
    string GetUserByEmail(string email);
}