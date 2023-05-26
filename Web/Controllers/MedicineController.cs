using Interfaces.Map;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using ViewModels.Responses;
using ViewModels.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MedicineController : ControllerBase
    {

        IPharmacyMap map;
        public MedicineController(IPharmacyMap _map)
        {
            map = _map;
        }

        [HttpGet]
        [AllowAnonymous]
        public dynamic Get()
        {
            try
            {
                var data = map.GetAllMedicine(0);
                return Ok(new BaseResponse<MedicineViewModel>()
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
        }


        [HttpPost]
        public dynamic Post([FromBody] MedicineViewModel medicine)
        {
            try
            {

                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                    map.AddMedicine(medicine, currentUserId);
                
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
        // PUT api/user/
        [HttpPut]
        public dynamic Put()
        {
            try
            {
                var obj = Request.Form["medicine"];
                var medicine = Newtonsoft.Json.JsonConvert.DeserializeObject<MedicineViewModel>(obj);
                if (Request.Form.Files.Count>0)
                {
                    var file = Request.Form.Files[0];
                    byte[] fileBites;
                    if (file != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBites = ms.ToArray();

                        }
                        medicine.ImageFile = fileBites;
                        medicine.OriginalFormat = file.ContentType;
                        medicine.ImageName = file.FileName;
                    }
                }
                



                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                
                    map.UpdateMedicine(medicine, currentUserId);
                
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
                var medicine = map.GetAllMedicine(currentUserId).Where(x => x.Id == id).FirstOrDefault();
                if (medicine != null) map.DeleteMedicine(medicine, currentUserId);
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
