using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models.Dtos.Base
{
    public class BaseEntityDto<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
