using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class ReporteProducto : IReporteProducto
    {
        private readonly ApplicationDbContext _context;

        public ReporteProducto(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ReporteProductoDTO>> ObtenerProductosAsync(DateTime? fechaInicio, DateTime? fechaFin, string? productoId)
        {
            var fechaInicioParam = new SqlParameter("@FechaInicio", fechaInicio ?? (object)DBNull.Value);
            var fechaFinParam = new SqlParameter("@FechaFin", fechaFin ?? (object)DBNull.Value);
            var productoIdParam = new SqlParameter("@ProductoID", productoId ?? (object)DBNull.Value);

            return await _context.Set<ReporteProductoDTO>()
                .FromSqlRaw("EXEC ReporteVentasPorProducto @FechaInicio, @FechaFin, @ProductoID",
                            fechaInicioParam, fechaFinParam, productoIdParam)
                .ToListAsync();
        }
    }
}
