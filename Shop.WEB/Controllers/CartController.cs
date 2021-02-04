using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Shop.Domain.Models.Dtos.Cart;
using Shop.Common.Extensions;
using Shop.Domain.Contracts.Services.Response;
using Shop.WEB.Models.ViewModels;
using AutoMapper;

namespace Shop.WEB.Controllers
{
    public class CartController : Controller
    {
        private readonly IServiceProvider _services;

        public CartController(IServiceProvider services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            List<ProductInCartDto> ProductsInCart = _services.GetService<ISession>()
                .Get<List<ProductInCartDto>>("ProductsInCart");
            ServiceResponse<CartDto> serviceResponse = _services.GetService<ICartService>()
                .CountCart(ProductsInCart);

            CartViewModel cartViewModel;
            if (serviceResponse.IsSuccessful)
                cartViewModel = _services.GetService<IMapper>()
                    .Map<CartViewModel>(serviceResponse.ResponseObject);
            else
                cartViewModel = new CartViewModel { TotalSum = 0 };

            return View(cartViewModel);
        }
    }
}
