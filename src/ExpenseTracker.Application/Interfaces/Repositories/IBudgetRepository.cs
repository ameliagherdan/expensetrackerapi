using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Interfaces.Repositories;

public interface IBudgetRepository
{
    Task<Budget> GetBudgetByUserMonthYearAsync(Guid userId, int month, int year);
    Task<List<Budget>> GetBudgetsByUserAsync(Guid userId);
    Task AddBudgetAsync(Budget budget);
    Task UpdateBudgetAsync(Budget budget);
    Task DeleteBudgetAsync(Guid id);
}