using Shop.Domain.Models.Dtos.Base;
using System;

namespace Shop.Domain.Models.Dtos.Image
{
    public class ImageDto : BaseEntityDto<Guid>
    {
        public string Name { get; set; }
        public byte[] BinaryData { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
