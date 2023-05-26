namespace ViewModels.ViewModels
{

    public class UserPhotoViewModel : IBaseViewModel
    {
        public int UserId { get; set; }
        public string ImageName { get; set; }
        public string OriginalFormat { get; set; }
        public byte[] ImageFile { get; set; }
    }
}
