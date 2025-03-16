namespace ExpenseTracker.Domain.Entities;

public class Expense
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
}