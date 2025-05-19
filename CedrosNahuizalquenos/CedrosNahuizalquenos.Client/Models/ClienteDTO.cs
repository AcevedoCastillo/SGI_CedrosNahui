namespace CedrosNahuizalquenos.Client.Models
{
    public class ClienteDTO
    {
        public int ClienteId { get; set; }  // Opcional en POST, requerido en PUT
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
