using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.WEB.Areas.Buyer.Controllers.Base
{
    [Area("Buyer")]
    public class BaseBuyerController : Controller
    {
        protected readonly IServiceProvider _services;

        public BaseBuyerController(IServiceProvider service)
        {
            _services = service;
        }
    }
}
