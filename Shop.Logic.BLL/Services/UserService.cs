using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Logic.BLL.Services.Base;

namespace Shop.Logic.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }
    }
}
