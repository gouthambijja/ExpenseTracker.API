using ExpenseTracker.DAL.Contracts;
using ExpenseTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DAL.Repositories
{
    public class DefaultCategoryRepository : IDefaultCategoryRepository
    {
        private readonly ExpenseTrackerContext Context;
        public DefaultCategoryRepository(ExpenseTrackerContext DBContext)
        {
            Context = DBContext;
        }
        public async Task<List<DefaultCategory>> Get()
        {
            try
            {
                return await Context.DefaultCategories.ToListAsync();
            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}
