using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_prueba.Models;

public partial class PruebafContext : DbContext
{
    public PruebafContext()
    {
    }

    public PruebafContext(DbContextOptions<PruebafContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<HistorialVisita> HistorialVisitas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<ProdCat> ProdCats { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
       //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=(local); DataBase=pruebaf; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.CarritoId).HasName("PK__Carrito__8647FB090E55830F");

            entity.ToTable("Carrito");

            entity.Property(e => e.CarritoId).HasColumnName("carrito_id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carrito__pedido___59FA5E80");

            entity.HasOne(d => d.Producto).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carrito__product__5AEE82B9");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__DB875A4F91CBEF45");

            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.FavoritoId).HasName("PK__Favorito__B8BA20CA7FD28327");

            entity.Property(e => e.FavoritoId).HasColumnName("favorito_id");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Producto).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Favoritos__produ__619B8048");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Favoritos__usuar__60A75C0F");
        });

        modelBuilder.Entity<HistorialVisita>(entity =>
        {
            entity.HasKey(e => e.HistorialId).HasName("PK__Historia__68FE18EED1CDEEFF");

            entity.Property(e => e.HistorialId).HasColumnName("historial_id");
            entity.Property(e => e.FechaVisita)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_visita");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Producto).WithMany(p => p.HistorialVisita)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Historial__produ__66603565");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialVisita)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Historial__usuar__656C112C");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PK__Pedidos__CBE76076632BEF93");

            entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Pedidos__usuario__571DF1D5");
        });

        modelBuilder.Entity<ProdCat>(entity =>
        {
            entity.HasKey(e => e.ProdcatId).HasName("PK__ProdCat__BC99016BC13050F3");

            entity.ToTable("ProdCat");

            entity.HasIndex(e => new { e.ProductoId, e.CategoriaId }, "UQ__ProdCat__16E49B49E9BFD403").IsUnique();

            entity.Property(e => e.ProdcatId).HasColumnName("prodcat_id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");

            entity.HasOne(d => d.Categoria).WithMany(p => p.ProdCats)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__ProdCat__categor__4316F928");

            entity.HasOne(d => d.Producto).WithMany(p => p.ProdCats)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__ProdCat__product__4222D4EF");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__FB5CEEECE9AC0D6B");

            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(30)
                .HasColumnName("categoria");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.ImagenProducto).HasColumnName("imagen_producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PorcentajeDescuento).HasColumnName("porcentaje_descuento");
            entity.Property(e => e.PorcentajeGanancia).HasColumnName("porcentaje_ganancia");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ValorDescuento).HasColumnName("valor_descuento");
            entity.Property(e => e.ValorGanancia).HasColumnName("valor_ganancia");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PK__Proveedo__88BBADA4059C4F8E");

            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stock__E86668626ABB0D1B");

            entity.ToTable("Stock");

            entity.Property(e => e.StockId).HasColumnName("stock_id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.PrecioIngresado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_ingresado");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");
            entity.Property(e => e.Talle)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("talle");

            entity.HasOne(d => d.Producto).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__producto___48CFD27E");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__proveedor__49C3F6B7");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__usuarios__D2D146375F65DDD3");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Apellido)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(40);
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(10)
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
