using EmprestimosAPI.Data;
using EmprestimosAPI.DTO.Local;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimosAPI.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly DbEmprestimosContext _context;

        public LocalRepository(DbEmprestimosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocalReadDTO>> GetAllLocaisAsync(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Locais
                .Where(l => l.IdAssociacao == idAssociacao)
                .Select(l => new LocalReadDTO
                {
                    IdLocal = l.IdLocal,
                    NomeLocal = l.NomeLocal,
                    idAssociacao = l.IdAssociacao
                });

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedList<LocalReadDTO>(items, count, pageNumber, pageSize);
        }

        public async Task<Local> GetLocalByIdAsync(int id, int idAssociacao)
        {
            return await _context.Locais
                .Where(l => l.IdLocal == id && l.IdAssociacao == idAssociacao)
                .FirstOrDefaultAsync(l => l.IdLocal == id);
        }

        public async Task<Local> AddLocalAsync(Local local, int idAssociacao)
        {
            local.IdAssociacao = idAssociacao;
            _context.Locais.Add(local);
            await _context.SaveChangesAsync();
            return local;
        }

        public async Task UpdateLocalAsync(Local local, int idAssociacao)
        {
            var existingLocal = await _context.Locais
                .Where(l => l.IdLocal == local.IdLocal && l.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();

            if (existingLocal == null)
            {
                throw new KeyNotFoundException("Local não encontrado ou não pertence à associação.");
            }
            _context.Entry(existingLocal).CurrentValues.SetValues(local);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLocalAsync(int id, int idAssociacao)
        {
            var local = await _context.Locais
                .Where(l => l.IdLocal == id && l.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();
            if (local == null)
            {
                throw new KeyNotFoundException("Local não encontrado.");
            }

            var equipamentosCadastrados = await _context.Equipamentos
                                            .Where(e => e.IdLocal == id)
                                            .AnyAsync();

            if (equipamentosCadastrados)
            {
                throw new InvalidOperationException("Não é possível remover locais que tenham equipamentos vinculados.");
            }

            _context.Locais.Remove(local);
            await _context.SaveChangesAsync();
        }
    }
}
