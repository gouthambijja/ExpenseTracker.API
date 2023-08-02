using ExpenseTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DAL.Contracts
{
    public interface IDefaultCategoryRepository
    {
        Task<List<DefaultCategory>> Get();
    }
}
