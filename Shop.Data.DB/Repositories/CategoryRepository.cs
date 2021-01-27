using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories.Base;
using Shop.Domain.Contracts.Repositories;
using Shop.Domain.Models.Entities;
using System;

namespace Shop.Data.DB.Repositories
{
    public class CategoryRepository : BaseRepository<Guid, Category>, ICategoryRepository
    {
        public CategoryRepository(ShopDbContext dbContext)
            : base (dbContext) {}
    }
}
