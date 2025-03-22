namespace Budgetify.Models.Response;

public class CategoryResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}