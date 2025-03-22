namespace Budgetify.Models.Response;

public class TranasactionResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}