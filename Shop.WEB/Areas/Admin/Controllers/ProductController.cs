using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Product;
using Shop.WEB.Areas.Admin.Controllers.Base;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.ViewModels;
using AutoMapper;

namespace Shop.WEB.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        public ProductController(IServiceProvider services)
            : base(services) { }

        public IActionResult Index()
        {
            IEnumerable<ProductDto> productsDto = _services.GetService<IProductService>().GetAll();
            IEnumerable<ProductViewModel> productsVM = _services.GetService<IMapper>()
                .Map<IEnumerable<ProductViewModel>>(productsDto);

            return View(productsVM);
        }
    }
}
