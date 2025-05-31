using CedrosNahuizalquenos.Aplication.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteProductoController : ControllerBase
    {
        private readonly IReporteProducto _reporteProd;

        public ReporteProductoController(IReporteProducto reporteProducto)
        {
            _reporteProd = reporteProducto;
        }
        [HttpGet("productos")]
        public async Task<IActionResult> GetProducts(DateTime? fechaInicio, DateTime? fechaFin, string? productoId)
        {
            var productoIdfinal = string.IsNullOrEmpty(productoId) || productoId == "0" ? null : productoId;
            var resultado = await _reporteProd.ObtenerProductosAsync(fechaInicio, fechaFin, productoIdfinal);
            return Ok(resultado);
        }
        [HttpGet("productos/excel")]
        public async Task<IActionResult> ExportarProductosExcel(DateTime? fechaInicio, DateTime? fechaFin, string? productoId)
        {
            var productoIdfinal = string.IsNullOrEmpty(productoId) || productoId == "0" ? null : productoId;
            var resultado = await _reporteProd.ObtenerProductosAsync(fechaInicio, fechaFin, productoIdfinal);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Productos");
            worksheet.Cell(1, 1).Value = "ProductoID";
            worksheet.Cell(1, 2).Value = "NombreProducto";
            worksheet.Cell(1, 3).Value = "TotalCantidadVendida";
            worksheet.Cell(1, 4).Value = "TotalVentas";

            for (int i = 0; i < resultado.Count; i++)
            {
                var v = resultado[i];
                worksheet.Cell(i + 2, 1).Value = v.ProductoID;
                worksheet.Cell(i + 2, 2).Value = v.NombreProducto;
                worksheet.Cell(i + 2, 3).Value = v.TotalCantidadVendida;
                worksheet.Cell(i + 2, 4).Value = v.TotalVentas;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteClientes.xlsx");
        }
    }
}
