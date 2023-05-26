using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Interfaces.Repository;
using Models;
using Repositories.dboModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Repositories.Query
{
    public class BaseQuery<T> where T: class, IDbEntity
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString(Connectionstring));
        public BaseQuery(IConfiguration config)
        {
            _config = config;
        }
        public IEnumerable<T> Select(string query)
        {
            using (IDbConnection conn = Connection)
            {
                return conn.QueryAsync<T>(query).Result;
            }
        }

        public bool ExecuteScalar(string query)
        {
            using (IDbConnection conn = Connection)
            {
                return conn.ExecuteScalar<bool>(query);
            }
        }
        public void PerformQuery(string query)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Execute(query);
            }
        }

        public List<T> GetAll()
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    return conn.GetAll<T>().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Get(object id)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    return conn.Get<T>(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T record)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Update(record);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public int Create(T record)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    return (int)conn.Insert(record);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(T record)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Delete(record);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
