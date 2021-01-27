using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories.Base;
using Shop.Domain.Contracts.Repositories;
using Shop.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.DB.Repositories
{
    public class OrderRepository : BaseRepository<Guid, Order>, IOrderRepository
    {
        public OrderRepository(ShopDbContext dbContext)
            : base(dbContext) {}
    }
}
