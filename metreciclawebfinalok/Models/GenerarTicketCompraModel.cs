using System.ComponentModel.DataAnnotations;

namespace MetReciclaWebFinalOK.Models
{
    public partial class GenerarTicketCompraModel
    {
        [Key]
        public int Iddetallecompra { get; set; }
        public int Idchatarra { get; set; }
        public int Idticketcompra { get; set; }
        public decimal Cantidad { get; set; }
        public float Subtotal { get; set; }
        public DateTime Fecha { get; set; }
        public int Idvendedor { get; set; }
        public int Idusuario { get; set; }
        public int Idtotalcompra { get; set; }
        public int Total { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public int? Idempleado { get; set; }
        public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    }
}
