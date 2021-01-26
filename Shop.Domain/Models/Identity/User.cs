using Microsoft.AspNetCore.Identity;
using Shop.Domain.Models.Entities;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Models.Identity
{
    public class User : IdentityUser<Guid>
    {
        //Navigation
        public UserProfile Profile { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
