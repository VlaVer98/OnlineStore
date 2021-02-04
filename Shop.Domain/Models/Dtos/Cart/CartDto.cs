using System.Collections.Generic;

namespace Shop.Domain.Models.Dtos.Cart
{
    public class CartDto
    {
        public decimal TotalSum { get; set; }
        public ICollection<ProductInCartDto> Products { get; set; }
    }
}
