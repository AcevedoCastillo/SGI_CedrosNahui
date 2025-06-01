using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IPedidoService
    {
        Task<int> GuardarPedidoAsync(PedidoDTO pedidoDto);
        Task<List<DetallePedido?>> GetByIdAsync(int pedidoId);

        Task<List<PedidoDTO>> GetAllAsync();

        Task<Pedido?> GetPedidoByIdAsync(int pedidoId);
        Task<IEnumerable<Pedido>> GetPedidosByClienteIdAsync(int clienteId); 
        Task<bool> EliminarPedidoAsync(int pedidoId);
        Task<bool> ActualizarEstadoAsync(int pedidoId, string nuevoEstado);
    }
}
