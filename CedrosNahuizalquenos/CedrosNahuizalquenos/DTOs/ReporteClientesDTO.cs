namespace CedrosNahuizalquenos.DTOs
{
    public class ReporteClientesDTO
    {
        public int ClienteID { get; set; }
        public string? Cliente {  get; set; }
        public int? TotalPedidos { get; set; }
        public decimal? MontoTotal { get; set; }
        public DateTime? PrimerPedido { get; set; }
        public DateTime? UltimoPedido { get; set; }
    }
}
