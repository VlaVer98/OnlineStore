using Shop.Domain.Enums;
using Shop.Domain.Models.Dtos.Base;
using Shop.Domain.Models.Dtos.Product;
using System;

namespace Shop.Domain.Models.Dtos.Order
{
    public class OrderDto : BaseEntityDto<Guid>
    {
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public decimal PriceProduct { get; set; }
        public Guid? ProductId { get; set; }
        public ProductDto Product { get; set; }
        public Guid? UserId { get; set; }
        public UserOrderDto User { get; set; }
    }
}
