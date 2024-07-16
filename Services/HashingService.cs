namespace EmprestimosAPI.Services
{
    using EmprestimosAPI.Models;
    using Microsoft.AspNetCore.Identity;
    public class HashingService
    {
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();
        private readonly PasswordHasher<Associacao> _passwordAssocHasher = new PasswordHasher<Associacao>();

        public string HashPassword(Usuario usuario, string password)
        {
            return _passwordHasher.HashPassword(usuario, password);
        }

        public string HashAssocPassword(Associacao associacao, string password)
        {
            return _passwordAssocHasher.HashPassword(associacao, password);
        }

        public bool VerifyPassword(Usuario usuario, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public bool VerifyAssocPassword(Associacao associacao, string providedPassword)
        {
            var result = _passwordAssocHasher.VerifyHashedPassword(associacao, associacao.Senha, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
