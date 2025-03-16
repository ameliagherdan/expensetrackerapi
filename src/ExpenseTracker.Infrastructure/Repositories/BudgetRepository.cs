using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class BudgetRepository : IBudgetRepository
{
    private readonly ApplicationDbContext _context;

    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Budget> GetBudgetByUserMonthYearAsync(Guid userId, int month, int year)
    {
        return await _context.Budgets
            .FirstOrDefaultAsync(b => b.UserId == userId && b.Month == month && b.Year == year);
    }

    public async Task<List<Budget>> GetBudgetsByUserAsync(Guid userId)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task AddBudgetAsync(Budget budget)
    {
        budget.Id = Guid.NewGuid();
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBudgetAsync(Budget budget)
    {
        _context.Budgets.Update(budget);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBudgetAsync(Guid id)
    {
        var budget = await _context.Budgets.FindAsync(id);
        if (budget == null)
            throw new KeyNotFoundException($"Budget with ID '{id}' not found.");

        _context.Budgets.Remove(budget);
        await _context.SaveChangesAsync();
    }
}