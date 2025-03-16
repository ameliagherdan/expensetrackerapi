using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ExpenseTracker.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _context;

    public ExpenseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Expense>> GetAllExpensesAsync()
    {
        return await _context.Expenses.Include(e=>e.Category).ToListAsync();
    }
    public async Task<List<Expense>> GetExpensesAsyncByUserId(Guid userId)
    {
        return await _context.Expenses.Include(e=>e.Category).Where(e => e.UserId == userId).ToListAsync();
    }
    public async Task<Expense> GetExpenseByIdAsync(Guid id)
    {
        return await _context.Expenses.Include(e=>e.Category).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateExpenseAsync(Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpenseAsync(Guid id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}