using System;
using System.Collections.Generic;

namespace Proyecto_prueba.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<ProdCat> ProdCats { get; set; } = new List<ProdCat>();
}
