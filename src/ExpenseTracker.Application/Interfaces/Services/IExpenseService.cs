using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface IExpenseService
{
    Task<List<ExpenseDto>> GetAllExpensesAsync();
    Task<ExpenseDto> GetExpenseAsyncById(Guid id);
    Task<ExpenseDto> CreateExpenseAsync(ExpenseDto expense);
    Task<ExpenseDto> UpdateExpenseAsync(ExpenseDto expense);
    Task<bool> DeleteExpenseAsync(Guid id);
}