using ExpenseTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerLogicLayer.Models
{
    public class BLTransaction
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

    }
}
