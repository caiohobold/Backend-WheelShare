using EmprestimosAPI.DTO.Equipamento;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface IEquipamentoRepository
    {
        Task<PagedList<EquipamentoReadDTO>> GetAllEquip(int pageNumber, int pageSize, int idAssociacao);
        Task<PagedList<Equipamento>> GetAllAvailableEquip(int pageNumber, int pageSize, int idAssociacao);
        Task<Equipamento> GetEquipById(int id, int idAssociacao);
        Task<Equipamento> AddEquip(Equipamento equipamento, int idAssociacao);
        Task UpdateEquip(Equipamento equipamento, int idAssociacao);
        Task DeleteEquip(int id, int idAssociacao);
    }
}
