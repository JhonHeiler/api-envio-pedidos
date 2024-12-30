using System.ComponentModel.DataAnnotations;

namespace ApiEnvioPedidos.Models
{
    public class PedidoResponse
    {
        [Required(ErrorMessage = "El código de envío es obligatorio.")]
        public string CodigoEnvio { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(100, ErrorMessage = "El estado no debe exceder los 100 caracteres.")]
        public string Estado { get; set; }
    }
}
