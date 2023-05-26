using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{


    public class PharmacyMedicine
    {
        public Int64 IdMedicine { get; set; }
        public int IdPharmacy { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
