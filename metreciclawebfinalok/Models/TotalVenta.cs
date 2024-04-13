using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class TotalVenta
{
    public int Idtotalventa { get; set; }

    public int? Idticketventa { get; set; }

    public int? Totalventa { get; set; }

    public virtual TicketVenta? IdticketventaNavigation { get; set; }
}
