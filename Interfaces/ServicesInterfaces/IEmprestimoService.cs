using EmprestimosAPI.DTO.Emprestimo;
using EmprestimosAPI.DTO.Equipamento;

namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface IEmprestimoService
    {
        Task<IEnumerable<EmprestimoReadDTO>> GetAllEmp(int pageNumber, int pageSize, int idAssociacao);
        Task<EmprestimoReadDTO> GetEmpById(int id, int idAssociacao);
        Task<IEnumerable<EmprestimoReadDTO>> GetEmpByPessoaId(int idPessoa, int idAssociacao);
        Task<IEnumerable<EmprestimoReadDTO>> GetActiveEmp(int pageNumber, int pageSize, int idAssociacao);
        Task<IEnumerable<EmprestimoReadDTO>> GetEmpAtrasados(int idAssociacao);
        Task<IEnumerable<EmprestimoReadDTO>> GetEmpAgendado(int idAssociacao);
        Task<EmprestimoReadDTO> AddEmp(EmprestimoCreateDTO emprestimoDTO, int idAssociacao);
        Task UpdateEmp(int id, EmprestimoUpdateDTO emprestimoDTO, int idAssociacao);
        Task DeleteEmp(int id, int idAssociacao);
        Task FinalizarEmp(int id, int idAssociacao);
    }
}
