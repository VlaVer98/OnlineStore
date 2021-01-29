using Shop.Domain.enums;
using Shop.Domain.Models.Dtos.Base;
using Shop.Domain.Models.Dtos.Category;
using Shop.Domain.Models.Dtos.Image;
using System;

namespace Shop.Domain.Models.Dtos.Product
{
    public class ProductDto : BaseEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public Guid? CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public Guid? ImageId { get; set; }
        public ImageDto Image { get; set; }
    }
}
