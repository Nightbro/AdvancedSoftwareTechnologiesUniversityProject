using Dapper;
using Microsoft.Extensions.Configuration;
using Repositories.dboModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Repositories.Query
{
    public class PharmacyMedicineQuery : BaseQuery<dboPharmacyMedicine>
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        public PharmacyMedicineQuery(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public List<dboPharmacyMedicine> GetForPharmacy(int id)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                string sql = "SELECT * FROM [dbo].[PharmacyMedicine] WHERE IdPharmacy=" + id;
                var result = db.Query<dboPharmacyMedicine>(sql);
                return result.ToList();

            }
        }

        public new void Update(dboPharmacyMedicine data)
        {

            string sql = "UPDATE [dbo].[PharmacyMedicine] SET Price="+data.Price+", Quantity="+data.Quantity+" WHERE IdMedicine=" + data.IdMedicine + " AND IdPharmacy=" + data.IdPharmacy;
            using (IDbConnection conn = Connection)
            {
                var i = conn.Execute(sql);
                if (i==0)
                {
                    base.Create(data);
                }

            }
        }

    }
}
