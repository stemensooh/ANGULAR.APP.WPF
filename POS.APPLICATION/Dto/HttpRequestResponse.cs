using System.Net;

namespace POS.APPLICATION.Dto
{
    public class HttpRequestResponse
    {
        public HttpStatusCode status { get; set; }
        public string? response { get; set; }
    }
}
