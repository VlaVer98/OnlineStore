using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Models.Identity;

namespace Shop.Domain.Contracts.Services
{
    public interface IUserService : IService
    {
        public User GetByEmail(string email);
    }
}
