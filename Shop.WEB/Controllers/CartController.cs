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
using System.Text.Json;

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
            List<ProductInCartDto> ProductsInCart = GetProductsFromSession();
            ServiceResponse<CartDto> serviceResponse = _services.GetService<ICartService>()
                .CountCart(ProductsInCart);

            CartViewModel cartViewModel = null;
            if (serviceResponse.IsSuccessful)
                cartViewModel = _services.GetService<IMapper>()
                    .Map<CartViewModel>(serviceResponse.ResponseObject);

            return View(cartViewModel);
        }

        [HttpPost]
        public IActionResult AddProduct(Guid id)
        {
            List<ProductInCartDto> productsInCart = GetProductsFromSession();
            var serviceResponse = _services.GetService<ICartService>()
                .AddProduct(productsInCart, id);
            if (!serviceResponse.IsSuccessful)
            {
                
            }
            AddProductsToSession(serviceResponse.ResponseObject);

            return RedirectToAction("index");
        }

        private List<ProductInCartDto> GetProductsFromSession()
        {
            return HttpContext.Session.Get<List<ProductInCartDto>>("ProductsInCart");
        }
        private void AddProductsToSession(ICollection<ProductInCartDto> products)
        {
            HttpContext.Session.Set<ICollection<ProductInCartDto>>
                ("ProductsInCart", products);
        }
    }
}
