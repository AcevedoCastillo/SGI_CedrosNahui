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

        // GET: api/pedido
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            return Ok(pedidos);
        }

        // GET: api/pedido/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _pedidoService.GetPedidoByIdAsync(id);
            if (pedido == null)
                return NotFound(new { Mensaje = "Pedido no encontrado" });

            return Ok(pedido);
        }

        // GET: api/pedido/cliente/{clienteId}
        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetByClienteId(int clienteId)
        {
            var pedidos = await _pedidoService.GetPedidosByClienteIdAsync(clienteId);
            return Ok(pedidos);
        }

        // POST: api/pedido
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

        // DELETE: api/pedido/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _pedidoService.EliminarPedidoAsync(id);
            if (!eliminado)
                return NotFound(new { Mensaje = "No se pudo eliminar. Pedido no encontrado." });

            return Ok(new { Mensaje = "Pedido eliminado correctamente" });
        }

        // PUT: api/pedido/{id}/estado
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EstadoDTO dto)
        {
            var actualizado = await _pedidoService.ActualizarEstadoAsync(id, dto.Estado);
            if (!actualizado)
                return NotFound(new { Mensaje = "Pedido no encontrado" });

            return Ok(new { Mensaje = "Estado actualizado correctamente" });
        }

        // Opcional: enviar correo de aprobación (uso interno)
        private async Task EnviarCorreoDeAprobacionAsync(PedidoDTO pedido)
        {
            var detalles = await _pedidoService.GetByIdAsync(pedido.PedidoId);
            var productos = detalles.Select(d => d.Producto.Nombre).ToList();
            var cliente = await _clienteRepository.GetByIdAsync(pedido.ClienteId);

            await _emailService.EnviarCorreoAprobacionAsync(
                correoDestino: cliente.Correo,
                nombreCliente: cliente.Nombre,
                productos: productos
            );
        }
        [HttpPost("{id}/enviar-correo-aprobacion")]
        public async Task<IActionResult> EnviarCorreoAprobacion(int id)
        {
            var pedido = await _pedidoService.GetPedidoByIdAsync(id);
            if (pedido == null)
                return NotFound(new { Mensaje = "Pedido no encontrado" });

            try
            {
                var pedidoDto = new PedidoDTO
                {
                    PedidoId = pedido.PedidoId,
                    ClienteId = pedido.ClienteId
                    // Agrega más campos si los necesitas internamente
                };

                await EnviarCorreoDeAprobacionAsync(pedidoDto);
                return Ok(new { Mensaje = "Correo enviado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error al enviar correo", Detalle = ex.Message });
            }
        }
        // Opcional: actualizar factura (uso interno)
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

    // DTO para actualizar el estado
    public class EstadoDTO
    {
        public string Estado { get; set; } = string.Empty;
    }
}
