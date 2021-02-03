using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Product;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface IProductService : IService
    {
        public IEnumerable<ProductDto> GetAll();
        public ProductDto Get(Guid id, bool withAllInclude = false);
        public ServiceResponse Create(ProductDto productDto);
        public ServiceResponse Update(ProductDto productDto);
        public ServiceResponse Delete(Guid id);
        public IEnumerable<ProductDto> GetAllByCategory(Guid categoryId);
    }
}
