using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
