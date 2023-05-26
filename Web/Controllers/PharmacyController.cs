using Interfaces.Map;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using ViewModels.Responses;
using ViewModels.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PharmacyController : ControllerBase
    {

        IPharmacyMap map;
        public PharmacyController(IPharmacyMap _map)
        {
            map = _map;
        }

        [HttpGet]
        [AllowAnonymous]

        public dynamic Get()
        {
            try
            {
                var data = map.GetAllPharmacies(0);
                return Ok(new BaseResponse<PharmacyViewModel>()
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
                    message = ex.ToString()

                });
            }
        }



        // PUT api/user/
        [HttpPut]
        public dynamic Put([FromBody] PharmacyViewModel pharmacy)
        {
            try
            {

                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (pharmacy.Id == 0)
                {
                    map.AddPharmacy(pharmacy, currentUserId);
                }
                else
                {
                    map.UpdatePharmacy(pharmacy, currentUserId);
                }
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
                    message = ex.ToString()
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
                var pharmacy = map.GetAllPharmacies(currentUserId).Where(x => x.Id == id).FirstOrDefault();
                if (pharmacy != null) map.DeletePharmacy(pharmacy, currentUserId);
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
                    message = ex.ToString()

                });
            }
        }
    }
}
