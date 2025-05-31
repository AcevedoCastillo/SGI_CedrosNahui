namespace CedrosNahuizalquenos.Client.Models
{
    public class DetallePedidoDTO
    {
        public int DetalleId { get; set; }

        public int PedidoId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

        public ProductoDTO? Producto { get; set; }
    }
}
