using Microsoft.AspNetCore.Mvc;
using Shop.WEB.Areas.Admin.Controllers.Base;
using System;

namespace Shop.WEB.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        public CategoryController(IServiceProvider services)
            : base (services) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
