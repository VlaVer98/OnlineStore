using Microsoft.AspNetCore.Mvc;
using Shop.WEB.Areas.Buyer.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.WEB.Areas.Buyer.Controllers
{
    public class ProfileController : BaseBuyerController
    {
        public ProfileController(IServiceProvider service)
            : base(service) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
