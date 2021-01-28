using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Shop.Domain.Models.Dtos.Category;
using AutoMapper;
using Shop.WEB.Models.ViewModels;

namespace Shop.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        public CategoryController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IActionResult Index()
        {
            IEnumerable<CategoryDto> categoriesDTO = _serviceProvider.GetService<ICategoryService>().GetAll();
            IEnumerable<CategoryViewModel> categoriesVM = _serviceProvider.GetService<IMapper>()
                .Map<IEnumerable<CategoryViewModel>>(categoriesDTO);

            return View(categoriesVM);
        }
    }
}
