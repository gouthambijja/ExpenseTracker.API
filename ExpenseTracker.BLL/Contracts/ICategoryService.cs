using ExpenseTracker.DAL.Models;
using ExpenseTrackerLogicLayer.Models;

namespace ExpenseTrackerLogicLayer.Contracts
{
    public interface ICategoryService
    {
        Task<(BLCategory? category, string ErrorMsg)> Add(BLCategory category);
        Task<(BLCategory? category, string ErrorMsg)> Delete(Guid categoryId);
        Task<(List<BLCategory>?, string ErrorMsg)> Get(Guid UserId);
        Task<(BLCategory? category, string ErrorMsg)> Update(BLCategory? category);

    }
}
