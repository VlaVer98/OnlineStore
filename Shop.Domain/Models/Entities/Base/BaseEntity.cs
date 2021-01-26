using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models.Entities.Base
{
    public class BaseEntity<TKey> : IEntity
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
