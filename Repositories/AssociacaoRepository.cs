using EmprestimosAPI.Data;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimosAPI.Repositories
{
    public class AssociacaoRepository : IAssociacaoRepository
    {
        private readonly DbEmprestimosContext _context;

        public AssociacaoRepository(DbEmprestimosContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Associacao>> GetAllAssocAsync(int pageNumber, int pageSize)
        {
            var query = _context.Associacoes.AsQueryable();

            return await PagedList<Associacao>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Associacao> GetAssocById(int id)
        {
            return await _context.Associacoes.FindAsync(id);
        }

        public async Task<Associacao> GetAssocByEmailAsync(string email)
        {
            return await _context.Associacoes
                .Where(u => u.EmailProfissional == email)
                .FirstOrDefaultAsync();
        }


        public async Task<Associacao> AddAssoc(Associacao associacao)
        {
            _context.Associacoes.Add(associacao);
            await _context.SaveChangesAsync();
            return associacao;
        }

        public async Task UpdateAssoc(Associacao associacao)
        {
            _context.Entry(associacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssoc(int id)
        {
            var associacao = await _context.Associacoes.FindAsync(id);
            if(associacao != null)
            {
                _context.Associacoes.Remove(associacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
