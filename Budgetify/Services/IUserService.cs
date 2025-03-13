using Budgetify.Models;
using Budgetify.Models.DTOs;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Services;

public interface IUserService
{
    BaseApiResponse<UserDto> UserRegister(RegisterRequest request);
    BaseApiResponse<List<GetAllUser>> GetAllUsers();
}