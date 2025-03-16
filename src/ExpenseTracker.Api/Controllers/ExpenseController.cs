using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    /// <summary>
    /// Gets all expenses.
    /// </summary>
    /// <returns>List of expenses.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseDto>))]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }
    
    /// <summary>
    /// Gets an expense by its ID.
    /// </summary>
    /// <param name="id">The expense ID.</param>
    /// <returns>The requested expense.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var expense = await _expenseService.GetExpenseAsyncById(id);
        return expense == null ? NotFound($"Expense with ID {id} not found.") : Ok(expense);
    }

    /// <summary>
    /// Creates a new expense.
    /// </summary>
    /// <param name="expenseDto">Expense data.</param>
    /// <returns>The created expense ID.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ExpenseDto expenseDto)
    {
        if (expenseDto == null)
            return BadRequest("Expense data cannot be null.");

        var expenseId = await _expenseService.CreateExpenseAsync(expenseDto);
        return CreatedAtAction(nameof(GetById), new { id = expenseId }, new { id = expenseId });
    }

    /// <summary>
    /// Updates an existing expense.
    /// </summary>
    /// <param name="id">The ID of the expense to update.</param>
    /// <param name="expenseDto">Updated expense data.</param>
    /// <returns>The updated expense.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ExpenseDto expenseDto)
    {
        if (expenseDto == null)
            return BadRequest("Expense data cannot be null.");

        var updatedExpense = await _expenseService.UpdateExpenseAsync(expenseDto);
        return updatedExpense == null ? NotFound($"Expense with ID {id} not found.") : Ok(updatedExpense);
    }

    /// <summary>
    /// Deletes an expense by its ID.
    /// </summary>
    /// <param name="id">The ID of the expense to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _expenseService.DeleteExpenseAsync(id);
        return deleted ? NoContent() : NotFound($"Expense with ID {id} not found.");
    }
}
