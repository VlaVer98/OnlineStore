using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Domain;
using Shop.Domain.Contracts.Models;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Account;
using Shop.Domain.Models.Entities;
using Shop.Domain.Models.Identity;
using Shop.Logic.BLL.Models;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Logic.BLL.Services
{
    public class AccountService: BaseService, IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
            : base (unitOfWork, mapper) 
        {
            _userManager = userManager;
        }

        public ServiceResponse<User> CreateBuyer(BuyerRegistrationDto buyerRegistrationDto)
        {
            List<string> errors = new List<string>();

            if (buyerRegistrationDto == null)
            {
                return new ServiceResponse<User>(false, "Data is invalid", null);
            }
            BuyerRegistrationModel buyerRegistrationModel = _mapper
                .Map<BuyerRegistrationModel>(buyerRegistrationDto);

            if (buyerRegistrationModel.Name == null)
                errors.Add("Name must not be empty");

            if (buyerRegistrationModel.Lastname == null)
                errors.Add("Lastname must not be empty");

            if (buyerRegistrationModel.Lastname == null)
                errors.Add("Lastname must not be empty");

            if (buyerRegistrationModel.Patronymic == null)
                errors.Add("Patronymic must not be empty");

            if (buyerRegistrationModel.Address == null)
                errors.Add("Address must not be empty");

            User user = _mapper.Map<User>(buyerRegistrationModel);
            UserProfile userProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                Name = buyerRegistrationModel.Name,
                Lastname = buyerRegistrationModel.Lastname,
                Patronymic = buyerRegistrationModel.Patronymic,
                Address = buyerRegistrationModel.Address
            };
            user.Profile = userProfile;

            var result = _userManager.CreateAsync(user, buyerRegistrationModel.Password).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return new ServiceResponse<User>(false, errors, null);
            }

            return new ServiceResponse<User>(true, "Successful registration", user);
        }

        public ServiceResponse ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return new ServiceResponse(false, $"User with id {userId} does not exist");

            if (oldPassword == string.Empty || newPassword == string.Empty)
                return new ServiceResponse(false, "Invalid argument");

            IdentityResult result =
                _userManager.ChangePasswordAsync(user, oldPassword, newPassword).GetAwaiter().GetResult();

            if (!result.Succeeded)
            {
                List<string> errors = new List<string>();
                foreach (var item in result.Errors)
                    errors.Add(item.Description);
                return new ServiceResponse(false, errors);
            }

            return new ServiceResponse(true, "Passsword is successful change");
        }
    }
}
