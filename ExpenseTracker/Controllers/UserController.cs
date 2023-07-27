using ExpenseTracker.BLL.RequestModels;
using ExpenseTracker.DAL.Models;
using ExpenseTracker.WEBAPI.Utilities;
using ExpenseTrackerLogicLayer.Contracts;
using ExpenseTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.WEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly AuthController _authController;
        public UserController(IUserService userService, IConfiguration configuration, AuthController authController)
        {
            _userService = userService;
            _config = configuration;
            _authController = authController;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(BLUser user)
        {
            try
            {
                var User = await _userService.IsUserExist(name: user.UserName, email: user.UserEmail);
                Console.WriteLine(User.isExist);
                if (User.isExist == false)
                {
                    Console.WriteLine("hey");
                    user.Password = Utility.Encrypt(user.Password);
                    var response = await _userService.Add(user);
                    if (response.user == null)
                    {
                        return BadRequest(response.ErrorMsg);
                    }
                    else
                    {
                        return Ok(response.user);
                    }
                }
                else
                {
                    return BadRequest("UserName Already Exists");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel LoginUser)
        {
            try
            {
                LoginUser.Password = Utility.Encrypt(LoginUser.Password);
                var response = await _userService.Get(LoginUser);
                if (response.user == null)
                {
                    return BadRequest(response.ErrorMsg);
                }
                else
                {
                    var accessToken = _authController.GenerateJwtToken(response.user.UserEmail, response.user.UserId, response.user.UserName, TokenType.AccessToken);
                    string refreshToken = _authController.GenerateJwtToken(response.user.UserEmail, response.user.UserId, response.user.UserName, TokenType.RefreshToken);
                    Response.Cookies.Append("RefreshToken", refreshToken,
                        new CookieOptions
                        {
                            HttpOnly = true
                        });
                    return Ok(accessToken);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("IsUserExists")]
        public async Task<IActionResult> IsUserExists(string? Username, string? email)
        {
            try
            {
                var response = await _userService.IsUserExist(Username, email);
                if (response.isExist) return Ok(response.isExist);
                else return BadRequest(response.ErrorMsg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetuserById(Guid userId)
        {
            try
            {
                var response = await _userService.GetUserById(userId);
                if (response.user != null) return Ok(response.user);
                else return BadRequest(response.ErrorMsg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePassword updatePassword)
        {
            try
            {
                var response = await _userService.ChangePassword(updatePassword.UserId, Utility.Encrypt(updatePassword.OldPassword), Utility.Encrypt(updatePassword.NewPassword));
                if (response.isSuccess)
                {
                    return Ok(response.isSuccess);
                }
                else
                {
                    return BadRequest(response.ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
