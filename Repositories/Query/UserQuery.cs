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
    public class UserQuery : BaseQuery<dboUser>
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        public UserQuery(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public dboUser Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                string sql = "SELECT * FROM [dbo].[Users] WHERE ID=" + id;
                var result = db.Query<dboUser>(sql);
                return result.FirstOrDefault();

            }
        }

        public dboUser GetByUsername(string username)
        {
            string sql = "SELECT * FROM [dbo].[Users] WHERE Username='" + username + "'";
            return base.Select(sql).FirstOrDefault();

        }

        public new List<dboUser> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                string sql = "SELECT * FROM [dbo].[Users]";
                var result = db.Query<dboUser>(sql);
                return result.ToList();

            }
        }
        public new int Create(dboUser user)
        {
            return base.Create(user);
        }

        public new void Update(dboUser user)
        {
            base.Update(user);
        }


        public void Delete(int ID)
        {
            
            using (var connection = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                var sqlStatement = "DELETE FROM [dbo].[Users] WHERE ID = @Id";
                var result = connection.Execute(sqlStatement, new { Id = ID });
            }
        }

        public dboUser AuthorizeUser(string username, string password)
        {


            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                string sql = "Select * from [dbo].[Users] where [Username]='" + username + "' and [Password]=CONVERT(varbinary,'" + password + "')";
                var result = db.Query<dboUser>(sql);
                return result.FirstOrDefault();
            }

        }
    }
}
