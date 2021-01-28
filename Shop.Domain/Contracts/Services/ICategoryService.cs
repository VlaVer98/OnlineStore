using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Category;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface ICategoryService : IService
    {
        public IEnumerable<CategoryDto> GetAll();
        public CategoryDto Get(Guid id);
        public ServiceResponse Update(CategoryDto categoryDto);
        public ServiceResponse Create(CategoryDto categoryDto);
        public ServiceResponse Delete(Guid id);
    }
}
