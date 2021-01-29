using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Product;
using Shop.Domain.Models.Entities;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shop.Logic.BLL.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public IEnumerable<ProductDto> GetAll()
        {
            IEnumerable<Product> products = _unitOfWork.Products.GetAll(GetAllIncludeProperties());
            IEnumerable<ProductDto> productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public ProductDto Get(Guid id, bool withAllInclude = false)
        {
            Product product;

            if (withAllInclude)
                product = _unitOfWork.Products.GetById(id, GetAllIncludeProperties());
            else
                product = _unitOfWork.Products.GetById(id);

            return _mapper.Map<ProductDto>(product);
        }

        public ServiceResponse Create(ProductDto productDto)
        {
            if(productDto == null)
            {
                return new ServiceResponse(false, "Invalid product");
            }

            if(productDto.Title == null)
            {
                return new ServiceResponse(false, "Title not set");
            }

            if(productDto.CategoryId != null)
            {

                Category category = _unitOfWork.Categories.GetById((Guid)productDto.CategoryId);
                if(category == null)
                {
                    return new ServiceResponse(false, "Invalid category");
                }
            }
            
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Title = productDto.Title,
                Content = productDto.Content,
                Price = productDto.Price,
                Status = productDto.Status,
                CategoryId = productDto.CategoryId
            };

            _unitOfWork.Products.Add(product);
            _unitOfWork.Commit();

            return new ServiceResponse(true, "Product successfully created");
        }

        public ServiceResponse Update(ProductDto productDto)
        {
            if (productDto == null)
            {
                return new ServiceResponse(true, "Invalid product");
            }

            if (productDto.Title == null)
            {
                return new ServiceResponse(false, "Title not set");
            }

            if (productDto.CategoryId != null)
            {

                Category category = _unitOfWork.Categories.GetById((Guid)productDto.CategoryId);
                if (category == null)
                {
                    return new ServiceResponse(false, "Invalid category");
                }
            }

            Product product = _unitOfWork.Products.GetById(productDto.Id);
            if(product == null)
            {
                return new ServiceResponse(false, "Invalid product id");
            }

            product.Title = productDto.Title;
            product.Content = productDto.Content;
            product.Price = productDto.Price;
            product.Status = productDto.Status;
            product.CategoryId = productDto.CategoryId;

            _unitOfWork.Products.Update(product);
            _unitOfWork.Commit();

            return new ServiceResponse(true, "Product successfully updated");
        }

        public ServiceResponse Delete(Guid id)
        {
            Product product = _unitOfWork.Products.GetById(id);
            if(product == null)
            {
                return new ServiceResponse(false, "Error delete. Invalid product id");
            }

            _unitOfWork.Products.Delete(product);
            _unitOfWork.Commit();

            return new ServiceResponse(true, "Product successfully delete");
        }

        private List<Expression<Func<Product, object>>> GetAllIncludeProperties()
        {
            return new List<Expression<Func<Product, object>>> {
                x => x.Category,
                x => x.Image
            };
        }  
    }
}
