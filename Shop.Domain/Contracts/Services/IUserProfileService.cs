using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.User;
using System;

namespace Shop.Domain.Contracts.Services
{
    public interface IUserProfileService : IService
    {
        public UserProfileDto Get(Guid id);
        public ServiceResponse Update(UserProfileDto userProfileDto);
        public UserProfileDto GetForUser(Guid userId);
    }
}
