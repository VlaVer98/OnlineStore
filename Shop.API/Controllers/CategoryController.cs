using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Category;
using Shop.WEB.Models.ViewModels;
using Shop.Domain.Contracts.Services.Response;
using AutoMapper;
using Shop.WEB.Models.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceProvider _services;

        public CategoryController(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CategoryDto> categoryDtos = _services.GetService<ICategoryService>()
                .GetAll();
            APIResponseModel<IEnumerable<CategoryDto>> response =
                new APIResponseModel<IEnumerable<CategoryDto>>(true, null, categoryDtos);
            return new ObjectResult(response);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            CategoryDto category = _services.GetService<ICategoryService>()
                .Get(id);
            APIResponseModel<CategoryDto> response =
                new APIResponseModel<CategoryDto>(true, null, category);
            return new ObjectResult(response);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    new APIResponseModel<CategoryDto>(false, ModelState));

            CategoryDto categoryDto = _services.GetService<IMapper>()
                .Map<CategoryDto>(categoryVM);
            ServiceResponse serviceResponse = _services.GetService<ICategoryService>()
                .Create(categoryDto);

            if (!serviceResponse.IsSuccessful)
                return BadRequest(
                    new APIResponseModel<CategoryDto>(false, serviceResponse.AllMessages));
                
            return Ok(new APIResponseModel<CategoryDto>(true, serviceResponse.AllMessages));
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    new APIResponseModel<CategoryDto>(false, ModelState));

            CategoryDto categoryDto = _services.GetService<IMapper>().Map<CategoryDto>(categoryVM);
            ServiceResponse serviceResponse = _services.GetService<ICategoryService>().Update(categoryDto);

            if (!serviceResponse.IsSuccessful)
                return BadRequest(
                    new APIResponseModel<CategoryDto>(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel<CategoryDto>(true, serviceResponse.AllMessages));
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            ServiceResponse serviceResponse = _services.GetService<ICategoryService>().Delete(id);
            if (!serviceResponse.IsSuccessful)
                return BadRequest(
                    new APIResponseModel<CategoryDto>(false, serviceResponse.AllMessages));

            return Ok(new APIResponseModel<CategoryDto>(true, serviceResponse.AllMessages));
        }
    }
}
