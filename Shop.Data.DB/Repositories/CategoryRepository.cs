using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories.Base;
using Shop.Domain.Contracts.Repositories;
using Shop.Domain.Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Data.DB.Repositories
{
    public class CategoryRepository : BaseRepository<Guid, Category>, ICategoryRepository
    {
        public CategoryRepository(ShopDbContext dbContext)
            : base (dbContext) {}

        public Category GetByTitle(string name)
        {
            Expression<Func<Category, bool>> predicate =
                x => x.Title == name;
            return FirstOrDefault(DbSet, predicate);
        }
    }
}
