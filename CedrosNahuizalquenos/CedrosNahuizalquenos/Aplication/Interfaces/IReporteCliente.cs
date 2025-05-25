using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IReporteCliente
    {
        Task<List<ReporteClientesDTO>> ObtenerClientesAsync(DateTime? fechaInicio, DateTime? fechaFin, string? clienteId);
    }
}
