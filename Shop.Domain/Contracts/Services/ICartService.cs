using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Cart;
using Shop.Domain.Models.Dtos.Order;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface ICartService : IService
    {
        public ServiceResponse<ICollection<ProductInCartDto>> AddProduct(
            ICollection<ProductInCartDto> productsDto, Guid idProduct);
        public ServiceResponse<ICollection<ProductInCartDto>> DeleteProduct(
             ICollection<ProductInCartDto> productsDto, Guid idProduct);
        public ServiceResponse<CartDto> CountCart(
            ICollection<ProductInCartDto> productsDto);
        public ServiceResponse<OrderDto> MakeOrder(Guid userId,
             ICollection<ProductInCartDto> productsDto);
    }
}
