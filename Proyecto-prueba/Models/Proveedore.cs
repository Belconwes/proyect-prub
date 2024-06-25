using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class Proveedore
{
    public int ProveedorId { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
