using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Product;
using Shop.WEB.Areas.Admin.Controllers.Base;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.ViewModels;
using AutoMapper;
using Shop.Domain.Models.Dtos.Category;
using Shop.Domain.Contracts.Services.Response;

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

        public IActionResult Create()
        {
            var productCreateVM = new ProductCreateViewModel
            {
                AllCategory = GetAllCategory()
            };

            return View(productCreateVM);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel productCreateVM)
        {
            if (ModelState.IsValid)
            {
                ProductDto productDto = _services.GetService<IMapper>().Map<ProductDto>(productCreateVM);
                ServiceResponse serviceResponse = _services.GetService<IProductService>().Create(productDto);

                if (serviceResponse.IsSuccessful)
                {
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", serviceResponse.Message);
            }

            productCreateVM.AllCategory = GetAllCategory();
            return View(productCreateVM);
        }

        private List<СategoryTitleAndIdViewModel> GetAllCategory()
        {
            IEnumerable<CategoryDto> categoriesDto = _services.GetService<ICategoryService>().GetAll();
            return _services.GetService<IMapper>()
                .Map<List<СategoryTitleAndIdViewModel>>(categoriesDto);
        }
    }
}
