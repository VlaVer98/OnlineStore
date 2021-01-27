using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Logic.BLL.Services.Base
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
        }
    }
}
