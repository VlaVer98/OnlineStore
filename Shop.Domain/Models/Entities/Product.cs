using Shop.Domain.enums;
using Shop.Domain.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }

        //Navigation
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }
    }
}
