using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Contracts.Models.Base
{
    public interface IBaseModel<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
