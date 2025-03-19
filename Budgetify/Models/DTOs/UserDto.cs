namespace Budgetify.Models.DTOs;

public class UserDto
{
    public int UserId { get; set; } = 0;
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    }