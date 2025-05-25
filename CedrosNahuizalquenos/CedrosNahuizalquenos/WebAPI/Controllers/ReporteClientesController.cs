using CedrosNahuizalquenos.Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteClientesController : ControllerBase
    {
        private readonly IReporteCliente _reporteCliente;

        public ReporteClientesController(IReporteCliente reporteCliente)
        {
            _reporteCliente = reporteCliente;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> GetClientes(DateTime? fechaInicio, DateTime? fechaFin, string? clienteId)
        {
            var resultado = await _reporteCliente.ObtenerClientesAsync(fechaInicio, fechaFin, clienteId);
            return Ok(resultado);
        }
    }
}
