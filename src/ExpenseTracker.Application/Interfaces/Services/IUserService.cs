using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<BudgetDto?> GetUserBudgetAsync(Guid userId);
    Task<UserDto> AssignBudgetAsync(Guid userId, BudgetCreateDto budgetCreateDto);
    Task<List<BudgetDto>> GetBudgetsByUserAsync(Guid userId);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task<UserDto> UpdateUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(Guid id);
}