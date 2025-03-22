using System.Data;
using Budgetify.Models;
using Budgetify.Models.DTOs;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Dapper;
using IDbConnection = Budgetify.Data.IDbConnection;

namespace Budgetify.Repositories;

public class UserRepository: IUserRepository
{
    private readonly IDbConnection _db;
    public UserRepository(IDbConnection db)
    {
        _db = db;
    }
    public ApiResponse<UserDto> UserRegister(RegisterRequest request)
    {
        var sp = "[dbo].[Register_1.0]";
        var result = _db.GetSingleData<UserDto>(sp,
            new
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName

            }
        );
        return new ApiResponse<UserDto>(result);
    }

    public ApiResponse<List<GetAllUser>> GetAllUsers()
    {
         var sp = "[dbo].[GetAllUsers_1.0]";
         var result = _db.GetData<GetAllUser>(sp);
         return new ApiResponse<List<GetAllUser>>(result.ToList());
           
    }

    public string GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public string GetUserByEmail(string email)
    {
       var connection = _db.CreateConnection();
       return "User found";
    }

    public ApiResponse<LoginResponse> Login(LoginRequest request)
    {
        var connection = _db.CreateConnection();
        var sp = "[dbo].[Login_1.0]";
        var result = connection.QuerySingle<LoginResponse>(sp, new
        {
            request.UsernameOrEmail,
            PasswordHash = request.Password,
            request.IpAddress
        }, commandType: CommandType.StoredProcedure);
        return new ApiResponse<LoginResponse>(result);
    }
}