using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POS.APPLICATION.Dto;
using POS.APPLICATION.InfraServices.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace POS.INFRA.HTTP.InfraServices
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly HttpClient client;

        public HttpRequestService()
        {
            client = new HttpClient();
        }

        public async Task<HttpRequestResponse> Get(string url)
        {
            HttpResponseMessage request = await client.GetAsync(url);
            return await ObtenerRespuesta(request);
        }

        public async Task<HttpRequestResponse> Get(string url, Dictionary<string, string> Headers)
        {
            using (var cliente = new HttpClient())
            {
                foreach (KeyValuePair<string, string> item in Headers)
                    cliente.DefaultRequestHeaders.Add(item.Key, item.Value);
                HttpResponseMessage request = await cliente.GetAsync(url);
                return await ObtenerRespuesta(request);
            }
        }

        public async Task<HttpRequestResponse> Get(string url, string Token, string ApiSoporteUsername = "", string ApiSoportePassword = "", string TokenType = "Bearer")
        {
            int intentos = 0;
            HttpResponseMessage request = null;
            using var cliente = new HttpClient();

            intentos++;
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
            request = await cliente.GetAsync(url);

            return await ObtenerRespuesta(request);
        }

        public async Task<HttpRequestResponse> Post(string url, object data)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage request = await client.PostAsync(url, httpContent);
            return await ObtenerRespuesta(request);
        }

        public async Task<HttpRequestResponse> Post(string url, object data, string Token, string ApiSoporteUsername = "", string ApiSoportePassword = "", string TokenType = "Bearer")
        {
            int intentos = 0;
            HttpResponseMessage request = null;
            using var cliente = new HttpClient();

            intentos++;
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
            request = await cliente.PostAsync(url, SerializarJson(data));

            return await ObtenerRespuesta(request);
        }

        public async Task<HttpRequestResponse> PostForm(string url, object data)
        {
            Dictionary<string, string> FormEncoded = JObject.FromObject(data).ToObject<Dictionary<string, string>>();
            HttpRequestMessage requestBody = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(FormEncoded) };
            HttpResponseMessage request = await client.SendAsync(requestBody);
            return await ObtenerRespuesta(request);
        }

        public async Task<HttpRequestResponse> Patch(string url, object data, string Token)
        {
            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage request = await cliente.PatchAsync(url, SerializarJson(data));
            return await ObtenerRespuesta(request);
        }

        public async Task<HttpRequestResponse>? Delete(string url, string Token, string TokenType = "Bearer")
        {
            try
            {
                using var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
                HttpResponseMessage request = await cliente.DeleteAsync(url);
                return await ObtenerRespuesta(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Helpers        

        private StringContent SerializarJson(object data)
        {
            string JSON = JsonConvert.SerializeObject(data);
            return new StringContent(JSON, Encoding.UTF8, "application/json");
        }

        private async Task<HttpRequestResponse> ObtenerRespuesta(HttpResponseMessage request)
        {
            try
            {
                string content = await request.Content.ReadAsStringAsync();
                return new HttpRequestResponse
                {
                    status = request.StatusCode,
                    response = content
                };
            }
            catch (Exception ex)
            {
                return new HttpRequestResponse
                {
                    status = HttpStatusCode.InternalServerError
                };
            }
        }

        #endregion    
    }
}
