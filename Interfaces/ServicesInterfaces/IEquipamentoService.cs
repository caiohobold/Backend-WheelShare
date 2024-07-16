using EmprestimosAPI.DTO.Equipamento;
using EmprestimosAPI.DTO.Local;
using EmprestimosAPI.DTO.Usuario;

namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface IEquipamentoService
    {
        Task<IEnumerable<EquipamentoReadDTO>> GetAllEquip(int pageNumber, int pageSize, int idAssociacao);
        Task<IEnumerable<EquipamentoReadDTO>> GetAllAvailableEquip(int pageNumber, int pageSize, int idAssociacao);
        Task<EquipamentoReadDTO> GetEquipById(int id, int idAssociacao);
        Task<EquipamentoReadDTO> AddEquip(EquipamentoCreateDTO usuarioDTO);
        Task UpdateEquip(int id, EquipamentoUpdateDTO usuarioDTO);
        Task UpdateLocal(int id, UpdateLocalDTO updateLocalDTO, int idAssociacao);
        Task UpdateEstado(int id, UpdateEstadoEquipamentoDTO updateEstadoDTO, int idAssociacao);
        Task DeleteEquip(int id, int idAssociacao);
    }
}
