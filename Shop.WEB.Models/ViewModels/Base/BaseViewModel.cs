using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels.Base
{
    public class BaseViewModel<TKey>
    {
        [Required]
        public TKey Id { get; set; }
    }
}
