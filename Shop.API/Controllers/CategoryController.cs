using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Category;
using Shop.WEB.Models.ViewModels;
using Shop.Domain.Contracts.Services.Response;
using AutoMapper;

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
            return new ObjectResult(categoryDtos);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            CategoryDto category = _services.GetService<ICategoryService>()
                .Get(id);
            return new ObjectResult(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            CategoryDto categoryDto = _services.GetService<IMapper>()
                .Map<CategoryDto>(categoryVM);
            ServiceResponse serviceResponse = _services.GetService<ICategoryService>()
                .Create(categoryDto);
            if (!serviceResponse.IsSuccessful)
            {
                foreach (var item in serviceResponse.AllMessages)
                    ModelState.AddModelError("", item);

                return BadRequest(ModelState.Values);
            }
                
            return Ok();
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            CategoryDto categoryDto = _services.GetService<IMapper>().Map<CategoryDto>(categoryVM);
            ServiceResponse serviceResponse = _services.GetService<ICategoryService>().Update(categoryDto);

            if (!serviceResponse.IsSuccessful)
            {
                foreach (var item in serviceResponse.AllMessages)
                    ModelState.AddModelError("", item);

                return BadRequest(ModelState.Values);
            }

            return Ok();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            ServiceResponse serviceResponse = _services.GetService<ICategoryService>().Delete(id);
            if (!serviceResponse.IsSuccessful)
            {
                foreach (var item in serviceResponse.AllMessages)
                    ModelState.AddModelError("", item);

                return BadRequest(ModelState.Values);
            }

            return Ok();
        }
    }
}
