using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Constants;
using Shop.WEB.Controllers.Base;
using System;
using System.Security.Claims;

namespace Shop.WEB.Areas.Buyer.Controllers.Base
{
    [Area("Buyer")]
    [Authorize(Roles = UserRoles.Buyer)]
    public class BaseBuyerController : BaseController
    {
        public BaseBuyerController(IServiceProvider services)
            : base(services) { }
    }
}
