﻿using Shop.Domain.Models.Dtos.Base;
using System;

namespace Shop.Domain.Models.Dtos.Order
{
    public class UserOrderDto : BaseEntityDto<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
    }
}
