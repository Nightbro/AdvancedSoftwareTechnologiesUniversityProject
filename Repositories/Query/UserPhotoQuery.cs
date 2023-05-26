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
    public class UserPhotoQuery : BaseQuery<dboUserPhoto>
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        public UserPhotoQuery(IConfiguration config): base(config)
        {
            _config = config;
        }

        public dboUserPhoto Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                string sql = "SELECT * FROM [dbo].[UserPhotos] WHERE UserID=" + id;
                var result = db.Query<dboUserPhoto>(sql);
                return result.FirstOrDefault();

            }
        }

        public void Add(dboUserPhoto photo)
        {
            string sql = @"INSERT INTO [dbo].[UserPhotos] (UserID, ImageName, OriginalFormat, ImageFile) Values (@UserID, @ImageName, @OriginalFormat, @ImageFile);";
            using (var connection = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                var affectedRows = connection.Execute(sql, new { UserID = photo.UserID, ImageName = photo.ImageName, OriginalFormat = photo.OriginalFormat, ImageFile= photo.ImageFile });

            }
        }

        public new void Update(dboUserPhoto photo)
        {
            base.Update(photo);
        }
        public void DeleteById(int userId)
        {
            string sql = "DELETE FROM [dbo].[UserPhotos] WHERE UserID=" + userId;
            base.PerformQuery(sql);
        }
    }
}
