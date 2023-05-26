using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.ViewModels
{

    public class MedicineViewModel : IBaseViewModel
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string OriginalFormat { get; set; }
        public byte[] ImageFile { get; set; }
    }
}
