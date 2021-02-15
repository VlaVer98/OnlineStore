using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Client.Models.Catalog
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ImageId { get; set; }
    }
}
