using Dapper.Contrib.Extensions;

namespace Repositories.dboModels
{
    [Table("RoleClaim")]
    public class dboRoleClaim : IDbEntity
    {
        [ExplicitKey]
        public int RoleID { get; set; }
        [ExplicitKey]
        public int ClaimID { get; set; }
    }
}
