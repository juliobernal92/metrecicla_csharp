using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class TotalCompra
{
    public int Idtotalcompra { get; set; }

    public int? Idticketcompra { get; set; }

    public int? Total { get; set; }

    public virtual TicketCompra? IdticketcompraNavigation { get; set; }
}
