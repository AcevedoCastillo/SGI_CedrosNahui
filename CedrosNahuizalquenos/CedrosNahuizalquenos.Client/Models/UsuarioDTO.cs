namespace CedrosNahuizalquenos.Client.Models
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public int Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; } // Nullable
    }
}
