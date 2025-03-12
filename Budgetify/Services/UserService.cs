using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Budgetify.Repositories;

namespace Budgetify.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public string UserRegister(RegisterRequest request)
    {
        var users = _userRepository.GetAllUsers();
        var isUserExist = users.Data.Exists(x => x.Username == request.Username);
        var isEmailExist = users.Data.Exists(x => x.Email == request.Email);
        if (isUserExist || isEmailExist)
            return "Username or Email already exists!";
        return _userRepository.UserRegister(request);
    }

    public BaseApiResponse<List<GetAllUser>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }
}