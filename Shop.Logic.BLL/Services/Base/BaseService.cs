using AutoMapper;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Logic.BLL.Services.Base
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _mapper = mapper ?? throw new ArgumentNullException("mapper");
        }
    }
}
