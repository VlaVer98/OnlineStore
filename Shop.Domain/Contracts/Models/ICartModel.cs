using Shop.Domain.Models.Entities;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Models
{
    public interface ICartModel
    {
        public decimal TotalSum { get; set; }
        public ICollection<IProductInCart> Products { get; }
        public void CountSumTotal();
    }
}
