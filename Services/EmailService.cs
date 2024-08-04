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
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Redefinição de Senha";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
            <html>
                <body>
                    <h2>Redefinição de Senha</h2>
                    <p>Olá,</p>
                    <p>Você solicitou a redefinição de sua senha. Sua nova senha é:</p>
                    <p><strong>{newPassword}</strong></p>
                    <p>Por favor, altere sua senha após o login.</p>
                    <br/>
                    <p>Atenciosamente,</p>
                    <p>Equipe {_emailSettings.SenderName}</p>
                </body>
            </html>";
                    bodyBuilder.TextBody = $@"
            Redefinição de Senha

            Olá,

            Você solicitou a redefinição de sua senha. Sua nova senha é:
            {newPassword}

            Por favor, altere sua senha após o login.

            Atenciosamente,
            Equipe {_emailSettings.SenderName}";

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

    }
}
