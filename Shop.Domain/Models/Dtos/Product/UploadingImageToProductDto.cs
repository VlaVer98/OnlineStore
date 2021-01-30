using System;
using System.IO;

namespace Shop.Domain.Models.Dtos.Product
{
    public class UploadingImageToProductDto
    {
        public Guid Id { get; set; }
        public Stream Stream { get; set; }
        public string NameImage { get; set; }
        public string TypeImage { get; set; }
        public long LengthImage { get; set; }
    }
}
