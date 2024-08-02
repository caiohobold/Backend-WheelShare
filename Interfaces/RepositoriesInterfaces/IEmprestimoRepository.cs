using EmprestimosAPI.DTO.Emprestimo;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface IEmprestimoRepository
    {
        Task<PagedList<Emprestimo>> GetAllEmp(int pageNumber, int pageSize, int idAssociacao);
        Task<Emprestimo> GetEmpById(int id, int idAssociacao);
        Task<IEnumerable<Emprestimo>> GetEmpByPessoaId(int idPessoa, int idAssociacao);
        Task<PagedList<Emprestimo>> GetActiveEmp(int pageNumber, int pageSize, int idAssociacao);
        Task<IEnumerable<Emprestimo>> GetEmpAtrasados(int idAssociacao);
        Task<IEnumerable<Emprestimo>> GetEmpAgendado(int idAssociacao);
        Task<Emprestimo> AddEmp(Emprestimo emprestimo, int idAssociacao);
        Task UpdateEmp(Emprestimo emprestimo, int idAssociacao);
        Task DeleteEmp(int id, int idAssociacao);
    }
}
