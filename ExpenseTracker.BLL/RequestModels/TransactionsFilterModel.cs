using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.BLL.RequestModels
{
    public class TransactionsFilterModel
    {
        public Guid UserId{get;set;}
        public string? Name{get;set;}
        public Guid? Category{get;set;}
        public string? Description{get;set;}
        public DateTime? StartDate{get;set;} 
        public DateTime? EndDate { get; set; }
    }
}
