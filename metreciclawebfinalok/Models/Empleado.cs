using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetReciclaWebFinalOK.Models;

public partial class Empleado
{
    [Key]
    public int Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
