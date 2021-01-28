using Microsoft.AspNetCore.Mvc;
using System;

namespace Shop.WEB.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
        protected readonly IServiceProvider _services;
        public BaseAdminController(IServiceProvider services)
        {
            _services = services;
        }
    }
}
