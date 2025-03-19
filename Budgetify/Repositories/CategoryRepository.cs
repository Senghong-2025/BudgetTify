using System.Data;
using Budgetify.Data;
using Budgetify.Enums;
using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Dapper;
using IDbConnection = Budgetify.Data.IDbConnection;

namespace Budgetify.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly IDbConnection _dbConnection;

    public CategoryRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public BaseResponse CreateCategory(CategoryRequest request)
    {
        var connection = _dbConnection.CreateConnection();
        var sp = "[dbo].[CreateCategory_1.0]";
        var result = connection.QueryFirstOrDefault<BaseResponse>(sp, request, commandType: CommandType.StoredProcedure);
        return result ?? new BaseResponse((int)EnumErrorCode.UnknownError, "CreateCategory Failed");
    }

    public BaseResponse UpdateCategory(CategoryRequest request)
    {
        var connection = _dbConnection.CreateConnection();
        var sp = "[dbo].[UpdateCategory_1.0]";
        var result = connection.QueryFirstOrDefault<BaseResponse>(sp, request, commandType: CommandType.StoredProcedure);
        return result ?? new BaseResponse((int)EnumErrorCode.UnknownError, "Update Failed");
    }

    public ApiResponse<List<CategoryResponse>> GetAllCategories(int userId)
    {
        var connection = _dbConnection.CreateConnection();
        var sp = "[dbo].[GetAllCategories_1.0]";
        var result = connection.Query<CategoryResponse>(
            sp, 
            new { userId }, 
            commandType: CommandType.StoredProcedure
        );
        return new ApiResponse<List<CategoryResponse>>((int)EnumErrorCode.UnknownError, "success", result.ToList());
    }
}