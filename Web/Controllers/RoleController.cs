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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : Controller
    {
        private IConfiguration _config;
        private IUserMap _map;
        public RoleController(IConfiguration config, IUserMap map)
        {
            _config = config;
            _map = map;
        }

        [HttpGet]
        public dynamic Get()
        {
            try
            {
                //If ID=-1 retrieve data for all current user
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var roles = _map.GetRoles( currentUserId);
                


                if (roles != null)
                {
                    return Ok(new BaseResponse<RoleViewModel>()
                    {
                        isSuccess = true,
                        message = "",
                        data= roles

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
                    message = ex.Message

                });
            }
            //return userMap.GetAll(); ;
        }


        [HttpPost]
        public dynamic Add([FromBody] RoleViewModel role)
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var roles = _map.AddRole(role, currentUserId);



                if (roles != null)
                {
                    return Ok(new BaseSingleResponse<RoleViewModel>()
                    {
                        isSuccess = true,
                        message = "",
                        data = roles

                    });
                }
                else
                {
                    return Ok(new BaseSingleResponse<UserViewModel>()
                    {
                        isSuccess = false,
                        message = "Could not not process the request"

                    });
                }
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

        [HttpPut]
        public dynamic Update([FromBody] RoleViewModel role)
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var roles = _map.UpdateRole(role, currentUserId);



                if (roles != null)
                {
                    return Ok(new BaseSingleResponse<RoleViewModel>()
                    {
                        isSuccess = true,
                        message = "",
                        data = roles

                    });
                }
                else
                {
                    return Ok(new BaseSingleResponse<UserViewModel>()
                    {
                        isSuccess = false,
                        message = "Could not process the request"

                    });
                }
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

        [HttpDelete("{id}")]
        public dynamic Delete(int id)
        {
            try
            {
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var roles = _map.DeleteRole(id, currentUserId);



                if (roles)
                {
                    return Ok(new BaseSingleResponse<RoleViewModel>()
                    {
                        isSuccess = true,
                        message = ""

                    });
                }
                else
                {
                    return Ok(new BaseSingleResponse<UserViewModel>()
                    {
                        isSuccess = false,
                        message = "Could not not process the request"

                    });
                }
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
