using Shop.Domain.Contracts.Services.Base;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Product;

namespace Shop.Domain.Contracts.Services
{
    public interface IImageService : IService
    {
        public ServiceResponse Upload(UploadingImageToProductDto uploadingImageDto);
    }
}
