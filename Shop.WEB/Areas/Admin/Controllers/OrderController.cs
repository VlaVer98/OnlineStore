using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Order;
using Shop.WEB.Areas.Admin.Controllers.Base;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Shop.WEB.Models.ViewModels;
using AutoMapper;
using Shop.Domain.Contracts.Services.Response;

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

        public IActionResult ChangeStatus(Guid id)
        {
            return View(new ChangingStatusOrderViewModel() { Id = id });
        }

        [HttpPost]
        public IActionResult ChangeStatus(ChangingStatusOrderViewModel changingStatus)
        {
            if (ModelState.IsValid)
            {
                ServiceResponse serviceResponse = _services.GetService<IOrderService>()
                    .ChangeStatus(changingStatus.Id, changingStatus.OrderStatus);
                if (!serviceResponse.IsSuccessful)
                {
                    ModelState.AddModelError("", serviceResponse.Message);
                }
            }

            return View(changingStatus);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(Guid id)
        {
            return View(new ServiceResponseViewModel { Id = id });
        }

        [HttpPost]
        public IActionResult Delete(ServiceResponseViewModel serviceResponseVM)
        {
            if (ModelState.IsValid)
            {
                ServiceResponse serviceResponse = _services.GetService<IOrderService>()
                    .Delete(serviceResponseVM.Id);
                if (serviceResponse.IsSuccessful)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", serviceResponse.Message);
            }
            return View(serviceResponseVM);
        }

        public IActionResult Details(Guid id)
        {
            OrderDto orderDto = _services.GetService<IOrderService>().GetWithAllRelations(id);
            OrderViewModel orderVM = _services.GetService<IMapper>()
                .Map<OrderViewModel>(orderDto);

            return View(orderVM);
        }
    }
}
