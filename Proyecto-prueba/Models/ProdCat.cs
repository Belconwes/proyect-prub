using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class ProdCat
{
    public int ProdcatId { get; set; }

    public int? ProductoId { get; set; }

    public int? CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual Producto? Producto { get; set; }
}
