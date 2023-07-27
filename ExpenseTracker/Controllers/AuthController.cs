using ExpenseTracker.WEBAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.WEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        //jwt methods//---------------------------------
        /// <summary>
        /// Service to Generate JWT Token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="Id"></param>
        /// <param name="role"></param>
        /// <param name="tokenType"></param>
        /// <returns>returns Token</returns>
        internal string GenerateJwtToken(string email,Guid Id, string name,TokenType tokenType)
        {
            DateTime expiresIn;
            string secretKey = string.Empty;
            Console.WriteLine(_config.GetConnectionString("DefaultConnection"));
            if (tokenType == TokenType.AccessToken)
            {
                secretKey = _config["Authentication:JWTSettings:AccessTokenSecretKey"];
                expiresIn = DateTime.Now.AddMinutes(30);
            }
            else
            {
                secretKey = _config["Authentication:JWTSettings:RefreshTokenSecretKey"];
                expiresIn = DateTime.Now.AddMonths(2);
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            var claimUsername = new Claim(ClaimTypes.Email, email);
            var claimIdentifier = new Claim(ClaimTypes.NameIdentifier, Id.ToString());
            var claimName = new Claim(ClaimTypes.Name, name);
            var claimIdentity = new ClaimsIdentity(new[] { claimUsername, claimIdentifier, claimName }, "serverAuth");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //
                Subject = claimIdentity,
                //expire in next x days
                Expires = expiresIn,
                //which 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        //public async Task<ActionResult<LoginCredintials>> GetUserByJwt([FromBody] string jwtToken)
        /// <summary>
        /// Service to LogOut
        /// </summary>
        /// <returns>returns true if logout Successful else false</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> logout()
        {
            try
            {
                Response.Cookies.Delete("RefreshToken");
                return Ok(true);
            }
            catch
            {
                return BadRequest(false);
            }
        }
        /// <summary>
        /// Service to GEnerate new Accesstoken using refresh token
        /// </summary>
        /// <returns>new access token</returns>
        [HttpGet("getNewAccessTokenUsingRefreshToken")]
        public async Task<IActionResult> FetNewAccessTokenUsingRefreshToken()
        {
            try
            {
                var RefreshToken = Request.Cookies["RefreshToken"];
                if(RefreshToken == null)
                {
                    return BadRequest();
                }
                //getting the secret key
                string secretKey = _config["Authentication:JWTSettings:RefreshTokenSecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);

                //preparing the validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                Console.WriteLine(RefreshToken);
                //validationg token
                var principle = tokenHandler.ValidateToken(RefreshToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var name = principle.FindFirst(ClaimTypes.Name)?.Value;
                    var email = principle.FindFirst(ClaimTypes.Email)?.Value;
                    var Id = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return Ok(GenerateJwtToken(email, Guid.Parse(Id),name,TokenType.AccessToken));
                }
                else
                {
                    //here if the token is not valid then a null is sent back
                    return BadRequest("Invalid Token");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
