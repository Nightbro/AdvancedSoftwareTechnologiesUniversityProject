using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Interfaces.Map;
using ViewModels.Responses;
using ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private IConfiguration _config;
        private IUserMap _map;
        public AuthenticationController(IConfiguration config, IUserMap map)
        {
            _config = config;
            _map = map;
        }
        [AllowAnonymous]
        [HttpPost]
        public dynamic Post([FromBody] LoginViewModel login)
        {
            try
            {
                IActionResult response = Unauthorized();
                var user = Authenticate(login);
                if (user != null)
                {
                    var tokenString = BuildToken(user);
                    response = Ok(new { token = tokenString });

                    return Ok(new BaseSingleResponse<TokenViewModel>()
                    {
                        isSuccess = true,
                        message = "",
                        data = new TokenViewModel() { token = tokenString} 

                    });
                } else
                {
                    return Ok(new BaseResponse<TokenViewModel>()
                    {
                        isSuccess = false,
                        message = "Your username/password combination is wrong"

                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<TokenViewModel>()
                {
                    isSuccess = false,
                    message = ex.Message

                }); 
            }
        }


        [HttpGet]
        //public Task<ActionResult> Get()
        public dynamic Get()
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = _map.GetUserInfo(currentUserId);
                
                if (user != null)
                {
                    return Ok(new BaseSingleResponse<UserViewModel>()
                    {
                        isSuccess = true,
                        message = "",
                        data= user

                    });
                } else
                {
                    return Ok(new BaseSingleResponse<UserViewModel>()
                    {
                        isSuccess = false,
                        message = "Could not get information about user"

                    });
                }

            }
            catch (Exception ex)
            {

                return Ok(new BaseSingleResponse<UserViewModel>()
                {
                    isSuccess = false,
                    message = ex.ToString()

                });
            }
            //return userMap.GetAll(); ;
        }
        private string BuildToken(UserViewModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
         
              _config["Jwt:Issuer"],
              claims: new Claim[] { 
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Actor, user.UserName)

              },
              expires: DateTime.Now.AddMinutes(600),
              signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserViewModel Authenticate(LoginViewModel login)
        {
            //UserViewModel user = null;
            return _map.AuthorizeUser(login.Username, login.Password);


            //if (login.username == "pablo" && login.password == "secret")
            //{
            //    user = new UserViewModel { fullName = "Pablo" };
            //}
            //return user;
        }
    }
}
