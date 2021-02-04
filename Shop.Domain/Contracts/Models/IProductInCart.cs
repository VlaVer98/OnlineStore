using Shop.Domain.Models.Entities;

namespace Shop.Domain.Contracts.Models
{
    public interface IProductInCart
    {
        public int Count { get; set; }
        public Product Product{ get; set; }
    }
}
