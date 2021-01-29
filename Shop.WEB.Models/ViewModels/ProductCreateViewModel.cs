using Shop.Domain.enums;
using Shop.WEB.Models.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels
{
    public class ProductCreateViewModel : BaseViewModel<Guid>
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [UIHint("Decimal")]
        public decimal Price { get; set; }
        [Required]
        public ProductStatus Status { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public IEnumerable<СategoryTitleAndIdViewModel> AllCategory { get; set; }
    }
}
