using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_prueba.Migrations
{
    /// <inheritdoc />
    public partial class firstchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    categoria_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__DB875A4F91CBEF45", x => x.categoria_id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    producto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    price = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    imagen_producto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    porcentaje_ganancia = table.Column<int>(type: "int", nullable: true),
                    valor_ganancia = table.Column<int>(type: "int", nullable: true),
                    porcentaje_descuento = table.Column<int>(type: "int", nullable: true),
                    categoria = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    valor_descuento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__FB5CEEECE9AC0D6B", x => x.producto_id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    proveedor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__88BBADA4059C4F8E", x => x.proveedor_id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Nombre = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    Apellido = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Contraseña = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuarios__D2D146375F65DDD3", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "ProdCat",
                columns: table => new
                {
                    prodcat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    producto_id = table.Column<int>(type: "int", nullable: true),
                    categoria_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProdCat__BC99016BC13050F3", x => x.prodcat_id);
                    table.ForeignKey(
                        name: "FK__ProdCat__categor__4316F928",
                        column: x => x.categoria_id,
                        principalTable: "Categorias",
                        principalColumn: "categoria_id");
                    table.ForeignKey(
                        name: "FK__ProdCat__product__4222D4EF",
                        column: x => x.producto_id,
                        principalTable: "Productos",
                        principalColumn: "producto_id");
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    stock_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    precio_ingresado = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    talle = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    proveedor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stock__E86668626ABB0D1B", x => x.stock_id);
                    table.ForeignKey(
                        name: "FK__Stock__producto___48CFD27E",
                        column: x => x.producto_id,
                        principalTable: "Productos",
                        principalColumn: "producto_id");
                    table.ForeignKey(
                        name: "FK__Stock__proveedor__49C3F6B7",
                        column: x => x.proveedor_id,
                        principalTable: "Proveedores",
                        principalColumn: "proveedor_id");
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    favorito_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    producto_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorito__B8BA20CA7FD28327", x => x.favorito_id);
                    table.ForeignKey(
                        name: "FK__Favoritos__produ__619B8048",
                        column: x => x.producto_id,
                        principalTable: "Productos",
                        principalColumn: "producto_id");
                    table.ForeignKey(
                        name: "FK__Favoritos__usuar__60A75C0F",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "HistorialVisitas",
                columns: table => new
                {
                    historial_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    producto_id = table.Column<int>(type: "int", nullable: true),
                    fecha_visita = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__68FE18EED1CDEEFF", x => x.historial_id);
                    table.ForeignKey(
                        name: "FK__Historial__produ__66603565",
                        column: x => x.producto_id,
                        principalTable: "Productos",
                        principalColumn: "producto_id");
                    table.ForeignKey(
                        name: "FK__Historial__usuar__656C112C",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    pedido_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pedidos__CBE76076632BEF93", x => x.pedido_id);
                    table.ForeignKey(
                        name: "FK__Pedidos__usuario__571DF1D5",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Carrito",
                columns: table => new
                {
                    carrito_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pedido_id = table.Column<int>(type: "int", nullable: false),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carrito__8647FB090E55830F", x => x.carrito_id);
                    table.ForeignKey(
                        name: "FK__Carrito__pedido___59FA5E80",
                        column: x => x.pedido_id,
                        principalTable: "Pedidos",
                        principalColumn: "pedido_id");
                    table.ForeignKey(
                        name: "FK__Carrito__product__5AEE82B9",
                        column: x => x.producto_id,
                        principalTable: "Productos",
                        principalColumn: "producto_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_pedido_id",
                table: "Carrito",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_producto_id",
                table: "Carrito",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_producto_id",
                table: "Favoritos",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_usuario_id",
                table: "Favoritos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialVisitas_producto_id",
                table: "HistorialVisitas",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialVisitas_usuario_id",
                table: "HistorialVisitas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_usuario_id",
                table: "Pedidos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProdCat_categoria_id",
                table: "ProdCat",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ProdCat__16E49B49E9BFD403",
                table: "ProdCat",
                columns: new[] { "producto_id", "categoria_id" },
                unique: true,
                filter: "[producto_id] IS NOT NULL AND [categoria_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_producto_id",
                table: "Stock",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_proveedor_id",
                table: "Stock",
                column: "proveedor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrito");

            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "HistorialVisitas");

            migrationBuilder.DropTable(
                name: "ProdCat");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
