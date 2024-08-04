using EmprestimosAPI.Data;
using EmprestimosAPI.DTO.Usuario;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;
using EmprestimosAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace EmprestimosAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbEmprestimosContext _context;
        private readonly ILogger<UsuarioRepository> _logger;
        private readonly HashingService _hashingService;

        public UsuarioRepository(DbEmprestimosContext context, ILogger<UsuarioRepository> logger, HashingService hashingService)
        {
            _context = context;
            _logger = logger;
            _hashingService = hashingService;
        }

        public async Task<PagedList<Usuario>> GetAllUsers(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Usuarios
                .Where(u => u.IdAssociacao == idAssociacao)
                .Include(u => u.Associacao);

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Usuario>(items, count, pageNumber, pageSize);
        }

        public async Task<Usuario> GetUserById(int id, int idAssociacao)
        { 
            try
            {
                var user = await _context.Usuarios
                                         .Where(u => u.IdUsuario == id && u.IdAssociacao == idAssociacao)
                                         .Include(u => u.Associacao)
                                         .FirstOrDefaultAsync();

                if (user == null)
                {
                    _logger.LogError($"User with ID {id} not found.");
                }
                else if (user.Associacao == null)
                {
                    _logger.LogError($"User with ID {id} is missing association details. FK: {user.IdAssociacao}");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetUserById: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<Usuario> GetUserByEmailAsync(string email, int idAssociacao)
        {
            return await _context.Usuarios
                .Where(u => u.EmailPessoal == email && u.IdAssociacao == idAssociacao)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUserByEmailNoAssocAsync(string email)
        {
            return await _context.Usuarios
                .Where(u => u.EmailPessoal == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> AddUser(UsuarioCreateDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                NomeCompleto = usuarioDTO.NomeCompleto,
                EmailPessoal = usuarioDTO.EmailPessoal,
                NumeroTelefone = usuarioDTO.NumeroTelefone,
                Cpf = usuarioDTO.Cpf,
                DataNascimento = usuarioDTO.DataNascimento,
                Endereco = usuarioDTO.Endereco,
                IdAssociacao = usuarioDTO.IdAssociacao
            };

            usuario.SenhaHash = _hashingService.HashPassword(usuario, usuarioDTO.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateUser(Usuario usuario, int idAssociacao)
        {
            var existingUsuario = await _context.Usuarios
                .Where(u => u.IdUsuario == usuario.IdUsuario && u.IdAssociacao == idAssociacao)
                .FirstOrDefaultAsync();

            if (existingUsuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado ou não pertence à associação.");
            }

            _context.Entry(existingUsuario).CurrentValues.SetValues(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id, int idAssociacao)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.IdUsuario == id && u.IdAssociacao == idAssociacao)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado ou não pertence à associação.");
            }

            var temEmprestimo = await _context.Emprestimos
                .AnyAsync(e => e.IdUsuario == id);

            if (temEmprestimo)
            {
                throw new InvalidOperationException("Não é possível remover um usuário que está vinculado a empréstimos.");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
