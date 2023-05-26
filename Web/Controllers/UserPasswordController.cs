using Interfaces.Map;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using ViewModels.Responses;
using ViewModels.ViewModels;
namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserPasswordController : Controller
    {
        private IConfiguration _config;
        private IUserMap _map;
        public UserPasswordController(IConfiguration config, IUserMap map)
        {
            _config = config;
            _map = map;
        }

        [HttpPut]
        public dynamic Update([FromBody] ChangePasswordViewModel roleClaim)
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                _map.UpdateUserPassword(roleClaim.Username, roleClaim.Password, currentUserId);
                return Ok(new BaseSingleResponse<IBaseViewModel>()
                {
                    isSuccess = true,
                    message = ""

                });
            }
            catch (Exception ex)
            {
                return Ok(new BaseSingleResponse<UserViewModel>()
                {
                    isSuccess = false,
                    message = ex.Message

                });
            }
        }

    }
}
