using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Models.Dtos.Category;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface ICategoryService : IService
    {
        public IEnumerable<CategoryDto> GetAll();
    }
}
