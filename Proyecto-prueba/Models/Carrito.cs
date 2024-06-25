using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class Carrito
{
    public int CarritoId { get; set; }

    public int PedidoId { get; set; }

    public int ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public bool? Estado { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
