namespace ExpenseTracker.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string AzureId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    public virtual Budget? Budget { get; set; }
    public virtual Expense[] Expenses { get; set; } = null!;
}