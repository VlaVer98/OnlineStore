using Shop.Domain.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [UIHint("Decimal")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Required]
        public ProductStatus Status { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public IEnumerable<СategoryTitleAndIdViewModel> AllCategory { get; set; }
    }
}
