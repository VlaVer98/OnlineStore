using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Shop.WEB.Models;
using System.Diagnostics;

namespace Shop.WEB.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IServiceProvider _services;

        public BaseController(IServiceProvider services)
        {
            _services = services;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected Guid GetNameIdentifier()
        {
            return User.FindFirstValue("sub") != null ?
                new Guid(User.FindFirstValue("sub"))
                : Guid.Empty;
        }
    }
}
