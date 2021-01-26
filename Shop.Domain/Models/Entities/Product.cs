using Shop.Domain.enums;
using Shop.Domain.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models.Entities
{
    class Product : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public ProductStatus Status { get; set; }

        //Navigation
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
