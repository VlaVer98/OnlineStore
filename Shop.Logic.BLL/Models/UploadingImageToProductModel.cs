using Shop.Domain.Contracts.Models;
using System;
using System.IO;

namespace Shop.Logic.BLL.Models
{
    public class UploadingImageToProductModel : IUploadingImageToProductModel
    {
        public Stream Stream { get; set; }
        public string NameImage { get; set; }
        public string TypeImage { get; set; }
        public long LengthImage { get; set; }
        public Guid Id { get; set; }
    }
}