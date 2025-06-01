using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Client.Pages.Pedidos;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetallePedido?>> GetByIdAsync(int pedidoId)
        {
            return await _context.DetallePedidos
                .Include(dp => dp.Producto)
                .Where(dp => dp.PedidoId == pedidoId)
                .ToListAsync();
        }

        public async Task<int> GuardarPedidoAsync(PedidoDTO pedidoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Calcular total
                decimal total = pedidoDto.DetallePedidos?.Sum(d => d.Cantidad * d.PrecioUnitario) ?? 0;

                // Crear entidad Pedido
                var pedido = new Pedido
                {
                    ClienteId = pedidoDto.ClienteId,
                    UsuarioId = pedidoDto.UsuarioId,
                    FechaPedido = DateTime.Now,
                    MetodoPago = pedidoDto.MetodoPago,
                    Estado = "Por Validar",
                    Nota = pedidoDto.Nota,
                    Total = total
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync(); // Necesario para obtener el PedidoID

                // Agregar detalles
                if (pedidoDto.DetallePedidos != null)
                {
                    foreach (var detalleDto in pedidoDto.DetallePedidos)
                    {
                        var detalle = new DetallePedido
                        {
                            PedidoId = pedido.PedidoId,
                            ProductoId = detalleDto.ProductoId,
                            Cantidad = detalleDto.Cantidad,
                            PrecioUnitario = detalleDto.PrecioUnitario,
                            Subtotal = detalleDto.Cantidad * detalleDto.PrecioUnitario
                        };
                        _context.DetallePedidos.Add(detalle);
                    }
                }

                // Generar factura automáticamente (sin depender del DTO)
                var factura = new Factura
                {
                    PedidoId = pedido.PedidoId,
                    FechaEmision = DateTime.Now,
                    Enviada = false 
                };

                _context.Facturas.Add(factura);


                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return pedido.PedidoId;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<List<PedidoDTO>> GetAllAsync()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Usuario)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(dp => dp.Producto)
                .ToListAsync();

            return pedidos.Select(p => new PedidoDTO
            {
                PedidoId = p.PedidoId,
                ClienteId = p.ClienteId,
                UsuarioId = p.UsuarioId,
                FechaPedido = p.FechaPedido,
                MetodoPago = p.MetodoPago,
                Estado = p.Estado,
                Nota = p.Nota,
                Total = p.Total,
                Cliente = p.Cliente != null ? new ClienteDTO
                {
                    ClienteId = p.Cliente.ClienteId,
                    Nombre = p.Cliente.Nombre
                } : null,
                Usuario = p.Usuario != null ? new UsuarioDTO
                {
                    UsuarioId = p.Usuario.UsuarioId,
                    Nombre = p.Usuario.Nombre
                } : null,
                DetallePedidos = p.DetallePedidos.Select(dp => new DetallePedidoDTO
                {
                    ProductoId = dp.ProductoId,
                    Cantidad = dp.Cantidad,
                    PrecioUnitario = dp.PrecioUnitario,
                    Subtotal = dp.Subtotal,
                    Producto = dp.Producto != null ? new ProductoDTO
                    {
                        ProductoId = dp.Producto.ProductoId,
                        Nombre = dp.Producto.Nombre
                    } : null
                }).ToList()
            }).ToList();
        }
        public async Task<Pedido?> GetPedidoByIdAsync(int pedidoId)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)  // Opcional
                .Include(p => p.Usuario)  // Opcional
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
        }
        public async Task<IEnumerable<Pedido>> GetPedidosByClienteIdAsync(int clienteId)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }
        public async Task<bool> EliminarPedidoAsync(int pedidoId)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                return false;

            // Opcional: eliminar también detalles y factura si existen
            var detalles = _context.DetallePedidos.Where(d => d.PedidoId == pedidoId);
            _context.DetallePedidos.RemoveRange(detalles);

            var factura = await _context.Facturas.FirstOrDefaultAsync(f => f.PedidoId == pedidoId);
            if (factura != null)
                _context.Facturas.Remove(factura);

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActualizarEstadoAsync(int pedidoId, string nuevoEstado)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                return false;

            pedido.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
