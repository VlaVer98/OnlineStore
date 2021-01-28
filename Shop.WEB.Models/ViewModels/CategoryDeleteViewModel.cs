using Shop.WEB.Models.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.WEB.Models.ViewModels
{
    public class CategoryDeleteViewModel : BaseViewModel<Guid>
    {
        public string Message { get; set; }
    }
}
