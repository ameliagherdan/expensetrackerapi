namespace ExpenseTracker.Application.DTOs;

public class BudgetCreateDto
{
    public decimal Total { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}