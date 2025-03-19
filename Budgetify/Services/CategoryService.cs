using Budgetify.Enums;
using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Budgetify.Repositories;

namespace Budgetify.Services;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IJwtService _jwtService;

    public CategoryService(ICategoryRepository categoryRepository, IJwtService jwtService)
    {
        _categoryRepository = categoryRepository;
        _jwtService = jwtService;
    }
    public BaseResponse CreateCategory(CategoryRequest request)
    {
        if (request.Type is not ("expense" or "income")) 
        {
            return new BaseResponse((int)EnumErrorCode.ValidationFailed, "Invalid category type");
        }
        var user = _jwtService.GetUserToken();
        request.UserId = user.UserId;
        return _categoryRepository.CreateCategory(request);
    }

    public BaseResponse UpdateCategory(CategoryRequest request)
    {
        return request.Type is not ("expense" or "income") 
            ? new BaseResponse((int)EnumErrorCode.ValidationFailed, "Invalid category type") 
            : _categoryRepository.UpdateCategory(request);
    }

    public ApiResponse<List<CategoryResponse>> GetAllCategories()
    {
        var user = _jwtService.GetUserToken();
        return _categoryRepository.GetAllCategories(user.UserId);
    }
}