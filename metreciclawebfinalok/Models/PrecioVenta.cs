using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class PrecioVenta
{
    public int Idprecioventa { get; set; }

    public int? Idchatarra { get; set; }

    public int? Idlocalventa { get; set; }

    public int? Precioventa { get; set; }

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();

    public virtual Chatarra? IdchatarraNavigation { get; set; }

    public virtual LocalVenta? IdlocalventaNavigation { get; set; }
}
