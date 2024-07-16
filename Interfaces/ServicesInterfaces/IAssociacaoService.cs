using EmprestimosAPI.DTO.Associacao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmprestimosAPI.Interfaces.Services
{
    public interface IAssociacaoService
    {
        Task<IEnumerable<AssociacaoReadDTO>> GetAllAsync(int pageNumber, int pageSize);
        Task<AssociacaoReadDTO> GetByIdAsync(int id);
        Task<AssociacaoReadDTO> CreateAsync(AssociacaoCreateDTO associacaoDto);
        Task UpdateAsync(int id, AssociacaoUpdateDTO associacaoDto);
        Task DeleteAsync(int id);
        Task ChangeAssocPassword(int id, string newPassword);
    }
}
