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
using System.Linq;
using System.Net.Http;

namespace Web.Controllers
{
    public class UpdateMedicineViewModel: PharmacyMedicineViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    [Route("api/[controller]")]
    public class PharmacyPublicController : ControllerBase
    {

        IUserMap userMap;
        IPharmacyMap pharmacyMap;
        public PharmacyPublicController(IUserMap map, IPharmacyMap pMap)
        {
            userMap = map;
            pharmacyMap = pMap;
        }
        // GET api/user
        [HttpGet]
        [AllowAnonymous]
        public async Task<dynamic> Get(string stringUrl, string destination)
        {
            var client = new HttpClient();
            var data = await client.GetAsync("https://maps.googleapis.com/maps/api/distancematrix/json?origins=" +  stringUrl + "&destinations=" + System.Web.HttpUtility.UrlEncode(destination)+"&key=AIzaSyCYaZJNTjh3HcM_pF-baen1e4zuicLy8ys");
            if (stringUrl != null && stringUrl != "" && destination != "" && destination != null)
            {
                var  content = await  data.Content.ReadAsStringAsync();
                return content;
            }
            return Problem("Not Enough params");

        }
        // PUT api/user/5
        [AllowAnonymous]
        [HttpPost]
        public dynamic Post([FromBody] IEnumerable<UpdateMedicineViewModel> reg)
        {
            try
            {
                if (reg == null || reg.Count() == 0)
                    return Ok(new BaseSingleResponse<IBaseViewModel>()
                    {
                        isSuccess = true,
                        message = "Nothing to Update"

                    });
                var username = reg.First().UserName;
                var password = reg.First().Password;
                var user = userMap.AuthorizeUser(username, password);


                reg.ToList().ForEach(x =>
                {
                    pharmacyMap.UpdateMedicineInPharmacy(x, user.Id);
                });



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


        [HttpPut]
        public dynamic Put([FromBody] IEnumerable<UpdateMedicineViewModel> pmedicine)
        {
            return "";
        }


        

    }
}
