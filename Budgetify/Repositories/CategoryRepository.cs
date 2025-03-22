using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
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
        var sp = "[dbo].[CreateCategory_1.0]";
        return _dbConnection.ExecuteNonQuery(sp, new { UserId = request.UserId, Name = request.Name, Type = request.Type });
    }

    public BaseResponse UpdateCategory(CategoryRequest request)
    {
        var sp = "[dbo].[UpdateCategory_1.0]";
        return _dbConnection.ExecuteNonQuery(sp, new
        {
           Id =  request.CategoryId,
           request.Name,
           request.Type,
        });
    }

    public ApiResponse<List<CategoryResponse>> GetAllCategories(int userId)
    {
        var sp = "[dbo].[GetAllCategories_1.0]";
        var result =  _dbConnection.GetData<CategoryResponse>(sp, new { userId });
        return new ApiResponse<List<CategoryResponse>>(result.ToList());
    }
}