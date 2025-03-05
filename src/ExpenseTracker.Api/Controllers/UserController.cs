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
    
    public UserController(IUserService userService)
    {
        _userService = userService;
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
    /// Assigns a budget to a user for a specific month and year.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <param name="budgetCreateDto">The budget creation data (Total, Month, and Year).</param>
    /// <returns>The updated user details with the budget.</returns>
    [HttpPost("{id}/budget")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignBudget(Guid id, [FromBody] BudgetCreateDto budgetCreateDto)
    {
        try
        {
            var updatedUser = await _userService.AssignBudgetAsync(id, budgetCreateDto);
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Gets all budgets for a user, ordered by month and year.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>A list of budgets for the user.</returns>
    [HttpGet("{id}/budgets")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BudgetDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBudgetsByUser(Guid id)
    {
        var budgets = await _userService.GetBudgetsByUserAsync(id);
        if (budgets == null || budgets.Count == 0)
        {
            return NotFound($"No budgets found for user with ID {id}.");
        }
        return Ok(budgets);
    }
}
