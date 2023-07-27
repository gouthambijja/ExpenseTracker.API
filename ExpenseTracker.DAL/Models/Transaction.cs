using System;
using System.Collections.Generic;

namespace ExpenseTracker.DAL.Models;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsPermanentDelete { get; set; }

    public double Amount { get; set; }

    public Guid UserId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User User { get; set; } = null!;
}
