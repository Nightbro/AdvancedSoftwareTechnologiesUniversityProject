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
    public class RoleQuery : BaseQuery<dboRole>
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        public RoleQuery(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public dboRole Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                string sql = "SELECT * FROM [dbo].[Role] WHERE ID=" + id;
                var result = db.Query<dboRole>(sql);
                return result.FirstOrDefault();

            }
        }

        public new List<dboRole> GetAll()
        {
            return base.GetAll();
        }

        public dboRole Add(dboRole role)
        {
            string sql = @"INSERT INTO [dbo].[Role] (Name) Values (@Name);";
            string sqlLatest = "SELECT * FROM [dbo].[Role] ORDER BY ID DESC";

            using (var connection = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                var affectedRows = connection.Execute(sql, new { Name = role.Name });
                var result = connection.Query<dboRole>(sqlLatest);
                return result.FirstOrDefault();

            }
        }

        public new dboRole Update(dboRole role)
        {
            string sql = @"UPDATE [dbo].[Role] SET Name=@Name WHERE ID=@Id";
            string sqlLatest = "SELECT * FROM [dbo].[Role] WHERE ID=@Id";

            using (var connection = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                var affectedRows = connection.Execute(sql, new { Name = role.Name, ID = role.ID });
                var result = connection.Query<dboRole>(sqlLatest, new { Id = role.ID });
                return result.FirstOrDefault();

            }
        }

        public bool Delete(int ID)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString(Connectionstring)))
            {
                var sqlStatement = "DELETE FROM [dbo].[Role] WHERE ID = @Id";
                var result = connection.Execute(sqlStatement, new { Id = ID });
                return true;
            }
        }
    }
}
