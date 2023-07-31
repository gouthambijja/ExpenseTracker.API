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
        public async Task<IActionResult> RegisterUser()
        {
            try
            {
                var image = Request.Form.Files[0];

                var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var imageBody = memoryStream.ToArray();

                var user = new BLUser();
                user.ProfileImg = imageBody;
                user.UserName = Request.Form["Name"];
                user.UserEmail = Request.Form["Email"];
                user.PhoneNumber = Request.Form["PhoneNumber"];
                user.SecurityQuestion = Request.Form["SecurityQuestion"];
                user.SecurityAnswer = Request.Form["SecurityAnswer"];
                user.Password = Request.Form["Password"];

                var User = await _userService.IsUserExist(name: user.UserName, email: user.UserEmail);
                Console.WriteLine(User.isExist);
                if (User.isExist == false)
                {
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
        public async Task<IActionResult> LoginUser(LoginModel? LoginUser)
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
        [AllowAnonymous]
        [HttpPost("GoogleSignIn")]
        public async Task<IActionResult> GoogleSignUp([FromBody] BLUser? user)
        {
            try
            {
                user.Password = user.GoogleId;
                var response = await _userService.IsGoogleIdExist(user.GoogleId);
                if (response.isExist == true)
                {
                    return await LoginUser(new() { UserEmail = user.UserEmail, Password = user.Password });

                }
                else
                {
                     var Password = Utility.Encrypt(user.Password);
                    var _user = await _userService.Add(new BLUser()
                    {
                        PhoneNumber = "",
                        UserEmail = user.UserEmail,
                        UserName = user.UserName,
                        GoogleId = user.GoogleId,
                        Password = Password,
                        SecurityQuestion = "",
                        SecurityAnswer = ""
                    });
                    return await LoginUser(new() { UserEmail = user.UserEmail, Password = user.Password });


                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("IsUserExists")]
        public async Task<IActionResult> IsUserExists(string? email)
        {
            try
            {
                var response = await _userService.IsUserExist("", email);
                Console.WriteLine(response.ErrorMsg);
                return Ok(response.isExist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetUserById/{userId:Guid}")]
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
