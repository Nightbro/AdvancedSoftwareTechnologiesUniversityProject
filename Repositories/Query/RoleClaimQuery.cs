using Dapper;
using Microsoft.Extensions.Configuration;
using Repositories.dboModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Repositories.Query
{
    public class RoleClaimQuery : BaseQuery<dboRoleClaim>
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        public RoleClaimQuery(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public List<dboRoleClaim> Get(int roleID)
        {
            string sql = "SELECT * FROM [dbo].[RoleClaim] WHERE RoleID=" + roleID;
            return Select(sql).ToList();

        }


        public new int Create(dboRoleClaim roleClaim)
        {
            return base.Create(roleClaim);
        }
        public new void Delete(dboRoleClaim roleClaim)
        {
            base.Delete(roleClaim);
            //using (var connection = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            //{
            //    var sqlStatement = "DELETE FROM [dbo].[RoleClaim] WHERE RoleID = "+roleClaim.RoleID+" AND ClaimID=" + roleClaim.ClaimID;
            //    var result = connection.Execute(sqlStatement);
            //}
        }
        


    }
}
