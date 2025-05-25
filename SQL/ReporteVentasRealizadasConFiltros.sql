-- =============================================
-- Author:		Michelle Zelada
-- Create date: 2025-05-24
-- Description:	Procedimiento para traer las ventas realizadas
-- =============================================
CREATE PROCEDURE ReporteVentasRealizadasConFiltros
    @FechaInicio DATETIME = NULL,
    @FechaFin DATETIME = NULL,
    @Estado VARCHAR(30) = NULL -- Opcional: 'Aprobado', 'Por Validar', etc.
AS
BEGIN
    SELECT 
        P.PedidoID,
        C.Nombre AS Cliente,
        U.Nombre AS Empleado,
        P.FechaPedido,
        P.MetodoPago,
        P.Estado,
        P.Total
    FROM Pedidos P
    INNER JOIN Clientes C ON P.ClienteID = C.ClienteID
    INNER JOIN Usuarios U ON P.UsuarioID = U.UsuarioID
    WHERE 
        (@FechaInicio IS NULL OR P.FechaPedido >= @FechaInicio) AND
        (@FechaFin IS NULL OR P.FechaPedido <= @FechaFin) AND
        (@Estado IS NULL OR P.Estado = @Estado)
    ORDER BY P.FechaPedido DESC;
END;
