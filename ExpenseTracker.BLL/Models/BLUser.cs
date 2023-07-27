using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerLogicLayer.Models
{
    public class BLUser
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

        public string? Password { get; set; }

    }
}
