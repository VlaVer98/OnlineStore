using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Account;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface IAccountService : IService
    {
        public ServiceResponse<List<string>> CreateBuyer(BuyerRegistrationDto buyerRegistrationDto);
    }
}
