using AutoMapper;
using ExpenseTracker.BLL.RequestModels;
using ExpenseTracker.DAL.Contracts;
using ExpenseTracker.DAL.Models;
using ExpenseTracker.DAL.Repositories;
using ExpenseTracker.DAL.Contracts;
using ExpenseTrackerLogicLayer.Contracts;
using ExpenseTrackerLogicLayer.Models;

namespace ExpenseTrackerLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        Mapper mapper = AutoMappers.InitializeAutoMapper();
        public UserService(IUserRepository userRepository,IUserCredentialsRepository userCredentialsRepository)
        {
            _userRepository = userRepository;
            _userCredentialsRepository = userCredentialsRepository;
        }

        public async Task<(BLUser? user, string ErrorMsg)> Add(BLUser user)
        {
            try
            {
                var userCredentials = await _userCredentialsRepository.AddUserCredentials(new UserCredential() { Password = user.Password });
                if (userCredentials.userCredentials == null) { return (null, userCredentials.ErrorMsg); }
                user.UserCredentialsId = userCredentials.userCredentials.UserCredentialsId;
                Console.WriteLine("Heybll");
                var response = await _userRepository.Add(mapper.Map<User>(user));
                return (mapper.Map<BLUser>(response.user), "");
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(bool isSuccess, string ErrorMsg)> ChangePassword(Guid CredentialId, string OldPassword, string NewPassword)
        {
            var response = await _userCredentialsRepository.UpdatePassword(CredentialId, OldPassword, NewPassword);
            if(!response.isSuccess)return (false,response.ErrorMsg);
            return (true,response.ErrorMsg);
        }

        public Task<BLUser> Delete(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<(BLUser? user,string? ErrorMsg)> Get(LoginModel loginModel)
        {
            try
            {
                var userResponse = loginModel.UserEmail != null ? (await _userRepository.
                    GetuserByEmail(loginModel.UserEmail)) : (await _userRepository.GetUserByName(loginModel.UserName));
                if (userResponse.user == null) return (null, userResponse.ErrorMsg);
                var userCredentialsResponse = await _userCredentialsRepository.IsValidPassword(userResponse.user.UserCredentialsId, loginModel.Password);
                if (!userCredentialsResponse.isValid) return (null, userCredentialsResponse.ErrorMsg);
                return (mapper.Map<BLUser>(userResponse.user), "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }


        public async Task<(BLUser? user,string ErrorMsg)> GetUserById(Guid UserId)
        {
            try
            {
                var user = await _userRepository.GetUserById(UserId);
                return (mapper.Map<BLUser>(user.user), user.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
            
        }

        public async Task<(bool isExist, string ErrorMsg)> IsUserExist(string name, string email)
        {
            return await _userRepository.IsUserExists(name, email);
        }

        public async Task<(BLUser? user,string ErrorMsg)> Update(BLUser user)
        {
            try
            {
                var _user = await _userRepository.Update(mapper.Map<User>(user));
                return (mapper.Map<BLUser>(_user.user), _user.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }

        }
    }
}
