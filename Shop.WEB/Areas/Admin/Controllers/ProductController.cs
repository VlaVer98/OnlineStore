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

        public IActionResult Edit(Guid id)
        {
            ProductDto productDto = _services.GetService<IProductService>().Get(id);
            if(productDto == null)
            {
                return BadRequest();
            }

            ProductCreateViewModel productCreateVM = _services.GetService<IMapper>()
                .Map<ProductCreateViewModel>(productDto);
            productCreateVM.AllCategory = GetAllCategory();

            return View(productCreateVM);
        }

        public IActionResult Details(Guid id)
        {
            ProductDto productDto = _services.GetService<IProductService>().Get(id, true);
            if(productDto == null)
            {
                return BadRequest();
            }

            ProductViewModel productVM = _services.GetService<IMapper>().Map<ProductViewModel>(productDto);
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductCreateViewModel productCreateVM)
        {
            if (ModelState.IsValid)
            {
                ProductDto productDto = _services.GetService<IMapper>().Map<ProductDto>(productCreateVM);
                ServiceResponse serviceResponse = _services.GetService<IProductService>().Update(productDto);

                if (!serviceResponse.IsSuccessful)
                {
                    ModelState.AddModelError("", serviceResponse.Message);
                }
            }

            productCreateVM.AllCategory = GetAllCategory();
            return View(productCreateVM);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(ServiceResponseViewModel serviceResponseVM)
        {
            return View(serviceResponseVM);
        }

        [HttpPost]
        public IActionResult Delete(ServiceResponseViewModel serviceResponseVM)
        {
            ServiceResponse serviceResponse = _services.GetService<IProductService>().Delete(serviceResponseVM.Id);
            if (serviceResponse.IsSuccessful == false)
            {
                serviceResponseVM.Message = serviceResponse.Message;
                return View(serviceResponseVM);
            }

            return RedirectToAction("Index");
        }

        private List<СategoryTitleAndIdViewModel> GetAllCategory()
        {
            IEnumerable<CategoryDto> categoriesDto = _services.GetService<ICategoryService>().GetAll();
            return _services.GetService<IMapper>()
                .Map<List<СategoryTitleAndIdViewModel>>(categoriesDto);
        }
    }
}
