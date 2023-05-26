using Dapper.Contrib.Extensions;

namespace Repositories.dboModels
{
    [Table("UserPassword")]
    public class dboUserPassword : IDbEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
