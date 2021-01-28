using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Category;
using Shop.WEB.Areas.Admin.Controllers.Base;
using Shop.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Contracts.Services.Response;

namespace Shop.WEB.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        public CategoryController(IServiceProvider services)
            : base (services) { }

        public IActionResult Index()
        {
            IEnumerable<CategoryDto> categoriesDTO = _services.GetService<ICategoryService>().GetAll();
            IEnumerable<CategoryViewModel> categoriesVM = _services.GetService<IMapper>()
                .Map<IEnumerable<CategoryViewModel>>(categoriesDTO);

            return View(categoriesVM);
        }

        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }
        
        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                CategoryDto categoryDto = _services.GetService<IMapper>().Map<CategoryDto>(categoryVM);
                ServiceResponse serviceResponse = _services.GetService<ICategoryService>().Create(categoryDto);

                if (serviceResponse.IsSuccessful == false)
                {
                    ModelState.AddModelError("", serviceResponse.Message);
                    return View(categoryVM);
                }

                return RedirectToAction("index");
            }

            return View(categoryVM);
        }

        public IActionResult Edit(Guid id)
        {
            CategoryDto categoryDto = _services.GetService<ICategoryService>().Get(id);
            CategoryViewModel categoryVM = _services.GetService<IMapper>().Map<CategoryViewModel>(categoryDto);

            if(categoryVM == null)
            {
                return BadRequest();
            }

            return View(categoryVM);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                CategoryDto categoryDto = _services.GetService<IMapper>().Map<CategoryDto>(categoryVM);
                ServiceResponse serviceResponse = _services.GetService<ICategoryService>().Update(categoryDto);

                if(serviceResponse.IsSuccessful == false)
                {
                    ModelState.AddModelError("", serviceResponse.Message);
                }
            }

            return View(categoryVM);
        }
    }
}
