using System;
using System.Collections.Generic;

namespace ExpenseTracker.DAL.Models;

public partial class BLUserCredential
{
    public Guid UserCredentialsId { get; set; }

    public string Password { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

}
