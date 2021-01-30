﻿using AutoMapper;
using Shop.Domain.Models.Dtos.Category;
using Shop.Domain.Models.Dtos.Image;
using Shop.Domain.Models.Dtos.Order;
using Shop.Domain.Models.Dtos.Product;
using Shop.Domain.Models.Entities;
using Shop.Domain.Models.Identity;
using Shop.Logic.BLL.Models;
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
            CreateMap<OrderProduct, OrderProductDto>();
            CreateMap<UserProfile, UserOrderDto>();
            CreateMap<User, UserOrderDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Products, opt => opt.MapFrom(y => y.Products))
                .ForMember(x => x.User, opt => opt.MapFrom(y => y.User))
                .ForMember(x => x.User, opt => opt.MapFrom(y => y.User.Profile));

            //BLL -> DAL
            CreateMap<CategoryDto, Category>();
            CreateMap<ImageDto, Image>();
            CreateMap<UploadingImageToProductDto, UploadingImageToProductModel>();

            //BLL -> Presentation
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CategoryDto, СategoryTitleAndIdViewModel>();
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap<ProductDto, ProductCreateViewModel>();
            CreateMap<ImageDto, ImageViewModel>();
            CreateMap<UserOrderDto, UserOrderViewModel>();
            CreateMap<OrderDto, OrderViewModel>();

            //Presentation -> BLL
            CreateMap<CategoryViewModel, CategoryDto>();
            CreateMap<СategoryTitleAndIdViewModel, CategoryDto>();
            CreateMap<ProductCreateViewModel, ProductDto>();
            CreateMap<UploadingImageToProductViewModel, UploadingImageToProductDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.NameImage, opt => opt.MapFrom(y => y.NameImage))
                .ForMember(x => x.Stream, opt => opt.MapFrom(y => y.FormFile.OpenReadStream()))
                .ForMember(x => x.LengthImage, opt => opt.MapFrom(y => y.FormFile.Length))
                .ForMember(x => x.TypeImage, opt => opt.MapFrom(y => y.FormFile.ContentType));
        }
    }
}
