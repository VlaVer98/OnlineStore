using Shop.Domain.Models.Entities.Base;
using Shop.Domain.Enums;
using System;
using Shop.Domain.Models.Identity;
using System.Collections.Generic;

namespace Shop.Domain.Models.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public decimal Amount { get; set; }

        //Navigation
        public ICollection<OrderProduct> Products { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
