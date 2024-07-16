using EmprestimosAPI.DTO.Categoria;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Interfaces.Services;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaReadDTO>> GetAllAsync(int pageNumber, int pageSize, int idAssociacao)
        {
            var categorias = await _repository.GetAllCategAsync(pageNumber, pageSize, idAssociacao);
            return categorias.Select(c => new CategoriaReadDTO
            {
                IdCategoria = c.IdCategoria,
                NomeCategoria = c.NomeCategoria,
                idAssociacao = c.IdAssociacao
            }).ToList();
        }

        public async Task<CategoriaReadDTO> GetByIdAsync(int id, int idAssociacao)
        {
            var categoria = await _repository.GetCategById(id, idAssociacao);
            if (categoria == null) return null;

            return new CategoriaReadDTO
            {
                IdCategoria = categoria.IdCategoria,
                NomeCategoria = categoria.NomeCategoria,
                idAssociacao = categoria.IdAssociacao
            };
        }

        public async Task<CategoriaReadDTO> CreateAsync(CategoriaCreateDTO categoriaDTO)
        {
            var categoria = new Categoria
            {
                NomeCategoria = categoriaDTO.NomeCategoria,
                IdAssociacao = categoriaDTO.idAssociacao
            };

            var newCategoria = await _repository.AddCateg(categoria, categoriaDTO.idAssociacao);

            return new CategoriaReadDTO
            {
                IdCategoria = newCategoria.IdCategoria,
                NomeCategoria = newCategoria.NomeCategoria,
                idAssociacao = newCategoria.IdAssociacao
            };
        }

        public async Task UpdateAsync(int id, CategoriaUpdateDTO categoriaDTO)
        {
            var categoria = await _repository.GetCategById(id, categoriaDTO.idAssociacao);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria Not Found");
            }

            categoria.NomeCategoria = categoriaDTO.NomeCategoria;
            categoria.IdAssociacao = categoriaDTO.idAssociacao;

            await _repository.UpdateCateg(categoria, categoriaDTO.idAssociacao);
        }

        public async Task DeleteAsync(int id, int idAssociacao)
        {
            var categoria = await _repository.GetCategById(id, idAssociacao);

            if(categoria == null)
            {
                throw new KeyNotFoundException("Categoria Not Found");
            }
            await _repository.DeleteCateg(id, idAssociacao);
        }

        
    }
}
