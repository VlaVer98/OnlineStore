using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Domain.Contracts.Repositories.Base
{
    public interface IRepository<TEntity>
    where TEntity : class
    {
        IQueryable<TEntity> GetAll(IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null);
    }
}
