using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class ServiceResponseViewModel : BaseViewModel<Guid>
    {
        public string Message { get; set; }
    }
}
