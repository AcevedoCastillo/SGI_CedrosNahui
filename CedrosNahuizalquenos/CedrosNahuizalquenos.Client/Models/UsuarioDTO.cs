namespace CedrosNahuizalquenos.Client.Models
{
    public class UsuarioDTO
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public int Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
