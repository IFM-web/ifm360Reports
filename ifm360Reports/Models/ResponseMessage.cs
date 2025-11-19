using System.Net;

namespace ifm360Reports.Models
{
    public class ResponseMessage
    {
       public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
