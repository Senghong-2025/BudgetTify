using Budgetify.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Budgetify.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private readonly IDbConnection _db;
        public UserController(IDbConnection db)
        {
            _db = db;
        }
        [HttpGet("get-user")]
        public IActionResult GetUser()
        {
            var connection = _db.CreateConnection();
            var sp = "SELECT * FROM Users";
            var result = connection.Query(sp);
            return new OkObjectResult(result);
        }
    }
}