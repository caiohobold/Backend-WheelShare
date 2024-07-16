namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string toEmail, string token);
    }
}
