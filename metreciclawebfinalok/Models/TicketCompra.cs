using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class TicketCompra
{
    public int Idticketcompra { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Idvendedor { get; set; }

    public int? Idusuario { get; set; }

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual Usuario? IdusuarioNavigation { get; set; }

    public virtual Vendedor? IdvendedorNavigation { get; set; }

    public virtual ICollection<TotalCompra> TotalCompras { get; set; } = new List<TotalCompra>();
}
