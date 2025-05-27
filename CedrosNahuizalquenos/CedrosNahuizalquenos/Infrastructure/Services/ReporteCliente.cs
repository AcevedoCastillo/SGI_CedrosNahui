using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class ReporteCliente : IReporteCliente
    {
        private readonly ApplicationDbContext _context;

        public ReporteCliente(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReporteClientesDTO>> ObtenerClientesAsync(DateTime? fechaInicio, DateTime? fechaFin, string? clienteId)
        {
            var fechaInicioParam = new SqlParameter("@FechaInicio", fechaInicio ?? (object)DBNull.Value);
            var fechaFinParam = new SqlParameter("@FechaFin", fechaFin ?? (object)DBNull.Value);
            var clienteIdParam = new SqlParameter("@ClienteID", clienteId ?? (object)DBNull.Value);

            return await _context.Set<ReporteClientesDTO>()
                .FromSqlRaw("EXEC ReportePedidosPorClientesConFiltros @FechaInicio, @FechaFin, @ClienteID",
                            fechaInicioParam, fechaFinParam, clienteIdParam)
                .ToListAsync();
        }
    }
}
