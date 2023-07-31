using ExpenseTracker.DAL.Models;

namespace ExpenseTracker.DAL.Contracts
{
    public interface ICategoryRepository
    {
        Task<(Category? category, string ErrorMsg)> Add(Category? category);
        Task<(Category? category, string ErrorMsg)> Delete(Guid? categoryId);
        Task<(List<Category>?, string ErrorMsg)> Get(Guid? UserId);
        Task<(Category? category, string ErrorMsg)> Update(Category? category);
    }
}
