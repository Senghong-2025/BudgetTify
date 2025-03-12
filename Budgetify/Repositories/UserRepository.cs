using System.Data;
using Budgetify.Models;
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
    public string? UserRegister(RegisterRequest request)
    {
        var connection = _db.CreateConnection();
        var sp = "Register_1_0";
        
        var result = connection.Query(sp, new
        {
            Username = request.Username,
            PasswordHash = request.Password,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        }, commandType: CommandType.StoredProcedure);
        return "User registered successfully";
    }

    public BaseApiResponse<List<GetAllUser>> GetAllUsers()
    {
       var connection = _db.CreateConnection();
         var sp = "GetAllUsers_1_0";
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
}