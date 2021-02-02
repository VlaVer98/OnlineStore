using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Models.Dtos.User;
using Shop.Domain.Models.Identity;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services
{
    public interface IUserService : IService
    {
        public UserDto Get(Guid id);
        public User GetByEmail(string email);
        public IEnumerable<UserDto> GetAll(bool withProfile = true);
    }
}
