using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models.ViewModels
{
    public class UploadingImageToProductViewModel : ServiceResponseViewModel
    {
        [Required]
        public string NameImage { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
    }
}
