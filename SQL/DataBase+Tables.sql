-- Crear base de datos
CREATE DATABASE CedrosNahuizalquenosSGI;
GO

-- Usar la base de datos
USE CedrosNahuizalquenosSGI;
GO

-- Tabla: Usuarios (Empleados / Administradores)
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(MAX) NOT NULL,
    Rol INT NOT NULL, --1 Empleado / 2 Administrador
    FechaRegistro DATETIME NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);

-- Tabla: Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Correo NVARCHAR(100),
    Direccion NVARCHAR(200),
    FechaRegistro DATETIME NOT NULL
);

-- Tabla: Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(250),
    Precio DECIMAL(10,2) NOT NULL,
    ImagenURL NVARCHAR(MAX),
    FechaRegistro DATETIME NOT NULL,
    Activo BIT NOT NULL
);

-- Tabla: Pedidos
CREATE TABLE Pedidos (
    PedidoID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL,
    UsuarioID INT NOT NULL,
    FechaPedido DATETIME NOT NULL,
    MetodoPago VARCHAR(30) NOT NULL,
    Estado VARCHAR(30) NOT NULL, -- Por Validar, Aprobado
    Nota NVARCHAR(500),
    Total DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Pedidos_Clientes FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
    CONSTRAINT FK_Pedidos_Usuarios FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

-- Tabla: DetallePedido
CREATE TABLE DetallePedido (
    DetalleID INT PRIMARY KEY IDENTITY(1,1),
    PedidoID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_DetallePedido_Pedidos FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID),
    CONSTRAINT FK_DetallePedido_Productos FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);

-- Tabla: Facturas
CREATE TABLE Facturas (
    FacturaID INT PRIMARY KEY IDENTITY(1,1),
    PedidoID INT NOT NULL,
    FechaEmision DATETIME NOT NULL,
    Enviada BIT NOT NULL,

    CONSTRAINT FK_Facturas_Pedidos FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID)
);
