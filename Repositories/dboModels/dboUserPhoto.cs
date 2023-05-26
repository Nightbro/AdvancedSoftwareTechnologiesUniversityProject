using Dapper.Contrib.Extensions;

namespace Repositories.dboModels
{
    [Table("UserPhotos")]
    public class dboUserPhoto : IDbEntity
    {
        [ExplicitKey]
        public int UserID { get; set; }
        public string ImageName { get; set; }
        public string OriginalFormat { get; set; }
        public byte[] ImageFile { get; set; }
    }
}
