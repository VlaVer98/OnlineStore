using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class UserProfileViewModel : BaseViewModel<Guid>
    {
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string GetFullName()
        {
            return $"{Lastname} {Name} {Patronymic}";
        }
    }
}
