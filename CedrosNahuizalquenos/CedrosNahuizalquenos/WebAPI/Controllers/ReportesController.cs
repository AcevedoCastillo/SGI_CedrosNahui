using CedrosNahuizalquenos.Aplication.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("ventas")]
        public async Task<IActionResult> GetVentas(DateTime? fechaInicio, DateTime? fechaFin, string estado)
        {
            var estadoFinal = string.IsNullOrEmpty(estado) || estado == "Todos" ? null : estado;
            var resultado = await _reporteService.ObtenerVentasAsync(fechaInicio, fechaFin, estadoFinal);
            return Ok(resultado);
        }
        [HttpGet("ventas/excel")]
        public async Task<IActionResult> ExportarVentasExcel(DateTime? fechaInicio, DateTime? fechaFin, string estado)
        {
            var estadoFinal = string.IsNullOrEmpty(estado) || estado == "Todos" ? null : estado;
            var ventas = await _reporteService.ObtenerVentasAsync(fechaInicio, fechaFin, estadoFinal);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ventas");
            worksheet.Cell(1, 1).Value = "PedidoID";
            worksheet.Cell(1, 2).Value = "Cliente";
            worksheet.Cell(1, 3).Value = "FechaPedido";
            worksheet.Cell(1, 4).Value = "MetodoPago";
            worksheet.Cell(1, 5).Value = "Estado";
            worksheet.Cell(1, 6).Value = "Total";

            for (int i = 0; i < ventas.Count; i++)
            {
                var v = ventas[i];
                worksheet.Cell(i + 2, 1).Value = v.PedidoID;
                worksheet.Cell(i + 2, 2).Value = v.Cliente;
                worksheet.Cell(i + 2, 3).Value = v.FechaPedido.ToString("dd/MM/yyyy");
                worksheet.Cell(i + 2, 4).Value = v.MetodoPago;
                worksheet.Cell(i + 2, 5).Value = v.Estado;
                worksheet.Cell(i + 2, 6).Value = v.Total;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVentas.xlsx");
        }

    }
}
