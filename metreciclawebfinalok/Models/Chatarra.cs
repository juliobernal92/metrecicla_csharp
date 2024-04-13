using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class Chatarra
{
    public int Idchatarra { get; set; }

    public string? Nombre { get; set; }

    public int? Preciocompra { get; set; }

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual ICollection<PrecioVenta> PrecioVenta { get; set; } = new List<PrecioVenta>();
}
