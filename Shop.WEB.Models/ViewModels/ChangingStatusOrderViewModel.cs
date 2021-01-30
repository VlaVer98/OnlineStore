using Shop.Domain.Enums;
using Shop.WEB.Models.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels
{
    public class ChangingStatusOrderViewModel : BaseViewModel<Guid>
    {
        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}
