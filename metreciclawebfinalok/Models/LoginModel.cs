using System.ComponentModel.DataAnnotations;

namespace MetReciclaWebFinalOK.Models
{
    public partial class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "El campo Cédula es obligatorio")]
        public int? Cedula { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string? Contraseña { get; set; }

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}