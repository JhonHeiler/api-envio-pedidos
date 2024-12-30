using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApiEnvioPedidos.Services
{
    public interface ISoapClient
    {
        Task<string> CallSoapServiceAsync(string url, string xmlRequest);
    }

    public class SoapClient : ISoapClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SoapClient> _logger;

        public SoapClient(HttpClient httpClient, ILogger<SoapClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> CallSoapServiceAsync(string url, string xmlRequest)
        {
            var content = new StringContent(xmlRequest, System.Text.Encoding.UTF8, "application/xml");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
