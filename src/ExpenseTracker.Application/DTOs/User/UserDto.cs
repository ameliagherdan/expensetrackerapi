namespace ExpenseTracker.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string AzureId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    public BudgetDto? Budget { get; set; }

}