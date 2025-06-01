using System.Net;
using System.Net.Mail;
using System.Text;
namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class EmailService
    {
        private readonly string _correoOrigen = "diegocas2222@gmail.com";
        private readonly string _claveApp = "xqei fnyl etit mfmj "; // Clave de aplicación de Gmail

        public async Task EnviarCorreoAprobacionAsync(string correoDestino, string nombreCliente, List<string> productos)
        {
            var asunto = "¡Tu pedido ha sido aprobado!";
            var cuerpo = new StringBuilder();
            cuerpo.AppendLine($"Hola {nombreCliente},<br><br>");
            cuerpo.AppendLine("Nos complace informarte que tu pedido ha sido aprobado y está en proceso.<br><br>");
            cuerpo.AppendLine("Productos solicitados:<ul>");
            foreach (var producto in productos)
            {
                cuerpo.AppendLine($"<li>{producto}</li>");
            }
            cuerpo.AppendLine("</ul><br>¡Gracias por tu compra!");

            var mensaje = new MailMessage(_correoOrigen, correoDestino, asunto, cuerpo.ToString())
            {
                IsBodyHtml = true
            };

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_correoOrigen, _claveApp),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mensaje);
        }
    }
}
