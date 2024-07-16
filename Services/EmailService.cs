using EmprestimosAPI.Interfaces.ServicesInterfaces;
using System.Net.Mail;

namespace EmprestimosAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendPasswordResetEmail(string toEmail, string token)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("caio.hobold@nextfit.com.br"),
                Subject = "Redefinição de Senha",
                Body = $"Por favor, utilize o seguinte token para redefinir sua senha: {token}",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
