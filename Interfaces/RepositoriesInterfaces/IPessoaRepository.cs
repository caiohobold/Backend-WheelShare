using EmprestimosAPI.DTO.Pessoa;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface IPessoaRepository
    {
        Task<PagedList<PessoaReadDTO>> GetAllPessoasAsync(int pageNumber, int pageSize, int idAssociacao);
        Task<PessoaReadDTO> GetPessoaByIdAsync(int id, int idAssociacao);
        Task<Pessoa> AddPessoaAsync(Pessoa pessoa, int idAssociacao);
        Task UpdatePessoaAsync(Pessoa pessoa, int idAssociacao);
        Task DeletePessoaAsync(int id, int idAssociacao);
    }
}
