using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Claim> Claims { get; set; }

    }
}
