using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class DetallesCompra
{
    public int Iddetallecompra { get; set; }

    public int? Idchatarra { get; set; }

    public int? Idticketcompra { get; set; }

    public decimal? Cantidad { get; set; }

    public double? Subtotal { get; set; }

    public virtual Chatarra? IdchatarraNavigation { get; set; }

    public virtual TicketCompra? IdticketcompraNavigation { get; set; }
}
