using System.ComponentModel.DataAnnotations;

namespace MetReciclaWebFinalOK.Models
{
    public class ChatarraVMRest
    {
        [Key]
        public int Idchatarra { get; set; }

        public string Nombre { get; set; }

        public int Preciocompra { get; set; }
    }
}
