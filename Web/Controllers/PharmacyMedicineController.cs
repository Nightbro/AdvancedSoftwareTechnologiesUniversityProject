using CsvHelper;
using Interfaces.Map;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using ViewModels.Responses;
using ViewModels.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PharmacyMedicineController : ControllerBase
    {

        IPharmacyMap map;
        public PharmacyMedicineController(IPharmacyMap _map)
        {
            map = _map;
        }


        [HttpPost]
        public dynamic UploadViaCSV()
        {
            try
            {
                var file = Request.Form.Files[0];
                byte[] fileBites;
                TextReader tx;
                List<PharmacyMedicineViewModel> records;
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header,
                };
                var csvReader = new CsvReader(new StreamReader(file.OpenReadStream()), config);
                records = csvReader.GetRecords<PharmacyMedicineViewModel>().ToList();
                
                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                records.ForEach(record =>
                {
                    map.UpdateMedicineInPharmacy(record, currentUserId);

                });


                return Ok(new BaseSingleResponse<IBaseViewModel>()
                {
                    isSuccess = true,
                    message = ""

                });
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

        // PUT api/user/
        [HttpPut]
        public dynamic Put([FromBody] PharmacyMedicineViewModel pmedicine)
        {
            try
            {

                int currentUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                 map.UpdateMedicineInPharmacy(pmedicine, currentUserId);

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
