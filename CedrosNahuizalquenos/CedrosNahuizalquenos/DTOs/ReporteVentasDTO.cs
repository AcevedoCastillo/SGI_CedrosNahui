namespace CedrosNahuizalquenos.DTOs
{
    public class ReporteVentasDTO
    {
        public int PedidoID { get; set; }
        public string? Cliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public string? MetodoPago { get; set; }
        public string? Estado { get; set; }
        public decimal Total { get; set; }
    }
}
