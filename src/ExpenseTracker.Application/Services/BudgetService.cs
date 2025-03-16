using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IMapper _mapper;

    public BudgetService(IBudgetRepository budgetRepository, IMapper mapper)
    {
        _budgetRepository = budgetRepository;
        _mapper = mapper;
    }

    public async Task<List<BudgetDto>> GetBudgetsByUserIdAsync(Guid userId)
    {
        var budgets = await _budgetRepository.GetBudgetsByUserAsync(userId);
        return _mapper.Map<List<BudgetDto>>(budgets);
    }

    public async Task<BudgetDto> CreateBudgetAsync(BudgetCreateDto budgetDto)
    {
        var budget = _mapper.Map<Budget>(budgetDto);
        await _budgetRepository.AddBudgetAsync(budget);
        return _mapper.Map<BudgetDto>(budget);
    }

    public async Task<BudgetDto> UpdateBudgetAsync(Guid id, BudgetDto budgetDto)
    {
        var budget = await _budgetRepository.GetBudgetByUserMonthYearAsync(budgetDto.UserId, budgetDto.Month, budgetDto.Year);
        if (budget == null)
            throw new KeyNotFoundException($"Budget with ID '{id}' not found.");

        _mapper.Map(budgetDto, budget);
        await _budgetRepository.UpdateBudgetAsync(budget);
        return _mapper.Map<BudgetDto>(budget);
    }

    public async Task<bool> DeleteBudgetAsync(Guid id)
    {
        await _budgetRepository.DeleteBudgetAsync(id);
        return true;
    }

    public async Task<BudgetDto> AssignBudgetToUserAsync(Guid userId, BudgetCreateDto budgetDto)
    {
        var budget = _mapper.Map<Budget>(budgetDto);
        budget.UserId = userId;
        await _budgetRepository.AddBudgetAsync(budget);
        return _mapper.Map<BudgetDto>(budget);
    }
}
