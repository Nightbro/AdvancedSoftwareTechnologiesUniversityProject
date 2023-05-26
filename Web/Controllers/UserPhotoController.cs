using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Interfaces.Map;
using ViewModels.Responses;
using ViewModels.ViewModels;
using System;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserPhotoController : Controller
    {
        private IConfiguration _config;
        private IUserMap _map;
        public UserPhotoController(IConfiguration config, IUserMap map)
        {
            _config = config;
            _map = map;
        }

        [HttpGet("{id}")]
        public dynamic Get(int id = -1)
        {
            try
            {
                //If ID=-1 retrieve data for all current user
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var photo = _map.GetUserPhoto(id, currentUserId);
                


                if (photo != null)
                {
                    return Ok(new BaseSingleResponse<UserPhotoViewModel>()
                    {
                        isSuccess = true,
                        message = "",
                        data= photo

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

        [HttpPut]
        public dynamic Update()
        {
            try
            {
                var file = Request.Form.Files[0];
                byte[] fileBites;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBites = ms.ToArray();
                    
                }
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                _map.UpdateUserPhoto(Int32.Parse(file.Name), file.ContentType, fileBites, currentUserId);

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
