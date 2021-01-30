using Shop.WEB.Models.ViewModels.Base;
using System;

namespace Shop.WEB.Models.ViewModels
{
    public class ImageViewModel : BaseViewModel<Guid>
    {
        public string Name { get; set; }
        public byte[] BinaryData { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
