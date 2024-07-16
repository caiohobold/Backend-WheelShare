using EmprestimosAPI.DTO.Categoria;

namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaReadDTO>> GetAllAsync(int pageNumber, int pageSize, int idAssociacao);
        Task<CategoriaReadDTO> GetByIdAsync(int id, int idAssociacao);
        Task<CategoriaReadDTO> CreateAsync(CategoriaCreateDTO categoriaDTO);
        Task UpdateAsync(int id, CategoriaUpdateDTO categoriaDTO);
        Task DeleteAsync(int id, int idAssociacao);
    }
}
