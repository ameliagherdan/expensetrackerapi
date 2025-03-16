using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface IBudgetService
{
    Task<List<BudgetDto>> GetBudgetsByUserIdAsync(Guid userId);
    Task<BudgetDto> CreateBudgetAsync(BudgetCreateDto budgetDto);
    Task<BudgetDto> UpdateBudgetAsync(Guid id, BudgetDto budgetDto);
    Task<bool> DeleteBudgetAsync(Guid id);
    Task<BudgetDto> AssignBudgetToUserAsync(Guid userId, BudgetCreateDto budgetCreateDto);
}