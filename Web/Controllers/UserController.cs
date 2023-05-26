using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interfaces.Map;
using Maps;
using Repositories;
using ViewModels.Responses;
using ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{



    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {

        IUserMap userMap;
        public UserController(IUserMap map)
        {
            userMap = map;
        }
        // GET api/user
        [HttpGet]
        public dynamic Get()
        {
            try
            {

                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var data = userMap.GetAllUsers(currentUserId);


                //var output = reader.ReadToEnd();

                return Ok(new BaseResponse<UserViewModel>()
                {
                    isSuccess = true,
                    message = "",
                    data = data

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
            //return userMap.GetAll(); ;
        }



        // PUT api/user/5
        [HttpPut]
        public dynamic Put([FromBody] UserViewModel user)
        {
            try
            {

                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (user.Id == 0)
                {
                    user.UserName = user.FirstName.ToLower().Substring(0, 1) + user.LastName.ToLower(); 
                    userMap.AddUser(user, currentUserId);

                }
                else
                {
                    userMap.UpdateUser(user, currentUserId);

                }



                //var output = reader.ReadToEnd();

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
        
        // DELETE api/user/5
        [HttpDelete("{id}")]
        public dynamic Delete(int id)
        {

            try
            {

                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                userMap.DeleteUser(id, currentUserId);


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
