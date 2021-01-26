using Shop.Domain.Models.Entities.Base;
using System;

namespace Shop.Domain.Models.Entities
{
    class Category : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
