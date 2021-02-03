using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.User;
using Shop.Domain.Models.Entities;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Logic.BLL.Services
{
    public class UserProfileService : BaseService, IUserProfileService
    {
        public UserProfileService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public UserProfileDto Get(Guid id)
        {
            UserProfile userProfile = _unitOfWork.UserProfiles.GetById(id);
            return _mapper.Map<UserProfileDto>(userProfile);
        }

        public UserProfileDto GetForUser(Guid userId)
        {
            UserProfile userProfile = _unitOfWork.UserProfiles.Get().FirstOrDefault(x=>x.UserId == userId);
            return _mapper.Map<UserProfileDto>(userProfile);
        }

        public ServiceResponse Update(UserProfileDto userProfileDto)
        {
            if(userProfileDto == null)
                return new ServiceResponse(false, "Data is invalid");

            UserProfile userProfile = _unitOfWork.UserProfiles.GetById(userProfileDto.Id);
            if(userProfile == null)
                return new ServiceResponse(false, $"User profile with id {userProfileDto.Id} not found");

            List<string> errors = new List<string>();
            if (userProfileDto.Name == null)
                errors.Add("Name must not be empty");
            if (userProfileDto.Lastname == null)
                errors.Add("Lastname must not be empty");
            if (userProfileDto.Patronymic == null)
                errors.Add("Patronymic must not be empty");
            if (userProfileDto.Address == null)
                errors.Add("Address must not be empty");
            if(errors.Count > 0)
                return new ServiceResponse(false, errors);

            userProfile.Lastname = userProfileDto.Lastname;
            userProfile.Name = userProfileDto.Name;
            userProfile.Patronymic = userProfileDto.Patronymic;
            userProfile.Address = userProfileDto.Address;

            _unitOfWork.UserProfiles.Update(userProfile);
            _unitOfWork.Commit();

            return new ServiceResponse(true, "User profile is successful change");
        }
    }
}
