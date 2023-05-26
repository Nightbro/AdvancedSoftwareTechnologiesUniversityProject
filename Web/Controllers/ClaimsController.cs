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
    public class ClaimsController : Controller
    {
        private IConfiguration _config;
        private IUserMap _map;
        public ClaimsController(IConfiguration config, IUserMap map)
        {
            _config = config;
            _map = map;
        }

        [HttpGet]
        public dynamic Get()
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var claim = _map.GetClaims(currentUserId);
                


                if (claim != null)
                {
                    return Ok(new BaseResponse<ClaimViewModel>()
                    {
                        isSuccess = true,
                        message = "",
                        data= claim

                    });
                } else
                {
                    return Ok(new BaseSingleResponse<IBaseViewModel>()
                    {
                        isSuccess = false,
                        message = "Could not get information about user"

                    });
                }

            }
            catch (Exception ex)
            {

                return Ok(new BaseSingleResponse<IBaseViewModel>()
                {
                    isSuccess = false,
                    message = ex.Message

                });
            }
        }
    }
}
