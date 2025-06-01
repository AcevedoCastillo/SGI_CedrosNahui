using Azure.Core;
using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly EmailService _emailService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFacturaService _facturaService;
        public PedidoController(IPedidoService pedidoService, EmailService emailService, IClienteRepository cliente, IFacturaService facturaService)
        {
            _pedidoService = pedidoService;
            _emailService = emailService;
            _clienteRepository = cliente;
            _facturaService = facturaService;
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
        private async Task EnviarCorreoDeAprobacionAsync(PedidoDTO pedido)
        {
            // Obtener detalles del pedido
            var detalles = await _pedidoService.GetByIdAsync(pedido.PedidoId);
            var productos = detalles.Select(d => d.Producto.Nombre).ToList();

            // Obtener info del cliente
            var cliente = await _clienteRepository.GetByIdAsync(pedido.ClienteId);

            await _emailService.EnviarCorreoAprobacionAsync(
                correoDestino: cliente.Correo,
                nombreCliente: cliente.Nombre,
                productos: productos
            );
        }
        private async Task ActualizarFacturaAsync(PedidoDTO pedido)
        {
            var factura = await _facturaService.GetByIdAsync(pedido.PedidoId);
            if (factura != null)
            {
                factura.Enviada = true;
                await _facturaService.UpdateAsync(factura);
            }
        }
    }
}
