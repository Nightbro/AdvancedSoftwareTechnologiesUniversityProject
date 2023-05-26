using Dapper;
using Microsoft.Extensions.Configuration;
using Repositories.dboModels;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Repositories.Query
{
    public class UserPasswordQuery : BaseQuery<dboUserPassword>
    {
        public UserPasswordQuery(IConfiguration config) :base(config)
        {
        }


        public new void Create(dboUserPassword entry)
        {
            string sql = "INSERT INTO [dbo].[UserPassword] (Username, Password) VALUES ('" + entry.Username + "', CONVERT(VARBINARY, '" + entry.Password + "'))";
            base.PerformQuery(sql);
        }
        public new void Update(dboUserPassword entry)
        {
            string sql = "UPDATE [dbo].[UserPassword] SET Password = CONVERT(VARBINARY, '" + entry.Password + "') WHERE Username='" + entry.Username + "'" ;
            base.PerformQuery(sql);
        }
        public void DeleteById(string username)
        {
            string sql = "DELETE FROM [dbo].[UserPassword] WHERE Username='" + username + "'";
            base.PerformQuery(sql);
        }

        public bool AuthorizeUser(dboUserPassword entry)
        {
            string sql = "Select Username from [dbo].[UserPassword] where [Username]='" + entry.Username + "' and [Password]=CONVERT(varbinary,'" + entry.Password + "')";
            var output = base.Select(sql);
            if (output != null && output.Count() == 1) return true;
            return false;

        }
    }
}
