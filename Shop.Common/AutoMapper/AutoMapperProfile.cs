using AutoMapper;
using Shop.Domain.Models.Dtos.Category;
using Shop.Domain.Models.Dtos.Image;
using Shop.Domain.Models.Dtos.Product;
using Shop.Domain.Models.Entities;
using Shop.WEB.Models.ViewModels;

namespace Shop.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //DAL -> BLL
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Image, ImageDto>();

            //BLL -> DAL
            CreateMap<CategoryDto, Category>();
            CreateMap<ImageDto, Image>();

            //BLL -> Presentation
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CategoryDto, СategoryTitleAndIdViewModel>();
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap<ProductDto, ProductCreateViewModel>();

            //Presentation -> BLL
            CreateMap<CategoryViewModel, CategoryDto>();
            CreateMap<СategoryTitleAndIdViewModel, CategoryDto>();
            CreateMap<ProductCreateViewModel, ProductDto>();
        }
    }
}
