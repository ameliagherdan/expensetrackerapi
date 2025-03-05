using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services;

public class UserService(IUserRepository userRepository, IBudgetRepository budgetRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await  _userRepository.AddUserAsync(user);
        return _mapper.Map<UserDto>(user);
    }
    
    public async Task<BudgetDto?> GetUserBudgetAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user == null || user.Budget == null)
            return null;

        return _mapper.Map<BudgetDto>(user.Budget);
    }
    
    public async Task<UserDto> AssignBudgetAsync(Guid userId, BudgetCreateDto budgetCreateDto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User with ID {userId} not found.");
        }
        
        var existingBudget = await _budgetRepository.GetBudgetByUserAndDateAsync(userId, budgetCreateDto.Month, budgetCreateDto.Year);
        if (existingBudget != null)
        {
            existingBudget.Total = budgetCreateDto.Total;
            await _budgetRepository.UpdateBudgetAsync(existingBudget);
        }
        else
        {
            var newBudget = _mapper.Map<Budget>(budgetCreateDto);
            newBudget.UserId = userId;
            await _budgetRepository.AddBudgetAsync(newBudget);
            
            user.Budget = newBudget;
        }
        
        user = await _userRepository.GetUserByIdAsync(userId);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<BudgetDto>> GetBudgetsByUserAsync(Guid userId)
    {
        var budgets = await _budgetRepository.GetBudgetsByUserAsync(userId);
        return _mapper.Map<List<BudgetDto>>(budgets);
    }
    
    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);
        if(existingUser == null) return null;
        
        _mapper.Map(userDto, existingUser);
        await _userRepository.UpdateUserAsync(existingUser);

        return _mapper.Map<UserDto>(existingUser);

    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) return false;

        await _userRepository.DeleteUserAsync(id);
        return true;
    }
}