using EmprestimosAPI.Data;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmprestimosAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DbEmprestimosContext _context;

        public CategoriaRepository(DbEmprestimosContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Categoria>> GetAllCategAsync(int pageNumber, int pageSize, int idAssociacao)
        {
            var query = _context.Categorias
                .Where(c => c.IdAssociacao == idAssociacao)
                .AsQueryable();
            return await PagedList<Categoria>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Categoria> GetCategById(int id, int idAssociacao)
        {
            return await _context.Categorias
                .Where(c => c.IdCategoria == id && c.IdAssociacao == idAssociacao)
                .FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task<Categoria> AddCateg(Categoria categoria, int idAssociacao)
        {
            categoria.IdAssociacao = idAssociacao;
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task UpdateCateg(Categoria categoria, int idAssociacao)
        {
            var existingCateg = await _context.Categorias
                .Where(l => l.IdCategoria == categoria.IdCategoria && l.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();

            if (existingCateg == null)
            {
                throw new KeyNotFoundException("Local não encontrado ou não pertence à associação.");
            }
            _context.Entry(existingCateg).CurrentValues.SetValues(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCateg(int id, int idAssociacao)
        {
            var categoria = await _context.Categorias
                .Where(c => c.IdCategoria == id && c.IdAssociacao == idAssociacao)
                .SingleOrDefaultAsync();
            if(categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            var equipamentoCadastrado = await _context.Equipamentos
                                        .Where(e => e.IdCategoria == id)
                                        .AnyAsync();

            if (equipamentoCadastrado)
            {
                throw new InvalidOperationException("Não é possível remover uma categoria vinculada a um equipamento cadastrado.");
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
