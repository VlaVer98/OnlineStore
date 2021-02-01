using Shop.Domain.Models.Entities.Base;
using System;

namespace Shop.Domain.Models.Entities
{
    public class OrderProduct : BaseEntity<Guid>
    {
        public decimal PricePurchase  { get; set; }

        //Navigation
        public Guid OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
