using Microsoft.Extensions.Configuration;
using Repositories.dboModels;
using System.Collections.Generic;

namespace Repositories.Query
{
    public class PharmacyQuery : BaseQuery<dboPharmacy>
    {
        public PharmacyQuery(IConfiguration config) : base(config)
        {

        }
        public new List<dboPharmacy> GetAll()
        {
            return base.GetAll();
        }

        public new void Create(dboPharmacy data)
        {
            base.Create(data);
        }

        public new void Update(dboPharmacy data)
        {
            base.Update(data);
        }

        public new void Delete(dboPharmacy data)
        {
            base.Delete(data);
        }

    }
}
