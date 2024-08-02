using EmprestimosAPI.Data;
using EmprestimosAPI.DTO.Emprestimo;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Diagnostics.CodeAnalysis;

namespace EmprestimosAPI.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly DbEmprestimosContext _context;
        
        public EmprestimoRepository(DbEmprestimosContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Emprestimo>> GetAllEmp(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Emprestimos
                .Where(e => e.IdAssociacao == idAssociacao)
                .Include(e => e.Pessoa)
                .Include(e => e.Usuario)
                .Include(e => e.Equipamento);

            var count = await query.CountAsync();

            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Emprestimo>(items, count, pageNumber, pageSize);

        }

        public async Task<Emprestimo> GetEmpById(int id, int idAssociacao)
        {
            var emprestimo = await _context.Emprestimos
                                            .Where(e => e.Id == id && e.IdAssociacao == idAssociacao)
                                            .Include(e => e.Pessoa)
                                            .Include(e => e.Usuario)
                                            .Include(e => e.Equipamento)
                                            .FirstOrDefaultAsync(e => e.Id == id);

            return emprestimo;
        }

        public async Task<IEnumerable<Emprestimo>> GetEmpByPessoaId(int idPessoa, int idAssociacao)
        {
            return await _context.Emprestimos
                .Include(e => e.Pessoa)
                .Include(e => e.Equipamento)
                .Where(e => e.IdPessoa == idPessoa && e.IdAssociacao == idAssociacao)
                .ToListAsync();
        }

        public async Task<PagedList<Emprestimo>> GetActiveEmp(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Emprestimos
                .Include(e => e.Pessoa)
                .Include(e => e.Usuario)
                .Include(e => e.Equipamento)
                .Where(e => e.Status == 0 && e.IdAssociacao == idAssociacao)
                .OrderBy(e => e.DataDevolucaoEmprestimo);

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Emprestimo>(items, count, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Emprestimo>> GetEmpAtrasados(int idAssociacao)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Emprestimos
                .Include(e => e.Pessoa)
                .Include(e => e.Equipamento)
                .Include(e => e.Usuario)
                .Where(e => e.Status == 0 && e.DataDevolucaoEmprestimo <= today && e.IdAssociacao == idAssociacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Emprestimo>> GetEmpAgendado(int idAssociacao)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Emprestimos
                .Include(e => e.Pessoa)
                .Include(e => e.Equipamento)
                .Include(e => e.Usuario)
                .Where(e => e.Status == 0 && e.DataEmprestimo > today && e.IdAssociacao == idAssociacao)
                .ToListAsync();
        }

        public async Task<Emprestimo> AddEmp(Emprestimo emprestimo, int idAssociacao)
        {

            var emprestimoAtivo = await _context.Emprestimos
                .AnyAsync(e => e.IdEquipamento == emprestimo.IdEquipamento && e.Status == 0 && e.IdAssociacao == idAssociacao);

            if (emprestimoAtivo)
            {
                throw new InvalidOperationException("Este equipamento já está emprestado.");
            }
            emprestimo.IdAssociacao = idAssociacao;
            _context.Entry(emprestimo).State = EntityState.Modified;
            _context.Emprestimos.Add(emprestimo);
            await _context.SaveChangesAsync();
            return emprestimo;
        }

        public async Task UpdateEmp(Emprestimo emprestimo, int idAssociacao)
        {
            var existingEmprestimo = await _context.Emprestimos
                .Where(e => e.Id == emprestimo.Id && e.IdAssociacao == idAssociacao)
                .FirstOrDefaultAsync();

            if (existingEmprestimo == null)
            {
                throw new KeyNotFoundException("Emprestimo não encontrado");
            }
            emprestimo.DataEmprestimo = DateTime.SpecifyKind(emprestimo.DataEmprestimo, DateTimeKind.Utc);
            emprestimo.DataDevolucaoEmprestimo = DateTime.SpecifyKind(emprestimo.DataDevolucaoEmprestimo, DateTimeKind.Utc);

            _context.Entry(emprestimo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmp(int id, int idAssociacao)
        {
            var emprestimo = await _context.Emprestimos
                .Where(e => e.Id == id && e.IdAssociacao == idAssociacao)
                .FirstOrDefaultAsync();

            if (emprestimo == null)
            {
                throw new KeyNotFoundException("Emprestimo não encontrado");
            }

            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();
        }
    }
}
