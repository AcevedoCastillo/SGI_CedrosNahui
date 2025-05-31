-- =============================================
-- Author:		Diego Castillo
-- Create date: 2025-05-31
-- Description:	Procedimiento para obtener las ventas por producto
-- =============================================
CREATE OR ALTER PROCEDURE ReporteVentasPorProducto
    @FechaInicio DATETIME = NULL,
    @FechaFin DATETIME = NULL,
    @ProductoID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.ProductoID,
        p.Nombre AS NombreProducto,
        SUM(dp.Cantidad) AS TotalCantidadVendida,
        SUM(dp.Subtotal) AS TotalVentas
    FROM DetallePedido dp
    INNER JOIN Productos p ON dp.ProductoID = p.ProductoID
    INNER JOIN Pedidos ped ON dp.PedidoID = ped.PedidoID
    WHERE 
        (@FechaInicio IS NULL OR ped.FechaPedido >= @FechaInicio) AND
        (@FechaFin IS NULL OR ped.FechaPedido <= @FechaFin) AND
        (@ProductoID IS NULL OR p.ProductoID = @ProductoID) AND
        ped.Estado = 'Aprobado'
    GROUP BY 
        p.ProductoID, p.Nombre
    ORDER BY 
        TotalVentas DESC;
END;

