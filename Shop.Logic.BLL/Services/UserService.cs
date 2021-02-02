using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Identity;
using Shop.Logic.BLL.Services.Base;

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
    }
}
