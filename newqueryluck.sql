create database pruebaf;
use pruebaf;

create table usuarios(
id_user int primary key identity(1,1),
rol nvarchar(10),
Nombre varchar(40),
Apellido varchar(40),
Email nvarchar(40),
Contraseña nvarchar(40),
);

CREATE TABLE Categorias (
    categoria_id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    nombre NCHAR(50)
);
GO

CREATE TABLE Productos (
    producto_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre VARCHAR(100),
	price decimal(12,2),
    imagen_producto VARBINARY(MAX),
    descripcion TEXT,
    porcentaje_ganancia INT NULL,
    valor_ganancia INT NULL,
    porcentaje_descuento INT NULL,
    categoria nvarchar (30),
    valor_descuento INT NULL,
);
GO

CREATE TABLE Pedidos (
    pedido_id INT IDENTITY PRIMARY KEY,
    usuario_id INT,
    fecha DATE,
    FOREIGN KEY (usuario_id) REFERENCES Usuarios(id_user)
);
GO

CREATE TABLE Carrito (
    carrito_id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    pedido_id INT NOT NULL,
    producto_id INT NOT NULL,
    cantidad INT,
    estado BIT,
    FOREIGN KEY (pedido_id) REFERENCES Pedidos(pedido_id),
    FOREIGN KEY (producto_id) REFERENCES Productos(producto_id)
);
GO

CREATE TABLE ProdCat (
    prodcat_id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    producto_id INT,
    categoria_id INT,
    UNIQUE (producto_id, categoria_id),
    FOREIGN KEY (producto_id) REFERENCES Productos(producto_id),
    FOREIGN KEY (categoria_id) REFERENCES Categorias(categoria_id)
);
GO

CREATE TABLE Proveedores (
    proveedor_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nombre NCHAR(100),
    telefono VARCHAR(15),
    descripcion TEXT
);
GO

CREATE TABLE Stock (
    stock_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    precio_ingresado DECIMAL(10, 2),
    talle NCHAR(10),
    cantidad INT,
    color VARCHAR(50),
    fecha DATE NOT NULL DEFAULT GETDATE(), 
    producto_id INT NOT NULL,
    proveedor_id INT NOT NULL,
    FOREIGN KEY (producto_id) REFERENCES Productos(producto_id),
    FOREIGN KEY (proveedor_id) REFERENCES Proveedores(proveedor_id)
);
GO

CREATE TABLE Favoritos (
    favorito_id INT IDENTITY (1,1) PRIMARY KEY,
    usuario_id INT,
    producto_id INT,
    FOREIGN KEY (usuario_id) REFERENCES Usuarios(id_user),
    FOREIGN KEY (producto_id) REFERENCES Productos(producto_id)
);
GO

CREATE TABLE HistorialVisitas (
    historial_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT,
    producto_id INT,
    fecha_visita DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (usuario_id) REFERENCES Usuarios(id_user),
    FOREIGN KEY (producto_id) REFERENCES Productos(producto_id)
);
GO

select * from Categorias;
select * from usuarios;


USE pruebaf; -- Cambia por el nombre de tu base de datos

ALTER TABLE Usuarios
ALTER COLUMN Contraseña NVARCHAR(256) ;

ALTER TABLE Usuarios
ALTER COLUMN rol NVARCHAR(MAX);