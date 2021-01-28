using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models.Dtos.Category;
using Shop.Domain.Models.Entities;
using Shop.Logic.BLL.Services.Base;
using System.Collections.Generic;

namespace Shop.Logic.BLL.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }
    }
}
