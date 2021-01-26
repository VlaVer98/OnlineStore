using Shop.Domain.Models.Entities.Base;
using Shop.Domain.Models.Identity;
using System;

namespace Shop.Domain.Models.Entities
{
    class UserProfile : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        //Navigation
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
