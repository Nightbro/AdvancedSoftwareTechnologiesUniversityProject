using Dapper.Contrib.Extensions;
using System;

namespace Repositories.dboModels
{
    [Table("Medicine")]
    public class dboMedicine : IDbEntity
    {
        [ExplicitKey]
        public Int64 Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string ImageName { get; set; }
        public string OriginalFormat { get; set; }
        public byte[] ImageFile { get; set; }
    }
}
