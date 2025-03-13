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
    public UserDto UserRegister(RegisterRequest request)
    {
        var connection = _db.CreateConnection();
        var sp = "[dbo].[Register_1.0]";
        
        var result = connection.QuerySingle<UserDto>(sp, new
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        }, commandType: CommandType.StoredProcedure);
        return result;
    }

    public BaseApiResponse<List<GetAllUser>> GetAllUsers()
    {
       var connection = _db.CreateConnection();
         var sp = "[dbo].[GetAllUsers_1.0]";
            var result = connection.Query<GetAllUser>(sp, commandType: CommandType.StoredProcedure);
            return new BaseApiResponse<List<GetAllUser>>(
                errorCode:0 , 
                errorMessage:"Success",
                result.ToList()
            );
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

    public LoginResponse Login(LoginRequest request)
    {
        var connection = _db.CreateConnection();
        var sp = "[dbo].[Login_1.0]";
        var result = connection.QuerySingle<LoginResponse>(sp, new
        {
            request.UsernameOrEmail,
            PasswordHash = request.Password,
            request.IpAddress
        }, commandType: CommandType.StoredProcedure);
        return result;
    }
}