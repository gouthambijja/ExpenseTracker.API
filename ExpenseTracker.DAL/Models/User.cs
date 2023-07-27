using System;
using System.Collections.Generic;

namespace ExpenseTracker.DAL.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string? GoogleId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public byte[]? ProfileImg { get; set; }

    public Guid UserCredentialsId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool IsPermanentDelete { get; set; }

    public string SecurityQuestion { get; set; } = null!;

    public string SecurityAnswer { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserCredential UserCredentials { get; set; } = null!;
}
