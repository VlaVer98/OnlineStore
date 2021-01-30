using Shop.Domain.Enums;
using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class OrderViewModel : BaseViewModel<Guid>
    {
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public decimal PriceProduct { get; set; }
        public Guid? ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public Guid? UserId { get; set; }
        public UserOrderViewModel User { get; set; }
    }
}
