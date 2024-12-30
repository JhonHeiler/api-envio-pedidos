using System.Xml.Linq;
using ApiEnvioPedidos.Models;
using Microsoft.Extensions.Logging;

namespace ApiEnvioPedidos.Services
{
    public static class TransformService
    {
        private static readonly ILogger _logger = LoggerFactory.Create(builder => 
            builder.AddConsole()).CreateLogger("TransformService");

        public static string JsonToXml(PedidoRequest request)
        {
            _logger.LogInformation("Transformando solicitud JSON a XML...");

            var xmlDoc = new XDocument(
                new XElement("EnvioPedidoRequest",
                    new XElement("pedido", request.NumPedido),
                    new XElement("Cantidad", request.CantidadPedido),
                    new XElement("EAN", request.CodigoEAN),
                    new XElement("Producto", request.NombreProducto),
                    new XElement("Cedula", request.NumDocumento),
                    new XElement("Direccion", request.Direccion)
                )
            );

            _logger.LogInformation("Solicitud transformada exitosamente a XML: {Xml}", xmlDoc);
            return xmlDoc.ToString();
        }

        public static PedidoResponse XmlToJson(string xmlResponse)
        {
            _logger.LogInformation("Transformando respuesta XML a JSON...");

            var doc = XDocument.Parse(xmlResponse);

            var response = new PedidoResponse
            {
                CodigoEnvio = doc.Descendants("Codigo").FirstOrDefault()?.Value ?? "N/A",
                Estado = doc.Descendants("Mensaje").FirstOrDefault()?.Value ?? "Sin respuesta"
            };

            _logger.LogInformation("Respuesta transformada exitosamente a JSON: {Json}", response);
            return response;
        }
    }
}
