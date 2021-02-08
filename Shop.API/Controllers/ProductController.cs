using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Product;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shop.WEB.Models.Models.Response;
using Shop.WEB.Models.ViewModels;
using AutoMapper;
using Shop.Domain.Contracts.Services.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public ProductController(IServiceProvider services)
        {
            _services = services;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ProductDto> productDtos = _services.GetService<IProductService>()
                .GetAll();

            var response = new APIResponseModel<IEnumerable<ProductDto>>
                (true, null, productDtos);
            return Ok(response);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            ProductDto productDto = _services.GetService<IProductService>()
                .Get(id);
            if (productDto == null)
                return NotFound(new APIResponseModel<ProductDto>(false, 
                    new List<string> { "Product not found"}));

            var response = new APIResponseModel<ProductDto>(true, null, productDto);
            return Ok(response);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post(ProductCreateViewModel productCreateVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(new APIResponseModel(false, ModelState));

            ProductDto productDto = _services.GetService<IMapper>()
                .Map<ProductDto>(productCreateVM);
            ServiceResponse serviceResponse = _services.GetService<IProductService>()
                .Create(productDto);

            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel(true, serviceResponse.AllMessages));
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(ProductCreateViewModel productCreateVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(new APIResponseModel(false, ModelState));

            ProductDto productDto = _services.GetService<IMapper>().Map<ProductDto>(productCreateVM);
            if(productDto == null)
                return BadRequest(new APIResponseModel(false, 
                    new List<string>{ "Product not found" }));

            ServiceResponse serviceResponse = _services.GetService<IProductService>().Update(productDto);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel(true, serviceResponse.AllMessages));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            ServiceResponse serviceResponse = _services.GetService<IProductService>().Delete(id);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(new APIResponseModel(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel(true, serviceResponse.AllMessages));
        }
    }
}
