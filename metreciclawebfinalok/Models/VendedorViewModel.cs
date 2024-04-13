using System.ComponentModel.DataAnnotations;

namespace MetReciclaWebFinalOK.Models
{
    public class VendedorViewModel
    {
        [Key]
        public int Idvendedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
