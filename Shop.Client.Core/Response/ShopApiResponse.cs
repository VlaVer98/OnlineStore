using System.Net;

namespace Shop.Client.Core.Response
{
    class ShopApiResponse<T>
    {
        public T ResponseObject { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public bool IsSuccessful
        {
            get { return StatusCode == HttpStatusCode.OK; }
        }
    }
}
