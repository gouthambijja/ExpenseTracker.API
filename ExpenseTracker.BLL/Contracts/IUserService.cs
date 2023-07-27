
using ExpenseTracker.BLL.RequestModels;
using ExpenseTrackerLogicLayer.Models;

namespace ExpenseTrackerLogicLayer.Contracts
{
    public interface IUserService
    {
        Task<(BLUser? user, string ErrorMsg)> Add(BLUser user);
        Task<(BLUser? user, string ErrorMsg)> Update(BLUser user);
        Task<(BLUser? user, string ErrorMsg)> Get(LoginModel loginModel);
        Task<(BLUser? user, string ErrorMsg)> GetUserById(Guid UserId);
        Task<(bool isSuccess, string ErrorMsg)> ChangePassword(Guid CredentialId, string OldPassword, string NewPassword);
        Task<(bool isExist,string ErrorMsg)> IsUserExist(string name,string email);  
    }
}
