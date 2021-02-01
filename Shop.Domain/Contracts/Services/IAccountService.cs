using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Account;
using Shop.Domain.Models.Identity;

namespace Shop.Domain.Contracts.Services
{
    public interface IAccountService : IService
    {
        public ServiceResponse<User> CreateBuyer(BuyerRegistrationDto buyerRegistrationDto);
    }
}
