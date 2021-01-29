using Shop.Domain.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models.Entities
{
    public class Image : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public byte[] BinaryData { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
