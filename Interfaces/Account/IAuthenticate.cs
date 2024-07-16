using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        Task<bool> AuthenticateAssocAsync(string email, string senha);
        Task<bool> UserExists(string email);
        Task<bool> AssocExists(string email);
        public string GenerateToken(int id, string email, string user, string role, int idAssoc, string nomeFantasia);
        public Task<Usuario> GetUserByEmail(string email);
        public Task<Associacao> GetAssocByEmail(string email);
    }
}
