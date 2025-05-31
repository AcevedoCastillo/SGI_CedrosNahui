namespace CedrosNahuizalquenos.Client.Models
{
    public class ReporteProductosDTO
    {
        public int ProductoID { get; set; }
        public string? NombreProducto { get; set; }
        public int TotalCantidadVendida { get; set; }
        public decimal TotalVentas { get; set; }
    }
}
