using Shop.Domain.Contracts.Repositories.Base;
using Shop.Domain.Models.Entities;
using System;

namespace Shop.Domain.Contracts.Repositories
{
    public interface IOrderRepository : IBaseRepository<Guid, Order>
    {
    }
}
