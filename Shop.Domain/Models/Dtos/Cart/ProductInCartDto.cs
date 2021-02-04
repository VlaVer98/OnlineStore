using Shop.Domain.Models.Dtos.Product;

namespace Shop.Domain.Models.Dtos.Cart
{
    public class ProductInCartDto
    {
        public int Count { get; set; }
        public ProductDto Product { get; set; }
    }
}
