using Budgetify.Models.DTOs;

namespace Budgetify.Models.Response;

public class GetAllUser: UserDto
{
    string CreatedAt { get; set; }
    string UpdatedAt { get; set; }
}