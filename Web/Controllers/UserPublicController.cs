using Interfaces.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ViewModels.Responses;
using ViewModels.ViewModels;

namespace Web.Controllers
{
    public class RegisterViewModel
    {
        public UserViewModel user { get; set; }
        public string password { get; set; }
    }


    [Route("api/[controller]")]
    public class UserPublicController : ControllerBase
    {

        IUserMap userMap;
        public UserPublicController(IUserMap map)
        {
            userMap = map;
        }
        // GET api/user
        

        // PUT api/user/5
        [HttpPut]
        [AllowAnonymous]
        public dynamic Put([FromBody] RegisterViewModel reg)
        {
            try
            {

                    userMap.RegisterUser(reg.user, reg.password);
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
