using Shop.Domain.Models.Dtos.Base;
using System;

namespace Shop.Domain.Models.Dtos.User
{
    public class UserProfileDto : BaseEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
    }
}
