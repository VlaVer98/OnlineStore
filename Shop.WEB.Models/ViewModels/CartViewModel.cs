using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.WEB.Models.ViewModels
{
    public class CartViewModel
    {
        public decimal TotalSum { get; set; }
        public ICollection<ProductInCartViewModel> Products { get; set; }
        public ICollection<string> Messages { get; set; }
    }
}
