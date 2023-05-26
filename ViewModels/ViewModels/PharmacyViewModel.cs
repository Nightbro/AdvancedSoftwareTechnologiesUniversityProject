using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.ViewModels
{
    public class PharmacyViewModel : IBaseViewModel
    {
           
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PharmacyMedicineViewModel> Medicines { get; set; }
    }
}
