using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.WEB.Models.Models.Response
{
    class APIResponseModel<T> where T : class
    {
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }
    }
}
