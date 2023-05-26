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
    public class ClaimQuery : BaseQuery<dboClaim>
    {
        public ClaimQuery(IConfiguration config) :base(config)
        {
        }
        public dboClaim Get(int id)
        {
            return base.Get(id);
        }


        public new List<dboClaim> GetAll()
        {
            return base.GetAll();
        }

    }
}
