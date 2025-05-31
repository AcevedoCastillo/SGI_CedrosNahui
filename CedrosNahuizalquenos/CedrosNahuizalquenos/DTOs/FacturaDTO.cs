namespace CedrosNahuizalquenos.DTOs
{
    public class FacturaDTO
    {
        public int FacturaId { get; set; }

        public int PedidoId { get; set; }

        public DateTime FechaEmision { get; set; }

        public bool Enviada { get; set; }
    }
}
