using Shop.Domain.Contracts.Repositories.Base;
using Shop.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shop.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetById(Guid id, IEnumerable<Expression<Func<User, object>>> includeProperties = null);
        User GetByEmail(string email);
    }
}
