using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using System;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
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

                // (Opcional) Generar factura si viene en el DTO
                if (pedidoDto.Facturas != null)
                {
                    foreach (var facturaDto in pedidoDto.Facturas)
                    {
                        var factura = new Factura
                        {
                            PedidoId = pedido.PedidoId,
                            FechaEmision = facturaDto.FechaEmision,
                            Enviada = facturaDto.Enviada
                        };
                        _context.Facturas.Add(factura);
                    }
                }

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
    }
}
