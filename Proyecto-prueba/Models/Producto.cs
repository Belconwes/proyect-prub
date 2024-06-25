using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string? Nombre { get; set; }

    public decimal? Price { get; set; }

    public byte[]? ImagenProducto { get; set; }

    public string? Descripcion { get; set; }

    public int? PorcentajeGanancia { get; set; }

    public int? ValorGanancia { get; set; }

    public int? PorcentajeDescuento { get; set; }

    public string? Categoria { get; set; }

    public int? ValorDescuento { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<HistorialVisita> HistorialVisita { get; set; } = new List<HistorialVisita>();

    public virtual ICollection<ProdCat> ProdCats { get; set; } = new List<ProdCat>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
