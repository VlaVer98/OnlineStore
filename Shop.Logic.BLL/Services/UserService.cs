using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.User;
using Shop.Domain.Models.Identity;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Logic.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserProfileService _userProfileService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, 
            IUserProfileService userProfileService)
            : base(unitOfWork, mapper) 
        {
            _userManager = userManager;
            _userProfileService = userProfileService;
        }

        public User GetByEmail(string email)
        {
            return _unitOfWork.Users.GetByEmail(email);
        }

        public IEnumerable<UserDto> GetAll(bool withProfile = true)
        {
            IEnumerable<User> users;
            if (withProfile)
                users = _unitOfWork.Users.GetAll().Include(x => x.Profile);
            else
                users = _unitOfWork.Users.GetAll();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto Get(Guid id)
        {
            User user = _userManager.Users.Include(x=>x.Profile).FirstOrDefault(x => x.Id == id);
            return _mapper.Map<UserDto>(user);
        }

        public ServiceResponse UpdateProfile(Guid userId, UserProfileDto userProfileDto)
        {
            User user = _userManager.Users.Include(x => x.Profile).FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return new ServiceResponse(false, $"User with id {userId} does not exist");
            if (user.Profile.Id != userProfileDto.Id)
                return new ServiceResponse(false, $"Data is invalid");

            return _userProfileService.Update(userProfileDto);
        }

        public ServiceResponse Delete(Guid id)
        {
            User user = _userManager.Users.Include(x => x.Profile).FirstOrDefault(x => x.Id == id);
            if (user == null)
                return new ServiceResponse(false, $"User with id {id} does not exist");

            var result = _userManager.DeleteAsync(user).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var item in result.Errors)
                {
                    errors.Add(item.Description);
                }
                return new ServiceResponse(false, errors);
            }

            return new ServiceResponse(true, "User is successed delete");
        }
    }
}
