using Budgetify.Models.DTOs;

namespace Budgetify.Models.Response;

public class LoginResponse: UserDto
{
    public string Token { get; set; }
}