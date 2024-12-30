using System.ComponentModel.DataAnnotations;

namespace ApiEnvioPedidos.Models
{
    public class PedidoRequest
    {
        [Required(ErrorMessage = "El número de pedido es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de pedido no debe exceder los 20 caracteres.")]
        public string NumPedido { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int CantidadPedido { get; set; }

        [Required(ErrorMessage = "El código EAN es obligatorio.")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "El código EAN debe tener exactamente 18 dígitos.")]
        public string CodigoEAN { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del producto no debe exceder los 100 caracteres.")]
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [RegularExpression(@"^\d{7,10}$", ErrorMessage = "El número de documento debe tener entre 7 y 10 dígitos.")]
        public string NumDocumento { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no debe exceder los 200 caracteres.")]
        public string Direccion { get; set; }
    }
}
