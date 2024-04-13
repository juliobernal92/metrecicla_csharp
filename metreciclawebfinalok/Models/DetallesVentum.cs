using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class DetallesVentum
{
    public int Iddetallesventa { get; set; }

    public int? Idprecioventa { get; set; }

    public decimal? Cantidad { get; set; }

    public int? Subtotal { get; set; }

    public int? Idticketventa { get; set; }

    public virtual PrecioVenta? IdprecioventaNavigation { get; set; }

    public virtual TicketVenta? IdticketventaNavigation { get; set; }
}
