using EmprestimosAPI.Helpers;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface IAssociacaoRepository
    {
        Task<PagedList<Associacao>> GetAllAssocAsync(int pageNumber, int pageSize);
        Task<Associacao> GetAssocById(int id);
        Task<Associacao> GetAssocByEmailAsync(string email);
        Task<Associacao> AddAssoc(Associacao associacao);
        Task UpdateAssoc(Associacao associacao);
        Task DeleteAssoc(int id);
    }
}
