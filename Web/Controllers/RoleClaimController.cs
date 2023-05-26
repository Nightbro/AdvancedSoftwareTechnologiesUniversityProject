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
    public class RoleClaimController : Controller
    {
        private IConfiguration _config;
        private IUserMap _map;
        public RoleClaimController(IConfiguration config, IUserMap map)
        {
            _config = config;
            _map = map;
        }

        [HttpPost]
        public dynamic Add([FromBody] RoleClaimViewModel roleClaim)
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                _map.AddRoleClaim(roleClaim.RoleId, roleClaim.ClaimId, currentUserId);
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

        [HttpDelete("{roleId}/{claimId}")]
        public dynamic Delete(int roleId, int claimId)
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                _map.DeleteRoleClaim(roleId, claimId, currentUserId); 

                return Ok(new BaseSingleResponse<RoleViewModel>()
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
