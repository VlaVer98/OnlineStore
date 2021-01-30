using Shop.Domain.Enums;
using Shop.Domain.Models.Dtos.Base;
using Shop.Domain.Models.Dtos.Product;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Models.Dtos.Order
{
    public class OrderDto : BaseEntityDto<Guid>
    {
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public decimal Amount { get; set; }
        public ICollection<OrderProductDto> Products { get; set; }
        public Guid? UserId { get; set; }
        public UserOrderDto User { get; set; }
    }
}
