using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IPedidoService
    {
        Task<int> GuardarPedidoAsync(PedidoDTO pedidoDto);
    }
}
