using Shop.Data.DB.Context;
using Shop.Data.DB.Repositories.Base;
using Shop.Domain.Contracts.Repositories;
using Shop.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Data.DB.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ShopDbContext dbContext)
            : base(dbContext) {}

        public Role GetByName(string name)
        {
            return DbSet.FirstOrDefault(x => x.Name == name);
        }
    }
}
