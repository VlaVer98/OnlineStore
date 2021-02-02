using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.User;
using Shop.Domain.Models.Identity;
using Shop.Logic.BLL.Services.Base;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Logic.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

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
    }
}
