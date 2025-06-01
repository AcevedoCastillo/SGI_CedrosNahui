namespace CedrosNahuizalquenos.Client.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public int RolId { get; set; } // 1 = Admin, 2 = Empleado
    }
}
