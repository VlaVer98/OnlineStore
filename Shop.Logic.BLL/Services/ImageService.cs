using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Models;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Product;
using Shop.Domain.Models.Entities;
using Shop.Logic.BLL.Models;
using Shop.Logic.BLL.Services.Base;
using System;
using System.IO;

namespace Shop.Logic.BLL.Services
{
    public class ImageService : BaseService, IImageService
    {
        private const long MAXSIZEIMAGE = 10000000;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
            : base (unitOfWork, mapper) { }
        
        public ServiceResponse Upload(UploadingImageToProductDto uploadingImageDto)
        {
            if(uploadingImageDto == null)
            {
                return new ServiceResponse(false, "Invalid argument");
            }

            IUploadingImageToProductModel uploadingImageModel = _mapper
                .Map<UploadingImageToProductModel>(uploadingImageDto);

            if (uploadingImageModel.LengthImage <= 0)
            {
                new ServiceResponse(false, $"Invalid image");
            }

            if (uploadingImageModel.LengthImage > MAXSIZEIMAGE || uploadingImageModel.LengthImage > int.MaxValue)
            {
                new ServiceResponse(false, $"Images larger than {MAXSIZEIMAGE} are not allowed");
            }

            byte[] imageData = new byte[(int)uploadingImageModel.LengthImage];
            using (var binaryReader = new BinaryReader(uploadingImageModel.Stream))
            {
                imageData = binaryReader.ReadBytes((int)uploadingImageModel.LengthImage);
            }

            Image image = new Image
            {
                Id = Guid.NewGuid(),
                BinaryData = imageData,
                Name = Path.GetFileName(uploadingImageModel.NameImage),
                UploadDate = DateTime.Now
            };
            _unitOfWork.Images.Add(image);

            Product product = _unitOfWork.Products.GetById(uploadingImageModel.Id);
            if(product == null)
            {
                return new ServiceResponse(false, "Product with this ID does not exist");
            }

            product.ImageId = image.Id;
            _unitOfWork.Products.Update(product);

            _unitOfWork.Commit();

            return new ServiceResponse(false, "Image saved successfully");
        }
    }
}
