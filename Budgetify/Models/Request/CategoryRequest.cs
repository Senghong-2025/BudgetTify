using Budgetify.Models.DTOs;
using Budgetify.Services;

namespace Budgetify.Models.Request;

public class CategoryRequest
{
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}
