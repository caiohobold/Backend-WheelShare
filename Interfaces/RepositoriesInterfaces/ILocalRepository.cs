using EmprestimosAPI.DTO.Local;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface ILocalRepository
    {
        Task<IEnumerable<LocalReadDTO>> GetAllLocaisAsync(int pageNumber, int pageSize, int idAssociacao);
        Task<Local> GetLocalByIdAsync(int id, int idAssociacao);
        Task<Local> AddLocalAsync(Local local, int idAssociacao);
        Task UpdateLocalAsync(Local local, int idAssociacao);
        Task DeleteLocalAsync(int id, int idAssociacao);
    }
}
