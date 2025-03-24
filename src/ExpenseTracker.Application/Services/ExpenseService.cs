using ExpenseTracker.Application.Interfaces;
using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<List<ExpenseDto>> GetAllExpensesAsync()
    {
        var expenses = await _expenseRepository.GetAllExpensesAsync();
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    public async Task<List<ExpenseDto>> GetAllExpensesAsyncByUserId(Guid userId)
    {
        var expenses = await _expenseRepository.GetExpensesAsyncByUserId(userId);
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    public async Task<ExpenseDto> GetExpenseAsyncById(Guid id)
    {
        var expense = await _expenseRepository.GetExpenseByIdAsync(id);
        return expense == null ? null : _mapper.Map<ExpenseDto>(expense);
    }

    public async Task<ExpenseDto> CreateExpenseAsync(ExpenseDto expenseDto)
    {
        var expense =  _mapper.Map<Expense>(expenseDto);
        await  _expenseRepository.AddExpenseAsync(expense);
        return _mapper.Map<ExpenseDto>(expense);
    }

    public async Task<ExpenseDto> UpdateExpenseAsync(ExpenseDto expenseDto)
    {
        var existingExpense = await _expenseRepository.GetExpenseByIdAsync(expenseDto.Id);
        if(existingExpense == null) return null;
        
        _mapper.Map(expenseDto, existingExpense);
        await _expenseRepository.UpdateExpenseAsync(existingExpense);

        return _mapper.Map<ExpenseDto>(existingExpense);

    }

    public async Task<bool> DeleteExpenseAsync(Guid id)
    {
        var expense = await _expenseRepository.GetExpenseByIdAsync(id);
        if (expense == null) return false;

        await _expenseRepository.DeleteExpenseAsync(id);
        return true;
    }
}