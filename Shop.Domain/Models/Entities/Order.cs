﻿using Shop.Domain.Models.Entities.Base;
using Shop.Domain.Enums;
using System;
using Shop.Domain.Models.Identity;

namespace Shop.Domain.Models.Entities
{
    class Order : BaseEntity<Guid>
    {
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public decimal PriceProduct { get; set; }

        //Navigation
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}