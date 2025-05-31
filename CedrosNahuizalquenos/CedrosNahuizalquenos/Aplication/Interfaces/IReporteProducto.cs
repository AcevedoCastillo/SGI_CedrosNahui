using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IReporteProducto
    {
        Task<List<ReporteProductoDTO>> ObtenerProductosAsync(DateTime? fechaInicio, DateTime? fechaFin, string? productoId);
    }
}
