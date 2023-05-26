using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserPhoto
    {
        public int UserID { get; set; }
        public string ImageName { get; set; }
        public string OriginalFormat { get; set; }
        public byte[] ImageFile { get; set; }
    }
}
