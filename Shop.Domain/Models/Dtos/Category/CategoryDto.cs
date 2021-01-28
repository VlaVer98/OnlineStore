using Shop.Domain.Models.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models.Dtos.Category
{
    public class CategoryDto : BaseEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
