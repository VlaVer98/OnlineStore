using Shop.Domain.enums;
using Shop.WEB.Models.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.WEB.Models.ViewModels
{
    public class ProductViewModel : BaseViewModel<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public CategoryViewModel Category { get; set; }
        public ImageViewModel Image { get; set; }
    }
}
