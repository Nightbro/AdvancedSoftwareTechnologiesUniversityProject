using System.Collections.Generic;

namespace ViewModels.ViewModels
{
    public class RoleViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ClaimViewModel> Claims { get; set; }
    }
}
