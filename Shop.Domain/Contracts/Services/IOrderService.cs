using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Enums;
using Shop.Domain.Models.Dtos.Order;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface IOrderService : IService
    {
        public IEnumerable<OrderDto> GetAll();
        public ServiceResponse ChangeStatus(Guid orderId, OrderStatus orderStatus);
        public OrderDto GetWithAllRelations(Guid id);
        public ServiceResponse Delete(Guid id);
        public IEnumerable<OrderDto> GetAllForUser(Guid userId);
        public OrderDto GetForUser(Guid userId, Guid orderId);
    }
}
