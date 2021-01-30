using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Order;
using Shop.WEB.Areas.Admin.Controllers.Base;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Shop.WEB.Models.ViewModels;
using AutoMapper;

namespace Shop.WEB.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        public OrderController(IServiceProvider services)
            : base(services) { }

        public ActionResult Index()
        {
            IEnumerable<OrderDto> ordersDto = _services.GetService<IOrderService>().GetAll();
            IEnumerable<OrderViewModel> orderVm = _services.GetService<IMapper>()
                .Map<IEnumerable<OrderViewModel>>(ordersDto);

            return View(orderVm);
        }
    }
}
