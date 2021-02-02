using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
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
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
            : base(unitOfWork, mapper) 
        {
            _userManager = userManager;
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
    }
}
