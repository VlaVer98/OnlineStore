using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shop.Domain.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Domain.Contracts.Repositories.Base
{
    public interface IBaseRepository<TKey, TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity AddOrUpdate(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
        void DeleteRange(IEnumerable<TKey> ids);
        EntityEntry<TEntity> GetEntry(TEntity entity);
        TEntity GetById(TKey id, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null, bool localOnly = false);
        IQueryable<TEntity> Get(IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null);
        void Load(Expression<Func<TEntity, bool>> condition = null);
    }
}
