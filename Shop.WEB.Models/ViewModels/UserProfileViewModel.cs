using Shop.WEB.Models.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels
{
    public class UserProfileViewModel : BaseViewModel<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Address { get; set; }
        public string GetFullName()
        {
            return $"{Lastname} {Name} {Patronymic}";
        }
    }
}
