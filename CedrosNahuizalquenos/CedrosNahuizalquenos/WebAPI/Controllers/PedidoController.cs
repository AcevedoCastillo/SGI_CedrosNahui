using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPedido([FromBody] PedidoDTO pedidoDto)
        {
            if (pedidoDto == null || pedidoDto.DetallePedidos == null || !pedidoDto.DetallePedidos.Any())
                return BadRequest(new { Mensaje = "El pedido debe contener al menos un detalle." });

            try
            {
                var id = await _pedidoService.GuardarPedidoAsync(pedidoDto);
                return Ok(new { PedidoID = id, Mensaje = "Pedido guardado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error al guardar el pedido", Detalle = ex.Message });
            }

        }
    }
}
