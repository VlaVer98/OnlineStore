using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Product;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.ViewModels;
using AutoMapper;

namespace Shop.WEB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceProvider _services;

        public ProductController(IServiceProvider services)
        {
            _services = services;
        }

        public IActionResult All()
        {
            IEnumerable<ProductDto> productsDto = _services.GetService<IProductService>()
                .GetAll();
            IEnumerable<ProductViewModel> productsVM = _services.GetService<IMapper>()
                .Map<IEnumerable<ProductViewModel>>(productsDto);
            return View(productsVM);
        }

        public IActionResult GetByCategory(Guid id)
        {
            IEnumerable<ProductDto> productsDto = _services.GetService<IProductService>()
                .GetAllByCategory(id);
            IEnumerable<ProductViewModel> productsVM = _services.GetService<IMapper>()
                .Map<IEnumerable<ProductViewModel>>(productsDto);
            return View(productsVM);
        }

        public IActionResult Details(Guid id)
        {
            ProductDto productDto = _services.GetService<IProductService>()
                .Get(id, true);
            if (productDto == null)
                return BadRequest();

            ProductViewModel productVM = _services.GetService<IMapper>()
                .Map<ProductViewModel>(productDto);
            return View(productVM);
        }
    }
}
