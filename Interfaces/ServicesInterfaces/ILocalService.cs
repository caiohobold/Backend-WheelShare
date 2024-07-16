using EmprestimosAPI.DTO.Local;

namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface ILocalService
    {
        Task<IEnumerable<LocalReadDTO>> GetAllLocaisAsync(int pageNumber, int pageSize, int idAssociacao);
        Task<LocalReadDTO> GetLocalByIdAsync(int id, int idAssociacao);
        Task<LocalReadDTO> AddLocalAsync(LocalCreateDTO localDTO);
        Task UpdateLocalAsync(int id, LocalUpdateDTO localDTO);
        Task DeleteLocalAsync(int id, int idAssociacao);
    }
}
