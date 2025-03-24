using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IExpenseService _expenseService;
    private readonly IBudgetService _budgetService;
    
    public UserController(IUserService userService, IExpenseService expenseService,  IBudgetService budgetService)
    {
        _userService = userService;
        _expenseService = expenseService;
        _budgetService = budgetService;
    }
    
    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>List of users.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Gets a user by its ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The requested user.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return user == null ? NotFound($"User with ID {id} not found.") : Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="userDto">User data.</param>
    /// <returns>The created user ID.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        if (userDto == null)
            return BadRequest("User data cannot be null.");

        var userId = await _userService.CreateUserAsync(userDto);
        return CreatedAtAction(nameof(GetById), new { id = userId }, new { id = userId });
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="userDto">Updated user data.</param>
    /// <returns>The updated user.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserDto userDto)
    {
        if (userDto == null)
            return BadRequest("User data cannot be null.");

        var updatedUser = await _userService.UpdateUserAsync(userDto);
        return updatedUser == null ? NotFound($"User with ID {id} not found.") : Ok(updatedUser);
    }

    /// <summary>
    /// Deletes a user by its ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _userService.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound($"User with ID {id} not found.");
    }
    
    
    /// <summary>
    /// Gets all budgets for a user.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>A list of budgets for the user.</returns>
    [HttpGet("{id}/expenses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseDto>))]
    public async Task<IActionResult> GetExpensesByUserId(Guid id)
    {
        var expenses = await _expenseService.GetAllExpensesAsyncByUserId(id);
        return Ok(expenses);
    }

    /// <summary>
    /// Retrieves budgets by user ID.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>List of budgets for the specified user.</returns>
    [HttpGet("{id}/budgets")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BudgetDto>))]
    public async Task<IActionResult> GetBudgetsByUserId(Guid id)
    {
        var budgets = await _budgetService.GetBudgetsByUserIdAsync(id);
        return Ok(budgets);
    }
}
