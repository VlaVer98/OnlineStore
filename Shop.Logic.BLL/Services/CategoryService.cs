using AutoMapper;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Models.Dtos.Category;
using Shop.Domain.Models.Entities;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;

namespace Shop.Logic.BLL.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public IEnumerable<CategoryDto> GetAll()
        {
            IEnumerable<Category> categories = _unitOfWork.Categories.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public CategoryDto Get(Guid id)
        {
            Category category = GetById(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public ServiceResponse Update(CategoryDto categoryDto)
        {
            if(categoryDto == null)
            {
                return new ServiceResponse(false, "Invalid category");
            }

            Category category = GetById(categoryDto.Id);
            if(category == null)
            {
                return new ServiceResponse(false, "Invalid category");
            }

            Category categoryByName = _unitOfWork.Categories.GetByTitle(categoryDto.Title);
            if (categoryByName != null)
            {
                return new ServiceResponse(false, $"Category '{categoryDto.Title}' already exists");
            }

            category.Title = categoryDto.Title;
            category.Description = categoryDto.Description;

            _unitOfWork.Commit();

            return new ServiceResponse(true, "Category successfully updated");
        }

        public ServiceResponse Create(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return new ServiceResponse(false, "Invalid category");
            }

            Category categoryByName = _unitOfWork.Categories.GetByTitle(categoryDto.Title);
            if (categoryByName != null)
            {
                return new ServiceResponse(false, $"Category '{categoryDto.Title}' already exists");
            }

            Category category = new Category
            {
                Id = Guid.NewGuid(),
                Title = categoryDto.Title,
                Description = categoryDto.Description
            };

            _unitOfWork.Categories.Add(category);
            _unitOfWork.Commit();

            return new ServiceResponse(true, "Category successfully created");
        }

        private Category GetById(Guid id)
        {
            return _unitOfWork.Categories.GetById(id);
        }
    }
}
