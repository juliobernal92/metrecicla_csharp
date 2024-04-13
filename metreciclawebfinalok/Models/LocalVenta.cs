using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class LocalVenta
{
    public int Idlocalventa { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<PrecioVenta> PrecioVenta { get; set; } = new List<PrecioVenta>();
}
