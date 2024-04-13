using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MetReciclaWebFinalOK.Models;

public partial class Vendedor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Idvendedor { get; set; }

    //[Required(ErrorMessage = "El nombre es obligatorio.")]
    public string? Nombre { get; set; }

    //[Required(ErrorMessage = "El Telefono es obligatorio.")]
    public string? Telefono { get; set; }
    //[Required(ErrorMessage = "La direccion es obligatoria.")]
    public string? Direccion { get; set; }
    [JsonIgnore]
    public virtual ICollection<TicketCompra> TicketCompras { get; set; } = new List<TicketCompra>();
}
