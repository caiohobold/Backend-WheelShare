using EmprestimosAPI.Data;
using EmprestimosAPI.DTO.Equipamento;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Runtime.InteropServices;

namespace EmprestimosAPI.Repositories
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly DbEmprestimosContext _context;

        public EquipamentoRepository(DbEmprestimosContext context)
        {
            _context = context;
        }

        public async Task<PagedList<EquipamentoReadDTO>> GetAllEquip(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Equipamentos
                .Where(p => p.IdAssociacao == idAssociacao)
                .Select(e => new EquipamentoReadDTO
                {
                    IdEquipamento = e.IdEquipamento,
                    NomeEquipamento = e.NomeEquipamento,
                    NomeCategoriaEquipamento = e.Categoria.NomeCategoria,
                    IdCategoria = e.Categoria.IdCategoria,
                    EstadoEquipamento = e.EstadoEquipamento,
                    CargaEquipamento = e.CargaEquipamento,
                    DescricaoEquipamento = e.DescricaoEquipamento,
                    IdLocal = e.IdLocal,
                    idAssociacao = e.IdAssociacao,
                    Foto1 = e.Foto1 != null ? Convert.ToBase64String(e.Foto1) : null,
                    Foto2 = e.Foto2 != null ? Convert.ToBase64String(e.Foto2) : null,
                    StatusEquipamento = _context.Emprestimos
                                        .Where(emp => emp.IdEquipamento == e.IdEquipamento)
                                        .OrderByDescending(emp => emp.Status == 0)
                                        .ThenByDescending(emp => emp.DataEmprestimo)
                                        .Select(emp => (int?)emp.Status)
                                        .FirstOrDefault() ?? -1
                });

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<EquipamentoReadDTO>(items, count, pageNumber, pageSize);
        }

        public async Task<PagedList<Equipamento>> GetAllAvailableEquip(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Equipamentos
                .Where(e => !_context.Emprestimos.Any(emp => emp.IdEquipamento == e.IdEquipamento && emp.Status == 0) && e.EstadoEquipamento != 2 && e.EstadoEquipamento != 3 && e.IdAssociacao == idAssociacao)
                .Include(e => e.Categoria);

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<Equipamento>(items, count, pageNumber, pageSize);
        }

        public async Task<Equipamento> GetEquipById(int id, int idAssociacao)
        {
            var equipamento = await _context.Equipamentos
                                .Where(e => e.IdEquipamento == id && e.IdAssociacao == idAssociacao)
                                .Include(e => e.Categoria)
                                .Include(e => e.Local)
                                .FirstOrDefaultAsync(e => e.IdEquipamento == id);

            return equipamento;
        }

        public async Task<Equipamento> AddEquip(Equipamento equipamento, int idAssociacao)
        {
            equipamento.IdAssociacao = idAssociacao;
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();
            return equipamento;
        }

        public async Task UpdateEquip(Equipamento equipamento, int idAssociacao)
        {
            var existingEquip = await _context.Equipamentos
                .Where(e => e.IdEquipamento == equipamento.IdEquipamento && e.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();

            if (existingEquip == null)
            {
                throw new KeyNotFoundException("Equipamento não encontrado ou não pertence à associação.");
            }

            _context.Entry(existingEquip).CurrentValues.SetValues(equipamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEquip(int id, int idAssociacao)
        {
            var equipamento = await _context.Equipamentos
                .Where(e => e.IdEquipamento == id && e.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();

            if(equipamento == null)
            {
                throw new KeyNotFoundException("Equipamento não encontrado ou não pertence à associação.");
            }

            var emprestimosAtivos = await _context.Emprestimos
                                        .Where(e => e.IdEquipamento == id && e.Status == 0 && e.IdAssociacao == idAssociacao)
                                        .AnyAsync();

            if (emprestimosAtivos)
            {
                throw new InvalidOperationException("Não é possível remover um equipamento que está atualmente emprestado.");
            }
            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();
        }
    }
}
