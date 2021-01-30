using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories.Base;
using Shop.Domain.Contracts.Repositories;
using Shop.Domain.Models.Entities;
using System;

namespace Shop.Data.DB.Repositories
{
    public class OrderProductRepository : BaseRepository<Guid, OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(ShopDbContext dbContext)
            : base(dbContext) { }
    }
}
