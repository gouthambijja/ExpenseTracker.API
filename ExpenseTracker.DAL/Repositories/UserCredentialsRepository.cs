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
    public class UserCredentialsRepository : IUserCredentialsRepository
    {
        private readonly ExpenseTrackerContext Context;
        public UserCredentialsRepository(ExpenseTrackerContext context)
        {
            Context = context;
        }
        public async Task<(UserCredential userCredentials, string ErrorMsg)> AddUserCredentials(UserCredential userCredentials)
        {
            try
            {
                await Context.UserCredentials.AddAsync(userCredentials);
                await Context.SaveChangesAsync();
                await Context.Entry(userCredentials).GetDatabaseValuesAsync();
                return (userCredentials, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }

        }

        public async Task<(bool isValid, string ErrorMsg)> IsValidPassword(Guid id, string password)
        {
            try
            {
                var userCredentials = await Context.UserCredentials.Where(e => e.UserCredentialsId == id).FirstOrDefaultAsync();
                if (userCredentials == null) return (false, "CredentialId not Found");
                return (password == userCredentials.Password, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool isSuccess, string ErrorMsg)> UpdatePassword(Guid id, string oldPassword,string newPassword)
        {
            try
            {

                var userCredentials = await Context.UserCredentials.Where(e => e.UserCredentialsId == id).FirstOrDefaultAsync();
                if (userCredentials == null) return (false, "CredentialId not Found");
                if (userCredentials.Password != oldPassword) return (false, "Entered Wrong Password");
                userCredentials.Password = newPassword;
                Context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    }
}
