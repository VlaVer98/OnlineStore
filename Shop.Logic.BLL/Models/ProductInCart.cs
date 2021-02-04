using Shop.Domain.Contracts.Models;
using Shop.Domain.Models.Entities;

namespace Shop.Logic.BLL.Models
{
    public class ProductInCart : IProductInCart
    {
        public int Count { get; set; }
        public Product Product { get; set; }
    }
}
