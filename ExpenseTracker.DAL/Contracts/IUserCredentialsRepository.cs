using ExpenseTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DAL.Contracts
{
    public interface IUserCredentialsRepository
    {
        Task<(UserCredential userCredentials,string ErrorMsg)> AddUserCredentials(UserCredential userCredentials);
        Task<(bool isValid,string ErrorMsg)> IsValidPassword(Guid id ,string password);
        Task<(bool isSuccess, string ErrorMsg)> UpdatePassword(Guid id,string oldPassword, string newPassword);

    }
}
