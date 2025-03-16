namespace ExpenseTracker.Application.DTOs;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }
}