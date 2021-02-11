using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Constants;
using Shop.WEB.Controllers.Base;
using System;

namespace Shop.WEB.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class BaseAdminController : BaseController
    {
        public BaseAdminController(IServiceProvider services)
            : base(services) { }
    }
}
