using Shop.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Logic.BLL.Models
{
    public class BuyerRegistrationModel : IBuyerRegistrationModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
