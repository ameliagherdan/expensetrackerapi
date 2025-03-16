namespace ExpenseTracker.Application.DTOs;

public class BudgetCreateDto
{
    public Guid UserId { get; set; }
    public decimal Total { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}