using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Account;
using Shop.Domain.Models.Identity;
using System;

namespace Shop.Domain.Contracts.Services
{
    public interface IAccountService : IService
    {
        public ServiceResponse<User> CreateBuyer(BuyerRegistrationDto buyerRegistrationDto);
        public ServiceResponse ChangePassword(Guid userId, string oldPassword, string newPassword);
    }
}
