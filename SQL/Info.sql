-- Insertar Usuarios (Empleados/Admins)
INSERT INTO Usuarios (Nombre, Correo, Contrasena, Rol, FechaRegistro, Activo)
VALUES
('Michelle Zelada', 'michelle@erp.com', 'pass123', 1, GETDATE(), 1),
('Carlos Vásquez', 'carlos@erp.com', 'pass456', 2, GETDATE(), 1);

-- Insertar Clientes
INSERT INTO Clientes (Nombre, Telefono, Correo, Direccion, FechaRegistro)
VALUES
('Juan Pérez', '70112233', 'juan@gmail.com', 'San Salvador', GETDATE()),
('Ana Ramírez', '74125698', 'ana@gmail.com', 'Santa Ana', GETDATE());

-- Insertar Productos
INSERT INTO Productos (Nombre, Descripcion, Precio, ImagenURL, FechaRegistro, Activo)
VALUES
('Pantry de Madera', 'Pantry color roble', 250.00, 'pantry1.jpg', GETDATE(), 1),
('Mesa de Cocina', 'Mesa rectangular blanca', 180.00, 'mesa.jpg', GETDATE(), 1);

-- Insertar Pedidos
INSERT INTO Pedidos (ClienteID, UsuarioID, FechaPedido, MetodoPago, Estado, Nota, Total)
VALUES
(1, 1, '2025-05-20', 'Efectivo', 'Aprobado', 'Entrega rápida', 250.00),
(2, 2, '2025-05-22', 'Transferencia', 'Por Validar', '', 180.00),
(1, 2, '2025-05-23', 'Tarjeta', 'Aprobado', '', 430.00); -- Pedido con 2 productos

-- Insertar Detalle de Pedidos
INSERT INTO DetallePedido (PedidoID, ProductoID, Cantidad, PrecioUnitario, Subtotal)
VALUES
(1, 1, 1, 250.00, 250.00),
(2, 2, 1, 180.00, 180.00),
(3, 1, 1, 250.00, 250.00),
(3, 2, 1, 180.00, 180.00);

-- Insertar Facturas
INSERT INTO Facturas (PedidoID, FechaEmision, Enviada)
VALUES
(1, '2025-05-21', 1),
(3, '2025-05-23', 0);
