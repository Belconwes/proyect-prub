using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public decimal? PrecioIngresado { get; set; }

    public string? Talle { get; set; }

    public int? Cantidad { get; set; }

    public string? Color { get; set; }

    public DateOnly Fecha { get; set; }

    public int ProductoId { get; set; }

    public int ProveedorId { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Proveedore Proveedor { get; set; } = null!;
}
