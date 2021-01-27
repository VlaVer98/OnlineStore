using Microsoft.EntityFrameworkCore;
using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories.Base;
using Shop.Domain.Contracts.Repositories;
using Shop.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Data.DB.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShopDbContext dbContext)
            : base(dbContext) { }

        public User GetByEmail(string email)
        {
            return DbSet.FirstOrDefault(x => x.Email == email);
        }

        public User GetById(Guid id, IEnumerable<Expression<Func<User, object>>> includeProperties = null)
        {
            IQueryable<User> query = DbSet.AsQueryable();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault(x => x.Id == id);
        }
    }
}
