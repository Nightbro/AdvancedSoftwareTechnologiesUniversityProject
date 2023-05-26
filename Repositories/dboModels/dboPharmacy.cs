using Dapper.Contrib.Extensions;

namespace Repositories.dboModels
{
    [Table("Pharmacy")]
    public class dboPharmacy : IDbEntity
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public decimal  Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

    }
}
