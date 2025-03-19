using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;

namespace Budgetify.Services;

public interface ICategoryService
{
    BaseResponse CreateCategory(CategoryRequest request);
    BaseResponse UpdateCategory(CategoryRequest request);
    ApiResponse<List<CategoryResponse>> GetAllCategories();
}