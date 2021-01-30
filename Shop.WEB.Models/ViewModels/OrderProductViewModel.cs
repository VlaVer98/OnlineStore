using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class OrderProductViewModel : BaseViewModel<Guid>
    {
        public decimal PricePurchase { get; set; }
        public Guid? ProductId { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
