using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Models;
using Shop.Domain.Models.Dtos.Order;
using Shop.Domain.Models.Entities;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shop.Logic.BLL.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public IEnumerable<OrderDto> GetAll()
        {
            IEnumerable<Order> orders = _unitOfWork.Orders.GetAll(GetAllIncludeProperties())
                .Include(x => x.User).ThenInclude(x => x.Profile);

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        private List<Expression<Func<Order, object>>> GetAllIncludeProperties()
        {
            return new List<Expression<Func<Order, object>>> {
                x => x.User,
                x => x.Product
            };
        }
    }
}
