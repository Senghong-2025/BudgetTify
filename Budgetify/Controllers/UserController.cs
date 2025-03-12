using Budgetify.Data;
using Budgetify.Models.Request;
using Budgetify.Services;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Budgetify.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var result = _userService.UserRegister(request);
            return Ok(result);
        }
        [HttpGet("all-users")]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            return Ok(result);
        }
    }
}