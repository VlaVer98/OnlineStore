using Microsoft.AspNetCore.Mvc;
using Shop.API.Controllers.Base;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Order;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.Models.Response;
using Shop.Domain.Contracts.Services.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        public OrderController(IServiceProvider services)
            : base(services) { }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<OrderDto> ordersDto = _services.GetService<IOrderService>()
                .GetAllForUser(GetNameIdentifier());
            var response = new APIResponseModel<IEnumerable<OrderDto>>(
                true, null, ordersDto);
            return Ok(response);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            OrderDto orderDto = _services.GetService<IOrderService>()
                .GetForUser(GetNameIdentifier(), id);
            if (orderDto == null)
                return BadRequest(new APIResponseModel(false));

            var response = new APIResponseModel<OrderDto>(true, null, orderDto);
            return Ok(response);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            ServiceResponse serviceResponse = _services.GetService<IOrderService>()
                .Delete(id);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            var response = new APIResponseModel<OrderDto>(true, serviceResponse.AllMessages);
            return Ok(response);
        }
    }
}
