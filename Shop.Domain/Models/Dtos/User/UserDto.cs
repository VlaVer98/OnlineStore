using Shop.Domain.Models.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models.Dtos.User
{
    public class UserDto : BaseEntityDto<Guid>
    {
        public virtual string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserProfileDto Profile { get; set; }
    }
}
