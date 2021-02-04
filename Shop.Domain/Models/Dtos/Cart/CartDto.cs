using System.Collections.Generic;

namespace Shop.Domain.Models.Dtos.Cart
{
    public class CartDto
    {
        public decimal SumTotal { get; set; }
        public ICollection<ProductInCartDto> Products { get; set; }
    }
}
