using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Logic.BLL.Services.Base;

namespace Shop.Logic.BLL.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }
    }
}
