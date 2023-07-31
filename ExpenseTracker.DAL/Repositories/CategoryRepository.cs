
using ExpenseTracker.DAL.Models;
using ExpenseTracker.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ExpenseTrackerContext Context;
        public CategoryRepository(ExpenseTrackerContext DBContext)
        {
            Context = DBContext;
        }
        public async Task<(Category? category, string ErrorMsg)> Add(Category? category)
        {
            try
            {
                Context.Categories.Add(category);
                await Context.SaveChangesAsync();
                await Context.Entry(category).GetDatabaseValuesAsync();
                return (category, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(Category?, string)> Delete(Guid? categoryId)
        {
            try
            {
                Console.WriteLine(categoryId);
                var category = await Context.Categories.
                     Where(e => e.CategoryId == categoryId).FirstOrDefaultAsync();
                if (category == null) return (null, "Category not present in the database");
                category.IsActive = false;
                category.UpdatedAt = DateTime.Now;
                await Context.SaveChangesAsync();
                return (category, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }


        public async Task<(List<Category>?, string)> Get(Guid? UserId)
        {
            try
            {
                var List = await Context.Categories.
                Where(e => e.UserId == UserId && e.IsActive == true).ToListAsync();
                if (List.Count == 0) return (null, "No Categories present");
                else return (List, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(Category? category, string ErrorMsg)> Update(Category? category)
        {
            try
            {
                var _category = await Context.Categories.Where(e => e.CategoryId == category.CategoryId).FirstOrDefaultAsync();
                _category.Name = category.Name;
                await Context.SaveChangesAsync();
                return (_category,"");
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}
