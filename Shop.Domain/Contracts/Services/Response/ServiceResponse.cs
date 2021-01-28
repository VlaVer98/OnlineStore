using System;

namespace Shop.Domain.Contracts.Services.Response
{
    public class ServiceResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public ServiceResponse() { }

        public ServiceResponse(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
            Message = string.Empty;
        }

        public ServiceResponse(Exception ex)
        {
            IsSuccessful = false;
            Message = ex.Message;
        }

        public ServiceResponse(bool isSuccessful, string message)
            : this(isSuccessful)
        {
            Message = message;
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

        public ServiceResponse(bool isSuccessful) : base(isSuccessful) { }

        public ServiceResponse(bool isSuccessful, string message) : base(isSuccessful, message) { }

        public ServiceResponse(bool isSuccessful, string message, T responseObject)
            : base(isSuccessful, message)
        {
            ResponseObject = responseObject;
        }
    }
}
