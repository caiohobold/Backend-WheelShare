using EmprestimosAPI.Data;
using EmprestimosAPI.Interfaces.Account;
using EmprestimosAPI.Models;
using EmprestimosAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmprestimosAPI.Token
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly DbEmprestimosContext _context;
        private readonly IConfiguration _configuration;
        private readonly HashingService _hashingService;

        public AuthenticateService(DbEmprestimosContext context, IConfiguration configuration, HashingService hashingService)
        {
            _context = context;
            _configuration = configuration;
            _hashingService = hashingService;
            
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios.Where(x => x.EmailPessoal.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if(usuario == null)
            {
                return false;
            }

            return _hashingService.VerifyPassword(usuario, senha);
        }

        public async Task<bool> AuthenticateAssocAsync(string email, string senha)
        {
            var associacao = await _context.Associacoes.Where(x => x.EmailProfissional.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if(associacao == null)
            {
                return false;
            }
            return _hashingService.VerifyAssocPassword(associacao, senha);
        }

        public async Task<bool> UserExists(string email)
        {
            var usuario = await _context.Usuarios.Where(x => x.EmailPessoal.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if(usuario == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AssocExists(string email)
        {
            var associacao = await _context.Associacoes.Where(x => x.EmailProfissional.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if(associacao == null)
            {
                return false;
            }

            return true;
        }

        public string GenerateToken(int id, string user, string email, string role, int idAssoc, string nomeFantasia)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("user", user),
                new Claim("email", email),
                new Claim("role", role),
                new Claim("idAssoc", idAssoc.ToString()),
                new Claim("nomeFantasiaAssoc", nomeFantasia),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(60);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Associacao)
                .Where(x => x.EmailPessoal.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<Associacao> GetAssocByEmail(string email)
        {
            return await _context.Associacoes.Where(x => x.EmailProfissional.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }
    }
}
