﻿using AutoMapper;
using Shop.Domain.Models.Dtos.Category;
using Shop.Domain.Models.Entities;

namespace Shop.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //DAL -> BLL
            CreateMap<Category, CategoryDto>();

            //BLL -> DAL
            CreateMap<CategoryDto, Category>();
        }
    }
}
