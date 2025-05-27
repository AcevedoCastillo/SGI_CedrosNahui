using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Infrastructure.Services;
using ClosedXML.Excel;
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
            var clienteIDfinal = string.IsNullOrEmpty(clienteId) || clienteId == "0" ? null : clienteId;
            var resultado = await _reporteCliente.ObtenerClientesAsync(fechaInicio, fechaFin, clienteIDfinal);
            return Ok(resultado);
        }
        [HttpGet("clientes/excel")]
        public async Task<IActionResult> ExportarVentasExcel(DateTime? fechaInicio, DateTime? fechaFin, string? clienteId)
        {
            var clienteIDfinal = string.IsNullOrEmpty(clienteId) || clienteId == "0" ? null : clienteId;
            var resultado = await _reporteCliente.ObtenerClientesAsync(fechaInicio, fechaFin, clienteIDfinal);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Clientes");
            worksheet.Cell(1, 1).Value = "ClienteID";
            worksheet.Cell(1, 2).Value = "Cliente";
            worksheet.Cell(1, 3).Value = "TotalPedidos";
            worksheet.Cell(1, 4).Value = "MontoTotal";
            worksheet.Cell(1, 5).Value = "PrimerPedido";
            worksheet.Cell(1, 6).Value = "UltimoPedido";

            for (int i = 0; i < resultado.Count; i++)
            {
                var v = resultado[i];
                worksheet.Cell(i + 2, 1).Value = v.ClienteID;
                worksheet.Cell(i + 2, 2).Value = v.Cliente;
                worksheet.Cell(i + 2, 3).Value = v.TotalPedidos;
                worksheet.Cell(i + 2, 4).Value = v.MontoTotal;
                worksheet.Cell(i + 2, 5).Value = v.PrimerPedido;
                worksheet.Cell(i + 2, 6).Value = v.UltimoPedido;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteClientes.xlsx");
        }
    }
}
