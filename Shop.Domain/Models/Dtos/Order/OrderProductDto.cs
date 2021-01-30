using Shop.Domain.Models.Dtos.Base;
using Shop.Domain.Models.Dtos.Product;
using System;

namespace Shop.Domain.Models.Dtos.Order
{
    public class OrderProductDto : BaseEntityDto<Guid>
    {
        public decimal PricePurchase { get; set; }
        public Guid? ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
