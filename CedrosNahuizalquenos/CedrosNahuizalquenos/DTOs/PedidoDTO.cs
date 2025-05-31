using CedrosNahuizalquenos.Client.Models;

namespace CedrosNahuizalquenos.DTOs
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }

        public int ClienteId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaPedido { get; set; }

        public string MetodoPago { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public string? Nota { get; set; }

        public decimal Total { get; set; }

        public ClienteDTO? Cliente { get; set; }

        public UsuarioDTO? Usuario { get; set; }

        public List<DetallePedidoDTO>? DetallePedidos { get; set; }

        public List<FacturaDTO>? Facturas { get; set; }
    }

}
