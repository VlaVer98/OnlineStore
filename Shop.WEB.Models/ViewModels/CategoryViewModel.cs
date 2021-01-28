using Shop.WEB.Models.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel<Guid>
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
