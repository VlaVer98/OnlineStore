using Shop.Domain.Contracts.Repositories.Base;
using Shop.Domain.Models.Identity;

namespace Shop.Domain.Contracts.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);
    }
}
