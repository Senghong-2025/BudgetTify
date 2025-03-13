namespace Budgetify.Models.Request;

public class LoginRequest
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
    public string IpAddress { get; set; }
}