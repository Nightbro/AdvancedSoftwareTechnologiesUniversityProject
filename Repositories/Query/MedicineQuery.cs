using Dapper;
using Microsoft.Extensions.Configuration;
using Interfaces.Repository;
using Models;
using Repositories.dboModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Repositories.Query
{
    public class MedicineQuery : BaseQuery<dboMedicine>
    {

        public MedicineQuery(IConfiguration config) : base(config)
        {

        }

        public new List<dboMedicine> GetAll()
        {
            return base.GetAll();
        }

        public new void Create(dboMedicine data)
        {
            base.Create(data);
        }

        public new void Update(dboMedicine data)
        {
            base.Update(data);
        }

        public new void Delete(dboMedicine data)
        {
            base.Delete(data);
        }
    }
}
