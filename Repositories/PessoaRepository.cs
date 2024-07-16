using EmprestimosAPI.Data;
using EmprestimosAPI.DTO.Pessoa;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EmprestimosAPI.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DbEmprestimosContext _context;
        public PessoaRepository(DbEmprestimosContext context)
        {
            _context = context;
        }

        public async Task<PagedList<PessoaReadDTO>> GetAllPessoasAsync(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Pessoas
                .Where(p => p.IdAssociacao == idAssociacao)
                .Select(p => new PessoaReadDTO
                {
                    IdPessoa = p.IdPessoa,
                    NomeCompleto = p.NomeCompleto,
                    Cpf = p.Cpf,
                    Email = p.Email,
                    Telefone = p.Telefone,
                    Descricao = p.Descricao,
                    Endereco = p.Endereco,
                    idAssociacao = p.IdAssociacao,
                    StatusEmprestimo = _context.Emprestimos
                                       .Where(e => e.IdPessoa == p.IdPessoa)
                                       .OrderByDescending(e => e.Status == 0)
                                       .ThenByDescending(e => e.DataEmprestimo)
                                       .Select(e => (int?)e.Status)
                                       .FirstOrDefault() ?? -1,
                    DataEmprestimo = _context.Emprestimos
                                       .Where(e => e.IdPessoa == p.IdPessoa)
                                       .Select(e => (DateTime?)e.DataEmprestimo)
                                       .FirstOrDefault()

                });

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
           
            return new PagedList<PessoaReadDTO>(items, count, pageNumber, pageSize);
        }

        public async Task<PessoaReadDTO> GetPessoaByIdAsync(int id, int idAssociacao)
        {
            var pessoa = await _context.Pessoas
                .Where(p => p.IdPessoa == id && p.IdAssociacao == idAssociacao)
                .Select(p => new PessoaReadDTO
                {
                    IdPessoa = p.IdPessoa,
                    NomeCompleto = p.NomeCompleto,
                    Cpf = p.Cpf,
                    Email = p.Email,
                    Telefone = p.Telefone,
                    Descricao = p.Descricao,
                    Endereco = p.Endereco,
                    idAssociacao = p.IdAssociacao,
                    StatusEmprestimo = _context.Emprestimos
                               .Where(e => e.IdPessoa == p.IdPessoa)
                               .OrderByDescending(e => e.Status == 0)
                               .ThenByDescending(e => e.DataEmprestimo)
                               .Select(e => (int?)e.Status)
                               .FirstOrDefault() ?? -1,
                    DataEmprestimo = _context.Emprestimos
                                       .Where(e => e.IdPessoa == p.IdPessoa)
                                       .Select(e => (DateTime?)e.DataEmprestimo)
                                       .FirstOrDefault()
                }).SingleOrDefaultAsync();

            return pessoa;
        }

        public async Task<Pessoa> AddPessoaAsync(Pessoa pessoa, int idAssociacao)
        {
            pessoa.IdAssociacao = idAssociacao;
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task UpdatePessoaAsync(Pessoa pessoa, int idAssociacao)
        {
            var existingPessoa = await _context.Pessoas
                .Where(p => p.IdPessoa == pessoa.IdPessoa && p.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();

            if (existingPessoa == null)
            {
                throw new KeyNotFoundException("Pessoa não encontrada ou não pertence à associação.");
            }

            _context.Entry(existingPessoa).CurrentValues.SetValues(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePessoaAsync(int id, int idAssociacao)
        {
            var pessoa = await _context.Pessoas
                .Where(p => p.IdPessoa == id && p.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();
            if (pessoa == null)
            {
                throw new KeyNotFoundException("Pessoa não encontrada ou não pertence à associação.");
            }

            var emprestimosAtivos = await _context.Emprestimos
               .Where(e => e.IdPessoa == id && e.Status == 0)
               .AnyAsync();

            if (emprestimosAtivos)
            {
                throw new InvalidOperationException("Não é possível remover uma pessoa que possui empréstimos em andamento.");
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            
        }
    }
}
