namespace ViewModels.ViewModels
{

    public class UserViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName { 
            get
            {
                return FirstName + " " + LastName;
            } 
        }
        public string Email { get; set; }
        public RoleViewModel Role { get; set; }
        public int RoleID { get; set; }


    }
}
