using Shop.Domain.Contracts.Models;
using Shop.Domain.enums;
using System.Collections.Generic;

namespace Shop.Logic.BLL.Models
{
    class CartModel : ICartModel
    {
        public decimal TotalSum { get; set; }
        public ICollection<IProductInCart> Products { get; }

        public CartModel(ICollection<IProductInCart> products)
        {
            Products = products;
        }

        public void CountSumTotal()
        {
            foreach (var item in Products)
            {
                if(item.Product.Status == ProductStatus.Available)
                    TotalSum += item.Product.Price * item.Count;
            }
        }
    }
}
