using System.Net;

namespace CakeShop.Models.Models.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; init; }
        public string Message { get; init; }
    }
}
