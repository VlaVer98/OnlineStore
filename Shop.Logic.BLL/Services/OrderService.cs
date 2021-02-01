using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Domain.Contracts.Services;
using Shop.Domain.Contracts.Services.Response;
using Shop.Domain.Enums;
using Shop.Domain.Models;
using Shop.Domain.Models.Dtos.Order;
using Shop.Domain.Models.Entities;
using Shop.Logic.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public OrderDto GetWithAllRelations(Guid id)
        {
            Order order = _unitOfWork.Orders.Get()
                .Include(x => x.User).ThenInclude(x => x.Profile)
                .Include(x => x.Products).ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<OrderDto>(order);
        }

        public ServiceResponse ChangeStatus(Guid orderId, OrderStatus orderStatus)
        {
            Order order = _unitOfWork.Orders.GetById(orderId);
            if(order == null)
            {
                return new ServiceResponse(false, $"Order with Id{orderId} does not exist");
            }

            order.Status = orderStatus;
            order.Updated = DateTime.Now;
            _unitOfWork.Orders.Update(order);

            _unitOfWork.Commit();
            return new ServiceResponse(true, "Order status is successful change");
        }

        public ServiceResponse Delete(Guid id)
        {
            Order order = _unitOfWork.Orders.GetById(id);
            if(order == null)
            {
                return new ServiceResponse(false, $"Order with Id {id} does not exist");
            }

            _unitOfWork.Orders.Delete(order);
            _unitOfWork.Commit();

            return new ServiceResponse(true, "Order is successful delete");
        }

        private List<Expression<Func<Order, object>>> GetAllIncludeProperties()
        {
            return new List<Expression<Func<Order, object>>> {
                x => x.User,
                x => x.Products
            };
        }
    }
}
