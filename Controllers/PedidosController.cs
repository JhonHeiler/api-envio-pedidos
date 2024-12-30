using Microsoft.AspNetCore.Mvc;
using ApiEnvioPedidos.Models;
using ApiEnvioPedidos.Business;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ApiEnvioPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IPedidoService _pedidoService;

        public PedidosController(ILogger<PedidosController> logger, IPedidoService pedidoService)
        {
            _logger = logger;
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarPedido([FromBody] PedidoRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Solicitud con datos no válidos.");
                return BadRequest(ModelState);
            }

            try
            {
                PedidoResponse response = await _pedidoService.EnviarPedidoAsync(request);
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error al llamar al servicio SOAP: {Message}", ex.Message);
                return StatusCode(502, "Error en el servicio externo.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error inesperado: {Message}", ex.Message);
                return StatusCode(500, "Error interno en la API.");
            }
        }
    }
}
