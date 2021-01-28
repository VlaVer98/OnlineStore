using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Category;
using Shop.WEB.Areas.Admin.Controllers.Base;
using Shop.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
