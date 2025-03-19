using Budgetify.Filters;
using Budgetify.Models;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Budgetify.Services;
using Microsoft.AspNetCore.Mvc;

namespace Budgetify.Controllers

{
    [ApiController]
    [Route("api/category")]
    [ServiceFilter(typeof(JwtFilter))]
    public class CategoryController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public BaseResponse CreateCategory([FromBody] CategoryRequest request)
        {
            return _categoryService.CreateCategory(request);
        }

        [HttpPost("update")]
        public BaseResponse UpdateCategory([FromBody] CategoryRequest request)
        {
            return _categoryService.UpdateCategory(request);
        }

        [HttpGet("get")]
        public ApiResponse<List<CategoryResponse>> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }
    
    }
}