using ApiEnvioPedidos.Models;
using ApiEnvioPedidos.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiEnvioPedidos.Business
{
    public interface IPedidoService
    {
        Task<PedidoResponse> EnviarPedidoAsync(PedidoRequest request);
    }

    public class PedidoService : IPedidoService
    {
        private readonly ISoapClient _soapClient;
        private readonly ILogger<PedidoService> _logger;

        private const string SoapEndpoint = "https://run.mocky.io/v3/2271f10a-4c2e-4f98-9b75-f13c0db34236";

        public PedidoService(ISoapClient soapClient, ILogger<PedidoService> logger)
        {
            _soapClient = soapClient;
            _logger = logger;
        }

        public async Task<PedidoResponse> EnviarPedidoAsync(PedidoRequest request)
        {
            _logger.LogInformation("Procesando pedido para NumPedido: {NumPedido}", request.NumPedido);

            // Transformar JSON a XML
            string xmlRequest = TransformService.JsonToXml(request);
            _logger.LogInformation("XML generado correctamente: {XmlRequest}", xmlRequest);

            // Llamada al servicio SOAP
            string xmlResponse = await _soapClient.CallSoapServiceAsync(SoapEndpoint, xmlRequest);

            // Validar respuesta
            if (string.IsNullOrEmpty(xmlResponse))
            {
                _logger.LogWarning("La respuesta del servicio SOAP está vacía.");
                throw new HttpRequestException("Respuesta vacía del servicio externo.");
            }

            _logger.LogInformation("Respuesta XML recibida: {XmlResponse}", xmlResponse);

            // Transformar XML a JSON
            PedidoResponse jsonResponse = TransformService.XmlToJson(xmlResponse);
            _logger.LogInformation("Transformación exitosa a JSON.");

            return jsonResponse;
        }
    }
}
