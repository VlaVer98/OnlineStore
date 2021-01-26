using System;
using Microsoft.AspNetCore.Identity;

namespace Shop.Domain.Models.Identity
{
    public class UserToken : IdentityUserToken<Guid>
    {
    }
}
