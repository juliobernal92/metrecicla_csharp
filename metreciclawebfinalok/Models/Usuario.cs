using System;
using System.Collections.Generic;

namespace MetReciclaWebFinalOK.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public int? Cedula { get; set; }

    public string Contraseña { get; set; } = null!;

    public virtual Empleado? CedulaNavigation { get; set; }

    public virtual ICollection<TicketCompra> TicketCompras { get; set; } = new List<TicketCompra>();

    public virtual ICollection<TicketVenta> TicketVenta { get; set; } = new List<TicketVenta>();
}
