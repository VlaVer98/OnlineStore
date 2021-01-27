using Microsoft.EntityFrameworkCore;
using Shop.Data.DB.Context;
using Shop.Domain.Contracts.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Shop.Data.DB.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class 
    {
        protected ShopDbContext DbContext { get; private set; }
        protected DbSet<TEntity> DbSet { get; set; }

        public Repository(ShopDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException("dbContext");
            DbSet = DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }
    }
}
