namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string email, string newPassword);
    }
}
