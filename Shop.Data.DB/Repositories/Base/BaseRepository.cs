using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shop.Data.DB.Context;
using Shop.Domain.Contracts.Repositories.Base;
using Shop.Domain.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Data.DB.Repositories.Base
{
    public class BaseRepository<TKey, TEntity> : Repository<TEntity>, IBaseRepository<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        public BaseRepository(ShopDbContext dbContext)
            : base(dbContext) { }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            if (entity.Id.Equals(default(TKey)))
            {
                entity.Id = default(TKey);
                Add(entity);
            }
            else
            {
                Update(entity);
            }

            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void Delete(TKey id)
        {
            TEntity entity = GetById(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public EntityEntry<TEntity> GetEntry(TEntity entity)
        {
            return DbContext.Entry(entity);
        }

        public virtual void DeleteRange(IEnumerable<TKey> ids)
        {
            if (ids == null || ids.Count() == 0)
            {
                return;
            }

            foreach (TKey id in ids)
            {
                Delete(id);
            }
        }

        public System.Linq.IQueryable<TEntity> Get(IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            IQueryable<TEntity> query = GetAll(includeProperties);
            return query;
        }

        public TEntity GetById(TKey id, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null, bool localOnly = false)
        {
            Expression<Func<TEntity, bool>> predicate =
                x => x.Id.Equals(id);

            return GetByExpression(predicate, includeProperties, localOnly);
        }

        protected TEntity GetByExpression(Expression<Func<TEntity, bool>> predicate, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null, bool localOnly = false)
        {
            TEntity entity = FirstOrDefault(LocalSet, predicate, includeProperties);
            if (entity != null && localOnly)
            {
                return entity;
            }

            return FirstOrDefault(DbSet, predicate, includeProperties);
        }

        protected TEntity FirstOrDefault(IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            IQueryable<TEntity> query = source.AsQueryable();
            //query = query.Where(predicate);

            if (includeProperties != null)
            {
                //int counter = 0;
                //var includesToList = includeProperties.ToList();
                //for (int i = 0; i < includesToList.Count(); i++) {
                //    counter++;
                //    query = query.Include(includesToList[i]);

                //    if (counter == 5) {
                //        counter = 0;
                //        query.ToList();
                //    }
                //}

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault(predicate);
        }

        protected IQueryable<TEntity> LocalSet
        {
            get { return DbSet.Local.AsQueryable(); }
        }

        public void Load(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query.Load();
        }
    }
}
