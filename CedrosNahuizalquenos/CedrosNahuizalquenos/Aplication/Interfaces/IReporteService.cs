using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IReporteService
    {
        Task<List<ReporteVentasDTO>> ObtenerVentasAsync(DateTime? fechaInicio, DateTime? fechaFin, string estado);
    }
}
