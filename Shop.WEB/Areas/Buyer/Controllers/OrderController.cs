using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Order;
using Shop.WEB.Areas.Buyer.Controllers.Base;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.ViewModels;
using AutoMapper;

namespace Shop.WEB.Areas.Buyer.Controllers
{
    public class OrderController : BaseBuyerController
    {
        public OrderController(IServiceProvider service)
            : base(service) { }

        public IActionResult Index()
        {
            IEnumerable<OrderDto> ordersDto = _services.GetService<IOrderService>()
                .GetAllForUser(GetNameIdentifier());
            IEnumerable<OrderViewModel> orderVM = _services.GetService<IMapper>()
                .Map<IEnumerable<OrderViewModel>>(ordersDto);
            return View(orderVM);
        }
    }
}
