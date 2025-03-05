namespace ExpenseTracker.Domain.Entities;

public class Budget
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Total { get; set; }
    
    public int Month { get; set; }
    public int Year { get; set; }
    
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; }
}