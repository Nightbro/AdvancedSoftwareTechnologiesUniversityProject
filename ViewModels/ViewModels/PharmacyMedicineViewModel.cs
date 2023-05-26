using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.ViewModels
{
    public class PharmacyMedicineViewModel : IBaseViewModel
    {
        public Int64 IdMedicine { get; set; }
        public int IdPharmacy { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
