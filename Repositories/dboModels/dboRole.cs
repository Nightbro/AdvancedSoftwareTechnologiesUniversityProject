using Dapper.Contrib.Extensions;
using System;

namespace Repositories.dboModels
{
    [Table("Role")]
    public class dboRole : IDbEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}
