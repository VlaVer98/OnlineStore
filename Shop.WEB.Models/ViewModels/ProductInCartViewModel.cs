using Shop.Domain.Models.Dtos.Product;

namespace Shop.WEB.Models.ViewModels
{
    public class ProductInCartViewModel
    {
        public int Count { get; set; }
        public ProductDto Product { get; set; }
    }
}
