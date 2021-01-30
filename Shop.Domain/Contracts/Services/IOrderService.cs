using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Models.Dtos.Order;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface IOrderService : IService
    {
        public IEnumerable<OrderDto> GetAll();
    }
}
