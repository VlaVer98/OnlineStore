using Shop.Domain.Contracts.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shop.Domain.Contracts.Models
{
    public interface IUploadingImageToProductModel : IBaseModel<Guid>
    {
        public Stream Stream { get; set; }
        public string NameImage { get; set; }
        public string TypeImage { get; set; }
        public long LengthImage { get; set; }
    }
}
