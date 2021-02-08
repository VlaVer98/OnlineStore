using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.WEB.Models.Models.Response;
using Shop.Domain.Models.Dtos.Cart;
using Shop.Domain.Models.Dtos.Order;
using Shop.API.Controllers.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        public CartController(IServiceProvider services)
            : base(services) { }

        // POST: api/<CartController>/CountCart
        [HttpPost]
        public IActionResult CountCart(ICollection<ProductInCartDto> productInCartDtos)
        {
            ServiceResponse<CartDto> serviceResponse = _services.GetService<ICartService>()
                .CountCart(productInCartDtos);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel<CartDto>(true,
                serviceResponse.AllMessages, serviceResponse.ResponseObject));
        }

        // POST api/<CartController>/AddProduct/id
        [HttpPost("{id}")]
        public IActionResult AddProduct(Guid id, ICollection<ProductInCartDto> productInCartDtos)
        {
            var serviceResponse = _services.GetService<ICartService>()
                .AddProduct(productInCartDtos, id);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel<ICollection<ProductInCartDto>>(true,
                serviceResponse.AllMessages, serviceResponse.ResponseObject));
        }

        // POST api/<CartController>/DeleteProduct/id
        [HttpPost("{id}")]
        public IActionResult DeleteProduct(Guid id, ICollection<ProductInCartDto> productInCartDtos)
        {
            var serviceResponse = _services.GetService<ICartService>()
                .DeleteProduct(productInCartDtos, id);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel<ICollection<ProductInCartDto>>(true,
                serviceResponse.AllMessages, serviceResponse.ResponseObject));
        }

        // POST api/<CartController>/MakeOrder
        [HttpPost("{id}")]
        public IActionResult MakeOrder(ICollection<ProductInCartDto> productInCartDtos)
        {
            var serviceResponse = _services.GetService<ICartService>()
                .MakeOrder(GetNameIdentifier(), productInCartDtos);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel<OrderDto>(true,
                serviceResponse.AllMessages, serviceResponse.ResponseObject));
        }
    }
}
