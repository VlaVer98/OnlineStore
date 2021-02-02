using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class UserViewModel : BaseViewModel<Guid>
    {
        public virtual string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserProfileViewModel Profile { get; set; }
    }
}
