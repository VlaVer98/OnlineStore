using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class UserOrderViewModel : BaseViewModel<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
    }
}
