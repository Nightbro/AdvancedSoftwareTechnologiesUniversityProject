using Dapper.Contrib.Extensions;
using System;

namespace Repositories.dboModels
{
    [Table("PharmacyMedicine")]
    public class dboPharmacyMedicine : IDbEntity
    {
        [ExplicitKey]
        public long IdMedicine { get; set; }
        [ExplicitKey]
        public int IdPharmacy { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
