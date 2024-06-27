using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public int UsuarioId { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual Usuario? Usuario { get; set; }
}
