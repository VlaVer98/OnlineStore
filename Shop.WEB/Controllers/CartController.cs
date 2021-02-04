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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Shop.WEB.Constants;

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
                //ToDo Display error
            }
            ChangeProductsToSession(serviceResponse.ResponseObject);

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult DeleteProduct(Guid id)
        {
            List<ProductInCartDto> productsInCart = GetProductsFromSession();
            var serviceResponse = _services.GetService<ICartService>()
                .DeleteProduct(productsInCart, id);
            if (!serviceResponse.IsSuccessful)
            {
                //ToDo Display error
            }
            ChangeProductsToSession(serviceResponse.ResponseObject);

            return RedirectToAction("index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult MakeOrder()
        {
            List<ProductInCartDto> productsInCart = GetProductsFromSession();
            var serviceResponse = _services.GetService<ICartService>()
                .MakeOrder(GetNameIdentifier(), productsInCart);
            if (!serviceResponse.IsSuccessful)
            {
                //ToDo Display a errors
                return RedirectToAction("index");
            }
            HttpContext.Session.Remove(SessionKeys.PRODUCTS_IN_CART);
            return RedirectToAction("Details", "Order", new { 
                area = "Buyer",
                id = serviceResponse.ResponseObject.Id
            });

        }
        protected Guid GetNameIdentifier()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) != null ?
                new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))
                : Guid.Empty;
        }

        private List<ProductInCartDto> GetProductsFromSession()
        {
            return HttpContext.Session.Get<List<ProductInCartDto>>(SessionKeys.PRODUCTS_IN_CART);
        }

        private void ChangeProductsToSession(ICollection<ProductInCartDto> products)
        {
            HttpContext.Session.Set<ICollection<ProductInCartDto>>
                (SessionKeys.PRODUCTS_IN_CART, products);
        }
    }
}
