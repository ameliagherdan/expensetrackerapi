using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }


    /// <summary>
    /// Creates a new budget.
    /// </summary>
    /// <param name="budgetDto">Budget data.</param>
    /// <returns>Newly created budget.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BudgetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBudget([FromBody] BudgetCreateDto budgetDto)
    {
        var budget = await _budgetService.CreateBudgetAsync(budgetDto);
        return StatusCode(StatusCodes.Status201Created, budget);
    }

    /// <summary>
    /// Updates an existing budget by its ID.
    /// </summary>
    /// <param name="id">Budget ID.</param>
    /// <param name="budgetDto">Updated budget data.</param>
    /// <returns>Updated budget data.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] BudgetDto budgetDto)
    {
        var updatedBudget = await _budgetService.UpdateBudgetAsync(id, budgetDto);
        return Ok(updatedBudget);
    }

    /// <summary>
    /// Deletes a budget by its ID.
    /// </summary>
    /// <param name="id">Budget ID.</param>
    /// <returns>No content if deleted successfully.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBudget(Guid id)
    {
        await _budgetService.DeleteBudgetAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Assigns a budget to a user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="budgetCreateDto">Budget data to assign.</param>
    /// <returns>Assigned budget.</returns>
    [HttpPost("assign/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignBudgetToUser(Guid userId, [FromBody] BudgetCreateDto budgetCreateDto)
    {
        var budget = await _budgetService.AssignBudgetToUserAsync(userId, budgetCreateDto);
        return Ok(budget);
    }
}