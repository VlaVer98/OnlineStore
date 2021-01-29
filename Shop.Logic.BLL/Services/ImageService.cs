using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Logic.BLL.Services.Base;

namespace Shop.Logic.BLL.Services
{
    public class ImageService : BaseService, IImageService
    {
        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
            : base (unitOfWork, mapper) { }
    }
}
