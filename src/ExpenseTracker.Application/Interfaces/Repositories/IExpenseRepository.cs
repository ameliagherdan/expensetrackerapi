﻿using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Interfaces.Repositories;

public interface IExpenseRepository
{
    Task<List<Expense>> GetAllExpensesAsync();
    Task<List<Expense>> GetExpensesAsyncByUserId(Guid userId);
    Task<Expense?> GetExpenseByIdAsync(Guid id);
    Task AddExpenseAsync(Expense expense);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(Guid id);
}