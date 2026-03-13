using POS.APPLICATION.Dto;

namespace POS.APPLICATION.InfraServices.Interfaces
{
    public interface IHttpRequestService
    {
        Task<HttpRequestResponse> Get(string url);
        Task<HttpRequestResponse> Get(string url, string Token, string ApiSoporteUsername = "", string ApiSoportePassword = "", string TokenType = "Bearer");
        Task<HttpRequestResponse> Get(string url, Dictionary<string, string> Headers);
        Task<HttpRequestResponse> Post(string url, object data);
        Task<HttpRequestResponse> Post(string url, object data, string Token, string ApiSoporteUsername = "", string ApiSoportePassword = "", string TokenType = "Bearer");
        Task<HttpRequestResponse> PostForm(string url, object data);
        Task<HttpRequestResponse> Patch(string url, object data, string Token);
        Task<HttpRequestResponse>? Delete(string url, string Token, string TokenType = "Bearer");
    }
}
