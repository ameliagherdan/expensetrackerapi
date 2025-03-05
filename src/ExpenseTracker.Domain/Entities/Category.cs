﻿namespace ExpenseTracker.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}