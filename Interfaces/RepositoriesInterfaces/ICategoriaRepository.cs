using EmprestimosAPI.Helpers;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface ICategoriaRepository
    {
        Task<PagedList<Categoria>> GetAllCategAsync(int pageNumber, int pageSize, int idAssociacao);
        Task<Categoria> GetCategById(int id, int idAssociacao);
        Task<Categoria> AddCateg(Categoria categoria, int idAssociacao);
        Task UpdateCateg(Categoria categoria, int idAssociacao);
        Task DeleteCateg(int id, int idAssociacao);
    }
}
