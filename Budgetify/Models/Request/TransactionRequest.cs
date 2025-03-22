namespace Budgetify.Models.Request;

public class TransactionRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = "pending";
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public int CreatedById { get; set; }
}