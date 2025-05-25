-- =============================================
-- Author:		Michelle Zelada
-- Create date: 2025-05-24
-- Description:	Procedimiento para traer las compras filtradas por cliente
-- =============================================
CREATE PROCEDURE ReportePedidosPorClientesConFiltros
    @FechaInicio DATETIME = NULL,
    @FechaFin DATETIME = NULL,
    @ClienteID INT = NULL
AS
BEGIN
    SELECT 
        C.ClienteID,
        C.Nombre AS Cliente,
        COUNT(P.PedidoID) AS TotalPedidos,
        SUM(P.Total) AS MontoTotal,
        MIN(P.FechaPedido) AS PrimerPedido,
        MAX(P.FechaPedido) AS UltimoPedido
    FROM Clientes C
    LEFT JOIN Pedidos P ON C.ClienteID = P.ClienteID
        AND (@FechaInicio IS NULL OR P.FechaPedido >= @FechaInicio)
        AND (@FechaFin IS NULL OR P.FechaPedido <= @FechaFin)
    WHERE (@ClienteID IS NULL OR C.ClienteID = @ClienteID)
    GROUP BY C.ClienteID, C.Nombre
    ORDER BY MontoTotal DESC;
END;

