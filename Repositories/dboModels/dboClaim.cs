using Dapper.Contrib.Extensions;

namespace Repositories.dboModels
{
    [Table("Claim")]
    public class dboClaim : IDbEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
