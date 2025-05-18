using System;
using System.Collections.Generic;

namespace CedrosNahuizalquenos.Domain.Entities;

public partial class Factura
{
    public int FacturaId { get; set; }

    public int PedidoId { get; set; }

    public DateTime FechaEmision { get; set; }

    public bool Enviada { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;
}
