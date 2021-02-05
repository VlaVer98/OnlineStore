using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Constants;
using Shop.WEB.Models;
using System;
using System.Diagnostics;

namespace Shop.WEB.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public class BaseAdminController : Controller
    {
        protected readonly IServiceProvider _services;
        public BaseAdminController(IServiceProvider services)
        {
            _services = services;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
