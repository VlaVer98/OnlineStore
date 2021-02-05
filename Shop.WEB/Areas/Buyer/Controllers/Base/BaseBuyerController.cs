using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Constants;
using System;
using System.Security.Claims;

namespace Shop.WEB.Areas.Buyer.Controllers.Base
{
    [Area("Buyer")]
    [Authorize(Roles = UserRoles.Buyer)]
    public class BaseBuyerController : Controller
    {
        protected readonly IServiceProvider _services;

        public BaseBuyerController(IServiceProvider service)
        {
            _services = service;
        }

        protected Guid GetNameIdentifier()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) != null ?
                new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))
                : Guid.Empty;
        }
    }
}
