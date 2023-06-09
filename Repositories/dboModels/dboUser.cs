﻿using Dapper.Contrib.Extensions;

namespace Repositories.dboModels
{
    [Table("Users")]
    public class dboUser : IDbEntity
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
    }
}
