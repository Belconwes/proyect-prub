using System.Net.Mail;
using System.Net;

namespace Proyecto_prueba.Servicios
{
    public class IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com"; // Coloca tu servidor SMTP aquí
                    smtp.Port = 587; // Puerto SMTP (por ejemplo, 587 para TLS/STARTTLS)
                    smtp.EnableSsl = true; // Habilita SSL/TLS si es necesario
                    smtp.Credentials = new NetworkCredential("gaunaabeltiago@gmail.com", "your-password"); // Credenciales del correo electrónico

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("gaunaabeltiago@gmail.com"); // Tu dirección de correo electrónico
                    mail.To.Add(email); // Destinatario del correo
                    mail.Subject = subject; // Asunto del correo
                    mail.Body = message; // Cuerpo del correo
                    mail.IsBodyHtml = true; // Si el cuerpo del correo es HTML

                    await smtp.SendMailAsync(mail); // Envía el correo electrónico
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores al enviar el correo electrónico
                throw new InvalidOperationException($"No se pudo enviar el correo electrónico. Error: {ex.Message}");
            }
        }
    }
}
