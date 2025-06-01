using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IPedidoService
    {
        Task<int> GuardarPedidoAsync(PedidoDTO pedidoDto);
        Task<List<DetallePedido?>> GetByIdAsync(int pedidoId);
    }
}
