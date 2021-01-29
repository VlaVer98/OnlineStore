using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class СategoryTitleAndIdViewModel : BaseViewModel<Guid>
    {
        public string Title { get; set; }
    }
}
