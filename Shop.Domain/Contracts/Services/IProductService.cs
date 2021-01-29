using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Models.Dtos.Product;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface IProductService : IService
    {
        public IEnumerable<ProductDto> GetAll();
    }
}
