using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class HistorialVisita
{
    public int HistorialId { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public DateTime? FechaVisita { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
