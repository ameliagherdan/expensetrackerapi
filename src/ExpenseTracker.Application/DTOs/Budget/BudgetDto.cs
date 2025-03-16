namespace ExpenseTracker.Application.DTOs;

public class BudgetDto
{
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Total { get; set; }
        
        public int Month { get; set; }
        public int Year { get; set; }
}