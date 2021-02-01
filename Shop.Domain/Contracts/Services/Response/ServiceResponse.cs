using System;
using System.Collections;
using System.Collections.Generic;

namespace Shop.Domain.Contracts.Services.Response
{
    public class ServiceResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { 
            get 
            {
                return AllMessages.Count > 0 ? AllMessages[0] : string.Empty;
            }
        }
        public List<string> AllMessages { get; set; }
        
        public ServiceResponse() {
            AllMessages = new List<string>();
        }

        public ServiceResponse(bool isSuccessful)
            : this()
        {
            IsSuccessful = isSuccessful;
        }

        public ServiceResponse(Exception ex)
            : this()
        {
            IsSuccessful = false;
            AllMessages.Add(ex.Message);
        }

        public ServiceResponse(bool isSuccessful, string message)
            : this(isSuccessful)
        {
            AllMessages.Add(message);
        }

        public ServiceResponse(bool isSuccessful, List<string> messages)
            : this(isSuccessful)
        {
            AllMessages = messages;
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T ResponseObject { get; set; }

        public ServiceResponse() { }

        public ServiceResponse(ServiceResponse response, T responseObject) : base(response.IsSuccessful, response.Message)
        {
            ResponseObject = responseObject;
        }

        public ServiceResponse(bool isSuccessful) 
            : base(isSuccessful) { }

        public ServiceResponse(bool isSuccessful, string message) 
            : base(isSuccessful, message) { }

        public ServiceResponse(bool isSuccessful, List<string> messages) 
            : base(isSuccessful, messages) { }

        public ServiceResponse(bool isSuccessful, string message, T responseObject)
            : base(isSuccessful, message)
        {
            ResponseObject = responseObject;
        }

        public ServiceResponse(bool isSuccessful, List<string> messages, T responseObject)
            : base(isSuccessful, messages)
        {
            ResponseObject = responseObject;
        }
    }
}
