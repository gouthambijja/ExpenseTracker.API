using ExpenseTracker.DAL.Models;
using ExpenseTracker.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ExpenseTrackerContext Context;
        public UserRepository(ExpenseTrackerContext userDbContext)
        {
            Context = userDbContext;
        }
        public async Task<(User? user,string ErrorMsg)> Add(User user)
        {

            try
            {
                Console.WriteLine("rep");
                Context.Add(user);
                await Context.SaveChangesAsync();
                await Context.Entry(user).GetDatabaseValuesAsync();
                return (user, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        //public async Task<(bool isSuccess, string ErrorMsg)> ChangePassword(string Username, string oldpassword, string newPassword)
        //{
        //    try
        //    {
        //        var users = await Context.Users.Where(e => e.UserName == Username && e.Password == oldpassword).ToListAsync();
        //        var user = users[0];
        //        if (user == null) { return (false,"User not Available"); }
        //        user.Password = newPassword ;
        //        user.UpdatedAt = DateTime.Now;
        //        await Context.SaveChangesAsync();
        //        return true ;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public async Task<(User? user,string ErrorMsg)> Delete(Guid UserId)
        {
            throw new NotImplementedException();
        }


        public async Task<(bool isSuccess, string ErrorMsg)> IsUserExists(Guid UserId)
        {
            try
            {
                var _user = await Context.Users.Where(u => u.UserId == UserId).FirstOrDefaultAsync();
                if (_user == null) return (false, "User Not Exists");
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool isSuccess, string ErrorMsg)> IsUserEmailExists(string email)
        {
            try
            {
                var _user = await Context.Users.Where(u => u.UserEmail == email).FirstOrDefaultAsync();
                if (_user == null) return (false, "User Not Exists");
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(User? user,string ErrorMsg)> GetUserById(Guid UserId)
        {
            var _user = await Context.Users.Where(u => u.UserId == UserId).FirstOrDefaultAsync();
            if (_user == null) return (null, "User not Exist");
            return (_user, "");
        }
        public async Task<(User? user, string ErrorMsg)> Update(User _user)
        {
            try
            {
                var user = await Context.Users.
                    Where(u => u.UserId == _user.UserId).FirstOrDefaultAsync();
                if (user == null) return (null, "User not Exist");
                user.UserName = _user.UserName;
                await Context.SaveChangesAsync();
                return (user, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }

        }

        public async Task<(User? user, string ErrorMsg)> GetUserByName(string Username)
        {
            try
            {
                var x = await Context.Users.
                    Where(u => u.UserName == Username)
                    .FirstOrDefaultAsync();
                return (x, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async  Task<(User? user, string ErrorMsg)> GetuserByEmail(string userEmail)
        {
            try
            {
                var x = await Context.Users.
                    Where(u => u.UserEmail == userEmail)
                    .FirstOrDefaultAsync();
                return (x, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(bool isExists, string ErrorMsg)> IsUserExists(string name, string email)
        {
            try
            {
                var user = await Context.Users.Where(e => e.UserName == name || e.UserEmail == email).FirstOrDefaultAsync();
                if (user != null) return (true, "");
                else return (false, "User Didn't Exist");
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
