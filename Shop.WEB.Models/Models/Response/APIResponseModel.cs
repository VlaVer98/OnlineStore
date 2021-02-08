using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.WEB.Models.Models.Response
{
    public class APIResponseModel<T> where T : class
    {
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }

        public APIResponseModel()
        {
            Messages = new List<string>();
            Data = null;
        }

        public APIResponseModel(bool isSuccessful)
            : this()
        {
            IsSuccessful = isSuccessful;
        }

        public APIResponseModel(bool isSuccessful, ModelStateDictionary modelState)
            : this(isSuccessful)
        {
            if (!modelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var item in modelState)
                {
                    errors.Add($"{item.Key}, {item.Value}");
                }
                Messages = errors;
            }
        }

        public APIResponseModel(bool isSuccessful, List<string> messages, T data = null)
            : this(isSuccessful)
        {
            Messages = messages;
            Data = data;
        }
    }
}
