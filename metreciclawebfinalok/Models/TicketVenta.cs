using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class TicketVenta
{
    public int Idticketventa { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Idusuario { get; set; }

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();

    public virtual Usuario? IdusuarioNavigation { get; set; }

    public virtual ICollection<TotalVenta> TotalVenta { get; set; } = new List<TotalVenta>();
}
