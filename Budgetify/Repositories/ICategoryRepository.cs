using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Repositories;

public interface ICategoryRepository
{
    BaseResponse CreateCategory(CategoryRequest request);
    BaseResponse UpdateCategory(CategoryRequest request);
    ApiResponse<List<CategoryResponse>> GetAllCategories(int userId);
}