using EmprestimosAPI.Interfaces.ServicesInterfaces;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using WheelShareAPI.Helpers;
using Microsoft.Extensions.Options;

namespace EmprestimosAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendResetPasswordEmailAsync(string email, string newPassword)
        {

            var domain = email.Split('@').Last();
            if (!_emailSettings.Providers.ContainsKey(domain))
            {
                domain = _emailSettings.DefaultProvider; // Usa o provedor padrão se não encontrar um específico
            }

            var providerSettings = _emailSettings.Providers[domain];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(providerSettings.SenderName, providerSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Redefinição de Senha";
            message.Body = new TextPart("plain")
            {
                Text = $"Sua nova senha é: {newPassword}"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(providerSettings.SmtpServer, providerSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(providerSettings.SenderEmail, providerSettings.SenderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }
    }
}
