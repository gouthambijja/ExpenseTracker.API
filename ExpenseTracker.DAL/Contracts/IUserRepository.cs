using ExpenseTracker.DAL.Models;

namespace ExpenseTracker.DAL.Contracts
{
    public interface IUserRepository
    {
        Task<(User? user,string ErrorMsg)> Add(User user);
        Task<(User? user,string ErrorMsg)> Update(User user);
        Task<(User? user,string ErrorMsg)> Delete(Guid UserId);
        Task<(User? user,string ErrorMsg)> GetUserByName(string userName);
        Task<(User? user, string ErrorMsg)> GetuserByEmail(string userEmail);
        Task<(bool isSuccess, string ErrorMsg)> IsUserExists(Guid UserId);
        Task<(bool isSuccess, string ErrorMsg)> IsUserEmailExists(string? userEmail);
        Task<(bool isExists, string ErrorMsg)> IsUserExists(string name,string email);
        Task<(User? user,string ErrorMsg)> GetUserById(Guid UserId);
    }
}
